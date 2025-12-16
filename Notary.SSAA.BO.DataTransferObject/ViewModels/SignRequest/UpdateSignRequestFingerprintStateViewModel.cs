namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class UpdateSignRequestFingerprintStateViewModel
    {
        public UpdateSignRequestFingerprintStateViewModel()
        {
                SignRequestPersons=new List<CheckSignRequestFingerprintPersonStatusViewModel>();
        }
        public string SignRequestId { get; set; }
        public IList<CheckSignRequestFingerprintPersonStatusViewModel>  SignRequestPersons { get; set; }
    }

   public class CheckSignRequestFingerprintPersonStatusViewModel
    {
        public string PersonId { get; set; }
        public string TFAState { get; set; }
        public string MOCState { get; set; }
        public bool IsFingerprintGotten { get; set; }
    }

}
