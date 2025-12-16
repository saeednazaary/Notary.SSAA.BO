using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument
{
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using Notary.SSAA.BO.Configuration;
    using Notary.SSAA.BO.DataTransferObject.Commands.Document;
    using Notary.SSAA.BO.DataTransferObject.Mappers.Document;
    using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ConfirmationAuthenticity;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.SharedKernel.AppSettingModel;
    using Notary.SSAA.BO.SharedKernel.Constants;
    using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.SharedKernel.Result;
    using Notary.SSAA.BO.Utilities.Extensions;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Notary.ExternalServices.WebApi.Models.RequestsModel.Person;
    using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Person;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;

    /// <summary>
    /// Defines the <see cref="LoadDocumentCoordinator" />
    /// </summary>
    public class LoadDocumentCoordinator
    {
        private readonly IDocumentInquiryRepository _documentInquiryRepository;
        /// <summary>
        /// Defines the documentRepository
        /// </summary>
        private readonly IDocumentRepository documentRepository;

        /// <summary>
        /// Defines the documentTypeRepository
        /// </summary>
        private readonly IDocumentTypeRepository documentTypeRepository;
        private readonly IDocumentElectronicBookBaseInfoRepository _documentElectronicBookBaseInfoRepository;
        private readonly IDocumentElectronicBookRepository _documentElectronicBookRepository;


        /// <summary>
        /// Defines the dateTimeService
        /// </summary>
        private readonly IDateTimeService dateTimeService;

        /// <summary>
        /// Defines the userService
        /// </summary>
        private IUserService userService;

        /// <summary>
        /// Defines the apiResult
        /// </summary>
        private ApiResult<DocumentViewModel> apiResult;

        /// <summary>
        /// Defines the validatorGateway
        /// </summary>
        private ValidatorGateway validatorGateway;

        /// <summary>
        /// Defines the logger
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// Defines the document
        /// </summary>
        public Notary.SSAA.BO.Domain.Entities.Document? document = null;

        /// <summary>
        /// Defines the _configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Defines the _httpEndPointCaller
        /// </summary>
        private readonly IHttpEndPointCaller _httpEndPointCaller;

        /// <summary>
        /// Defines the mediator
        /// </summary>
        private IMediator mediator;

        /// <summary>
        /// Defines the _clientConfiguration
        /// </summary>
        private readonly ClientConfiguration _clientConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadDocumentCoordinator"/> class.
        /// </summary>
        /// <param name="_userService">The _userService<see cref="IUserService"/></param>
        /// <param name="_logger">The _logger<see cref="ILogger"/></param>
        /// <param name="_validatorGateway">The _validatorGateway<see cref="ValidatorGateway"/></param>
        /// <param name="_documentRepository">The _documentRepository<see cref="IDocumentRepository"/></param>
        /// <param name="_dateTimeService">The _dateTimeService<see cref="IDateTimeService"/></param>
        /// <param name="_documentTypeRepository">The _documentTypeRepository<see cref="IDocumentTypeRepository"/></param>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/></param>
        /// <param name="_mediator">The _mediator<see cref="IMediator"/></param>
        /// <param name="httpEndPointCaller">The httpEndPointCaller<see cref="IHttpEndPointCaller"/></param>
        /// <param name="clientConfiguration">The clientConfiguration<see cref="ClientConfiguration"/></param>
        public LoadDocumentCoordinator(IUserService _userService, ILogger _logger, ValidatorGateway _validatorGateway
    , IDocumentRepository _documentRepository, IDateTimeService _dateTimeService, IDocumentTypeRepository _documentTypeRepository, IDocumentInquiryRepository documentInquiryRepository, IDocumentElectronicBookRepository documentElectronicBookRepository,
    IDocumentElectronicBookBaseInfoRepository documentElectronicBookBaseInfoRepository, IConfiguration configuration, IMediator _mediator,
    IHttpEndPointCaller httpEndPointCaller, ClientConfiguration clientConfiguration)
        {
            apiResult = new();
            documentRepository = _documentRepository;
            dateTimeService = _dateTimeService;
            validatorGateway = _validatorGateway;
            logger = _logger;
            userService = _userService;
            documentTypeRepository = _documentTypeRepository;
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            mediator = _mediator;
            _clientConfiguration = clientConfiguration;
            _documentInquiryRepository = documentInquiryRepository;
            _documentElectronicBookBaseInfoRepository = documentElectronicBookBaseInfoRepository;
            _documentElectronicBookRepository = documentElectronicBookRepository;
        }

        /// <summary>
        /// The LoadDocumentDetail
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <param name="detailName">The detailName<see cref="string"/></param>
        /// <param name="caseType">The caseType<see cref="CaseType"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{ApiResult{DocumentViewModel}}"/></returns>
        public async Task<ApiResult<DocumentViewModel>> LoadDocumentDetail(string documentId, string detailName, CaseType caseType,

          CancellationToken cancellationToken
      )
        {
            documentRepository.ClearTracking();

            DocumentViewModel result = new();
            Notary.SSAA.BO.Domain.Entities.Document? document = null;
            ApiResult<DocumentViewModel> apiResult = new();
            if (!string.IsNullOrEmpty(detailName))
            {

                if (detailName == DocumentDatailName.DocumentPeople.GetString())
                {
                    document = await documentRepository.GetDocumentById(documentId.ToGuid(), new List<string> { "DocumentPeople" }, cancellationToken);

                    result = DocumentMapper.MapDocumentToDocumentViewModel(document);



                    GetPersonPhotoListServiceInput getPersonForNationalNo = new();
                    getPersonForNationalNo.NationalNos = result.DocumentPeople.Where(x => x.IsPersonIranian == true && x.IsPersonSabteAhvalChecked == true).Select(x => x.PersonNationalNo).ToList();
                    if (getPersonForNationalNo.NationalNos.Count > 0)
                    {
                        ApiResult<GetPersonPhotoListViewModel> personalImages = new();
                        personalImages.IsSuccess = false;
                        GetPersonPhotoListFromSabteAhvalServiceInput getPerson = new();
                        getPerson.Persons = result.DocumentPeople.Where(x => x.IsPersonIranian == true && x.IsPersonSabteAhvalChecked == true).Select(x => new PersonPhotoListItem() { NationalNo = x.PersonNationalNo, BirthDate = x.PersonBirthDate }).ToList();
                        getPerson.MainObjectId = document.Id.ToString();
                        if (getPerson.Persons.Count > 0)
                            personalImages = await mediator.Send(getPerson, cancellationToken);
                        foreach (var item in result.DocumentPeople)
                        {
                            if (personalImages.IsSuccess)
                            {
                                var personalImage = personalImages.Data.PersonsData
                                    .FirstOrDefault(x => x.NationalNo == item.PersonNationalNo);

                                item.PersonalImage = personalImage != null && personalImage.PersonalImage != null ? Convert.ToBase64String(personalImage.PersonalImage) : null;
                            }

                            if (item.IsPersonOriginal)
                            {
                                var personOrginal = document.DocumentPeople
                                    .FirstOrDefault(x => x.Id == item.PersonId.ToGuid());

                                if (personOrginal != null)
                                    item.PersonPost = personOrginal.DocumentPersonType?.SingularTitle;
                            }

                            var relatedPosts = document.DocumentPersonRelatedDocuments
                                .Where(x => x.AgentPersonId == Guid.Parse(item.PersonId))
                                .OrderBy(x => x.RowNo)
                                .Select(x =>
                                    $"{x.AgentType.Title} {document.DocumentPeople.FirstOrDefault(p => p.Id == x.MainPersonId)?.Name} {document.DocumentPeople.FirstOrDefault(p => p.Id == x.MainPersonId)?.Family}"
                                )
                                .ToList();

                            if (relatedPosts.Any())
                            {
                                if (!item.PersonPost.IsNullOrWhiteSpace())
                                    item.PersonPost += " و ";

                                item.PersonPost += string.Join(" و ", relatedPosts);
                            }
                        }
                    }
                    result.DocumentPeople = result.DocumentPeople.OrderBy(x => x.RowNo).ToList();
                    //_ = result.RequestType;
                    apiResult.Data = result;
                }
                else
                if (detailName == DocumentDatailName.DocumentRelatedPeople.GetString())

                {
                    document = await documentRepository.GetDocumentById(documentId.ToGuid(), new List<string> { "DocumentRelatedPeople" }, cancellationToken);
                    if (document != null)
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel(document);

                        result.DocumentRelatedPeople = result.DocumentRelatedPeople.OrderBy(x => x.RowNo).ToList();
                        apiResult.Data = result;
                    }
                }
                else
                if (detailName == DocumentDatailName.DocumentCase.GetString())
                {

                    if (caseType == CaseType.DocumentCase)
                    {
                        document = await documentRepository.GetDocumentById(documentId.ToGuid(), new List<string> { "DocumentCases" }, cancellationToken);
                        if (document != null)
                        {
                            result = DocumentMapper.MapDocumentToDocumentViewModel(document);

                            result.DocumentCases = result.DocumentCases.OrderBy(x => x.RowNo).ToList();
                            apiResult.Data = result;
                        }
                    }
                    else
                    if (caseType == CaseType.DocumentVehicle)
                    {
                        document = await documentRepository.GetDocumentById(documentId.ToGuid(), new List<string> { "DocumentVehicles" }, cancellationToken);

                        if (document != null)
                        {
                            result = DocumentMapper.MapDocumentToDocumentViewModel(document);
                            result.DocumentVehicles = result.DocumentVehicles.OrderBy(x => x.RowNo).ToList();

                            apiResult.Data = result;
                        }

                    }
                    else
                    if (caseType == CaseType.DocumentEstate)
                    {
                        document = await documentRepository.GetDocumentById(documentId.ToGuid(), new List<string> { "DocumentEstates" }, cancellationToken);

                        if (document != null)
                        {
                            result = DocumentMapper.MapDocumentToDocumentViewModel(document);
                            result.DocumentEstates = result.DocumentEstates.OrderBy(x => x.RowNo).ToList();

                            apiResult.Data = result;
                        }
                    }
                }
                else
                if (detailName == DocumentDatailName.DocumentInfoOther.GetString())
                {
                    document = await documentRepository.GetDocumentById(documentId.ToGuid(), new List<string> { "DocumentInfoOther" }, cancellationToken);
                    if (document != null)
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel(document);
                        result.DocumentInfoOther.DocumentTypeTitle = document.DocumentTypeTitle;

                        if (result.DocumentInfoText != null)
                        {
                            if (result.DocumentInfoOther != null && !string.IsNullOrEmpty(result.DocumentInfoText.DocumentDescription))
                            {
                                result.DocumentInfoOther.DocumentDescription = result.DocumentInfoText.DocumentDescription;

                            }
                        }
                        result.DocumentInfoText = null;
                        apiResult.Data = result;
                    }
                }
                else
                if (detailName == DocumentDatailName.DocumentInfoText.GetString())
                {
                    document = await documentRepository.GetDocumentById(documentId.ToGuid(), new List<string> { "DocumentInfoText" }, cancellationToken);
                    if (document != null)
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel(document);
                        apiResult.Data = result;
                    }
                }
                else
                if (detailName == DocumentDatailName.RelationDocument.GetString())
                {
                    document = await documentRepository.GetDocumentById(documentId.ToGuid(), new List<string> { "DocumentRelationDocuments" }, cancellationToken);
                    if (document != null)
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel(document);

                        if (result != null && result.DocumentRelations != null && result.DocumentRelations.Count() > 0)
                        {
                            IList<string?> documentTypeIds = result.DocumentRelations.Select(p => p.RelatedDocumentTypeId.FirstOrDefault()).ToList();
                            string?[] documentscriptoriumIds = result.DocumentRelations.Select(p => p.ScriptoriumId.FirstOrDefault()).ToArray();
                            GetScriptoriumByIdServiceInput scriptoriumQuery = new(documentscriptoriumIds);
                            IList<Notary.SSAA.BO.Domain.Entities.DocumentType> documentTypes = await documentTypeRepository.GetDocumentTypes(documentTypeIds, cancellationToken);
                            string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().BASEINFO_APIS_Prefix;
                            var baseInfoUrl = _configuration.GetValue<string>("InternalGatewayUrl") + bo_Apis_prefix + "api/v1/Specific/Ssar/";
                            IList<ScriptoriumData>? scriptoriumData = null;
                            ApiResult<Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ConfirmationAuthenticity.ScriptoriumViewModel> scriptoriumResult = new();
                            if (scriptoriumQuery.IdList != null && scriptoriumQuery.IdList[0] != null)
                            {
                                scriptoriumResult = await _httpEndPointCaller.CallPostApiAsync<Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ConfirmationAuthenticity.ScriptoriumViewModel, GetScriptoriumByIdServiceInput>(
                               new HttpEndpointRequest<GetScriptoriumByIdServiceInput>(baseInfoUrl + ScriptoriumRequestConstant.Scriptorium,
                               userService.UserApplicationContext.Token, scriptoriumQuery), cancellationToken);

                                scriptoriumData = scriptoriumResult.Data.ScriptoriumList;

                            }

                            for (int i = 0; i < result.DocumentRelations.Count; i++)
                            {
                                string? sId = result.DocumentRelations[i].ScriptoriumId.FirstOrDefault();
                                string? dtypeId = result.DocumentRelations[i].RelatedDocumentTypeId.FirstOrDefault();
                                if (scriptoriumData != null)
                                {
                                    if (scriptoriumData.FirstOrDefault(p => p.Id == sId) != null)
                                    {

                                        result.DocumentRelations[i].RelatedDocumentScriptoriumTitle = scriptoriumData.FirstOrDefault(p => p.Id == sId)?.Name;
                                    }

                                }
                                if (documentTypes != null)
                                {

                                    if (documentTypes.FirstOrDefault(p => p.Id == dtypeId) != null)
                                    {
                                        result.DocumentRelations[i].RelatedDocumentTypeTitle = documentTypes.FirstOrDefault(p => p.Id == dtypeId)?.Title;

                                    }
                                }

                            }

                            result.DocumentRelations = result.DocumentRelations.OrderByDescending(x => x.RequestDate).OrderBy(x => x.RequestRecordDate).ToList();
                        }
                        DocumentRelationViewModel documentRelationViewModel = new DocumentRelationViewModel();
                        documentRelationViewModel.IsDelete = false;
                        documentRelationViewModel.IsDirty = false;
                        documentRelationViewModel.IsValid = true;
                        documentRelationViewModel.IsNew = false;
                        documentRelationViewModel.RequestNo = document.RelatedDocumentNo;
                        documentRelationViewModel.RequestDate = document.RelatedDocumentDate;
                        documentRelationViewModel.RequestSecretCode = document.RelatedDocumentSecretCode;
                        documentRelationViewModel.IsRequestAbroad = document.IsRelatedDocAbroad.ToNullableYesNo();
                        documentRelationViewModel.IsRequestInSsar = document.RelatedDocumentIsInSsar.ToNullableYesNo();
                        documentRelationViewModel.RelatedDocumentScriptorium = document.RelatedDocumentScriptorium;
                        documentRelationViewModel.RelatedDocAbroadCountryId = document.RelatedDocAbroadCountryId == null ? new List<string> { } : new List<string> { document.RelatedDocAbroadCountryId.ToString() };
                        documentRelationViewModel.ScriptoriumId = document.RelatedScriptoriumId == null ? new List<string> { } : new List<string> { document.RelatedScriptoriumId };
                        documentRelationViewModel.RelatedDocumentTypeId = document.RelatedDocumentTypeId == null ? new List<string> { } : new List<string> { document.RelatedDocumentTypeId };
                        if (result != null)
                        {
                            result.DocumentMainRelation = documentRelationViewModel;
                            apiResult.Data = result;
                        }
                    }

                }
                else
                if (detailName == DocumentDatailName.DocumentInquirie.GetString())
                {
                    document = await documentRepository.GetDocumentById(documentId.ToGuid(), new List<string> { "DocumentInquiries" }, cancellationToken);

                    if (document != null)
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel(document);
                        result.DocumentInquiries = result.DocumentInquiries.ToList();
                        apiResult.Data = result;
                    }

                }
                if (detailName == DocumentDatailName.DocumentCost.GetString())
                {
                    //    document = await documentRepository.GetDocumentById ( documentId.ToGuid (), new List<string> { "DocumentCost" }, cancellationToken );
                    document = await documentRepository.GetDocumentCostsAndCostUnchange(documentId.ToGuid(), cancellationToken);

                    if (document != null)
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel(document);
                        result.DocumentCosts = result.DocumentCosts.OrderBy(x => x.recordDateTime).ToList();
                        apiResult.Data = result;
                    }

                }
                if (detailName == DocumentDatailName.DocumentPayment.GetString())
                {
                    document = await documentRepository.GetDocumentById(documentId.ToGuid(), new List<string> { "DocumentPayments" }, cancellationToken);

                    if (document != null)
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel(document);
                        result.DocumentPayments = result.DocumentPayments.ToList();
                        apiResult.Data = result;
                    }

                }
                if (detailName == DocumentDatailName.DocumentSms.GetString())
                {
                    document = await documentRepository.GetDocumentById(documentId.ToGuid(), new List<string> { "DocumentSms" }, cancellationToken);

                    if (document != null)
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel(document);
                        result.DocumentSms = result.DocumentSms.ToList();
                        apiResult.Data = result;
                    }

                }
            }

            if (document == null)
            {
                apiResult.IsSuccess = true;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("سند مربوطه یافت نشد");
            }
            else
            {

                if (DsuUtility.isLoadDocumentInquiriesList(document.DocumentType, document.IsRegistered,
                        document.Id.ToString(), _clientConfiguration))
                {

                    var documentInquiriesInformation = await _documentInquiryRepository.GetDocumentInquiriesInformation(document.Id);
                    string messages = null;
                    bool isDSUPermitted = DsuUtility.IsDSUGeneratingPermitted(ref messages, document, documentInquiriesInformation, true,
                        _clientConfiguration);
                    apiResult.Data.IsDSUPermitted = isDSUPermitted;

                }

                bool IsDocTypeDSUForced = DsuUtility.IsDocTypeDSUForced(document.DocumentType);
                bool isDocRestrictedType = false;
                if (document.DocumentType != null)
                {
                    isDocRestrictedType = Mapper.IsONotaryDocumentRestrictedType(document.DocumentType.Id);
                    apiResult.Data.IsDocRestrictedType = isDocRestrictedType;

                }

                bool isENoteBookBaseInfoInitialized = false;
                string eNoteBookBaseInfoId = null;
                DocumentElectronicBookBaseinfo eNoteBookBaseInfo = null;

                (isENoteBookBaseInfoInitialized, eNoteBookBaseInfoId, eNoteBookBaseInfo) =
                    await IsENoteBookBaseInfoInitialized(cancellationToken);
                apiResult.Data.IsENoteBookBaseInfoInitialized = isENoteBookBaseInfoInitialized;


                bool isClassifyNoEditable = false;
                long relatedDocClassifyNo = 0;
                if (document.State != NotaryRegServiceReqState.Finalized.GetString() &&
                     document.State != NotaryRegServiceReqState.CanceledAfterGetCode.GetString() &&
                     document.State != NotaryRegServiceReqState.CanceledBeforeGetCode.GetString())
                {
                    (isClassifyNoEditable, relatedDocClassifyNo) = await IsClassifyNoEditable(document, cancellationToken);
                    apiResult.Data.IsClassifyNoEditable = isClassifyNoEditable;

                }
                apiResult.Data.RelatedDocClassifyNo = relatedDocClassifyNo;
                apiResult.Data.SeparationVerify = IsCurrentOragnizationPermitted("CanSendDS");
                apiResult.Data.ForbiddenCostTypeCodes = _clientConfiguration.ForbiddenCostTypeCodes;

                //apiResult.Data.IsDSUPermitted = false;
                //apiResult.Data.IsDSUForced = false;
                //apiResult.Data.IsDocRestrictedType = false;
                //apiResult.Data.IsENoteBookBaseInfoInitialized = false;
                //apiResult.Data.IsClassifyNoEditable = false;
                apiResult.Data.RelatedDocDigitalBookClassifyNo = null;
                //apiResult.Data.IsENoteBookEnabled = false;
                // apiResult.Data.SeparationVerify = false;
                apiResult.Data.GetPersonPhotoEnabled = _clientConfiguration.GetPersonPhotoEnabled;
                apiResult.Data.SMSServiceEnabled = _clientConfiguration.SMSServiceEnabled;
                apiResult.Data.UserDefinedMessages = _clientConfiguration.UserDefinedMessages;
                apiResult.Data.ViewRelatedDocumentImageEnabled = _clientConfiguration.ViewRelatedDocumentImageEnabled;
                apiResult.Data.IsMechanizedTaxEnabled = _clientConfiguration.IsMechanizedTaxEnabled;
                apiResult.Data.IsDSUDealSummaryCreationEnabled = _clientConfiguration.IsDSUDealSummaryCreationEnabled.GetString();
                apiResult.Data.IsMechanizedMunicipalitySettlementEnabled = _clientConfiguration.IsMechanizedMunicipalitySettlementEnabled;
                apiResult.Data.showFinalVerificationWindow = _clientConfiguration.showFinalVerificationWindow;
                apiResult.Data.IsFingerprintEnabled = _clientConfiguration.IsFingerprintEnabled;
                apiResult.Data.ENoteBookEnabledDate = _clientConfiguration.ENoteBookEnabledDate;
                apiResult.Data.AutomatedRemoveRestrictionEnabled = _clientConfiguration.IsAutomatedRemoveRestrictionEnabled;
                apiResult.Data.ForbiddenSabtCostDocumentTypeCodes = _clientConfiguration.ForbiddenSabtCostDocumentTypeCodes;
                apiResult.Data.ForbiddenTahrirCostDocumentTypeCodes = _clientConfiguration.ForbiddenTahrirCostDocumentTypeCodes;
                apiResult.Data.IsENoteBookEnabled = _clientConfiguration.IsENoteBookAutoClassifyNoEnabled;



            }

            return apiResult;
        }

        /// <summary>
        /// The GetDetails
        /// </summary>
        /// <param name="request">The request<see cref="SaveDocumentCommand"/></param>
        /// <returns>The <see cref="List{string}"/></returns>
        public List<string> GetDetails(SaveDocumentCommand request)
        {
            List<string> detailsList = new List<string>();

            if (request.DocumentCostQuestion != null)
            {
                detailsList.Add("DocumentCostQuestion");
            }
            if (request.DocumentCases?.Count > 0)
            {
                detailsList.Add("DocumentCases");
            }
            if (request.DocumentPeople?.Count > 0)
            {
                detailsList.Add("DocumentPeople");

            }
            if (request.DocumentVehicles?.Count > 0)
            {
                detailsList.Add("DocumentVehicles");

            }
            if (request.DocumentEstates.Count > 0)
            {
                detailsList.Add("DocumentEstates");
            }
            if (request.DocumentRelatedPeople.Count > 0 || request.DocumentPeople != null && request.DocumentPeople.Count > 0)
            {
                detailsList.Add("DocumentRelatedPeople");
            }
            if (request.DocumentRelations?.Count > 0)
            {
                detailsList.Add("DocumentRelationDocuments");

            }
            if (request.DocumentCosts?.Count > 0)
            {
                detailsList.Add("DocumentCosts");
                detailsList.Add("DocumentCostUnchangeds");

            }
            if (request.DocumentPayments?.Count > 0)
            {
                detailsList.Add("DocumentPayments");
            }
            if (request.DocumentSms?.Count > 0)
            {
                detailsList.Add("DocumentSms");
            }
            if (request.DocumentInfoJudgment != null)
            {
                detailsList.Add("DocumentInfoJudgment");
            }
            if (request.DocumentInfoOther != null)
            {
                detailsList.Add("DocumentInfoOther");
                detailsList.Add("DocumentInfoText");
            }
            if (request.DocumentInfoText != null)
            {
                detailsList.Add("DocumentInfoText");
            }
            if (request.DocumentCostQuestion != null)
            {
                detailsList.Add("DocumentInfoOther");

            }
            if (request.DocumentInquiries != null)
            {
                detailsList.Add("DocumentInquiries");
            }
            return detailsList;
        }

        /// <summary>
        /// The GetCopyDetails
        /// </summary>
        /// <param name="request">The request<see cref="SaveDocumentCommand"/></param>
        /// <returns>The <see cref="List{string}"/></returns>
        public List<string> GetCopyDetails(SaveDocumentCommand request)
        {
            List<string> detailsList = new List<string>();
            if (!string.IsNullOrEmpty(request.DocumentCopyId))
            {
                if (request.IsCopyDocumentPeople == true)
                {
                    detailsList.Add("DocumentPeople");
                }
                if (request.IsCopyDocumentCases == true)
                {
                    detailsList.Add("DocumentCases");
                    detailsList.Add("DocumentVehicles");
                }
                if (request.IsCopyDocumentInfoText == true)
                {
                    detailsList.Add("DocumentInfoText");
                }

            }

            return detailsList;
        }


        private async Task<(bool, string, DocumentElectronicBookBaseinfo)> IsENoteBookBaseInfoInitialized(CancellationToken cancellationToken)
        {
            DocumentElectronicBookBaseinfo theDigitalBookBaseInfo = null;
            string digitalBookBaseInfoObjectId = null;
            var currentScriptoriumDigitalBookBaseInfos = await _documentElectronicBookBaseInfoRepository.GetElectronicBooks(new List<string> { }, cancellationToken, userService.UserApplicationContext.BranchAccess.BranchCode);
            if (currentScriptoriumDigitalBookBaseInfos != null && currentScriptoriumDigitalBookBaseInfos.Count > 0)
            {
                theDigitalBookBaseInfo = currentScriptoriumDigitalBookBaseInfos.ElementAt(0);
                digitalBookBaseInfoObjectId = theDigitalBookBaseInfo.Id.ToString();
                if (
                    theDigitalBookBaseInfo == null ||
                    theDigitalBookBaseInfo.LastClassifyNo == 0 ||
                    theDigitalBookBaseInfo.NumberOfBooks == 0 ||
                    theDigitalBookBaseInfo.NumberOfBooksAgent == null ||
                    theDigitalBookBaseInfo.NumberOfBooksArzi == null ||
                    theDigitalBookBaseInfo.NumberOfBooksJari == null ||
                    theDigitalBookBaseInfo.NumberOfBooksOghaf == null ||
                    theDigitalBookBaseInfo.NumberOfBooksOthers == null ||
                    theDigitalBookBaseInfo.NumberOfBooksRahni == null ||
                    theDigitalBookBaseInfo.NumberOfBooksVehicle == null
                )
                    return (false, digitalBookBaseInfoObjectId, theDigitalBookBaseInfo);
            }
            else
            {
                return (true, digitalBookBaseInfoObjectId, theDigitalBookBaseInfo);


            }

            return (false, digitalBookBaseInfoObjectId, theDigitalBookBaseInfo);


        }


        private async Task<(bool, long)> IsClassifyNoEditable(Document theCurrentRequest, CancellationToken cancellationToken)
        {
            long relatedDocClassifyNo = 0;
            if (!_clientConfiguration.IsENoteBookAutoClassifyNoEnabled)
                return (true, relatedDocClassifyNo);

            if (theCurrentRequest.DocumentType.IsSupportive == YesNo.No.ToString())
                return (false, relatedDocClassifyNo);

            //در تمامی اسناد خدمات تبعی بجز اسناد از نوع (اخطاریه، قبض سپرده و گواهي علت عدم انجام معامله) لازم است که شماره ترتیب سند غیر قابل تغییر بوده و خودکار پر شود.
            List<string> editableDocTypes = new List<string>() { "008", "002", "0012", "009" };
            if (
                theCurrentRequest.DocumentType.IsSupportive == YesNo.Yes.ToString() &&
                !editableDocTypes.Contains(theCurrentRequest.DocumentTypeId) &&
                theCurrentRequest.RelatedDocumentIsInSsar != YesNo.Yes.GetString()
                )
            {
                relatedDocClassifyNo = (!string.IsNullOrWhiteSpace(theCurrentRequest.RelatedDocumentNo)) ? long.Parse(theCurrentRequest.RelatedDocumentNo) : 0;
                return (false, relatedDocClassifyNo);
            }

            List<string> notEditableDocTypes = new List<string>() { "004", "005", "006", "007", "0034", "0022" };
            if (!notEditableDocTypes.Contains(theCurrentRequest.DocumentTypeId))
                return (true, relatedDocClassifyNo);

            if (theCurrentRequest.State == NotaryRegServiceReqState.Created.GetString() ||
                theCurrentRequest.State == NotaryRegServiceReqState.CostPaid.GetString() ||
                theCurrentRequest.State == NotaryRegServiceReqState.SetNationalDocumentNo.GetString()
                )
                return (false, relatedDocClassifyNo);

            if (
                theCurrentRequest.RelatedDocumentIsInSsar == YesNo.Yes.GetString() &&
                !string.IsNullOrWhiteSpace(theCurrentRequest.RelatedDocumentNo) &&
                theCurrentRequest.RelatedDocumentNo.Length >= 18 &&
                !editableDocTypes.Contains(theCurrentRequest.DocumentTypeId)
                )
            {
                DocumentElectronicBook theDigitalBookDocEntity = await _documentElectronicBookRepository.GetDocumentElectronicBook(theCurrentRequest.RelatedDocumentNo, cancellationToken);


                if (theDigitalBookDocEntity == null)
                {

                    int? result = await documentRepository.GetClassifyNoDocument(theCurrentRequest.RelatedDocumentNo,
                        userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);

                    if (result == null)
                    {
                        if (theCurrentRequest.State == NotaryRegServiceReqState.CostPaid.GetString())
                            return (true, relatedDocClassifyNo);
                        else
                            return (false, relatedDocClassifyNo);
                    }
                    else
                    {
                        relatedDocClassifyNo = long.Parse(result.ToString());
                        return (false, relatedDocClassifyNo);
                    }
                }
                else
                {
                    relatedDocClassifyNo = (long)theDigitalBookDocEntity.ClassifyNo;
                    return (false, relatedDocClassifyNo);
                }
            }
            else
            {
                return (true, relatedDocClassifyNo);
            }
        }

        internal bool IsCurrentOragnizationPermitted(string configKey)
        {
            string masterConfigString = Settings.CanSendDS; //System.Configuration.ConfigurationManager.AppSettings[configKey] as string;

            if (string.IsNullOrWhiteSpace(masterConfigString))
                return true;

            if (masterConfigString == "*" || masterConfigString.ToLower() == "true")
                return true;

            if (masterConfigString == "0" || masterConfigString.ToLower() == "false")
                return false;



            string[] masterConfigSectionsCollection = null;

            if (masterConfigString.Contains("|"))
                masterConfigSectionsCollection = masterConfigString.Split('|');
            else
                masterConfigSectionsCollection = new string[] { masterConfigString };


            List<ConfigCouple> configCoupleCollection = new List<ConfigCouple>();
            foreach (string theOneMasterSection in masterConfigSectionsCollection)
            {
                if (theOneMasterSection == "*")
                    return true;

                ConfigCouple configCouple = new ConfigCouple();
                string[] theOneMasterSectionParts = null;

                if (theOneMasterSection.Contains(":"))
                {
                    theOneMasterSectionParts = theOneMasterSection.Split(':');
                    configCouple.Value = theOneMasterSectionParts[1];
                }
                else
                    theOneMasterSectionParts = new string[] { theOneMasterSection };

                configCouple.Key = theOneMasterSectionParts[0];

                configCoupleCollection.Add(configCouple);
            }

            foreach (ConfigCouple theOneConfigCouple in configCoupleCollection)
            {
                if (theOneConfigCouple.Key == "E" && theOneConfigCouple.Value == "0")
                    return false;

                bool isDenyingKey = false;
                if (theOneConfigCouple.Key.Contains("-"))
                {
                    theOneConfigCouple.Key = theOneConfigCouple.Key.Replace("-", "");
                    isDenyingKey = true;
                }

                if (userService.UserApplicationContext.BranchAccess.BranchCode != null)
                {
                    var levelCodePrefix = userService.UserApplicationContext.ScriptoriumInformation.Unit.LevelCode.Substring(0, 4);

                    if (isDenyingKey && theOneConfigCouple.Key == levelCodePrefix)
                        return false;

                    if (theOneConfigCouple.Key == "*")
                        return true;

                    if (theOneConfigCouple.Key != levelCodePrefix)
                        continue;

                }
                //else
                //{
                //    if ( isDenyingKey && theOneConfigCouple.Key == theCurrentCMSOrganization.TheUnit.LevelCode.Substring ( 0, 4 ) )
                //        return false;

                //    if ( theOneConfigCouple.Key == "*" )
                //        return true;

                //    if ( theOneConfigCouple.Key != theCurrentCMSOrganization.TheUnit.LevelCode.Substring ( 0, 4 ) )
                //        continue;
                //}

                if (theOneConfigCouple.Value == null || theOneConfigCouple.Value == "*")
                    return true;

                string[] subLevels;
                if (theOneConfigCouple.Value.Contains(","))
                    subLevels = theOneConfigCouple.Value.Split(',');
                else
                    subLevels = new string[] { theOneConfigCouple.Value };

                foreach (string rawSubLevel in subLevels)
                {
                    string cleanSubLevel = rawSubLevel;
                    bool returnValue = true;

                    if (cleanSubLevel.Contains("-"))
                    {
                        cleanSubLevel = cleanSubLevel.Replace("-", "");
                        returnValue = true;
                    }

                    if (userService.UserApplicationContext.ScriptoriumInformation != null)
                    {
                        if (cleanSubLevel == userService.UserApplicationContext.ScriptoriumInformation.Code)
                            return returnValue;
                        else if (cleanSubLevel == "*")
                            return true;
                    }

                    // if (theCurrentCMSOrganization.TheUnit != null)
                    // {
                    //     if (cleanSubLevel == theCurrentCMSOrganization.TheUnit.Code)
                    //         return returnValue;
                    //     else if (cleanSubLevel == "*")
                    //         return true;
                    // }
                }
            }

            return false;
        }


    }
    internal class ConfigCouple
    {
        internal string Key { get; set; }
        internal string Value { get; set; }
    }
}
