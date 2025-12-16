using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExordiumAccount;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.Exordium
{
    public class GetAccountInfoByScriptoriumCodeInput : BaseExternalRequest<ApiResult<GetAccountInfoByScriptoriumCodeViewModel>>
    {
        public string ScriptoriumCode { get; set; }
    }
}
