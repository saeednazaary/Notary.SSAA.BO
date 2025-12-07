using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentModify;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentModify
{
    public class GetDocumentModifyByIdValidator : AbstractValidator<GetDocumentModifyByIdQuery>
    {
        public GetDocumentModifyByIdValidator()
        {
            RuleFor(v => v.DocumentModifyId)
                .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه معتبر نیست");
        }
    }
}
