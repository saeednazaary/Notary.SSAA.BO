using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry
{
    public class ValidateEstateInquiryForDealSummaryValidator : AbstractValidator<ValidateEstateInquiryForDealSummaryQuery>
    {
        public ValidateEstateInquiryForDealSummaryValidator()
        {
            RuleFor(x => x.EstateInquiryId)
            .NotEmpty().WithMessage("شناسه استعلام اجباری است");
        }
    }
}
