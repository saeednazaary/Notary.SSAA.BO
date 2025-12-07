using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices
{
    public class SendSmsFromKanoonServiceInput : BaseExternalRequest<ApiResult<SMSFromKanoonServiceViewModel>>
    {
        public string MobileNo { get; set; }
        public string ClientId { get; set; }
        public string Message { get; set; }
        public string IsSuccess { get; set; }
    }
}
