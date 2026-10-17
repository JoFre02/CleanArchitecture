using Domain.Enums;

namespace Application.Interfaces.TransferenciaExterna
{
    public interface IEvaluarFraudeReader
    {
        Task<NivelRiesgo> EvaluarAsync(string cuentaOrigen, decimal monto);
    }
}