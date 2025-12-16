using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Wrappers;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Wrappers
{
    public class ElectronicBookBaseInfoLastClassifyWrapperRequest : BaseQueryRequest<ApiResult<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public ElectronicBookBaseInfoLastClassifyWrapperRequest()
        {
            ScriptoriumId = "";
            UserName = string.Empty;
            Password = string.Empty;
        }
        public string ScriptoriumId { get; set; }

    }

}
