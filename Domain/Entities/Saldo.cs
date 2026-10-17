using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Saldo
    {
        public string NumeroCuenta { get; set; } = string.Empty;
        public decimal Monto { get; set; }
    }
}