using Microsoft.AspNetCore.Components.Forms;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Stimulsoft.System.Windows.Forms;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractSmsViewModel : EntityState
    {
        public DocumentStandardContractSmsViewModel() { }
        public string DocumentId { get; set; }
        public string Id { get; set; }
        public string MobileNo { get; set; }
        public string SmsText { get; set; }
        public string ReceiverName { get; set; }
        public string IsMechanizedTitle { get; set; }
        public string IsSentTitle { get; set; }
        public bool IsMechanized { get; set; }
        public bool IsSent { get; set; }
        public string CreateDateTime { get; set; }
    }
}
