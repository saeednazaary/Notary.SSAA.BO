using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateTaxInquiry
{
    public class ExistsActiveEstateTaxInquiryValidator : AbstractValidator<ExistsActiveEstateTaxInquiryQuery>
    {
        public ExistsActiveEstateTaxInquiryValidator()
        {
            RuleFor(x => x.EstateInquiryId)
            .NotNull().WithMessage("شناسه استعلام ملک اجباری می باشد");            
        }
    }
}
