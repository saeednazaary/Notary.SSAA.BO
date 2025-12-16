
namespace Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint
{
    public class GetPersonFingerprintImageRepositoryObject
    {
        public string FingerprintId { get; set; }
        public string PersonObjectId { get; set; }
        public string MainObjectId { get; set; }
        public string PersonNationalNo { get; set; }
        public string PersonFingerTypeId { get; set; }
        public byte[] FingerPrintImage { get; set; }
    }
    public class PersonFingerprintHash
    {
        public string PersonObjectId { get; set; }
        public string Description { get; set; }
       
        public byte [ ] FingerPrintImage { get; set; }
    }
}
