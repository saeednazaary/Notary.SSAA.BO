using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentStandardContract
{
    public class UpdateDocumentStandardContractAfterPaidValidator : AbstractValidator<UpdateDocumentStandardContractAfterPaidCommand>
    {
        public UpdateDocumentStandardContractAfterPaidValidator()
        {
            RuleFor(v => v.DocumentNo)
                .NotEmpty()
                .WithMessage("شناسه معتبر نیست");
        }
    }
}
