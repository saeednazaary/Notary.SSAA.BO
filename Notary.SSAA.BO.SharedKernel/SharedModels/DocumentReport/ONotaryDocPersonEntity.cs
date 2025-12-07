

namespace Notary.SSAA.BO.SharedKernel.SharedModels.DocumentReport
{
    public class ONotaryDocPersonEntity
    {
        public string LegalPersonBaseTypeTitle { get; set; }
        public string Address { get; set; }
        public string CompanyTypeTile { get; set; }
        public string BirthDate { get; set; }
        public string CompanyRegisterDate { get; set; }
        public string CompanyRegisterLocation { get; set; }
        public string CompanyRegisterNo { get; set; }
        public string Family { get; set; }
        public string FatherName { get; set; }
        public string FullName { get; set; }
        public string Id { get; set; }
        public string IdentityIssueLocation { get; set; }
        public string IdentityIssueLocationTitle { get; set; }
        public string IdentityNo { get; set; }
        public int? IsOriginalPerson { get; set; }
        public int? IsIranian { get; set; }
        public string IsOriginalPersonDesc { get; set; }
        public string LastLegalPaperDate { get; set; }
        public string LastLegalPaperNo { get; set; }
        public int? LegalPersonType { get; set; }
        public string LegalPersonTypeDesc { get; set; }
        public string Name { get; set; }
        public string NameAndPost { get; set; }
        public string NationalNo { get; set; }
        public string NationalNoLabel { get; set; }
        public string Nationality { get; set; }
        public string ONotaryRegisterServiceReqId { get; set; }
        public string ONotaryRegisterServiceReqTitle { get; set; }
        public string ONotaryRegServicePersonTypeId { get; set; }
        public string ONotaryRegServicePersonTypeTitle { get; set; }
        public int? PersonType { get; set; }
        public string PersonTypeId { get; set; }
        public string PersonTypeDesc { get; set; }
        public string PostalCode { get; set; }
        public int? SexType { get; set; }
        public string SexTypeDesc { get; set; }
        public string TelNo { get; set; }
        public string CaseQouta { get; set; }
        public string Description { get; set; }
        public string PrintableSpec { get; set; }
        public decimal? RowNo { get; set; }
        public byte[] SignImage { get; set; }
        public byte[] FingerprintImage { get; set; }
        public decimal DocPersonCount { get; set; }
        public List<ONotaryDocPersonEntity> Agents { get; set; }
        public string DocAgentInfo { get; set; }
        public string RelatedPersonInfo { get; set; }
        /// <summary>
        /// This Property Says which object this person is being derived from, in order to decide whether it is repeated or not
        /// </summary>
        public string ParentPersonId { get; set; }
        public int IsUsed { get; set; }
        public int IsLastNodeInRelationsGraph { get; set; }
        public int RelationDepth { get; set; }
        public string MokamelAgentStatement { get; set; }
        public string PrintPluralTitle {  get; set; }
        public string PrintSingularTitle { get; set; }
        public string AgentTypeCode { get; set; }
        public string AgentTypeRoot{ get; set; }
        public string ReliablePersonReasonCode { get; set; }
        public string RelatedAgentDocumentNo { get; set; }
        public string RelatedAgentDocumentDate { get;set; }
        public string RelatedAgentDocumentIssuer { get; set; }
        public string IsOwner { get; set; }


    }
}
