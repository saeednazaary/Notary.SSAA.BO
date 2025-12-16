using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Circular;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Services.Circular
{
    public class CircularAdvancedSearchValidator : AbstractValidator<CircularAdvancedSearchQuery>
    {
        public CircularAdvancedSearchValidator()
        {
            RuleFor(v => v.PageIndex)
                           .GreaterThanOrEqualTo(1).WithMessage(SystemMessagesConstant.Grid_PageIndex_Invalid)
                           .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);

            RuleFor(v => v.PageSize)
             .ExclusiveBetween(0, 11).WithMessage(SystemMessagesConstant.Grid_PageSize_Invalid)
             .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);

            RuleForEach(v => v.SelectedItems).Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه آیتم انتخاب شده معتبر نیست . ");

            RuleForEach(v => v.ExtraParams.CircularProvinceId).Must(ValidatorHelper.BeValidNumber).WithMessage("شناسه استان معتبر نیست")
                .When(v => v.ExtraParams is not null);

            RuleForEach(v => v.ExtraParams.CircularUnitId).Must(ValidatorHelper.BeValidNumber).WithMessage("شناسه واحد ثبتی معتبر نیست")
                .When(v => v.ExtraParams is not null);

            RuleForEach(v => v.ExtraParams.CircularTypeId).Must(ValidatorHelper.BeValidNumber).WithMessage("شناسه نوع بخشنامه معتبر نیست")
                .When(v => v.ExtraParams is not null);

            RuleForEach(v => v.ExtraParams.FollowingCircularId).Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه بخشنامه های ثبتی معتبر نیست")
                .When(v => v.ExtraParams is not null);

            RuleForEach(v => v.ExtraParams.CircularItemTypeId).Must(ValidatorHelper.BeValidNumber).WithMessage("شناسه بی اعتباری بخشنامه های ثبتی معتبر نیست")
                .When(v => v.ExtraParams is not null);

            RuleFor(v => v.ExtraParams.CircularIssueDate).Must(ValidatorHelper.BeValidPersianDate).WithMessage("تاریخ بخشنامه معتبر نیست")
                .When(v => v.ExtraParams is not null && !string.IsNullOrWhiteSpace(v.ExtraParams.CircularIssueDate));

            RuleFor(v => v.ExtraParams.FollowingCircularDate).Must(ValidatorHelper.BeValidPersianDate).WithMessage("تاریخ پیروی معتبر نیست")
                .When(v => v.ExtraParams is not null && !string.IsNullOrWhiteSpace(v.ExtraParams.FollowingCircularDate));

            RuleFor(v => v.ExtraParams.CircularCourtDate).Must(ValidatorHelper.BeValidPersianDate).WithMessage("تاریخ مرجع قضایی معتبر نیست")
                .When(v => v.ExtraParams is not null && !string.IsNullOrWhiteSpace(v.ExtraParams.CircularCourtDate));

            RuleFor(v => v.ExtraParams.CircularBaseClaimerDate).Must(ValidatorHelper.BeValidPersianDate).WithMessage("تاریخ نامه مرجع اعلام کننده معتبر نیست")
                .When(v => v.ExtraParams is not null && !string.IsNullOrWhiteSpace(v.ExtraParams.CircularBaseClaimerDate));

            RuleFor(v => v.ExtraParams.CircularLastLegalPaperDate).Must(ValidatorHelper.BeValidPersianDate).WithMessage("تاريخ آخرين روزنامه رسمي/گواهي ثبت شرکت ها معتبر نیست")
                .When(v => v.ExtraParams is not null && !string.IsNullOrWhiteSpace(v.ExtraParams.CircularLastLegalPaperDate));

            RuleFor(v => v.ExtraParams.CircularStateId).Must(x => ValidatorHelper.ValidateRangeValue(x, 1, 2)).WithMessage("وضعیت معتبر نیست")
                .When(v => v.ExtraParams is not null && !string.IsNullOrWhiteSpace(v.ExtraParams.CircularStateId));
        }
    }
}
