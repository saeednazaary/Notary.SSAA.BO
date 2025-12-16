using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.LegacySystem;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.LegacySystem
{
    public class LegacySystemCommandValidator : AbstractValidator<LegacySystemCommand>
    {
        public LegacySystemCommandValidator()
        {
            RuleFor(x => x.UserName)
               .NotEmpty().WithMessage("1");
            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("1");
            RuleFor(x => x.EntityType)
               .NotEmpty().WithMessage("2");
            RuleFor(x => x.Data)
                .NotEmpty().WithMessage("3");
        }
    }
}
