namespace Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint
{
    public class GetInquiryPersonFingerprintRepositoryObject
    {
        public string FingerprintId { get; set; }
        public string PersonObjectId { get; set; }
        public string MainObjectId { get; set; }
        public string PersonNationalNo { get; set; }
        public string PersonFingerTypeId { get; set; }
        public string PersonFingerTypeTitle { get; set; }
        public string TFAState { get; set; }
        public bool IsFingerprintGotten { get; set; }
        public string MOCState { get; set; }
        public bool IsDeleted { get; set; }
        public string FingerprintDateTime { get; set; }
        public string SabtAhvalFingerCode { get; set; }
    }
}
