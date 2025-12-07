using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo
{
    public class SignRequestBasicInfoServiceInput : BaseExternalRequest<ApiResult<SignRequestBasicInfoViewModel>>
    {
        public string ScriptoriumId { get; set; }
        public string CurrentDateTime { get; set; }
    }
}
