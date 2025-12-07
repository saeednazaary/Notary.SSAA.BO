using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint
{
    public class DeletePersonFingerprintCommand : BaseCommandRequest<ApiResult>
    {
        public string FingerprintId { get; set; }
    }
    public class DeletePersonFingerprintV2Command : BaseCommandRequest<ApiResult>
    {
        public string FingerprintId { get; set; }
    }
}
