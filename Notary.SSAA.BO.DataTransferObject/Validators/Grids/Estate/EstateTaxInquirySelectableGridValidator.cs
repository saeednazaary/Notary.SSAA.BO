using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids.Estate;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Grids.Estate
{
    public class EstateTaxInquirySelectableGridValidator : AbstractValidator<EstateTaxInquirySelectableGridQuery>
    {
        public EstateTaxInquirySelectableGridValidator()
        {

            RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage("مقدار شماره صقحه غیر مجاز است")
            .NotNull().WithMessage("فیلد شماره صفحه اجباری است");
            RuleFor(x => x.PageSize)
             .ExclusiveBetween(0, 11).WithMessage("مقدار اندازه صقحه غیر مجاز است")
             .NotNull().WithMessage("فیلد شماره صفحه اجباری است");

        }

    }
}
