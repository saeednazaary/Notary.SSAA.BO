using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.DataTransferObject.Mappers.Document;
using Notary.SSAA.BO.DataTransferObject.Queries.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ConfirmationAuthenticity;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Document;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;

namespace Notary.SSAA.BO.QueryHandler.Document
{
    public class GetDocumentRelationsByIdQueryHandler
        : BaseQueryHandler<GetDocumentRelationsByIdQuery, ApiResult<DocumentViewModel>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IDocumentInfoOtherRepository _documentInfoOtherRepository;
        private readonly IDocumentInquiryRepository _documentInquiryRepository;
        private readonly ClientConfiguration _clientConfiguration;
        private readonly IDocumentElectronicBookBaseInfoRepository _documentElectronicBookBaseInfoRepository;
        private readonly IDocumentElectronicBookRepository _documentElectronicBookRepository;
        public GetDocumentRelationsByIdQueryHandler(
            IMediator mediator,
            IUserService userService,
            IDocumentRepository documentRepository,
           IDocumentTypeRepository documentTypeRepository,
           IDocumentInfoOtherRepository documentInfoOtherRepository,

            IConfiguration configuration,
            IHttpEndPointCaller httpEndPointCaller,
            IDocumentInquiryRepository documentInquiryRepository,
            IDocumentElectronicBookBaseInfoRepository documentElectronicBookBaseInfoRepository,
            IDocumentElectronicBookRepository documentElectronicBookRepository,
             ClientConfiguration clientConfiguration

        )
            : base(mediator, userService)
        {
            _documentRepository =
                documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            _documentTypeRepository = documentTypeRepository ?? throw new ArgumentNullException(nameof(documentTypeRepository));
            _documentInfoOtherRepository = documentInfoOtherRepository;
            _clientConfiguration = clientConfiguration;
            _documentInquiryRepository = documentInquiryRepository;
            _userService = userService;
            _documentElectronicBookBaseInfoRepository = documentElectronicBookBaseInfoRepository;
            _documentElectronicBookRepository = documentElectronicBookRepository;

        }

        protected override bool HasAccess(
            GetDocumentRelationsByIdQuery request,
            IList<string> userRoles
        )
        {
            return userRoles.Contains(RoleConstants.Sardaftar)
                || userRoles.Contains(RoleConstants.Daftaryar)
                || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentViewModel>> RunAsync(
            GetDocumentRelationsByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            DocumentViewModel result = new();
            ApiResult<DocumentViewModel> apiResult = new();

            Domain.Entities.Document document = await _documentRepository.GetDocumentRelations(
                request.DocumentId.ToGuid(),
                cancellationToken
            );
            if (document != null)
            {
                result = DocumentMapper.MapDocumentToDocumentViewModel(document);

                if (result.DocumentRelations != null && result.DocumentRelations.Count > 0)
                {
                    IList<string> documentTypeIds = result.DocumentRelations.Select(p => p.RelatedDocumentTypeId.FirstOrDefault()).ToList();
                    string[] documentscriptoriumIds = result.DocumentRelations.Select(p => p.ScriptoriumId.FirstOrDefault()).ToArray();
                    GetScriptoriumByIdServiceInput scriptoriumQuery = new(documentscriptoriumIds);
                    IList<Domain.Entities.DocumentType> documentTypes = await _documentTypeRepository.GetDocumentTypes(documentTypeIds, cancellationToken);
                    string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().BASEINFO_APIS_Prefix;
                    var baseInfoUrl = _configuration.GetValue<string>("InternalGatewayUrl") + bo_Apis_prefix+ "api/v1/Specific/Ssar/";
                    IList<ScriptoriumData> scriptoriumData = null;
                    ApiResult<ScriptoriumViewModel> scriptoriumResult = new();
                    if (scriptoriumQuery.IdList != null && scriptoriumQuery.IdList[0] != null)
                    {
                        scriptoriumResult = await _httpEndPointCaller.CallPostApiAsync<ScriptoriumViewModel, GetScriptoriumByIdServiceInput>(
                       new HttpEndpointRequest<GetScriptoriumByIdServiceInput>(baseInfoUrl + ScriptoriumRequestConstant.Scriptorium,
                       _userService.UserApplicationContext.Token, scriptoriumQuery), cancellationToken);

                        scriptoriumData = scriptoriumResult.Data.ScriptoriumList;



                    }

                    for (int i = 0; i < result.DocumentRelations.Count; i++)
                    {
                        var relation = result.DocumentRelations[i];

                        string sId = relation.ScriptoriumId?.FirstOrDefault();
                        string dtypeId = relation.RelatedDocumentTypeId?.FirstOrDefault();

                        if (scriptoriumData != null && !string.IsNullOrEmpty(sId))
                        {
                            var scriptorium = scriptoriumData.FirstOrDefault(p => p.Id == sId);
                            if (scriptorium != null)
                            {
                                relation.RelatedDocumentScriptoriumTitle = scriptorium.Name;
                            }
                        }

                        if (documentTypes != null && !string.IsNullOrEmpty(dtypeId))
                        {
                            var documentType = documentTypes.FirstOrDefault(p => p.Id == dtypeId);
                            if (documentType != null)
                            {
                                relation.RelatedDocumentTypeTitle = documentType.Title;
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
                result.DocumentMainRelation = documentRelationViewModel;
                apiResult.Data = result;
                DocumentInfoOtherObject documentInfoOtherObject = await _documentInfoOtherRepository
                    .GetDocumentInfoOtherInformation((Guid.Parse(result.RequestId)));
                result.RequestType.AssetTypeId = documentInfoOtherObject.AssetTypeId;
                result.RequestType.DocumentTypeSubjectId = documentInfoOtherObject.DocumentTypeSubjectId;
                #region SetCalculateField
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
                    await CalculateFieldUtility.IsENoteBookBaseInfoInitialized(
                        _documentElectronicBookBaseInfoRepository,
                        _userService,
                        cancellationToken);
                apiResult.Data.IsENoteBookBaseInfoInitialized = isENoteBookBaseInfoInitialized;

                bool isClassifyNoEditable = false;
                long relatedDocClassifyNo = 0;
                if (document.State != NotaryRegServiceReqState.Finalized.GetString() &&
                    document.State != NotaryRegServiceReqState.CanceledAfterGetCode.GetString() &&
                    document.State != NotaryRegServiceReqState.CanceledBeforeGetCode.GetString())
                {
                    (isClassifyNoEditable, relatedDocClassifyNo) = await CalculateFieldUtility.IsClassifyNoEditable(
                        document,
                        _clientConfiguration,
                        _documentRepository,
                        _documentElectronicBookRepository,
                        _userService,
                        cancellationToken);
                    apiResult.Data.IsClassifyNoEditable = isClassifyNoEditable;
                }

                apiResult.Data.RelatedDocClassifyNo = relatedDocClassifyNo;
                apiResult.Data.SeparationVerify = CalculateFieldUtility.IsCurrentOragnizationPermitted(
                "CanSendDS",
                _userService,
                Settings.CanSendDS);

                apiResult.Data.ForbiddenCostTypeCodes = _clientConfiguration.ForbiddenCostTypeCodes;
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
                #endregion
            }
            else
            {
                apiResult.IsSuccess = true;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
                apiResult.message.Add("سند مربوطه یافت نشد");
            }
            return apiResult;
        }
    }
}
