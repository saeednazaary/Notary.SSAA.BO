using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentTemplate;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentTemplate
{
    public sealed class GetDocumenttemplateByIdValidator
        : AbstractValidator<GetDocumenttemplateByIdQuery>
    {
        public GetDocumenttemplateByIdValidator()
        {
            RuleFor(x => x.DocumentTemplateId)
                .Must(ValidatorHelper.BeValidGuid)
                .WithMessage(SystemMessagesConstant.DocumentTemplate_Id_NotValid);
        }
    }
}
