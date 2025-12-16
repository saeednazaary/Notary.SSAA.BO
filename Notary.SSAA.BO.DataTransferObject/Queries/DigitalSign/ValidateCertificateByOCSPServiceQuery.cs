using Newtonsoft.Json;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign
{
    public class ValidateCertificateByOCSPServiceQuery : BaseQueryRequest<ApiResult<ValidateCertificateByOCSPViewModel>>
    {
        [JsonProperty ( "certificate" )]
        public string Certificate { get; set; }
        [JsonProperty("clientId")]
        public string ClientId { get; set; }
        public ValidateCertificateByOCSPServiceQuery()
        {
            this.ClientId = "SSAR";
        }
    }


}
