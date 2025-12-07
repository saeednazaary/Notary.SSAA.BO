using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media
{
    public class CreateMediaServiceInput : BaseExternalRequest<ApiResult>
    {
        public CreateMediaServiceInput()
        {
            DocumentTypeId = "";
            DocumentNumber = "";
            DocumentDescription = "";
            DocumentTitle = "";
            ClientId = "";
            RelatedRecordId = "";
            CreateDateTime = "";
        }
        public string DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentDescription { get; set; }
        public string DocumentTitle { get; set; }
        public string ClientId { get; set; }
        public string RelatedRecordId { get; set; }
        public string CreateDateTime { get; set; }
    }
}
