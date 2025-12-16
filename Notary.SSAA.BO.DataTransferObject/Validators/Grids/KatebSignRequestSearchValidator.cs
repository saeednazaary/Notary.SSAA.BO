

using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Grids
{
    public class KatebSignRequestSearchValidator : AbstractValidator<KatebSignRequestSearchQuery>
    {
        public KatebSignRequestSearchValidator()
        {
            RuleFor(v => v.PageIndex)
           .GreaterThanOrEqualTo(1).WithMessage(SystemMessagesConstant.Grid_PageIndex_Invalid)
           .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);

            RuleFor(v => v.PageSize)
             .ExclusiveBetween(0, 11).WithMessage(SystemMessagesConstant.Grid_PageSize_Invalid)
             .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);

            RuleFor(v => v.ExtraParams.FromDate).Must(ValidatorHelper.BeValidPersianDate).WithMessage("فیلد تاریخ درخواست از معتبر نمیباشد")
              .When(x => x.ExtraParams is not null && !x.ExtraParams.FromDate.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.ToDate).Must(ValidatorHelper.BeValidPersianDate).WithMessage("فیلد تاریخ درخواست تا معتبر نمیباشد")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.ToDate.IsNullOrWhiteSpace());
        }
    }
}
