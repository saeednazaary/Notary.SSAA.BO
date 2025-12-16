using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry
{
    public class GetEstateOwnersByInquiryIdValidator : AbstractValidator<GetEstateOwnersByInquiryIdQuery>
    {
        public GetEstateOwnersByInquiryIdValidator()
        {
            RuleFor(x => x.EstateInquiryId)
            .NotNull().WithMessage("شناسه استعلام اجباری است");
        }
    }
}
