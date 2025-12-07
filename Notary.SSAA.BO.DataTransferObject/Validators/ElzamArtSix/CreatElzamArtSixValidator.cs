using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.ElzamArtSix;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Constants;

namespace Notary.SSAA.BO.DataTransferObject.Validators.ElzamArtSix
{
    public class CreateElzamArtSixValidator : AbstractValidator<CreateElzamArtSixCommand>
    {
        public CreateElzamArtSixValidator()
        {
            RuleFor(v => v.WorkflowStatesId)
                .Must(v=> v != EstateConstant.EstateElzamSixArtInquiryStates.Sended && v != EstateConstant.EstateElzamSixArtInquiryStates.Responsed)
                .WithMessage("در وضعیت فعلی گردش کار امکان ثبت وجود ندارد");
            RuleFor(v => v.EstateUnitId)
                .NotEmpty().WithMessage("مقدار واحد ثبتی اجباری می‌باشد");
            RuleFor(v => v.EstateSectionId)
                .NotEmpty().WithMessage("مقدار بخش اجباری می‌باشد");
            RuleFor(v => v.EstateSubsectionId)
                .NotEmpty().WithMessage("مقدار ناحیه اجباری می‌باشد");
            RuleFor(v => v.EstateMainPlaque)
                .NotEmpty().WithMessage("مقدار پلاک اصلی اجباری می‌باشد");
            RuleFor(v => v.EstateSecondaryPlaque)
                .NotEmpty().WithMessage("مقدار پلاک فرعی اجباری می‌باشد");
            RuleFor(v => v.EstateArea)
                .NotEmpty().WithMessage("مقدار مساحت اجباری می‌باشد");
            RuleFor(v => v.EstatePostCode)
                .NotEmpty().WithMessage("مقدار کدپستی ملک اجباری می‌باشد");
            RuleFor(v => v.ProvinceId)
                .NotEmpty().WithMessage("مقدار استان اجباری می‌باشد");
            RuleFor(v => v.CountyId)
                .NotEmpty().WithMessage("مقدار شهرستان اجباری می‌باشد");
            RuleFor(v => v.EstateUsingId)
                .NotEmpty().WithMessage("مقدار نوع کاربری ملک اجباری می‌باشد");
            RuleFor(v => v.EstateTypeId)
                .NotEmpty().WithMessage("مقدار نوع ملک اجباری می‌باشد");
            RuleFor(v => v.ElzamArtSixSellerPerson)
               .NotEmpty().WithMessage("تعیین مشخصات فروشنده اجباری می‌باشد")
               .DependentRules(() =>
               {
                   RuleFor(v => v.ElzamArtSixSellerPerson.NationalityCode)
                     .NotEmpty().WithMessage("مقدار شناسه ملی فروشنده اجباری می‌باشد");
               }).When(x => x.ElzamArtSixSellerPerson != null && x.ElzamArtSixSellerPerson.PersonType == PersonType.Legal.ToAssignedValue())
                .DependentRules(() =>
                {
                    RuleFor(v => v.ElzamArtSixSellerPerson.NationalityCode)
                      .NotEmpty().WithMessage("مقدار کدملی شخص فروشنده اجباری می‌باشد");
                    RuleFor(v => v.ElzamArtSixSellerPerson.BirthDate)
                      .NotEmpty().WithMessage("مقدار تاریخ تولد شخص فروشنده اجباری می‌باشد");
                    RuleFor(v => v.ElzamArtSixSellerPerson.Name)
                      .NotEmpty().WithMessage("مقدار نام شخص فروشنده اجباری می‌باشد");
                    RuleFor(v => v.ElzamArtSixSellerPerson.Family)
                      .NotEmpty().WithMessage("مقدار نام‌خانوادگی شخص فروشنده اجباری می‌باشد");
                    RuleFor(v => v.ElzamArtSixSellerPerson.FatherName)
                      .NotEmpty().WithMessage("مقدار نام پدر فروشنده اجباری می‌باشد");
                    RuleFor(v => v.ElzamArtSixSellerPerson.IdentityNo)
                      .NotEmpty().WithMessage("مقدار شماره شناسنامه فروشنده اجباری می‌باشد");
                    RuleFor(v => v.ElzamArtSixSellerPerson.IssuePlaceText)
                      .NotEmpty().WithMessage("مقدار محل صدور فروشنده اجباری می‌باشد");
                    RuleFor(v => v.ElzamArtSixSellerPerson.Seri)
                      .NotEmpty().WithMessage("مقدار سری شناسنامه فروشنده اجباری می‌باشد");
                    RuleFor(v => v.ElzamArtSixSellerPerson.SerialNo)
                      .NotEmpty().WithMessage("مقدار سریال شناسنامه فروشنده اجباری می‌باشد");
                    RuleFor(v => v.ElzamArtSixSellerPerson)
                      .Must(v =>
                      {
                          if (!decimal.TryParse(v.SharePart, out var part)) return false;
                          if (!decimal.TryParse(v.ShareTotal, out var total)) return false;
                      
                          return part <= total;
                      })
                      .When(v =>
                          !string.IsNullOrWhiteSpace(v.ElzamArtSixSellerPerson.SharePart) &&
                          !string.IsNullOrWhiteSpace(v.ElzamArtSixSellerPerson.ShareTotal))
                      .WithMessage("مقدار جز سهم نمی‌تواند بیشتر از مقدار کل سهم باشد.");
                    RuleFor(v => v.ElzamArtSixSellerPerson.ShareTotal)
                      .NotEmpty().WithMessage("مقدار کل سهم اجباری می‌باشد");
                    RuleFor(v => v.ElzamArtSixSellerPerson.SharePart)
                      .NotEmpty().WithMessage("مقدار جز سهم اجباری می‌باشد");
                }).When(x => x.ElzamArtSixSellerPerson != null && x.ElzamArtSixSellerPerson.PersonType == PersonType.NaturalPerson.ToAssignedValue())
                .DependentRules(() =>
                {
                    RuleFor(v => v.ElzamArtSixSellerPerson.NationalityCode)
                    .NotEqual(v => v.ElzamArtSixBuyerPerson.NationalityCode)
                    .WithMessage("کد ملی فروشنده و خریدار نمی‌تواند یکسان باشد.")
                    .When(v => !v.ElzamArtSixSellerPerson.NationalityCode.IsNullOrEmpty() && !v.ElzamArtSixBuyerPerson.NationalityCode.IsNullOrEmpty());
                    RuleFor(v => v.ElzamArtSixSellerPerson.PersonType)
                    .NotEmpty().WithMessage("مقدار نوع شخص فروشنده اجباری می‌باشد");
                });

            RuleFor(v => v.ElzamArtSixBuyerPerson)
               .NotEmpty().WithMessage("تعیین مشخصات خریدار اجباری می‌باشد")
               .DependentRules(() =>
               {
                   RuleFor(v => v.ElzamArtSixBuyerPerson.NationalityCode)
                     .NotEmpty().WithMessage("مقدار شناسه ملی خریدار اجباری می‌باشد");
               }).When(x => x.ElzamArtSixBuyerPerson != null && x.ElzamArtSixBuyerPerson.PersonType == PersonType.Legal.ToAssignedValue())
                .DependentRules(() =>
                {
                    RuleFor(v => v.ElzamArtSixBuyerPerson.NationalityCode)
                      .NotEmpty().WithMessage("مقدار کدملی شخص خریدار اجباری می‌باشد");
                }).When(x => x.ElzamArtSixBuyerPerson != null && x.ElzamArtSixBuyerPerson.PersonType == PersonType.NaturalPerson.ToAssignedValue())
                .DependentRules(() =>
                {
                    RuleFor(v => v.ElzamArtSixBuyerPerson.PersonType)
                    .NotEmpty().WithMessage("مقدار نوع شخص خریدار اجباری می‌باشد");
                });
        }
    }
}