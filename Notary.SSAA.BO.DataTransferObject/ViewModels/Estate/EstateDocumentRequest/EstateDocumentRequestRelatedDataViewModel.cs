using Notary.SSAA.BO.DataTransferObject.Bases;
using System.Text.Json.Serialization;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateDocumentRequest
{
   
    public class EstateDocumentRequestRelatedDataViewModel : EntityState
    {       
        public EstateDocumentRequestRelatedDataViewModel()
        {
            RequestPersons = new List<EstateDocumentRequestPersonViewModel>();           
        }              
        public string RequestEstateBasic{ get; set; }
        public bool RequestEstateBasicRemaining { get; set; }
        public bool RequestHasSecondary { get; set; }
        public string RequestEstateSecondary { get; set; }
        public bool RequestEstateSecondaryRemaining { get; set; }
        public IList<string> RequestEstateUnitId { get; set; }
        public IList<string> RequestEstateSectionId { get; set; }
        public IList<string> RequestEstateSubSectionId { get; set; }        
        public string RequestEstatePieceNo { get; set; }
        public string RequestEstateBlockNo { get; set; }
        public string RequestEstateFloorNo { get; set; }
        public string RequestEstatePostCode { get; set; }
        public string RequestEstateAddress { get; set; }                
        public string EstateTransferDocumentNo { get; set; }
        public string EstateTransferDocumentDate { get; set; }
        public string EstateTransferDocumentVerificationCode { get; set; }
        public IList<string> EstateTransferDocumentTypeId { get; set; }
        public IList<string> EstateTransferDocumentScriptoriumId { get; set; }
        public IList<string> EstateTransferDocumentId { get; set; }
        public List<EstateDocumentRequestPersonViewModel> RequestPersons { get; set; }
       

    }
  
    
}
