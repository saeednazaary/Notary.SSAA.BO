using Notary.SSAA.BO.DataTransferObject.Bases;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class RelatedDocumentStandardContractPersonViewModel : EntityState
    {
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Tel { get; set; }
        public string PersonId { get; set; }
        public string DocumentId { get; set; }
    }
}
