using FluentValidation;
using  Notary.SSAA.BO.DataTransferObject.Queries.Document;
using  Notary.SSAA.BO.Utilities.Extensions;

namespace  Notary.SSAA.BO.DataTransferObject.Validators.Document
{
    public class GetDocumentInfoJudgmentByIdValidator : AbstractValidator<GetDocumentInfoJudgmentByIdQuery>
    {
        public GetDocumentInfoJudgmentByIdValidator()
        {
            RuleFor(v => v.DocumentId)
           .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه معتبر نیست");
        }
    }
}
