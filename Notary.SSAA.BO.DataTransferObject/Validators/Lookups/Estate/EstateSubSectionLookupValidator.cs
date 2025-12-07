using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Lookups.Estate
{
    public class EstateSubSectionLookupValidator : AbstractValidator<EstateSubSectionLookupQuery>
    {
        public EstateSubSectionLookupValidator()
        {
            RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage("مقدار شماره صقحه غیر مجاز است")
            .NotNull().WithMessage("فیلد شماره صفحه اجباری است");
            RuleFor(x => x.PageSize)
             .ExclusiveBetween(0, 11).WithMessage("مقدار اندازه صقحه غیر مجاز است")
             .NotNull().WithMessage("فیلد شماره صفحه اجباری است");
            RuleFor(x => x.ExtraParams)
            .NotNull().WithMessage("ابتدا  بخش را انتخاب کنید");
            RuleFor(x => x.ExtraParams.SectionId)
            .NotEmpty().WithMessage("ابتدا بخش را انتخاب کنید");
        }
    }
}
