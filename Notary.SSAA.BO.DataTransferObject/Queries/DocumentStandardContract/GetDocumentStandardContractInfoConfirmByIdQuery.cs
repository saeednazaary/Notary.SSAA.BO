using  Notary.SSAA.BO.DataTransferObject.Bases;
using  Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using  Notary.SSAA.BO.SharedKernel.Result;


namespace  Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract
{
  
    public class GetDocumentStandardContractInfoConfirmByIdQuery : BaseQueryRequest<ApiResult<DocumentStandardContractViewModel>>
    {
        public GetDocumentStandardContractInfoConfirmByIdQuery(string documentId)
        {
            DocumentId = documentId;
        }
        public string DocumentId { get; set; }


    }

}
