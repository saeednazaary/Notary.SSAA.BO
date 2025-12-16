namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class UpdateSignRequestPaymentStateViewModel
    {
        public string IsCostPaid { get; set; }
        public string PaymentLink { get; set; }
        public string SignRequestId { get; set; }
        public string PayCostDate { get; set; }
        public string PayCostTime { get; set; }
        public string ReceiptNo { get; set; }
        public string BillNo { get; set; }
        public string SumPrices { get; set; }
        public string PaymentType { get; set; }

    }
}
