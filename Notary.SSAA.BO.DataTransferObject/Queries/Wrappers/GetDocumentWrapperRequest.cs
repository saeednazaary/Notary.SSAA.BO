using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Wrappers;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Wrappers
{
    public class GetDocumentWrapperRequest : BaseQueryRequest<ApiResult<WrappersDocument>>
    {
        public GetDocumentWrapperRequest()
        {
            ScriptoriumId = "";
            NationalNo = "";
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ScriptoriumId { get; set; }
        public string NationalNo { get; set; }

    }

}
