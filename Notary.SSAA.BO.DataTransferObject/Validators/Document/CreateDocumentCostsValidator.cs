using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Document;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Document
{
    public class CreateDocumentCostsValidator : AbstractValidator<CreateDocumentCostsCommand>
    {
        public CreateDocumentCostsValidator()
        {
            RuleFor(v => v.DocumentId)
    .Must(ValidatorHelper.BeValidGuid)
    .WithMessage("شناسه معتبر نیست");


        }
    }
}

