using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.InquiryFromUnit
{
    public sealed class InquiryFromUnitPersonViewModel : EntityState
    {
        public string RowNo { get; set; }
        public bool IsPersonSabteAhvalChecked { get; set; }
        public bool IsPersonSabteAhvalCorrect { get; set; }
        public string PersonId { get; set; }
        public string InquiryFromUnitId { get; set; }
        public string PersonNationalNo { get; set; }
        public string PersonBirthDate { get; set; }
        public string PersonName { get; set; }
        public string PersonFamily { get; set; }
        public string PersonalImage { get; set; }
        public string PersonFatherName { get; set; }
        public string PersonIdentityNo { get; set; }
        public string PersonType { get; set; }
        public string PersonIdentityIssueLocation { get; set; }
        public string PersonSeri { get; set; }
        public string PersonSerial { get; set; }
        public string PersonPostalCode { get; set; }
        public string PersonMobileNo { get; set; }
        public string PersonAddress { get; set; }
        public string PersonTel { get; set; }
        public string PersonEmail { get; set; }
        public string PersonDescription { get; set; }
        public string PersonSexType { get; set; }
        public IList<string> PersonNationalityId { get; set; }
        public bool IsPersonRelated { get; set; }
        public bool IsPersonAlive { get; set; }
        public bool IsPersonOriginal { get; set; }
        public bool IsPersonIranian { get; set; }
        public string PersonAlphabetSeri { get; set; }
        public IList<string> LegalPersonTypeId { get; set; }
        public string CompanyRegisterNo { get; set; }
        public string CompanyRegisterDate { get; set; }
    }
}
