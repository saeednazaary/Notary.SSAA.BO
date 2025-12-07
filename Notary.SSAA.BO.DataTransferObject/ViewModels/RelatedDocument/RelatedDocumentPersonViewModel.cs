using Notary.SSAA.BO.DataTransferObject.Bases;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.RelatedDocument
{
    public class RelatedDocumentPersonViewModel : EntityState
    {
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Tel { get; set; }
        public string DocumentId { get; set; }
        public IList<string> PersonId { get; set; }
    }
}
