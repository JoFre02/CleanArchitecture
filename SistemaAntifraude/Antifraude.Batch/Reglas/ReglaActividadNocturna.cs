using Antifraude.Batch.Modelos;

namespace Antifraude.Batch.Reglas
{
    // Operar de madrugada (00:00 - 05:59) es sospechoso solo si el cliente NO suele hacerlo.
    // Por eso se compara la actividad de las últimas 24 h con su comportamiento anterior.
    public class ReglaActividadNocturna : IReglaFraude
    {
        private const double ProporcionHabitual = 0.2;

        private static bool EsMadrugada(Movimiento m) => m.Fecha.Hour < 6;

        public ResultadoRegla Evaluar(ContextoCuenta contexto)
        {
            var hace24h = contexto.Ahora.AddHours(-24);
            int nocturnasRecientes = contexto.Historial.Count(m => m.Fecha >= hace24h && EsMadrugada(m));

            if (nocturnasRecientes == 0)
                return ResultadoRegla.SinRiesgo(nameof(ReglaActividadNocturna));

            // Patrón habitual = movimientos anteriores a las últimas 24 h.
            var anteriores = contexto.Historial.Where(m => m.Fecha < hace24h).ToList();
            double proporcion = anteriores.Count == 0 ? 0 : anteriores.Count(EsMadrugada) / (double)anteriores.Count;

            return proporcion < ProporcionHabitual
                ? new(nameof(ReglaActividadNocturna), 15, $"{nocturnasRecientes} operación(es) de madrugada, fuera de su patrón habitual")
                : ResultadoRegla.SinRiesgo(nameof(ReglaActividadNocturna));
        }
    }
}
