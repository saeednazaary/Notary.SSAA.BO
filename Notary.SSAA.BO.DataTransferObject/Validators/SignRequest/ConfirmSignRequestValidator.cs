using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public class ConfirmSignRequestValidator : AbstractValidator<ConfirmSignRequestCommand>
    {
        public ConfirmSignRequestValidator()
        {
            RuleFor(v => v.ElectronicBookSignedObjects)
                .NotEmpty().WithMessage("امضای دفاتر الکترونیک موجود نمیباشد . ");

            RuleFor(v => v.SignRequestSignedObject)
                .NotEmpty().WithMessage("امضای گواهی امضا موجود نمیباشد . ");
        }
    }
}
