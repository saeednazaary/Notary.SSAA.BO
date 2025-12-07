using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateTaxInquiry
{
    public class GetEstateTaxInquiryStatusValidator : AbstractValidator<GetEstateTaxInquiryStatusCommand>
    {
        public GetEstateTaxInquiryStatusValidator()
        {
            RuleFor(x=>x.EstateTaxInquiryId).NotEmpty().WithMessage("شناسه استعلام مالیاتی اجباری می باشد");
        }


    }
}

