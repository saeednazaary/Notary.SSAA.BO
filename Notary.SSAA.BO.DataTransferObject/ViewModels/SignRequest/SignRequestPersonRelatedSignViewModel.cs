
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class SignRequestPersonRelatedSignViewModel
    {
        public string Id { get; set; }
        public string SignRequestId { get; set; }
        public string RowNo { get; set; }
        public string MainPersonId { get; set; }
        public string AgentPersonId { get; set; }
        public string AgentTypeId { get; set; }
        public string IsAgentDocumentAbroad { get; set; }
        public string AgentDocumentCountryId { get; set; }
        public string IsRelatedDocumentInSsar { get; set; }
        public string AgentDocumentNo { get; set; }
        public string AgentDocumentDate { get; set; }
        public string AgentDocumentIssuer { get; set; }
        public string AgentDocumentSecretCode { get; set; }
        public string AgentDocumentScriptoriumId { get; set; }
        public string AgentDocumentId { get; set; }
        public string IsLawyer { get; set; }
        public string ReliablePersonReasonId { get; set; }
        public string Description { get; set; }
        public string SignRequestScriptoriumId { get; set; }
    }
}
