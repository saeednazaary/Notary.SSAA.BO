using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids.Estate;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Grids.Estate
{
    public class DealSummarySelectableGridValidator : AbstractValidator<DealSummarySelectableGridQuery>
    {
        public DealSummarySelectableGridValidator()
        {
            RuleFor(x => x.ExtraParams).NotNull().WithMessage("از تاریخ و تا تاریخ اجباری و باید حداکثر بازه 6 ماهه باشد");
            RuleFor(x => x.ExtraParams.DS_DateFrom).NotEmpty().When(x => x.ExtraParams != null).WithMessage("آیتم از تاریخ  اجباری می باشد");
            RuleFor(x => x.ExtraParams.DS_DateTo).NotEmpty().When(x => x.ExtraParams != null).WithMessage("آیتم تا تاریخ اجباری می باشد");
            RuleFor(x => x.ExtraParams.DS_Status).NotEmpty().When(x => x.ExtraParams != null).WithMessage("انتخاب وضعیت اجباری می باشد");
            RuleFor(x => x.ExtraParams.DS_DateFrom).Must(fromDate => fromDate.IsValidDate()).When(x => x.ExtraParams != null && !string.IsNullOrWhiteSpace(x.ExtraParams.DS_DateFrom)).WithMessage("فرمت آیتم از تاریخ نادرست می باشد");
            RuleFor(x => x.ExtraParams.DS_DateTo).Must(ToDate => ToDate.IsValidDate()).When(x => x.ExtraParams != null && !string.IsNullOrWhiteSpace(x.ExtraParams.DS_DateTo)).WithMessage("فرمت آیتم تا تاریخ نادرست می باشد");
            RuleFor(x => x.ExtraParams).Must(ep =>
            {
                var distance = ep.DS_DateTo.GetDateTimeDistance(ep.DS_DateFrom);
                if (distance.TotalDays <= 186)
                {
                    return true;
                }
                return false;
            }).When(x => x.ExtraParams != null && !string.IsNullOrWhiteSpace(x.ExtraParams.DS_DateFrom) && x.ExtraParams.DS_DateFrom.IsValidDate() && !string.IsNullOrWhiteSpace(x.ExtraParams.DS_DateTo) && x.ExtraParams.DS_DateTo.IsValidDate()).WithMessage("بازه جستجو نمی تواند بیش از 6 ماه باشد");
            RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage(SystemMessagesConstant.Grid_PageIndex_Invalid)
            .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);
            RuleFor(x => x.PageSize)
             .ExclusiveBetween(0, 11).WithMessage(SystemMessagesConstant.Grid_PageSize_Invalid)
             .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);

        }
    }
}
