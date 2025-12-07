
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.SignRequest
{
    public class GetKatebSignRequestByIdQuery : BaseQueryRequest<ApiResult<KatebSignRequestViewModel>>
    {
        public GetKatebSignRequestByIdQuery(string signRequestId)
        {
            Id = signRequestId;
        }

        public string Id { get; set; }
    }
}
