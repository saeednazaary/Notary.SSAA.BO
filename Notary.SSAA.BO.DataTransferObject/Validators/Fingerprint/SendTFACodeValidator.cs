using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint
{
    public class SendTFACodeValidator : AbstractValidator<SendTFACodeCommand>
    {
        public SendTFACodeValidator()
        {
            RuleFor(v => v.FingerprintId)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه اثر انگشت اجباری است .");
        }
    }
    public class SendTFACodeValidator_V2 : AbstractValidator<SendTFACodeV2Command>
    {
        public SendTFACodeValidator_V2()
        {
            RuleFor(v => v.FingerprintId)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه اثر انگشت اجباری است .");
        }
    }
}
