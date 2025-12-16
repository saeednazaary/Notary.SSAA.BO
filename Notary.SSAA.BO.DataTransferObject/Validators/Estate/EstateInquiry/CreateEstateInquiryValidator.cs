using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Text.RegularExpressions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry
{
    public class CreateEstateInquiryValidator : AbstractValidator<CreateEstateInquiryCommand>
    {
        public CreateEstateInquiryValidator()
        {
            //RuleFor(x => x.IsValid)
            //    .Must(x => x == true).WithMessage("وضعیت استعلام نا معتبر است");

            RuleFor(x => x.IsNew)
                .Must(x => x == true).WithMessage("وضعیت استعلام جدید نمیباشد");

            RuleFor(x => x.IsDelete)
                .Must(x => x == false).WithMessage("وضعیت استعلام حذف شده میباشد");

            RuleFor(x => x.InqId).Empty().WithMessage("شناسه استعلام باید خالی باشد");
            RuleFor(x => x.InqInquiryNo).NotEmpty().WithMessage("شماره استعلام اجباری می باشد");
            RuleFor(x => x.InqUnitId).Must(x => x != null && x.Count > 0).WithMessage("انتخاب گیرنده استعلام (واحد ثبتی) اجباری می باشد");
            RuleFor(x => x.InqGeoLocationId).Must(x => x != null && x.Count > 0).WithMessage("انتخاب شهر ملک اجباری می باشد");
            RuleFor(x => x.InqEstateSectionId).Must(x => x != null && x.Count > 0).WithMessage("انتخاب بخش(ثبتی) ملک اجباری می باشد");
            RuleFor(x => x.InqEstateSubsectionId).Must(x => x != null && x.Count > 0).WithMessage("انتخاب ناحیه(ثبتی) ملک اجباری می باشد");
            RuleFor(x => x.InqArea).NotEmpty().WithMessage("مساحت ملک اجباری می باشد");
            RuleFor(x => x.InqBasic).NotEmpty().WithMessage("پلاک اصلی ملک اجباری می باشد");
            RuleFor(x => x.InqSecondary).NotEmpty().WithMessage("پلاک فرعی ملک اجباری می باشد در صورتی که ملک پلاک فرعی ندارد مقدار صفر را وارد کنید");

            RuleFor(x => x.InqEstateInquiryTypeId).NotEmpty().WithMessage("انتخاب نوع ملک  اجباری می باشد");
            RuleFor(x => x.InqDocPrintNo).NotEmpty().When(x => x.InqEstateInquiryTypeId != null && x.InqEstateInquiryTypeId.Any() && x.InqEstateInquiryTypeId.First() == "1").WithMessage("شماره چاپی سند اجباری می باشد");
            RuleFor(x => x.InqMortgageText).NotEmpty().When(x => x.InqEstateInquiryTypeId != null && x.InqEstateInquiryTypeId.Any() && x.InqEstateInquiryTypeId.First() == "1").WithMessage("متن رهن اجباری می باشد در صورتی که متن رهن وجود ندارد کلمه ندارد را وارد کنید");
            RuleFor(x => x.InqEstateNoteNo).Empty().When(x => !string.IsNullOrWhiteSpace(x.InqElectronicEstateNoteNo) && x.InqEstateInquiryTypeId != null && x.InqEstateInquiryTypeId.Any() && x.InqEstateInquiryTypeId.First() == "1").WithMessage("یا باید شماره دفتر املاک الکترونیک مقدار داشته باشد یا شماره ثبت ، شماره دفتر ، شماره صفحه و سری  دفتر مقدار داشته باشند ");
            RuleFor(x => x.InqPageNo).Empty().When(x => !string.IsNullOrWhiteSpace(x.InqElectronicEstateNoteNo) && x.InqEstateInquiryTypeId != null && x.InqEstateInquiryTypeId.Any() && x.InqEstateInquiryTypeId.First() == "1").WithMessage("یا باید شماره دفتر املاک الکترونیک مقدار داشته باشد یا شماره ثبت ، شماره دفتر ، شماره صفحه و سری  دفتر مقدار داشته باشند ");
            RuleFor(x => x.InqEstateSeridaftarId).Must(x => x == null || x.Count == 0).When(x => !string.IsNullOrWhiteSpace(x.InqElectronicEstateNoteNo) && x.InqEstateInquiryTypeId != null && x.InqEstateInquiryTypeId.Any() && x.InqEstateInquiryTypeId.First() == "1").WithMessage("یا باید شماره دفتر املاک الکترونیک مقدار داشته باشد یا شماره ثبت ،  شماره دفتر ، شماره صفحه و سری  دفتر مقدار داشته باشند ");
            RuleFor(x => x.InqRegisterNo).Empty().When(x => !string.IsNullOrWhiteSpace(x.InqElectronicEstateNoteNo) && x.InqEstateInquiryTypeId != null && x.InqEstateInquiryTypeId.Any() && x.InqEstateInquiryTypeId.First() == "1").WithMessage("یا باید شماره دفتر املاک الکترونیک مقدار داشته باشد یا شماره ثبت ، شماره دفتر ، شماره صفحه و سری  دفتر مقدار داشته باشند ");
            RuleFor(x => x.InqEstateNoteNo).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.InqElectronicEstateNoteNo) && x.InqEstateInquiryTypeId != null && x.InqEstateInquiryTypeId.Any() && x.InqEstateInquiryTypeId.First() == "1").WithMessage("یا باید شماره دفتر املاک الکترونیک مقدار داشته باشد یا شماره ثبت ، شماره دفتر ، شماره صفحه و سری  دفتر مقدار داشته باشند ");
            RuleFor(x => x.InqPageNo).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.InqElectronicEstateNoteNo) && x.InqEstateInquiryTypeId != null && x.InqEstateInquiryTypeId.Any() && x.InqEstateInquiryTypeId.First() == "1").WithMessage("یا باید شماره دفتر املاک الکترونیک مقدار داشته باشد یا شماره ثبت ، شماره دفتر ، شماره صفحه و سری  دفتر مقدار داشته باشند ");
            RuleFor(x => x.InqEstateSeridaftarId).Must(x => x != null && x.Count > 0).When(x => string.IsNullOrWhiteSpace(x.InqElectronicEstateNoteNo) && x.InqEstateInquiryTypeId != null && x.InqEstateInquiryTypeId.Any() && x.InqEstateInquiryTypeId.First() == "1").WithMessage("یا باید شماره دفتر املاک الکترونیک مقدار داشته باشد یا شماره ثبت ، شماره دفتر ، شماره صفحه و سری  دفتر مقدار داشته باشند ");
            RuleFor(x => x.InqRegisterNo).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.InqElectronicEstateNoteNo) && x.InqEstateInquiryTypeId != null && x.InqEstateInquiryTypeId.Any() && x.InqEstateInquiryTypeId.First() == "1").WithMessage("یا باید شماره دفتر املاک الکترونیک مقدار داشته باشد یا شماره ثبت ، شماره دفتر ، شماره صفحه و سری  دفتر مقدار داشته باشند ");
            RuleFor(x => x.InqEdeclarationNo).Empty().When(x => x.InqEstateInquiryTypeId != null && x.InqEstateInquiryTypeId.Any() && x.InqEstateInquiryTypeId.First() == "1").WithMessage("شماره اظهار نامه باید خالی باشد.");
            RuleFor(x => x.InqEdeclarationNo).NotEmpty().When(x => x.InqEstateInquiryTypeId != null && x.InqEstateInquiryTypeId.Any() && x.InqEstateInquiryTypeId.First() == "2").WithMessage("شماره اظهارنامه اجباری می باشد ");

            RuleFor(x => x.InqInquiryPerson).NotNull().WithMessage("اطلاعات شخص(مالک) خالی می باشد");
            RuleFor(v => v.InqInquiryPerson.PersonId).Empty().When(x => x.InqInquiryPerson != null).WithMessage("شناسه مالک باید خالی باشد");
            RuleFor(x => x.InqInquiryPerson.PersonType).NotEmpty().When(x => x.InqInquiryPerson != null).WithMessage("انتخاب نوع شخص اجباری می باشد");
            RuleFor(x => x.InqInquiryPerson.PersonName).NotEmpty().When(x => x.InqInquiryPerson != null).WithMessage("نام مالک اجباری می باشد");
            RuleFor(x => x.InqInquiryPerson.PersonCityId).Must(x => x != null && x.Count > 0).When(x => x.InqInquiryPerson != null && !x.InqInquiryPerson.PersonExecutiveTransfer).WithMessage("انتخاب شهر محل اقامت مالک اجباری می باشد");
            RuleFor(x => x.InqInquiryPerson.PersonPostalCode).NotEmpty().When(x => x.InqInquiryPerson != null && !x.InqInquiryPerson.PersonExecutiveTransfer).WithMessage("کد پستی محل اقامت مالک اجباری می باشد");
            RuleFor(x => x.InqInquiryPerson.PersonNationalityId).Must(x => x != null && x.Count > 0).When(x => x.InqInquiryPerson != null && !x.InqInquiryPerson.PersonIsIrani && !x.InqInquiryPerson.PersonExecutiveTransfer).WithMessage("انتخاب تابعیت مالک اجباری می باشد");
            RuleFor(x => x.InqInquiryPerson.PersonFamily).NotEmpty().When(x => x.InqInquiryPerson != null && x.InqInquiryPerson.PersonType == "1").WithMessage("نام خانوادگی مالک اجباری می باشد");
            RuleFor(x => x.InqInquiryPerson.PersonNationalityCode).NotEmpty().When(x => x.InqInquiryPerson != null && !x.InqInquiryPerson.PersonExecutiveTransfer).WithMessage("شماره/شناسه ملی مالک اجباری می باشد");
            RuleFor(x => x.InqInquiryPerson.PersonIssuePlaceId).Must(x => x != null && x.Count > 0).When(x => x.InqInquiryPerson != null && x.InqInquiryPerson.PersonType == "1" && !x.InqInquiryPerson.PersonExecutiveTransfer).WithMessage("انتخاب محل صدور شناسنامه مالک اجباری می باشد");

            RuleFor(v => v.InqEstatePostalCode).Must(x =>
            {
                return IsMatch(@"^(\d{10})$", x);
            }).When(x => !string.IsNullOrWhiteSpace(x.InqEstatePostalCode)).WithMessage("کد پستی ملک اشتباه می باشد");
            RuleFor(v => v.InqInquiryDate).Must(x =>
            {
                return ValidatorHelper.BeValidPersianDate(x);
            }).When(x => !string.IsNullOrWhiteSpace(x.InqInquiryDate)).WithMessage(" تاریخ استعلام اشتباه می باشد");
            RuleFor(v => v.InqElectronicEstateNoteNo).Must(x =>
            {
                return IsMatch(@"^([1-9]{1}\d{17})$", x);
            }).When(x => !string.IsNullOrWhiteSpace(x.InqElectronicEstateNoteNo)).WithMessage("شماره دفتر املاک الکترونیک اشتباه می باشد");

            RuleFor(v => v.InqInquiryPerson.PersonNationalityCode).Must(x => x.IsValidNationalCode()).When(x => x.InqInquiryPerson != null && x.InqInquiryPerson.PersonIsIrani && !string.IsNullOrWhiteSpace(x.InqInquiryPerson.PersonNationalityCode) && x.InqInquiryPerson.PersonType == EstateConstant.PersonTypeConstant.RealPerson).WithMessage("فرمت شماره ملی اشتباه می باشد");
            RuleFor(v => v.InqInquiryPerson.PersonNationalityCode).Must(x => x.IsValidLegalNationalCode()).When(x => x.InqInquiryPerson != null && x.InqInquiryPerson.PersonIsIrani && !string.IsNullOrWhiteSpace(x.InqInquiryPerson.PersonNationalityCode) && x.InqInquiryPerson.PersonType == EstateConstant.PersonTypeConstant.LegalPerson).WithMessage("فرمت شناسه ملی اشتباه می باشد");
            RuleFor(v => v.InqInquiryPerson.PersonPostalCode).Must(x =>
            {
                return IsMatch(@"^(\d{10})$", x);
            }).When(x => x.InqInquiryPerson != null && !string.IsNullOrWhiteSpace(x.InqInquiryPerson.PersonPostalCode)).WithMessage("کد پستی محل سکونت شخص اشتباه می باشد");
            RuleFor(v => v.InqInquiryPerson.PersonMobileNo).Must(x =>
            {
                return ValidatorHelper.BeValidMobileNumber(x);
            }).When(x => x.InqInquiryPerson != null && !string.IsNullOrWhiteSpace(x.InqInquiryPerson.PersonMobileNo)).WithMessage(" شماره موبایل شخص اشتباه می باشد");
            RuleFor(v => v.InqInquiryPerson.PersonBirthDate).Must(x =>
            {
                var bv = IsMatch(@"^([1-9]{4})$", x);
                if (bv) return true;
                return ValidatorHelper.BeValidPersianDate(x);
            }).When(x => x.InqInquiryPerson != null && !string.IsNullOrWhiteSpace(x.InqInquiryPerson.PersonBirthDate)).WithMessage(" تاریخ تولد/ثبت شخص اشتباه می باشد");

        }
        private static bool IsMatch(string regularExpression, string value)
        {
            Regex regular = new Regex(regularExpression);
            if (regular.IsMatch(value))
                return true;
            return false;
        }
    }
}

