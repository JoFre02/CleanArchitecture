namespace Antifraude.Batch.Modelos
{
    // Todo lo que una regla necesita para evaluar una cuenta: su historial
    // y el momento en que corre el batch (se recibe como dato para poder probarlo).
    public record ContextoCuenta(
        string NumeroCuenta,
        IReadOnlyList<Movimiento> Historial,
        DateTime Ahora);
}
