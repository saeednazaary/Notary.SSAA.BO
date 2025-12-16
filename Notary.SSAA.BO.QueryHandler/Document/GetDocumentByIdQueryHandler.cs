using MediatR;
using Microsoft.AspNetCore.Http;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.DataTransferObject.Commands.Document;
using Notary.SSAA.BO.DataTransferObject.Mappers.Document;
using Notary.SSAA.BO.DataTransferObject.Queries.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Document;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;
using Notary.ExternalServices.WebApi.Models.RequestsModel.Person;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Person;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;

namespace Notary.SSAA.BO.QueryHandler.Document
{
    public class GetDocumentByIdQueryHandler : BaseQueryHandler<GetDocumentByIdQuery, ApiResult<DocumentViewModel>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentInfoOtherRepository _documentInfoOtherRepository;
        private readonly ValidatorGateway validatorGateway;
        private readonly IDocumentInquiryRepository _documentInquiryRepository;
        private readonly ClientConfiguration _clientConfiguration;
        private readonly IDocumentElectronicBookBaseInfoRepository _documentElectronicBookBaseInfoRepository;
        private readonly IDocumentElectronicBookRepository _documentElectronicBookRepository;

        public GetDocumentByIdQueryHandler(IMediator mediator, IUserService userService,
            IDocumentRepository documentRepository,
            IDocumentInfoOtherRepository documentInfoOtherRepository,
            ValidatorGateway _validatorGateway,
            IDocumentInquiryRepository documentInquiryRepository,
            IDocumentElectronicBookBaseInfoRepository documentElectronicBookBaseInfoRepository,
            IDocumentElectronicBookRepository documentElectronicBookRepository,
             ClientConfiguration clientConfiguration
            )
            : base(mediator, userService)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            validatorGateway = _validatorGateway;
            _documentInfoOtherRepository = documentInfoOtherRepository;
            _clientConfiguration = clientConfiguration;
            _documentInquiryRepository = documentInquiryRepository;
            _userService = userService;
            _documentElectronicBookBaseInfoRepository = documentElectronicBookBaseInfoRepository;
            _documentElectronicBookRepository = documentElectronicBookRepository;
        }
        protected override bool HasAccess(GetDocumentByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }

        protected override async Task<ApiResult<DocumentViewModel>> RunAsync(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            DocumentViewModel result = new();
            ApiResult<DocumentViewModel> apiResult = new();

            Domain.Entities.Document document = await _documentRepository.GetDocument(request.DocumentId.ToGuid(), cancellationToken);
            if (document != null)
            {

                if (document.State == NotaryRegServiceReqState.SetNationalDocumentNo.GetString())
                {
                    var paymentInquiryResult = await _mediator.Send(new UpdateDocumentPaymentStateCommand() { DocumentNo = document.RequestNo, InquiryMode = true }, cancellationToken);
                    if (paymentInquiryResult.IsSuccess)
                    {
                        document.CostPaymentDate = paymentInquiryResult.Data.PayCostDate;
                        document.CostPaymentTime = paymentInquiryResult.Data.PayCostTime;
                        document.ReceiptNo = paymentInquiryResult.Data.ReceiptNo;
                        document.BillNo = paymentInquiryResult.Data.BillNo;
                        document.PaymentType = paymentInquiryResult.Data.PaymentType;
                    }
                }


                //if (document.IsCostPaymentConfirmed == CostPaymentConfirmed.Uncertain.ToAssignedValue())
                //{
                //    var paymentInquiryResult = await _mediator.Send(new UpdateDocumentAfterPaidCommand() { DocumentNo = document.RequestNo }, cancellationToken);
                //    if (paymentInquiryResult.IsSuccess && paymentInquiryResult.Data.IsCostPaymentConfirmed !=null)
                //    {
                //        document.IsCostPaymentConfirmed = paymentInquiryResult.Data.IsCostPaymentConfirmed;
                //        document.State = paymentInquiryResult.Data.State;
                //        document.CostPaymentDate = paymentInquiryResult.Data.CostPaymentDate;
                //        document.CostPaymentTime = paymentInquiryResult.Data.CostPaymentTime;
                //        document.ReceiptNo = paymentInquiryResult.Data.ReceiptNo;
                //        document.BillNo = paymentInquiryResult.Data.BillNo;
                //        document.PaymentType = paymentInquiryResult.Data.PaymentType;
                //    }
                //}
                //#region CheckCostPaidState
                //if (document.IsCostPaymentConfirmed == CostPaymentConfirmed.Paid.ToAssignedValue() && document.State == NotaryRegServiceReqState.SetNationalDocumentNo.GetString())
                //{
                //    SaveDocumentCommand saveDocument = new();
                //    document.State = NotaryRegServiceReqState.CostPaid.GetString();
                //    saveDocument = DocumentMapper.MapDocumentToSaveDocumentCommand(document);
                //    var changeDocumentState = await _mediator.Send(saveDocument, cancellationToken);
                //    if (!changeDocumentState.IsSuccess)
                //    {
                //        apiResult.IsSuccess = true;
                //        apiResult.statusCode = ApiResultStatusCode.NotFound;
                //        apiResult.message.Add("هزینه ها پرداخت شده است اما وضعیت تغییر نکرده است لطفا با پشتیبانی تماس بگیرید.");
                //        return apiResult;
                //    }
                //}
                //#endregion
                result = DocumentMapper.MapDocumentToDocumentViewModel(document);
                GetPersonPhotoListServiceInput getPersonForNationalNo = new();
                getPersonForNationalNo.NationalNos = result.DocumentPeople.Where(x => x.IsPersonIranian == true && x.IsPersonSabteAhvalChecked == true && x.PersonType == "1").Select(x => x.PersonNationalNo).ToList();
                if (getPersonForNationalNo.NationalNos.Count > 0)
                {
                    ApiResult<GetPersonPhotoListViewModel> personalImages = new();
                    personalImages.IsSuccess = false;
                    GetPersonPhotoListFromSabteAhvalServiceInput getPerson = new();
                    getPerson.Persons = result.DocumentPeople.Where(x => x.IsPersonIranian == true && x.IsPersonSabteAhvalChecked == true && x.PersonType=="1").Select(x => new PersonPhotoListItem() { NationalNo = x.PersonNationalNo, BirthDate = x.PersonBirthDate }).ToList();
                    getPerson.MainObjectId=document.Id.ToString();
                    if (getPerson.Persons.Count > 0)
                        personalImages = await _mediator.Send(getPerson, cancellationToken);
                    foreach (DocumentPersonViewModel item in result.DocumentPeople)
                    {
                        if (personalImages.IsSuccess)
                        {
                            var personalImage = personalImages.Data.PersonsData.Where(x => x.NationalNo == item.PersonNationalNo).FirstOrDefault();
                            item.PersonalImage = personalImage != null && personalImage.PersonalImage != null ? Convert.ToBase64String(personalImage.PersonalImage) : null;
                        }
                        var personOrginal = document.DocumentPeople.FirstOrDefault(x => x.Id == item.PersonId.ToGuid());
                        if (personOrginal != null && personOrginal.DocumentPersonType != null)
                        {
                            item.PersonPost = personOrginal.DocumentPersonType.SingularTitle;

                        }

                        var personRelated = document.DocumentPersonRelatedDocuments.Where(x => x.AgentPersonId == Guid.Parse(item.PersonId)).OrderBy(x => x.RowNo).ToList();
                        foreach (var personRelatedItem in personRelated)
                        {
                            if (!item.PersonPost.IsNullOrWhiteSpace())
                                item.PersonPost += " و ";
                            item.PersonPost += personRelatedItem.AgentType.Title + " " + document.DocumentPeople.
                                Where(x => x.Id == personRelatedItem.MainPersonId).Select(x => x.Name + " " + x.Family).FirstOrDefault();
                        }
                    }
                }

                result.DocumentPeople = result.DocumentPeople.OrderBy(x => x.RowNo).ToList();
                DocumentInfoOtherObject documentInfoOtherObject = await _documentInfoOtherRepository
                    .GetDocumentInfoOtherInformation((Guid.Parse(result.RequestId)));
                result.RequestType.AssetTypeId = documentInfoOtherObject.AssetTypeId;
                result.RequestType.DocumentTypeSubjectId = documentInfoOtherObject.DocumentTypeSubjectId;
                _ = result.RequestType;
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