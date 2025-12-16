using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint
{
    public class GetInquiryPersonFingerprintMocRelatedDataValidator : AbstractValidator<GetPersonFingerprintMocRelatedDataQuery>
    {
        public GetInquiryPersonFingerprintMocRelatedDataValidator()
        {

            RuleFor(v => v.FingerPrintId)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه اثر انگشت اجباری است .");

        }
    }
}
