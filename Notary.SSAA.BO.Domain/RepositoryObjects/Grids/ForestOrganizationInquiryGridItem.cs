namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class ForestOrganizationInquiryGridItem
    {
        public string InquiryId { get; set; }
        public string InquiryNnumber { get; set; }
        public string InquiryDate { get; set; }
        public string InquirySendDate { get; set; }
        public string InquiryResponseDate { get; set; }
        public string EstatePostalCode { get; set; }
        public string EstateBasic { get; set; }
        public string EstateSecondary { get; set; }
        public string OwnerName { get; set; }        
        public string InquiryTrackingNo { get; set; }
        public string EstateProvince { get; set; }
        public string EstateCity { get; set; }
        public string Status { get; set; }
        public bool IsSelected { get; set; }
        public string StatusTitle { get; set; }
        public string DefectText { get; set; }
        public string InquiryTime { get; set; }
        public string InquirySendTime { get; set; }

        public string InquiryResponseTime { get; set; }
    }
}
