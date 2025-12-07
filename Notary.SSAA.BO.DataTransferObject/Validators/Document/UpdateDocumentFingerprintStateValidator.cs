using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Document;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Document
{
    public class UpdateDocumentFingerprintStateValidator : AbstractValidator<UpdateDocumentFingerprintStateCommand>
    {
        public UpdateDocumentFingerprintStateValidator()
        {
            RuleFor(v => v.DocumentId)
                .Must(ValidatorHelper.BeValidGuid)
                .WithMessage("شناسه معتبر نیست");
        }
    }
}
