using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentTemplate;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentTemplate
{
    public class GetDocumenttemplateByIdValidator : AbstractValidator<GetDocumenttemplateByIdQuery>
    {
        public GetDocumenttemplateByIdValidator()
        {
            RuleFor(x => x.DocumentTemplateId).Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه مورد نظر معتبر نمیباشد . ");
        }
    }
}
