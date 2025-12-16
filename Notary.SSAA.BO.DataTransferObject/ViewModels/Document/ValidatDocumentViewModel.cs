namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class ValidatDocumentViewModel
    {
        public ValidatDocumentViewModel() 
        {
            ErrorMessages = new List<string>();
        }
        public bool IsValidat { get; set; }
        public IList<string> ErrorMessages { get; set; }
        public string DocumentId { get; set; }
    }
}
