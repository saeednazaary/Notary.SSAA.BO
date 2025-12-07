namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class SignRequestElectronicBookPageViewModel
    {
        public SignRequestElectronicBookPageViewModel()
        {
            SignRequestElectronicBookPagePersons = new List<SignRequestElectronicPersonViewModel>();
        }

        public string SignRequestId { get; set; }
        public string SignRequestReqNo { get; set; }
        // شناسه یکتا گواهی امضا
        public string SignRequestNationalNo { get; set; }
        //تاریخ گواهی امضا
        public string SignRequestSignDate { get; set; }
        //موضوع گواهی امضا
        public string SignRequestSubjectTitle { get; set; }
        //مخاطب
        public string SignRequestGetterTitle { get; set; }
        //رمز تصدیق
        public string SignRequestSecretCode { get; set; }
        //آخرین تصویر
        public string SignRequestImageFile { get; set; }
        public decimal PageNumber { get; set; }

        public IList<SignRequestElectronicPersonViewModel> SignRequestElectronicBookPagePersons { get; set; }
    }
 
}
