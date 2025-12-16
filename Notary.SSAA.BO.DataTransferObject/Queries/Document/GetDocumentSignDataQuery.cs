using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Document
{
    public class GetDocumentSignDataQuery : BaseQueryRequest<ApiResult<SignDataViewModel>>
    {
        public GetDocumentSignDataQuery(string documentId)
        {
            DocumentId = documentId;
        }

        public string DocumentId { get; set; }

    }
}
