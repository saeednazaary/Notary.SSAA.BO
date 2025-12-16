using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Wrappers
{
    public class NewDocumentReqNoWrapperRequest : BaseQueryRequest<ApiResult<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public NewDocumentReqNoWrapperRequest()
        {
            ScriptoriumId = "";
            UserName = string.Empty;
            Password = string.Empty;
        }
        public string ScriptoriumId { get; set; }

    }

}
