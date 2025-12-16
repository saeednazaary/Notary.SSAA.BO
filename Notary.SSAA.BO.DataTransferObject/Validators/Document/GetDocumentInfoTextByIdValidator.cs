using FluentValidation;
using  Notary.SSAA.BO.DataTransferObject.Queries.Document;
using  Notary.SSAA.BO.Utilities.Extensions;

namespace  Notary.SSAA.BO.DataTransferObject.Validators.Document
{
    public class GetDocumentInfoTextByIdValidator : AbstractValidator<GetDocumentInfoTextByIdQuery>
    {
        public GetDocumentInfoTextByIdValidator()
        {
            RuleFor(v => v.DocumentId)
           .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه معتبر نیست");
        }
    }
}
