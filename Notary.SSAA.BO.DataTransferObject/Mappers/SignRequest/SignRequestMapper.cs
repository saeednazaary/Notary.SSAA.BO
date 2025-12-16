using Mapster;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.SignRequest;
using Notary.SSAA.BO.Domain.RepositoryObjects.SignRequestReports;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest
{
    public static class SignRequestMapper
    {
        public static void ToEntity(ref Domain.Entities.SignRequest entity, CreateSignRequestCommand viewModel)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<CreateSignRequestCommand, Domain.Entities.SignRequest>()

                .Map(dest => dest.SignRequestSubjectId, src => src.SignRequestSubjectId.First())
                .Map(dest => dest.SignRequestGetterId, src => src.SignRequestGetterId.FirstOrDefault())
                .Map(dest => dest.IsRemoteRequest, src => src.IsRemoteRequest == true ? "1" : "2")
                .Map(dest => dest.RemoteRequestId, src => src.RemoteRequestId)
                .Map(dest => dest.SignText, src => src.SignRequestSignText)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            viewModel.Adapt(entity, config);

        }
        public static void ToEntity(ref Domain.Entities.SignRequest entity, Domain.Entities.SignRequest source)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<Domain.Entities.SignRequest, Domain.Entities.SignRequest>()
                .Ignore(x => x.SignRequestPersonRelateds)
                .Ignore(x => x.Id)
                .Ignore(x => x.SignRequestPeople);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            source.Adapt(entity, config);


            foreach (var item in source.SignRequestPeople)
            {
                var selectedItem = entity.SignRequestPeople.First(x => x.Id == item.Id);
                selectedItem = ToEntity(item);
            }
            foreach (var item in source.SignRequestPersonRelateds)
            {
                var selectedItem = entity.SignRequestPersonRelateds.First(x => x.Id == item.Id);
                selectedItem = ToEntity(item);
            }
        }
        public static Domain.Entities.SignRequestPerson ToEntity(Domain.Entities.SignRequestPerson source)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<Domain.Entities.SignRequestPerson, Domain.Entities.SignRequestPerson>()
                .Ignore(x => x.Id)
                .Ignore(x => x.SignRequestId);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            return source.Adapt<Domain.Entities.SignRequestPerson>(config);
        }
        public static Domain.Entities.SignRequestPersonRelated ToEntity(Domain.Entities.SignRequestPersonRelated source)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<Domain.Entities.SignRequestPersonRelated, Domain.Entities.SignRequestPersonRelated>()
                .Ignore(x => x.Id)
                .Ignore(x => x.SignRequestId);

            // Apply the configuration
            config.Compile();

            // Perform the mapping

            return source.Adapt<Domain.Entities.SignRequestPersonRelated>(config);


        }
        public static void ToEntity(ref Domain.Entities.SignRequest entity, UpdateSignRequestCommand viewModel)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<UpdateSignRequestCommand, Domain.Entities.SignRequest>()

                .Map(dest => dest.SignRequestSubjectId, src => src.SignRequestSubjectId.First())
                .Map(dest => dest.SignRequestGetterId, src => src.SignRequestGetterId.FirstOrDefault())
                .Map(dest => dest.SignText, src => src.SignRequestSignText)

                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            viewModel.Adapt(entity, config);

        }
        public static void ToEntity(ref SignRequestPersonRelated signRequestPersonRelated, ToRelatedPersonViewModel signRequestRelatedPersonViewModel)
        {
            var config = new TypeAdapterConfig();

            // Configure the mapping from SignRequestPersonRelated to SignRequestRelatedPersonViewModel
            config.NewConfig<ToRelatedPersonViewModel, SignRequestPersonRelated>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.RelatedPersonId.ToGuid())
                .Map(dest => dest.SignRequestId, src => src.SignRequestId.ToGuid())
                .Map(dest => dest.MainPersonId, src => src.MainPersonId.First().ToGuid())
                .Map(dest => dest.AgentPersonId, src => src.RelatedAgentPersonId.First().ToGuid())
                .Map(dest => dest.AgentTypeId, src => src.RelatedAgentTypeId.First())
                //.Map(dest => dest.IsAgentDocumentAbroad, src => src.IsRelatedAgentDocumentAbroad.ToYesNo())
                // .Map(dest => dest.AgentDocumentCountryId, src => src.RelatedAgentDocumentCountryId != null ? src.RelatedAgentDocumentCountryId.FirstOrDefault().ToNullableInt() : null)
                // .Map(dest => dest.IsRelatedDocumentInSsar, src => src.IsRelatedDocumentInSSAR.ToYesNo())
                .Map(dest => dest.AgentDocumentNo, src => src.RelatedAgentDocumentNo)
                .Map(dest => dest.AgentDocumentDate, src => src.RelatedAgentDocumentDate)
                .Map(dest => dest.AgentDocumentIssuer, src => src.RelatedAgentDocumentIssuer)
                // .Map(dest => dest.AgentDocumentSecretCode, src => src.RelatedAgentDocumentSecretCode)
                //.Map(dest => dest.AgentDocumentScriptoriumId, src => src.RelatedAgentDocumentScriptoriumId != null ? src.RelatedAgentDocumentScriptoriumId.FirstOrDefault() : null)
                //.Map(dest => dest.IsLawyer, src => src.IsRelatedPersonLawyer.ToYesNo())
                .Map(dest => dest.ReliablePersonReasonId, src => src.RelatedReliablePersonReasonId != null ? src.RelatedReliablePersonReasonId.FirstOrDefault() : null)
                .Map(dest => dest.Description, src => src.RelatedPersonDescription)
                .Ignore(dest => dest.Ilm).IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();
            // Perform the mapping
            signRequestRelatedPersonViewModel.Adapt(signRequestPersonRelated, config);
        }
        public static void ToEntity(ref SignRequestPerson signRequestPerson, SignRequestPersonViewModel viewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<SignRequestPersonViewModel, SignRequestPerson>().IgnoreNullValues(true)
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.PersonId.ToGuid())
                .Map(dest => dest.SignRequestId, src => src.SignRequestId.ToGuid())
                .Map(dest => dest.IsRelated, src => src.IsPersonRelated.ToYesNo())
                .Map(dest => dest.IsIranian, src => src.IsPersonIranian.ToYesNo())
                .Map(dest => dest.NationalityId, src => src.PersonNationalityId.Count > 0 ? src.PersonNationalityId.First().ToNullableInt() : null)
                .Map(dest => dest.NationalNo, src => src.PersonNationalNo)
                .Map(dest => dest.Name, src => src.PersonName)
                .Map(dest => dest.Family, src => src.PersonFamily)
                .Map(dest => dest.BirthDate, src => src.PersonBirthDate)
                .Map(dest => dest.SexType, src => src.PersonSexType)
                .Map(dest => dest.FatherName, src => src.PersonFatherName)
                .Map(dest => dest.IdentityIssueLocation, src => src.PersonIdentityIssueLocation)
                .Map(dest => dest.IdentityNo, src => src.PersonIdentityNo)
                .Map(dest => dest.SeriAlpha, src => src.PersonAlphabetSeri)
                .Map(dest => dest.Seri, src => src.PersonSeri)
                .Map(dest => dest.Serial, src => src.PersonSerial)
                .Map(dest => dest.IsOriginal, src => src.IsPersonOriginal.ToYesNo())
                .Map(dest => dest.IsAlive, src => src.IsPersonAlive.ToYesNo())
                .Map(dest => dest.MobileNo, src => src.PersonMobileNo)
                .Map(dest => dest.Email, src => src.PersonEmail)
                .Map(dest => dest.PostalCode, src => src.PersonPostalCode)
                .Map(dest => dest.Address, src => src.PersonAddress)
                .Map(dest => dest.Tel, src => src.PersonTel)
                .Map(dest => dest.Description, src => src.PersonDescription)
                .Ignore(src => src.Ilm)
                .Ignore(src => src.IsFingerprintGotten)
                .IgnoreIf((src, dest) => src.IsNew, dest => dest.SignRequestId)
                .Ignore(src => src.PersonType)
                .IgnoreNonMapped(true);
            // Apply the configuration
            config.Compile();
            // Perform the mapping by reference
            viewModel.Adapt(signRequestPerson, config);
        }
        public static SignRequestViewModel ToViewModel(Domain.Entities.SignRequest signRequest)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.SignRequest, SignRequestViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.SignRequestReqNo, src => src.ReqNo)
                .Map(dest => dest.SignRequestReqDate, src => src.ReqDate)
                .Map(dest => dest.SignRequestGetterId, src =>string.IsNullOrWhiteSpace(src.SignRequestGetterId)?new List<string>(): new List<string> { src.SignRequestGetterId.ToString().Trim() })
                .Map(dest => dest.SignRequestSubjectId, src => new List<string> { src.SignRequestSubjectId.ToString().Trim() })
                .Map(dest => dest.SignRequestNationalNo, src => src.NationalNo)
                .Map(dest => dest.SignRequestSecretCode, src => src.SecretCode)
                .Map(dest => dest.StateId, src => src.State)
                .Map(dest => dest.SignRequestConfirmer, src => src.Confirmer)
                .Map(dest => dest.SignRequestConfirmDateTime, src => src.ConfirmDate == null || src.ConfirmTime == null ? "" : src.ConfirmDate + " - " + src.ConfirmTime)
                .Map(dest => dest.SignRequestPaymentMethod, src => src.PaymentType)
                .Map(dest => dest.SignRequestPayCostDateTime, src => src.PayCostDate == null || src.PayCostTime == null ? "" : src.PayCostDate + " - " + src.PayCostTime)
                .Map(dest => dest.SignRequestReceiptNo, src => src.ReceiptNo)
                .Map(dest => dest.SignRequestBillNo, src => src.BillNo)
                .Map(dest => dest.SignRequestSignText, src => src.SignText)
                .Map(dest => dest.SignRequestTotalPrice, src => src.SumPrices.HasValue? (src.SumPrices.ToString()+" ریال "):"")
                .Map(dest => dest.SignRequestSignDate, src => src.SignDate)
                .Map(dest => dest.IsCostPaid, src => src.IsCostPaid.ToBoolean())
                .Map(dest => dest.SignRequestPersons, src => ToPersonList(src.SignRequestPeople.ToList()))
                .Map(dest => dest.SignRequestRelatedPersons, src => ToRelatedPersonList(src.SignRequestPersonRelateds.ToList()))
                .Map(dest => dest.RemoteRequestId, src => src.RemoteRequestId)
                .Map(dest => dest.IsRemoteRequest, src => src.IsRemoteRequest)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            var returnObject = signRequest.Adapt<SignRequestViewModel>(config);
            return returnObject;
        }
        public static SignRequestPersonViewModel ToPersonViewModel(SignRequestPerson signRequestPerson)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from SignRequestPerson to SignRequestPersonViewModel
            config.NewConfig<SignRequestPerson, SignRequestPersonViewModel>().IgnoreNullValues(true)
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.PersonId, src => src.Id.ToString())
                .Map(dest => dest.PersonState, src => src.IsFingerprintGotten == "1" ? "2" : "1")
                .Map(dest => dest.SignRequestId, src => src.SignRequestId.ToString())
                .Map(dest => dest.RowNo, src => src.RowNo.ToString())
                .Map(dest => dest.PersonClassifyNo, src => src.SignClassifyNo)
                .Map(dest => dest.IsPersonOriginal, src => src.IsOriginal.ToBoolean())
                .Map(dest => dest.IsPersonRelated, src => src.IsRelated.ToBoolean())
                .Map(dest => dest.IsPersonIranian, src => src.IsIranian.ToBoolean())
                .Map(dest => dest.PersonNationalityId, src => src.NationalityId.HasValue ? new List<string> { src.NationalityId.ToString().Trim() } : new List<string>())
                .Map(dest => dest.PersonNationalNo, src => src.NationalNo)
                .Map(dest => dest.PersonName, src => src.Name)
                .Map(dest => dest.PersonFamily, src => src.Family)
                .Map(dest => dest.PersonBirthDate, src => src.BirthDate)
                .Map(dest => dest.PersonSexType, src => src.SexType)
                .Map(dest => dest.PersonFatherName, src => src.FatherName)
                .Map(dest => dest.PersonIdentityIssueLocation, src => src.IdentityIssueLocation)
                .Map(dest => dest.PersonIdentityNo, src => src.IdentityNo)
                .Map(dest => dest.PersonAlphabetSeri, src => src.SeriAlpha)
                .Map(dest => dest.PersonSeri, src => src.Seri)
                .Map(dest => dest.PersonSerial, src => src.Serial)
                .Map(dest => dest.IsPersonSanaChecked, src => src.SanaState.ToNullabbleBoolean())
                .Map(dest => dest.AmlakEskanState, src => src.AmlakEskanState.ToNullabbleBoolean())
                .Map(dest => dest.PersonMobileNoState, src => src.MobileNoState.ToNullabbleBoolean())
                .Map(dest => dest.IsPersonSabteAhvalChecked, src => src.IsSabtahvalChecked.ToNullabbleBoolean())
                .Map(dest => dest.IsPersonSabteAhvalCorrect, src => src.IsSabtahvalCorrect.ToNullabbleBoolean())
                .Map(dest => dest.IsPersonAlive, src => src.IsAlive.ToNullabbleBoolean())
                .Map(dest => dest.PersonMobileNo, src => src.MobileNo)
                .Map(dest => dest.PersonEmail, src => src.Email)
                .Map(dest => dest.PersonPostalCode, src => src.PostalCode)
                .Map(dest => dest.PersonAddress, src => src.Address)
                .Map(dest => dest.PersonTel, src => src.Tel)
                .Map(dest => dest.IsTFARequired, src => src.TfaRequired.ToNullabbleBoolean())
                .Map(dest => dest.IsFingerprintGotten, src => src.IsFingerprintGotten.ToNullabbleBoolean())
                .Map(dest => dest.TFAState, src => src.TfaState)
                .Map(dest => dest.PersonDescription, src => src.Description);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            var signRequestPersonViewModel = signRequestPerson.Adapt<SignRequestPersonViewModel>(config);
            return signRequestPersonViewModel;
        }
        public static SignRequestElectronicPersonViewModel ToElectronicBookPagePersonViewModel(SignRequestPerson signRequestPerson)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from SignRequestPerson to SignRequestPersonViewModel
            config.NewConfig<SignRequestPerson, SignRequestElectronicPersonViewModel>().IgnoreNullValues(true)
                .Map(dest => dest.PersonSignClassifyNo, src => src.SignClassifyNo)
                .Map(dest => dest.RowNo, src => src.RowNo.ToString())               
                .Map(dest => dest.PersonDescription, src => $"فقط صحت امضای {src.Name} {src.Family} به شماره ملی {src.NationalNo} متولد {src.BirthDate} مورد تایید است .");
            // Apply the configuration
            config.Compile();

            // Perform the mapping
            var signRequestPersonViewModel = signRequestPerson.Adapt<SignRequestElectronicPersonViewModel>(config);
            return signRequestPersonViewModel;
        }
        public static SignRequestElectronicBookPageViewModel ToElectronicBookPageViewModel(Domain.Entities.SignRequest signRequest)
        {
            TypeAdapterConfig<Domain.Entities.SignRequest, SignRequestElectronicBookPageViewModel>
                .NewConfig()
                .Map(dest => dest.SignRequestId, src => src.Id.ToString())
                .Map(dest => dest.SignRequestReqNo, src => src.ReqNo)
                .Map(dest => dest.SignRequestNationalNo, src => src.NationalNo)
                .Map(dest => dest.SignRequestGetterTitle, src =>src.SignRequestGetter!=null? src.SignRequestGetter.Title:"")
                .Map(dest => dest.SignRequestSubjectTitle, src => src.SignRequestSubject.Title != null ? src.SignRequestSubject.Title : "")
                .Map(dest => dest.SignRequestSecretCode, src => src.SecretCode)
                .Map(dest => dest.SignRequestSignDate, src => src.SignDate)
                .Map(dest => dest.SignRequestElectronicBookPagePersons, src => ToElectronicBookPagePersonsList(src.SignRequestPeople.ToList()))
                .IgnoreNonMapped(true);

            var returnObject = signRequest.Adapt<SignRequestElectronicBookPageViewModel>();

            return returnObject;
        }
        public static ToRelatedPersonViewModel ToRelatedPersonViewModel(SignRequestPersonRelated signRequestPersonRelated)
        {
            var config = new TypeAdapterConfig();

            // Configure the mapping from SignRequestPersonRelated to SignRequestRelatedPersonViewModel
            config.NewConfig<SignRequestPersonRelated, ToRelatedPersonViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.RelatedPersonId, src => src.Id.ToString())
                .Map(dest => dest.SignRequestId, src => src.SignRequestId.ToString())
                .Map(dest => dest.MainPersonId, src => new List<string> { src.MainPersonId.ToString() })
                .Map(dest => dest.RelatedAgentPersonId, src => new List<string> { src.AgentPersonId.ToString() })
                .Map(dest => dest.RelatedAgentTypeId, src => new List<string> { src.AgentTypeId.ToString() })
                //.Map(dest => dest.RelatedAgentDocumentCountryId, src => src.AgentDocumentCountryId != null ? new List<string> { src.AgentDocumentCountryId.ToString() } : new List<string>())
                //.Map(dest => dest.IsRelatedAgentDocumentAbroad, src => src.IsAgentDocumentAbroad.ToBoolean())
                //.Map(dest => dest.IsRelatedDocumentInSSAR, src => src.IsRelatedDocumentInSsar.ToBoolean())
                .Map(dest => dest.RelatedAgentDocumentNo, src => src.AgentDocumentNo)
                .Map(dest => dest.RelatedAgentDocumentDate, src => src.AgentDocumentDate)
                .Map(dest => dest.RelatedAgentDocumentIssuer, src => src.AgentDocumentIssuer)
                .Map(dest => dest.RelatedAgentTypeTitle, src => src.AgentType.Title)
                //.Map(dest => dest.RelatedAgentDocumentSecretCode, src => src.AgentDocumentSecretCode)
                //.Map(dest => dest.RelatedAgentDocumentScriptoriumId, src => src.AgentDocumentScriptoriumId != null ? new List<string> { src.AgentDocumentScriptoriumId.ToString() } : new List<string>())
                //.Map(dest => dest.IsRelatedPersonLawyer, src => src.IsLawyer.ToBoolean())
                .Map(dest => dest.RelatedReliablePersonReasonId, src => src.ReliablePersonReasonId != null ? new List<string> { src.ReliablePersonReasonId.ToString() } : new List<string>())
                .Map(dest => dest.RelatedPersonDescription, src => src.Description);

            // Apply the configuration
            config.Compile();

            var signRequestRelatedPersonViewModel = new ToRelatedPersonViewModel();
            // Perform the mapping
            signRequestRelatedPersonViewModel = signRequestPersonRelated.Adapt<ToRelatedPersonViewModel>(config);

            return signRequestRelatedPersonViewModel;
        }
        public static List<SignRequestPersonViewModel> ToPersonList(List<SignRequestPerson> signRequestPersons)
        {
            List<SignRequestPersonViewModel> signRequestPersonViewModels = new List<SignRequestPersonViewModel>();
            signRequestPersons.ForEach(sp =>
            {
                signRequestPersonViewModels.Add(ToPersonViewModel(sp));


            });
            return signRequestPersonViewModels;

        }
        public static List<SignRequestElectronicPersonViewModel> ToElectronicBookPagePersonsList(List<SignRequestPerson> signRequestPersons)
        {
            List<SignRequestElectronicPersonViewModel> signRequestPersonViewModels = new List<SignRequestElectronicPersonViewModel>();
            signRequestPersons.ForEach(sp =>
            {
                signRequestPersonViewModels.Add(ToElectronicBookPagePersonViewModel(sp));


            });
            return signRequestPersonViewModels;

        }
        public static List<ToRelatedPersonViewModel> ToRelatedPersonList(List<SignRequestPersonRelated> signRequestPersonRelatedList)
        {
            List<ToRelatedPersonViewModel> signRequestRelatedPersonViewModels = new List<ToRelatedPersonViewModel>();
            signRequestPersonRelatedList.ForEach(sp =>
            {
                signRequestRelatedPersonViewModels.Add(ToRelatedPersonViewModel(sp));


            });
            return signRequestRelatedPersonViewModels;
        }
        public static SignRequestPersonFingerprintViewModel ToFingerprintViewModel(SignRequestPerson signRequestPerson)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from SignRequestPerson to SignRequestPersonViewModel
            config.NewConfig<SignRequestPerson, SignRequestPersonFingerprintViewModel>().IgnoreNullValues(true)

                .Map(dest => dest.PersonId, src => src.Id.ToString())
                .Map(dest => dest.MainObjectId, src => src.SignRequestId.ToString())
                .Map(dest => dest.PersonNationalNo, src => src.NationalNo)
                .Map(dest => dest.IsPersonOriginal, src => src.IsOriginal.ToBoolean())
                .Map(dest => dest.PersonName, src => src.Name)
                .Map(dest => dest.IsPersian, src => src.IsIranian.ToBoolean())
                .Map(dest => dest.PersonFamily, src => src.Family)
                .Map(dest => dest.PersonBirthDate, src => src.BirthDate)
                .Map(dest => dest.PersonSexType, src => src.SexType)
                .Map(dest => dest.PersonFatherName, src => src.FatherName)
                .Map(dest => dest.IsPersonAlive, src => src.IsAlive.ToBoolean())
                .Map(dest => dest.PersonMobileNo, src => src.MobileNo)
                .Map(dest => dest.IsTFARequired, src => src.TfaRequired.ToNullabbleBoolean())
                .Map(dest => dest.IsFingerprintGotten, src => src.IsFingerprintGotten.ToNullabbleBoolean())
                .Map(dest => dest.TFAState, src => src.TfaState);
            // Apply the configuration
            config.Compile();

            // Perform the mapping
            var signRequestPersonViewModel = signRequestPerson.Adapt<SignRequestPersonFingerprintViewModel>(config);
            return signRequestPersonViewModel;
        }
        public static SignRequestPrintViewModel ToPrintViewModel(Domain.Entities.SignRequest signRequest)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.SignRequest, SignRequestPrintViewModel>
                ().IgnoreNullValues(true)
                .Map(dest => dest.SecretCode, src => src.SecretCode)
                .Map(dest => dest.SignNationalNo, src => src.NationalNo)
                .Map(dest => dest.SignSubjectType, src => src.SignRequestSubject != null ? src.SignRequestSubject.Title : null)
                .Map(dest => dest.SignGetter, src => src.SignRequestGetter != null ? "جهت ارائه به: " + (signRequest.SignRequestGetter != null ? signRequest.SignRequestGetter.Title : "") : null)
                .Map(dest => dest.SignDate, src => src.SignDate)
                .Map(dest => dest.SardaftarNameFamily, src => src.Confirmer)
                .Map(dest => dest.LegalText, src => src.SignText)
                .Map(dest => dest.Title, src => src.SignRequestSubject != null ? "موضوع: " + src.SignRequestSubject.Title : "")
                .Map(dest => dest.ScriptoriumId, src => src.ScriptoriumId);

            config.Compile();
            var signRequestViewModel = signRequest.Adapt<SignRequestPrintViewModel>(config);

            return signRequestViewModel;
        }
        public static KatebSignRequestViewModel ToKatebSignRequestViewModel(Domain.Entities.SignRequest signRequest)
        {
            TypeAdapterConfig<Domain.Entities.SignRequest, KatebSignRequestViewModel>
                .NewConfig().IgnoreNullValues(true)
                .Map(dest => dest.ReqNo, src => src.ReqNo)
                .Map(dest => dest.ReqDate, src => src.ReqDate)
                .Map(dest => dest.SignRequestSubjectTitle, src => src.SignRequestSubject != null ? src.SignRequestSubject.Title : null)
                .Map(dest => dest.SignRequestGetterTitle, src => src.SignRequestGetter != null ? (signRequest.SignRequestGetter != null ? signRequest.SignRequestGetter.Title : "") : null)
                .Map(dest => dest.SsarNo, src => src.NationalNo)
                .Map(dest => dest.ScriptoriumId, src => src.ScriptoriumId)
                .Map(dest => dest.SignText, src => src.SignText)
                .Map(dest => dest.IsCostPaid, src => src.IsCostPaid)
                .Map(dest => dest.IsRemote, src => src.IsRemoteRequest)
                .Map(dest => dest.RemoteRequestId, src => src.RemoteRequestId)
                .Map(dest => dest.IsReadyToPay, src => src.IsReadyToPay)
                .Map(dest => dest.State, src => src.State)
                .Map(dest => dest.personViewModel, src => ToKatebPerson(src.SignRequestPeople.Where(x => x.IsOriginal == YesNo.Yes.GetString()).FirstOrDefault()))
                .IgnoreNonMapped(true);
            return signRequest.Adapt<KatebSignRequestViewModel>();
        }
        private static KatebSignRequestPersonViewModel ToKatebPerson(SignRequestPerson signRequestPerson)
        {
            TypeAdapterConfig<Domain.Entities.SignRequestPerson, KatebSignRequestPersonViewModel>
                .NewConfig().IgnoreNullValues(true)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Family, src => src.Family)
                .Map(dest => dest.BirthDate, src => src.BirthDate)
                .Map(dest => dest.NationalNo, src => src.NationalNo)
                .Map(dest => dest.MobileNo, src => src.MobileNo)
                .Map(dest => dest.IsFingerprintGotten, src => src.IsFingerprintGotten);
            return signRequestPerson.Adapt<KatebSignRequestPersonViewModel>();
        }

        public static SignRequestTextPrintViewModel ToTextPrintViewModel(Domain.Entities.SignRequest masterEntity)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.SignRequest, SignRequestTextPrintViewModel>
                ().IgnoreNullValues(true)

                .Map(dest => dest.SignGetter, src => src.SignRequestGetter != null ? "جهت ارائه به: " + (masterEntity.SignRequestGetter != null ? masterEntity.SignRequestGetter.Title : "") : null)
                .Map(dest => dest.Text, src => src.SignText)
                .Map(dest => dest.Title, src => src.SignRequestSubject != null ? "موضوع: " + src.SignRequestSubject.Title : "");

            config.Compile();
            var signRequestViewModel = masterEntity.Adapt<SignRequestTextPrintViewModel>(config);

            return signRequestViewModel;
        }

        public static SignRequestStatisticItem ToSignReqStatisticReportViewModel(SignRequestStatisticRepositoryObject item)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<SignRequestStatisticRepositoryObject, SignRequestStatisticItem>
                ().IgnoreNullValues(true)

                .Map(dest => dest.ClassifyNo, src => src.ClassifyNo)
                .Map(dest => dest.FullName, src => src.Name + " " + src.Family)
                .Map(dest => dest.Subjects, src => src.Subjects)
                .Map(dest => dest.NationalNo, src => src.NationalNo)
                .Map(dest => dest.SignDate, src => src.SignDate)
                .Map(dest => dest.Getters, src => src.Getters);

            config.Compile();
            var signRequestViewModel = item.Adapt<SignRequestStatisticItem>(config);

            return signRequestViewModel;
        }
        public static SignRequestDocumentTemplateViewModel ToViewModel(Domain.Entities.DocumentTemplate entity)
        {
            SignRequestDocumentTemplateViewModel documentTemplateViewModel = new();
            TypeAdapterConfig config = new();

            _ = config.NewConfig<Domain.Entities.DocumentTemplate, SignRequestDocumentTemplateViewModel>()

                .Map(dest => dest.DocumentTemplateText, src => src.Text)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.DocumentTemplateCode, src => src.Code)
                .Map(dest => dest.DocumentTemplateId, src => src.Id.ToString())
                .Map(dest => dest.DocumentTypeId, src => new List<string> { src.DocumentTypeId })
                .Map(dest => dest.DocumentTemplateTitle, src => src.Title)
                .Map(dest => dest.DocumentTemplateStateId, src => src.State)
                .Map(dest => dest.LastModifer, src => src.Modifier)
                .Map(dest => dest.LastModifyDateTime, src => src.ModifyDateTime.Replace("-", " - "))
                .Map(dest => dest.CreateDate, src => src.CreateDate + " - " + src.CreateTime)
                .Map(dest => dest.ScriptoriumId, src => src.ScriptoriumId)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            _ = entity.Adapt(documentTemplateViewModel, config);

            return documentTemplateViewModel;
        }
        public static SignRequestFingerItem ToSignReqFingerReportViewModel(SignRequestFingerPrintRepositoryObject item)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<SignRequestFingerPrintRepositoryObject, SignRequestFingerItem>
                ().IgnoreNullValues(true)

                .Map(dest => dest.FingerType, src => src.FingerType)
                .Map(dest => dest.DocDate, src => src.DocDate)
                .Map(dest => dest.DocNo, src => src.DocNo)
                .Map(dest => dest.FingerDate, src => src.FingerDate)
                .Map(dest => dest.FingerDeviceName, src => src.FingerDeviceName)
                .Map(dest => dest.FingerImage, src => src.FingerImage)
                .Map(dest => dest.FingerTime, src => src.FingerTime)
                .Map(dest => dest.IsFingerMatch, src => ((YesNo)src.IsFingerMatch.ToInt()).GetEnumDescription())
                .Map(dest => dest.PersonMobileNoFinger, src => src.PersonMobileNoFinger)
                .Map(dest => dest.PersonFamilyFinger, src => src.PersonFamilyFinger)
                .Map(dest => dest.PersonNameFinger, src => src.PersonNameFinger)
                .Map(dest => dest.PersonNatinalNoFinger, src => src.PersonNatinalNoFinger)
                .Map(dest => dest.PersonPostalCodeFinger, src => src.PersonPostalCodeFinger)
                .Map(dest => dest.TFASendDate, src => src.TFASendDate)
                .Map(dest => dest.TFASendTime, src => src.TFASendTime)
                .Map(dest => dest.ScriptoriumId, src => src.scriptoriumId)
                .Map(dest => dest.SignNationalNo, src => src.SignNationalNo);

            config.Compile();
            var signRequestViewModel = item.Adapt<SignRequestFingerItem>(config);

            return signRequestViewModel;
        }
    }
}