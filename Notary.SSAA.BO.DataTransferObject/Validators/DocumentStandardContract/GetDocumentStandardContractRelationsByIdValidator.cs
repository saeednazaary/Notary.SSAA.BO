using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentStandardContract
{
    public class GetDocumentStandardContractRelationsByIdValidator
        : AbstractValidator<GetDocumentStandardContractRelationsByIdQuery>
    {
        public GetDocumentStandardContractRelationsByIdValidator()
        {
            RuleFor(v => v.DocumentId)
                .Must(ValidatorHelper.BeValidGuid)
                .WithMessage("شناسه معتبر نیست");
        }
    }
}
