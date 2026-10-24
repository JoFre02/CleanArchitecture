using Antifraude.Batch.Modelos;

namespace Antifraude.Batch.Datos
{
    // Tablas del Core Bancario que el batch solo LEE (su fuente de datos).
    public class CuentaTable
    {
        public string NumeroCuenta { get; set; } = string.Empty;
        public bool Activa { get; set; }
    }

    public class MovimientoTable
    {
        public string CodigoOperacion { get; set; } = string.Empty;
        public string CuentaOrigen { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }

    // Tabla que el batch ESCRIBE: es el contrato con el sistema de transferencias.
    public class PerfilRiesgoCuentaTable
    {
        public string NumeroCuenta { get; set; } = string.Empty;
        public NivelRiesgo NivelBase { get; set; }
        public decimal UmbralMedio { get; set; }
        public decimal UmbralAlto { get; set; }
        public string? Motivos { get; set; }
        public DateTime FechaCalculo { get; set; }
    }
}
