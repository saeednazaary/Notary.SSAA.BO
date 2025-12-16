using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentStandardContract
{
    public class UpdateDocumentStandardContractPaymentStateValidator : AbstractValidator<UpdateDocumentStandardContractPaymentStateCommand>
    {
        public UpdateDocumentStandardContractPaymentStateValidator()
        {
            RuleFor(v => v.DocumentNo)
                .NotEmpty()
                .WithMessage("شناسه معتبر نیست");
        }
    }
}
