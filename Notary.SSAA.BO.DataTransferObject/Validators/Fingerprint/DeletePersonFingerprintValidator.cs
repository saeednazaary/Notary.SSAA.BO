using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint
{
    public class DeletePersonFingerprintValidator : AbstractValidator<DeletePersonFingerprintCommand>
    {
        public DeletePersonFingerprintValidator()
        {
            RuleFor(v => v.FingerprintId)
                .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه اثر انگشت اجباری است .");
        }
    }
    public class DeletePersonFingerprintValidator_V2 : AbstractValidator<DeletePersonFingerprintV2Command>
    {
        public DeletePersonFingerprintValidator_V2()
        {
            RuleFor(v => v.FingerprintId)
                .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه اثر انگشت اجباری است .");
        }
    }
}
