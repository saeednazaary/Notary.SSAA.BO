using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Document;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Document
{
    public class UpdateDocumentPaymentStateValidator : AbstractValidator<UpdateDocumentPaymentStateCommand>
    {
        public UpdateDocumentPaymentStateValidator()
        {
            RuleFor(v => v.DocumentNo)
                .NotEmpty()
                .WithMessage("شناسه معتبر نیست");
        }
    }
}
