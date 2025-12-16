using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public class UpdateRemoteRequestFingerprintStateValidator : AbstractValidator<UpdateRemoteRequestFingerprintStateCommand>
    {
        public UpdateRemoteRequestFingerprintStateValidator()
        {
            RuleFor(v => v.SignRequestNo)
                .NotEmpty().WithMessage("شماره گواهی امضا معتبر نیست .");
            RuleFor(v => v.PersonNationalNo)
                .Must(ValidatorHelper.BeValidPersianNationalNumber).WithMessage("شماره ملی شخص معتبر نیست . ");
        }
    }
}
