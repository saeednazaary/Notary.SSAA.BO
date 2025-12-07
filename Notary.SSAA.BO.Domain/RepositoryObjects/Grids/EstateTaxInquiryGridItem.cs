namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class EstateTaxInquiryGridItem
    {
        public string InquiryId { get; set; }
        public string InquiryNo { get; set; }
        public string InquiryDate { get; set; }
        public string InquirySendDate { get; set; }
        public string InquiryResponseDate { get; set; }
        public string Status { get; set; }
        public string StatusTitle { get; set; }
        public string EstateUnitTitle { get; set; }
        public string EstateSectionTitle { get; set; }
        public string EstateSubSectionTitle { get; set; }
        public string EstateBasic { get; set; }
        public string EstateSecondary { get; set; }
        public string OwnerName { get; set; }
        public bool IsSelected { get; set; }
        public string OwnerNationalityCode { get; set; }
        public string RelatedServer { get; set; }
    }
}
