using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint
{
    public class VerifyTFACodeCommand : BaseCommandRequest<ApiResult>

    {
        public string FingerprintId { get; set; }
        public string TFACode { get; set; }
    }
    public class VerifyTFACodeV2Command : BaseCommandRequest<ApiResult>

    {
        public string FingerprintId { get; set; }
        public string TFACode { get; set; }
    }
}
