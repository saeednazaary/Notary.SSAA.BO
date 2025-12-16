using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentTemplate;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentTemplate
{
    public sealed class CreateDocumentTemplateValidator
        : AbstractValidator<CreateDocumentTemplateCommand>
    {
        public CreateDocumentTemplateValidator()
        {
            RuleFor(v => v.IsNew)
                .Equal(true)
                .WithMessage(SystemMessagesConstant.Request_IsNew_Invalid);

            RuleFor(v => v.IsDelete)
                .Equal(false)
                .WithMessage(SystemMessagesConstant.Request_IsDelete_Invalid);

            RuleFor(v => v.DocumentTemplateStateId)
                .Must(value => ValidatorHelper.ValidateRangeValue(value, 1, 2))
                .WithMessage(SystemMessagesConstant.DocumentTemplate_State_Invalid);

            RuleFor(v => v.DocumentTemplateCode)
                .Must(ValidatorHelper.BeValidNumber)
                    .WithMessage(SystemMessagesConstant.DocumentTemplate_Code_Invalid)
                .MaximumLength(5)
                    .WithMessage(SystemMessagesConstant.DocumentTemplate_Code_MaxLength);

            RuleFor(x => x.DocumentTypeId)
                .Must(x => x != null && x.Count == 1)
                .WithMessage(SystemMessagesConstant.DocumentType_Count_Invalid)
                .DependentRules(() =>
                {
                    RuleForEach(v => v.DocumentTypeId)
                        .Must(ValidatorHelper.BeValidNumber)
                            .WithMessage(SystemMessagesConstant.DocumentType_Id_Invalid)
                        .MaximumLength(4)
                            .WithMessage(SystemMessagesConstant.DocumentType_Id_MaxLength);
                });

            RuleFor(v => v.DocumentTemplateTitle)
                .NotEmpty()
                    .WithMessage(SystemMessagesConstant.DocumentTemplateTitle_Required)
                .MaximumLength(100)
                    .WithMessage(SystemMessagesConstant.DocumentTemplateTitle_MaxLength);
        }
    }
}
