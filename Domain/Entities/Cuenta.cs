using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Cuenta
    {
        public string NumeroCuenta { get; set; } = string.Empty;
        public string TitularDni { get; set; } = string.Empty;
        public string Banco { get; set; } = string.Empty;
        public bool Activa { get; set; }
        public CategoriaCliente Segmento { get; set; }
        public decimal LimiteDiario { get; set; }
    }
}