

namespace SNotary.SSAA.BO.DataTransferObject.ServiceInputs.Edm
{
    public class SignRequestEdmInputService
    {
        public SignRequestEdmInputService() 
        {
            document = new();
        }
        public DOCUMENT document { get; set; }
    }
    public class DOCUMENT
    {
        public string documentLifeCycleStatus { get; set; }
        public string documentType { get; set; }
        public string authenticationCode { get; set; }
        public string documentCode { get; set; }
        public string description { get; set; }
        public List<Certificate> certificate { get; set; }
        public List<DocumentLifeCycle> documentLifeCycle { get; set; }
        public List<DocumentPart> documentPart { get; set; }
    }

    public class Certificate
    {
        public string certificateType { get; set; }
        public string certificateSubjectCode { get; set; }
    }

    public class DocumentLifeCycle
    {
        public string userTitle { get; set; }
        public string documentTransactionType { get; set; }
        public string transactionReference { get; set; }
        public SimpleDate transactionDate { get; set; }
    }

    public class SimpleDate
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
    }

    public class DocumentPart
    {
        public string documentPartType { get; set; }
        public List<DocumentApplication> documentApplication { get; set; }
    }

    public class DocumentApplication
    {
        public string title { get; set; }
        public string documentApplicationType { get; set; }
        public OrganizationUnit organizationUnit { get; set; }
        public List<Party> party { get; set; }
        public DocumentPic documentPic { get; set; }
    }

    public class OrganizationUnit
    {
        public string unitTitle { get; set; }
        public string unitId { get; set; }
        public string unitName { get; set; }
        public string unitType { get; set; }
        public string unitCode { get; set; }
        public List<ContactMechanismApplication> contactMechanismApplication { get; set; }
        public List<GeographyBoundary> geographyBoundry { get; set; }
    }

    public class GeographyBoundary
    {
        public string GBType { get; set; }
        public string GBId { get; set; }
        public string GBCompleteName { get; set; }
    }

    public class ContactMechanismApplication
    {
        public ContactMechanismApplication()
        {
            contactMechanism = new();
        }
        public string contactMechanismApplicationType { get; set; }
        public ContactMechanism contactMechanism { get; set; }
    }

    public class ContactMechanism
    {
        public PostalAddress postalAddress { get; set; }
        public List<TelecomminucationNumber> telecommunicationNumber { get; set; }
    }

    public class PostalAddress
    {
        public List<PostalAddressPart> postalAddressPart { get; set; }
    }

    public class PostalAddressPart
    {
        public string postalAddressPartValue { get; set; }
        public string postalAddressPartType { get; set; }
    }
    public class TelecomminucationNumber
    {
        public string telecommunicationType { get; set; }
        public string phoneNumber { get; set; }
    }

    public class Party
    {
        public string identityId { get; set; }
        public string partyType { get; set; }
        public string partyId { get; set; }
        public string name { get; set; }
        public IList<PartyRole> partyRole { get; set; }
        public Person person { get; set; }
        public List<ContactMechanismApplication> contactMechanismApplication { get; set; }
        public ImageCollection fingerprintPic { get; set; }
    }

    public class PartyRole
    {
        public string partyRoleType { get; set; }
    }

    public class Person
    {
        public string familyName { get; set; }
        public string fatherName { get; set; }
        public string gender { get; set; }
        public SimpleDate dateOfBirth { get; set; }
    }
    public class DocumentPic
    {
        public ImageCollection signCertificatePic { get; set; }
    }

    public class ImageCollection
    {
        public ImageCollection()
        {
            image = new();
        }
        public List<ImageFile> image { get; set; }
    }

    public class ImageFile
    {
        public string file { get; set; }
    }

}
