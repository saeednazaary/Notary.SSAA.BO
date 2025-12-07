using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public class GetSignRequestDocumentTemplateByIdValidator : AbstractValidator<GetSignRequestDocumentTemplateByIdQuery>
    {
        public GetSignRequestDocumentTemplateByIdValidator()
        {
            RuleFor(x => x.DocumentTemplateId).Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه مورد نظر معتبر نمیباشد . ");
        }
    }
}
