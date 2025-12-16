using  Notary.SSAA.BO.DataTransferObject.Bases;


namespace  Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class DocumentPaymentQuestionViewModel : EntityState
    {
        public string DocumentId { get; set; }
        public bool? IsCostPaymentConfirmed { get; set; }
    }
}
