using Antifraude.Batch.Modelos;
using Antifraude.Batch.Reglas;

namespace Antifraude.Batch.Perfil
{
    // Corre el lote de reglas sobre UNA cuenta y arma su perfil de riesgo.
    // No toca la base de datos: así se puede probar con datos inventados.
    public class CalculadoraPerfil
    {
        private const int PuntosMedio = 30;
        private const int PuntosAlto = 60;

        private static readonly IReglaFraude[] Reglas =
        [
            new ReglaFrecuencia(),
            new ReglaActividadNocturna(),
            new ReglaCuentaNueva()
        ];

        public PerfilRiesgo Calcular(ContextoCuenta contexto)
        {
            // Paso 1: ejecutar todas las reglas y quedarnos con las que sumaron puntos.
            var observaciones = Reglas
                .Select(r => r.Evaluar(contexto))
                .Where(r => r.Puntos > 0)
                .ToList();

            // Paso 2: convertir el puntaje total en el nivel base de la cuenta.
            int puntaje = observaciones.Sum(r => r.Puntos);
            NivelRiesgo nivelBase = puntaje switch
            {
                >= PuntosAlto => NivelRiesgo.Alto,
                >= PuntosMedio => NivelRiesgo.Medio,
                _ => NivelRiesgo.Bajo
            };

            // Paso 3: calcular los montos límite personalizados.
            var (umbralMedio, umbralAlto) = CalculadoraUmbrales.Calcular(contexto.Historial);

            var motivos = observaciones.Select(r => $"{r.Regla}(+{r.Puntos}): {r.Motivo}").ToList();
            return new PerfilRiesgo(nivelBase, puntaje, umbralMedio, umbralAlto, motivos);
        }
    }
}
