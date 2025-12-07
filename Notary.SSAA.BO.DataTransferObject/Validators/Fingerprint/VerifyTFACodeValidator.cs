using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint
{
    public class VerifyTFACodeValidator : AbstractValidator<VerifyTFACodeCommand>
    {
        public VerifyTFACodeValidator()
        {
            RuleFor(v => v.FingerprintId)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه اثر انگشت اجباری است .");
            RuleFor(v => v.TFACode)
                .Length(6).WithMessage("کد دو مرحله ای مجاز نیست . ");
        }
    }
    public class VerifyTFACodeValidator_V2 : AbstractValidator<VerifyTFACodeV2Command>
    {
        public VerifyTFACodeValidator_V2()
        {
            RuleFor(v => v.FingerprintId)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه اثر انگشت اجباری است .");
            RuleFor(v => v.TFACode)
                .Length(6).WithMessage("کد دو مرحله ای مجاز نیست . ");
        }
    }
}
