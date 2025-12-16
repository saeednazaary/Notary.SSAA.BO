using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract
{
    public class GetDetailsOfDocumentStandardContractQuery : BaseQueryRequest<ApiResult<DocumentStandardContractViewModel>>
    {
        public GetDetailsOfDocumentStandardContractQuery ( string documentId, string detailName,CaseType caseType )
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
