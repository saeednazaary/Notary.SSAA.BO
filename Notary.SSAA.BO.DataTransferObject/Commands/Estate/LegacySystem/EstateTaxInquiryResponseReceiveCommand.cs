using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.LegacySystem
{
    public class EstateTaxInquiryResponseReceiveCommand : BaseExternalCommandRequest<ExternalApiResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TrackingCode { get; set; }
        public int Status { get; set; }
        public string PaymentId { get; set; }
        public string PaymentId2 { get; set; }
        public string PaymentId3 { get; set; }
        public string TaxAmount { get; set; }
        public string TaxAmount2 { get; set; }
        public string TaxAmount3 { get; set; }
        public bool IsLicenseReady { get; set; }
        public string LicenseNumber { get; set; }
        public string LicenseHtml { get; set; }
        public string ShebaNo { get; set; }
        public string ShebaNo2 { get; set; }
        public string ShebaNo3 { get; set; }
        public string TaxBillHtml { get; set; }
    }
}
