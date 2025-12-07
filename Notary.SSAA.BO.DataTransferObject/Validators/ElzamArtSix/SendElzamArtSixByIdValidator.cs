using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.ElzamArtSix;

namespace Notary.SSAA.BO.DataTransferObject.Validators.ElzamArtSix
{
    public class SendElzamArtSixByIdValidator : AbstractValidator<SendElzamArtSixCommand>
    {
        public SendElzamArtSixByIdValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("شناسه استعلام نمی تواند خالی باشد");
        }
    }
}