namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class ReusedDocumentPaymentLookupRepositoryObject
    {
        public ReusedDocumentPaymentLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ReusedDocumentPaymentLookupItem> GridItems { get; set; }
        public List<ReusedDocumentPaymentLookupItem> SelectedItems { get; set; }
    }

    public class ReusedDocumentPaymentLookupExtraParams
    {
        public string Id { get; set; }
        public string DocumentId { get; set; }
        public string CostTypeId { get; set; }
        public string CostTypeTitle { get; set; }
        public string No { get; set; }
        public long? Price { get; set; }
        public string HowToQuotation { get; set; }
        public string HowToPay { get; set; }
        public string PaymentNo { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentTime { get; set; }
        public string PaymentType { get; set; }
        public string BankCounterInfo { get; set; }
        public string CardNo { get; set; }
        public DateTime? RecordDate { get; set; }
    }
}