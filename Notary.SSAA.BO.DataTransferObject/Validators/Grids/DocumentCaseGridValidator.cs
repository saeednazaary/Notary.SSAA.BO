using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Grids
{
    public sealed class DocumentCaseGridValidator : AbstractValidator<DocumentCaseGridQuery>
    {
        public DocumentCaseGridValidator()
        {
            RuleFor(v => v.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage("مقدار شماره صقحه غیر مجاز است")
            .NotNull().WithMessage("فیلد شماره صفحه اجباری است");

            RuleFor(v => v.PageSize)
             .ExclusiveBetween(0, 11).WithMessage("مقدار اندازه صقحه غیر مجاز است")
             .NotNull().WithMessage("فیلد شماره صفحه اجباری است");

            RuleForEach(v => v.SelectedItems).Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه آیتم انتخاب شده معتبر نیست . ");

        }
    }
}
