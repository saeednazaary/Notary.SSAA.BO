using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo
{
    public class ScriptoriumInput : BaseExternalRequest<ApiResult<ScriptoriumViewModel>>
    {
        public ScriptoriumInput(string[] idList)
        {
            IdList = idList;
        }
        public string[] IdList { get; set; }
    }
}
