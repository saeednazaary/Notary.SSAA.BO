namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class ValidatDocumentStandardContractViewModel
    {
        public ValidatDocumentStandardContractViewModel() 
        {
            ErrorMessages = new List<string>();
        }
        public bool IsValidat { get; set; }
        public IList<string> ErrorMessages { get; set; }
        public string DocumentId { get; set; }
    }
}
