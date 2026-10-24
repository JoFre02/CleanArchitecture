using Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace Application.Adapters
{
    public class TraceIdentifierAdapter
    {
        public string? DeviceIdentifier { get; set; }
        public string? MessageIdentifier { get; set; }
        public string? ChannelIdentifier { get; set; }
    }
    public class TraceIdentifierAdapterValidator : AbstractValidator<TraceIdentifierAdapter>
    {
        public TraceIdentifierAdapterValidator()
        {
            RuleFor(x => x.DeviceIdentifier)
                .NotEmpty().WithMessage(MessageCatalog.GetErrorByCode(21002)).WithErrorCode("21002")
                .MinimumLength(5).WithMessage(MessageCatalog.GetErrorByCode(21004)).WithErrorCode("21004")
                .MaximumLength(42).WithMessage(MessageCatalog.GetErrorByCode(21005)).WithErrorCode("21005");

            RuleFor(x => x.MessageIdentifier)
                .NotEmpty().WithMessage(MessageCatalog.GetErrorByCode(21002)).WithErrorCode("21002")
                .MinimumLength(5).WithMessage(MessageCatalog.GetErrorByCode(21004)).WithErrorCode("21004")
                .MaximumLength(42).WithMessage(MessageCatalog.GetErrorByCode(21005)).WithErrorCode("21005");

            RuleFor(x => x.ChannelIdentifier)
                .NotEmpty().WithMessage(MessageCatalog.GetErrorByCode(21002)).WithErrorCode("21002")
                .MinimumLength(5).WithMessage(MessageCatalog.GetErrorByCode(21004)).WithErrorCode("21004")
                .MaximumLength(42).WithMessage(MessageCatalog.GetErrorByCode(21005)).WithErrorCode("21005");
        }
    }


}
