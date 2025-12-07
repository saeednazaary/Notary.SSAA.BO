using System.ComponentModel;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Services.EPayment
{
    public class InquiryPaymentEndpointInput
    {
        public string NationalNo { get; set; }
        public string ClientId { get; set; } = "SSAR";

    }
}
