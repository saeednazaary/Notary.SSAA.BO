using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry
{
    public class DeArchiveEstateInquiryValidator : AbstractValidator<DeArchiveEstateInquiryCommand>
    {
        public DeArchiveEstateInquiryValidator()
        {
            RuleFor(x => x.EstateInquiryId).NotEmpty().WithMessage("شناسه استعلام ملک خالی می باشد");
        }
    }
}
