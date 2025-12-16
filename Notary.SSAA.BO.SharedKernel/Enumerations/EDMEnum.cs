

using System.ComponentModel;

namespace Notary.SSAA.BO.SharedKernel.Enumerations
{
    public enum DocumentPartType
    {
        [Description("")]
        None = 0,
        [Description("مشخصات دفترخانه")]
        scriptorium = 1,
        [Description("مشخصات شخص اصیل")]
        primaryParty = 2,
        [Description("مشخصات تصاویر گواهی")]
        image = 3,
        [Description("نقش سند")]
        documentRole = 4
    }

    public enum DocumentTransacionType
    {
        [Description("draft")]
        draft = 0,
        [Description("issue")]
        issue = 1,
        [Description("revival")]
        revival = 2,
        [Description("revoke")]
        revoke = 3,
    }
    public enum EdmDocmentType
    {
        [Description("سند گواهی الکترونیک امضا")]
        SignDocument = 32,
    }

    public enum EdmCertificateType
    {
        Signertificate = 27,
    }
    public enum EdmUnitType
    {
        scriptorium,
    }
    public enum ContactMechanismType
    {
        None = 0,
        postalAddress = 1,
        telecommunication = 2,
    }
    public enum PostalAddressPartType
    {
        None = 0,
        postalCode = 1,
        addressText = 2,
    }
    public enum TeleComminucationType
    {
        None = 0,
        telephone = 1,
    }
    public enum PartyType
    {
        None = 0,
        person = 1,
        organization =2
    }

    public enum EdmSexType
    {
        [Description("female")]
        Female = 1,
        [Description("male")]
        Male = 2,
        [Description("")]
        None = 0
    }
    public enum OriginalSystem
    {
        systemCode = 100009,
    }
    public enum SignatureType
    {
        systemSignature = 1,
    }
    public enum ElectronicFormat
    {
        JSON = 1,
    }
    public enum Status
    {
        Draft = 1,
        Issue= 2,
    }
    public enum PartyRoleType
    {
        verifier
    }
    public enum TelecommunicationType
    {
        mobile,
        telephone
    }
}
