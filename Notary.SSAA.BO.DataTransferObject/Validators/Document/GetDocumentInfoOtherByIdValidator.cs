using FluentValidation;
using  Notary.SSAA.BO.DataTransferObject.Queries.Document;
using  Notary.SSAA.BO.Utilities.Extensions;

namespace  Notary.SSAA.BO.DataTransferObject.Validators.Document
{
    public class GetDocumentInfoOtherByIdValidator : AbstractValidator<GetDocumentInfoOtherByIdQuery>
    {
        public GetDocumentInfoOtherByIdValidator()
        {
            RuleFor(v => v.DocumentId)
           .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه معتبر نیست");
        }
    }
}
