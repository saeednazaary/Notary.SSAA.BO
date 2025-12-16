namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class SignRequestViewModel
    {
        public SignRequestViewModel()
        {
            SignRequestSubjectId = new List<string>();
            SignRequestGetterId = new List<string>();
            SignRequestPersons = new List<SignRequestPersonViewModel>();
            SignRequestRelatedPersons = new List<ToRelatedPersonViewModel>();
        }
        public bool IsValid { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDirty { get; set; }
        public string SignRequestId { get; set; }
        public string SignRequestReqNo { get; set; }
        public string SignRequestNationalNo { get; set; }
        public string SignRequestReqDate { get; set; }
        public string SignRequestSignDate { get; set; }
        public IList<string> SignRequestSubjectId { get; set; }
        public IList<string> SignRequestGetterId { get; set; }
        public string SignRequestSecretCode { get; set; }
        public string SignRequestTotalPrice { get; set; }
        public string SignRequestSignText { get; set; }
        public string SignRequestBillNo { get; set; }
        public string SignRequestReceiptNo { get; set; }
        public string SignRequestPayCostDateTime { get; set; }
        public string SignRequestPaymentMethod { get; set; }
        public string SignRequestConfirmer { get; set; }
        public string SignRequestConfirmDateTime { get; set; }
        public bool IsCostPaid { get; set; }
        public string StateId { get; set; }
        public string StateTitle => this.StateId switch
        {
            "1" => "پرونده ایجاد شده است",
            "2" => "تأیید نهایی شده است",
            "3" => "پرونده بسته شده است",
            _ => "وضعیت نا مشخص است",
        };
        public IList<SignRequestPersonViewModel> SignRequestPersons { get; set; }
        public IList<ToRelatedPersonViewModel> SignRequestRelatedPersons { get; set; }
        public string RemoteRequestId { get; set; }
        public string IsRemoteRequest { get; set; }
    }
}
