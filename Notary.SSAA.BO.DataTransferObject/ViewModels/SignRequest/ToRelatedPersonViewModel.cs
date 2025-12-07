using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class ToRelatedPersonViewModel : EntityState
    {
        public ToRelatedPersonViewModel()
        {

        }
        public string RowNo { get; set; }
        public string RelatedPersonId { get; set; }
        public string RelatedState { get; set; }
        public string SignRequestId { get; set; }
        public IList<string> MainPersonId { get; set; }
        public IList<string> RelatedAgentPersonId { get; set; }
        public IList<string> RelatedAgentTypeId { get; set; }
        //public IList<string> RelatedAgentDocumentCountryId { get; set; }
        //public bool IsRelatedAgentDocumentAbroad { get; set; }
        //public bool IsRelatedDocumentInSSAR { get; set; }
        public string RelatedAgentDocumentNo { get; set; }
        public string RelatedAgentDocumentDate { get; set; }
        public string RelatedAgentDocumentIssuer { get; set; }
        //public string RelatedAgentDocumentSecretCode { get; set; }
        //public IList<string> RelatedAgentDocumentScriptoriumId { get; set; }
        //public bool IsRelatedPersonLawyer { get; set; }
        public IList<string> RelatedReliablePersonReasonId { get; set; }
        public string RelatedPersonDescription { get; set; }
        public string RelatedAgentTypeTitle { get; set; }
    }
}
