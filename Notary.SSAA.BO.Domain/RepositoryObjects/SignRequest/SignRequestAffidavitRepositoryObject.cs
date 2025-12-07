

namespace Notary.SSAA.BO.Domain.RepositoryObjects.SignRequest
{
    public class SignRequestAffidavitRepositoryObject
    {
        public string SignRequestId { get; set; }
        public string SignRequestNo { get; set; }
        public string PersonId { get; set; }
        public byte[] ScanFile { get; set; }
        // Core request metadata
        public string NationalRegisterNo { get; set; }
        public string DocDate { get; set; }
        public string SignGetterTitle { get; set; }
        public string SubjectTypeTitle { get; set; }

        // Person (main signer)
        public string NationalNo { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Title { get; set; }
        public string MobileNo { get; set; }
        public string Tel { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }

        // Movakel (proxy / representative)
        public string MovakelName { get; set; }
        public string MovakelFamily { get; set; }
        public string MovakelNationalNo { get; set; }
        public string MovakelMobileNo { get; set; }
        public string MovakelTelNo { get; set; }
        public string MovakelPostalCode { get; set; }
        public string MovakelAddress { get; set; }

        // Additional metadata
        public string ScriptoriumId { get; set; }
    }
}
