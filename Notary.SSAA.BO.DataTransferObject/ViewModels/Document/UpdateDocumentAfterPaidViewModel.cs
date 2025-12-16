namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class UpdateDocumentAfterPaidViewModel
    {
        public string IsCostPaymentConfirmed { get; set; }
        public string State { get; set; }
        public string CostPaymentDate { get; set; }
        public string CostPaymentTime { get; set; }
        public string ReceiptNo { get; set; }
        public string BillNo { get; set; }
        public string PaymentType { get; set; }
    }
}
