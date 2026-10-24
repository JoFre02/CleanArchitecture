using Domain.Catalogs;
using FluentValidation;
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
    public class TransferenciaRequestValidator : AbstractValidator<TransferenciaRequest>
    {
        public TransferenciaRequestValidator()
        {
            RuleFor(x => x.CuentaOrigen)
                .NotEmpty().WithMessage(MessageCatalog.GetErrorByCode(21002)).WithErrorCode("21002");

            RuleFor(x => x.CuentaDestino)
                .NotEmpty().WithMessage(MessageCatalog.GetErrorByCode(21002)).WithErrorCode("21002")
                .NotEqual(x => x.CuentaOrigen).WithMessage(MessageCatalog.GetErrorByCode(21007)).WithErrorCode("21007");

            RuleFor(x => x.Monto)
                .GreaterThan(0).WithMessage(MessageCatalog.GetErrorByCode(21006)).WithErrorCode("21006");

            RuleFor(x => x.CodigoOtp)
                .Length(6).WithMessage(MessageCatalog.GetErrorByCode(21008)).WithErrorCode("21008")
                .When(x => !string.IsNullOrEmpty(x.CodigoOtp));
        }
    }
}