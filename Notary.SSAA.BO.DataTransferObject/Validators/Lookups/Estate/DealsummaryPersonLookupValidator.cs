using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.SharedKernel.Constants;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Lookups.Estate
{
    public class DealsummaryPersonLookupValidator : AbstractValidator<DealSummaryPersonLookupQuery>
    {
        public DealsummaryPersonLookupValidator()
        {
            RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage(SystemMessagesConstant.Grid_PageIndex_Invalid)
            .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);
            RuleFor(x => x.PageSize)
             .ExclusiveBetween(0, 11).WithMessage(SystemMessagesConstant.Grid_PageSize_Invalid)
             .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);
            RuleFor(x => x.ExtraParams)
            .NotNull().WithMessage("شناسه خلاصه معامله اجباری می باشد");
            RuleFor(x => x.ExtraParams.DealSummaryId)
            .NotEmpty().WithMessage("شناسه خلاصه معامله اجباری می باشد");
        }
    }
}
