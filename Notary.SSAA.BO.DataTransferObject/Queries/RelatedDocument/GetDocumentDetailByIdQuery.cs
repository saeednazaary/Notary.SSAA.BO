using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.RelatedDocument;
using Notary.SSAA.BO.SharedKernel.Result;

namespace   Notary.SSAA.BO.DataTransferObject.Queries.RelatedDocument
{
    public class GetDocumentDetailByIdQuery : BaseQueryRequest<ApiResult<DocumentDetailViewModel>>
    {
        public GetDocumentDetailByIdQuery(string documentId)
        {
            DocumentId = documentId;
        }
        public string DocumentId { get; set; }

    }
}
