using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint
{
    public class GetInquiryFingerprintValidator : AbstractValidator<GetInquiryPersonFingerprintQuery>
    {
        public GetInquiryFingerprintValidator()
        {

            RuleFor(v => v.FingerprintId)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه اثر انگشت اجباری است .");


        }
    }
}
