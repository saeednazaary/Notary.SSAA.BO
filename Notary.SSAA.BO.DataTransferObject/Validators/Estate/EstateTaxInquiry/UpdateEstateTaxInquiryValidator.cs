using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Text.RegularExpressions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateTaxInquiry
{
    public class UpdateEstateTaxInquiryValidator : AbstractValidator<UpdateEstateTaxInquiryCommand>
    {
        public UpdateEstateTaxInquiryValidator()
        {
            string[] sa = new string[] { "04", "05", "06" };
            //RuleFor(x => x.IsValid)
            //    .Must(x => x == true).WithMessage("وضعیت استعلام نا معتبر است");

            //RuleFor(x => x.IsDirty)
            //    .Must(x => x == true).WithMessage("وضعیت استعلام ویرایش شده نمیباشد");

            RuleFor(x => x.IsDelete)
                .Must(x => x == false).WithMessage("وضعیت استعلام حذف شده میباشد");

            RuleFor(x => x.TheInquiryBuyersList)
            .NotEmpty().WithMessage("لیست خریداران نمی تواند خالی باشد");
            RuleFor(x => x.TheInquiryOwner)
             .NotEmpty().WithMessage("استعلام فاقد مشخصات مالک می باشد");
            //RuleFor(x => x.TheInquiryOwner)
            // .Must(x =>
            // {
            //     return  x.IsValid;
            // }).When(x => x.TheInquiryOwner != null).WithMessage("اطلاعات مالک باید در وضعیت معتبر  باشد");

            RuleFor(x => x.InquiryId).NotEmpty().WithMessage("شناسه استعلام نباید خالی باشد");
            //RuleFor(x => x.EstateInquiryId).NotEmpty().WithMessage("انتخاب استعلام ملک اجباری می باشد");
            RuleFor(x => x.InquiryApartmentArea).NotEmpty().WithMessage("مساحت مفید اعیانی ها نمی تواند خالی باشد");
            RuleFor(x => x.InquiryArsehArea).NotEmpty().WithMessage("مساحت عرصه نمی تواند خالی باشد");
            RuleFor(x => x.InquiryAvenue).NotEmpty().WithMessage("قسمت خیابان آدرس ملک نمی تواند خالی باشد");
            RuleFor(x => x.InquiryBuildingTypeId).NotEmpty().When(x => x.InquiryTransferTypeId != null && x.InquiryTransferTypeId.Count > 0 && x.InquiryTransferTypeId.First() != "01").WithMessage("نوع ساختمان  اعیانی نمی تواند خالی باشد");
            RuleFor(x => x.InquiryBuildingStatusId).NotEmpty().When(x => x.InquiryTransferTypeId != null && x.InquiryTransferTypeId.Count > 0 && x.InquiryTransferTypeId.First() != "01").WithMessage("وضعیت ساختمان  اعیانی نمی تواند خالی باشد");
            RuleFor(x => x.InquiryBuildingConstructionStepId).NotEmpty().When(x => x.InquiryTransferTypeId != null && x.InquiryTransferTypeId.Count > 0 && x.InquiryTransferTypeId.First() != "01" && x.InquiryBuildingStatusId != null && x.InquiryBuildingStatusId.Count > 0 && x.InquiryBuildingStatusId.First() == "02").WithMessage("مرحله ساخت ساختمان  اعیانی نمی تواند خالی باشد");
            RuleFor(x => x.InquiryBuildingUsingTypeId).NotEmpty().When(x => x.InquiryTransferTypeId != null && x.InquiryTransferTypeId.Count > 0 && x.InquiryTransferTypeId.First() != "01").WithMessage("نوع کاربری ساختمان  اعیانی نمی تواند خالی باشد");

            RuleFor(x => x.InquiryBuildingOld).NotEmpty().WithMessage("قدمت ساختمان اعیانی نمی تواند خالی باشد");
            RuleFor(x => x.InquiryCessionDate).NotEmpty().WithMessage("تاریخ نقل وانتقال نمی تواند خالی باشد");
            RuleFor(x => x.InquiryCessionPrice).NotEmpty().WithMessage("مبلغ معامله طبق مبایعه نامه یا قرارداد نمی تواند خالی باشد");
            RuleFor(x => x.InquiryDocumentRequestTypeId).NotEmpty().WithMessage("نوع درخواست تنظیم سند نمی تواند خالی باشد");
            RuleFor(x => x.InquiryEstateAddress).NotEmpty().WithMessage("آدرس ملک نمی تواند خالی باشد");
            RuleFor(x => x.InquiryEstatebasic).NotEmpty().WithMessage("پلاک اصلی ملک نمی تواند خالی باشد");
            RuleFor(x => x.InquiryEstatePostCode).NotEmpty().WithMessage("کد پستی ملک نمی تواند خالی باشد");
            RuleFor(x => x.InquiryEstatesecondary).NotEmpty().WithMessage(" پلاک فرعی ملک  نمی تواند خالی باشد . در صورتی که ملک پلاک فرعی نداردمقدار صفر را وارد کنید");
            RuleFor(x => x.InquiryEstateSectionId).NotEmpty().WithMessage("بخش ثبتی ملک  نمی تواند خالی باشد");
            RuleFor(x => x.InquiryEstateSector).NotEmpty().WithMessage("شماره قطعه ملک نمی تواند خالی باشد");
            RuleFor(x => x.InquiryEstateSubSectionId).NotEmpty().WithMessage("ناحیه ثبتی ملک نمی تواند خالی باشد");
            RuleFor(x => x.InquiryEstateTaxProvinceId).NotEmpty().WithMessage("استان ملک نمی تواند خالی باشد");
            RuleFor(x => x.InquiryEstateTaxCountyId).NotEmpty().WithMessage("شهرستان ملک نمی تواند خالی باشد");
            RuleFor(x => x.InquiryEstateTaxCityId).NotEmpty().WithMessage("شهر ملک نمی تواند خالی باشد");
            RuleFor(x => x.InquiryEstateUnitId).NotEmpty().WithMessage("واحد ثبتی ملک نمی تواند خالی باشد");
            RuleFor(x => x.InquiryFieldTypeId).NotEmpty().WithMessage("نوع عرصه نمی تواند خالی باشد");
            RuleFor(x => x.InquiryFieldUsingTypeId).NotEmpty().WithMessage("نوع کاربری عرصه نمی تواند خالی باشد");
            RuleFor(x => x.InquiryFloorNo).NotEmpty().When(x => !x.InquiryIsGroundLevel).WithMessage("شماره طبقه واحد مورد واگذاری نمی تواند خالی باشد");
            RuleFor(x => x.InquiryLicenseDate).NotEmpty().WithMessage("تاریخ جواز/پروانه ساخت نمی تواند خالی باشد");
            RuleFor(x => x.InquiryLocationAssignRightDealCurrentValue).NotEmpty().When(x => x.InquiryTransferTypeId != null && x.InquiryTransferTypeId.Count > 0 && sa.Contains(x.InquiryTransferTypeId.First())).WithMessage("ارزش روز معامله حق واگذاری محل نمی تواند خالی باشد");
            RuleFor(x => x.InquiryLocationAssignRigthOwnershipTypeId).NotEmpty().When(x => x.InquiryTransferTypeId != null && x.InquiryTransferTypeId.Count > 0 && sa.Contains(x.InquiryTransferTypeId.First())).WithMessage("نوع مالکیت  حق واگذاری محل نمی تواند خالی باشد");
            RuleFor(x => x.InquiryLocationAssignRigthUsingTypeId).NotEmpty().When(x => x.InquiryTransferTypeId != null && x.InquiryTransferTypeId.Count > 0 && sa.Contains(x.InquiryTransferTypeId.First())).WithMessage("نوع کاربری حق واگذاری محل نمی تواند خالی باشد");
            RuleFor(x => x.InquiryPlateNo).NotEmpty().WithMessage("شماره پلاک ملک نمی تواند خالی باشد");
            RuleFor(x => x.InquirySeparationProcessNo).NotEmpty().WithMessage("شماره صورت مجلس تفکیکی نمی تواند خالی باشد");
            RuleFor(x => x.InquiryShareOfOwnership).NotEmpty().WithMessage("سهم مالک از مالکیت نمی تواند خالی باشد");
            RuleFor(x => x.InquiryTotalArea).NotEmpty().WithMessage("مجموع متراژ کل اعیانی ها ، انباری ها ،پارکینگ های ملک نمی تواند خالی باشد");
            RuleFor(x => x.InquiryTotalOwnershipShare).NotEmpty().WithMessage("کل سهم مالکیت نمی تواند خالی باشد");
            RuleFor(x => x.InquiryTranceWidth).NotEmpty().WithMessage("عرض از معبر عرصه نمی تواند خالی باشد");
            RuleFor(x => x.InquiryTransferTypeId).NotEmpty().WithMessage("موضوع نقل و انتقال نمی تواند خالی باشد");
            RuleFor(x => x.InquiryTransitionShare).NotEmpty().WithMessage("میزان انتقال برحسب سهم نمی تواند خالی باشد");
            RuleFor(x => x.InquiryValuebookletBlockNo).NotEmpty().WithMessage("شماره بلوک بر اساس دفترچه ارزش معاملاتی نمی تواند خالی باشد");
            RuleFor(x => x.InquiryValuebookletRowNo).NotEmpty().WithMessage("شماره ردیف براساس دفترچه ارزش معاملاتی نمی تواند خالی باشد");
            RuleFor(x => x.InquiryWorkCompletionCertificateDate).NotEmpty().WithMessage("تاریخ گواهی پایان کار نمی تواند خالی باشد");

            RuleFor(v => v.InquiryEstatePostCode).Must(x =>
            {
                return IsMatch(@"^(\d{10})$", x);
            }).When(x => !string.IsNullOrWhiteSpace(x.InquiryEstatePostCode)).WithMessage("کد پستی ملک اشتباه می باشد");
            RuleFor(v => v.InquiryLicenseDate).Must(x =>
            {
                return ValidatorHelper.BeValidPersianDate(x);
            }).When(x => !string.IsNullOrWhiteSpace(x.InquiryLicenseDate)).WithMessage(" تاریخ جواز اشتباه می باشد");
            RuleFor(v => v.InquiryCessionDate).Must(x =>
            {
                return ValidatorHelper.BeValidPersianDate(x);
            }).When(x => !string.IsNullOrWhiteSpace(x.InquiryCessionDate)).WithMessage(" تاریخ نقل و انتقال اشتباه می باشد");
            RuleFor(v => v.InquiryWorkCompletionCertificateDate).Must(x =>
            {
                return ValidatorHelper.BeValidPersianDate(x);
            }).When(x => !string.IsNullOrWhiteSpace(x.InquiryWorkCompletionCertificateDate)).WithMessage(" تاریخ گواهی پایان کار اشتباه می باشد");

            RuleFor(x => x.TheInquiryOwner.PersonType).NotEmpty().When(x => x.TheInquiryOwner != null && x.TheInquiryOwner.IsDirty && !x.TheInquiryOwner.IsDelete).WithMessage("انتخاب نوع شخص (حقیقی/حقوقی) برای مالک اجباری می باشد");
            RuleFor(v => v.TheInquiryOwner.PersonNationalityCode).NotEmpty().When(x => x.TheInquiryOwner != null && x.TheInquiryOwner.IsDirty  && !x.TheInquiryOwner.IsDelete).WithMessage("شماره ملی/شناسه ملی مالک نمی تواند خالی باشد");
            //RuleFor(v => v.TheInquiryOwner).Must(x => x != null && x.PersonNationalityId != null && x.PersonNationalityId.Count > 0 &&  !x.PersonIsIrani && x.IsNew && x.IsValid && !x.IsDelete).WithMessage("تابعیت مالک نمی تواند خالی باشد");
            //RuleFor(v => v.TheInquiryOwner.PersonBirthDate).NotEmpty().When(x => x.TheInquiryOwner != null && x.TheInquiryOwner.IsNew && x.TheInquiryOwner.IsValid && !x.TheInquiryOwner.IsDelete && x.TheInquiryOwner.PersonPersonType == EstateConstant.PersonTypeConstant.RealPerson).WithMessage("تاریخ تولد مالک نمی تواند خالی باشد");
            RuleFor(v => v.TheInquiryOwner.PersonMobileNo).NotEmpty().When(x => x.TheInquiryOwner != null && x.TheInquiryOwner.IsDirty  && !x.TheInquiryOwner.IsDelete).WithMessage("شماره تلفن همراه مالک نمی تواند خالی باشد");
            RuleFor(v => v.TheInquiryOwner.PersonTel).NotEmpty().When(x => x.TheInquiryOwner != null && x.TheInquiryOwner.IsDirty  && !x.TheInquiryOwner.IsDelete).WithMessage("شماره تلفن تماس مالک نمی تواند خالی باشد");
            RuleFor(v => v.TheInquiryOwner.PersonName).NotEmpty().When(x => x.TheInquiryOwner != null && x.TheInquiryOwner.IsDirty  && !x.TheInquiryOwner.IsDelete).WithMessage("نام مالک نمی تواند خالی باشد");
            RuleFor(v => v.TheInquiryOwner.PersonFamily).NotEmpty().When(x => x.TheInquiryOwner != null && x.TheInquiryOwner.IsDirty  && !x.TheInquiryOwner.IsDelete && x.TheInquiryOwner.PersonType == EstateConstant.PersonTypeConstant.RealPerson).WithMessage("نام خانوادگی مالک نمی تواند خالی باشد");
            //RuleFor(v => v.TheInquiryOwner.PersonIdentityNo).NotEmpty().When(x => x.TheInquiryOwner != null && x.TheInquiryOwner.IsNew && x.TheInquiryOwner.IsValid && !x.TheInquiryOwner.IsDelete && x.TheInquiryOwner.PersonPersonType == EstateConstant.PersonTypeConstant.RealPerson).WithMessage("شماره شناسنامه مالک نمی تواند خالی باشد");
            RuleFor(v => v.TheInquiryOwner.PersonNationalityCode).Must(x => x.IsValidNationalCode()).When(x => x.TheInquiryOwner != null && x.TheInquiryOwner.IsDirty  && !x.TheInquiryOwner.IsDelete && x.TheInquiryOwner.PersonIsIrani && !string.IsNullOrWhiteSpace(x.TheInquiryOwner.PersonNationalityCode) && x.TheInquiryOwner.PersonType == EstateConstant.PersonTypeConstant.RealPerson).WithMessage(" شماره ملی مالک اشتباه می باشد");
            RuleFor(v => v.TheInquiryOwner.PersonNationalityCode).Must(x => x.IsValidLegalNationalCode()).When(x => x.TheInquiryOwner != null && x.TheInquiryOwner.IsDirty &&  !x.TheInquiryOwner.IsDelete && x.TheInquiryOwner.PersonIsIrani && !string.IsNullOrWhiteSpace(x.TheInquiryOwner.PersonNationalityCode) && x.TheInquiryOwner.PersonType == EstateConstant.PersonTypeConstant.LegalPerson).WithMessage(" شناسه ملی مالک اشتباه می باشد");

            RuleFor(v => v.TheInquiryBuyersList).Must(x =>
            {
                bool bv = false;
                foreach (var y in x)
                {
                    if ((y.IsDirty || y.IsNew) && !y.IsDelete)
                    {
                        if (string.IsNullOrWhiteSpace(y.PersonType)) bv = true;
                        if (y.PersonType == EstateConstant.PersonTypeConstant.RealPerson)
                        {
                            if (string.IsNullOrWhiteSpace(y.PersonName)) bv = true;
                            if (string.IsNullOrWhiteSpace(y.PersonFamily)) bv = true;
                            if (string.IsNullOrWhiteSpace(y.PersonNationalityCode)) bv = true;
                            if (string.IsNullOrWhiteSpace(y.PersonBirthDate)) bv = true;
                            if (y.PersonIsIrani)
                            {
                                if (!string.IsNullOrWhiteSpace(y.PersonNationalityCode) && !y.PersonNationalityCode.IsValidNationalCode())
                                    bv = true;
                            }
                            if(!bv)
                            {
                                if (!ValidatorHelper.BeValidPersianDate(y.PersonBirthDate))
                                    bv = true;
                            }
                        }
                        else if (y.PersonType == EstateConstant.PersonTypeConstant.LegalPerson)
                        {
                            if (string.IsNullOrWhiteSpace(y.PersonName)) bv = true;
                            if (string.IsNullOrWhiteSpace(y.PersonNationalityCode)) bv = true;
                            if (y.PersonIsIrani)
                            {
                                if (!string.IsNullOrWhiteSpace(y.PersonNationalityCode) && !y.PersonNationalityCode.IsValidLegalNationalCode())
                                    bv = true;
                            }
                        }
                        if (string.IsNullOrWhiteSpace(y.PersonPostalCode))
                            bv = true;
                        else
                        {
                            if (!IsMatch(@"^(\d{10})$", y.PersonPostalCode))
                                bv = true;
                        }
                        if (string.IsNullOrWhiteSpace(y.PersonAddress)) bv = true;
                        //if (y.PersonNationalityId == null || y.PersonNationalityId.Count == 0) bv = true;
                        if (y.PersonSharePart == 0) bv = true;
                        if (y.PersonShareTotal == 0) bv = true;
                        if (y.PersonRelationTypeId == null || y.PersonRelationTypeId.Count == 0) bv = true;
                        y.IsValid = !bv;
                    }
                }
                return !bv;
            })
            .When(v => v.TheInquiryBuyersList != null && v.TheInquiryBuyersList.Count > 0)
            .WithMessage(" در لیست خریداران سطر یا سطرهایی وجود دارد که در آنها برخی از آیتم های اجباری مشخصات خریدار مقدار ندارد یا مقدار کد/شناسه ملی یا کد پستی اشتباه می باشد");

            RuleFor(x => x.TheInquiryEstateAttachList).Must(x =>
            {
                bool bv = false;
                foreach (var y in x)
                {
                    if ((y.IsDirty || y.IsNew) &&  !y.IsDelete)
                    {
                        if (y.AttachArea == 0) bv = true;
                        if (string.IsNullOrWhiteSpace(y.AttachBlock)) bv = true;
                        if (y.AttachEstatePieceTypeId == null || y.AttachEstatePieceTypeId.Count == 0) bv = true;
                        if (y.AttachEstateSideTypeId == null || y.AttachEstateSideTypeId.Count == 0) bv = true;
                        if (string.IsNullOrWhiteSpace(y.AttachFloor)) bv = true;
                        if (string.IsNullOrWhiteSpace(y.AttachPiece)) bv = true;
                        y.IsValid = !bv;
                    }
                }
                return !bv;
            })
            .When(x => x.TheInquiryEstateAttachList != null && x.TheInquiryEstateAttachList.Count > 0)
            .WithMessage("در لیست منضمات ملک سطر یا سطر هایی وجود دارد که در آنها برخی از آیتم های اجباری مشخصات منضم مقدار ندارد");

            RuleFor(v => v.TheInquiryOwner.PersonPostalCode).Must(x =>
            {
                return IsMatch(@"^(\d{10})$", x);
            }).When(x => x.TheInquiryOwner != null && x.TheInquiryOwner.IsDirty &&  !x.TheInquiryOwner.IsDelete && !string.IsNullOrWhiteSpace(x.TheInquiryOwner.PersonPostalCode)).WithMessage("کد پستی محل سکونت مالک اشتباه می باشد");
            RuleFor(v => v.TheInquiryOwner.PersonMobileNo).Must(x =>
            {
                return ValidatorHelper.BeValidMobileNumber(x);
            }).When(x => x.TheInquiryOwner != null && x.TheInquiryOwner.IsDirty  && !x.TheInquiryOwner.IsDelete && !string.IsNullOrWhiteSpace(x.TheInquiryOwner.PersonMobileNo)).WithMessage(" شماره موبایل مالک اشتباه می باشد");
            RuleFor(v => v.TheInquiryOwner.PersonBirthDate).Must(x =>
            {
                return ValidatorHelper.BeValidPersianDate(x);
            }).When(x => x.TheInquiryOwner != null && x.TheInquiryOwner.IsDirty  && !x.TheInquiryOwner.IsDelete && !string.IsNullOrWhiteSpace(x.TheInquiryOwner.PersonBirthDate)).WithMessage(" تاریخ تولد/ثبت مالک اشتباه می باشد");
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

