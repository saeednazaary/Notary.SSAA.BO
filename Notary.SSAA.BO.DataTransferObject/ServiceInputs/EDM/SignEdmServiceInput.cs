

using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices.EDM;
using Notary.SSAA.BO.SharedKernel.Result;

namespace SNotary.SSAA.BO.DataTransferObject.ServiceInputs.Edm
{
    public class SignEdmServiceInput : BaseExternalRequest<ApiResult<SignEdmServiceResponse>>
    {
        public string TokenAndClaims { get; set; }
        public string OrginalB64Doc { get; set; }
        public string SignerId { get; set; }
        public string ClientId { get; set; }
        public string MainObjectId { get; set; }
    }
}
