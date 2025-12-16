using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint
{
    public class CreatePersonFingerprintValidator_V2 : AbstractValidator<CreatePersonFingerprintV2Command>
    {
        public CreatePersonFingerprintValidator_V2()
        {
            
            RuleFor(v => v.PersonObjectId)
                 .Must(ValidatorHelper.BeValidGuid).When(x=>x.ClientId!="7").WithMessage("شناسه شخص اجباری است .");
            RuleFor(v => v.MainObjectId)
     .Must(ValidatorHelper.BeValidGuid).When(x => x.ClientId != "7").WithMessage("شناسه مدرک مربوظه اجباری است .");
            RuleFor(v => v.ClientId)
                .NotNull().WithMessage("شناسه سیستم اجباری است");
            RuleFor(v => v.IsTFARequired)
                .NotNull().WithMessage("آیا احراز هویت دو مرحله ای اجباری است ؟ اجباری است ");
            RuleFor(v => v.PersonNationalNo)
               //.Must(ValidatorHelper.BeValidPersianNationalNumber).WithMessage("شماره ملی شخص اجباری است . ");
               .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار فیلد شماره ملی غیر مجاز است")
                .Length(10).WithMessage("طول شماره ملی غیر مجاز است")
                 .NotEmpty().WithMessage("فیلد شماره ملی اجباری است");
            RuleFor(v => v.PersonMobileNo)
                .Must(ValidatorHelper.BeValidMobileNumber).When(x => x.ClientId != "7").WithMessage("شماره موبایل شخص اجباری است .");
            RuleFor(v => v.PersonNameFamily)
                .NotEmpty().When(x => x.ClientId != "7").WithMessage("نام شخص اجباری است .");
        }
    }
}
