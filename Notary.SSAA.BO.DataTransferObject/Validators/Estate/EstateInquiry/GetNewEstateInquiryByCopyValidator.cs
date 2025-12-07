using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry
{
    public class GetNewEstateInquiryByCopyValidator : AbstractValidator<GetNewEstateInquiryByCopyQuery>
    {
        public GetNewEstateInquiryByCopyValidator()
        {
            RuleFor(x => x.EstateInquiryId)
            .NotNull().WithMessage("شناسه استعلام اجباری است");
        }
    }
}
