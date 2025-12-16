using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.SsrConfig;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SsrConfig
{
    public class GetSsrConfigByIdValidator: AbstractValidator<GetSsrConfigByIdQuery>
    {
        public GetSsrConfigByIdValidator()
        {
            RuleFor(v => v.SsrConfigId)
                .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه معتبر نیست");
        }
    }
}
