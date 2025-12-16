using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ClientLogin;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ClientLogin
{
    public class ClientLoginServiceInput : BaseExternalRequest<ApiResult<ClientLoginViewModel>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ClientId { get; set; }
        public string GrantType { get; set; }
    }
}


