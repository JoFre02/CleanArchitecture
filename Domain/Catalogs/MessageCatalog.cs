using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Catalogs
{
    public static class MessageCatalog
    {
        private static readonly Dictionary<int, string> _errores = new()
        {
            // Generales Device Identifier
            { 21002, "{PropertyName} es obligatorio" },
            { 21004, "{PropertyName} debe tener al menos {MinLength} caracteres" },
            { 21005, "{PropertyName} no debe superar los {MaxLength} caracteres" },

            // Transferencia
            { 21006, "El monto debe ser mayor a 0" },
            { 21007, "La cuenta destino no puede ser igual a la cuenta origen" },
            { 21008, "El código OTP debe tener 6 dígitos" },
            
        };
       
        public static string GetErrorByCode(int codigo) =>
            _errores.TryGetValue(codigo, out var mensaje) ? mensaje : "Error no catalogado";
    }
}
