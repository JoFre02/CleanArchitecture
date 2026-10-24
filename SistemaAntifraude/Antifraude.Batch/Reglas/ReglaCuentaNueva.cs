using Antifraude.Batch.Modelos;

namespace Antifraude.Batch.Reglas
{
    // Una cuenta sin historial no permite saber qué es "normal" para ella,
    // y las cuentas recién abiertas son muy usadas en fraudes.
    public class ReglaCuentaNueva : IReglaFraude
    {
        public const int MinimoMovimientos = 3;

        public ResultadoRegla Evaluar(ContextoCuenta contexto)
        {
            int total = contexto.Historial.Count;

            return total < MinimoMovimientos
                ? new(nameof(ReglaCuentaNueva), 15, $"Cuenta con pocos movimientos ({total})")
                : ResultadoRegla.SinRiesgo(nameof(ReglaCuentaNueva));
        }
    }
}
