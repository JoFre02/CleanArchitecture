using Application.Interfaces.TransferenciaExterna;
using Domain.Enums;

namespace Infrastructure.Antifraude.DataAccess
{
    public class EvaluarFraudeReader : IEvaluarFraudeReader
    {
        public Task<NivelRiesgo> EvaluarAsync(string cuentaOrigen, decimal monto)
        {
            NivelRiesgo nivel = monto switch
            {
                > 50000 => NivelRiesgo.Alto,
                > 10000 => NivelRiesgo.Medio,
                _ => NivelRiesgo.Bajo
            };

            return Task.FromResult(nivel);
        }
    }
}