using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.SignRequest
{
    public class GetSignRequestAdminByIdQuery : BaseQueryRequest<ApiResult<SignRequestViewModel>>
    {
        public GetSignRequestAdminByIdQuery(string signrequestId)
        {
            SignRequestId = signrequestId;
        }
        public string SignRequestId { get; set; }
    }
}
