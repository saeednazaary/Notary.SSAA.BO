using Notary.SSAA.BO.DataTransferObject.Bases;
using System.Text.Json.Serialization;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateDocumentRequest
{
   
    public class ConfirmValidationViewModel 
    {
        public bool Result { get; set; }
        public string ErrorMessage { get; set; }
    }
   
}
