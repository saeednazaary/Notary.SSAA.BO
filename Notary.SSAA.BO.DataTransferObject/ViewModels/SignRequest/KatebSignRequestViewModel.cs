

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class KatebSignRequestViewModel
    {
        public string SignRequestSubjectTitle { get; set; }
        public string SignRequestGetterTitle { get; set; }
        public string ReqNo { get; set; }
        public string ReqDate { get; set; }
        public string State { get; set; }
        public string SsarNo { get; set; }
        public string ScriptoriumId { get; set; }
        public string SignText { get; set; }
        public string IsCostPaid { get; set; }
        public string IsRemote { get; set; }
        public string RemoteRequestId { get; set; }
        public string IsReadyToPay { get; set; }
        public string PaymentLink { get; set; }
        public string ReceiptNo { get; set; }
        public string SumPrices { get; set; }
        public string SardaftarNationaNo { get; set; }
        public string SardaftarPost { get; set; }
        public KatebSignRequestPersonViewModel personViewModel { get; set; }
    }

    public class KatebSignRequestPersonViewModel
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalNo { get; set; }
        public string BirthDate { get; set; }
        public string MobileNo { get; set; }
        public string IsFingerprintGotten { get; set; }
    }
}
