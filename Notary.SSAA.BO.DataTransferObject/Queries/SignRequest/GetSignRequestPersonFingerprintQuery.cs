using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.SignRequest
{
    public class GetSignRequestPersonFingerprintQuery : BaseQueryRequest<ApiResult<List<SignRequestPersonFingerprintViewModel>>>
    {
        public GetSignRequestPersonFingerprintQuery(string signRequestId)
        {
            SignRequestId = signRequestId;
        }
        public string SignRequestId { get; set; }
    }
}
