using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.SignRequest
{
    public class GetSignRequestByIdQuery : BaseQueryRequest<ApiResult<SignRequestViewModel>>
    {
        public GetSignRequestByIdQuery(string signrequestId)
        {
            SignRequestId = signrequestId;
        }
        public string SignRequestId { get; set; }
    }
}
