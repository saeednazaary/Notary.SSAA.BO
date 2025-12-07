using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public class GetSignRequestPersonFingerprintValidator : AbstractValidator<GetSignRequestPersonFingerprintQuery>
    {
        public GetSignRequestPersonFingerprintValidator()
        {
            RuleFor(v => v.SignRequestId)
                .NotEmpty().WithMessage("شناسه اجباری است");
        }
    }
}
