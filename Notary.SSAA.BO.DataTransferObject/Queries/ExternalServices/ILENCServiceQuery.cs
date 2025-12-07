using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public class ILENCServiceQuery : BaseQueryRequest<ApiResult<ILENCServiceViewModel>>
    {
        public string NationalNo { get; set; }
        public string ClientId { get; set; }
        public ILENCServiceQuery()
        {
            ClientId = "SSAR";
        }
    }
}
