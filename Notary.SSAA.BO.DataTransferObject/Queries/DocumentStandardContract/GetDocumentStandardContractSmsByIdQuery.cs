using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract
{
    public class GetDocumentStandardContractSmsByIdQuery : BaseQueryRequest<ApiResult<DocumentStandardContractViewModel>>
    {
        public GetDocumentStandardContractSmsByIdQuery(string documentId)
        {
            DocumentId = documentId;
        }

        public string DocumentId { get; set; }
    }
}
