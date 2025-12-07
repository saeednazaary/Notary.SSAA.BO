using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ElzamArtSixPerson
{
    public class ElzamArtSixPersonViewModel : EntityState
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string FatherName { get; set; }
        public string IdentityNo { get; set; }
        public string BirthDate { get; set; }
        public string NationalityCode { get; set; }
        public string AlphabetSeri { get; set; }
        public string Seri { get; set; }
        public string SerialNo { get; set; }
        public string SharePart { get; set; }
        public string ShareText { get; set; }
        public string ShareTotal { get; set; }
        public string IssuePlaceText { get; set; }
        public string ForiegnBirthPlace { get; set; }
        public string ForiegnIssuePlace { get; set; }
        public string SexType { get; set; }
        public string PersonType { get; set; }
        public string RelationType { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string ExecutiveTransfer { get; set; }
        public List<string> IssuePlaceId { get; set; }
        public List<string> NationalityId { get; set; }
        public string ScriptoriumId { get; set; }
        public List<string> BirthPlaceId { get; set; }
        public List<string> CityId { get; set; }
        public List<string> SsrArticleInqId { get; set; }
        public long? Timestamp { get; set; }
        public string HasSmartCard { get; set; }
        public string MocState { get; set; }
        public bool? IsSanaChecked { get; set; }
        public bool? TfaRequired { get; set; }
        public string TfaState { get; set; }
        public bool? IsIranian { get; set; }
        public bool? IsSabteAhvalChecked { get; set; }
        public bool? IsSabtahvalCorrect { get; set; }
        public bool? IsIlencChecked { get; set; }
        public string IsIlencCorrect { get; set; }
        public string IsForeignerssysChecked { get; set; }
        public string IsForeignerssysCorrect { get; set; }
        public string Ilm { get; set; }
        public string MobileNo { get; set; }
        public bool? MobileNoState { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public bool? IsAlive { get; set; }
        public bool? IsOriginal { get; set; }
        public bool? IsRelated { get; set; }
        public long? SanaHasOrganizationChart { get; set; }
        public string SanaInquiryDate { get; set; }
        public string SanaInquiryTime { get; set; }
        public string SanaMobileNo { get; set; }
        public string SanaOrganizationCode { get; set; }
        public string SanaOrganizationName { get; set; }
        public string Description { get; set; }
        public string LastLegalPaperDate { get; set; }
        public string LastLegalPaperNo { get; set; }
        public List<string> LegalpersonNatureId { get; set; }
        public List<string> LegalpersonTypeId { get; set; }
        public List<string> CompanyTypeId { get; set; }
        public long? OwnershipDocumentType { get; set; }
        public string PersonalImage { get; set; }
        public string ManagerNationalNo { get; set; }
        public string ManagerName { get; set; }
        public string ManagerFamily { get; set; }
        public string LawyerNationalId { get; set; }
        public string LawyerName { get; set; }
        public string LawyerMobile { get; set; }
        public string LawyerPostalCode { get; set; }
        public string LawyerBirthDate { get; set; }
        public string LawyerFatherName { get; set; }
        public bool? HasLawyer { get; set; }
    }
}