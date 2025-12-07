

namespace Notary.SSAA.BO.SharedKernel.SharedModels.DocumentReport
{
    public class ONotaryRegServiceInquiryEntity
    {
        public long InquiryCount { get; set; }
        public string Id { get; set; }
        public string InquirySpec { get; set; }
        public string ONotaryInquiryOrganizationId { get; set; }
        public string ONotaryInquiryOrganizationTitle { get; set; }
        public string ONotaryRegisterServiceReqId { get; set; }
        public string ReplyDate { get; set; }
        public string ReplyNo { get; set; }
        public string ReqDate { get; set; }
        public long? ReqNo { get; set; }
        public string Description { get; set; }
    }
}
