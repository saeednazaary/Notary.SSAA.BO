using Notary.SSAA.BO.DataTransferObject.Bases;
using System.Text.Json.Serialization;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateDocumentRequest
{
    public class EstateDocumentRequestExtraParam
    {
        public EstateDocumentRequestExtraParam()
        {
            
        }
        public bool CanEdit { get; set; }
        public bool CanCancel { get; set; }
        public bool CanConfirm { get; set; }
        public bool CanFinalPrint { get; set; }        
    }
    public class EstateDocumentRequestViewModel : EntityState
    {
        public EstateDocumentRequestExtraParam ExtraParams { get; set; }
        public EstateDocumentRequestViewModel()
        {
            RequestPersons = new List<EstateDocumentRequestPersonViewModel>();
            RequestRelatedPersons = new List<EstateDocumentRequestRelatedPersonViewModel>();
        }
        public string RequestId { get; set; }
        public string RequestStatus { get;set; }
        public string RequestStatusTitle { get; set; }
        public string RequestNo { get; set; }
        public string RequestCreateDate { get; set; }
        public string RequestCreateTime { get; set; }
        public IList<string> RequestTypeId { get; set; }
        public string RequestPostBarCode { get; set; }
        public string RequestDescription { get; set; }
        public string RequestEstateBasic{ get; set; }
        public bool RequestEstateBasicRemaining { get; set; }
        public bool RequestHasSecondary { get; set; }
        public string RequestEstateSecondary { get; set; }
        public bool RequestEstateSecondaryRemaining { get; set; }
        public IList<string> RequestEstateUnitId { get; set; }
        public IList<string> RequestEstateSectionId { get; set; }
        public IList<string> RequestEstateSubSectionId { get; set; }
        public IList<string> RequestCurrentDocumentTypeId { get; set; }

        public string RequestEstatePieceNo { get; set; }
        public string RequestEstateBlockNo { get; set; }
        public string RequestEstateFloorNo { get; set; }
        public string RequestEstatePostCode { get; set; }
        public string RequestEstateAddress { get; set; }
        public IList<string> RequestEstateTypeId { get; set; }
        public IList<string> RequestEstateOwnershipTypeId { get; set; }

        public bool EstateTransferDocumentIsInSsar { get; set; }

        public string EstateTransferDocumentNo { get; set; }
        public string EstateTransferDocumentDate { get; set; }
        public string EstateTransferDocumentVerificationCode { get; set; }

        public IList<string> EstateTransferDocumentTypeId { get; set; }
        public IList<string> EstateTransferDocumentScriptoriumId { get; set; }
        public IList<string> EstateTransferDocumentId { get; set; }
        public IList<string> RevokedRequestId { get; set; }
        public IList<string> DefectiveRequestId { get; set; }
        public List<EstateDocumentRequestPersonViewModel> RequestPersons { get; set; }
        public List<EstateDocumentRequestRelatedPersonViewModel> RequestRelatedPersons { get; set; }

        public decimal RequestTimeStamp { get; set; }
    }
    public class EstateDocumentRequestPersonViewModel : EstateBasePersonViewModel
    {
        public string PersonLastLegalPaperDate { get; set; }
        public string PersonLastLegalPaperNo { get; set; }
        public IList<string> PersonLegalPersonNatureId { get; set; }
        public IList<string> PersonLegalPersonTypeId { get; set; }
        public IList<string> PersonCompanyTypeId { get; set; }

        public IList<string> DocumentPersonId { get; set; }
    }
    public class EstateDocumentRequestRelatedPersonViewModel : EntityState
    {               
        public string Id { get; set; }
        public IList<string> MainPersonId { get; set; }        
        public IList<string> RelatedAgentPersonId { get; set; }
        public IList<string> RelatedAgentTypeId { get; set; }
        public bool IsRelatedAgentDocumentAbroad { get; set; }
        public IList<int> RelatedAgentDocumentCountryId { get; set; }
        public bool IsRelatedDocumentInSSAR { get; set; }
        public string RelatedAgentDocumentNo { get; set; }
        public string RelatedAgentDocumentDate { get; set; }
        public string RelatedAgentDocumentIssuer { get; set; }
        public string RelatedAgentDocumentSecretCode { get; set; }
        public IList<string> RelatedAgentDocumentScriptoriumId { get; set; }
        public bool IsRelatedPersonLawyer { get; set; }
        public IList<string> RelatedReliablePersonReasonId { get; set; }
        public string RelatedPersonDescription { get; set; }
        [JsonIgnore]
        public IList<string> ValidationMessages { get; set; }
        public string RelatedAgentTypeTitle { get; set; }

        public decimal TimeStamp { get; set; }
    }    
}
