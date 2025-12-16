using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry
{
    public class ValidateEstateInquiryItemsValidator : AbstractValidator<ValidateEstateInquiryItemsQuery>
    {
        public ValidateEstateInquiryItemsValidator()
        {
            RuleFor(x => x.Basic)
            .NotNull().WithMessage("مقدار پلاک اصلی نمی تواند خالی باشد");
            RuleFor(x => x.DocPrintNo)
            .NotNull().WithMessage("مقدار شماره چاپی سند نمی تواند خالی باشد");
            RuleFor(x => x.InquiryDate)
           .NotNull().WithMessage("مقدار تاریخ استعلام نمی تواند خالی باشد");
            RuleFor(x => x.InquiryNo)
           .NotNull().WithMessage("مقدار شماره استعلام نمی تواند خالی باشد");
            RuleFor(x => x.ScriptoriumId)
           .NotNull().WithMessage("مقدار شناسه دفترخانه نمی تواند خالی باشد");
            RuleFor(x => x.UnitId)
           .NotNull().WithMessage("مقدار شناسه واحد ثبتی نمی تواند خالی باشد");

        }
    }
}
