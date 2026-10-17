namespace Infrastructure.CoreBancario.DataModels
{
    // Representa las tablas del Core Bancario (mock)
    public class CuentaTable
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public string TitularDni { get; set; } = string.Empty;
        public bool Activa { get; set; }
    }
}