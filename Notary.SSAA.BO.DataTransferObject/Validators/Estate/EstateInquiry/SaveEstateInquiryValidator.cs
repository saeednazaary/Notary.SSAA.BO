using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry
{
    public class SaveEstateInquiryValidator : AbstractValidator<SaveEstateInquiryCommand>
    {
        public SaveEstateInquiryValidator()
        {
            RuleFor(x => x.Data)
                .NotEmpty().WithMessage("داده ورودی جهت ثبت در سیستم اجباری  می باشد");
        }
    }
}
