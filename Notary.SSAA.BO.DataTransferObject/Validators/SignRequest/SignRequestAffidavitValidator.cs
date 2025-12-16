using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public class SignRequestAffidavitValidator : AbstractValidator<SignRequestAffidavitQuery>
    {

        public SignRequestAffidavitValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty()
                .WithMessage("نام کاربری اجباری است.")
                .WithErrorCode("110");

            RuleFor(v => v.Password)
                .NotEmpty()
                .WithMessage("رمز عبور اجباری است.")
                .WithErrorCode("111");

            RuleFor(v => v.SignRequestNationalNo)
                .NotEmpty()
                .WithMessage("شناسه یکتا گواهی امضا اجباری است.")
                .WithErrorCode("112")
                .Length(18)
                .WithMessage("شناسه یکتا گواهی امضا معتبر نیست.")
                .WithErrorCode("113");

            RuleFor(v => v.SignRequestSecretCode)
                .NotEmpty()
                .WithMessage("رمز تصدیق گواهی امضا اجباری است.")
                .WithErrorCode("114")
                .Length(6)
                .WithMessage("رمز تصدیق گواهی امضا معتبر نیست.")
                .WithErrorCode("115");
        }
    }
}

