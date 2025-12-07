using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Circular;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Services.Circular
{
    public class CircularTypeLookupValidator : AbstractValidator<CircularTypeLookupQuery>
    {
        public CircularTypeLookupValidator()
        {
            RuleFor(v => v.PageIndex)
                .GreaterThanOrEqualTo(1).WithMessage("مقدار شماره صقحه غیر مجاز است")
                .NotNull().WithMessage("فیلد شماره صفحه اجباری است");

            RuleFor(v => v.PageSize)
             .ExclusiveBetween(0, 11).WithMessage("مقدار اندازه صقحه غیر مجاز است")
             .NotNull().WithMessage("فیلد شماره صفحه اجباری است");

        }
    }
}
