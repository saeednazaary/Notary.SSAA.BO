using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint
{
    public class UndoPersonFingerprintValidator : AbstractValidator<UndoPersonFingerprintCommand>
    {
        public UndoPersonFingerprintValidator()
        {
            RuleFor(v => v.FingerprintId)
                .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه اثر انگشت اجباری است .");
        }
    }
    public class UndoPersonFingerprintValidator_V2 : AbstractValidator<UndoPersonFingerprintV2Command>
    {
        public UndoPersonFingerprintValidator_V2()
        {
            RuleFor(v => v.FingerprintId)
                .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه اثر انگشت اجباری است .");
        }
    }
}
