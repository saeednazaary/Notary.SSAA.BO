using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Notary.SSAA.BO.DataTransferObject.Bases;
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractPersonViewModel : EntityState
    {
        public string RowNo { get; set; }
        public bool? IsPersonSabteAhvalChecked { get; set; }
        public bool? IsPersonSabteAhvalCorrect { get; set; }
        public string PersonId { get; set; }
        public string PersonType { get; set; }
        public string DocumentId { get; set; }
        public string PersonNationalNo { get; set; }
        public string PersonPassportNo { get; set; }
        public string PersonBirthDate { get; set; }
        public string PersonBirthYear { get; set; }
        public string PersonName { get; set; }
        public string PersonFamily { get; set; }
        public string PersonFatherName { get; set; }
        public string PersonIdentityNo { get; set; }
        public string PersonIdentityIssueLocation { get; set; }
        public string PersonSeri { get; set; }
        public string PersonSerial { get; set; }
        public string PersonPostalCode { get; set; }
        public string PersonMobileNo { get; set; }
        public string PersonAddress { get; set; }
        public string PersonAddressType { get; set; }
        public bool? HasSmartCard { get; set; }
        public string PersonTel { get; set; }
        public string PersonFaxNo { get; set; }
        public string PersonEmail { get; set; }
        public string PersonDescription { get; set; }
        public string PersonSexType { get; set; }
        public string PersonPost { get; set; }
        public bool IsPersonRelated { get; set; }
        public bool? IsPersonSanaChecked { get; set; }
        public bool? PersonMobileNoState { get; set; }
        public bool? AmlakEskanState { get; set; }
        public bool? IsPersonAlive { get; set; }
        public bool IsPersonOriginal { get; set; }
        public bool IsPersonIranian { get; set; }
        public string PersonalImage { get; set; }
        public string PersonAlphabetSeri { get; set; }
        public string CompanyRegisterNo { get; set; }
        public string CompanyRegisterDate { get; set; }

        public string CompanyRegisterLocation { get; set; }
        public string LastLegalPaperNo { get; set; }
        public string LastLegalPaperDate { get; set; }

        public bool? IsMartyrApplicant { get; set; }
        public bool? MartyrIsIncluded { get; set; }
        public string MartyrInquiryDateTime { get; set; }
        public string MartyrCode { get; set; }
        public string MartyrDescription { get; set; }

        public bool? HasGrowthJudgment { get; set; }
        public string GrowthJudgmentNo { get; set; }
        public string GrowthJudgmentDate { get; set; }
        public string GrowthLetterNo { get; set; }
        public string GrowthLetterDate { get; set; }
        public string GrowthDescription { get; set; }
        public string EstateInquiryId { get; set; }

        public IList<string> PersonNationalityId { get; set; }
        public IList<string> PersonIdentityIssueGeoLocationId { get; set; }

        public IList<string> PersonLegalPersonNatureid { get; set; }
        public IList<string> PersonLegalPersonTypeId { get; set; }
        public IList<string> RequestPersonTypeId { get; set; }
        public IList<string> PersonCompanyTypeId { get; set; }
    }
}
