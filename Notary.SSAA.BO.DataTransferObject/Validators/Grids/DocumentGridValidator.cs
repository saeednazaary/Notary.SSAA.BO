using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Grids
{
    public sealed class DocumentGridValidator : AbstractValidator<DocumentGridQuery>
    {
        public DocumentGridValidator()
        {
            RuleFor(v => v.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage(SystemMessagesConstant.Grid_PageIndex_Invalid)
            .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);

            RuleFor(v => v.PageSize)
             .ExclusiveBetween(0, 11).WithMessage(SystemMessagesConstant.Grid_PageSize_Invalid)
             .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);

            RuleForEach(v => v.SelectedItems).Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه آیتم انتخاب شده معتبر نیست . ");

            RuleFor(v => v.Extraparams.StateId).Must(value => ValidatorHelper.ValidateRangeValue(value, 1, 2)).WithMessage("شناسه وضعیت معتبر نمیباشد").When(v=>v.Extraparams is not null
             && !string.IsNullOrWhiteSpace(v.Extraparams?.StateId));

            RuleFor(v => v.Extraparams.DocumentTypeId).Must(ValidatorHelper.BeValidNumber).WithMessage("شناسه نوع سند معتبر نمیباشد").When(v=>v.Extraparams is not null 
            && !string.IsNullOrWhiteSpace(v.Extraparams?.DocumentTypeId));

            RuleFor(v => v.Extraparams.DocumentTypeGroupOneId).Must(ValidatorHelper.BeValidNumber).WithMessage("شناسه سطح اول گروه‌بندي اسناد و خدمات ثبتي معتبر نمیباشد")
                .When(v=>v.Extraparams is not null && !string.IsNullOrWhiteSpace(v.Extraparams?.DocumentTypeGroupOneId) );

            RuleFor(v => v.Extraparams.DocumentTypeGroupTwoId).Must(ValidatorHelper.BeValidNumber).WithMessage("شناسه سطح دوم گروه‌بندي اسناد و خدمات ثبتي معتبر نمیباشد")
                .When(v=>v.Extraparams is not null && !string.IsNullOrWhiteSpace(v.Extraparams?.DocumentTypeGroupTwoId));
        }
    }
}
