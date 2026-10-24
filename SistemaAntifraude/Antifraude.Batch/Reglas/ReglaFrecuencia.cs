using Antifraude.Batch.Modelos;

namespace Antifraude.Batch.Reglas
{
    // Muchas operaciones seguidas suelen indicar que alguien tomó control
    // de la cuenta y la está "vaciando" rápidamente.
    public class ReglaFrecuencia : IReglaFraude
    {
        private static readonly TimeSpan Ventana = TimeSpan.FromHours(1);

        public ResultadoRegla Evaluar(ContextoCuenta contexto)
        {
            var desde = contexto.Ahora - Ventana;
            int recientes = contexto.Historial.Count(m => m.Fecha >= desde);

            return recientes switch
            {
                >= 10 => new(nameof(ReglaFrecuencia), 60, $"{recientes} operaciones en la última hora"),
                >= 6 => new(nameof(ReglaFrecuencia), 40, $"{recientes} operaciones en la última hora"),
                >= 3 => new(nameof(ReglaFrecuencia), 20, $"{recientes} operaciones en la última hora"),
                _ => ResultadoRegla.SinRiesgo(nameof(ReglaFrecuencia))
            };
        }
    }
}
