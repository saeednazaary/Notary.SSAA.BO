
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAR.Wrappers.WebApi.Models.RequestsModel
{
    public class CreateSemaphoreWrapperCommand : BaseCommandRequest<ApiResult>
    {
        public CreateSemaphoreWrapperCommand()
        {
            ScriptoriumId = "";
            SemaphorType = null;            
        }
        public string ScriptoriumId { get; set; }
        public int? SemaphorType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}
