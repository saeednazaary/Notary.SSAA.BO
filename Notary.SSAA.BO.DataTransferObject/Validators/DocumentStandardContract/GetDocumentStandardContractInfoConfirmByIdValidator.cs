using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentStandardContract
{
    public class GetDocumentStandardContractInfoConfirmByIdValidator : AbstractValidator<GetDocumentStandardContractInfoConfirmByIdQuery>
    {
        public GetDocumentStandardContractInfoConfirmByIdValidator()
        {
            RuleFor(v => v.DocumentId)
           .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه معتبر نیست");
        }
    }
}
