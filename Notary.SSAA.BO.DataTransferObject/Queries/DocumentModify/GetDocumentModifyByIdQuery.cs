using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentModify;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.DocumentModify
{
    public class GetDocumentModifyByIdQuery : BaseQueryRequest<ApiResult<DocumentModifyViewModel>>
    {
        public GetDocumentModifyByIdQuery(string documentModifyId)
        {
            DocumentModifyId = documentModifyId;
        }
        public string DocumentModifyId { get; set; }
    }
}
