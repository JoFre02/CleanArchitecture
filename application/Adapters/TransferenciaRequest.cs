using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Adapters
{
    public class TransferenciaRequest
    {
        public string CuentaOrigen { get; set; } = string.Empty;
        public string CuentaDestino { get; set; } = string.Empty;
        public string BancoDestino { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string? CodigoOtp { get; set; } // solo viene si el monto es >= S/10,000
    }
}