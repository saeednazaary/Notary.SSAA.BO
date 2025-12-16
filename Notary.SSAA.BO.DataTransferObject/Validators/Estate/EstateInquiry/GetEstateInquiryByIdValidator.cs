using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry
{
    public class GetEstateInquiryByIdValidator : AbstractValidator<GetEstateInquiryByIdQuery>
    {
        public GetEstateInquiryByIdValidator()
        {
            RuleFor(x => x.EstateInquiryId)
            .NotNull().When(x => string.IsNullOrWhiteSpace(x.LegacyId)).WithMessage("یکی از آیتم های شناسه استعلام و یا شناسه استعلام در سامانه قدیمی باید مقدار داشته باشد");

            RuleFor(x => x.LegacyId)
            .NotNull().When(x => string.IsNullOrWhiteSpace(x.EstateInquiryId)).WithMessage("یکی از آیتم های شناسه استعلام و یا شناسه استعلام در سامانه قدیمی باید مقدار داشته باشد");
        }
    }
}
