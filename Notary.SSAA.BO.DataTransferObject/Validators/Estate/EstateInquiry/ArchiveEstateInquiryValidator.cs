using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry
{
    public class ArchiveEstateInquiryValidator : AbstractValidator<ArchiveEstateInquiryCommand>
    {
        public ArchiveEstateInquiryValidator()
        {
            RuleFor(x => x.EstateInquiryId).NotEmpty().WithMessage("شناسه استعلام ملک خالی می باشد");
        }
    }
}
