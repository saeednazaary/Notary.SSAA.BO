using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry
{
    public class GetDocumentRelatedDataValidator : AbstractValidator<GetDocumentRelatedDataQuery>
    {
        public GetDocumentRelatedDataValidator()
        {
            RuleFor(x => x.EstateInquiryId).NotEmpty().WithMessage("شناسه استعلام اجباری می باشد");
            //RuleFor(x => x.IsAttachment).NotEmpty().WithMessage("انتقال از نوع منضم هست یا نه ، مشخص نشده است");
            RuleFor(x => x.IsRegistered).NotEmpty().WithMessage("ملک ثبت شده می باشد یا نه،مشخص نشده است");
            RuleFor(x => x.DocumentTypeCode).NotEmpty().WithMessage("کد نوع سند اجباری می باشد");
        }
    }
}
