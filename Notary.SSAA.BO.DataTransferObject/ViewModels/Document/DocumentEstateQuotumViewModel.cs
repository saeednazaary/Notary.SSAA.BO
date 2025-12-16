using Notary.SSAA.BO.DataTransferObject.Bases;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class DocumentEstateQuotumViewModel: EntityState
    {
        public string DocumentEstateQuotumId { get; set; }

        public string DocumentEstateId { get; set; }

        public IList<string> DocumentPersonId { get; set; }

        public string DetailQuota { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string FullName => Name + " " + Family;

        public string TotalQuota { get; set; }

        public string QuotaText { get; set; }
    }
}
