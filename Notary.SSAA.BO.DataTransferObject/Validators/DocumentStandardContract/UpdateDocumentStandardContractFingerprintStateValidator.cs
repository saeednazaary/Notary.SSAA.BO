using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentStandardContract
{
    public class UpdateDocumentStandardContractFingerprintStateValidator : AbstractValidator<UpdateDocumentStandardContractFingerprintStateCommand>
    {
        public UpdateDocumentStandardContractFingerprintStateValidator()
        {
            RuleFor(v => v.DocumentId)
                .Must(ValidatorHelper.BeValidGuid)
                .WithMessage("شناسه معتبر نیست");
        }
    }
}
