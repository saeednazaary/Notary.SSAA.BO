namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices
{
    public class IPGPaymentDetailServiceViewModel
    {
        public string ReferenceNO { get; set; }
        public int Amount { get; set; }
        public int PaymentType { get; set; }
        public string CardNO { get; set; }
        public string UserName { get; set; }
        public string SentToReverse { get; set; }
        public string SentToSettle { get; set; }
        public string ServiceDes { get; set; }
        public string ServiceId { get; set; }
        public string Password { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public DateTime RequestDate { get; set; }
        public string SystemRequestId { get; set; }
        public string MerchantID { get; set; }
        public string TrackingCode { get; set; }
        public string HowToQuotationWithVerhoeff { get; set; }
        public string TerminalId { get; set; }
        public string RecordKey { get; set; }
        public bool BankRequestResult { get; set; }
        public bool VerificationResult { get; set; }
        public bool SystemVerificationResult { get; set; }
        public string TranSuccessful { get; set; }
        public DateTime SettlementDateTime { get; set; }
        public string TraceNO { get; set; }
        public int TransactionStatus { get; set; }
        public DateTime ReverseDateTime { get; set; }
        public string Reverseresult { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
}
