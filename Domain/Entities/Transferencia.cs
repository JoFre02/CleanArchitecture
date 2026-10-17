using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Transferencia
    {
        public string CuentaOrigen { get; set; } = string.Empty;
        public string CuentaDestino { get; set; } = string.Empty;
        public string BancoDestino { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public decimal Comision { get; set; }
        public DateTime Fecha { get; set; }
        public string CodigoOperacion { get; set; } = string.Empty;
        public bool EsInterbancaria { get; set; }
    }
}