using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Lookups
{
    public class SignRequestOriginalPersonLookupValidator : AbstractValidator<SignRequestOriginalPersonLookupQuery>
    {
        public SignRequestOriginalPersonLookupValidator()
        {
            RuleFor(v => v.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage(SystemMessagesConstant.Grid_PageIndex_Invalid)
            .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);
            RuleFor(v => v.PageSize)
             .ExclusiveBetween(0, 11).WithMessage(SystemMessagesConstant.Grid_PageSize_Invalid)
             .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);
            RuleFor(v => v.ExtraParams.SignRequestId)
                .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه معتبر نیست");
        }
    }
}
