namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class UpdateDocumentStandardContractFingerprintStateViewModel
    {
        public UpdateDocumentStandardContractFingerprintStateViewModel()
        {
                DocumentPersons=new List<CheckDocumentStandardContractFingerprintPersonStatusViewModel>();
        }
        public string DocumentId { get; set; }
        public IList<CheckDocumentStandardContractFingerprintPersonStatusViewModel>  DocumentPersons { get; set; }
    }

   public class CheckDocumentStandardContractFingerprintPersonStatusViewModel
    {
        public string PersonId { get; set; }
        public string TFAState { get; set; }
        public string MOCState { get; set; }
        public bool IsFingerprintGotten { get; set; }
    }

}
