using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry
{
    public class GetEstateInquiryLastNoValidator : AbstractValidator<GetEstateInquiryLastNoQuery>
    {
        public GetEstateInquiryLastNoValidator()
        {
            RuleFor(x => x.Year)
         .NotEmpty().WithMessage("مقدار سال اجباری می باشد");
            RuleFor(x => x.ScriptoriumCode)
            .NotEmpty().WithMessage("کد دفترخانه اجباری می باشد");
        }
    }
}
