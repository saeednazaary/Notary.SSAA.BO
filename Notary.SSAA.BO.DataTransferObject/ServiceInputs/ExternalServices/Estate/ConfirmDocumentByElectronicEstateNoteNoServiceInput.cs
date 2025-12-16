using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class ConfirmDocumentByElectronicEstateNoteNoServiceInput : BaseExternalRequest<ApiResult<ConfirmDocumentByElectronicEstateNoteNoViewModel>>
    {
        public ConfirmDocumentByElectronicEstateNoteNoServiceInput()
        {
            ClientId = "SSAR";
            ConsumerPassword = "1284369772";
            ConsumerUsername = "sabtman";
        }
        public string ConsumerPassword { get; set; }
        public string ConsumerUsername { get; set; }
        public string ElectronicEstateNoteNo { get; set; }
        public string NationalityCode { get; set; }
        public string ReceiverCmsorganizationId { get; set; }
        public string RequestDateTime { get; set; }
        public string ClientId { get; set; }
    }
}
