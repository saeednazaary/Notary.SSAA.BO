

using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Grids
{
    public class KatebSignRequestSearchValidator : AbstractValidator<KatebSignRequestSearchQuery>
    {
        public KatebSignRequestSearchValidator()
        {
            RuleFor(v => v.PageIndex)
           .GreaterThanOrEqualTo(1).WithMessage("مقدار شماره صقحه غیر مجاز است")
           .NotNull().WithMessage("فیلد شماره صفحه اجباری است");

            RuleFor(v => v.PageSize)
             .ExclusiveBetween(0, 11).WithMessage("مقدار اندازه صقحه غیر مجاز است")
             .NotNull().WithMessage("فیلد شماره صفحه اجباری است");

            RuleFor(v => v.ExtraParams.FromDate).Must(ValidatorHelper.BeValidPersianDate).WithMessage("فیلد تاریخ درخواست از معتبر نمیباشد")
              .When(x => x.ExtraParams is not null && !x.ExtraParams.FromDate.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.ToDate).Must(ValidatorHelper.BeValidPersianDate).WithMessage("فیلد تاریخ درخواست تا معتبر نمیباشد")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.ToDate.IsNullOrWhiteSpace());
        }
    }
}
