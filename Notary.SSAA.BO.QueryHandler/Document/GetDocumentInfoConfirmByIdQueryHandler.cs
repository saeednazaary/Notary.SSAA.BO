using MediatR;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.DataTransferObject.Mappers.Document;
using Notary.SSAA.BO.DataTransferObject.Queries.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;

namespace Notary.SSAA.BO.QueryHandler.Document
{
    public class GetDocumentInfoConfirmByIdQueryHandler
        : BaseQueryHandler<GetDocumentInfoConfirmByIdQuery, ApiResult<DocumentViewModel>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentInfoOtherRepository _documentInfoOtherRepository;
        private readonly IDocumentInquiryRepository _documentInquiryRepository;
        private readonly ClientConfiguration _clientConfiguration;
        private readonly IDocumentElectronicBookBaseInfoRepository _documentElectronicBookBaseInfoRepository;
        private readonly IDocumentElectronicBookRepository _documentElectronicBookRepository;
        public GetDocumentInfoConfirmByIdQueryHandler(
            IMediator mediator,
            IUserService userService,
            IDocumentRepository documentRepository,
            IDocumentInfoOtherRepository documentInfoOtherRepository,
            IDocumentInquiryRepository documentInquiryRepository,
            IDocumentElectronicBookBaseInfoRepository documentElectronicBookBaseInfoRepository,
            IDocumentElectronicBookRepository documentElectronicBookRepository,
             ClientConfiguration clientConfiguration

        )
            : base(mediator, userService)
        {
            _documentRepository =
                documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _documentInfoOtherRepository = documentInfoOtherRepository;
            _clientConfiguration = clientConfiguration;
            _documentInquiryRepository = documentInquiryRepository;
            _userService = userService;
            _documentElectronicBookBaseInfoRepository = documentElectronicBookBaseInfoRepository;
            _documentElectronicBookRepository = documentElectronicBookRepository;

        }

        protected override bool HasAccess(
            GetDocumentInfoConfirmByIdQuery request,
            IList<string> userRoles
        )
        {
            return userRoles.Contains(RoleConstants.Sardaftar)
                || userRoles.Contains(RoleConstants.Daftaryar)
                || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentViewModel>> RunAsync(
            GetDocumentInfoConfirmByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            DocumentViewModel result = new();
            ApiResult<DocumentViewModel> apiResult = new();

            Domain.Entities.Document document = await _documentRepository.GetDocumentInfoConfirmById(
                request.DocumentId,
                cancellationToken
            );
            if (document != null)
            {
                result = DocumentMapper.MapDocumentToDocumentViewModel(document);
                apiResult.Data = result;
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
