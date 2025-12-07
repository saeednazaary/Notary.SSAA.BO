using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public class UpdateSignRequestPaymentStateValidator : AbstractValidator<UpdateSignRequestPaymentStateCommand>
    {
        public UpdateSignRequestPaymentStateValidator()
        {
            RuleFor(v => v.SignRequestId)
                .NotEmpty().WithMessage("شناسه اجباری است").When(x => string.IsNullOrWhiteSpace(x.SignRequestNo));
            RuleFor(v => v.SignRequestNo)
                .NotEmpty().WithMessage("شناسه معتبر نیست").When(x=>string.IsNullOrWhiteSpace(x.SignRequestId));
        }
    }
}
