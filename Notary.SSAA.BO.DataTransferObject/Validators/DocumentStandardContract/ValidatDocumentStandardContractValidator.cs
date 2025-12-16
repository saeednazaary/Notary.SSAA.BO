using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Document;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentStandardContract
{
    public class ValidatDocumentStandardContractValidator : AbstractValidator<ValidatDocumentStandardContractQuery>
    {
        public ValidatDocumentStandardContractValidator()
        {
            RuleFor(x => x.DocumentDate)
            .Must(ValidatorHelper.BeValidPersianDate).WithMessage("مقدار تاریخ سند غیر مجاز است")
            .NotEmpty().WithMessage("فیلد تاریخ سند اجباری است");

            RuleFor(x => x.DocumentNationalNo)
            .MaximumLength(18).WithMessage("طول شماره سند بیشتر از حد مجاز است ")
            .NotEmpty().WithMessage("فیلد شماره سند اجباری است");

            RuleFor(x => x.DocumentScriptoriumId)
            .NotEmpty().WithMessage("دفترخانه صادر کننده سند اجباری است");

            RuleFor(x => x.DocumentTypeId)
            .NotEmpty().WithMessage("فیلد نوع سند اجباری است");

            RuleFor(x => x.DocumentDate)
                .GreaterThan("1392/06/26").WithMessage("تاریخ وارد شده برای سند وابسته در بخش اطلاعات سند وابسته،قبل از راه اندازی سامانه ثبت الکترونیک اسناد می باشد")
                .When(x => x.IsRelatedDocumentInSSAR == true);
        }
    }
}
