using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateArticle6Inquiry
{
    public class EstateArticle6InquiryRelatedOrgansCommand : BaseExternalCommandRequest<ExternalApiResult>
    {
        public EstateArticle6InquiryRelatedOrgansCommand()
        {
            this.RelatedOrganList = new List<EstateArticle6InquiryRelatedOrgans>();
        }
        public List<EstateArticle6InquiryRelatedOrgans> RelatedOrganList {  get; set; }
        public string RequestNo { get; set; }              
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class EstateArticle6InquiryRelatedOrgans
    {
        public string MinistryCode { get; set; }
        public string OrganizationCode { get; set; }
        public string OrganizationTrackingCode { get; set; }
        public string SendDate { get; set; }
    }
}
