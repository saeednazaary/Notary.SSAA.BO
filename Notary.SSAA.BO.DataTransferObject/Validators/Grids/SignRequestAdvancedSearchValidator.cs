using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.Utilities.Extensions;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Grids
{
    public class SignRequestAdvancedSearchValidator : AbstractValidator<SignRequestAdvancedSearchQuery>
    {
        public SignRequestAdvancedSearchValidator()
        {
            RuleFor(v => v.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage("مقدار شماره صقحه غیر مجاز است")
            .NotNull().WithMessage("فیلد شماره صفحه اجباری است");

            RuleFor(v => v.PageSize)
             .ExclusiveBetween(0, 11).WithMessage("مقدار اندازه صقحه غیر مجاز است")
             .NotNull().WithMessage("فیلد شماره صفحه اجباری است");


            RuleFor(v => v.ExtraParams.SignRequestReqNo).MaximumLength(18).WithMessage("طول شماره درخواست مجاز نمیباشد")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.SignRequestReqNo.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.SignRequestNationalNo).MaximumLength(18).WithMessage("طول شناسه یکتا گواهی امضا مجاز نمیباشد ")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.SignRequestNationalNo.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.SignRequestStateId).Must(x => int.TryParse(x, out int alphabet) && alphabet >= 1 && alphabet <= 5)
                .WithMessage("مقدار وضعیت مجاز نمیباشد ")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.SignRequestStateId.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.SignRequestReqDateFrom).Must(ValidatorHelper.BeValidPersianDate).WithMessage("فیلد تاریخ درخواست از معتبر نمیباشد")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.SignRequestReqDateFrom.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.SignRequestReqDateTo).Must(ValidatorHelper.BeValidPersianDate).WithMessage("فیلد تاریخ درخواست تا معتبر نمیباشد")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.SignRequestReqDateTo.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.SignRequestSignDateFrom).Must(ValidatorHelper.BeValidPersianDate).WithMessage("فیلد تاریخ امضا از معتبر نمیباشد ")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.SignRequestSignDateFrom.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.SignRequestSignDateTo).Must(ValidatorHelper.BeValidPersianDate).WithMessage("فیلد تاریخ درخواست تا معتبر نمیباشد ")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.SignRequestSignDateTo.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonNationalNo).MaximumLength(10).WithMessage("طول شماره ملی غیر مجاز است")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonNationalNo.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonName).MaximumLength(150).WithMessage("طول نام بیشتر از حد مجاز است ")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonName.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonFamily).MaximumLength(50).WithMessage("طول نام خانوادگی بیشتر از حد مجاز است ")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonFamily.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonFatherName).MaximumLength(50).WithMessage("طول نام پدر بیشتر از حد مجاز است ")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonFatherName.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonSignClassifyNo).Must(ValidatorHelper.BeValidNumber).WithMessage("فرمت شناسه کلاسه صحیح نمیباشد ")
                .MaximumLength(11).WithMessage("طول شناسه کلاسه  غیر مجاز است")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonSignClassifyNo.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonBirthDate).Must(ValidatorHelper.BeValidPersianDate).WithMessage("مقدار تاریخ تولد غیر مجاز است")
                .NotEmpty().WithMessage("فیلد تاریخ تولد اجباری است")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonBirthDate.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonSeri).Must(ValidatorHelper.BeValidNumber).WithMessage("سری شناسنامه صحیح نمیباشد")
                .MaximumLength(10).WithMessage("طول سری شخص صحیح نمیباشد")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonSeri.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonSerial).Must(ValidatorHelper.BeValidNumber).WithMessage("طول سریال شناسنامه بیشتر از حد مجاز است ")
                .MaximumLength(8).WithMessage("طول سریال شناسنامه بیشتر از حد مجاز است ")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonSerial.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonPostalCode).Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار فیلد کد پستی غیر مجاز است ")
                 .MaximumLength(10).WithMessage("طول کد پستی غیر مجاز است")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonPostalCode.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonMobileNo).Must(ValidatorHelper.BeValidMobileNumber).WithMessage("فرمت شماره موبایل اشتباه است")
                .MaximumLength(11).WithMessage("طول شماره موبایل غیر مجاز است")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonMobileNo.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonAddress).MaximumLength(200).WithMessage("طول سریال شناسنامه بیشتر از حد مجاز است ")
                .NotNull().WithMessage("فیلد آدرس اجباری است")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonAddress.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonTel).Must(ValidatorHelper.BeValidNumber).WithMessage("فرمت شماره تلفن اشتباه است.")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonTel.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonSexType).Must(value => ValidatorHelper.ValidateRangeValue(value, 1, 2)).WithMessage("مقدار فیلد جنسیت غیر مجاز است")
                .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار فیلد جنسیت غیر مجاز است")
                .MaximumLength(1).WithMessage("مقدار جنسیت غیر مجاز میباشد.")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonSexType.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonAlphabetSeri).Must(x => int.TryParse(x, out int alphabet) && alphabet >= 1 && alphabet <= 32)
                .WithMessage("مقدار سری الفبایی شناسنامه غیر مجاز است")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonAlphabetSeri.IsNullOrWhiteSpace());

            RuleFor(v => v.ExtraParams.PersonIdentityNo).MaximumLength(10).WithMessage("طول شماره شناسنامه بیشتر از حد مجاز است ")
                .Must(ValidatorHelper.BeValidNumber).WithMessage("فرمت شماره شناسنامه اشتباه است")
                .When(x => x.ExtraParams is not null && !x.ExtraParams.PersonIdentityNo.IsNullOrWhiteSpace());

        }
    }
}
