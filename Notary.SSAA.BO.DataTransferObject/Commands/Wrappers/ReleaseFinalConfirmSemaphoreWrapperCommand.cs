
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAR.Wrappers.WebApi.Models.RequestsModel
{
    public class ReleaseFinalConfirmSemaphoreWrapperCommand : BaseCommandRequest<ApiResult>
    {
        public ReleaseFinalConfirmSemaphoreWrapperCommand()
        {
            ScriptoriumId = "";
            UserName = string.Empty;
            Password = string.Empty;
        }
        public string ScriptoriumId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}
