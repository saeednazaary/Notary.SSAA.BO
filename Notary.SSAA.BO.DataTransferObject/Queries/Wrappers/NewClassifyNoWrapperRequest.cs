using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Wrappers
{
    public class NewClassifyNoWrapperRequest : BaseQueryRequest<ApiResult<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public NewClassifyNoWrapperRequest()
        {
            ScriptoriumId = "";
            DocumentId = "";
            ClassifyNo = "";
            GenerateSemaphore = false;
        }
        public string ScriptoriumId { get; set; }
        public string DocumentId { get; set; }
        public string ClassifyNo { get; set; }
        public bool GenerateSemaphore { get; set; }
    }

}
