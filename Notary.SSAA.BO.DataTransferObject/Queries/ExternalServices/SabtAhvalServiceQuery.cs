using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public class SabtAhvalServiceQuery : BaseQueryRequest<ApiResult<SabtAhvalServiceViewModel>>
    {
        public string BirthDate { get; set; }

        public string NationalNo { get; set; }

        public bool WithPostData { get; set; }
        public bool WithImageData { get; set; }

        public string CardBackSerial { get; set; }
        public string ClientId { get; set; }
        public SabtAhvalServiceQuery()
        {
            ClientId = "SSAR";
        }
    }
}
