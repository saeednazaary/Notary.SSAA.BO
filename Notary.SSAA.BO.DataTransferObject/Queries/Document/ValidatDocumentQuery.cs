using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.DataTransferObject.Commands.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Document
{
    public class ValidatDocumentQuery : BaseQueryRequest<ApiResult<ValidatDocumentViewModel>>
    {
        public ValidatDocumentQuery(string documentdate, string documentnationalno, string documentscriptoriumid, string documenttypeid, string secretcode, bool isrelateddocumentinssar)
        {
            DocumentDate = documentdate;
            DocumentNationalNo = documentnationalno;
            DocumentScriptoriumId = documentscriptoriumid;
            DocumentTypeId = documenttypeid;
            IsRelatedDocumentInSSAR = isrelateddocumentinssar;
            Secretcode = secretcode;
        }
        public string DocumentDate { get; set; }
        public string DocumentNationalNo { get; set; }
        public string DocumentScriptoriumId { get; set; }
        public string DocumentTypeId { get; set; }
        public bool IsRelatedDocumentInSSAR { get; set; }
        public string Secretcode { get; set; }
    }
}
