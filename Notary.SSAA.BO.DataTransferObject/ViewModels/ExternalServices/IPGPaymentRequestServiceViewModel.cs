namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices
{
    public class IPGPaymentRequestServiceViewModel
    {
        public string TrackingCode { get; set; }
        public string HowToQuotationWithVerhoeff { get; set; }
        public string PaymentLink { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
}
