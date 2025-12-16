using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint
{

    public class GetPersonLastFingerprintValidator : AbstractValidator<GetPersonLastFingerprintQuery>
    {
        public GetPersonLastFingerprintValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty()
                .WithMessage("نام کاربری اجباری است.")
                .WithErrorCode("110");

            RuleFor(v => v.Password)
                .NotEmpty()
                .WithMessage("رمز عبور اجباری است.")
                .WithErrorCode("111");

            RuleFor(v => v.PersonNationalNo)
                .NotEmpty()
                .WithMessage("کد ملی شخص نمی‌تواند خالی باشد.")
                .WithErrorCode("112");

            RuleFor(v => v.DocumentId)
                .NotEmpty()
                .WithMessage("شناسه سند نمی‌تواند خالی باشد.")
                .WithErrorCode("113");

            RuleFor(v => v.SelectedFinger)
                .GreaterThan(0)
                .WithMessage("کد انگشت شخص مشخص نشده است.")
                .WithErrorCode("114");
        }
    }

}
