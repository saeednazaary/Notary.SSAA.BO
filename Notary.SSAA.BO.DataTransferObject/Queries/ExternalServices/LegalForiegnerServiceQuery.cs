using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public class LegalForiegnerServiceQuery : BaseQueryRequest<ApiResult<LegalForeignerServiceViewModel>>
    {
        public string ForeignerCode { get; set; }
        public string ClientId { get; set; }
        public LegalForiegnerServiceQuery()
        {
            ClientId = "SSAR";
        }

    }
}
