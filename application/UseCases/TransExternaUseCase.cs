using Application.Adapters;
using Application.Executors;
using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;

namespace Application.UseCases
{
    public class TransExternaUseCase : ITransExternaUseCase
    {
        private readonly ICuentasReader _cuentasReader;
        private readonly ICuentasWriter _cuentasWriter;
        private readonly ISaldosReader _saldosReader;
        private readonly ISaldosWriter _saldosWriter;
        private readonly IMovimientosReader _movimientosReader;
        private readonly IMovimientosWriter _movimientosWriter;
        private readonly IRegistrarMovimientoWriter _camaraCompensacion;
        private readonly IEvaluarFraudeReader _antifraude;
        private readonly IEnviarNotificacionWriter _notificaciones;

        private readonly IValidator<TransferenciaRequest> _requestValidator;
        private readonly IValidator<TraceIdentifierAdapter> _traceValidator;

        public TransExternaUseCase(
            ICuentasReader cuentasReader,
            ICuentasWriter cuentasWriter,
            ISaldosReader saldosReader,
            ISaldosWriter saldosWriter,
            IMovimientosReader movimientosReader,
            IMovimientosWriter movimientosWriter,
            IRegistrarMovimientoWriter camaraCompensacion,
            IEvaluarFraudeReader antifraude,
            IEnviarNotificacionWriter notificaciones,
            IValidator<TransferenciaRequest> requestValidator,
            IValidator<TraceIdentifierAdapter> traceValidator
            )
        {
            _cuentasReader = cuentasReader;
            _cuentasWriter = cuentasWriter;
            _saldosReader = saldosReader;
            _saldosWriter = saldosWriter;
            _movimientosReader = movimientosReader;
            _movimientosWriter = movimientosWriter;
            _camaraCompensacion = camaraCompensacion;
            _antifraude = antifraude;
            _notificaciones = notificaciones;
            _requestValidator = requestValidator;
            _traceValidator = traceValidator;
        }

        public async Task<Result<TransferenciaResponse>> ExecuteAsync(TransferenciaRequest request, TraceIdentifierAdapter trace)
        {
            // 1. Validar trazabilidad
            var validacionTrace = await _traceValidator.ValidateAsync(trace);
            if (!validacionTrace.IsValid)
            {
                var error = validacionTrace.Errors[0];
                return Result<TransferenciaResponse>.Error(error.ErrorMessage, error.ErrorCode);
            }

            // 2. Validar el payload
            var validacionRequest = await _requestValidator.ValidateAsync(request);
            if (!validacionRequest.IsValid)
            {
                var error = validacionRequest.Errors[0];
                return Result<TransferenciaResponse>.Error(error.ErrorMessage, error.ErrorCode);
            }

            var cuenta = await _cuentasReader.ObtenerPorNumeroAsync(request.CuentaOrigen);
            if (cuenta is null || !cuenta.Activa)
                return Result<TransferenciaResponse>.Error("Cuenta no encontrada o inactiva", "CUENTA_INVALIDA");

            var saldo = await _saldosReader.ObtenerPorCuentaAsync(request.CuentaOrigen);
            decimal comision = 5; 
            bool esInterbancaria = !string.IsNullOrEmpty(request.BancoDestino) && request.BancoDestino != cuenta.Banco;
            comision = esInterbancaria ? 5 : 0;

            if (saldo is null || saldo.Monto < (request.Monto + comision))
                return Result<TransferenciaResponse>.Error("Saldo insuficiente", "SALDO_INSUFICIENTE");

            var movimientosHoy = await _movimientosReader.ObtenerPorCuentaAsync(request.CuentaOrigen, DateTime.Today);
            decimal totalHoy = movimientosHoy.Sum(m => m.Monto);
            if (totalHoy + request.Monto > cuenta.LimiteDiario)
                return Result<TransferenciaResponse>.Error("Límite diario excedido", "LIMITE_EXCEDIDO");

            if (request.Monto >= 10000 && string.IsNullOrEmpty(request.CodigoOtp))
                return Result<TransferenciaResponse>.Error("Se requiere código OTP para este monto", "OTP_REQUERIDO");

            var nivelRiesgo = await _antifraude.EvaluarAsync(request.CuentaOrigen, request.Monto);
            if (nivelRiesgo == NivelRiesgo.Alto)
                return Result<TransferenciaResponse>.Error("Operación bloqueada por riesgo alto", "RIESGO_ALTO");

            if (nivelRiesgo == NivelRiesgo.Medio && string.IsNullOrEmpty(request.CodigoOtp))
                return Result<TransferenciaResponse>.Error("Se requiere confirmación adicional por riesgo medio", "RIESGO_MEDIO_OTP_REQUERIDO");

            var transferencia = new Transferencia
            {
                CuentaOrigen = request.CuentaOrigen,
                CuentaDestino = request.CuentaDestino,
                BancoDestino = request.BancoDestino,
                Monto = request.Monto,
                Comision = comision,
                Fecha = DateTime.Now,
                CodigoOperacion = Guid.NewGuid().ToString("N")[..10].ToUpper(),
                EsInterbancaria = esInterbancaria
            };

            bool enviado = await _camaraCompensacion.EnviarACamaraCompensacionAsync(transferencia);
            if (!enviado)
                return Result<TransferenciaResponse>.Error("No se pudo procesar en la cámara de compensación", "ERROR_CAMARA_COMPENSACION");

            saldo.Monto -= (request.Monto + comision);
            await _saldosWriter.ActualizarAsync(saldo);

            bool registrado = await _movimientosWriter.RegistrarAsync(transferencia);
            if (!registrado)
                return Result<TransferenciaResponse>.Error("No se pudo registrar el movimiento", "ERROR_REGISTRO_MOVIMIENTO");

            await _notificaciones.EnviarAsync(cuenta.TitularDni, $"Transferencia realizada por S/ {request.Monto}. Código: {transferencia.CodigoOperacion}");

            var response = new TransferenciaResponse
            {
                CodigoOperacion = transferencia.CodigoOperacion,
                Comision = comision,
                SaldoActual = saldo.Monto
            };

            return Result<TransferenciaResponse>.Ok(response, "Transferencia realizada con éxito");
        }
    }
}