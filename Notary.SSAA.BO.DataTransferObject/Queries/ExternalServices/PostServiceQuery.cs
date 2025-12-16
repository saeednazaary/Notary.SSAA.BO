using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public class PostServiceQuery : BaseQueryRequest<ApiResult<PostServiceViewModel>>
    {
        public string ClientId { get; set; }
        public string PostalCode { get; set; }
        public PostServiceQuery()
        {
            ClientId = "SSAR";
        }
    }
}
