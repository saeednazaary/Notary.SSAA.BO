using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateTaxInquiry
{
    public class GetNewEstateTaxInquiryByCopyValidator : AbstractValidator<GetNewEstateTaxInquiryByCopyQuery>
    {
        public GetNewEstateTaxInquiryByCopyValidator()
        {
            RuleFor(x => x.EstateTaxInquiryId)
            .NotNull().WithMessage("شناسه استعلام اجباری است");
        }
    }
}
