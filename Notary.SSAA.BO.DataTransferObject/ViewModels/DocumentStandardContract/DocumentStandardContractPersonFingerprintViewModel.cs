using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractPersonFingerprintViewModel : EntityState
    {
        public string PersonId { get; set; }
        public string MainObjectId { get; set; }
        public string PersonNationalNo { get; set; }
        public string PersonPost { get; set; }
        public string PersonBirthDate { get; set; }
        public string PersonName { get; set; }
        public bool IsPersonOriginal { get; set; }
        public string PersonFamily { get; set; }
        public string PersonFatherName { get; set; }
        public string PersonMobileNo { get; set; }
        public string PersonSexType { get; set; }
        public bool IsPersonAlive { get; set; }
        public bool? IsFingerprintGotten { get; set; }
        public bool? IsTFARequired { get; set; }
        public bool IsPersian { get; set; }
        public string TFAState { get; set; }
        public string MOCState { get; set; }
        public string PersonalImage { get; set; }
    }
}
