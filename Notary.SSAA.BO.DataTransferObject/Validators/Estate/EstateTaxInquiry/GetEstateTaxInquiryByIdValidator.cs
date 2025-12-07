using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateTaxInquiry
{
    public class GetEstateTaxInquiryByIdValidator : AbstractValidator<GetEstateTaxInquiryByIdQuery>
    {
        public GetEstateTaxInquiryByIdValidator()
        {
            RuleFor(x => x.EstateTaxInquiryId)
            .NotNull().When(x=>string.IsNullOrWhiteSpace(x.LegacyId)).WithMessage("یکی از موارد شناسه استعلام یا شناسه استعلام در سامانه قدیمی باید مقدار داشته باشد");

            RuleFor(x => x.LegacyId)
       .NotNull().When(x => string.IsNullOrWhiteSpace(x.EstateTaxInquiryId)).WithMessage("یکی از موارد شناسه استعلام یا شناسه استعلام در سامانه قدیمی باید مقدار داشته باشد");
        }
    }
}
