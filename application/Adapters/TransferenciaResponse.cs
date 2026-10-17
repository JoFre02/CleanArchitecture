namespace Application.Adapters
{
    public class TransferenciaResponse
    {
        public string CodigoOperacion { get; set; } = string.Empty;
        public decimal Comision { get; set; }
        public decimal SaldoActual { get; set; }
    }
}