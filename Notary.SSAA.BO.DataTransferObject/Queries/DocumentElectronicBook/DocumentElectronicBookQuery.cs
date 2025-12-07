using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentElectronic;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.DocumentElectronicBook
{
    public class DocumentElectronicBookQuery : BaseQueryRequest<ApiResult<DocumentElectronicBookViewModel>>
    {

        public DocumentElectronicBookQuery()
        {
            ScriptoriumId = "";
            NationalNo = "";
            ClassifyNo = "";
        }

        public string ScriptoriumId { get; set; }
        public string NationalNo { get; set; }
        public string ClassifyNo { get; set; }
        public bool GetMaxClassifyNo { get; set; }
        public DocumentDataSection? Section { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public enum DocumentDataSection
    {
        None = 0,
        Header = 1,
        Persons = 2,
        Cases = 3,
        OwnershipDocuments = 4,
        DocumentText = 5
    }
}
