using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Document;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Document
{
    public class AutoGenerateOwnershipDocsValidator : AbstractValidator<AutoGenerateOwnershipDocsCommand>
    {
        public AutoGenerateOwnershipDocsValidator()
        {
            RuleFor(v => v.RestRegisterServiceReqID)
                .Must(ValidatorHelper.BeValidGuid)
                .WithMessage("شناسه معتبر نیست");
        }
    }
}
