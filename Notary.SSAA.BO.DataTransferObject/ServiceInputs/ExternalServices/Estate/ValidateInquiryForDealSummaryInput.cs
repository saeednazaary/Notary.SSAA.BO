using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class ValidateInquiryForDealSummaryInput : BaseExternalRequest<ApiResult<InquiryIsValidForDealSummaryOutput>>
    {
        public ValidateInquiryForDealSummaryInput()
        {
            ClientId = "SSAR";
            ConsumerPassword = "";
            ConsumerUsername = "";
            InquiryId = "";
            ReceiverCmsorganizationId = "";
            RequestDateTime = "";
        }
        public string ConsumerPassword { get; set; }
        public string ConsumerUsername { get; set; }
        public string ReceiverCmsorganizationId { get; set; }
        public string RequestDateTime { get; set; }
        public string InquiryId { get; set; }
        public string ClientId { get; set; }
    }
}
