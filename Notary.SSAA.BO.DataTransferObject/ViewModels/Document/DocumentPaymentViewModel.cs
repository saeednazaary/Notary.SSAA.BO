using Notary.SSAA.BO.DataTransferObject.Bases;
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class DocumentPaymentViewModel : EntityState
    {
        public DocumentPaymentViewModel() { }
        public string Id { get; set; }
        public string DocumentId { get; set; }
        public string[] CostTypeId { get; set; }
        public string CostTypeTitle { get; set; }
        public string No { get; set; }
        public long? Price { get; set; }
        public string HowToQuotation { get; set; }
        public string HowToPay { get; set; }
        public string HowToPayTitle { get; set; }
        public string PaymentNo { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentTime { get; set; }
        public string PaymentType { get; set; }
        public string BankCounterInfo { get; set; }
        public string CardNo { get; set; }
        public string IsReused { get; set; }
        public string[] ReusedDocumentPaymentId { get; set; }
        public string ReusedDocumentPaymentTitle { get; set; }
        public string ScriptoriumId { get; set; }
        public DateTime? RecordDate { get; set; }
        public string Ilm { get; set; }
        public string LegacyId { get; set; }
    }
}