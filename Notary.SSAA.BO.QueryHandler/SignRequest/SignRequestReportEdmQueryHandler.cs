
using Mapster;
using MediatR;
using Newtonsoft.Json;
using Notary.SSAA.BO.DataTransferObject.Queries.EDM;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using SNotary.SSAA.BO.DataTransferObject.ServiceInputs.Edm;
using SSAA.Notary.DataTransferObject.Commands.SignRequest;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    internal class SignRequestReportEdmQueryHandler : BaseQueryHandler<SignRequestReportEdmQuery, ApiResult>
    {
        private readonly ISignRequestRepository _signRequestRepository;
        private ApiResult _apiResult;
        public SignRequestReportEdmQueryHandler(IMediator mediator, IUserService userService, ISignRequestRepository signRequestRepository) : base(mediator, userService)
        {
            _signRequestRepository = signRequestRepository;
            _apiResult = new();
        }

        protected override bool HasAccess(SignRequestReportEdmQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult> RunAsync(SignRequestReportEdmQuery request, CancellationToken cancellationToken)
        {
            var signRequest = await _signRequestRepository.GetKatebSignRequesById(request.SignRequestId, cancellationToken);
            if (signRequest == null)
            {
                _apiResult.IsSuccess = false;
                _apiResult.message.Add("گواهی امضا یافت نشد.");
                return _apiResult;
            }
            string ScriptoriumAddress = string.Empty;
            string ScriptoriumTell = string.Empty;
            string ScriptoriumLocation = string.Empty;
            string[] idList = new string[] { signRequest.ScriptoriumId };
            ScriptoriumInput scriptoriumInput = new ScriptoriumInput(idList);
            ApiResult<ScriptoriumViewModel> scriptoriumResponse = await _mediator.Send(scriptoriumInput, cancellationToken);

            if (scriptoriumResponse.IsSuccess)
            {
                ScriptoriumLocation = scriptoriumResponse.Data.ScriptoriumList.First().GeoLocationName.Replace("ء", "ی");
                ScriptoriumAddress = " نشانی دفترخانه: " + scriptoriumResponse.Data.ScriptoriumList.First().Address.Replace("ء", "ی");
                ScriptoriumTell = scriptoriumResponse.Data.ScriptoriumList.First().Tel;
            }
            else
            {
                _apiResult.IsSuccess = false;
                _apiResult.message.Add("سرویس اطلاعات پایه با خطا مواجه شد . ");
                return _apiResult;
            }
            SignRequestEdmInputService inputService = new();

            inputService.document = new()
            {
                authenticationCode = signRequest.SecretCode != null ? signRequest.SecretCode : signRequest.ReqNo,
                documentLifeCycleStatus = DocumentTransacionType.issue.GetEnumDescription(),
                documentType = EdmDocmentType.SignDocument.ToInt32().ToString(),
                documentCode = signRequest.NationalNo != null ? signRequest.NationalNo : signRequest.ReqNo,
                description = signRequest.Description != null ? signRequest.Description : "چاپ گواهی امضا",
                certificate = new List<Certificate>(),
                documentLifeCycle = new List<DocumentLifeCycle>(),
                documentPart = new List<DocumentPart>(),
            };
            Certificate certificate = new Certificate();
            certificate.certificateType = EdmCertificateType.Signertificate.ToInt32().ToString();
            certificate.certificateSubjectCode = signRequest.SignRequestSubject != null ? signRequest.SignRequestSubject.Code : null;
            inputService.document.certificate.Add(certificate);

            DocumentLifeCycle documentLifeCycle = new DocumentLifeCycle();
            documentLifeCycle.userTitle = _userService.UserApplicationContext.User.Name + " " + _userService.UserApplicationContext.User.Family;
            documentLifeCycle.documentTransactionType = DocumentTransacionType.issue.GetEnumDescription();
            documentLifeCycle.transactionDate = new SimpleDate()
            {
                year = signRequest.ConfirmDate != null ? signRequest.ConfirmDate.Substring(0, 4).ToInt() : signRequest.ReqDate.Substring(0, 4).ToInt(),
                month = signRequest.ConfirmDate != null ? signRequest.ConfirmDate.Substring(5, 2).ToInt() : signRequest.ReqDate.Substring(5, 2).ToInt(),
                day = signRequest.ConfirmDate != null ? signRequest.ConfirmDate.Substring(8, 2).ToInt() : signRequest.ReqDate.Substring(8, 2).ToInt(),
            };
            inputService.document.documentLifeCycle.Add(documentLifeCycle);

            DocumentPart documentPartScrip = new();
            documentPartScrip.documentPartType = DocumentPartType.scriptorium.ToString();
            documentPartScrip.documentApplication = new List<DocumentApplication>();
            DocumentApplication docApplication = new();
            docApplication.title = DocumentPartType.scriptorium.GetEnumDescription();
            docApplication.documentApplicationType = DocumentPartType.scriptorium.ToString();
            docApplication.organizationUnit = new OrganizationUnit()
            {
                contactMechanismApplication = new List<ContactMechanismApplication>(),
                geographyBoundry = new(),
                unitCode = _userService.UserApplicationContext.BranchAccess.BranchCode,
                unitId = _userService.UserApplicationContext.BranchAccess.BranchCode,
                unitName = _userService.UserApplicationContext.BranchAccess.BranchName,
                unitTitle = _userService.UserApplicationContext.BranchAccess.BranchAccessDisplayName,
                unitType = EdmUnitType.scriptorium.ToString(),
            };
            ContactMechanismApplication contactMechanismApplicationPost = new();

            contactMechanismApplicationPost.contactMechanismApplicationType = ContactMechanismType.postalAddress.ToString();
            contactMechanismApplicationPost.contactMechanism = new()
            {
                postalAddress = new PostalAddress()
                {
                    postalAddressPart = new List<PostalAddressPart>(),
                }
            };


            PostalAddressPart postalCodePart = new();
            postalCodePart.postalAddressPartType = PostalAddressPartType.postalCode.ToString();
            postalCodePart.postalAddressPartValue = "-";
            contactMechanismApplicationPost.contactMechanism.postalAddress.postalAddressPart.Add(postalCodePart);

            PostalAddressPart postalAddressPart = new();

            postalAddressPart.postalAddressPartType = PostalAddressPartType.addressText.ToString();
            postalAddressPart.postalAddressPartValue = ScriptoriumAddress;
            contactMechanismApplicationPost.contactMechanism.postalAddress.postalAddressPart.Add(postalAddressPart);

            docApplication.organizationUnit.contactMechanismApplication.Add(contactMechanismApplicationPost);
            if (ScriptoriumTell != null)
            {
                ContactMechanismApplication contactMechanismApplicationTel = new();
                contactMechanismApplicationTel.contactMechanismApplicationType = ContactMechanismType.telecommunication.ToString();
                contactMechanismApplicationTel.contactMechanism.telecommunicationNumber = new List<TelecomminucationNumber>()
                {
                     new TelecomminucationNumber()
                        {
                            phoneNumber = ScriptoriumTell,
                            telecommunicationType = TeleComminucationType.telephone.ToString(),
                        }
                };
                docApplication.organizationUnit.contactMechanismApplication.Add(contactMechanismApplicationTel);
            }


            documentPartScrip.documentApplication.Add(docApplication);
            inputService.document.documentPart.Add(documentPartScrip);


            DocumentPart documentPartRole = new();
            documentPartRole.documentPartType = DocumentPartType.documentRole.ToString();
            documentPartRole.documentApplication = new List<DocumentApplication>();
            DocumentApplication documentApplicationPartRole = new();
            documentApplicationPartRole.title = DocumentPartType.documentRole.GetEnumDescription();
            documentApplicationPartRole.documentApplicationType = DocumentPartType.documentRole.ToString();
            documentApplicationPartRole.party = new List<Party>();
            Party partyRole = new Party();
            partyRole.partyType = PartyType.organization.ToString();
            partyRole.partyId = signRequest.SignRequestGetterId;
            partyRole.partyRole = new List<PartyRole>();
            PartyRole roletype = new PartyRole()
            {
                partyRoleType = PartyRoleType.verifier.ToString(),
            };
            partyRole.partyRole.Add(roletype);
            documentApplicationPartRole.party.Add(partyRole);
            documentPartRole.documentApplication.Add(documentApplicationPartRole);
            inputService.document.documentPart.Add(documentPartRole);

            DocumentPart documentPartPerson = new();
            documentPartPerson.documentPartType = DocumentPartType.primaryParty.ToString();

            documentPartPerson.documentApplication = new List<DocumentApplication>();
            DocumentApplication documentApplicationPerson = new();
            documentApplicationPerson.title = DocumentPartType.primaryParty.GetEnumDescription();
            documentApplicationPerson.documentApplicationType = DocumentPartType.primaryParty.ToString();
            documentApplicationPerson.party = new List<Party>();

            var personList = signRequest.SignRequestPeople.Where(x => x.IsOriginal == YesNo.Yes.GetString()).ToList();
            var getPersonFingerprintImage = new GetPersonFingerprintImageQuery(signRequest.Id.ToString());

            var fingerprintRes = await _mediator.Send(getPersonFingerprintImage, cancellationToken);
  
            List<DocumentOwners> documentOwnersList = new List<DocumentOwners>();
            foreach (var person in personList)
            {
                Party partyAsil = new Party();
                partyAsil.identityId = person.NationalNo;
                partyAsil.partyType = PartyType.person.ToString();
                partyAsil.partyId = person.SignClassifyNo.ToString();
                partyAsil.name = person.Name;
                partyAsil.person = new Person()
                {
                    familyName = person.Family,
                    fatherName = person.FatherName,
                    gender = person.SexType == EdmSexType.Female.ToInt32().ToString() ? EdmSexType.Female.GetEnumDescription() : EdmSexType.Male.GetEnumDescription(),
                    dateOfBirth = new SimpleDate()
                    {
                        year = person.BirthDate.Substring(0, 4).ToInt(),
                        month = person.BirthDate.Substring(5, 2).ToInt(),
                        day = person.BirthDate.Substring(8, 2).ToInt(),
                    }
                };
                partyAsil.contactMechanismApplication = new List<ContactMechanismApplication>();
                ContactMechanismApplication contactMechanismApplicationPerson = new ContactMechanismApplication();
                contactMechanismApplicationPerson.contactMechanismApplicationType = ContactMechanismType.postalAddress.ToString();
                contactMechanismApplicationPerson.contactMechanism = new()
                {
                    postalAddress = new PostalAddress()
                    {
                        postalAddressPart = new List<PostalAddressPart>(),
                    }
                };
                PostalAddressPart postalCodePerson = new();
                postalCodePerson.postalAddressPartType = PostalAddressPartType.postalCode.ToString();
                postalCodePerson.postalAddressPartValue = person.PostalCode;
                contactMechanismApplicationPerson.contactMechanism.postalAddress.postalAddressPart.Add(postalCodePerson);

                PostalAddressPart AddressPerson = new();
                AddressPerson.postalAddressPartType = PostalAddressPartType.addressText.ToString();
                AddressPerson.postalAddressPartValue = person.Address;
                contactMechanismApplicationPerson.contactMechanism.postalAddress.postalAddressPart.Add(AddressPerson);
                partyAsil.contactMechanismApplication.Add(contactMechanismApplicationPerson);
                if (fingerprintRes.IsSuccess && fingerprintRes.Data != null && fingerprintRes.Data.Count > 0)
                {
                    partyAsil.fingerprintPic = new ImageCollection();
                    partyAsil.fingerprintPic.image = new()
                    {
                        new ImageFile()
                        {
                            file =fingerprintRes.Data.FirstOrDefault(x => x.PersonObjectId == person.Id.ToString()) != null ? Convert.ToBase64String(fingerprintRes.Data?.FirstOrDefault(x => x.PersonObjectId == person.Id.ToString()).FingerPrintImage) : "",
                        }
                    };
                }
                DocumentOwners documentOwner = new()
                {
                    sequence = person.RowNo,
                    ownerId = person.Id.ToString(),
                    ownerType = 1,
                    documentOwner = person.NationalNo,
                    ownerMobileNo = "+98" + person.MobileNo.Substring(1, 10).ToString(),
                    //ownerRole = 1,
                    legalPersonAgentId = person.NationalNo,
                };
                documentOwnersList.Add(documentOwner);
                documentApplicationPerson.party.Add(partyAsil);
            }


            documentPartPerson.documentApplication.Add(documentApplicationPerson);


            inputService.document.documentPart.Add(documentPartPerson);

            DocumentPart documentPartImage = new();
            documentPartImage.documentPartType = DocumentPartType.image.ToString();
            documentPartImage.documentApplication = new();
            DocumentApplication application = new DocumentApplication();
            application.title = DocumentPartType.image.GetEnumDescription();
            application.documentApplicationType = DocumentPartType.image.ToString();
            application.documentPic = new();
            DocumentPic documentPic = new DocumentPic();
            documentPic.signCertificatePic = new();
            documentPic.signCertificatePic.image = new()
            {
                new ImageFile()
                {
                    file =signRequest.SignRequestFile != null ? Convert.ToBase64String(signRequest.SignRequestFile.ScanFile) : "" ,
                }
            };
            application.documentPic.signCertificatePic = documentPic.signCertificatePic;
            documentPartImage.documentApplication.Add(application);


            inputService.document.documentPart.Add(documentPartImage);
            var json = JsonConvert.SerializeObject(inputService, Formatting.Indented);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            JsonNode root = JsonNode.Parse(json);

            RemoveNullFieldsRecursive(root);

            string cleanedJson = root.ToJsonString(new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            byte[] bytes = Encoding.UTF8.GetBytes(cleanedJson);
            string stringBase64 = Convert.ToBase64String(bytes);

            var signEdmServiceInput = new SignEdmServiceInput()
            {
                OrginalB64Doc = stringBase64,
                SignerId = "default",
                TokenAndClaims = "default",
                ClientId = "SSAR",
                MainObjectId = signRequest.Id.ToString(),
            };
            var signEdmResult = await _mediator.Send(signEdmServiceInput, cancellationToken);

            if (signEdmResult != null && signEdmResult.IsSuccess && signEdmResult.Data != null)
            {
                var importEdmServiceInput = new ImportEdmServiceInput()
                {
                    ClientId = "SSAR",
                    MainObjectId = signRequest.Id.ToString(),
                    externalTrackCode = signRequest.Id.ToString(),
                    documentMetadata = new DocumentMetadata()
                    {
                        descriptiveData = new DescriptiveData()
                        {
                            documentCode = signRequest.NationalNo != null ? signRequest.NationalNo : signRequest.ReqNo,
                            documentType = EdmDocmentType.SignDocument.ToInt32(),
                            documentName = EdmDocmentType.SignDocument.GetEnumDescription(),
                            description = signRequest.Description,
                            documentOwners = documentOwnersList,
                            originalSystem = OriginalSystem.systemCode.ToInt32()
                        },
                        administrativeData = new AdministrativeData()
                        {
                            createDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            modifyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            documentSize = "1kb",
                            accessLevel = "public",
                            documentPath = "/path/to",
                            backupTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            signatureType = SignatureType.systemSignature.ToInt32(),
                        },
                        structuralData = new StructuralData()
                        {
                            documentVersion = 1,
                            electronicFormat = ElectronicFormat.JSON.ToInt32(),
                            pageCount = 1,
                            schemaVersion = "signCertificate_1.6",
                            status = Status.Issue.ToInt32(),
                        }
                    },
                    documentPayload = new DocumentPayload()
                    {
                        TokenAndClaims = "default",
                        SignedDoc = signEdmResult.Data.signed_Docmsg,
                    }
                };




                var json2 = JsonConvert.SerializeObject(importEdmServiceInput, Formatting.Indented);
                var importEdmResult = await _mediator.Send(importEdmServiceInput, cancellationToken);
                var json1 = JsonConvert.SerializeObject(importEdmResult, Formatting.Indented);
                if (importEdmResult != null && importEdmResult.IsSuccess && importEdmResult.Data != null)
                {

                    var createSignRequestFileEdmCommand = new CreateSignRequestFileEdmCommand
                    {
                        EDMId = importEdmResult.Data.DocumentId,
                        EDMVersion = importEdmResult.Data.Version.ToString(),
                        SignRequestId = signRequest.Id,
                    };
                    await _mediator.Send(createSignRequestFileEdmCommand, cancellationToken);

                }
                else
                {
                    if (importEdmResult.Data.ReasonCode == 1020008)
                    {
                        string text = importEdmResult.Data.ResponseDescription;

                        string documentId = string.Empty;
                        Match match = Regex.Match(text, @"\d+");
                        if (match.Success)
                        {
                            documentId = match.Value;
                        }

                        var createSignRequestFileEdmCommand = new CreateSignRequestFileEdmCommand
                        {
                            EDMId = documentId,
                            SignRequestId = signRequest.Id,
                        };
                        await _mediator.Send(createSignRequestFileEdmCommand, cancellationToken);
                    }
                    else
                    {
                        _apiResult.IsSuccess = false;
                        _apiResult.message.AddRange(importEdmResult.message);
                        _apiResult.message.Add(importEdmResult.Data.ReasonCode.ToString());
                        _apiResult.message.Add("خطا در بارگزاری فایل");
                    }

                }
            }
            else
            {
                _apiResult.IsSuccess = false;
                _apiResult.message.AddRange(signEdmResult.message);
                _apiResult.message.Add("خطا در امضا");
            }

            return _apiResult;

        }



        static void RemoveNullFieldsRecursive(JsonNode node)
        {
            if (node is JsonObject jsonObject)
            {
                // کلیدهایی که مقدار null دارند را جمع‌آوری می‌کنیم
                var keysToRemove = new List<string>();

                foreach (var kvp in jsonObject)
                {
                    if (kvp.Value == null)
                    {
                        keysToRemove.Add(kvp.Key);
                    }
                    else
                    {
                        RemoveNullFieldsRecursive(kvp.Value);
                    }
                }

                // سپس کلیدها را حذف می‌کنیم (در حلقه بالا نمی‌شود حذف کرد)
                foreach (var key in keysToRemove)
                {
                    jsonObject.Remove(key);
                }
            }
            else if (node is JsonArray jsonArray)
            {
                foreach (var item in jsonArray)
                {
                    if (item != null)
                        RemoveNullFieldsRecursive(item);
                }
            }
        }
    }
}
