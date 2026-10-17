using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.CoreBancario.DataAccess
{
    public class CuentasReader : ICuentasReader
    {
        public Task<Cuenta?> ObtenerPorNumeroAsync(string numeroCuenta)
        {
            var cuenta = new Cuenta
            {
                NumeroCuenta = numeroCuenta,
                TitularDni = "12345678",
                Banco = "BancoMock",
                Activa = true,
                Segmento = CategoriaCliente.Estandar,
                LimiteDiario = 15000
            };

            return Task.FromResult<Cuenta?>(cuenta);
        }
    }
}