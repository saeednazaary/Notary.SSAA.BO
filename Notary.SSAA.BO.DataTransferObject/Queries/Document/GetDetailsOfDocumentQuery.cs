using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Document
{
    public class GetDetailsOfDocumentQuery : BaseQueryRequest<ApiResult<DocumentViewModel>>
    {
        public GetDetailsOfDocumentQuery ( string documentId, string detailName,CaseType caseType )
        {
            DocumentId = documentId;
            DetailName = detailName;
            CaseType = caseType;
        }

        public string DocumentId { get; set; }
        public string DetailName { get; set; }
        public CaseType CaseType { get; set; }
    }
}
