using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint
{
    public class CreatePersonFingerprintCommand : BaseCommandRequest<ApiResult>
    {
        public string ClientId { get; set; }
        public string MainObjectId { get; set; }
        public string PersonObjectId { get; set; }
        public string PersonNationalNo { get; set; }
        public string PersonMobileNo { get; set; }
        public string PersonNameFamily { get; set; }
        public bool IsTFARequired { get; set; }
    }
    public class CreatePersonFingerprintV2Command : BaseCommandRequest<ApiResult>
    {
        public string ClientId { get; set; }
        public string MainObjectId { get; set; }
        public string PersonObjectId { get; set; }
        public string PersonNationalNo { get; set; }
        public string PersonMobileNo { get; set; }
        public string PersonNameFamily { get; set; }
        public bool IsTFARequired { get; set; }
    }
}
