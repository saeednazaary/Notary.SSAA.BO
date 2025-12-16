using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Document;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentStandardContract
{
    public class GetDocumentStandardContractPersonFingerprintValidator : AbstractValidator<GetDocumentStandardContractPersonFingerprintQuery>
    {
        public GetDocumentStandardContractPersonFingerprintValidator()
        {
            RuleFor(v => v.DocumentId)
                .Must(ValidatorHelper.BeValidGuid)
                .WithMessage("شناسه معتبر نیست");
        }
    }
}
