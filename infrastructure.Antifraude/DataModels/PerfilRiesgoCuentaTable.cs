using Domain.Enums;

namespace Infrastructure.Antifraude.DataModels
{
    // Representa la tabla contrato que publica el Sistema Antifraude (solo lectura para nosotros)
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
