using  Notary.SSAA.BO.DataTransferObject.Bases;
namespace  Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractPaymentsConfirmQueryViewModel : EntityState
    {
        public DocumentStandardContractPaymentsConfirmQueryViewModel() { }
        public string StateId { get; set; }
        public string IsCostPaymentConfirmed { get; set; }
    }
}