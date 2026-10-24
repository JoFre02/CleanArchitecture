namespace Antifraude.Batch.Modelos
{
    // Resultado del batch para una cuenta: lo que luego se guarda en la tabla contrato.
    public record PerfilRiesgo(
        NivelRiesgo NivelBase,
        int Puntaje,
        decimal UmbralMedio,
        decimal UmbralAlto,
        IReadOnlyList<string> Motivos);
}
