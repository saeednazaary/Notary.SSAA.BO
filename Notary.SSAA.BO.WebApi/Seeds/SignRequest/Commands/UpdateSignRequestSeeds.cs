using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Commands
{
    public class UpdateSignRequestCommandGenerator
    {
        public List<UpdateSignRequestCommand> GenerateCommands()
        {
            var commands = new List<UpdateSignRequestCommand>();

            //گواهی امضا از قبل وجود دارد و شخص نیز دارد و 2 شخص جدید به آن اضافه میشود
            // --------- COMMAND 1 ----------
            var addPeopleWithExistingPeople = new UpdateSignRequestCommand
            {
                IsValid = true,
                IsNew = false,
                IsDirty = false,
                IsDelete = false,
                SignRequestSignText = "امضا انجام شد",
                SignRequestGetterId = new List<string> { "10" },
                SignRequestSubjectId = new List<string> { "0101" },
                SignRequestId = "62f720b6-c03b-4dfd-908e-cff35e8c6bed",
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
                    PersonId = "e731dbde-b900-4bcc-ae6d-3cfff332aa30",
                    SignRequestId ="62f720b6-c03b-4dfd-908e-cff35e8c6bed",
                    PersonClassifyNo = "72875",
                    PersonNationalNo = "1234567890",
                    PersonName = "محمد",
                    PersonFamily = "محمدی",
                    PersonBirthDate = "1360/01/01",
                    PersonFatherName = "حسن",
                    PersonIdentityNo = "1000001",
                    PersonIdentityIssueLocation = "تهران",
                    PersonSeri = "10",
                    PersonSerial = "500001",
                    PersonPostalCode = "1234567890",
                    PersonMobileNo = "09123456780",
                    PersonAddress = "آدرس نمونه در شهر تهران",
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
                    PersonAlphabetSeri = "1"
                },
                new SignRequestPersonViewModel
                {
                    IsValid = true,
                    IsNew = true,
                    IsDirty = false,
                    IsDelete = false,
                    IsLoaded = true,
                    RowNo = "2",
                    PersonState = "1",
                    IsPersonSabteAhvalChecked = true,
                    IsPersonSabteAhvalCorrect = true,
                    SignRequestId="62f720b6-c03b-4dfd-908e-cff35e8c6bed",
                    PersonId = "",
                    PersonClassifyNo = "72876",
                    PersonNationalNo = "1234567891",
                    PersonName = "زهرا",
                    PersonFamily = "رضایی",
                    PersonBirthDate = "1362/01/01",
                    PersonFatherName = "علی",
                    PersonIdentityNo = "2000001",
                    PersonIdentityIssueLocation = "مشهد",
                    PersonSeri = "20",
                    PersonSerial = "500002",
                    PersonPostalCode = "1234567891",
                    PersonMobileNo = "09123456781",
                    PersonAddress = "آدرس نمونه در شهر مشهد",
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
                    PersonAlphabetSeri = "2"
                }
            },
                SignRequestRelatedPersons = new List<ToRelatedPersonViewModel>
                {

                }
            };
            commands.Add(addPeopleWithExistingPeople);

            //گواهی امضا با وجود 2 شخص از قبل و ایجاد شخص وابسته
            // --------- COMMAND 2 ----------
            var addRelatedcmd = new UpdateSignRequestCommand
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
                    SignRequestId="0a190675-b674-46d3-8ad6-33736ff33bdc",
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
                    PersonAlphabetSeri = "1"
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
                    SignRequestId="0a190675-b674-46d3-8ad6-33736ff33bdc",
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
                    PersonAlphabetSeri = "2"
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
                    RelatedAgentTypeTitle = "وکیل"
                }
            }
            };
            commands.Add(addRelatedcmd);

            //گواهی امضا از قبل وجود دارد و شخصی دارد و 2یک شخص آن اصلاح میشود
            //مرحله 1-1
            // --------- COMMAND 3 ----------
            var addPeoplecmd = new UpdateSignRequestCommand
            {
                IsValid = true,
                IsNew = false,
                IsDirty = false,
                IsDelete = false,
                SignRequestSignText = "امضا انجام شد",
                SignRequestGetterId = new List<string> { "12" },
                SignRequestSubjectId = new List<string> { "0105" },
                SignRequestId = "62f720b6-c03b-4dfd-908e-cff35e8c6bed",
                SignRequestPersons = new List<SignRequestPersonViewModel>
    {
        new SignRequestPersonViewModel
        {
            IsValid = true,
            IsNew = false,
            IsDirty = false,
            IsDelete = false,
            IsLoaded = true,
            RowNo = "1",
            PersonState = "1",
            IsPersonSabteAhvalChecked = true,
            IsPersonSabteAhvalCorrect = true,
            PersonId = "e731dbde-b900-4bcc-ae6d-3cfff332aa30",
            SignRequestId = "62f720b6-c03b-4dfd-908e-cff35e8c6bed",
            PersonClassifyNo = "72875",
            PersonNationalNo = "1234567890",
            PersonName = "محمد",
            PersonFamily = "محمدی",
            PersonBirthDate = "1360/01/01",
            PersonFatherName = "حسن",
            PersonIdentityNo = "1000001",
            PersonIdentityIssueLocation = "تهران",
            PersonSeri = "10",
            PersonSerial = "500001",
            PersonPostalCode = "1234567890",
            PersonMobileNo = "09123456780",
            PersonAddress = "آدرس نمونه در شهر تهران",
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
            PersonAlphabetSeri = "1"
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
            PersonId = "870ec471-7e8e-4d50-a53e-b9855471750f",
            SignRequestId = "62f720b6-c03b-4dfd-908e-cff35e8c6bed",
            PersonClassifyNo = "72876",
            PersonNationalNo = "1234567891",
            PersonName = "زهرا",
            PersonFamily = "رضایی",
            PersonBirthDate = "1362/01/01",
            PersonFatherName = "علی",
            PersonIdentityNo = "2000001",
            PersonIdentityIssueLocation = "مشهد",
            PersonSeri = "20",
            PersonSerial = "500002",
            PersonPostalCode = "1234567891",
            PersonMobileNo = "09123456781",
            PersonAddress = "آدرس نمونه در شهر مشهد",
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
            PersonAlphabetSeri = "2"
        },
        new SignRequestPersonViewModel
        {
            IsValid = true,
            IsNew = false,
            IsDirty = true,
            IsDelete = false,
            IsLoaded = true,
            RowNo = "3",
            PersonState = "1",
            IsPersonSabteAhvalChecked = true,
            IsPersonSabteAhvalCorrect = true,
            PersonId = "f98ddfac-81fc-431a-b9ae-8f1e8be58e40",
            SignRequestId = "62f720b6-c03b-4dfd-908e-cff35e8c6bed",
            PersonClassifyNo = "72877",
            PersonNationalNo = "1234567892",
            PersonName = "مریم",
            PersonFamily = " پور جعفری",
            PersonBirthDate = "1365/01/01",
            PersonFatherName = "رضا",
            PersonIdentityNo = "3000001",
            PersonIdentityIssueLocation = "اصفهان",
            PersonSeri = "30",
            PersonSerial = "500003",
            PersonPostalCode = "1234567892",
            PersonMobileNo = "09123456782",
            PersonAddress = "آدرس نمونه در شهر اصفهان",
            PersonSexType = "1",
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
            PersonAlphabetSeri = "2"
        }
    },
                SignRequestRelatedPersons = new List<ToRelatedPersonViewModel>
    {
        new ToRelatedPersonViewModel
        {
            IsValid = true,
            IsNew = false,
            IsDirty = false,
            IsDelete = false,
            IsLoaded = true,
            RowNo = "1",
            RelatedPersonId = "6fc3ac49-5dc3-4dc2-8cd7-39e1a48a7dbd",
            RelatedState = "1",
            MainPersonId = new List<string> { "e731dbde-b900-4bcc-ae6d-3cfff332aa30" },
            RelatedAgentPersonId = new List<string> { "870ec471-7e8e-4d50-a53e-b9855471750f" },
            RelatedAgentTypeId = new List<string> { "1" },
            RelatedAgentDocumentNo = "1234",
            RelatedAgentDocumentDate = "1404/01/01",
            RelatedAgentDocumentIssuer = "تهران",
            RelatedReliablePersonReasonId = new List<string> { "1" },
            RelatedPersonDescription = "رابطه وکالت تنظیم شده",
            RelatedAgentTypeTitle = "وکیل"
        }
    }
            }; commands.Add(addPeoplecmd);

            //حذف شخص بدون وابستگی 
            // --------- COMMAND 4 ----------
            var cmd4 = new UpdateSignRequestCommand
            {
                IsValid = true,
                IsNew = false,
                IsDirty = false,
                IsDelete = false,
                SignRequestSignText = "امضا انجام شد",
                SignRequestGetterId = new List<string> { "13" },
                SignRequestSubjectId = new List<string> { "0104" },
                SignRequestId = "7050d603-d96e-452f-9277-13ba0afb8000",
                SignRequestPersons = new List<SignRequestPersonViewModel>
            {
                    new SignRequestPersonViewModel
                    {
                    IsValid = true,
                    IsNew = false,
                    IsDirty = true,
                    IsDelete = true,
                    IsLoaded = true,
                    RowNo = "1",
                    PersonState = "1",
                    IsPersonSabteAhvalChecked = true,
                    IsPersonSabteAhvalCorrect = true,
                    PersonId = "3b917530-1f05-4e0e-8d71-77fd05126758", 
                    SignRequestId="7050d603-d96e-452f-9277-13ba0afb8000",
                    PersonClassifyNo = "72877",
                    PersonNationalNo = "3234567890",
                    PersonName = "امیر",
                    PersonFamily = "کاظمی",
                    PersonBirthDate = "1362/01/01",
                    PersonFatherName = "مهدی",
                    PersonIdentityNo = "1000003",
                    PersonIdentityIssueLocation = "اهواز",
                    PersonSeri = "12",
                    PersonSerial = "500003",
                    PersonPostalCode = "3234567890",
                    PersonMobileNo = "09123456786",
                    PersonAddress = "آدرس نمونه در شهر اهواز",
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
                    PersonAlphabetSeri = "1"
                    },
                new SignRequestPersonViewModel
                {
                    IsValid = true,
                    IsNew = true,
                    IsDirty = false,
                    IsDelete = false,
                    IsLoaded = true,
                    RowNo = "2",
                    PersonState = "1",
                    IsPersonSabteAhvalChecked = true,
                    IsPersonSabteAhvalCorrect = true,
                    PersonId ="",
                    PersonClassifyNo = "72879",
                    PersonNationalNo = "4234567891",
                    PersonName = "سمیه",
                    PersonFamily = "حسینی",
                    PersonBirthDate = "1365/01/01",
                    PersonFatherName = "رضا",
                    PersonIdentityNo = "2000004",
                    PersonIdentityIssueLocation = "ارومیه",
                    PersonSeri = "23",
                    PersonSerial = "500005",
                    PersonPostalCode = "4234567891",
                    PersonMobileNo = "09123456790",
                    PersonAddress = "آدرس نمونه در شهر ارومیه",
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
                    PersonAlphabetSeri = "2"
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
                    RelatedPersonId = Guid.NewGuid().ToString(),
                    RelatedState = "1",
                    MainPersonId = new List<string> { commands[3]?.SignRequestPersons[0]?.PersonId },
                    RelatedAgentPersonId = new List<string> { commands[3]?.SignRequestPersons[1]?.PersonId },
                    RelatedAgentTypeId = new List<string> { "01" },
                    RelatedAgentDocumentNo = "4234",
                    RelatedAgentDocumentDate = "1404/01/04",
                    RelatedAgentDocumentIssuer = "یزد",
                    RelatedReliablePersonReasonId = new List<string> { "1" },
                    RelatedPersonDescription = "رابطه وکالت تنظیم شده",
                    RelatedAgentTypeTitle = "وکیل"
                }
            }
            };
            commands.Add(cmd4);

            // --------- COMMAND 5 ----------
            var cmd5 = new UpdateSignRequestCommand
            {
                IsValid = true,
                IsNew = false,
                IsDirty = false,
                IsDelete = false,
                SignRequestSignText = "امضا انجام شد",
                SignRequestGetterId = new List<string> { "14" },
                SignRequestSubjectId = new List<string> { "0104" },
                SignRequestId = Guid.NewGuid().ToString(),
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
                    PersonId = Guid.NewGuid().ToString(),
                    PersonClassifyNo = "72879",
                    PersonNationalNo = "5234567890",
                    PersonName = "مجید",
                    PersonFamily = "جعفری",
                    PersonBirthDate = "1364/01/01",
                    PersonFatherName = "حسین",
                    PersonIdentityNo = "1000005",
                    PersonIdentityIssueLocation = "کرمان",
                    PersonSeri = "14",
                    PersonSerial = "500005",
                    PersonPostalCode = "5234567890",
                    PersonMobileNo = "09123456792",
                    PersonAddress = "آدرس نمونه در شهر کرمان",
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
                    PersonAlphabetSeri = "1"
                }
            },
                SignRequestRelatedPersons = new List<ToRelatedPersonViewModel>()
            };
            commands.Add(cmd5);

            // --------- COMMAND 6 ----------
            var cmd6 = new UpdateSignRequestCommand
            {
                IsValid = true,
                IsNew = false,
                IsDirty = false,
                IsDelete = false,
                SignRequestSignText = "امضا انجام شد",
                SignRequestGetterId = new List<string> { "15" },
                SignRequestSubjectId = new List<string> { "0108" },
                SignRequestId = Guid.NewGuid().ToString(),
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
                    PersonId = Guid.NewGuid().ToString(),
                    PersonClassifyNo = "72880",
                    PersonNationalNo = "6234567890",
                    PersonName = "سعید",
                    PersonFamily = "محمدی",
                    PersonBirthDate = "1365/01/01",
                    PersonFatherName = "علی",
                    PersonIdentityNo = "1000006",
                    PersonIdentityIssueLocation = "زاهدان",
                    PersonSeri = "15",
                    PersonSerial = "500006",
                    PersonPostalCode = "6234567890",
                    PersonMobileNo = "09123456795",
                    PersonAddress = "آدرس نمونه در شهر زاهدان",
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
                    PersonAlphabetSeri = "1"
                },
                new SignRequestPersonViewModel
                {
                    IsValid = true,
                    IsNew = true,
                    IsDirty = false,
                    IsDelete = false,
                    IsLoaded = true,
                    RowNo = "2",
                    PersonState = "1",
                    IsPersonSabteAhvalChecked = true,
                    IsPersonSabteAhvalCorrect = true,
                    PersonId = Guid.NewGuid().ToString(),
                    PersonClassifyNo = "72881",
                    PersonNationalNo = "6234567891",
                    PersonName = "فاطمه",
                    PersonFamily = "حسنی",
                    PersonBirthDate = "1367/01/01",
                    PersonFatherName = "محمدرضا",
                    PersonIdentityNo = "2000006",
                    PersonIdentityIssueLocation = "گرگان",
                    PersonSeri = "25",
                    PersonSerial = "500007",
                    PersonPostalCode = "6234567891",
                    PersonMobileNo = "09123456796",
                    PersonAddress = "آدرس نمونه در شهر گرگان",
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
                    PersonAlphabetSeri = "2"
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
                    RelatedPersonId = Guid.NewGuid().ToString(),
                    RelatedState = "1",
                    MainPersonId = new List<string> { commands[5]?.SignRequestPersons[0]?.PersonId },
                    RelatedAgentPersonId = new List<string> { commands[5]?.SignRequestPersons[1]?.PersonId },
                    RelatedAgentTypeId = new List<string> { "01" },
                    RelatedAgentDocumentNo = "6234",
                    RelatedAgentDocumentDate = "1404/01/06",
                    RelatedAgentDocumentIssuer = "زاهدان",
                    RelatedReliablePersonReasonId = new List<string> { "1" },
                    RelatedPersonDescription = "رابطه وکالت تنظیم شده",
                    RelatedAgentTypeTitle = "وکیل"
                }
            }
            };
            commands.Add(cmd6);

            // --------- COMMAND 7 ----------
            var cmd7 = new UpdateSignRequestCommand
            {
                IsValid = true,
                IsNew = false,
                IsDirty = false,
                IsDelete = false,
                SignRequestSignText = "امضا انجام شد",
                SignRequestGetterId = new List<string> { "16" },
                SignRequestSubjectId = new List<string> { "0108" },
                SignRequestId = Guid.NewGuid().ToString(),
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
                    PersonId = Guid.NewGuid().ToString(),
                    PersonClassifyNo = "72881",
                    PersonNationalNo = "7234567890",
                    PersonName = "حمید",
                    PersonFamily = "رضایی",
                    PersonBirthDate = "1366/01/01",
                    PersonFatherName = "مهدی",
                    PersonIdentityNo = "1000007",
                    PersonIdentityIssueLocation = "بوشهر",
                    PersonSeri = "16",
                    PersonSerial = "500007",
                    PersonPostalCode = "7234567890",
                    PersonMobileNo = "09123456798",
                    PersonAddress = "آدرس نمونه در شهر بوشهر",
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
                    PersonAlphabetSeri = "1"
                }
            },
                SignRequestRelatedPersons = new List<ToRelatedPersonViewModel>()
            };
            commands.Add(cmd7);

            // --------- COMMAND 8 ----------
            var cmd8 = new UpdateSignRequestCommand
            {
                IsValid = true,
                IsNew = false,
                IsDirty = false,
                IsDelete = false,
                SignRequestSignText = "امضا انجام شد",
                SignRequestGetterId = new List<string> { "17" },
                SignRequestSubjectId = new List<string> { "0109" },
                SignRequestId = Guid.NewGuid().ToString(),
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
                    PersonId = Guid.NewGuid().ToString(),
                    PersonClassifyNo = "72882",
                    PersonNationalNo = "8234567890",
                    PersonName = "علیرضا",
                    PersonFamily = "کرمی",
                    PersonBirthDate = "1367/01/01",
                    PersonFatherName = "محمدعلی",
                    PersonIdentityNo = "1000008",
                    PersonIdentityIssueLocation = "اراک",
                    PersonSeri = "17",
                    PersonSerial = "500008",
                    PersonPostalCode = "8234567890",
                    PersonMobileNo = "09123456801",
                    PersonAddress = "آدرس نمونه در شهر اراک",
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
                    PersonAlphabetSeri = "1"
                },
                new SignRequestPersonViewModel
                {
                    IsValid = true,
                    IsNew = true,
                    IsDirty = false,
                    IsDelete = false,
                    IsLoaded = true,
                    RowNo = "2",
                    PersonState = "1",
                    IsPersonSabteAhvalChecked = true,
                    IsPersonSabteAhvalCorrect = true,
                    PersonId = Guid.NewGuid().ToString(),
                    PersonClassifyNo = "72883",
                    PersonNationalNo = "8234567891",
                    PersonName = "معصومه",
                    PersonFamily = "نظری",
                    PersonBirthDate = "1369/01/01",
                    PersonFatherName = "رحمان",
                    PersonIdentityNo = "2000008",
                    PersonIdentityIssueLocation = "یزد",
                    PersonSeri = "27",
                    PersonSerial = "500009",
                    PersonPostalCode = "8234567891",
                    PersonMobileNo = "09123456802",
                    PersonAddress = "آدرس نمونه در شهر یزد",
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
                    PersonAlphabetSeri = "2"
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
                    RelatedPersonId = Guid.NewGuid().ToString(),
                    RelatedState = "1",
                    MainPersonId = new List<string> { commands[7]?.SignRequestPersons[0]?.PersonId },
                    RelatedAgentPersonId = new List<string> { commands[7]?.SignRequestPersons[1]?.PersonId },
                    RelatedAgentTypeId = new List<string> { "01" },
                    RelatedAgentDocumentNo = "8234",
                    RelatedAgentDocumentDate = "1404/01/08",
                    RelatedAgentDocumentIssuer = "اراک",
                    RelatedReliablePersonReasonId = new List<string> { "1" },
                    RelatedPersonDescription = "رابطه وکالت تنظیم شده",
                    RelatedAgentTypeTitle = "وکیل"
                }
            }
            };
            commands.Add(cmd8);

            // --------- COMMAND 9 ----------
            var cmd9 = new UpdateSignRequestCommand
            {
                IsValid = true,
                IsNew = false,
                IsDirty = false,
                IsDelete = false,
                SignRequestSignText = "امضا انجام شد",
                SignRequestGetterId = new List<string> { "18" },
                SignRequestSubjectId = new List<string> { "0201" },
                SignRequestId = Guid.NewGuid().ToString(),
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
                    PersonId = Guid.NewGuid().ToString(),
                    PersonClassifyNo = "72883",
                    PersonNationalNo = "9234567890",
                    PersonName = "محمدرضا",
                    PersonFamily = "جعفری",
                    PersonBirthDate = "1368/01/01",
                    PersonFatherName = "عباس",
                    PersonIdentityNo = "1000009",
                    PersonIdentityIssueLocation = "بجنورد",
                    PersonSeri = "18",
                    PersonSerial = "500009",
                    PersonPostalCode = "9234567890",
                    PersonMobileNo = "09123456804",
                    PersonAddress = "آدرس نمونه در شهر بجنورد",
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
                    PersonAlphabetSeri = "1"
                }
            },
                SignRequestRelatedPersons = new List<ToRelatedPersonViewModel>()
            };
            commands.Add(cmd9);

            // --------- COMMAND 10 ----------
            var cmd10 = new UpdateSignRequestCommand
            {
                IsValid = true,
                IsNew = false,
                IsDirty = false,
                IsDelete = false,
                SignRequestSignText = "امضا انجام شد",
                SignRequestGetterId = new List<string> { "19" },
                SignRequestSubjectId = new List<string> { "0202" },
                SignRequestId = Guid.NewGuid().ToString(),
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
                    PersonId = Guid.NewGuid().ToString(),
                    PersonClassifyNo = "72884",
                    PersonNationalNo = "0234567890",
                    PersonName = "امیرحسین",
                    PersonFamily = "مهدوی",
                    PersonBirthDate = "1369/01/01",
                    PersonFatherName = "محمد",
                    PersonIdentityNo = "1000010",
                    PersonIdentityIssueLocation = "خرم آباد",
                    PersonSeri = "19",
                    PersonSerial = "500010",
                    PersonPostalCode = "0234567890",
                    PersonMobileNo = "09123456807",
                    PersonAddress = "آدرس نمونه در شهر خرم آباد",
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
                    PersonAlphabetSeri = "1"
                },
                new SignRequestPersonViewModel
                {
                    IsValid = true,
                    IsNew = true,
                    IsDirty = false,
                    IsDelete = false,
                    IsLoaded = true,
                    RowNo = "2",
                    PersonState = "1",
                    IsPersonSabteAhvalChecked = true,
                    IsPersonSabteAhvalCorrect = true,
                    PersonId = Guid.NewGuid().ToString(),
                    PersonClassifyNo = "72885",
                    PersonNationalNo = "0234567891",
                    PersonName = "مهین",
                    PersonFamily = "سعادت",
                    PersonBirthDate = "1371/01/01",
                    PersonFatherName = "حسین",
                    PersonIdentityNo = "2000010",
                    PersonIdentityIssueLocation = "بیرجند",
                    PersonSeri = "29",
                    PersonSerial = "500011",
                    PersonPostalCode = "0234567891",
                    PersonMobileNo = "09123456808",
                    PersonAddress = "آدرس نمونه در شهر بیرجند",
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
                    PersonAlphabetSeri = "2"
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
                    RelatedPersonId = Guid.NewGuid().ToString(),
                    RelatedState = "1",
                    MainPersonId = new List<string> { commands[9]?.SignRequestPersons[0]?.PersonId },
                    RelatedAgentPersonId = new List<string> { commands[9]?.SignRequestPersons[1]?.PersonId },
                    RelatedAgentTypeId = new List<string> { "01" },
                    RelatedAgentDocumentNo = "0234",
                    RelatedAgentDocumentDate = "1404/01/10",
                    RelatedAgentDocumentIssuer = "خرم آباد",
                    RelatedReliablePersonReasonId = new List<string> { "1" },
                    RelatedPersonDescription = "رابطه وکالت تنظیم شده",
                    RelatedAgentTypeTitle = "وکیل"
                }
            }
            };
            commands.Add(cmd10);

            return commands;
        }
    }
}
