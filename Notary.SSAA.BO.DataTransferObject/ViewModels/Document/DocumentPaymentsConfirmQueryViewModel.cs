using  Notary.SSAA.BO.DataTransferObject.Bases;
namespace  Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class DocumentPaymentsConfirmQueryViewModel : EntityState
    {
        public DocumentPaymentsConfirmQueryViewModel() { }
        public string StateId { get; set; }
        public string IsCostPaymentConfirmed { get; set; }
    }
}