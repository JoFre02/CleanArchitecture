namespace Antifraude.Batch.Modelos
{
    // Lo que devuelve cada regla: cuántos puntos de riesgo suma y por qué.
    public record ResultadoRegla(string Regla, int Puntos, string Motivo)
    {
        public static ResultadoRegla SinRiesgo(string regla) => new(regla, 0, "Sin observaciones");
    }
}
