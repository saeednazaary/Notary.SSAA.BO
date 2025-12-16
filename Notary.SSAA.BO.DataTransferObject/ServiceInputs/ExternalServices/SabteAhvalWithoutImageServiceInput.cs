using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices
{
    public class SabteAhvalWithoutImageServiceInput : BaseExternalRequest<ApiResult<SabtAhvalServiceViewModel>>
    {
        public string ClientId { get; set; }
        public string BirthDate { get; set; }
        public string NationalNo { get; set; }
        public string MainObjectId { get; set; }
    }
}
