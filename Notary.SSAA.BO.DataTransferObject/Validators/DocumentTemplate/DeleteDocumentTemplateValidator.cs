using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentTemplate;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentTemplate;

public sealed class DeleteDocumentTemplateValidator
    : AbstractValidator<DeleteDocumentTemplateCommand>
{
    public DeleteDocumentTemplateValidator()
    {
        RuleFor(x => x.DocumentTemplateId)
            .Must(ValidatorHelper.BeValidGuid)
            .WithMessage(SystemMessagesConstant.DocumentTemplate_Id_Invalid);
    }
}
