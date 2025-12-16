using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry
{
    public class EstateInquiryPrintValidator : AbstractValidator<EstateInquiryPrintQuery>
    {
        public EstateInquiryPrintValidator()
        {
            RuleFor(x => x.EstateInquiryId).NotEmpty().WithMessage("شناسه استعلام ملک خالی می باشد");
        }
    }
    
}
