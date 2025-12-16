using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.LegacySystem;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateTaxInquiry
{
    public class ReceiveTaxInquiryResponseCommandValidator : AbstractValidator<EstateTaxInquiryResponseReceiveCommand>
    {
        public ReceiveTaxInquiryResponseCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("1-" + "نام کاربری یا کلمه عبور  خالی می باشد");
            RuleFor(x => x.Password).NotEmpty().WithMessage("1-" + "نام کاربری یا کلمه عبور  خالی می باشد");
            RuleFor(x => x.TrackingCode).NotEmpty().WithMessage("2-" + "کد رهگیری استعلام مالیاتی خالی می باشد");
            RuleFor(x => x.Status).NotEmpty().WithMessage("3-" + "مقدار وضعیت نا معتبر  می باشد");
            RuleFor(x => x.LicenseNumber).NotEmpty().When(x => x.Status == 40).WithMessage("4-" + "شماره گواهی و فایل گواهی خالی می باشد");
            RuleFor(x => x.LicenseHtml).NotEmpty().When(x => x.Status == 40).WithMessage("5-" + " فایل گواهی خالی می باشد");
            RuleFor(x => x.PaymentId).NotEmpty().When(x => x.Status == 30 ).WithMessage("4-" + "شناسه 30 رقمی پرداخت خالی می باشد");
            RuleFor(x => x.TaxAmount).NotEmpty().When(x => x.Status == 30).WithMessage("5-" + "مبلغ بدهی خالی می باشد");
            RuleFor(x => x.TaxBillHtml).NotEmpty().When(x => x.Status == 30).WithMessage("6-" + "فایل قبض مالیاتی خالی می باشد");
            RuleFor(x => x.ShebaNo).NotEmpty().When(x => x.Status == 30).WithMessage("7-" + "شماره شبا خالی می باشد");
        }


    }
}

