using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateArticle6Inquiry
{
    public class EstateArticle6InquiryResponseCommand : BaseExternalCommandRequest<ExternalApiResult>
    {
        public EstateArticle6InquiryResponseCommand()
        {

        }
        public string ResponseOrganization { get; set; }
        public int ResponseType { get; set; }
        public string ResponseDate { get; set; }
        public string ResponseNo { get; set; }
        public string RequestNo { get; set; }
        public string Description { get; set; }
        public string OppositionReasonCode { get; set; }
        public string OppositionReasonTitle { get; set; }
        public string Map { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
