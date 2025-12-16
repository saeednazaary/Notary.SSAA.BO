using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public class ReadyToPaySignRequestValidator : AbstractValidator<ReadyToPaySignRequestCommand>
    {
        public ReadyToPaySignRequestValidator()
        {
            RuleFor(v => v.SignRequestId)
                .NotEmpty().WithMessage("شناسه اجباری است");
        }
    }
}
