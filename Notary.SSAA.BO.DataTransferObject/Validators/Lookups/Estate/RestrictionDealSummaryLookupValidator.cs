using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Lookups.Estate
{
    public class RestrictionDealSummaryLookupValidator : AbstractValidator<RestrictionDealSummaryLookupQuery>
    {
        public RestrictionDealSummaryLookupValidator()
        {
            RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage("مقدار شماره صقحه غیر مجاز است")
            .NotNull().WithMessage("فیلد شماره صفحه اجباری است");
            RuleFor(x => x.PageSize)
             .ExclusiveBetween(0, 11).WithMessage("مقدار اندازه صقحه غیر مجاز است")
             .NotNull().WithMessage("فیلد شماره صفحه اجباری است");
            RuleFor(x => x.ExtraParams)
            .NotNull().WithMessage("ابتدا دفترخانه را انتخاب کنید");
            RuleFor(x => x.ExtraParams.ScriptoriumId)
            .NotEmpty().WithMessage("ابتدا دفترخانه را انتخاب کنید");
        }

    }
}
