using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint
{
    public class SetPersonMocStateValidator : AbstractValidator<SetPersonMocStateCommand>
    {
        public SetPersonMocStateValidator()
        {
            
            RuleFor(v => v.FingerprintId)
                 .NotEmpty().WithMessage("شناسه اثر انگشت اجباری است .");
            RuleFor(v => v.MocState)
                 .NotEmpty().WithMessage("وضعیت MOC اجباری است .");

        }
    }
}
