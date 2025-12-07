using FluentValidation;
using  Notary.SSAA.BO.DataTransferObject.Queries.Document;
using  Notary.SSAA.BO.Utilities.Extensions;

namespace  Notary.SSAA.BO.DataTransferObject.Validators.DocumentPayments
{
    public class DocumentPaymentsConfirmValidator : AbstractValidator<DocumentPaymentsConfirmQuery>
    {
        public DocumentPaymentsConfirmValidator()
        {
            RuleFor(v => v.DocumentId)
                .Must(ValidatorHelper.BeValidGuid)
                .WithMessage("شناسه معتبر نیست");
        }
    }
}