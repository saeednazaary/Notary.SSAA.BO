using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices
{
    public class IPGPaymentRequestServiceInput : BaseExternalRequest<ApiResult<IPGPaymentRequestServiceViewModel>>
    {
        public IPGPaymentRequestServiceInput()
        {
            LstQuotation = new();
        }
        public string SystemRequestID { get; set; }
        public string RecordKey { get; set; }
        public string ClientId { get; set; }
        public string ServiceCode { get; set; }
        public int Amount { get; set; }
        public string BriefDesc { get; set; }
        public string DocTypeID { get; set; }
        public int HowtoPay { get; set; }
        public string Title { get; set; }
        public List<PaymentQuotation> LstQuotation { get; set; }
        public string InquiryCode { get; set; }
        public string RedirectAddress { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string NationalCode { get; set; }
        public int PspCode { get; set; }
        public string HowToQuotationWithVerhofe { get; set; }

    }
    public class PaymentQuotation
    {
        public string ExtensionData { get; set; }
        public string HowToQuotationWithVerhofe { get; set; }
        public string CostTypeID { get; set; }
        public string DetailPrice { get; set; }
        public bool IsGovernmental { get; set; }
        public bool IsMaliyati { get; set; }
        public bool IsSetadi { get; set; }
        public string AccountNo { get; set; }

    }
}
