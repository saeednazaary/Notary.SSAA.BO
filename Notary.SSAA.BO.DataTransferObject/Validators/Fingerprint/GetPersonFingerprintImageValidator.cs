using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint
{
    public class GetInquiryPersonFingerprintListValidator : AbstractValidator<GetInquiryPersonFingerprintListQuery>
    {
        public GetInquiryPersonFingerprintListValidator()
        {

            RuleFor(v => v.MainObjectId)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه مدرک مربوطه اجباری است .");

        }
    }
}
