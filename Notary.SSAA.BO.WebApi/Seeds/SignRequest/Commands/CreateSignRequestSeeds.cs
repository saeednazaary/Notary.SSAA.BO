using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Commands
{
    public static class CreateSignRequestSeeds
    {
        public static CreateSignRequestCommand StandardSignRequest { get; set; } = new()
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            SignRequestSignText = "Sample sign text",
            SignRequestGetterId = new List<string> { "01" },
            SignRequestSubjectId = new List<string> { "0101" },
            SignRequestPersons = new List<SignRequestPersonViewModel>
    {
        new SignRequestPersonViewModel
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            IsLoaded = true,
            RowNo = "1",
            PersonState = "1",
            IsPersonSabteAhvalChecked = true,
            IsPersonSabteAhvalCorrect = true,
            PersonId = "",
            SignRequestId = "",
            PersonClassifyNo = "001",
            PersonNationalNo = "2160074284",
            PersonPost = "",
            PersonBirthDate = "1381/10/10",
            PersonName = "نام-تست",
            PersonFamily = "نام خانوادگی-تست",
            PersonFatherName = "نام پدر - تست",
            PersonIdentityNo = "152",
            PersonIdentityIssueLocation = "تهران",
            PersonSeri = "123",
            PersonSerial = "456789",
            PersonPostalCode = "1234567890",
            PersonMobileNo = "09123456789",
            PersonAddress = "تهران تست",
            PersonTel = "02112345678",
            PersonEmail = "john.doe@example.com",
            PersonDescription = "First person",
            PersonSexType = "1",
            PersonNationalityId = new List<string> { "01" },
            IsPersonRelated = false,
            IsPersonSanaChecked = true,
            AmlakEskanState = true,
            PersonMobileNoState = true,
            IsPersonAlive = true,
            IsPersonOriginal = true,
            IsPersonIranian = true,
            IsFingerprintGotten = true,
            IsTFARequired = false,
            TFAState = "2",
            PersonalImage = "base64encodedimage1",
            PersonAlphabetSeri = "11"
        },
        new SignRequestPersonViewModel
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            IsLoaded = true,
            RowNo = "2",
            PersonState = "1",
            IsPersonSabteAhvalChecked = true,
            IsPersonSabteAhvalCorrect = true,
            PersonId = "",
            SignRequestId = "",
            PersonClassifyNo = "002",
            PersonNationalNo = "5847709943",
            PersonPost = "Developer",
            PersonBirthDate = "1382/09/09",
            PersonName = "نام-تست",
            PersonFamily = "نام خانوادگی-تست",
            PersonFatherName = "نام پدر - تست",
            PersonIdentityNo = "152",
            PersonIdentityIssueLocation = "تهران",
            PersonSeri = "456",
            PersonSerial = "123456",
            PersonPostalCode = "0987654321",
            PersonMobileNo = "09129876543",
            PersonAddress = "تهران اصفهان",
            PersonTel = "03198765432",
            PersonEmail = "jane.smith@example.com",
            PersonDescription = "Second person",
            PersonSexType = "2",
            PersonNationalityId = new List<string> { "01" },
            IsPersonRelated = false,
            IsPersonSanaChecked = true,
            AmlakEskanState = true,
            PersonMobileNoState = true,
            IsPersonAlive = true,
            IsPersonOriginal = true,
            IsPersonIranian = true,
            IsFingerprintGotten = true,
            IsTFARequired = true,
            TFAState = "2",
            PersonalImage = "base64encodedimage2",
            PersonAlphabetSeri = "10"
        }
    },
            
            IsRemoteRequest = false,
            RemoteRequestId = null
        };
        public static CreateSignRequestCommand NullSignRequest { get; set; } = new()
        {
            IsValid = false,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            SignRequestSignText = null,
            SignRequestGetterId = null,
            SignRequestSubjectId = null,
            SignRequestPersons = null,
            IsRemoteRequest = false,
            RemoteRequestId = null
        };

        public static CreateSignRequestCommand EmptySignRequest { get; set; } = new()
        {
            IsValid = false,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            SignRequestSignText = "",
            SignRequestGetterId = new List<string>(),
            SignRequestSubjectId = new List<string>(),
            SignRequestPersons = new List<SignRequestPersonViewModel>(),
            IsRemoteRequest = false,
            RemoteRequestId = null
        };
        public static CreateSignRequestCommand MaxLengthSignRequest { get; set; } = new()
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            SignRequestSignText = new string('A', 1000), // Assuming max length
            SignRequestGetterId = new List<string> { "01" },
            SignRequestSubjectId = new List<string> { "02" },
            SignRequestPersons = new List<SignRequestPersonViewModel>
    {
        new SignRequestPersonViewModel
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            IsLoaded = true,
            RowNo = "1",
            PersonState = "1",
            IsPersonSabteAhvalChecked = true,
            IsPersonSabteAhvalCorrect = true,
            PersonId = "",
            SignRequestId = "",
            PersonClassifyNo = "001",
            PersonNationalNo = new string('1', 20), // Max length national no
            PersonPost = new string('A', 100), // Max length post
            PersonBirthDate = "1381/10/10",
            PersonName = new string('ن', 50), // Max length name
            PersonFamily = new string('ف', 70), // Max length family
            PersonFatherName = new string('پ', 50), // Max length father name
            PersonIdentityNo = new string('1', 20), // Max length identity no
            PersonIdentityIssueLocation = new string('ت', 50), // Max length
            PersonSeri = new string('1', 10), // Max length seri
            PersonSerial = new string('1', 20), // Max length serial
            PersonPostalCode = new string('1', 15), // Max length postal code
            PersonMobileNo = new string('1', 15), // Max length mobile
            PersonAddress = new string('آ', 500), // Max length address
            PersonTel = new string('1', 20), // Max length tel
            PersonEmail = "very.long.email.address.that.exceeds.typical.limits@example.com",
            PersonDescription = new string('ت', 1000), // Max length description
            PersonSexType = "1",
            PersonNationalityId = new List<string> { "01" },
            IsPersonRelated = false,
            IsPersonSanaChecked = true,
            AmlakEskanState = true,
            PersonMobileNoState = true,
            IsPersonAlive = true,
            IsPersonOriginal = true,
            IsPersonIranian = true,
            IsFingerprintGotten = true,
            IsTFARequired = false,
            TFAState = "2",
            PersonalImage = "base64encodedimage1",
            PersonAlphabetSeri = "11"
        }
    },
            IsRemoteRequest = false,
            RemoteRequestId = null
        };
        public static CreateSignRequestCommand InvalidFormatSignRequest { get; set; } = new()
        {
            IsValid = false,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            SignRequestSignText = "Sample",
            SignRequestGetterId = new List<string> { "invalid" },
            SignRequestSubjectId = new List<string> { "invalid" },
            SignRequestPersons = new List<SignRequestPersonViewModel>
    {
        new SignRequestPersonViewModel
        {
            IsValid = false,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            IsLoaded = true,
            RowNo = "invalid",
            PersonState = "invalid",
            IsPersonSabteAhvalChecked = true,
            IsPersonSabteAhvalCorrect = true,
            PersonId = "",
            SignRequestId = "",
            PersonClassifyNo = "invalid",
            PersonNationalNo = "invalid_national", // Invalid format
            PersonPost = "",
            PersonBirthDate = "invalid_date", // Invalid date format
            PersonName = "Name123", // Invalid characters
            PersonFamily = "Family456", // Invalid characters
            PersonFatherName = "Father789", // Invalid characters
            PersonIdentityNo = "invalid",
            PersonIdentityIssueLocation = "Location123", // Invalid characters
            PersonSeri = "invalid",
            PersonSerial = "invalid",
            PersonPostalCode = "invalid_postal", // Invalid postal code
            PersonMobileNo = "invalid_mobile", // Invalid mobile format
            PersonAddress = "Address",
            PersonTel = "invalid_tel", // Invalid telephone
            PersonEmail = "invalid-email", // Invalid email format
            PersonDescription = "Description",
            PersonSexType = "invalid", // Invalid sex type
            PersonNationalityId = new List<string> { "invalid" },
            IsPersonRelated = false,
            IsPersonSanaChecked = true,
            AmlakEskanState = false,
            PersonMobileNoState = false,
            IsPersonAlive = false,
            IsPersonOriginal = false,
            IsPersonIranian = false,
            IsFingerprintGotten = false,
            IsTFARequired = false,
            TFAState = "invalid",
            PersonalImage = "invalid_base64",
            PersonAlphabetSeri = "invalid"
        }
    },
            IsRemoteRequest = false,
            RemoteRequestId = null
        };
        public static CreateSignRequestCommand EdgeCaseSignRequest { get; set; } = new()
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            SignRequestSignText = "   ", // Whitespace only
            SignRequestGetterId = new List<string> { "" }, // Empty string in list
            SignRequestSubjectId = new List<string> { "   " }, // Whitespace in list
            SignRequestPersons = new List<SignRequestPersonViewModel>
    {
        new SignRequestPersonViewModel
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            IsLoaded = true,
            RowNo = "999", // High row number
            PersonState = "9", // Edge state
            IsPersonSabteAhvalChecked = false,
            IsPersonSabteAhvalCorrect = false,
            PersonId = "   ", // Whitespace
            SignRequestId = "   ",
            PersonClassifyNo = "999", // Edge classify no
            PersonNationalNo = "0000000000", // All zeros
            PersonPost = "   ",
            PersonBirthDate = "1400/01/01", // Future date
            PersonName = "ا", // Single character
            PersonFamily = "ب",
            PersonFatherName = "پ",
            PersonIdentityNo = "000",
            PersonIdentityIssueLocation = "م",
            PersonSeri = "000",
            PersonSerial = "000000",
            PersonPostalCode = "0000000000",
            PersonMobileNo = "0900000000", // Edge mobile
            PersonAddress = "   ",
            PersonTel = "0210000000", // Edge telephone
            PersonEmail = "a@b.c", // Minimal email
            PersonDescription = "   ",
            PersonSexType = "3", // Edge sex type
            PersonNationalityId = new List<string> { "99" }, // Edge nationality
            IsPersonRelated = true, // Related person
            IsPersonSanaChecked = false,
            AmlakEskanState = false,
            PersonMobileNoState = false,
            IsPersonAlive = false,
            IsPersonOriginal = false,
            IsPersonIranian = false,
            IsFingerprintGotten = false,
            IsTFARequired = true,
            TFAState = "9", // Edge TFA state
            PersonalImage = "", // Empty image
            PersonAlphabetSeri = "99" // Edge alphabet seri
        }
    },
            IsRemoteRequest = true,
            RemoteRequestId = "remote_id_123"
        };
        public static CreateSignRequestCommand MinimumValidSignRequest { get; set; } = new()
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            SignRequestSignText = "Min",
            SignRequestGetterId = new List<string> { "01" },
            SignRequestSubjectId = new List<string> { "01" },
            SignRequestPersons = new List<SignRequestPersonViewModel>
    {
        new SignRequestPersonViewModel
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            IsLoaded = true,
            RowNo = "1",
            PersonState = "1",
            IsPersonSabteAhvalChecked = true,
            IsPersonSabteAhvalCorrect = true,
            PersonId = "",
            SignRequestId = "",
            PersonClassifyNo = "001",
            PersonNationalNo = "1234567890",
            PersonPost = "",
            PersonBirthDate = "1360/01/01",
            PersonName = "نام",
            PersonFamily = "خانوادگی",
            PersonFatherName = "پدر",
            PersonIdentityNo = "123",
            PersonIdentityIssueLocation = "تهران",
            PersonSeri = "123",
            PersonSerial = "123456",
            PersonPostalCode = "1234567890",
            PersonMobileNo = "09123456789",
            PersonAddress = "آدرس",
            PersonTel = "02112345678",
            PersonEmail = "test@example.com",
            PersonDescription = "",
            PersonSexType = "1",
            PersonNationalityId = new List<string> { "01" },
            IsPersonRelated = false,
            IsPersonSanaChecked = true,
            AmlakEskanState = true,
            PersonMobileNoState = true,
            IsPersonAlive = true,
            IsPersonOriginal = true,
            IsPersonIranian = true,
            IsFingerprintGotten = true,
            IsTFARequired = false,
            TFAState = "2",
            PersonalImage = "image",
            PersonAlphabetSeri = "01"
        }
    },
            IsRemoteRequest = false,
            RemoteRequestId = null
        };
        public static CreateSignRequestCommand MultiplePersonsSignRequest { get; set; } = new()
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            SignRequestSignText = "Multiple persons",
            SignRequestGetterId = new List<string> { "01", "02", "03" },
            SignRequestSubjectId = new List<string> { "01", "02" },
            SignRequestPersons = new List<SignRequestPersonViewModel>
    {
        // Person 1
        new SignRequestPersonViewModel
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            IsLoaded = true,
            RowNo = "1",
            PersonState = "1",
            IsPersonSabteAhvalChecked = true,
            IsPersonSabteAhvalCorrect = true,
            PersonId = "",
            SignRequestId = "",
            PersonClassifyNo = "001",
            PersonNationalNo = "2160074284",
            PersonPost = "Manager",
            PersonBirthDate = "1360/01/01",
            PersonName = "مدیر",
            PersonFamily = "سیستم",
            PersonFatherName = "پدر",
            PersonIdentityNo = "001",
            PersonIdentityIssueLocation = "تهران",
            PersonSeri = "A",
            PersonSerial = "001001",
            PersonPostalCode = "1234567890",
            PersonMobileNo = "09120000001",
            PersonAddress = "تهران",
            PersonTel = "02100000001",
            PersonEmail = "manager@example.com",
            PersonDescription = "مدیر سیستم",
            PersonSexType = "1",
            PersonNationalityId = new List<string> { "01" },
            IsPersonRelated = false,
            IsPersonSanaChecked = true,
            AmlakEskanState = true,
            PersonMobileNoState = true,
            IsPersonAlive = true,
            IsPersonOriginal = true,
            IsPersonIranian = true,
            IsFingerprintGotten = true,
            IsTFARequired = true,
            TFAState = "1",
            PersonalImage = "image1",
            PersonAlphabetSeri = "01"
        },
        // Person 2
        new SignRequestPersonViewModel
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            IsLoaded = true,
            RowNo = "2",
            PersonState = "2",
            IsPersonSabteAhvalChecked = false,
            IsPersonSabteAhvalCorrect = false,
            PersonId = "",
            SignRequestId = "",
            PersonClassifyNo = "002",
            PersonNationalNo = "5847709943",
            PersonPost = "Developer",
            PersonBirthDate = "1370/02/02",
            PersonName = "توسعه",
            PersonFamily = "دهنده",
            PersonFatherName = "پدر",
            PersonIdentityNo = "002",
            PersonIdentityIssueLocation = "اصفهان",
            PersonSeri = "B",
            PersonSerial = "002002",
            PersonPostalCode = "0987654321",
            PersonMobileNo = "09120000002",
            PersonAddress = "اصفهان",
            PersonTel = "03100000002",
            PersonEmail = "developer@example.com",
            PersonDescription = "توسعه دهنده",
            PersonSexType = "1",
            PersonNationalityId = new List<string> { "01", "02" },
            IsPersonRelated = true,
            IsPersonSanaChecked = false,
            AmlakEskanState = false,
            PersonMobileNoState = false,
            IsPersonAlive = true,
            IsPersonOriginal = false,
            IsPersonIranian = true,
            IsFingerprintGotten = false,
            IsTFARequired = false,
            TFAState = "2",
            PersonalImage = "image2",
            PersonAlphabetSeri = "02"
        },
        // Person 3
        new SignRequestPersonViewModel
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
            IsLoaded = true,
            RowNo = "3",
            PersonState = "3",
            IsPersonSabteAhvalChecked = true,
            IsPersonSabteAhvalCorrect = false,
            PersonId = "",
            SignRequestId = "",
            PersonClassifyNo = "003",
            PersonNationalNo = "1234567890",
            PersonPost = "Tester",
            PersonBirthDate = "1380/03/03",
            PersonName = "تست",
            PersonFamily = "کننده",
            PersonFatherName = "پدر",
            PersonIdentityNo = "003",
            PersonIdentityIssueLocation = "مشهد",
            PersonSeri = "C",
            PersonSerial = "003003",
            PersonPostalCode = "1122334455",
            PersonMobileNo = "09120000003",
            PersonAddress = "مشهد",
            PersonTel = "05100000003",
            PersonEmail = "tester@example.com",
            PersonDescription = "تست کننده",
            PersonSexType = "2",
            PersonNationalityId = new List<string> { "03" },
            IsPersonRelated = false,
            IsPersonSanaChecked = true,
            AmlakEskanState = true,
            PersonMobileNoState = true,
            IsPersonAlive = true,
            IsPersonOriginal = true,
            IsPersonIranian = false,
            IsFingerprintGotten = true,
            IsTFARequired = true,
            TFAState = "3",
            PersonalImage = "image3",
            PersonAlphabetSeri = "03"
        }
    },
            IsRemoteRequest = true,
            RemoteRequestId = "remote_multi_123"
        };
    }
}
