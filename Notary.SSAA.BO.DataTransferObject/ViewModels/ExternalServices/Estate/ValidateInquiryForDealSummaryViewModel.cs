using System.Runtime.Serialization;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate
{
    public class ValidateInquiryForDealSummaryViewModel
    {
        public ValidateInquiryForDealSummaryViewModel()
        {
            Data = new InquiryIsValidForDealSummaryOutput();
        }
        public InquiryIsValidForDealSummaryOutput Data { get; set; }
    }
    public class InquiryIsValidForDealSummaryOutput
    {

        public int IsValid { get; set; }
        public bool Successful
        {
            get;
            set;
        }
        public string ErrorMessage
        {
            get;
            set;
        }
        public string ResponseNo { get; set; }
        public string ResponseDateTime { get; set; }
        public string RequestDateTime { get; set; }
    }
}
