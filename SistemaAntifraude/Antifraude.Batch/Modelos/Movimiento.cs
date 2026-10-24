namespace Antifraude.Batch.Modelos
{
    // Una transferencia ya realizada por la cuenta (solo los datos que el batch necesita).
    public record Movimiento(decimal Monto, DateTime Fecha);
}
