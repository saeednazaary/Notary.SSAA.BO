using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class SignRequestPersonViewModel : EntityState
    {
        public string RowNo { get; set; }
        public string PersonState { get; set; }
        public bool? IsPersonSabteAhvalChecked { get; set; }
        public bool? IsPersonSabteAhvalCorrect { get; set; }
        public string PersonId { get; set; }
        public string SignRequestId { get; set; }
        public string PersonClassifyNo { get; set; }
        public string PersonNationalNo { get; set; }
        public string PersonPost { get; set; }
        public string PersonBirthDate { get; set; }
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
        public string PersonTel { get; set; }
        public string PersonEmail { get; set; }
        public string PersonDescription { get; set; }
        public string PersonSexType { get; set; }
        public IList<string> PersonNationalityId { get; set; }
        public bool IsPersonRelated { get; set; }
        public bool? IsPersonSanaChecked { get; set; }
        public bool? AmlakEskanState { get; set; }
        public bool? PersonMobileNoState { get; set; }
        public bool? IsPersonAlive { get; set; }
        public bool IsPersonOriginal { get; set; }
        public bool IsPersonIranian { get; set; }
        public bool? IsFingerprintGotten { get; set; }
        public bool? IsTFARequired { get; set; }
        public string TFAState { get; set; }
        public string PersonalImage { get; set; }
        public string PersonAlphabetSeri { get; set; }
    }
}
