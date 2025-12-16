using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.SharedKernel.Constants;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Grids
{
    public class LastSsrConfigAdvancedSearchValidator : AbstractValidator<LastSsrConfigAdvancedSearchQuery>
    {
        public LastSsrConfigAdvancedSearchValidator()
        {
            RuleFor(v => v.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage(SystemMessagesConstant.Grid_PageIndex_Invalid)
            .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);

            RuleFor(v => v.PageSize)
             .ExclusiveBetween(0, 11).WithMessage(SystemMessagesConstant.Grid_PageSize_Invalid)
             .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);

        }
    }
}
