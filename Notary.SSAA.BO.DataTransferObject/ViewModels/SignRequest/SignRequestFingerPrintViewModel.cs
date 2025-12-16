

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class SignRequestFingerPrintViewModel
    {
        public SignRequestFingerPrintViewModel() 
        {
            signRequestFingerItems = new List<SignRequestFingerItem>();
        }
        public IList<SignRequestFingerItem> signRequestFingerItems {  get; set; }
        public string CurrentDate { get; set; }
        public string ScriptoriumName { get; set; }
    }

    public class SignRequestFingerItem
    {
        public byte[] FingerImage { get; set; }
        public string IsFingerMatch { get; set; }
        public string FingerType { get; set; }
        public string FingerDate { get; set; }
        public string FingerTime { get; set; }
        public string FingerDeviceName { get; set; }
        public string PersonNameFinger { get; set; }
        public string PersonFamilyFinger { get; set; }
        public string PersonNatinalNoFinger { get; set; }
        public string PersonMobileNoFinger { get; set; }
        public string PersonPostalCodeFinger { get; set; }
        public string TFASendDate { get; set; }
        public string TFASendTime { get; set; }
        public string PersonPostFinger { get; set; }
        public string DocNo { get; set; }
        public string DocDate { get; set; }
        public string SignNationalNo { get; set; }
        public string ScriptoriumId { get; set; }
    }
}
