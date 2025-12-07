using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.SignRequest
{
    public class CheckSignRequestFinalStateQuery: BaseQueryRequest<ApiResult>
    {
        public string SignRequestId { get; set; }
    }
}
