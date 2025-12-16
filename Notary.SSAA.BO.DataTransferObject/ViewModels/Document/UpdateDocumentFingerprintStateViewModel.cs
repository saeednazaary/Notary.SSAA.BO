namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class UpdateDocumentFingerprintStateViewModel
    {
        public UpdateDocumentFingerprintStateViewModel()
        {
                DocumentPersons=new List<CheckDocumentFingerprintPersonStatusViewModel>();
        }
        public string DocumentId { get; set; }
        public IList<CheckDocumentFingerprintPersonStatusViewModel>  DocumentPersons { get; set; }
    }

   public class CheckDocumentFingerprintPersonStatusViewModel
    {
        public string PersonId { get; set; }
        public string TFAState { get; set; }
        public string MOCState { get; set; }
        public bool IsFingerprintGotten { get; set; }
    }

}
