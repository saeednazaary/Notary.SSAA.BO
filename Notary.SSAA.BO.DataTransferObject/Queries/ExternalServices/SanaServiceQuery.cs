using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public class SanaServiceQuery : BaseQueryRequest<ApiResult<SanaServiceViewModel>>
    {
        public string NationalNo { get; set; }
        public string ClientId { get; set; }
        public SanaServiceQuery()
        {
            ClientId = "SSAR";
        }
    }
}
