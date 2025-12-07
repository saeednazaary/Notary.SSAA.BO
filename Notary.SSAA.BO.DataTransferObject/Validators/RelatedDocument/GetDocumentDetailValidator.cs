using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.RelatedDocument;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.RelatedDocument
{
    public class GetDocumentDetailValidator : AbstractValidator<GetDocumentDetailByIdQuery>
    {
        public GetDocumentDetailValidator()
        {
            RuleFor(v => v.DocumentId)
               .Must(ValidatorHelper.BeValidGuid)
               .WithMessage("شناسه معتبر نیست");
        }
    }
}