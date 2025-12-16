using Notary.SSAA.BO.DataTransferObject.Bases;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractCostQuestionViewModel : EntityState
    {
        public List<string> validateIsEstateRegisterd(string documentTypeCode,string documentTypeTitle)
        {
            List<string> DocumentTypeCodesValid = new List<string>()
            {
                "321",
                "332",
                "910",
                "911",
                "912",
                "913",
                "914",
                "915",
                "916",
                "917",
                "921",
            };
            List<string> messages = new List<string>();
            bool isRequired = false;
            if ((IsEstateRegister != null) && !string.IsNullOrEmpty( documentTypeCode) && ( IsNew || IsDirty))
            {
                
              
                if (!DocumentTypeCodesValid.Contains(documentTypeCode) )
                {
                    messages.Add($"در سند {documentTypeTitle}  ");
                    return messages;
                }
            }

            return null;
        }
        public string DocumentId { get; set; }
        public bool? IsRequestCostCalculateConfirmed { get; set; }
        public bool? IsEstateRegister { get; set; }
      
    }
}
