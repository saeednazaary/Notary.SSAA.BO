using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public class ShahkarServiceQuery : BaseQueryRequest<ApiResult<ShahkarServiceViewModel>>
    {
        public string MobileNumber { get; set; }
        public string NationalNo { get; set; }
        public string ClientId { get; set; }
        public ShahkarServiceQuery()
        {
            ClientId = "SSAR";
        }
    }
}
