using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateTaxInquiry
{
    public class SaveEstateTaxInquiryValidator : AbstractValidator<SaveEstateTaxInquiryCommand>
    {
        public SaveEstateTaxInquiryValidator()
        {
            RuleFor(x => x.Data)
                .NotEmpty().WithMessage("داده ورودی جهت ثبت در سیستم اجباری  می باشد");
        }
    }
}
