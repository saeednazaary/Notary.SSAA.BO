using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Domain.Entities;
using SignElectronicBook = Notary.SSAA.BO.Domain.Entities.SignElectronicBook;

namespace Notary.SSAA.BO.FunctionalTests.Seeds.SignRequestSeedData
{
    public static class SignRequestSeedData
    {
        public static CreateSignRequestCommand CreateSignRequestCommand { get; set; } = new()
        {
            IsValid = true,
            IsNew = true,
            IsDelete = false,
            IsDirty = false,
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
        public static UpdateSignRequestCommand UpdateSignRequestCommand { get; set; } = new UpdateSignRequestCommand
        {
            IsValid = true,
            IsNew = false,
            IsDirty = false,
            IsDelete = false,
            SignRequestSignText = "امضا انجام شد",
            SignRequestGetterId = new List<string> { "11" },
            SignRequestSubjectId = new List<string> { "0102" },
            SignRequestId = "0a190675-b674-46d3-8ad6-33736ff33bdc",
            SignRequestPersons = new List<SignRequestPersonViewModel>
 {
     new SignRequestPersonViewModel
     {
         IsValid = true,
         IsNew = true,
         IsDirty = false,
         IsDelete = false,
         IsLoaded = true,
         RowNo = "1",
         PersonState = "1",
         IsPersonSabteAhvalChecked = true,
         IsPersonSabteAhvalCorrect = true,
         PersonId = "F0A58C6E-099D-4D6C-AA68-818DF826842A",
         PersonClassifyNo = "72876",
         PersonNationalNo = "2234567890",
         PersonName = "رضا",
         PersonFamily = "رحمانی",
         PersonBirthDate = "1361/01/01",
         PersonFatherName = "محمد",
         PersonIdentityNo = "1000002",
         PersonIdentityIssueLocation = "شیراز",
         PersonSeri = "11",
         PersonSerial = "500002",
         PersonPostalCode = "2234567890",
         PersonMobileNo = "09123456783",
         PersonAddress = "آدرس نمونه در شهر شیراز",
         PersonSexType = "2",
         PersonNationalityId = new List<string> { "1" },
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
         PersonAlphabetSeri = "1",
         SignRequestId = "0a190675-b674-46d3-8ad6-33736ff33bdc",

     },
     new SignRequestPersonViewModel
     {
         IsValid = true,
         IsNew = false,
         IsDirty = false,
         IsDelete = false,
         IsLoaded = true,
         RowNo = "2",
         PersonState = "1",
         IsPersonSabteAhvalChecked = true,
         IsPersonSabteAhvalCorrect = true,
         PersonId = "4191a952-36a7-4880-bc17-07c72037a239",
         PersonClassifyNo = "72877",
         PersonNationalNo = "2234567891",
         PersonName = "نرگس",
         PersonFamily = "موسوی",
         PersonBirthDate = "1363/01/01",
         PersonFatherName = "حسین",
         PersonIdentityNo = "2000002",
         PersonIdentityIssueLocation = "تبریز",
         PersonSeri = "21",
         PersonSerial = "500003",
         PersonPostalCode = "2234567891",
         PersonMobileNo = "09123456784",
         PersonAddress = "آدرس نمونه در شهر تبریز",
         PersonSexType = "1",
         PersonNationalityId = new List<string> { "1" },
         IsPersonRelated = true,
         IsPersonSanaChecked = true,
         AmlakEskanState = true,
         PersonMobileNoState = true,
         IsPersonAlive = true,
         IsPersonOriginal = false,
         IsPersonIranian = true,
         IsFingerprintGotten = true,
         IsTFARequired = true,
         TFAState = "1",
         PersonAlphabetSeri = "2",
         SignRequestId = "0a190675-b674-46d3-8ad6-33736ff33bdc",

     }
 },
            SignRequestRelatedPersons = new List<ToRelatedPersonViewModel>
 {
     new ToRelatedPersonViewModel
     {
         IsValid = true,
         IsNew = true,
         IsDirty = false,
         IsDelete = false,
         IsLoaded = true,
         RowNo = "1",
         RelatedPersonId ="",
         RelatedState = "1",
         MainPersonId = new List<string> { "F0A58C6E-099D-4D6C-AA68-818DF826842A" },
         RelatedAgentPersonId = new List<string> { "4191a952-36a7-4880-bc17-07c72037a239" },
         RelatedAgentTypeId = new List<string> { "01" },
         RelatedAgentDocumentNo = "2234",
         RelatedAgentDocumentDate = "1404/01/02",
         RelatedAgentDocumentIssuer = "شیراز",
         RelatedReliablePersonReasonId = new List<string> { "1" },
         RelatedPersonDescription = "رابطه وکالت تنظیم شده",
         RelatedAgentTypeTitle = "وکیل",
         SignRequestId = "0a190675-b674-46d3-8ad6-33736ff33bdc",

     }
 }
        };
        public static List<Domain.Entities.SignRequest> GetSignRequests()
        {
            var requests = new List<Domain.Entities.SignRequest>();

            // --------- REQUEST 1 ----------
            var sr1Id = Guid.Parse("62f720b6-c03b-4dfd-908e-cff35e8c6bed");
            var sr1PersonIds = new List<Guid> { Guid.Parse("e731dbde-b900-4bcc-ae6d-3cfff332aa30"), Guid.Parse("870ec471-7e8e-4d50-a53e-b9855471750f"), Guid.Parse("f98ddfac-81fc-431a-b9ae-8f1e8be58e40") };
            var sr1 = new Domain.Entities.SignRequest
            {
                Id = sr1Id,
                ReqNo = "140444457999000001",
                ReqDate = "1404/01/01",
                ReqTime = "10:00:00",
                ScriptoriumId = "57999",
                SignRequestGetterId = "01",
                SignRequestSubjectId = "0101",
                IsCostPaid = "1",
                SumPrices = 35000,
                ReceiptNo = "1234567891",
                PayCostDate = "1404/01/01",
                PayCostTime = "10:00:00",
                PaymentType = "PCPOS",
                BillNo = "1404010112345671",
                NationalNo = "140402157999000001",
                SecretCode = "123456",
                SignDate = "1404/01/01",
                SignTime = "10:00:00",
                Modifier = "زهرا رضایی",
                ModifyDate = "1404/01/01",
                ModifyTime = "10:00:00",
                Confirmer = "فاطمه محمدی",
                ConfirmDate = "1404/01/01",
                ConfirmTime = "10:00:00",
                DigitalSign = "StaticBase64String",
                SignCertificateDn = "CN=SampleCertificate-1234,O=Notary,OU=SSAA",
                IsRemoteRequest = "1",
                State = "2",
                Ilm = "1",
                RecordDate = DateTime.MaxValue,
                IsReadyToPay = "1",
                LegacyId = $"LEGACY_{sr1Id.ToString().Substring(0, 8).ToUpper()}",
                SignText = "امضا انجام شد",
                SignRequestPeople = new List<SignRequestPerson>
            {
                new SignRequestPerson
                {
                    Id = sr1PersonIds[0],
                    SignRequestId = sr1Id,
                    RowNo = 1,
                    SignClassifyNo = 72875,
                    IsOriginal = "1",
                    IsRelated = "2",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "1234567890",
                    Name = "محمد",
                    Family = "محمدی",
                    BirthDate = "1360/01/01",
                    SexType = "2",
                    FatherName = "حسن",
                    IdentityIssueLocation = "تهران",
                    IdentityNo = "1000001",
                    SeriAlpha = "1",
                    Seri = "10",
                    Serial = "500001",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456780",
                    PostalCode = "1234567890",
                    Address = "آدرس نمونه در شهر تهران",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب اصلی"
                },
                new SignRequestPerson
                {
                    Id = sr1PersonIds[1],
                    SignRequestId = sr1Id,
                    RowNo = 2,
                    SignClassifyNo = 72876,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "1234567891",
                    Name = "زهرا",
                    Family = "رضایی",
                    BirthDate = "1362/01/01",
                    SexType = "1",
                    FatherName = "علی",
                    IdentityIssueLocation = "مشهد",
                    IdentityNo = "2000001",
                    SeriAlpha = "2",
                    Seri = "20",
                    Serial = "500002",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456781",
                    PostalCode = "1234567891",
                    Address = "آدرس نمونه در شهر مشهد",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                },
                new SignRequestPerson
                {
                    Id = sr1PersonIds[2],
                    SignRequestId = sr1Id,
                    RowNo = 3,
                    SignClassifyNo = 72877,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "1234567892",
                    Name = "مریم",
                    Family = "جعفری",
                    BirthDate = "1365/01/01",
                    SexType = "1",
                    FatherName = "رضا",
                    IdentityIssueLocation = "اصفهان",
                    IdentityNo = "3000001",
                    SeriAlpha = "2",
                    Seri = "30",
                    Serial = "500003",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456782",
                    PostalCode = "1234567892",
                    Address = "آدرس نمونه در شهر اصفهان",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                }
            },
                SignRequestPersonRelateds = new List<SignRequestPersonRelated>
            {
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("6fc3ac49-5dc3-4dc2-8cd7-39e1a48a7dbd"),
                    SignRequestId = sr1Id,
                    RowNo = 1,
                    MainPersonId = sr1PersonIds[0],
                    AgentPersonId = sr1PersonIds[1],
                    AgentTypeId = "1",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "1234",
                    AgentDocumentDate = "1404/01/01",
                    AgentDocumentIssuer = "تهران",
                    SignRequestScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                }
            },
                SignElectronicBooks = new List<SignElectronicBook>
            {
                new SignElectronicBook
                {
                    Id = Guid.Parse("0824ba77-86a2-4380-9450-e2ecb182710d"),
                    ScriptoriumId = "57999",
                    ClassifyNo = 72875,
                    SignRequestId = sr1Id,
                    SignRequestNationalNo = "1234567890",
                    SignRequestPersonId = sr1PersonIds[0],
                    SignDate = "1404/01/01",
                    HashOfFingerprint = "StaticBase64String44",
                    HashOfFile = "StaticBase64String44",
                    DigitalSign = "StaticBase64String200",
                    ConfirmDate = "1404/01/01",
                    ConfirmTime = "10:00:00",
                    RecordDate = DateTime.MaxValue,
                    Ilm = "1",
                    SignCertificateDn = "CN=SampleCertificate-1234,O=Notary,OU=SSAA"
                }
            }
            };
            requests.Add(sr1);

            // --------- REQUEST 2 ----------
            var sr2Id = Guid.Parse("0a190675-b674-46d3-8ad6-33736ff33bdc");
            var sr2PersonIds = new List<Guid> { Guid.Parse("6f7c40c4-6b54-49e8-8d8e-742dd205924a"), Guid.Parse("4191a952-36a7-4880-bc17-07c72037a239"), Guid.Parse("22dfd2cb-762b-490b-b28a-2bbc4cb2a52a") };
            var sr2 = new Domain.Entities.SignRequest
            {
                Id = sr2Id,
                ReqNo = "140444457999000002",
                ReqDate = "1404/01/02",
                ReqTime = "11:00:00",
                ScriptoriumId = "57999",
                SignRequestGetterId = "02",
                SignRequestSubjectId = "0101",
                IsCostPaid = "1",
                SumPrices = 36000,
                ReceiptNo = "1234567892",
                PayCostDate = "1404/01/02",
                PayCostTime = "11:00:00",
                PaymentType = "PCPOS",
                BillNo = "1404010112345672",
                NationalNo = "140402157999000002",
                SecretCode = "234567",
                SignDate = "1404/01/02",
                SignTime = "11:00:00",
                Modifier = "سارا حسینی",
                ModifyDate = "1404/01/02",
                ModifyTime = "11:00:00",
                Confirmer = "مینا کریمی",
                ConfirmDate = "1404/01/02",
                ConfirmTime = "11:00:00",
                DigitalSign = "StaticBase64String2",
                SignCertificateDn = "CN=SampleCertificate-1235,O=Notary,OU=SSAA",
                IsRemoteRequest = "1",
                State = "2",
                Ilm = "1",
                RecordDate = DateTime.MaxValue,
                IsReadyToPay = "1",
                LegacyId = $"LEGACY_{sr2Id.ToString().Substring(0, 8).ToUpper()}",
                SignText = "امضا انجام شد",
                SignRequestPeople = new List<SignRequestPerson>
            {
                new SignRequestPerson
                {
                    Id = sr2PersonIds[0],
                    SignRequestId = sr2Id,
                    RowNo = 1,
                    SignClassifyNo = 72876,
                    IsOriginal = "1",
                    IsRelated = "2",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "2234567890",
                    Name = "رضا",
                    Family = "رحمانی",
                    BirthDate = "1361/01/01",
                    SexType = "2",
                    FatherName = "محمد",
                    IdentityIssueLocation = "شیراز",
                    IdentityNo = "1000002",
                    SeriAlpha = "1",
                    Seri = "11",
                    Serial = "500002",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456783",
                    PostalCode = "2234567890",
                    Address = "آدرس نمونه در شهر شیراز",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب اصلی"
                },
                new SignRequestPerson
                {
                    Id = sr2PersonIds[1],
                    SignRequestId = sr2Id,
                    RowNo = 2,
                    SignClassifyNo = 72877,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "2234567891",
                    Name = "نرگس",
                    Family = "موسوی",
                    BirthDate = "1363/01/01",
                    SexType = "1",
                    FatherName = "حسین",
                    IdentityIssueLocation = "تبریز",
                    IdentityNo = "2000002",
                    SeriAlpha = "2",
                    Seri = "21",
                    Serial = "500003",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456784",
                    PostalCode = "2234567891",
                    Address = "آدرس نمونه در شهر تبریز",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                },
                new SignRequestPerson
                {
                    Id = sr2PersonIds[2],
                    SignRequestId = sr2Id,
                    RowNo = 3,
                    SignClassifyNo = 72878,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "2234567892",
                    Name = "لیلا",
                    Family = "قاسمی",
                    BirthDate = "1366/01/01",
                    SexType = "1",
                    FatherName = "احمد",
                    IdentityIssueLocation = "رشت",
                    IdentityNo = "3000002",
                    SeriAlpha = "2",
                    Seri = "31",
                    Serial = "500004",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456785",
                    PostalCode = "2234567892",
                    Address = "آدرس نمونه در شهر رشت",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                }
            },
                SignRequestPersonRelateds = new List<SignRequestPersonRelated>
            {
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("a86272fc-2dcb-480d-98ac-a9793ff59022"),
                    SignRequestId = sr2Id,
                    RowNo = 1,
                    MainPersonId = sr2PersonIds[0],
                    AgentPersonId = sr2PersonIds[1],
                    AgentTypeId = "1",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "2234",
                    AgentDocumentDate = "1404/01/02",
                    AgentDocumentIssuer = "شیراز",
                    SignRequestScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                },
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("64028e98-802a-4c37-aafe-27c3aa418fde"),
                    SignRequestId = sr2Id,
                    RowNo = 2,
                    MainPersonId = sr2PersonIds[0],
                    AgentPersonId = sr2PersonIds[2],
                    AgentTypeId = "2",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "2235",
                    AgentDocumentDate = "1404/01/02",
                    AgentDocumentIssuer = "تبریز",
                    SignRequestScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                }
            },
                SignElectronicBooks = new List<SignElectronicBook>
            {
                new SignElectronicBook
                {
                    Id = Guid.Parse("34e4c049-1d53-4bd5-a0de-82e700b11c46"),
                    ScriptoriumId = "57999",
                    ClassifyNo = 72876,
                    SignRequestId = sr2Id,
                    SignRequestNationalNo = "2234567890",
                    SignRequestPersonId = sr2PersonIds[0],
                    SignDate = "1404/01/02",
                    HashOfFingerprint = "StaticBase64String45",
                    HashOfFile = "StaticBase64String45",
                    DigitalSign = "StaticBase64String201",
                    ConfirmDate = "1404/01/02",
                    ConfirmTime = "11:00:00",
                    RecordDate = DateTime.MaxValue,
                    Ilm = "1",
                    SignCertificateDn = "CN=SampleCertificate-1235,O=Notary,OU=SSAA"
                }
            }
            };
            requests.Add(sr2);

            // --------- REQUEST 3 ----------
            var sr3Id = Guid.Parse("7050d603-d96e-452f-9277-13ba0afb8000");
            var sr3PersonIds = new List<Guid> { Guid.Parse("3b917530-1f05-4e0e-8d71-77fd05126758"), Guid.Parse("44a83e67-1c90-43de-b703-637429625974"), Guid.Parse("9116cbc1-423f-4825-b48a-68f81744dafc") };
            var sr3 = new Domain.Entities.SignRequest
            {
                Id = sr3Id,
                ReqNo = "140444457999000003",
                ReqDate = "1404/01/03",
                ReqTime = "12:00:00",
                ScriptoriumId = "57999",
                SignRequestGetterId = "03",
                SignRequestSubjectId = "0102",
                IsCostPaid = "1",
                SumPrices = 37000,
                ReceiptNo = "1234567893",
                PayCostDate = "1404/01/03",
                PayCostTime = "12:00:00",
                PaymentType = "PCPOS",
                BillNo = "1404010112345673",
                NationalNo = "140402157999000003",
                SecretCode = "345678",
                SignDate = "1404/01/03",
                SignTime = "12:00:00",
                Modifier = "پریسا احمدی",
                ModifyDate = "1404/01/03",
                ModifyTime = "12:00:00",
                Confirmer = "سمیرا جعفری",
                ConfirmDate = "1404/01/03",
                ConfirmTime = "12:00:00",
                DigitalSign = "StaticBase64String3",
                SignCertificateDn = "CN=SampleCertificate-1236,O=Notary,OU=SSAA",
                IsRemoteRequest = "1",
                State = "2",
                Ilm = "1",
                RecordDate = DateTime.MaxValue,
                IsReadyToPay = "1",
                LegacyId = $"LEGACY_{sr3Id.ToString().Substring(0, 8).ToUpper()}",
                SignText = "امضا انجام شد",
                SignRequestPeople = new List<SignRequestPerson>
            {
                new SignRequestPerson
                {
                    Id = sr3PersonIds[0],
                    SignRequestId = sr3Id,
                    RowNo = 1,
                    SignClassifyNo = 72877,
                    IsOriginal = "1",
                    IsRelated = "2",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "3234567890",
                    Name = "امیر",
                    Family = "کاظمی",
                    BirthDate = "1362/01/01",
                    SexType = "2",
                    FatherName = "مهدی",
                    IdentityIssueLocation = "اهواز",
                    IdentityNo = "1000003",
                    SeriAlpha = "1",
                    Seri = "12",
                    Serial = "500003",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456786",
                    PostalCode = "3234567890",
                    Address = "آدرس نمونه در شهر اهواز",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب اصلی"
                },
                new SignRequestPerson
                {
                    Id = sr3PersonIds[1],
                    SignRequestId = sr3Id,
                    RowNo = 2,
                    SignClassifyNo = 72878,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "3234567891",
                    Name = "فریبا",
                    Family = "نوری",
                    BirthDate = "1364/01/01",
                    SexType = "1",
                    FatherName = "کریم",
                    IdentityIssueLocation = "قم",
                    IdentityNo = "2000003",
                    SeriAlpha = "2",
                    Seri = "22",
                    Serial = "500004",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456787",
                    PostalCode = "3234567891",
                    Address = "آدرس نمونه در شهر قم",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                },
                new SignRequestPerson
                {
                    Id = sr3PersonIds[2],
                    SignRequestId = sr3Id,
                    RowNo = 3,
                    SignClassifyNo = 72879,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "3234567892",
                    Name = "نگار",
                    Family = "سلیمانی",
                    BirthDate = "1367/01/01",
                    SexType = "1",
                    FatherName = "مجید",
                    IdentityIssueLocation = "کرج",
                    IdentityNo = "3000003",
                    SeriAlpha = "2",
                    Seri = "32",
                    Serial = "500005",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456788",
                    PostalCode = "3234567892",
                    Address = "آدرس نمونه در شهر کرج",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                }
            },
                SignRequestPersonRelateds = new List<SignRequestPersonRelated>
            {
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("ddb585ca-0451-4dea-a8d2-24278ba40ceb"),
                    SignRequestId = sr3Id,
                    RowNo = 1,
                    MainPersonId = sr3PersonIds[0],
                    AgentPersonId = sr3PersonIds[1],
                    AgentTypeId = "1",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "3234",
                    AgentDocumentDate = "1404/01/03",
                    AgentDocumentIssuer = "اهواز",
                    SignRequestScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                },
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("fc6e2fd3-154d-4e8c-92c1-99a91e2c099d"),
                    SignRequestId = sr3Id,
                    RowNo = 2,
                    MainPersonId = sr3PersonIds[0],
                    AgentPersonId = sr3PersonIds[2],
                    AgentTypeId = "2",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "3235",
                    AgentDocumentDate = "1404/01/03",
                    AgentDocumentIssuer = "قم",
                    SignRequestScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                }
            },
                SignElectronicBooks = new List<SignElectronicBook>
            {
                new SignElectronicBook
                {
                    Id = Guid.Parse("1933b715-1fa8-4167-a76f-dcf528bc3c57"),
                    ScriptoriumId = "57999",
                    ClassifyNo = 72877,
                    SignRequestId = sr3Id,
                    SignRequestNationalNo = "3234567890",
                    SignRequestPersonId = sr3PersonIds[0],
                    SignDate = "1404/01/03",
                    HashOfFingerprint = "StaticBase64String46",
                    HashOfFile = "StaticBase64String46",
                    DigitalSign = "StaticBase64String202",
                    ConfirmDate = "1404/01/03",
                    ConfirmTime = "12:00:00",
                    RecordDate = DateTime.MaxValue,
                    Ilm = "1",
                    SignCertificateDn = "CN=SampleCertificate-1236,O=Notary,OU=SSAA"
                }
            }
            };
            requests.Add(sr3);

            // --------- REQUEST 4 ----------
            var sr4Id = Guid.Parse("597e2af5-43bc-40bc-83ec-6d233f04df26");
            var sr4PersonIds = new List<Guid> { Guid.Parse("840e93b0-95dc-4ebd-8b86-fd1c58b5d0ab"), Guid.Parse("e48e7011-2d22-4130-baeb-ca3e919efc18"), Guid.Parse("b16e2ae4-c915-4b01-8da0-e8a25e1ad997") };
            var sr4 = new Domain.Entities.SignRequest
            {
                Id = sr4Id,
                ReqNo = "140444457999000004",
                ReqDate = "1404/01/04",
                ReqTime = "13:00:00",
                ScriptoriumId = "57999",
                SignRequestGetterId = "04",
                SignRequestSubjectId = "0103",
                IsCostPaid = "1",
                SumPrices = 38000,
                ReceiptNo = "1234567894",
                PayCostDate = "1404/01/04",
                PayCostTime = "13:00:00",
                PaymentType = "PCPOS",
                BillNo = "1404010112345674",
                NationalNo = "140402157999000004",
                SecretCode = "456789",
                SignDate = "1404/01/04",
                SignTime = "13:00:00",
                Modifier = "نازنین رحیمی",
                ModifyDate = "1404/01/04",
                ModifyTime = "13:00:00",
                Confirmer = "الهه محمودی",
                ConfirmDate = "1404/01/04",
                ConfirmTime = "13:00:00",
                DigitalSign = "StaticBase64String4",
                SignCertificateDn = "CN=SampleCertificate-1237,O=Notary,OU=SSAA",
                IsRemoteRequest = "1",
                State = "2",
                Ilm = "1",
                RecordDate = DateTime.MaxValue,
                IsReadyToPay = "1",
                LegacyId = $"LEGACY_{sr4Id.ToString().Substring(0, 8).ToUpper()}",
                SignText = "امضا انجام شد",
                SignRequestPeople = new List<SignRequestPerson>
            {
                new SignRequestPerson
                {
                    Id = sr4PersonIds[0],
                    SignRequestId = sr4Id,
                    RowNo = 1,
                    SignClassifyNo = 72878,
                    IsOriginal = "1",
                    IsRelated = "2",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "4234567890",
                    Name = "حسین",
                    Family = "مهدوی",
                    BirthDate = "1363/01/01",
                    SexType = "2",
                    FatherName = "عباس",
                    IdentityIssueLocation = "یزد",
                    IdentityNo = "1000004",
                    SeriAlpha = "1",
                    Seri = "13",
                    Serial = "500004",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456789",
                    PostalCode = "4234567890",
                    Address = "آدرس نمونه در شهر یزد",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب اصلی"
                },
                new SignRequestPerson
                {
                    Id = sr4PersonIds[1],
                    SignRequestId = sr4Id,
                    RowNo = 2,
                    SignClassifyNo = 72879,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "4234567891",
                    Name = "سمیه",
                    Family = "حسینی",
                    BirthDate = "1365/01/01",
                    SexType = "1",
                    FatherName = "رضا",
                    IdentityIssueLocation = "ارومیه",
                    IdentityNo = "2000004",
                    SeriAlpha = "2",
                    Seri = "23",
                    Serial = "500005",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456790",
                    PostalCode = "4234567891",
                    Address = "آدرس نمونه در شهر ارومیه",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                },
                new SignRequestPerson
                {
                    Id = sr4PersonIds[2],
                    SignRequestId = sr4Id,
                    RowNo = 3,
                    SignClassifyNo = 72880,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "4234567892",
                    Name = "مرضیه",
                    Family = "امیری",
                    BirthDate = "1368/01/01",
                    SexType = "1",
                    FatherName = "محسن",
                    IdentityIssueLocation = "بندرعباس",
                    IdentityNo = "3000004",
                    SeriAlpha = "2",
                    Seri = "33",
                    Serial = "500006",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456791",
                    PostalCode = "4234567892",
                    Address = "آدرس نمونه در شهر بندرعباس",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                }
            },
                SignRequestPersonRelateds = new List<SignRequestPersonRelated>
            {
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("f88d75cd-9d7c-4867-9285-c9cb2361d998"),
                    SignRequestId = sr4Id,
                    RowNo = 1,
                    MainPersonId = sr4PersonIds[0],
                    AgentPersonId = sr4PersonIds[1],
                    AgentTypeId = "0",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "4234",
                    AgentDocumentDate = "1404/01/04",
                    AgentDocumentIssuer = "یزد",
                    SignRequestScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                },
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("705ea36e-4c71-4f2e-a665-c8be760d48bc"),
                    SignRequestId = sr4Id,
                    RowNo = 2,
                    MainPersonId = sr4PersonIds[0],
                    AgentPersonId = sr4PersonIds[2],
                    AgentTypeId = "2",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "4235",
                    AgentDocumentDate = "1404/01/04",
                    AgentDocumentIssuer = "ارومیه",
                    SignRequestScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                }
            },
                SignElectronicBooks = new List<SignElectronicBook>
            {
                new SignElectronicBook
                {
                    Id = Guid.Parse("dd14dfaa-9cd6-4891-9e05-86b493f3afea"),
                    ScriptoriumId = "57999",
                    ClassifyNo = 72878,
                    SignRequestId = sr4Id,
                    SignRequestNationalNo = "4234567890",
                    SignRequestPersonId = sr4PersonIds[0],
                    SignDate = "1404/01/04",
                    HashOfFingerprint = "StaticBase64String47",
                    HashOfFile = "StaticBase64String47",
                    DigitalSign = "StaticBase64String203",
                    ConfirmDate = "1404/01/04",
                    ConfirmTime = "13:00:00",
                    RecordDate = DateTime.MaxValue,
                    Ilm = "1",
                    SignCertificateDn = "CN=SampleCertificate-1237,O=Notary,OU=SSAA"
                }
            }
            };
            requests.Add(sr4);

            // --------- REQUEST 5 ----------
            var sr5Id = Guid.Parse("40af6b8a-def6-41bc-ac53-326b54f6cb0c");
            var sr5PersonIds = new List<Guid> { Guid.Parse("299b48a8-fa42-47fc-ab9a-59305cb6c6b9"), Guid.Parse("dc2039ae-499f-4607-8294-d25a2fd4caf2"), Guid.Parse("4f93497c-e3af-449a-acb7-07851dd58518") };
            var sr5 = new Domain.Entities.SignRequest
            {
                Id = sr5Id,
                ReqNo = "140444457999000005",
                ReqDate = "1404/01/05",
                ReqTime = "14:00:00",
                ScriptoriumId = "57999",
                SignRequestGetterId = "05",
                SignRequestSubjectId = "0104",
                IsCostPaid = "1",
                SumPrices = 39000,
                ReceiptNo = "1234567895",
                PayCostDate = "1404/01/05",
                PayCostTime = "14:00:00",
                PaymentType = "PCPOS",
                BillNo = "1404010112345675",
                NationalNo = "140402157999000005",
                SecretCode = "567890",
                SignDate = "1404/01/05",
                SignTime = "14:00:00",
                Modifier = "الهام صادقی",
                ModifyDate = "1404/01/05",
                ModifyTime = "14:00:00",
                Confirmer = "شیرین محمدی",
                ConfirmDate = "1404/01/05",
                ConfirmTime = "14:00:00",
                DigitalSign = "StaticBase64String5",
                SignCertificateDn = "CN=SampleCertificate-1238,O=Notary,OU=SSAA",
                IsRemoteRequest = "1",
                State = "2",
                Ilm = "1",
                RecordDate = DateTime.MaxValue,
                IsReadyToPay = "1",
                LegacyId = $"LEGACY_{sr5Id.ToString().Substring(0, 8).ToUpper()}",
                SignText = "امضا انجام شد",
                SignRequestPeople = new List<SignRequestPerson>
            {
                new SignRequestPerson
                {
                    Id = sr5PersonIds[0],
                    SignRequestId = sr5Id,
                    RowNo = 1,
                    SignClassifyNo = 72879,
                    IsOriginal = "1",
                    IsRelated = "2",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "5234567890",
                    Name = "مجید",
                    Family = "جعفری",
                    BirthDate = "1364/01/01",
                    SexType = "2",
                    FatherName = "حسین",
                    IdentityIssueLocation = "کرمان",
                    IdentityNo = "1000005",
                    SeriAlpha = "1",
                    Seri = "14",
                    Serial = "500005",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456792",
                    PostalCode = "5234567890",
                    Address = "آدرس نمونه در شهر کرمان",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب اصلی"
                },
                new SignRequestPerson
                {
                    Id = sr5PersonIds[1],
                    SignRequestId = sr5Id,
                    RowNo = 2,
                    SignClassifyNo = 72880,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "5234567891",
                    Name = "طاهره",
                    Family = "کریمی",
                    BirthDate = "1366/01/01",
                    SexType = "1",
                    FatherName = "محمود",
                    IdentityIssueLocation = "همدان",
                    IdentityNo = "2000005",
                    SeriAlpha = "2",
                    Seri = "24",
                    Serial = "500006",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456793",
                    PostalCode = "5234567891",
                    Address = "آدرس نمونه در شهر همدان",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                },
                new SignRequestPerson
                {
                    Id = sr5PersonIds[2],
                    SignRequestId = sr5Id,
                    RowNo = 3,
                    SignClassifyNo = 72881,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "5234567892",
                    Name = "زینب",
                    Family = "رحمانی",
                    BirthDate = "1369/01/01",
                    SexType = "1",
                    FatherName = "اکبر",
                    IdentityIssueLocation = "سنندج",
                    IdentityNo = "3000005",
                    SeriAlpha = "2",
                    Seri = "34",
                    Serial = "500007",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456794",
                    PostalCode = "5234567892",
                    Address = "آدرس نمونه در شهر سنندج",
                    Description = "",
                    ScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                }
            },
                SignRequestPersonRelateds = new List<SignRequestPersonRelated>
            {
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("f1c0b65e-842f-4d25-87b5-8e762ebf1c43"),
                    SignRequestId = sr5Id,
                    RowNo = 1,
                    MainPersonId = sr5PersonIds[0],
                    AgentPersonId = sr5PersonIds[1],
                    AgentTypeId = "1",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "5234",
                    AgentDocumentDate = "1404/01/05",
                    AgentDocumentIssuer = "کرمان",
                    SignRequestScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                },
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("a0cec4c7-a31e-4139-ae1e-c1391da07748"),
                    SignRequestId = sr5Id,
                    RowNo = 2,
                    MainPersonId = sr5PersonIds[0],
                    AgentPersonId = sr5PersonIds[2],
                    AgentTypeId = "2",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "5235",
                    AgentDocumentDate = "1404/01/05",
                    AgentDocumentIssuer = "همدان",
                    SignRequestScriptoriumId = "57999",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                }
            },
                SignElectronicBooks = new List<SignElectronicBook>
            {
                new SignElectronicBook
                {
                    Id = Guid.Parse("b3e7f505-c24a-4c4e-9fdb-7cb29956855b"),
                    ScriptoriumId = "52539",
                    ClassifyNo = 72879,
                    SignRequestId = sr5Id,
                    SignRequestNationalNo = "5234567890",
                    SignRequestPersonId = sr5PersonIds[0],
                    SignDate = "1404/01/05",
                    HashOfFingerprint = "StaticBase64String48",
                    HashOfFile = "StaticBase64String48",
                    DigitalSign = "StaticBase64String204",
                    ConfirmDate = "1404/01/05",
                    ConfirmTime = "14:00:00",
                    RecordDate = DateTime.MaxValue,
                    Ilm = "1",
                    SignCertificateDn = "CN=SampleCertificate-1238,O=Notary,OU=SSAA"
                }
            }
            };
            requests.Add(sr5);

            // --------- REQUEST 6 ----------
            var sr6Id = Guid.Parse("2008281d-a894-4e03-bdc2-ed60bcaad69d");
            var sr6PersonIds = new List<Guid> { Guid.Parse("344e6fd9-bfd8-4194-b6fc-c5a5f51690eb"), Guid.Parse("93aae3fd-79ae-4e48-8652-79c799256c46"), Guid.Parse("73360bf3-7572-4ade-80a3-4b619e93c1e4") };
            var sr6 = new Domain.Entities.SignRequest
            {
                Id = sr6Id,
                ReqNo = "140444452539000001",
                ReqDate = "1404/01/06",
                ReqTime = "15:00:00",
                ScriptoriumId = "52539",
                SignRequestGetterId = "04",
                SignRequestSubjectId = "0105",
                IsCostPaid = "1",
                SumPrices = 40000,
                ReceiptNo = "1234567896",
                PayCostDate = "1404/01/06",
                PayCostTime = "15:00:00",
                PaymentType = "PCPOS",
                BillNo = "1404010112345676",
                NationalNo = "1404021525399000001",
                SecretCode = "678901",
                SignDate = "1404/01/06",
                SignTime = "15:00:00",
                Modifier = "مینا احمدی",
                ModifyDate = "1404/01/06",
                ModifyTime = "15:00:00",
                Confirmer = "نرگس رضایی",
                ConfirmDate = "1404/01/06",
                ConfirmTime = "15:00:00",
                DigitalSign = "StaticBase64String6",
                SignCertificateDn = "CN=SampleCertificate-1239,O=Notary,OU=SSAA",
                IsRemoteRequest = "1",
                State = "2",
                Ilm = "1",
                RecordDate = DateTime.MaxValue,
                IsReadyToPay = "1",
                LegacyId = $"LEGACY_{sr6Id.ToString().Substring(0, 8).ToUpper()}",
                SignText = "امضا انجام شد",
                SignRequestPeople = new List<SignRequestPerson>
            {
                new SignRequestPerson
                {
                    Id = sr6PersonIds[0],
                    SignRequestId = sr6Id,
                    RowNo = 1,
                    SignClassifyNo = 72880,
                    IsOriginal = "1",
                    IsRelated = "2",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "6234567890",
                    Name = "سعید",
                    Family = "محمدی",
                    BirthDate = "1365/01/01",
                    SexType = "2",
                    FatherName = "علی",
                    IdentityIssueLocation = "زاهدان",
                    IdentityNo = "1000006",
                    SeriAlpha = "1",
                    Seri = "15",
                    Serial = "500006",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456795",
                    PostalCode = "6234567890",
                    Address = "آدرس نمونه در شهر زاهدان",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب اصلی"
                },
                new SignRequestPerson
                {
                    Id = sr6PersonIds[1],
                    SignRequestId = sr6Id,
                    RowNo = 2,
                    SignClassifyNo = 72881,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "6234567891",
                    Name = "فاطمه",
                    Family = "حسنی",
                    BirthDate = "1367/01/01",
                    SexType = "1",
                    FatherName = "محمدرضا",
                    IdentityIssueLocation = "گرگان",
                    IdentityNo = "2000006",
                    SeriAlpha = "2",
                    Seri = "25",
                    Serial = "500007",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456796",
                    PostalCode = "6234567891",
                    Address = "آدرس نمونه در شهر گرگان",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                },
                new SignRequestPerson
                {
                    Id = sr6PersonIds[2],
                    SignRequestId = sr6Id,
                    RowNo = 3,
                    SignClassifyNo = 72882,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "6234567892",
                    Name = "لیلا",
                    Family = "موسوی",
                    BirthDate = "1370/01/01",
                    SexType = "1",
                    FatherName = "حیدر",
                    IdentityIssueLocation = "ساری",
                    IdentityNo = "3000006",
                    SeriAlpha = "2",
                    Seri = "35",
                    Serial = "500008",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456797",
                    PostalCode = "6234567892",
                    Address = "آدرس نمونه در شهر ساری",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                }
            },
                SignRequestPersonRelateds = new List<SignRequestPersonRelated>
            {
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("057e6962-f300-480d-8c8c-60fef31a1a8f"),
                    SignRequestId = sr6Id,
                    RowNo = 1,
                    MainPersonId = sr6PersonIds[0],
                    AgentPersonId = sr6PersonIds[1],
                    AgentTypeId = "1",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "6234",
                    AgentDocumentDate = "1404/01/06",
                    AgentDocumentIssuer = "زاهدان",
                    SignRequestScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                },
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("75eeea07-6ae5-4783-88a1-b12097c70414"),
                    SignRequestId = sr6Id,
                    RowNo = 2,
                    MainPersonId = sr6PersonIds[0],
                    AgentPersonId = sr6PersonIds[2],
                    AgentTypeId = "2",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "6235",
                    AgentDocumentDate = "1404/01/06",
                    AgentDocumentIssuer = "گرگان",
                    SignRequestScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                }
            },
                SignElectronicBooks = new List<SignElectronicBook>
            {
                new SignElectronicBook
                {
                    Id = Guid.Parse("f4c784de-2001-4a71-9103-f50a27b40d6c"),
                    ScriptoriumId = "52539",
                    ClassifyNo = 72880,
                    SignRequestId = sr6Id,
                    SignRequestNationalNo = "6234567890",
                    SignRequestPersonId = sr6PersonIds[0],
                    SignDate = "1404/01/06",
                    HashOfFingerprint = "StaticBase64String49",
                    HashOfFile = "StaticBase64String49",
                    DigitalSign = "StaticBase64String205",
                    ConfirmDate = "1404/01/06",
                    ConfirmTime = "15:00:00",
                    RecordDate = DateTime.MaxValue,
                    Ilm = "1",
                    SignCertificateDn = "CN=SampleCertificate-1239,O=Notary,OU=SSAA"
                }
            }
            };
            requests.Add(sr6);

            // --------- REQUEST 7 ----------
            var sr7Id = Guid.Parse("34308507-8f13-4a51-8a83-429f3bbdbd6d");
            var sr7PersonIds = new List<Guid> { Guid.Parse("f16126d0-cadb-46f5-a434-90227c8c5272"), Guid.Parse("d25b01d6-0538-457a-a521-5d219b6f7a12"), Guid.Parse("79f5405a-16ae-4d3e-afca-e154ec15a9d7") };
            var sr7 = new Domain.Entities.SignRequest
            {
                Id = sr7Id,
                ReqNo = "140444452539000002",
                ReqDate = "1404/01/07",
                ReqTime = "16:00:00",
                ScriptoriumId = "52539",
                SignRequestGetterId = "06",
                SignRequestSubjectId = "0106",
                IsCostPaid = "1",
                SumPrices = 41000,
                ReceiptNo = "1234567897",
                PayCostDate = "1404/01/07",
                PayCostTime = "16:00:00",
                PaymentType = "PCPOS",
                BillNo = "1404010112345677",
                NationalNo = "1404021525399000002",
                SecretCode = "789012",
                SignDate = "1404/01/07",
                SignTime = "16:00:00",
                Modifier = "زهرا کریمی",
                ModifyDate = "1404/01/07",
                ModifyTime = "16:00:00",
                Confirmer = "مریم احمدی",
                ConfirmDate = "1404/01/07",
                ConfirmTime = "16:00:00",
                DigitalSign = "StaticBase64String7",
                SignCertificateDn = "CN=SampleCertificate-1240,O=Notary,OU=SSAA",
                IsRemoteRequest = "1",
                State = "2",
                Ilm = "1",
                RecordDate = DateTime.MaxValue,
                IsReadyToPay = "1",
                LegacyId = $"LEGACY_{sr7Id.ToString().Substring(0, 8).ToUpper()}",
                SignText = "امضا انجام شد",
                SignRequestPeople = new List<SignRequestPerson>
            {
                new SignRequestPerson
                {
                    Id = sr7PersonIds[0],
                    SignRequestId = sr7Id,
                    RowNo = 1,
                    SignClassifyNo = 72881,
                    IsOriginal = "1",
                    IsRelated = "2",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "7234567890",
                    Name = "حمید",
                    Family = "رضایی",
                    BirthDate = "1366/01/01",
                    SexType = "2",
                    FatherName = "مهدی",
                    IdentityIssueLocation = "بوشهر",
                    IdentityNo = "1000007",
                    SeriAlpha = "1",
                    Seri = "16",
                    Serial = "500007",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456798",
                    PostalCode = "7234567890",
                    Address = "آدرس نمونه در شهر بوشهر",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب اصلی"
                },
                new SignRequestPerson
                {
                    Id = sr7PersonIds[1],
                    SignRequestId = sr7Id,
                    RowNo = 2,
                    SignClassifyNo = 72882,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "7234567891",
                    Name = "نازیلا",
                    Family = "جعفری",
                    BirthDate = "1368/01/01",
                    SexType = "1",
                    FatherName = "کریم",
                    IdentityIssueLocation = "کرمانشاه",
                    IdentityNo = "2000007",
                    SeriAlpha = "2",
                    Seri = "26",
                    Serial = "500008",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456799",
                    PostalCode = "7234567891",
                    Address = "آدرس نمونه در شهر کرمانشاه",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                },
                new SignRequestPerson
                {
                    Id = sr7PersonIds[2],
                    SignRequestId = sr7Id,
                    RowNo = 3,
                    SignClassifyNo = 72883,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "7234567892",
                    Name = "سپیده",
                    Family = "محمودی",
                    BirthDate = "1371/01/01",
                    SexType = "1",
                    FatherName = "فرهاد",
                    IdentityIssueLocation = "ایلام",
                    IdentityNo = "3000007",
                    SeriAlpha = "2",
                    Seri = "36",
                    Serial = "500009",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456800",
                    PostalCode = "7234567892",
                    Address = "آدرس نمونه در شهر ایلام",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                }
            },
                SignRequestPersonRelateds = new List<SignRequestPersonRelated>
            {
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("f8fc9dce-c256-4709-97b8-4590af6cb05f"),
                    SignRequestId = sr7Id,
                    RowNo = 1,
                    MainPersonId = sr7PersonIds[0],
                    AgentPersonId = sr7PersonIds[1],
                    AgentTypeId = "1",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "7234",
                    AgentDocumentDate = "1404/01/07",
                    AgentDocumentIssuer = "بوشهر",
                    SignRequestScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                },
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("871fde4e-20a4-46a5-b534-3fa7a5d7858b"),
                    SignRequestId = sr7Id,
                    RowNo = 2,
                    MainPersonId = sr7PersonIds[0],
                    AgentPersonId = sr7PersonIds[2],
                    AgentTypeId = "2",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "7235",
                    AgentDocumentDate = "1404/01/07",
                    AgentDocumentIssuer = "کرمانشah",
                    SignRequestScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                }
            },
                SignElectronicBooks = new List<SignElectronicBook>
            {
                new SignElectronicBook
                {
                    Id = Guid.Parse("f432b5b7-df20-4e37-805d-d1a8235e0509"),
                    ScriptoriumId = "52539",
                    ClassifyNo = 72881,
                    SignRequestId = sr7Id,
                    SignRequestNationalNo = "7234567890",
                    SignRequestPersonId = sr7PersonIds[0],
                    SignDate = "1404/01/07",
                    HashOfFingerprint = "StaticBase64String50",
                    HashOfFile = "StaticBase64String50",
                    DigitalSign = "StaticBase64String206",
                    ConfirmDate = "1404/01/07",
                    ConfirmTime = "16:00:00",
                    RecordDate = DateTime.MaxValue,
                    Ilm = "1",
                    SignCertificateDn = "CN=SampleCertificate-1240,O=Notary,OU=SSAA"
                }
            }
            };
            requests.Add(sr7);

            // --------- REQUEST 8 ----------
            var sr8Id = Guid.Parse("b9787927-ec18-4321-87e0-da357f082679");
            var sr8PersonIds = new List<Guid> { Guid.Parse("7cf1d770-6605-480c-a507-32a6fd82edce"), Guid.Parse("6493b554-9846-45c1-a51e-4ead8415d534"), Guid.Parse("d634f08d-6424-49ce-bf8e-eddeebb42323") };
            var sr8 = new Domain.Entities.SignRequest
            {
                Id = sr8Id,
                ReqNo = "140444452539000003",
                ReqDate = "1404/01/08",
                ReqTime = "17:00:00",
                ScriptoriumId = "52539",
                SignRequestGetterId = "07",
                SignRequestSubjectId = "0107",
                IsCostPaid = "1",
                SumPrices = 42000,
                ReceiptNo = "1234567898",
                PayCostDate = "1404/01/08",
                PayCostTime = "17:00:00",
                PaymentType = "PCPOS",
                BillNo = "1404010112345678",
                NationalNo = "1404021525399000003",
                SecretCode = "890123",
                SignDate = "1404/01/08",
                SignTime = "17:00:00",
                Modifier = "فاطمه حسینی",
                ModifyDate = "1404/01/08",
                ModifyTime = "17:00:00",
                Confirmer = "زهرا موسوی",
                ConfirmDate = "1404/01/08",
                ConfirmTime = "17:00:00",
                DigitalSign = "StaticBase64String8",
                SignCertificateDn = "CN=SampleCertificate-1241,O=Notary,OU=SSAA",
                IsRemoteRequest = "1",
                State = "2",
                Ilm = "1",
                RecordDate = DateTime.MaxValue,
                IsReadyToPay = "1",
                LegacyId = $"LEGACY_{sr8Id.ToString().Substring(0, 8).ToUpper()}",
                SignText = "امضا انجام شد",
                SignRequestPeople = new List<SignRequestPerson>
            {
                new SignRequestPerson
                {
                    Id = sr8PersonIds[0],
                    SignRequestId = sr8Id,
                    RowNo = 1,
                    SignClassifyNo = 72882,
                    IsOriginal = "1",
                    IsRelated = "2",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "8234567890",
                    Name = "علیرضا",
                    Family = "کرمی",
                    BirthDate = "1367/01/01",
                    SexType = "2",
                    FatherName = "محمدعلی",
                    IdentityIssueLocation = "اراک",
                    IdentityNo = "1000008",
                    SeriAlpha = "1",
                    Seri = "17",
                    Serial = "500008",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456801",
                    PostalCode = "8234567890",
                    Address = "آدرس نمونه در شهر اراک",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب اصلی"
                },
                new SignRequestPerson
                {
                    Id = sr8PersonIds[1],
                    SignRequestId = sr8Id,
                    RowNo = 2,
                    SignClassifyNo = 72883,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "8234567891",
                    Name = "معصومه",
                    Family = "نظری",
                    BirthDate = "1369/01/01",
                    SexType = "1",
                    FatherName = "رحمان",
                    IdentityIssueLocation = "یزد",
                    IdentityNo = "2000008",
                    SeriAlpha = "2",
                    Seri = "27",
                    Serial = "500009",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456802",
                    PostalCode = "8234567891",
                    Address = "آدرس نمونه در شهر یزد",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                },
                new SignRequestPerson
                {
                    Id = sr8PersonIds[2],
                    SignRequestId = sr8Id,
                    RowNo = 3,
                    SignClassifyNo = 72884,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "8234567892",
                    Name = "مهسا",
                    Family = "صادقی",
                    BirthDate = "1372/01/01",
                    SexType = "1",
                    FatherName = "حمیدرضا",
                    IdentityIssueLocation = "قزوین",
                    IdentityNo = "3000008",
                    SeriAlpha = "2",
                    Seri = "37",
                    Serial = "500010",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456803",
                    PostalCode = "8234567892",
                    Address = "آدرس نمونه در شهر قزوین",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                }
            },
                SignRequestPersonRelateds = new List<SignRequestPersonRelated>
            {
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("dcadf5b8-b2d3-401a-ae6c-baaa15c22d48"),
                    SignRequestId = sr8Id,
                    RowNo = 1,
                    MainPersonId = sr8PersonIds[0],
                    AgentPersonId = sr8PersonIds[1],
                    AgentTypeId = "1",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "8234",
                    AgentDocumentDate = "1404/01/08",
                    AgentDocumentIssuer = "اراک",
                    SignRequestScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                },
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("85e9077a-ec04-4b7d-ad4b-d60ca2d12bbc"),
                    SignRequestId = sr8Id,
                    RowNo = 2,
                    MainPersonId = sr8PersonIds[0],
                    AgentPersonId = sr8PersonIds[2],
                    AgentTypeId = "2",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "8235",
                    AgentDocumentDate = "1404/01/08",
                    AgentDocumentIssuer = "یزد",
                    SignRequestScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                }
            },
                SignElectronicBooks = new List<SignElectronicBook>
            {
                new SignElectronicBook
                {
                    Id = Guid.Parse("97594b1f-69cb-460c-ba89-b5267025b948"),
                    ScriptoriumId = "52539",
                    ClassifyNo = 72882,
                    SignRequestId = sr8Id,
                    SignRequestNationalNo = "8234567890",
                    SignRequestPersonId = sr8PersonIds[0],
                    SignDate = "1404/01/08",
                    HashOfFingerprint = "StaticBase64String51",
                    HashOfFile = "StaticBase64String51",
                    DigitalSign = "StaticBase64String207",
                    ConfirmDate = "1404/01/08",
                    ConfirmTime = "17:00:00",
                    RecordDate = DateTime.MaxValue,
                    Ilm = "1",
                    SignCertificateDn = "CN=SampleCertificate-1241,O=Notary,OU=SSAA"
                }
            }
            };
            requests.Add(sr8);

            // --------- REQUEST 9 ----------
            var sr9Id = Guid.Parse("7d34f66e-9200-4c7c-8dc3-0389bdcbe23a");
            var sr9PersonIds = new List<Guid> { Guid.Parse("b7668909-8428-4d53-9950-1997621525d7"), Guid.Parse("dc17f364-530e-4f88-be5a-1a35d3075385"), Guid.Parse("88c87e45-7196-46fd-afbe-3cc3ed13c56c") };
            var sr9 = new Domain.Entities.SignRequest
            {
                Id = sr9Id,
                ReqNo = "140444452539000004",
                ReqDate = "1404/01/09",
                ReqTime = "18:00:00",
                ScriptoriumId = "52539",
                SignRequestGetterId = "08",
                SignRequestSubjectId = "0201",
                IsCostPaid = "1",
                SumPrices = 43000,
                ReceiptNo = "1234567899",
                PayCostDate = "1404/01/09",
                PayCostTime = "18:00:00",
                PaymentType = "PCPOS",
                BillNo = "1404010112345679",
                NationalNo = "1404021525399000004",
                SecretCode = "901234",
                SignDate = "1404/01/09",
                SignTime = "18:00:00",
                Modifier = "ناهید محمدی",
                ModifyDate = "1404/01/09",
                ModifyTime = "18:00:00",
                Confirmer = "پگاه رحیمی",
                ConfirmDate = "1404/01/09",
                ConfirmTime = "18:00:00",
                DigitalSign = "StaticBase64String9",
                SignCertificateDn = "CN=SampleCertificate-1242,O=Notary,OU=SSAA",
                IsRemoteRequest = "1",
                State = "2",
                Ilm = "1",
                RecordDate = DateTime.MaxValue,
                IsReadyToPay = "1",
                LegacyId = $"LEGACY_{sr9Id.ToString().Substring(0, 8).ToUpper()}",
                SignText = "امضا انجام شد",
                SignRequestPeople = new List<SignRequestPerson>
            {
                new SignRequestPerson
                {
                    Id = sr9PersonIds[0],
                    SignRequestId = sr9Id,
                    RowNo = 1,
                    SignClassifyNo = 72883,
                    IsOriginal = "1",
                    IsRelated = "2",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "9234567890",
                    Name = "محمدرضا",
                    Family = "جعفری",
                    BirthDate = "1368/01/01",
                    SexType = "2",
                    FatherName = "عباس",
                    IdentityIssueLocation = "بجنورد",
                    IdentityNo = "1000009",
                    SeriAlpha = "1",
                    Seri = "18",
                    Serial = "500009",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456804",
                    PostalCode = "9234567890",
                    Address = "آدرس نمونه در شهر بجنورد",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب اصلی"
                },
                new SignRequestPerson
                {
                    Id = sr9PersonIds[1],
                    SignRequestId = sr9Id,
                    RowNo = 2,
                    SignClassifyNo = 72884,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "9234567891",
                    Name = "زینت",
                    Family = "کرمانی",
                    BirthDate = "1370/01/01",
                    SexType = "1",
                    FatherName = "مهدی",
                    IdentityIssueLocation = "سمنان",
                    IdentityNo = "2000009",
                    SeriAlpha = "2",
                    Seri = "28",
                    Serial = "500010",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456805",
                    PostalCode = "9234567891",
                    Address = "آدرس نمونه در شهر سمنان",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                },
                new SignRequestPerson
                {
                    Id = sr9PersonIds[2],
                    SignRequestId = sr9Id,
                    RowNo = 3,
                    SignClassifyNo = 72885,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "9234567892",
                    Name = "نگین",
                    Family = "حیدری",
                    BirthDate = "1373/01/01",
                    SexType = "1",
                    FatherName = "رضا",
                    IdentityIssueLocation = "زنجان",
                    IdentityNo = "3000009",
                    SeriAlpha = "2",
                    Seri = "38",
                    Serial = "500011",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456806",
                    PostalCode = "9234567892",
                    Address = "آدرس نمونه در شهر زنجان",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                }
            },
                SignRequestPersonRelateds = new List<SignRequestPersonRelated>
            {
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("ebb9f49a-19c6-4c84-aa03-e21f012731d0"),
                    SignRequestId = sr9Id,
                    RowNo = 1,
                    MainPersonId = sr9PersonIds[0],
                    AgentPersonId = sr9PersonIds[1],
                    AgentTypeId = "1",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "9234",
                    AgentDocumentDate = "1404/01/09",
                    AgentDocumentIssuer = "بجنورد",
                    SignRequestScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                },
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("734d12de-f62a-4882-b863-0f499ad6fe25"),
                    SignRequestId = sr9Id,
                    RowNo = 2,
                    MainPersonId = sr9PersonIds[0],
                    AgentPersonId = sr9PersonIds[2],
                    AgentTypeId = "2",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "9235",
                    AgentDocumentDate = "1404/01/09",
                    AgentDocumentIssuer = "سمنان",
                    SignRequestScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                }
            },
                SignElectronicBooks = new List<SignElectronicBook>
            {
                new SignElectronicBook
                {
                    Id = Guid.Parse("49b2ddde-7cae-4000-b155-deb84cc38359"),
                    ScriptoriumId = "52539",
                    ClassifyNo = 72883,
                    SignRequestId = sr9Id,
                    SignRequestNationalNo = "9234567890",
                    SignRequestPersonId = sr9PersonIds[0],
                    SignDate = "1404/01/09",
                    HashOfFingerprint = "StaticBase64String52",
                    HashOfFile = "StaticBase64String52",
                    DigitalSign = "StaticBase64String208",
                    ConfirmDate = "1404/01/09",
                    ConfirmTime = "18:00:00",
                    RecordDate = DateTime.MaxValue,
                    Ilm = "1",
                    SignCertificateDn = "CN=SampleCertificate-1242,O=Notary,OU=SSAA"
                }
            }
            };
            requests.Add(sr9);

            // --------- REQUEST 10 ----------
            var sr10Id = Guid.Parse("87c69e83-ad82-449c-a88d-815ceb340b33");
            var sr10PersonIds = new List<Guid> { Guid.Parse("6be54d7c-8ed7-4578-8cb3-827492bb976f"), Guid.Parse("6f629c99-4889-44a9-aea2-c81ad52c8847"), Guid.Parse("92c8b38f-25cb-4503-8b0c-8196ab73998c") };
            var sr10 = new Domain.Entities.SignRequest
            {
                Id = sr10Id,
                ReqNo = "140444452539000005",
                ReqDate = "1404/01/10",
                ReqTime = "19:00:00",
                ScriptoriumId = "52539",
                SignRequestGetterId = "09",
                SignRequestSubjectId = "0202",
                IsCostPaid = "1",
                SumPrices = 44000,
                ReceiptNo = "1234567900",
                PayCostDate = "1404/01/10",
                PayCostTime = "19:00:00",
                PaymentType = "PCPOS",
                BillNo = "1404010112345680",
                NationalNo = "1404021525399000005",
                SecretCode = "012345",
                SignDate = "1404/01/10",
                SignTime = "19:00:00",
                Modifier = "شیرین احمدی",
                ModifyDate = "1404/01/10",
                ModifyTime = "19:00:00",
                Confirmer = "نازیلا محمدی",
                ConfirmDate = "1404/01/10",
                ConfirmTime = "19:00:00",
                DigitalSign = "StaticBase64String10",
                SignCertificateDn = "CN=SampleCertificate-1243,O=Notary,OU=SSAA",
                IsRemoteRequest = "1",
                State = "2",
                Ilm = "1",
                RecordDate = DateTime.MaxValue,
                IsReadyToPay = "1",
                LegacyId = $"LEGACY_{sr10Id.ToString().Substring(0, 8).ToUpper()}",
                SignText = "امضا انجام شد",
                SignRequestPeople = new List<SignRequestPerson>
            {
                new SignRequestPerson
                {
                    Id = sr10PersonIds[0],
                    SignRequestId = sr10Id,
                    RowNo = 1,
                    SignClassifyNo = 72884,
                    IsOriginal = "1",
                    IsRelated = "2",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "0234567890",
                    Name = "امیرحسین",
                    Family = "مهدوی",
                    BirthDate = "1369/01/01",
                    SexType = "2",
                    FatherName = "محمد",
                    IdentityIssueLocation = "خرم آباد",
                    IdentityNo = "1000010",
                    SeriAlpha = "1",
                    Seri = "19",
                    Serial = "500010",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456807",
                    PostalCode = "0234567890",
                    Address = "آدرس نمونه در شهر خرم آباد",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب اصلی"
                },
                new SignRequestPerson
                {
                    Id = sr10PersonIds[1],
                    SignRequestId = sr10Id,
                    RowNo = 2,
                    SignClassifyNo = 72885,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "0234567891",
                    Name = "مهین",
                    Family = "سعادت",
                    BirthDate = "1371/01/01",
                    SexType = "1",
                    FatherName = "حسین",
                    IdentityIssueLocation = "بیرجند",
                    IdentityNo = "2000010",
                    SeriAlpha = "2",
                    Seri = "29",
                    Serial = "500011",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456808",
                    PostalCode = "0234567891",
                    Address = "آدرس نمونه در شهر بیرجند",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                },
                new SignRequestPerson
                {
                    Id = sr10PersonIds[2],
                    SignRequestId = sr10Id,
                    RowNo = 3,
                    SignClassifyNo = 72886,
                    IsOriginal = "2",
                    IsRelated = "1",
                    PersonType = "1",
                    IsIranian = "1",
                    NationalNo = "0234567892",
                    Name = "راحله",
                    Family = "امانی",
                    BirthDate = "1374/01/01",
                    SexType = "1",
                    FatherName = "علیرضا",
                    IdentityIssueLocation = "یزد",
                    IdentityNo = "3000010",
                    SeriAlpha = "2",
                    Seri = "39",
                    Serial = "500012",
                    SanaState = "1",
                    MobileNoState = "1",
                    IsSabtahvalChecked = "1",
                    IsSabtahvalCorrect = "1",
                    IsAlive = "1",
                    TfaState = "1",
                    IsFingerprintGotten = "1",
                    MobileNo = "09123456809",
                    PostalCode = "0234567892",
                    Address = "آدرس نمونه در شهر یزد",
                    Description = "",
                    ScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    SignClassifyNoDescription = "شماره ترتیب وابسته"
                }
            },
                SignRequestPersonRelateds = new List<SignRequestPersonRelated>
            {
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("afd342e7-5f8a-47ee-8675-26c24b91c004"),
                    SignRequestId = sr10Id,
                    RowNo = 1,
                    MainPersonId = sr10PersonIds[0],
                    AgentPersonId = sr10PersonIds[1],
                    AgentTypeId = "1",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "0234",
                    AgentDocumentDate = "1404/01/10",
                    AgentDocumentIssuer = "خرم آباد",
                    SignRequestScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                },
                new SignRequestPersonRelated
                {
                    Id = Guid.Parse("ccbd4733-b844-4267-a587-686855afe259"),
                    SignRequestId = sr10Id,
                    RowNo = 2,
                    MainPersonId = sr10PersonIds[0],
                    AgentPersonId = sr10PersonIds[2],
                    AgentTypeId = "2",
                    IsAgentDocumentAbroad = "2",
                    IsRelatedDocumentInSsar = "1",
                    AgentDocumentNo = "0235",
                    AgentDocumentDate = "1404/01/10",
                    AgentDocumentIssuer = "بیرجند",
                    SignRequestScriptoriumId = "52539",
                    Ilm = "1",
                    RecordDate = DateTime.MaxValue,
                    Description = "رابطه وکالت تنظیم شده"
                }
            },
                SignElectronicBooks = new List<SignElectronicBook>
            {
                new SignElectronicBook
                {
                    Id = Guid.Parse("428997c5-b373-4dfb-90ef-9ad16e9a3f87"),
                    ScriptoriumId = "52539",
                    ClassifyNo = 72884,
                    SignRequestId = sr10Id,
                    SignRequestNationalNo = "0234567890",
                    SignRequestPersonId = sr10PersonIds[0],
                    SignDate = "1404/01/10",
                    HashOfFingerprint = "StaticBase64String53",
                    HashOfFile = "StaticBase64String53",
                    DigitalSign = "StaticBase64String209",
                    ConfirmDate = "1404/01/10",
                    ConfirmTime = "19:00:00",
                    RecordDate = DateTime.MaxValue,
                    Ilm = "1",
                    SignCertificateDn = "CN=SampleCertificate-1243,O=Notary,OU=SSAA"
                }
            }
            };
            requests.Add(sr10);

            return requests;
        }


        public static List<SignRequestSubject> GetSignRequestSubjects()
        {
            return new List<SignRequestSubject>
        {
            new SignRequestSubject
            {
                Id = "0101",
                SignRequestSubjectGroupId = "01",
                Code = "0101",
                Title = "رضایت نامه خروج از كشور همسر",
                State = "1",
                LegacyId = "0101"
            },
            new SignRequestSubject
            {
                Id = "0102",
                SignRequestSubjectGroupId = "01",
                Code = "0102",
                Title = "رضایت نامه خروج از كشور فرزندان",
                State = "1",
                LegacyId = "0102"
            },
            new SignRequestSubject
            {
                Id = "0103",
                SignRequestSubjectGroupId = "01",
                Code = "0103",
                Title = "رضایت نامه ساخت و ساز برای شهرداری",
                State = "1",
                LegacyId = "0103"
            },
            new SignRequestSubject
            {
                Id = "0104",
                SignRequestSubjectGroupId = "01",
                Code = "0104",
                Title = "رضایت نامه",
                State = "1",
                LegacyId = "0104"
            },
            new SignRequestSubject
            {
                Id = "0105",
                SignRequestSubjectGroupId = "01",
                Code = "0105",
                Title = "رضایت نامه پیشروی ساختمان همسایه",
                State = "1",
                LegacyId = "0105"
            },
            new SignRequestSubject
            {
                Id = "0106",
                SignRequestSubjectGroupId = "01",
                Code = "0106",
                Title = "رضایت نامه استفاده از نام خانوادگی",
                State = "1",
                LegacyId = "0106"
            },
            new SignRequestSubject
            {
                Id = "0107",
                SignRequestSubjectGroupId = "01",
                Code = "0107",
                Title = "رضایت نامه معاف كردن فرزند",
                State = "1",
                LegacyId = "0107"
            },
            new SignRequestSubject
            {
                Id = "0201",
                SignRequestSubjectGroupId = "02",
                Code = "0201",
                Title = "تعهد",
                State = "1",
                LegacyId = "0201"
            },
            new SignRequestSubject
            {
                Id = "0202",
                SignRequestSubjectGroupId = "02",
                Code = "0202",
                Title = "تعهد اخذ شناسنامه ساختمان مالك و ناظرین",
                State = "1",
                LegacyId = "0202"
            },
            new SignRequestSubject
            {
                Id = "0203",
                SignRequestSubjectGroupId = "02",
                Code = "0203",
                Title = "تعهد مفقود شدن كارت پایان خدمت",
                State = "1",
                LegacyId = "0203"
            },
            new SignRequestSubject
            {
                Id = "0204",
                SignRequestSubjectGroupId = "02",
                Code = "0204",
                Title = "تعهد مفقود شدن گواهینامه رانندگی",
                State = "1",
                LegacyId = "0204"
            },
            new SignRequestSubject
            {
                Id = "0205",
                SignRequestSubjectGroupId = "02",
                Code = "0205",
                Title = "تعهد مفقود شدن كارت هوشمند راننده",
                State = "1",
                LegacyId = "0205"
            },
            new SignRequestSubject
            {
                Id = "0206",
                SignRequestSubjectGroupId = "02",
                Code = "0206",
                Title = "تعهد مفقoid شدن كارت هوشمند خودرو",
                State = "1",
                LegacyId = "0206"
            },
            new SignRequestSubject
            {
                Id = "0207",
                SignRequestSubjectGroupId = "02",
                Code = "0207",
                Title = "تعهد قبول مسئولیت مالكیت محل حفر چاه",
                State = "1",
                LegacyId = "0207"
            },
            new SignRequestSubject
            {
                Id = "0208",
                SignRequestSubjectGroupId = "02",
                Code = "0208",
                Title = "تعهد شاغل نبودن",
                State = "1",
                LegacyId = "0208"
            },
            new SignRequestSubject
            {
                Id = "0209",
                SignRequestSubjectGroupId = "02",
                Code = "0209",
                Title = "تعهد بازگشت به كشور زائرین عتبات",
                State = "1",
                LegacyId = "0209"
            },
            new SignRequestSubject
            {
                Id = "0210",
                SignRequestSubjectGroupId = "02",
                Code = "0210",
                Title = "تعهد اخذ انشعاب گاز املاك بی سند",
                State = "1",
                LegacyId = "0210"
            },
            new SignRequestSubject
            {
                Id = "0211",
                SignRequestSubjectGroupId = "02",
                Code = "0211",
                Title = "تعهد متقاضیان وام كمیته امداد",
                State = "1",
                LegacyId = "0211"
            },
            new SignRequestSubject
            {
                Id = "0212",
                SignRequestSubjectGroupId = "02",
                Code = "0212",
                Title = "تعهد اخذ كارت هوشمند راننده",
                State = "1",
                LegacyId = "0212"
            },
            new SignRequestSubject
            {
                Id = "0213",
                SignRequestSubjectGroupId = "02",
                Code = "0213",
                Title = "تعهد اخذ كارت هوشمند خودرو",
                State = "1",
                LegacyId = "0213"
            },
            new SignRequestSubject
            {
                Id = "0214",
                SignRequestSubjectGroupId = "02",
                Code = "0214",
                Title = "تعهد متقاضیان خرید خدمت مشمولین غایب",
                State = "1",
                LegacyId = "0214"
            },
            new SignRequestSubject
            {
                Id = "0215",
                SignRequestSubjectGroupId = "02",
                Code = "0215",
                Title = "تعهد ضمانت مستخدمین نیروی انتظامی",
                State = "1",
                LegacyId = "0215"
            },
            new SignRequestSubject
            {
                Id = "0216",
                SignRequestSubjectGroupId = "02",
                Code = "0216",
                Title = "تعهد ازدواج اول",
                State = "1",
                LegacyId = "0216"
            },
            new SignRequestSubject
            {
                Id = "0301",
                SignRequestSubjectGroupId = "03",
                Code = "0301",
                Title = "نمونه امضا",
                State = "1",
                LegacyId = "0301"
            },
            new SignRequestSubject
            {
                Id = "0401",
                SignRequestSubjectGroupId = "04",
                Code = "0401",
                Title = "استشهاد",
                State = "1",
                LegacyId = "0401"
            },
            new SignRequestSubject
            {
                Id = "0402",
                SignRequestSubjectGroupId = "04",
                Code = "0402",
                Title = "استشهاد انحصار وراثت",
                State = "1",
                LegacyId = "0402"
            },
            new SignRequestSubject
            {
                Id = "0403",
                SignRequestSubjectGroupId = "04",
                Code = "0403",
                Title = "استشهاد ثبت ملك",
                State = "1",
                LegacyId = "0403"
            },
            new SignRequestSubject
            {
                Id = "0404",
                SignRequestSubjectGroupId = "04",
                Code = "0404",
                Title = "استشهاد خدمت سربازی",
                State = "1",
                LegacyId = "0404"
            },
            new SignRequestSubject
            {
                Id = "0405",
                SignRequestSubjectGroupId = "04",
                Code = "0405",
                Title = "استشهاد فقدان سند مالكیت",
                State = "1",
                LegacyId = "0405"
            },
            new SignRequestSubject
            {
                Id = "0406",
                SignRequestSubjectGroupId = "04",
                Code = "0406",
                Title = "استشهاد نداشتن شغل و همسر",
                State = "1",
                LegacyId = "0406"
            },
            new SignRequestSubject
            {
                Id = "0407",
                SignRequestSubjectGroupId = "04",
                Code = "0407",
                Title = "استشهاد متصرف بودن ملك",
                State = "1",
                LegacyId = "0407"
            },
            new SignRequestSubject
            {
                Id = "0408",
                SignRequestSubjectGroupId = "04",
                Code = "0408",
                Title = "استشهاد مستاجر عادی بودن ملك",
                State = "1",
                LegacyId = "0408"
            },
            new SignRequestSubject
            {
                Id = "0409",
                SignRequestSubjectGroupId = "04",
                Code = "0409",
                Title = "استشهاد اعسار",
                State = "1",
                LegacyId = "0409"
            },
            new SignRequestSubject
            {
                Id = "0501",
                SignRequestSubjectGroupId = "05",
                Code = "0501",
                Title = "دادخواست",
                State = "1",
                LegacyId = "0501"
            },
            new SignRequestSubject
            {
                Id = "0601",
                SignRequestSubjectGroupId = "06",
                Code = "0601",
                Title = "اقرار",
                State = "1",
                LegacyId = "0601"
            },
            new SignRequestSubject
            {
                Id = "0602",
                SignRequestSubjectGroupId = "06",
                Code = "0602",
                Title = "اقرار شاغل نبودن و نگرفتن حقوق و مستمری",
                State = "1",
                LegacyId = "0602"
            },
            new SignRequestSubject
            {
                Id = "0603",
                SignRequestSubjectGroupId = "06",
                Code = "0603",
                Title = "اقرار شاغل نبودن در ادارات دولتی",
                State = "1",
                LegacyId = "0603"
            },
            new SignRequestSubject
            {
                Id = "0604",
                SignRequestSubjectGroupId = "06",
                Code = "0604",
                Title = "اقرار قطع ادعای مسكن مهر",
                State = "1",
                LegacyId = "0604"
            },
            new SignRequestSubject
            {
                Id = "0605",
                SignRequestSubjectGroupId = "06",
                Code = "0605",
                Title = "اقرار انصراف از گرفتن خودرو",
                State = "1",
                LegacyId = "0605"
            },
            new SignRequestSubject
            {
                Id = "0606",
                SignRequestSubjectGroupId = "06",
                Code = "0606",
                Title = "اقرار انصراف از مسكن مهر",
                State = "1",
                LegacyId = "0606"
            },
            new SignRequestSubject
            {
                Id = "0607",
                SignRequestSubjectGroupId = "06",
                Code = "0607",
                Title = "اقرار انصراف",
                State = "1",
                LegacyId = "0607"
            },
            new SignRequestSubject
            {
                Id = "0701",
                SignRequestSubjectGroupId = "07",
                Code = "0701",
                Title = "وكالت",
                State = "1",
                LegacyId = "0701"
            },
            new SignRequestSubject
            {
                Id = "0702",
                SignRequestSubjectGroupId = "07",
                Code = "0702",
                Title = "وكالت وكلای دادگستری",
                State = "1",
                LegacyId = "0702"
            },
            new SignRequestSubject
            {
                Id = "0801",
                SignRequestSubjectGroupId = "08",
                Code = "0801",
                Title = "درخواست",
                State = "1",
                LegacyId = "0801"
            },
            new SignRequestSubject
            {
                Id = "0802",
                SignRequestSubjectGroupId = "08",
                Code = "0802",
                Title = "درخواست كارت بازرگانی",
                State = "1",
                LegacyId = "0802"
            },
            new SignRequestSubject
            {
                Id = "0803",
                SignRequestSubjectGroupId = "08",
                Code = "0803",
                Title = "درخواست حفر چاه آب",
                State = "1",
                LegacyId = "0803"
            },
            new SignRequestSubject
            {
                Id = "0901",
                SignRequestSubjectGroupId = "09",
                Code = "0901",
                Title = "نامه",
                State = "1",
                LegacyId = "0901"
            },
            new SignRequestSubject
            {
                Id = "1001",
                SignRequestSubjectGroupId = "10",
                Code = "1001",
                Title = "اعتراض",
                State = "1",
                LegacyId = "1001"
            },
            new SignRequestSubject
            {
                Id = "1002",
                SignRequestSubjectGroupId = "10",
                Code = "1002",
                Title = "اعتراض to آرا",
                State = "1",
                LegacyId = "1002"
            },
            new SignRequestSubject
            {
                Id = "9901",
                SignRequestSubjectGroupId = "99",
                Code = "9901",
                Title = "سایر",
                State = "1",
                LegacyId = "9901"
            }
        };
        }
        public static List<SignRequestGetter> GetSignRequestGetters()
        {
            return new List<SignRequestGetter>
        {
            new SignRequestGetter
            {
                Id = "01",
                Code = "01",
                Title = "نیروی انتظامی",
                State = "1",
                LegacyId = "01"
            },
            new SignRequestGetter
            {
                Id = "02",
                Code = "02",
                Title = "شهرداری",
                State = "1",
                LegacyId = "02"
            },
            new SignRequestGetter
            {
                Id = "03",
                Code = "03",
                Title = "مراجع قضایی",
                State = "1",
                LegacyId = "03"
            },
            new SignRequestGetter
            {
                Id = "04",
                Code = "04",
                Title = "بانك",
                State = "1",
                LegacyId = "04"
            },
            new SignRequestGetter
            {
                Id = "05",
                Code = "05",
                Title = "بنیاد شهید و امور ایثارگران",
                State = "1",
                LegacyId = "05"
            },
            new SignRequestGetter
            {
                Id = "06",
                Code = "06",
                Title = "سازمان وظیفه عمومی",
                State = "1",
                LegacyId = "06"
            },
            new SignRequestGetter
            {
                Id = "07",
                Code = "07",
                Title = "سorganize امور مالیاتی",
                State = "1",
                LegacyId = "07"
            },
            new SignRequestGetter
            {
                Id = "08",
                Code = "08",
                Title = "كمیته امداد امام خمینی",
                State = "1",
                LegacyId = "08"
            },
            new SignRequestGetter
            {
                Id = "09",
                Code = "09",
                Title = "نظام مهندسی",
                State = "1",
                LegacyId = "09"
            },
            new SignRequestGetter
            {
                Id = "10",
                Code = "10",
                Title = "سازمان ثبت اسناد و املاك",
                State = "1",
                LegacyId = "10"
            },
            new SignRequestGetter
            {
                Id = "11",
                Code = "11",
                Title = "شركت خودرو سازی",
                State = "1",
                LegacyId = "11"
            },
            new SignRequestGetter
            {
                Id = "12",
                Code = "12",
                Title = "شركت لیزینگ",
                State = "1",
                LegacyId = "12"
            },
            new SignRequestGetter
            {
                Id = "13",
                Code = "13",
                Title = "كانون وكلای دادگستری",
                State = "1",
                LegacyId = "13"
            },
            new SignRequestGetter
            {
                Id = "14",
                Code = "14",
                Title = "كانون سردفتران و دفتریاران",
                State = "1",
                LegacyId = "14"
            },
            new SignRequestGetter
            {
                Id = "15",
                Code = "15",
                Title = "استانداری/فرمانداری",
                State = "1",
                LegacyId = "15"
            },
            new SignRequestGetter
            {
                Id = "16",
                Code = "16",
                Title = "سپاه",
                State = "1",
                LegacyId = "16"
            },
            new SignRequestGetter
            {
                Id = "17",
                Code = "17",
                Title = "ارتش",
                State = "1",
                LegacyId = "17"
            },
            new SignRequestGetter
            {
                Id = "18",
                Code = "18",
                Title = "سازمان ثبت احوال",
                State = "1",
                LegacyId = "18"
            },
            new SignRequestGetter
            {
                Id = "19",
                Code = "19",
                Title = "اصناف",
                State = "1",
                LegacyId = "19"
            },
            new SignRequestGetter
            {
                Id = "20",
                Code = "20",
                Title = "شركت بیمه",
                State = "1",
                LegacyId = "20"
            },
            new SignRequestGetter
            {
                Id = "21",
                Code = "21",
                Title = "سازمان تامین اجتماعی",
                State = "1",
                LegacyId = "21"
            },
            new SignRequestGetter
            {
                Id = "22",
                Code = "22",
                Title = "اداره كار و امور اجتماعی",
                State = "1",
                LegacyId = "22"
            },
            new SignRequestGetter
            {
                Id = "23",
                Code = "23",
                Title = "دیوان عدالت اداری",
                State = "1",
                LegacyId = "23"
            },
            new SignRequestGetter
            {
                Id = "24",
                Code = "24",
                Title = "اداره گاز",
                State = "1",
                LegacyId = "24"
            },
            new SignRequestGetter
            {
                Id = "25",
                Code = "25",
                Title = "اداره برق",
                State = "1",
                LegacyId = "25"
            },
            new SignRequestGetter
            {
                Id = "26",
                Code = "26",
                Title = "اداره آبفا",
                State = "1",
                LegacyId = "26"
            },
            new SignRequestGetter
            {
                Id = "27",
                Code = "27",
                Title = "اداره منابع آب",
                State = "1",
                LegacyId = "27"
            },
            new SignRequestGetter
            {
                Id = "28",
                Code = "28",
                Title = "اداره راهنمایی و رانندگی",
                State = "1",
                LegacyId = "28"
            },
            new SignRequestGetter
            {
                Id = "29",
                Code = "29",
                Title = "جهاد كشاورزی",
                State = "1",
                LegacyId = "29"
            },
            new SignRequestGetter
            {
                Id = "30",
                Code = "30",
                Title = "اداره منابع طبیعی و آبخیزداری",
                State = "1",
                LegacyId = "30"
            },
            new SignRequestGetter
            {
                Id = "31",
                Code = "31",
                Title = "وكیل دادگستری",
                State = "1",
                LegacyId = "31"
            },
            new SignRequestGetter
            {
                Id = "32",
                Code = "32",
                Title = "نظام پزشكی",
                State = "1",
                LegacyId = "32"
            },
            new SignRequestGetter
            {
                Id = "33",
                Code = "33",
                Title = "نظام پرستاری",
                State = "1",
                LegacyId = "33"
            },
            new SignRequestGetter
            {
                Id = "34",
                Code = "34",
                Title = "مراجع بهداشت و درمان",
                State = "1",
                LegacyId = "34"
            },
            new SignRequestGetter
            {
                Id = "99",
                Code = "99",
                Title = "سـایـر",
                State = "1",
                LegacyId = "99"
            }
        };
        }
        public static List<SignRequestSubjectGroup> GetSignRequestSubjectGroups()
        {
            return new List<SignRequestSubjectGroup>
        {
            new SignRequestSubjectGroup
            {
                Id = "01",
                Code = "01",
                Title = "رضایت",
                State = "1",
                LegacyId = "01"
            },
            new SignRequestSubjectGroup
            {
                Id = "02",
                Code = "02",
                Title = "تعهد",
                State = "1",
                LegacyId = "02"
            },
            new SignRequestSubjectGroup
            {
                Id = "03",
                Code = "03",
                Title = "نمونه امضا",
                State = "1",
                LegacyId = "03"
            },
            new SignRequestSubjectGroup
            {
                Id = "04",
                Code = "04",
                Title = "استشهاد",
                State = "1",
                LegacyId = "04"
            },
            new SignRequestSubjectGroup
            {
                Id = "05",
                Code = "05",
                Title = "دادخواست",
                State = "1",
                LegacyId = "05"
            },
            new SignRequestSubjectGroup
            {
                Id = "06",
                Code = "06",
                Title = "اقرار",
                State = "1",
                LegacyId = "06"
            },
            new SignRequestSubjectGroup
            {
                Id = "07",
                Code = "07",
                Title = "وكالت",
                State = "1",
                LegacyId = "07"
            },
            new SignRequestSubjectGroup
            {
                Id = "08",
                Code = "08",
                Title = "درخواست",
                State = "1",
                LegacyId = "08"
            },
            new SignRequestSubjectGroup
            {
                Id = "09",
                Code = "09",
                Title = "نامه",
                State = "1",
                LegacyId = "09"
            },
            new SignRequestSubjectGroup
            {
                Id = "10",
                Code = "10",
                Title = "اعتراض",
                State = "1",
                LegacyId = "10"
            },
            new SignRequestSubjectGroup
            {
                Id = "99",
                Code = "99",
                Title = "سایر",
                State = "1",
                LegacyId = "99"
            }
        };
        }
        public static List<SsrSignEbookBaseinfo> GetSsrSignEbookBaseinfoes()
        {
            return new List<SsrSignEbookBaseinfo>
        {
            new SsrSignEbookBaseinfo
            {
                Id = Guid.NewGuid(),
                ScriptoriumId = "57999",
                LastClassifyNo = 124,
                LastRegisterVolumeNo = "12",
                LastRegisterPaperNo = 5,
                NumberOfBooks = 15,
                ExordiumConfirmDate = "1402/10/10",
                ExordiumConfirmTime = "10:10",
                ExordiumDigitalSign = "d"
            },
            new SsrSignEbookBaseinfo
            {
                Id = Guid.NewGuid(),
                ScriptoriumId = "52539",
                LastClassifyNo = 134,
                LastRegisterVolumeNo = "22",
                LastRegisterPaperNo = 6,
                NumberOfBooks = 17,
                ExordiumConfirmDate = "1402/10/10",
                ExordiumConfirmTime = "10:10",
                ExordiumDigitalSign = "d"
            }
        };
        }

        public static List<AgentType> GetAgentTypes()
        {
            return new List<AgentType>
            {
                new AgentType
                {
                    Id = "6",
                    Code = "06",
                    Title = "متولي",
                    Adjective = "متولي",
                    Root = "متولي",
                    DocumentTitle = "مدرک",
                    State = "1",
                    LegacyId = "178126f8f460468882de769fca8c434c"
                },
                new AgentType
                {
                    Id = "7",
                    Code = "07",
                    Title = "قيم",
                    Adjective = "قيموميتاً",
                    Root = "قيموميت",
                    DocumentTitle = "قيم‌نامه",
                    State = "1",
                    LegacyId = "75279BC97D7343BBA90FC6052E7476A3"
                },
                new AgentType
                {
                    Id = "2",
                    Code = "02",
                    Title = "نماينده",
                    Adjective = "به نمايندگي",
                    Root = "نمايندگي",
                    DocumentTitle = "مدرک نمايندگي",
                    State = "1",
                    LegacyId = "68D4ED807A604C40908692375262DBFE"
                },
                new AgentType
                {
                    Id = "3",
                    Code = "03",
                    Title = "ولي",
                    Adjective = "ولايتاً",
                    Root = "ولايت",
                    DocumentTitle = "مدرک",
                    State = "1",
                    LegacyId = "527757eee0d747a69e8a3318cf731b01"
                },
                new AgentType
                {
                    Id = "5",
                    Code = "05",
                    Title = "قائم مقام",
                    Adjective = "به قائم مقامي",
                    Root = "قائم مقامي",
                    DocumentTitle = "مدرک",
                    State = "1",
                    LegacyId = "6835323387fa4f6381488011e45680bc"
                },
                new AgentType
                {
                    Id = "4",
                    Code = "04",
                    Title = "مدير",
                    Adjective = "به مديريت",
                    Root = "مديريت",
                    DocumentTitle = "مدرک",
                    State = "1",
                    LegacyId = "30eaeb789e254c95af5e1a5464f0ac47"
                },
                new AgentType
                {
                    Id = "1",
                    Code = "01",
                    Title = "وكيل",
                    Adjective = "وکالتاً",
                    Root = "وکالت",
                    DocumentTitle = "وکالت‌نامه",
                    State = "1",
                    LegacyId = "E74C3AAE62204ABF875146C9D82A954A"
                },
                new AgentType
                {
                    Id = "8",
                    Code = "08",
                    Title = "وارث",
                    Adjective = "وارث",
                    Root = "وراثت",
                    DocumentTitle = "گواهي حصر وراثت",
                    State = "2",
                    LegacyId = "9E30DAC89A0143D4A30AE3C9A686E5C8"
                },
                new AgentType
                {
                    Id = "9",
                    Code = "09",
                    Title = "مورث",
                    Adjective = "مورث",
                    Root = "وراثت",
                    DocumentTitle = "گواهي حصر وراثت",
                    State = "1",
                    LegacyId = "87B02FD7A1DB4ED49E8C10FBE9E884B8"
                },
                new AgentType
                {
                    Id = "15",
                    Code = "15",
                    Title = "شاهد",
                    Adjective = "با شهادت",
                    Root = "شهادت",
                    DocumentTitle = "شهادتنامه",
                    State = "2",
                    LegacyId = "8CFEE84F6AE64026B219EC9A6FD808EB"
                },
                new AgentType
                {
                    Id = "16",
                    Code = "16",
                    Title = "وصي",
                    Adjective = "با وصيت به",
                    Root = "وصيت",
                    DocumentTitle = "وصيتنامه",
                    State = "2",
                    LegacyId = "D10E127F4E2E484E9B4973AD9358B782"
                },
                new AgentType
                {
                    Id = "17",
                    Code = "17",
                    Title = "امين",
                    Adjective = "بعنوان امين",
                    Root = "امانت",
                    DocumentTitle = "مدرک",
                    State = "2",
                    LegacyId = "AA6EBFCE7DB841C1A7C99D9AB7574A96"
                },
                new AgentType
                {
                    Id = "10",
                    Code = "10",
                    Title = "معتمد",
                    Adjective = "معتمداً",
                    Root = "معتمديت",
                    DocumentTitle = "مدرک",
                    State = "1",
                    LegacyId = "DB3DBBA69326433E97C309E0C16A9EE7"
                },
                new AgentType
                {
                    Id = "11",
                    Code = "11",
                    Title = "معرف",
                    Adjective = "معرفاً",
                    Root = "معرفيت",
                    DocumentTitle = "مدرک",
                    State = "1",
                    LegacyId = "2868E199431A48418DE93A9FB4AB216B"
                },
                new AgentType
                {
                    Id = "12",
                    Code = "12",
                    Title = "مترجم",
                    Adjective = "مترجماً",
                    Root = "مترجميت",
                    DocumentTitle = "مدرک",
                    State = "1",
                    LegacyId = "EDFBB50DFB6C42E295A7A842C3D92D69"
                },
                new AgentType
                {
                    Id = "14",
                    Code = "14",
                    Title = "دادستان يا رئيس دادگاه بخش",
                    Adjective = "بعنوان دادستان يا نماينده وي",
                    Root = "نمايندگي",
                    DocumentTitle = "مدرک",
                    State = "1",
                    LegacyId = "10EFC6F267484B578E7A67AD6CEF774D"
                },
                new AgentType
                {
                    Id = "13",
                    Code = "13",
                    Title = "موصي",
                    Adjective = "وصيتاً",
                    Root = "وصايت",
                    DocumentTitle = "وصيتنامه",
                    State = "1",
                    LegacyId = "91FC82BEDDDD41528354C896FC36D35D"
                },
                new AgentType
                {
                    Id = "18",
                    Code = "18",
                    Title = "نماينده مقام قضايي",
                    Adjective = "مقام قضايي",
                    Root = "نمايندگي",
                    DocumentTitle = "مدرک",
                    State = "1",
                    LegacyId = "6CF1E7F1DAA446CD93816D2ED28F2300"
                }
            };
        }

        public static List<SignRequestFile> GetSignRequestFiles()
        {
            return new List<SignRequestFile>
            {
                new SignRequestFile
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                    SignRequestId = Guid.Parse("87c69e83-ad82-449c-a88d-815ceb340b33"),
                    ScanFile = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 },
                    ScanFileCreateDate = "1403/01/15",
                    ScanFileCreateTime = "10:30:45",
                    LastFile = new byte[] { 0x06, 0x07, 0x08, 0x09, 0x0A },
                    LastFileCreateDate = "1403/01/15",
                    LastFileCreateTime = "11:45:30",
                    ScanLegacyId = "SCAN_LEGACY_001",
                    LastLegacyId = "LAST_LEGACY_001",
                    ScriptoriumId = "12345",
                    RecordDate = new DateTime(2024, 4, 5),
                    Ilm = "1",
                    EdmId = "EDM_001",
                    EdmVersion = "1.0"
                },
                new SignRequestFile
                {
                    Id = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-345678901234"),
                    SignRequestId = Guid.Parse("62f720b6-c03b-4dfd-908e-cff35e8c6bed"),
                    ScanFile = new byte[] { 0x0B, 0x0C, 0x0D, 0x0E, 0x0F },
                    ScanFileCreateDate = "1403/02/20",
                    ScanFileCreateTime = "09:15:20",
                    LastFile = new byte[] { 0x10, 0x11, 0x12, 0x13, 0x14 },
                    LastFileCreateDate = "1403/02/20",
                    LastFileCreateTime = "10:25:35",
                    ScanLegacyId = "SCAN_LEGACY_002",
                    LastLegacyId = "LAST_LEGACY_002",
                    ScriptoriumId = "12345",
                    RecordDate = new DateTime(2024, 5, 10),
                    Ilm = "1",
                    EdmId = "EDM_002",
                    EdmVersion = "1.1"
                },
                new SignRequestFile
                {
                    Id = Guid.Parse("e5f6a7b8-c9d0-1234-efgh-567890123456"),
                    SignRequestId = Guid.Parse("0a190675-b674-46d3-8ad6-33736ff33bdc"),
                    ScanFile = new byte[] { 0x15, 0x16, 0x17, 0x18, 0x19 },
                    ScanFileCreateDate = "1403/03/25",
                    ScanFileCreateTime = "14:20:10",
                    LastFile = new byte[] { 0x1A, 0x1B, 0x1C, 0x1D, 0x1E },
                    LastFileCreateDate = "1403/03/25",
                    LastFileCreateTime = "15:40:25",
                    ScanLegacyId = "SCAN_LEGACY_003",
                    LastLegacyId = "LAST_LEGACY_003",
                    ScriptoriumId = "67890",
                    RecordDate = new DateTime(2024, 6, 15),
                    Ilm = "2",
                    EdmId = "EDM_003",
                    EdmVersion = "2.0"
                },
                new SignRequestFile
                {
                    Id = Guid.Parse("a7b8c9d0-e1f2-3456-ghij-789012345678"),
                    SignRequestId = Guid.Parse("7050d603-d96e-452f-9277-13ba0afb8000"),
                    ScanFile = new byte[] { 0x1F, 0x20, 0x21, 0x22, 0x23 },
                    ScanFileCreateDate = "1403/04/10",
                    ScanFileCreateTime = "08:45:55",
                    LastFile = new byte[] { 0x24, 0x25, 0x26, 0x27, 0x28 },
                    LastFileCreateDate = "1403/04/10",
                    LastFileCreateTime = "09:55:40",
                    ScanLegacyId = "SCAN_LEGACY_004",
                    LastLegacyId = "LAST_LEGACY_004",
                    ScriptoriumId = "67890",
                    RecordDate = new DateTime(2024, 7, 1),
                    Ilm = "1",
                    EdmId = "EDM_004",
                    EdmVersion = "1.5"
                },
                new SignRequestFile
                {
                    Id = Guid.Parse("c9d0e1f2-g3h4-5678-ijkl-901234567890"),
                    SignRequestId = Guid.Parse("597e2af5-43bc-40bc-83ec-6d233f04df26"),
                    ScanFile = new byte[] { 0x29, 0x2A, 0x2B, 0x2C, 0x2D },
                    ScanFileCreateDate = "1403/05/05",
                    ScanFileCreateTime = "16:10:30",
                    LastFile = new byte[] { 0x2E, 0x2F, 0x30, 0x31, 0x32 },
                    LastFileCreateDate = "1403/05/05",
                    LastFileCreateTime = "17:20:15",
                    ScanLegacyId = "SCAN_LEGACY_005",
                    LastLegacyId = "LAST_LEGACY_005",
                    ScriptoriumId = "54321",
                    RecordDate = new DateTime(2024, 8, 20),
                    Ilm = "3",
                    EdmId = null,
                    EdmVersion = null
                }
            };
        }
        public static List<SsrConfigMainSubject> GetMainSubjects()
        {
            return new List<SsrConfigMainSubject>
    {
        new SsrConfigMainSubject
        {
            Id =Guid.Parse("940E9F25411B1A06E0631314A8C06C8C"),
            Title = "ساعت کاری گواهی امضا",
            State = "1",
            Code = "SignRequest-WorkTime"
        },
        new SsrConfigMainSubject
        {
            Id = Guid.Parse("411B1A069F26940EE0631314A8C06C8C"),
            Title = "سرویس های گواهی امضا",
            State = "1",
            Code = "SignRequest-Service"
        },
        new SsrConfigMainSubject
        {
            Id = Guid.Parse("411B1A069F28940EE0631314A8C06C8C"),
            Title = "گردش کار گواهی امضا",
            State = "2",
            Code = "SignRequest-WorkFlow"
        },
        new SsrConfigMainSubject
        {
            Id = Guid.Parse("43275716B649371FE0631314A8C067A9"),
            Title = "مجوزهای کارکرد دفترخانه ها با بخش های مختلف سامانۀ جدید ثبت الکترونیک اسناد",
            State = "1",
            Code = "NewSystem-Scriptorium-Grants"
        }
    };
        }

        public static List<SsrConfigSubject> GetSubjects()
        {
            return new List<SsrConfigSubject>
    {
        new SsrConfigSubject
        {
            Id = Guid.Parse("411B1A069F30940EE0631314A8C06C8C"),
            SsrConfigMainSubjectId = Guid.Parse("940E9F25411B1A06E0631314A8C06C8C"),
            ConfigType = "2",
            Title = "ساعت کاری اخذ شناسه یکتا",
            ConfilictResolveType = "3",
            State = "1",
            Code = "SignRequest-NationalNo-WorkTime"
        },
        new SsrConfigSubject
        {
            Id = Guid.Parse("416401F74B9181AF8A662EF3B8BF8631"),
            SsrConfigMainSubjectId = Guid.Parse("940E9F25411B1A06E0631314A8C06C8C"),
            ConfigType = "2",
            Title = "ساعت کاری اخذ اثرانگشت",
            ConfilictResolveType = "3",
            State = "1",
            Code = "SignRequest-GetFingerprint-Worktime"
        },
        new SsrConfigSubject
        {
            Id = Guid.Parse("4A033922CCFD42CC962EDE9D5B0D302F"),
            SsrConfigMainSubjectId = Guid.Parse("411B1A069F26940EE0631314A8C06C8C"),
            ConfigType = "2",
            Title = "سرویس ثنا",
            ConfilictResolveType = "3",
            State = "1",
            Code = "SignRequest-Sana-Service"
        },
        new SsrConfigSubject
        {
            Id = Guid.Parse("40C4D71162ACDECB9396167A6E4E7939"),
            SsrConfigMainSubjectId = Guid.Parse("411B1A069F26940EE0631314A8C06C8C"),
            ConfigType = "2",
            Title = "سرویس شاهکار",
            ConfilictResolveType = "3",
            State = "1",
            Code = "SignRequest-Shahkar-Service"
        },
        new SsrConfigSubject
        {
            Id = Guid.Parse("4F79245F2CF348F8B8497561E18AE006"),
            SsrConfigMainSubjectId = Guid.Parse("411B1A069F26940EE0631314A8C06C8C"),
            ConfigType = "2",
            Title = "سرویس ثبت احوال",
            ConfilictResolveType = "3",
            State = "1",
            Code = "SignRequest-SabteAhval-Service"
        },
        new SsrConfigSubject
        {
            Id = Guid.Parse("4D0260F9B1947DF0BF0CF1C949F51879"),
            SsrConfigMainSubjectId = Guid.Parse("411B1A069F26940EE0631314A8C06C8C"),
            ConfigType = "2",
            Title = "سرویس احراز هویت دو عاملی",
            ConfilictResolveType = "3",
            State = "1",
            Code = "SignRequest-TFA-Service"
        },
        new SsrConfigSubject
        {
            Id = Guid.Parse("43275716B649371FE0631314A8C067A9"),
            SsrConfigMainSubjectId = Guid.Parse("43275716B649371FE0631314A8C067A9"),
            ConfigType = "2",
            Title = "مجوزهای کارکرد دفترخانه ها با بخش گواهی امضاء سامانۀ جدید ثبت الکترونیک اسناد",
            ConfilictResolveType = "3",
            State = "1",
            Code = "NewSystem-SignRequest-Scriptorium-Grants"
        }
    };
        }

        public static List<SsrConfig> GetConfigs()
        {
            return new List<SsrConfig>
    {
        new SsrConfig
        {
            Id = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            SsrConfigMainSubjectId = Guid.Parse("940E9F25411B1A06E0631314A8C06C8C"),
            SsrConfigSubjectId = Guid.Parse("416401F74B9181AF8A662EF3B8BF8631"),
            Value = null,
            ConfigStartDate = "1403/07/26",
            ConfigStartTime = "12:00",
            ConfigEndDate = "9999/12/29",
            ConfigEndTime = "23:59",
            ConditionType = "1",
            DocTypeCondition = "0",
            PersonTypeCondition = "0",
            AgentTypeCondition = "0",
            UnitCondition = "0",
            ScriptoriumCondition = "0",
            GeoCondition = "0",
            CostTypeCondition = "0",
            TimeCondition = "1",
            ActionType = "2",
            IsConfirmed = "1",
            Confirmer = "زهرا محبی",
            ConfirmDate = "1403/07/26",
            ConfirmTime = "15:00"
        },
        new SsrConfig
        {
            Id = Guid.Parse("3433E18967BB4D80BA2F7A6F165E028C"),
            SsrConfigMainSubjectId = Guid.Parse("411B1A069F26940EE0631314A8C06C8C"),
            SsrConfigSubjectId = Guid.Parse("40C4D71162ACDECB9396167A6E4E7939"),
            Value = null,
            ConfigStartDate = "1403/07/26",
            ConfigStartTime = "12:00",
            ConfigEndDate = "9999/12/29",
            ConfigEndTime = "23:59",
            ConditionType = "1",
            DocTypeCondition = "0",
            PersonTypeCondition = "0",
            AgentTypeCondition = "0",
            UnitCondition = "0",
            ScriptoriumCondition = "0",
            GeoCondition = "0",
            CostTypeCondition = "0",
            TimeCondition = "0",
            ActionType = "2",
            IsConfirmed = "1",
            Confirmer = "زهرا محبی",
            ConfirmDate = "1403/07/26",
            ConfirmTime = "15:00"
        },
        new SsrConfig
        {
            Id = Guid.Parse("E4BB679056554972880482CFBA699EAA"),
            SsrConfigMainSubjectId = Guid.Parse("411B1A069F26940EE0631314A8C06C8C"),
            SsrConfigSubjectId = Guid.Parse("4A033922CCFD42CC962EDE9D5B0D302F"),
            Value = null,
            ConfigStartDate = "1403/07/26",
            ConfigStartTime = "12:00",
            ConfigEndDate = "9999/12/29",
            ConfigEndTime = "23:59",
            ConditionType = "1",
            DocTypeCondition = "0",
            PersonTypeCondition = "0",
            AgentTypeCondition = "0",
            UnitCondition = "0",
            ScriptoriumCondition = "0",
            GeoCondition = "0",
            CostTypeCondition = "0",
            TimeCondition = "0",
            ActionType = "2",
            IsConfirmed = "1",
            Confirmer = "زهرا محبی",
            ConfirmDate = "1403/07/26",
            ConfirmTime = "15:00"
        },
        new SsrConfig
        {
            Id = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            SsrConfigMainSubjectId = Guid.Parse("940E9F25411B1A06E0631314A8C06C8C"),
            SsrConfigSubjectId = Guid.Parse("411B1A069F30940EE0631314A8C06C8C"),
            Value = null,
            ConfigStartDate = "1403/07/26",
            ConfigStartTime = "12:00",
            ConfigEndDate = "9999/12/29",
            ConfigEndTime = "23:59",
            ConditionType = "1",
            DocTypeCondition = "0",
            PersonTypeCondition = "0",
            AgentTypeCondition = "0",
            UnitCondition = "0",
            ScriptoriumCondition = "0",
            GeoCondition = "0",
            CostTypeCondition = "0",
            TimeCondition = "1",
            ActionType = "2",
            IsConfirmed = "1",
            Confirmer = "زهرا محبی",
            ConfirmDate = "1403/07/26",
            ConfirmTime = "15:00"
        },
        new SsrConfig
        {
            Id =Guid.Parse("A69B6F779FDB4FCB91B035C2ADF58353"),
            SsrConfigMainSubjectId = Guid.Parse("411B1A069F26940EE0631314A8C06C8C"),
            SsrConfigSubjectId = Guid.Parse("4D0260F9B1947DF0BF0CF1C949F51879"),
            Value = null,
            ConfigStartDate = "1403/07/26",
            ConfigStartTime = "12:00",
            ConfigEndDate = "9999/12/29",
            ConfigEndTime = "23:59",
            ConditionType = "1",
            DocTypeCondition = "0",
            PersonTypeCondition = "0",
            AgentTypeCondition = "0",
            UnitCondition = "0",
            ScriptoriumCondition = "0",
            GeoCondition = "0",
            CostTypeCondition = "0",
            TimeCondition = "0",
            ActionType = "2",
            IsConfirmed = "1",
            Confirmer = "زهرا محبی",
            ConfirmDate = "1403/07/26",
            ConfirmTime = "15:00"
        },
        new SsrConfig
        {
            Id = Guid.Parse("43275716B64D371FE0631314A8C067A9"),
            SsrConfigMainSubjectId = Guid.Parse("43275716B649371FE0631314A8C067A9"),
            SsrConfigSubjectId = Guid.Parse("43275716B649371FE0631314A8C067A9"),
            Value = null,
            ConfigStartDate = "1404/08/18",
            ConfigStartTime = "05:00",
            ConfigEndDate = "9999/12/29",
            ConfigEndTime = "23:59",
            ConditionType = "2",
            DocTypeCondition = "0",
            PersonTypeCondition = "0",
            AgentTypeCondition = "0",
            UnitCondition = "0",
            ScriptoriumCondition = "1",
            GeoCondition = "0",
            CostTypeCondition = "0",
            TimeCondition = "0",
            ActionType = "2",
            IsConfirmed = "1",
            Confirmer = "سازمان ثبت اسناد و املاک کشور",
            ConfirmDate = "1404/08/18",
            ConfirmTime = "05:00"
        }
    };
        }
        public static List<SsrConfigConditionScrptrm> GetConfigConditionScrptrm()
        {
            return new List<SsrConfigConditionScrptrm>
    {
        new SsrConfigConditionScrptrm
        {
            Id = Guid.Parse("43275716B64E371FE0631314A8C067A9"),
            SsrConfigId = Guid.Parse("43275716B64D371FE0631314A8C067A9"),
            ScriptoriumId = "57999"
        }
    };
        }
        public static List<SsrConfigConditionTime> GetConfigConditionTime()
        {
            return new List<SsrConfigConditionTime>
    {
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("D87459FD05BF4E31B13806B8817307D8"),
            SsrConfigId = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "1",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("4B10FCE315474B8592308C978D26F993"),
            SsrConfigId = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "2",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("2F4E1EC8CC264A2C86F041EF3E1CEB70"),
            SsrConfigId = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "3",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("5467862ED1C842E582DE581BA7587897"),
            SsrConfigId = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "4",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("E408ECE31ECA402C99E4E04F71B9E730"),
            SsrConfigId = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "5",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("25714F90E9444A2388AD1C76EF0E16E7"),
            SsrConfigId =Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "6",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("E56696B9C3074F3A8448A035E329E662"),
            SsrConfigId = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "7",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("14DE4466FE514A63B21C09E9B9B92241"),
            SsrConfigId = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "1",
            FromTime = "19:00",
            ToTime = "23:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("BD80C97A0EC7433A94B9C950B6B6A3C1"),
            SsrConfigId = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "2",
            FromTime = "19:00",
            ToTime = "23:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("A08B65FC355B4C28A3308E84A20C748B"),
            SsrConfigId = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "3",
            FromTime = "19:00",
            ToTime = "23:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("7C9D05D08A88458C93211FA0F6CB589D"),
            SsrConfigId = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "4",
            FromTime = "19:00",
            ToTime = "23:59"
        },
        new SsrConfigConditionTime
        {
            Id =Guid.Parse("168F4DECAD9C4E908B2909E4E2384EE8"),
            SsrConfigId = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "5",
            FromTime = "19:00",
            ToTime = "23:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("93AB990FB7DE473F8DCA30098F370E86"),
            SsrConfigId = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "6",
            FromTime = "19:00",
            ToTime = "23:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("E075E77EF5C14A2CB69928F6B545E3FA"),
            SsrConfigId = Guid.Parse("DD09DE455AFA48679C963A50BF4ADA1F"),
            DayOfWeek = "7",
            FromTime = "19:00",
            ToTime = "23:59"
        },
        // Second config group
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("ECABF09F9311450EAF9F2362467BC3F8"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "1",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("1766DC57B476450EA42393D5EB82AEEB"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "2",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("A9F22D45F1D548E796D66E6E19D120A1"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "3",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("597B7339113F4641A058DEBB4FC2C32D"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "4",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("AB687BB2F8E043F198A3B033823868F6"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "5",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("396B648BD99E495D948709D3E99442E3"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "6",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("51BFF4C63826473DAE1119E04A90490A"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "7",
            FromTime = "00:00",
            ToTime = "06:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("B1D83F1E6DE54B7EA33563600BB7B2AF"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "1",
            FromTime = "19:00",
            ToTime = "23:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("EEA876AE427149E6B372060242216293"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "2",
            FromTime = "19:00",
            ToTime = "23:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("B37798EFC0404E3F912581B8AD77F86D"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "3",
            FromTime = "19:00",
            ToTime = "23:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("0509FF3FDE0C4CD4A152E0D286CDA738"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "4",
            FromTime = "19:00",
            ToTime = "23:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("BE847899BA1A44A6ABE12A585F547817"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "5",
            FromTime = "19:00",
            ToTime = "23:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("F0A087A1592B4864B785BD80B5D6A863"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "6",
            FromTime = "19:00",
            ToTime = "23:59"
        },
        new SsrConfigConditionTime
        {
            Id = Guid.Parse("D3CEA9ADBA214915A8C8B9CED4BAFAA2"),
            SsrConfigId = Guid.Parse("C4C2DB6E2D564906B6E3B9D1BF45D5DE"),
            DayOfWeek = "7",
            FromTime = "19:00",
            ToTime = "23:59"
        }
    };
        }


    }
}


