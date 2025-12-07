using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Document;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Document
{
    public class UpdateDocumentAfterPaidValidator : AbstractValidator<UpdateDocumentAfterPaidCommand>
    {
        public UpdateDocumentAfterPaidValidator()
        {
            RuleFor(v => v.DocumentNo)
                .NotEmpty()
                .WithMessage("شناسه معتبر نیست");
        }
    }
}
