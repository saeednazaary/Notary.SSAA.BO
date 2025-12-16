using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;


namespace Notary.SSAA.BO.DataTransferObject.Validators.EPayment
{
    public class InquiryPaymentServiceValidator : AbstractValidator<InquiryPaymentServiceInput>
    {
        public InquiryPaymentServiceValidator()
        {
            RuleFor(x => x.NationalNo).NotEmpty().WithMessage("شماره پرداخت اجباری است .");

        }
    }
}
