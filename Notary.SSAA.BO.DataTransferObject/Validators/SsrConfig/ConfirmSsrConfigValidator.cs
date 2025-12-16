using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.SsrConfig;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SsrConfig
{

    public class ConfirmSsrConfigValidator : AbstractValidator<ConfirmSsrConfigCommand>
    {
        public ConfirmSsrConfigValidator()
        {
            RuleFor(x => x.ConfigId)
                .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه پیکربندی اجباری است .");
        }
    }
}
