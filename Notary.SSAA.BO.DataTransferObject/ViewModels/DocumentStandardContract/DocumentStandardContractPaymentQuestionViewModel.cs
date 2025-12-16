using  Notary.SSAA.BO.DataTransferObject.Bases;


namespace  Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractPaymentQuestionViewModel : EntityState
    {
        public string DocumentId { get; set; }
        public bool? IsCostPaymentConfirmed { get; set; }
    }
}
