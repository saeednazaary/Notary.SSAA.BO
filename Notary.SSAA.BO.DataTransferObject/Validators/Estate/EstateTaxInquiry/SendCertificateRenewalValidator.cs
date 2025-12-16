using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateTaxInquiry
{
    public class SendCertificateRenewalValidator : AbstractValidator<SendCertificateRenewalCommand>
    {
        public SendCertificateRenewalValidator()
        {
            RuleFor(x => x.EstateTaxInquiryId)
            .NotNull().WithMessage("شناسه استعلام مالیاتی اجباری می باشد");   
        }
    }
}
