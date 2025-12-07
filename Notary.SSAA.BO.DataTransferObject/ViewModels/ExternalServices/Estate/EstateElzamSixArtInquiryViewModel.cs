
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate
{
    public class EstateElzamSixArtInquiryViewModel
    {
        public EstateElzamSixArtInquiryViewModel()
        {
            this.ResMessage = string.Empty;
        }
        public int ResCode { get; set; }
        public string ResMessage { get; set; }
        public EstateElzamSixArtInquiryResult Data { get; set; }
    }
    public class EstateElzamSixArtInquiryResult
    {
        public string TrackingCode { get; set; }
    }
}
