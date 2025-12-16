

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class EstateDocumentRequestGridItem
    {
        public string RequestId { get; set; }
        public string RequestNo { get; set; }
        public string RequestDate { get; set; }
        public string RequestTypeTitle { get; set; }
        public string PreviousRequestNo { get; set; }
        public string Status { get; set; }  
        public string StatusTitle { get; set; }
        public string OwnershipDocumentTypeTitle { get; set; }
        public string EstateTypeTitle { get; set; }
        public string EstateUnitTitle { get; set; }
        public string EstateSectionTitle { get; set; }
        public string EstateSubSectionTitle { get; set; }
        public string EstateBasic { get; set; }
        public string EstateSecondary { get; set; }
        public string OwnerName { get; set; }
        
        public bool IsSelected { get; set; }

        public string Hamesh { get; set; }
        public string RequestTime { get; set; }
    }
}
