using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Document;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Document
{
    public class GetDocumentByIdValidator : AbstractValidator<GetDocumentByIdQuery>
    {
        public GetDocumentByIdValidator()
        {
            RuleFor(v => v.DocumentId)
                .Must(ValidatorHelper.BeValidGuid)
                .WithMessage("شناسه معتبر نیست");
        }
    }
    public class GetDetailsOfDocumentValidator : AbstractValidator<GetDetailsOfDocumentQuery>
    {
        public GetDetailsOfDocumentValidator ( )
        {
            RuleFor ( v => v.DocumentId )
                .Must ( ValidatorHelper.BeValidGuid )
                .WithMessage ( "شناسه معتبر نیست" );
        }
    }
}
