using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Shell;
using Notary.SSAA.BO.DataTransferObject.Coordinators.Estate;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.ENoteBook;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Messaging;
    using Notary.SSAA.BO.DataTransferObject.Commands.Document;
    using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;
    using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
    using Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract;
    using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;
    using Notary.SSAA.BO.DataTransferObject.Queries.Document;
    using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Domain.Abstractions.Base;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Constants;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.SharedKernel.Result;
    using Notary.SSAA.BO.SharedKernel.SharedModels;
    using Notary.SSAA.BO.Utilities;
    using Notary.SSAA.BO.Utilities.Extensions;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="SaveDocumentStandardContractCoordinator" />
    /// </summary>
    public class SaveDocumentStandardContractCoordinator
    {
        /// <summary>
        /// Defines the documentRepository
        /// </summary>
        private readonly IDocumentRepository documentRepository;

        /// <summary>
        /// Defines the _clientConfiguration
        /// </summary>
        private readonly ClientConfiguration _clientConfiguration;

        /// <summary>
        /// Defines the documentTypeRepository
        /// </summary>
        private readonly IDocumentTypeRepository documentTypeRepository;

        /// <summary>
        /// Defines the _personFingerprintRepository
        /// </summary>
        private readonly IPersonFingerprintRepository _personFingerprintRepository;

        /// <summary>
        /// Defines the _documentPersonRepository
        /// </summary>
        private readonly IDocumentPersonRepository _documentPersonRepository;

        /// <summary>
        /// Defines the _documentFileRepository
        /// </summary>
        private readonly IDocumentFileRepository _documentFileRepository;

        /// <summary>
        /// Defines the _documentSemaphoreRepository
        /// </summary>
        private readonly IRepository<DocumentSemaphore> _documentSemaphoreRepository;

        /// <summary>
        /// Defines the _documentElectronicBookRepository
        /// </summary>
        private readonly IDocumentElectronicBookRepository _documentElectronicBookRepository;

        /// <summary>
        /// Defines the _documentPersonRelatedRepository
        /// </summary>
        private readonly IDocumentPersonRelatedRepository _documentPersonRelatedRepository;

        /// <summary>
        /// Defines the _personOwnershipDocsInheritorCore
        /// </summary>
        private readonly PersonOwnershipDocsInheritorCore _personOwnershipDocsInheritorCore;

        /// <summary>
        /// Defines the _separationDealSummaryValidator
        /// </summary>
        private readonly SeparationDealSummaryValidator _separationDealSummaryValidator;

        /// <summary>
        /// Defines the _estateInquiryEngine
        /// </summary>
        private readonly EstateInquiryEngine _estateInquiryEngine;

        /// <summary>
        /// Defines the _eNoteBookServerController
        /// </summary>
        private readonly ENoteBookServerController _eNoteBookServerController;

        /// <summary>
        /// Defines the _signProvider
        /// </summary>
        private readonly SignProvider _signProvider;

        /// <summary>
        /// Defines the _quotasValidator
        /// </summary>
        private readonly QuotasValidator _quotasValidator;

        /// <summary>
        /// Defines the _smartQuotaGeneratorEngine
        /// </summary>
        private readonly SmartQuotaGeneratorEngine _smartQuotaGeneratorEngine;

        /// <summary>
        /// Defines the documentTemplateRepository
        /// </summary>
        private readonly IDocumentTemplateRepository documentTemplateRepository;

        /// <summary>
        /// Defines the dateTimeService
        /// </summary>
        private readonly IDateTimeService dateTimeService;

        /// <summary>
        /// Defines the estatePieceTypeRepository
        /// </summary>
        internal IRepository<EstatePieceType> estatePieceTypeRepository;

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
        public Document? document;

        /// <summary>
        /// Defines the loadDocumentCoordinator
        /// </summary>
        private LoadDocumentStandardContractCoordinator loadDocumentCoordinator;

        /// <summary>
        /// Defines the _mediator
        /// </summary>
        private IMediator _mediator;

        /// <summary>
        /// Defines the _messagingCore
        /// </summary>
        private MessagingCore _messagingCore;

        /// <summary>
        /// Defines the _applicationContextService
        /// </summary>
        private readonly IApplicationContextService _applicationContextService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveDocumentStandardContractCoordinator"/> class.
        /// </summary>
        /// <param name="mediator">The mediator<see cref="IMediator"/></param>
        /// <param name="_userService">The _userService<see cref="IUserService"/></param>
        /// <param name="_logger">The _logger<see cref="ILogger"/></param>
        /// <param name="_validatorGateway">The _validatorGateway<see cref="ValidatorGateway"/></param>
        /// <param name="_documentRepository">The _documentRepository<see cref="IDocumentRepository"/></param>
        /// <param name="_dateTimeService">The _dateTimeService<see cref="IDateTimeService"/></param>
        /// <param name="_documentTypeRepository">The _documentTypeRepository<see cref="IDocumentTypeRepository"/></param>
        /// <param name="documentPersonRepository">The documentPersonRepository<see cref="IDocumentPersonRepository"/></param>
        /// <param name="documentFileRepository">The documentFileRepository<see cref="IDocumentFileRepository"/></param>
        /// <param name="documentSemaphoreRepository">The documentSemaphoreRepository<see cref="IRepository{DocumentSemaphore}"/></param>
        /// <param name="documentElectronicBookRepository">The documentElectronicBookRepository<see cref="IDocumentElectronicBookRepository"/></param>
        /// <param name="documentPersonRelatedRepository">The documentPersonRelatedRepository<see cref="IDocumentPersonRelatedRepository"/></param>
        /// <param name="personOwnershipDocsInheritorCore">The personOwnershipDocsInheritorCore<see cref="PersonOwnershipDocsInheritorCore"/></param>
        /// <param name="_documentTemplateRepository">The _documentTemplateRepository<see cref="IDocumentTemplateRepository"/></param>
        /// <param name="personFingerprintRepository">The personFingerprintRepository<see cref="IPersonFingerprintRepository"/></param>
        /// <param name="separationDealSummaryValidator">The separationDealSummaryValidator<see cref="SeparationDealSummaryValidator"/></param>
        /// <param name="_loadDocumentCoordinator">The _loadDocumentCoordinator<see cref="LoadDocumentStandardContractCoordinator"/></param>
        /// <param name="estateInquiryEngine">The estateInquiryEngine<see cref="EstateInquiryEngine"/></param>
        /// <param name="clientConfiguration">The clientConfiguration<see cref="ClientConfiguration"/></param>
        /// <param name="messagingCore">The messagingCore<see cref="MessagingCore"/></param>
        /// <param name="eNoteBookServerController">The eNoteBookServerController<see cref="ENoteBookServerController"/></param>
        /// <param name="signProvider">The signProvider<see cref="SignProvider"/></param>
        /// <param name="quotasValidator">The quotasValidator<see cref="QuotasValidator"/></param>
        /// <param name="smartQuotaGeneratorEngine">The smartQuotaGeneratorEngine<see cref="SmartQuotaGeneratorEngine"/></param>
        /// <param name="applicationContextService">The applicationContextService<see cref="IApplicationContextService"/></param>
        public SaveDocumentStandardContractCoordinator(IMediator mediator, IUserService _userService, ILogger _logger, ValidatorGateway _validatorGateway
            , IDocumentRepository _documentRepository, IDateTimeService _dateTimeService, IDocumentTypeRepository _documentTypeRepository
            , IDocumentPersonRepository documentPersonRepository, IDocumentFileRepository documentFileRepository, IRepository<DocumentSemaphore> documentSemaphoreRepository,
            IDocumentElectronicBookRepository documentElectronicBookRepository, IDocumentPersonRelatedRepository documentPersonRelatedRepository, PersonOwnershipDocsInheritorCore personOwnershipDocsInheritorCore,
            IDocumentTemplateRepository _documentTemplateRepository, IPersonFingerprintRepository personFingerprintRepository, SeparationDealSummaryValidator separationDealSummaryValidator
            , LoadDocumentStandardContractCoordinator _loadDocumentCoordinator, EstateInquiryEngine estateInquiryEngine, ClientConfiguration clientConfiguration, MessagingCore messagingCore
            , ENoteBookServerController eNoteBookServerController, SignProvider signProvider, QuotasValidator quotasValidator,
            SmartQuotaGeneratorEngine smartQuotaGeneratorEngine, IApplicationContextService applicationContextService)
        {
            apiResult = new();
            documentRepository = _documentRepository;
            dateTimeService = _dateTimeService;
            validatorGateway = _validatorGateway;
            logger = _logger;
            userService = _userService;
            documentTypeRepository = _documentTypeRepository;
            documentTemplateRepository = _documentTemplateRepository;
            loadDocumentCoordinator = _loadDocumentCoordinator;
            _messagingCore = messagingCore;
            _clientConfiguration = clientConfiguration;
            _mediator = mediator;
            _personFingerprintRepository = personFingerprintRepository;
            _documentPersonRepository = documentPersonRepository;
            _documentFileRepository = documentFileRepository;
            _separationDealSummaryValidator = separationDealSummaryValidator;
            _estateInquiryEngine = estateInquiryEngine;
            _documentSemaphoreRepository = documentSemaphoreRepository;
            _documentElectronicBookRepository = documentElectronicBookRepository;
            _documentPersonRelatedRepository = documentPersonRelatedRepository;
            _personOwnershipDocsInheritorCore = personOwnershipDocsInheritorCore;
            _eNoteBookServerController = eNoteBookServerController;
            _signProvider = signProvider;
            _quotasValidator = quotasValidator;
            _smartQuotaGeneratorEngine = smartQuotaGeneratorEngine;
            _applicationContextService = applicationContextService;
        }

        /// <summary>
        /// The SaveDocument
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <param name="saveDocumentCommand">The saveDocumentCommand<see cref="SaveDocumentStandardContractCommand"/></param>
        /// <returns>The <see cref="Task{ApiResult}"/></returns>
        public async Task<ApiResult> SaveDocument(CancellationToken cancellationToken,
            SaveDocumentStandardContractCommand saveDocumentCommand)
        {
            if (document == null)
            {

                apiResult.IsSuccess = false;
                apiResult.message = new List<string> { "سندی برای ذخیره سازی وجود ندارد" };
                apiResult.statusCode = ApiResultStatusCode.NotFound;
            }
            else
            {

                if (documentRepository.IsNew(document))
                {
                    if (saveDocumentCommand.InquiriesId != null
                        && saveDocumentCommand.InquiriesId.Count > 0)
                    {
                        GetDocumentRelatedDataQuery documentRelatedDataQuery =
                            new GetDocumentRelatedDataQuery(saveDocumentCommand.InquiriesId.ToArray());
                        documentRelatedDataQuery.DocumentTypeCode = document.DocumentTypeId;
                        documentRelatedDataQuery.EstateInquiryId = saveDocumentCommand.InquiriesId.ToArray();
                        documentRelatedDataQuery.IsAttachment = saveDocumentCommand.IsAttachment;
                        documentRelatedDataQuery.IsRegistered = saveDocumentCommand.IsRegistered;
                        documentRelatedDataQuery.CheckRepeatedRequest = saveDocumentCommand.CheckRepeatedRequest;
                        (string?, bool) MessageInquiry = await GetInquiries(documentRelatedDataQuery, cancellationToken);
                        //if MessageInquiry.Item2==true then (HasAllarmMessage and result =success) else if MessageInquiry.Item2==false and MessageInquiry.Item1 != null then (errorMessage and result success=false)
                        if (MessageInquiry.Item1 != null) // errorMessage is not null
                        {
                            if (MessageInquiry.Item2) // HasAllarmMessage
                            {
                                apiResult.HasAllarmMessage = true;
                                apiResult.message = new List<string> { MessageInquiry.Item1 };
                                apiResult.statusCode = ApiResultStatusCode.Success;
                                return apiResult;
                            }
                            else
                            {
                                apiResult.IsSuccess = false;
                                apiResult.message = new List<string> { MessageInquiry.Item1 };
                                apiResult.statusCode = ApiResultStatusCode.Success;
                                return apiResult;

                            }

                        }

                    }

                    document.RecordDate = dateTimeService.CurrentDateTime;
                    document.RequestDate = dateTimeService.CurrentPersianDate;
                    document.RequestTime = dateTimeService.CurrentTime;
                    document.ScriptoriumId = userService.UserApplicationContext.BranchAccess.BranchCode;
                    document.RequestNo =
                        await CreateDocumentNo(userService.UserApplicationContext.BranchAccess.BranchCode,
                            cancellationToken);
                    var documentType = documentTypeRepository.GetById(saveDocumentCommand.DocumentTypeId.First());
                    document.DocumentTypeTitle = documentType?.Title;
                    document.State = NotaryRegServiceReqState.Created.GetString();
                    await documentRepository.SaveChanges(cancellationToken);
                    // await documentRepository.SaveChanges ( cancellationToken );

                }
                else
                {
                    if (saveDocumentCommand.StateId == NotaryRegServiceReqState.CanceledAfterGetCode)
                    {

                        bool isDSUSent = await _estateInquiryEngine.IsDSUSent4ThisRequest(saveDocumentCommand.RequestId, cancellationToken);

                        bool isRevoked = false;
                        string revokingMessage = string.Empty;

                        if (isDSUSent)
                        {
                            revokingMessage = "سند جاری دارای خلاصه معامله ارسال شده می باشد و امکان بی اثر نمودن این سند وجود ندارد.";
                            isRevoked = false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(saveDocumentCommand.RequestSignDate))
                            {
                                revokingMessage = "فیلد تاریخ امضاء سند ورود اطلاعات شده است. امکان بی اثر نمودن این سند وجود ندارد.";
                                isRevoked = false;
                            }
                            else
                            {
                                document.State = NotaryRegServiceReqState.CanceledAfterGetCode.GetString();
                                document.RequestDate = dateTimeService.CurrentPersianDate;
                                document.RequestTime = dateTimeService.CurrentTime;

                                //بلا اثر کردن پرداخت
                                //PaymentServices ps = new PaymentServices();
                                //string message = string.Empty;
                                //if (ps.DoEpaymentRefundable(theCurrentNotaryRegisterServiceReqEntity, out message))
                                //{
                                //    Rad.CMS.OjbBridge.TransactionContext.Current.Commit();
                                //}
                                //else
                                //{
                                //    messageText = message;
                                //    Rad.CMS.OjbBridge.TransactionContext.Current.Commit();
                                //    ONotaryRegisterServiceReqOutputMessage outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
                                //    outputError.Pack(this.ExecutionContext.SerializationContext);
                                //    return outputError;
                                //}

                                isRevoked = true;
                            }
                        }
                        if (!isRevoked)
                        {
                            apiResult.IsSuccess = false;
                            apiResult.statusCode = ApiResultStatusCode.Success;
                            apiResult.message.Add(revokingMessage);
                            return apiResult;
                        }
                    }
                    if (saveDocumentCommand.StateId == NotaryRegServiceReqState.CanceledAfterGetCode ||
                        saveDocumentCommand.StateId == NotaryRegServiceReqState.CanceledBeforeGetCode)
                    {
                        document.DocumentInfoConfirm.CreateDate = dateTimeService.CurrentPersianDate;
                        document.DocumentInfoConfirm.CreateTime = dateTimeService.CurrentTime;
                    }

                    if (document.NationalNo == null && document.State != NotaryRegServiceReqState.SetNationalDocumentNo.GetString() &&
                         saveDocumentCommand.StateId.GetString() == NotaryRegServiceReqState.SetNationalDocumentNo.GetString())
                    {

                        ONotaryRegisterServiceReqOutputMessage resut = await SetDocNoService(cancellationToken);
                        if (!resut.Message)
                        {
                            apiResult.IsSuccess = false;
                            apiResult.statusCode = ApiResultStatusCode.Success;
                            apiResult.message.Add(resut.MessageText);
                            return apiResult;
                        }
                        //  document.NationalNo = nationalNo;
                    }
                    else
                    if (document.State != NotaryRegServiceReqState.Finalized.GetString() && saveDocumentCommand.PrepareDocumentConfirmation &&
                         saveDocumentCommand.StateId.GetString() == NotaryRegServiceReqState.Finalized.GetString())
                    {
                        PrepareDocumentConfirmationRequest prepareDocumentConfirmationRequest =
                            new PrepareDocumentConfirmationRequest();
                        prepareDocumentConfirmationRequest.TheCurrentReq = document;
                        prepareDocumentConfirmationRequest.UnSignedPersonsListIds =
                            saveDocumentCommand.UnSignedPersonsListIds;
                        PrepareDocumentConfirmation prepareDocumentConfirmation = await PrepareDocumentConfirmation(prepareDocumentConfirmationRequest
                            , cancellationToken);

                        if (!prepareDocumentConfirmation.ServiceExecutionSucceeded)
                        {
                            apiResult.IsSuccess = false;
                            apiResult.statusCode = ApiResultStatusCode.Success;
                            apiResult.message.Add(prepareDocumentConfirmation.ServiceExecutionMessage);
                            return apiResult;
                        }
                    }
                    else
                    if (document.State != NotaryRegServiceReqState.Finalized.GetString() && saveDocumentCommand.SardaftarConfirm &&
                         saveDocumentCommand.StateId.GetString() == NotaryRegServiceReqState.Finalized.GetString())
                    {
                        var sardaftarConfirmInput = await PrepareSardaftarConfirmInput(saveDocumentCommand, cancellationToken);
                        var sardaftarConfirmResult = await SardaftarConfirm(sardaftarConfirmInput, cancellationToken);
                        if (!sardaftarConfirmResult.Message)
                        {
                            apiResult.IsSuccess = false;
                            apiResult.statusCode = ApiResultStatusCode.Success;
                            apiResult.message.Add(sardaftarConfirmResult.MessageText);
                            return apiResult;
                        }


                    }
                    else

                    if (document.State == NotaryRegServiceReqState.Finalized.GetString() &&
                        saveDocumentCommand.PrepareDaftaryarConfirm &&
                        saveDocumentCommand.StateId.GetString() == NotaryRegServiceReqState.Finalized.GetString())
                    {
                        var daftaryarConfirmInput = await PrepareDaftaryarConfirmInput(saveDocumentCommand, cancellationToken);
                        var daftaryarConfirmResult = await DaftaryarConfirm(daftaryarConfirmInput, cancellationToken);
                        if (!daftaryarConfirmResult.Message)
                        {
                            apiResult.IsSuccess = false;
                            apiResult.statusCode = ApiResultStatusCode.Success;
                            apiResult.message.Add(daftaryarConfirmResult.MessageText);
                            return apiResult;
                        }
                    }
                    else
                    {
                        document.RecordDate = dateTimeService.CurrentDateTime;
                        document.RequestDate = dateTimeService.CurrentPersianDate;
                        document.RequestTime = dateTimeService.CurrentTime;

                    }

                    //document.State = NotaryRegServiceReqState.Created.GetString ();
                    await documentRepository.UpdateAsync(document, cancellationToken);

                }

            }

            return
                apiResult;
        }

        /// <summary>
        /// The validateRelatedPerson
        /// </summary>
        /// <param name="document">The document<see cref="Document"/></param>
        /// <returns>The <see cref="Task{List{DocAgentValidationResponsPacket}?}"/></returns>
        public async Task<List<DocAgentValidationResponsPacket>?> validateRelatedPerson(Document document)
        {
            List<DocAgentValidationRequestPacket>? docAgentValidationInputPacketCollection =
                CollectDocAgentValidationRequests(document);
            string? overalMessageText = null;
            List<DocAgentValidationResponsPacket>? docAgentValidationResponsPacket =
                new List<DocAgentValidationResponsPacket>();
            (docAgentValidationResponsPacket, overalMessageText) =
                await validatorGateway.ValidateAgentDocumentsCollection(docAgentValidationInputPacketCollection,
                    document.Id.ToString(), document.DocumentType.Title);

            return docAgentValidationResponsPacket;
        }

        /// <summary>
        /// The CollectDocAgentValidationRequests
        /// </summary>
        /// <param name="document">The document<see cref="Document"/></param>
        /// <returns>The <see cref="List{DocAgentValidationRequestPacket}?"/></returns>
        private List<DocAgentValidationRequestPacket>? CollectDocAgentValidationRequests(Document document)
        {

            ICollection<DocumentPersonRelated> theDocRelatedPersonsList = document.DocumentPersonRelatedDocuments;
            ICollection<DocumentPerson> thePersonList = document.DocumentPeople;
            List<DocAgentValidationRequestPacket> docAgentValidationInputPacketCollection =
                new List<DocAgentValidationRequestPacket>();
            List<string> validationAgentTypes = new List<string>()
            {
                "1", // وکالتنامه 
                "13" // وصیت نامه
            };

            if (theDocRelatedPersonsList == null || theDocRelatedPersonsList.Count == 0)
                return null;

            foreach (DocumentPersonRelated theOneRelatedPerson in theDocRelatedPersonsList)
            {

                //Only If the AgentType Is "وکالتنامه"
                if (!validationAgentTypes.Contains(theOneRelatedPerson.AgentTypeId))
                    continue;

                //If DocAgent Is Not Validated It should be verified by extordium, mentioning the approving reason
                if (theOneRelatedPerson.IsAgentDocumentAbroad == "true" ||
       theOneRelatedPerson.IsRelatedDocumentInSsar == "false" ||
       theOneRelatedPerson.IsRelatedDocumentInSsar == null)
                {
                    continue;
                }

                //If The Validation Is Done Before, Don't Validate Again.
                //This "theOneDocAgent.ONotaryRegisterServiceReqId" is filled only if the validation is Succeeded Or The Current Agent Object change
                //if (this.IsAgentValidationRelatedObjectsEdited(theOneDocAgent))
                //{
                //    theOneDocAgent.ONotaryRegisterServiceReqId = null;
                //}
                //else
                //{
                //    if (!string.IsNullOrWhiteSpace(theOneDocAgent.ONotaryRegisterServiceReqId))
                //        continue;
                //}

                DocAgentValidationRequestPacket? docAgentValidationInputPacket =
                    ImplementValidationPacket(theOneRelatedPerson, document);
                if (docAgentValidationInputPacket != null)
                    docAgentValidationInputPacketCollection.Add(docAgentValidationInputPacket);

            }

            return docAgentValidationInputPacketCollection;
        }

        /// <summary>
        /// The ImplementValidationPacket
        /// </summary>
        /// <param name="theOneRelatedPerson">The theOneRelatedPerson<see cref="DocumentPersonRelated"/></param>
        /// <param name="document">The document<see cref="Document"/></param>
        /// <returns>The <see cref="DocAgentValidationRequestPacket?"/></returns>
        private DocAgentValidationRequestPacket? ImplementValidationPacket(DocumentPersonRelated theOneRelatedPerson,
            Document document)
        {
            ICollection<DocumentPerson> thePersonList = document.DocumentPeople;
            if (theOneRelatedPerson == null)
                return null;

            DocAgentValidationRequestPacket docAgentValidationInputPacket = new DocAgentValidationRequestPacket();

            docAgentValidationInputPacket.ID = Guid.NewGuid().ToString();
            docAgentValidationInputPacket.CurrentDocAgentObjectID = theOneRelatedPerson.Id.ToString();
            docAgentValidationInputPacket.DocumentDate = theOneRelatedPerson.AgentDocumentDate;
            docAgentValidationInputPacket.DocumentNationalNo = theOneRelatedPerson.AgentDocumentNo;
            docAgentValidationInputPacket.DocumentScriptoriumId = theOneRelatedPerson.DocumentScriptoriumId;
            docAgentValidationInputPacket.DocumentSecretCode = theOneRelatedPerson.AgentDocumentSecretCode;
            docAgentValidationInputPacket.MovakelFullName = thePersonList
                .Where(p => p.Id == theOneRelatedPerson.MainPersonId).FirstOrDefault()
                ?.FullName(); //theOneDocAgent.TheMainPrs.FullName;
            docAgentValidationInputPacket.MovakelNationalNo = thePersonList
                .Where(p => p.Id == theOneRelatedPerson.MainPersonId).FirstOrDefault()
                ?.NationalNo; // theOneDocAgent.TheMainPrs.NationalNo;
            docAgentValidationInputPacket.VakilFullName = thePersonList
                .Where(p => p.Id == theOneRelatedPerson.AgentPersonId).FirstOrDefault()?.FullName();
            ; //theOneDocAgent.TheAgentPrs.FullName;
            docAgentValidationInputPacket.VakilNationalNo = thePersonList
                .Where(p => p.Id == theOneRelatedPerson.AgentPersonId).FirstOrDefault()?.NationalNo;
            ; //theOneDocAgent.TheAgentPrs.NationalNo;

            // IONotaryRegisterServiceReq theCurrentRegisterServiceReq = (theOneDocAgent.TheMainPrs != null) ? theOneDocAgent.TheMainPrs.TheONotaryRegisterServiceReq : theOneDocAgent.TheAgentPrs.TheONotaryRegisterServiceReq;
            docAgentValidationInputPacket.DocumentTypeID = document.DocumentTypeId;

            //if ( theCurrentRegisterServiceReq.TheONotaryRegCaseList.ListHasElement () )
            //{
            //    docAgentValidationInputPacket.CurrentRegCasesCollection = new List<IONotaryRegCase> ();
            //    foreach ( IONotaryRegCase theOneCase in theCurrentRegisterServiceReq.TheONotaryRegCaseList )
            //    {
            //        docAgentValidationInputPacket.CurrentRegCasesCollection.Add ( theOneCase );
            //    }
            //}

            if (theOneRelatedPerson.DocumentSmsId == null)
                docAgentValidationInputPacket.SMSIsRequired = true;
            else
                docAgentValidationInputPacket.SMSIsRequired = false;

            return docAgentValidationInputPacket;
        }

        /// <summary>
        /// The NewDocument
        /// </summary>
        public void NewDocument(SaveDocumentStandardContractCommand request)
        {
            document = new();
            document.ScriptoriumId = userService.UserApplicationContext.BranchAccess.BranchCode;
            document.Ilm = DocumentConstants.CreateIlm;
            document.State = NotaryRegServiceReqState.Created.GetString();
            if (request.IsRemoteRequest==true && Guid.TryParse(request.RequestId, out Guid documentId))
            {
                document.Id = documentId;
            }
            else
            {
                document.Id = Guid.NewGuid();

            }

            documentRepository.Add(document, false);
        }

        /// <summary>
        /// The LoadDocument
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <param name="datailList">The datailList<see cref="List{string}"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task LoadDocument(string id, List<string> datailList, CancellationToken cancellationToken)
        {
            document = await documentRepository.GetDocumentById(id.ToGuid(), datailList, cancellationToken);
        }

        /// <summary>
        /// The LoadDocumentForWorkflowProcess
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task LoadDocumentForWorkflowProcess(string id, CancellationToken cancellationToken)
        {
            document = await documentRepository.GetDocumentById(id.ToGuid(), new List<string>() { "DocumentPeople", "DocumentInquiries", "DocumentEstates", "DocumentInfoConfirm", "DocumentInfoText" }, cancellationToken);
            documentRepository.Attach(document);
        }

        /// <summary>
        /// The ValidateDocumentViewModel
        /// </summary>
        /// <param name="request">The request<see cref="SaveDocumentStandardContractCommand"/></param>
        /// <returns>The <see cref="(bool, List{string})"/></returns>
        public (bool, List<string>) ValidateDocumentViewModel(SaveDocumentStandardContractCommand request)
        {

            List<string> messages = new List<string>();
            bool isValid = false;

            if (document != null)
            {
                //if(request.IsRequestInSetNationalDocumentNo() && (!string.IsNullOrEmpty( document.NationalNo ) || document.State==NotaryRegServiceReqState.SetNationalDocumentNo.GetString() ))
                //{
                //    messages.Add ( " وضعیت سند نادرست است " );
                //    isValid = false;
                //}

                foreach (var item in request.DocumentRelatedPeople)
                {
                    if (item.IsNew)
                    {
                        if (document.DocumentPersonRelatedDocuments.Any(x =>
                                x.MainPersonId == item.MainPersonId.First().ToGuid()
                                && x.AgentPersonId == item.RelatedAgentPersonId.First().ToGuid() &&
                                x.AgentTypeId == item.RelatedAgentTypeId.First()))
                        {
                            messages.Add("شخص وابسته تکراری است .");
                        }
                    }

                    if (!item.IsDelete && (item.IsDirty || item.IsNew))
                    {
                        if (!(request.IsRemoteRequest == true))
                        {
                            if (!document.DocumentPeople.Any(x => x.Id == item.MainPersonId.First().ToGuid()))
                            {

                                messages.Add("شخص اصیل شخص وابسته حذف شده است");
                            }

                            if (!document.DocumentPeople.Any(x => x.Id == item.RelatedAgentPersonId.First().ToGuid()))
                            {

                                messages.Add("شخص نماینده شخص وابسته حذف شده است ");
                            }
                        }

                        var validateRelatedAgentDocumentIssuer = item.validateRelatedAgentDocumentIssuer();
                        if (validateRelatedAgentDocumentIssuer != null)
                        {
                            messages.AddRange(validateRelatedAgentDocumentIssuer);
                        }

                        if (item.RelatedAgentTypeId.First() == "10")
                        {
                            if (item.RelatedReliablePersonReasonId.Count < 1)
                            {
                                messages.Add("فیلد دلیل نیاز به معتمد اجباری است .");
                            }
                        }

                        if (item.RelatedAgentTypeId.First() is not "3" and not "10" and not "11" and not "12")
                        {
                            if (!ValidatorHelper.BeValidPersianDate(item.RelatedAgentDocumentDate) ||
                                item.RelatedAgentDocumentDate?.GetDateTimeDistance(dateTimeService
                                    .CurrentPersianDateTime) > TimeSpan.FromDays(1))
                            {
                                messages.Add("مقدار تاریخ وکالتنامه غیر مجاز است ");
                            }

                            if (string.IsNullOrWhiteSpace(item.RelatedAgentDocumentNo) ||
                                item.RelatedAgentDocumentNo.Length > 50)
                            {
                                messages.Add("فیلد شماره وکالتنامه اجباری است .");
                            }

                            //if (string.IsNullOrWhiteSpace(item.RelatedAgentDocumentIssuer))
                            //{
                            //    messages.Add("فیلد مرجع صدور اجباری میباشد . ");
                            //}
                        }

                    }

                    if (item.IsDirty && item.IsNew == false)
                    {
                        if (!document.DocumentPersonRelatedDocuments.Any(x => x.Id == item.RelatedPersonId.ToGuid()))
                        {
                            messages.Add(" شخص وابسته حذف شده است ");
                        }
                    }
                }

                foreach (var item in request.DocumentPeople)
                {

                    if (item.IsDelete)
                    {
                        if (document.DocumentPersonRelatedDocuments.Any(x => x.MainPersonId == item.PersonId.ToGuid())
                            || document.DocumentPersonRelatedDocuments.Any(x =>
                                x.AgentPersonId == item.PersonId.ToGuid()))
                        {
                            messages.Add($"شخص با کد ملی {item.PersonNationalNo} دارای فرد وابسته میباشد ");
                        }
                    }

                    if (item.IsDirty && !item.IsNew && !item.IsDelete)
                    {
                        if (!document.DocumentPeople.Any(x => x.Id == item.PersonId.ToGuid()))
                        {
                            messages.Add($"شخص با شماره ملی {item.PersonNationalNo} حذف شده است ");
                        }
                    }
                }

                if (request.DocumentCostQuestion != null)
                {
                    if (request.DocumentCostQuestion.IsDirty || request.DocumentCostQuestion.IsNew)
                    {
                        var validateDocumentCostQuestionIsEstateRegister =
                            request.DocumentCostQuestion.validateIsEstateRegisterd(document?.DocumentType?.Code,
                                document?.DocumentType?.Title);
                        if (validateDocumentCostQuestionIsEstateRegister != null)
                        {
                            messages.AddRange(validateDocumentCostQuestionIsEstateRegister);
                        }
                    }
                }

            }
            else
            {
                messages.Add("سند مربوطه یافت نشد ");
            }

            if (messages.Count > 0)
            {
                isValid = false;
            }
            else isValid = true;

            return (isValid, messages);
        }

        /// <summary>
        /// The MapToDocument
        /// </summary>
        /// <param name="request">The request<see cref="SaveDocumentStandardContractCommand"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task MapToDocument(SaveDocumentStandardContractCommand request, CancellationToken cancellationToken)
        {
            if (request.IsRequestInSetNationalDocumentNo() || request.IsRequestInFinalized())
                return;
            DocumentStandardContractMapper.MapToDocumentStandardContract(ref document, request);
            if (document.ScriptoriumId == null)
            {
                document.ScriptoriumId = userService.UserApplicationContext.ScriptoriumInformation.Id;
            }

            // document.State = createEnumDocumentState.ToString ();
            short rowNoCounter = 1;
            short newCounter = 0;
            int rowNoMax = 0;
            bool isDeletePersonRequest = false;
            if (request.DocumentCostQuestion != null)
            {
                if (request.DocumentCostQuestion.IsNew || request.DocumentCostQuestion.IsDirty)
                {
                    document.IsCostCalculateConfirmed =
                        request.DocumentCostQuestion.IsRequestCostCalculateConfirmed.ToYesNo();
                }
            }

            if (request.DocumentPeople?.Count > 0)
            {
                rowNoMax = 0;
                if (document.DocumentPeople.Count > 0)
                    rowNoMax = document.DocumentPeople.Count <= document.DocumentPeople.Max(x => x.RowNo)
                        ? document.DocumentPeople.Max(x => x.RowNo) -
                          request.DocumentPeople.Where(p => p.IsDelete).Count()
                        : document.DocumentPeople.Count - request.DocumentPeople.Where(p => p.IsDelete).Count();
                newCounter = (short)(request.DocumentPeople.Where(x => x.IsNew).Count() + rowNoMax);

                foreach (DocumentStandardContractPersonViewModel DocumentStandardContractPersonViewModel in request.DocumentPeople)
                {
                    if (DocumentStandardContractPersonViewModel.IsNew)
                    {
                        DocumentPerson newDocumentPerson = new();
                        DocumentStandardContractPersonMapper.MapToDocumentStandardContractPerson(ref newDocumentPerson, DocumentStandardContractPersonViewModel, request.IsRemoteRequest);
                        newDocumentPerson.ScriptoriumId = document.ScriptoriumId;
                        newDocumentPerson.RowNo = newCounter;
                        newDocumentPerson.Ilm = DocumentConstants.CreateIlm;

                        newDocumentPerson.IsFingerprintGotten = DocumentTemporaryConstants.IsFingerprintGotten;
                        if (DocumentStandardContractPersonViewModel.HasGrowthJudgment == true)
                        {
                            newDocumentPerson.HasGrowthJudgment = DocumentStandardContractPersonViewModel.HasGrowthJudgment.ToYesNo();
                            newDocumentPerson.GrowthDescription = DocumentStandardContractPersonViewModel.GrowthDescription;
                            newDocumentPerson.GrowthJudgmentDate = DocumentStandardContractPersonViewModel.GrowthJudgmentDate;
                            newDocumentPerson.GrowthJudgmentNo = DocumentStandardContractPersonViewModel.GrowthJudgmentNo;
                            newDocumentPerson.GrowthLetterDate = DocumentStandardContractPersonViewModel.GrowthLetterDate;
                            newDocumentPerson.GrowthLetterNo = DocumentStandardContractPersonViewModel.GrowthLetterNo;
                        }
                        else
                        {
                            newDocumentPerson.HasGrowthJudgment = DocumentStandardContractPersonViewModel.HasGrowthJudgment.ToYesNo();
                            newDocumentPerson.GrowthDescription = null;
                            newDocumentPerson.GrowthJudgmentDate = null;
                            newDocumentPerson.GrowthJudgmentNo = null;
                            newDocumentPerson.GrowthLetterDate = null;
                            newDocumentPerson.GrowthLetterNo = null;
                        }
                        if (DocumentStandardContractPersonViewModel.IsMartyrApplicant == true)
                        {
                            newDocumentPerson.IsMartyrApplicant =
                                DocumentStandardContractPersonViewModel.IsMartyrApplicant.ToYesNo();
                            newDocumentPerson.IsMartyrIncluded = DocumentStandardContractPersonViewModel.MartyrIsIncluded.ToYesNo();
                            if (!string.IsNullOrEmpty(DocumentStandardContractPersonViewModel.MartyrInquiryDateTime))
                            {
                                if (DocumentStandardContractPersonViewModel.MartyrInquiryDateTime.Length > 10)
                                {
                                    newDocumentPerson.MartyrInquiryDate = DocumentStandardContractPersonViewModel.MartyrInquiryDateTime.Substring(0, 10);
                                    newDocumentPerson.MartyrInquiryTime = DocumentStandardContractPersonViewModel.MartyrInquiryDateTime.Substring(10);
                                }
                                else
                                {
                                    newDocumentPerson.MartyrInquiryDate = DocumentStandardContractPersonViewModel.MartyrInquiryDateTime;

                                }
                            }
                            newDocumentPerson.MartyrInquiryDate = DocumentStandardContractPersonViewModel.MartyrInquiryDateTime;
                            newDocumentPerson.MartyrCode = DocumentStandardContractPersonViewModel.MartyrCode;
                            newDocumentPerson.MartyrDescription = DocumentStandardContractPersonViewModel.MartyrDescription;
                        }
                        else
                        {
                            newDocumentPerson.IsMartyrApplicant =
                              DocumentStandardContractPersonViewModel.IsMartyrApplicant.ToYesNo();
                            newDocumentPerson.IsMartyrIncluded = null;
                            newDocumentPerson.MartyrInquiryDate = null;
                            newDocumentPerson.MartyrInquiryTime = null;
                            newDocumentPerson.MartyrCode = null;
                            newDocumentPerson.MartyrDescription = null;
                        }
                        if (DocumentStandardContractPersonViewModel.IsDirty)
                        {

                        }

                        document.DocumentPeople.Add(newDocumentPerson);
                        newCounter--;

                    }

                    else if (DocumentStandardContractPersonViewModel.IsDelete)
                    {
                        _ = document.DocumentPeople.Remove(
                            document.DocumentPeople.First(x => x.Id == DocumentStandardContractPersonViewModel.PersonId.ToGuid()));
                        isDeletePersonRequest = true;

                    }
                    else if (!DocumentStandardContractPersonViewModel.IsDirty)
                    {
                        DocumentPerson updatingDocumentPerson =
                            document.DocumentPeople.First(x => x.Id == DocumentStandardContractPersonViewModel.PersonId.ToGuid());
                        //updatingDocumentPerson.RowNo = rowNoCounter;
                        rowNoCounter++;

                    }

                }

                IList<DocumentStandardContractPersonViewModel> documentPeopleDirty =
                    request.DocumentPeople.Where(p => p.IsDirty && !p.IsNew && !p.IsDelete).ToList();
                ICollection<DocumentPerson> dbDocumentPeople;
                IList<Guid> documentPeopleDeleteId;
                IList<Guid> documentPeopleDirtyId;
                if (isDeletePersonRequest)
                {
                    documentPeopleDeleteId = request.DocumentPeople.Where(p => p.IsDelete)
                        .Select(p => Guid.Parse(p.PersonId)).ToList();
                    dbDocumentPeople = document.DocumentPeople.Where(x => !documentPeopleDeleteId.Contains(x.Id))
                        .OrderBy(x => x.RowNo).ToList();

                }
                else
                {
                    documentPeopleDirtyId = documentPeopleDirty.Select(p => Guid.Parse(p.PersonId)).ToList();
                    dbDocumentPeople = document.DocumentPeople.Where(x => documentPeopleDirtyId.Contains(x.Id))
                        .OrderBy(x => x.RowNo).ToList();
                }

                foreach (DocumentPerson dbDocumentPerson in dbDocumentPeople)
                {
                    DocumentPerson updatingDocumentPerson = dbDocumentPerson;
                    DocumentStandardContractPersonViewModel? documentPersonViewModel =
                        documentPeopleDirty.FirstOrDefault(pd => pd.PersonId == updatingDocumentPerson.Id.ToString());
                    if (documentPersonViewModel != null)
                    {
                        DocumentStandardContractPersonMapper.MapToDocumentStandardContractPerson(ref updatingDocumentPerson, documentPersonViewModel,request.IsRemoteRequest);
                        if (documentPersonViewModel.HasGrowthJudgment == true)
                        {
                            updatingDocumentPerson.HasGrowthJudgment =
                                documentPersonViewModel.HasGrowthJudgment.ToYesNo();
                            updatingDocumentPerson.GrowthDescription = documentPersonViewModel.GrowthDescription;
                            updatingDocumentPerson.GrowthJudgmentDate = documentPersonViewModel.GrowthJudgmentDate;
                            updatingDocumentPerson.GrowthJudgmentNo = documentPersonViewModel.GrowthJudgmentNo;
                            updatingDocumentPerson.GrowthLetterDate = documentPersonViewModel.GrowthLetterDate;
                            updatingDocumentPerson.GrowthLetterNo = documentPersonViewModel.GrowthLetterNo;
                        }
                        else
                        {
                            updatingDocumentPerson.HasGrowthJudgment =
                                documentPersonViewModel.HasGrowthJudgment.ToYesNo();
                            updatingDocumentPerson.GrowthDescription = null;
                            updatingDocumentPerson.GrowthJudgmentDate = null;
                            updatingDocumentPerson.GrowthJudgmentNo = null;
                            updatingDocumentPerson.GrowthLetterDate = null;
                            updatingDocumentPerson.GrowthLetterNo = null;
                        }
                        if (documentPersonViewModel.IsMartyrApplicant == true)
                        {
                            updatingDocumentPerson.IsMartyrApplicant =
                                documentPersonViewModel.IsMartyrApplicant.ToYesNo();
                            updatingDocumentPerson.IsMartyrIncluded = documentPersonViewModel.MartyrIsIncluded.ToYesNo();
                            if (!string.IsNullOrEmpty(documentPersonViewModel.MartyrInquiryDateTime))
                            {
                                if (documentPersonViewModel.MartyrInquiryDateTime.Length > 10)
                                {
                                    updatingDocumentPerson.MartyrInquiryDate = documentPersonViewModel.MartyrInquiryDateTime.Substring(0, 10);
                                    updatingDocumentPerson.MartyrInquiryTime = documentPersonViewModel.MartyrInquiryDateTime.Substring(10);
                                }
                                else
                                {
                                    updatingDocumentPerson.MartyrInquiryDate = documentPersonViewModel.MartyrInquiryDateTime;

                                }
                            }
                            updatingDocumentPerson.MartyrInquiryDate = documentPersonViewModel.MartyrInquiryDateTime;
                            updatingDocumentPerson.MartyrCode = documentPersonViewModel.MartyrCode;
                            updatingDocumentPerson.MartyrDescription = documentPersonViewModel.MartyrDescription;
                        }
                        else
                        {
                            updatingDocumentPerson.IsMartyrApplicant =
                              documentPersonViewModel.IsMartyrApplicant.ToYesNo();
                            updatingDocumentPerson.IsMartyrIncluded = null;
                            updatingDocumentPerson.MartyrInquiryDate = null;
                            updatingDocumentPerson.MartyrInquiryTime = null;
                            updatingDocumentPerson.MartyrCode = null;
                            updatingDocumentPerson.MartyrDescription = null;
                        }
                    }

                    if (isDeletePersonRequest)
                    {

                        updatingDocumentPerson.RowNo = rowNoCounter;
                        rowNoCounter++;
                    }

                }
            }

            if (request.DocumentRelatedPeople?.Count > 0)
            {

                isDeletePersonRequest = false;
                rowNoCounter = 1;
                rowNoMax = 0;
                if (document.DocumentPersonRelatedDocuments.Count > 0)
                    rowNoMax = document.DocumentPersonRelatedDocuments.Count <=
                               document.DocumentPersonRelatedDocuments.Max(x => x.RowNo)
                        ? document.DocumentPersonRelatedDocuments.Max(x => x.RowNo) -
                          request.DocumentRelatedPeople.Where(p => p.IsDelete).Count()
                        : document.DocumentPersonRelatedDocuments.Count -
                          request.DocumentRelatedPeople.Where(p => p.IsDelete).Count();
                newCounter = (short)(request.DocumentRelatedPeople.Where(x => x.IsNew).Count() + rowNoMax);

                foreach (DocumentStandardContractRelatedPersonViewModel DocumentStandardContractRelatedPersonViewModel in request.DocumentRelatedPeople)
                {
                    if (DocumentStandardContractRelatedPersonViewModel.IsNew)
                    {
                        DocumentPersonRelated newDocumentPersonRelated = new();
                        DocumentStandardContractRelatedPersonMapper.MapToDocumentStandardContractRelatedPerson(ref newDocumentPersonRelated,
                            DocumentStandardContractRelatedPersonViewModel,request.IsRemoteRequest);
                        newDocumentPersonRelated.RowNo = (byte)newCounter;
                        newDocumentPersonRelated.Ilm = DocumentConstants.UpdateIlm;
                        newDocumentPersonRelated.DocumentScriptoriumId = document.ScriptoriumId;
                        if (DocumentStandardContractRelatedPersonViewModel.IsDirty)
                        {

                        }

                        document.DocumentPersonRelatedDocuments.Add(newDocumentPersonRelated);
                        newCounter--;

                    }
                    else if (DocumentStandardContractRelatedPersonViewModel.IsDelete)
                    {
                        _ = document.DocumentPersonRelatedDocuments.Remove(
                            document.DocumentPersonRelatedDocuments.First(x =>
                                x.Id == DocumentStandardContractRelatedPersonViewModel.RelatedPersonId.ToGuid()));
                        isDeletePersonRequest = true;
                    }

                    else if (!DocumentStandardContractRelatedPersonViewModel.IsDirty)
                    {
                        DocumentPersonRelated updatingDocumentPersonRelated =
                            document.DocumentPersonRelatedDocuments.First(x =>
                                x.Id == DocumentStandardContractRelatedPersonViewModel.RelatedPersonId.ToGuid());
                        //           updatingDocumentEstate.RowNo = (byte)rowNoCounter;
                        rowNoCounter++;

                    }

                }

                IList<DocumentStandardContractRelatedPersonViewModel> documentRelatedPersonDirty = request.DocumentRelatedPeople
                    .Where(p => p.IsDirty && !p.IsNew && !p.IsDelete).ToList();
                ICollection<DocumentPersonRelated> dbDocumentRelatedPeople;
                IList<Guid> documentRelatedPeopleDeleteId;
                IList<Guid> documentRelatedPeopleDirtyId;
                if (isDeletePersonRequest)
                {
                    documentRelatedPeopleDeleteId = request.DocumentRelatedPeople.Where(p => p.IsDelete)
                        .Select(p => Guid.Parse(p.RelatedPersonId)).ToList();
                    dbDocumentRelatedPeople = document.DocumentPersonRelatedDocuments
                        .Where(x => !documentRelatedPeopleDeleteId.Contains(x.Id)).OrderBy(x => x.RowNo).ToList();

                }
                else
                {
                    documentRelatedPeopleDirtyId =
                        documentRelatedPersonDirty.Select(p => Guid.Parse(p.RelatedPersonId)).ToList();
                    dbDocumentRelatedPeople = document.DocumentPersonRelatedDocuments
                        .Where(x => documentRelatedPeopleDirtyId.Contains(x.Id)).OrderBy(x => x.RowNo).ToList();
                }

                foreach (DocumentPersonRelated dbDocumentRelatedPerson in dbDocumentRelatedPeople)
                {
                    DocumentPersonRelated updatingDocumentRelatedPerson = dbDocumentRelatedPerson;
                    DocumentStandardContractRelatedPersonViewModel? documentEstateViewModel =
                        documentRelatedPersonDirty.FirstOrDefault(pd =>
                            pd.RelatedPersonId == updatingDocumentRelatedPerson.Id.ToString());
                    if (documentEstateViewModel != null)
                    {
                        DocumentStandardContractRelatedPersonMapper.MapToDocumentStandardContractRelatedPerson(ref updatingDocumentRelatedPerson,
                            documentEstateViewModel,request.IsRemoteRequest);

                    }

                    if (isDeletePersonRequest)
                    {

                        updatingDocumentRelatedPerson.RowNo = rowNoCounter;
                        rowNoCounter++;
                    }
                }

            }

            if (request.DocumentEstates?.Count > 0)
            {
                foreach (var documentEstate in request.DocumentEstates)
                {
                    foreach (var documentEstateAttachment in documentEstate.DocumentEstateAttachments)
                    {
                        if (documentEstateAttachment.IsDirty == true || documentEstateAttachment.IsNew == true)
                        {
                            documentEstate.IsDirty = true;
                        }
                    }

                    foreach (var documentEstateOwnerShip in documentEstate.DocumentEstateOwnerShips)
                    {
                        if (documentEstateOwnerShip.IsDirty == true || documentEstateOwnerShip.IsNew == true)
                        {
                            documentEstate.IsDirty = true;
                        }
                    }

                    foreach (var documentEstateSeparation in documentEstate.DocumentEstateSeparationPieces)
                    {
                        if (documentEstateSeparation.IsDirty == true || documentEstateSeparation.IsNew == true)
                        {
                            documentEstate.IsDirty = true;
                        }
                        foreach (var DocumentEstateSeparationPiecesQuota in documentEstateSeparation.DocumentEstateSeparationPiecesQuotaList)
                        {
                            if (DocumentEstateSeparationPiecesQuota.IsDirty == true || DocumentEstateSeparationPiecesQuota.IsNew == true)
                            {
                                documentEstate.IsDirty = true;
                                documentEstateSeparation.IsDirty = true;
                            }
                        }
                    }

                    foreach (var documentEstateQuotaDetail in documentEstate.DocumentEstateQuotaDetails)
                    {
                        if (documentEstateQuotaDetail.IsDirty == true || documentEstateQuotaDetail.IsNew == true)
                        {
                            documentEstate.IsDirty = true;
                        }
                    }

                    foreach (var documentEstateQuotum in documentEstate.DocumentEstateQuotums)
                    {
                        if (documentEstateQuotum.IsDirty == true || documentEstateQuotum.IsNew == true)
                        {
                            documentEstate.IsDirty = true;
                        }
                    }

                }

                isDeletePersonRequest = false;
                rowNoCounter = 1;
                rowNoMax = 0;
                if (document.DocumentEstates.Count > 0)
                    rowNoMax = document.DocumentEstates.Count <= document.DocumentEstates.Max(x => x.RowNo)
                        ? document.DocumentEstates.Max(x => x.RowNo) -
                          request.DocumentEstates.Where(p => p.IsDelete).Count()
                        : document.DocumentEstates.Count - request.DocumentEstates.Where(p => p.IsDelete).Count();
                newCounter = (short)(request.DocumentEstates.Where(x => x.IsNew).Count() + rowNoMax);

                foreach (DocumentStandardContractEstateViewModel DocumentStandardContractEstateViewModel in request.DocumentEstates)
                {
                    if (DocumentStandardContractEstateViewModel.IsNew)
                    {
                        DocumentEstate newDocumentEstate = new();
                        DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstate(ref newDocumentEstate, DocumentStandardContractEstateViewModel,request.IsRemoteRequest);
                        newDocumentEstate.ScriptoriumId = document.ScriptoriumId;
                        newDocumentEstate.RowNo = (byte)newCounter;
                        newDocumentEstate.Ilm = DocumentConstants.UpdateIlm;
                        if (DocumentStandardContractEstateViewModel.IsDirty)
                        {

                        }

                        if (DocumentStandardContractEstateViewModel.DocumentEstateOwnerShips?.Count > 0)
                        {
                            int rowNoMaxOwnerShip = 0;
                            if (newDocumentEstate.DocumentEstateOwnershipDocuments?.Count > 0)
                                rowNoMaxOwnerShip = newDocumentEstate.DocumentEstateOwnershipDocuments.Count <=
                                                    newDocumentEstate.DocumentEstateOwnershipDocuments.Max(x => x.RowNo)
                                    ? newDocumentEstate.DocumentEstateOwnershipDocuments.Max(x => x.RowNo) -
                                      DocumentStandardContractEstateViewModel.DocumentEstateOwnerShips.Where(p => p.IsDelete).Count()
                                    : newDocumentEstate.DocumentEstateOwnershipDocuments.Count - DocumentStandardContractEstateViewModel
                                        .DocumentEstateOwnerShips.Where(p => p.IsDelete).Count();
                            int newCounterOwnerShip =
                                (short)(DocumentStandardContractEstateViewModel.DocumentEstateOwnerShips.Where(x => x.IsNew).Count() +
                                        rowNoMaxOwnerShip);

                            foreach (DocumentStandardContractEstateOwnerShipViewModel documentEstateOwnerShipViewModel in
                                     DocumentStandardContractEstateViewModel.DocumentEstateOwnerShips)
                            {
                                if (documentEstateOwnerShipViewModel.IsNew)
                                {
                                    DocumentEstateOwnershipDocument documentEstateOwnershipDocument = new();
                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstate(ref documentEstateOwnershipDocument,
                                        documentEstateOwnerShipViewModel,request.IsRemoteRequest);
                                    documentEstateOwnershipDocument.DocumentEstateId = newDocumentEstate.Id;
                                    documentEstateOwnershipDocument.RowNo = (byte)newCounterOwnerShip;
                                    documentEstateOwnershipDocument.ScriptoriumId = document.ScriptoriumId;
                                    documentEstateOwnershipDocument.Ilm = DocumentConstants.CreateIlm;
                                    newDocumentEstate.DocumentEstateOwnershipDocuments?.Add(
                                        documentEstateOwnershipDocument);
                                    newCounterOwnerShip--;
                                }
                            }

                        }

                        if (DocumentStandardContractEstateViewModel.DocumentEstateAttachments?.Count > 0)
                        {
                            int rowNoMaxAttachment = 0;
                            if (newDocumentEstate.DocumentEstateAttachments?.Count > 0)
                                rowNoMaxAttachment = newDocumentEstate.DocumentEstateAttachments.Max(x => x.RowNo);
                            short newCounterAttachment =
                                (short)(DocumentStandardContractEstateViewModel.DocumentEstateAttachments.Where(x => x.IsNew).Count() +
                                        rowNoMaxAttachment);
                            if (newDocumentEstate.DocumentEstateAttachments != null)
                            {
                                foreach (var item in newDocumentEstate.DocumentEstateAttachments)
                                {

                                    item.DocumentEstateId = newDocumentEstate.Id;
                                    item.ScriptoriumId = document.ScriptoriumId;
                                    if (item.RowNo <= 0)
                                    {
                                        item.RowNo = newCounterAttachment;
                                        newCounterAttachment--;
                                    }
                                }
                            }

                            foreach (DocumentStandardContractEstateAttachmentViewModel documentEstateAttachmentViewModel in
                                     DocumentStandardContractEstateViewModel.DocumentEstateAttachments)
                            {
                                if (documentEstateAttachmentViewModel.IsNew)
                                {
                                    DocumentEstateAttachment documentEstateAttachment = new();
                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateAttachment(ref documentEstateAttachment,
                                        documentEstateAttachmentViewModel);
                                    documentEstateAttachment.DocumentEstateId = newDocumentEstate.Id;
                                    documentEstateAttachment.RowNo = (byte)newCounterAttachment;
                                    documentEstateAttachment.ScriptoriumId = document.ScriptoriumId;
                                    documentEstateAttachment.Ilm = DocumentConstants.CreateIlm;
                                    newDocumentEstate.DocumentEstateAttachments?.Add(documentEstateAttachment);
                                }
                            }
                        }

                        if (DocumentStandardContractEstateViewModel.DocumentEstateSeparationPieces?.Count > 0)
                        {

                            foreach (DocumentStandardContractEstateSeparationPieceViewModel documentEstateSeparationViewModel in
                                     DocumentStandardContractEstateViewModel.DocumentEstateSeparationPieces)
                            {
                                if (documentEstateSeparationViewModel.IsNew)
                                {
                                    DocumentEstateSeparationPiece documentEstateSeparation = new();
                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstate(ref documentEstateSeparation,
                                        documentEstateSeparationViewModel);
                                    documentEstateSeparation.DocumentEstateId = newDocumentEstate.Id;
                                    documentEstateSeparation.DocumentId = document.Id;
                                    documentEstateSeparation.ScriptoriumId = document.ScriptoriumId;
                                    documentEstateSeparation.Ilm = DocumentConstants.CreateIlm;
                                    if (document.DocumentTypeId == "612")
                                    {
                                        var estatePieceType = await estatePieceTypeRepository.GetByIdAsync(cancellationToken, documentEstateSeparation.EstatePieceTypeId);
                                        var EstatePieceMainTypeId = estatePieceType.EstatePieceMainTypeId;
                                        switch (EstatePieceMainTypeId)
                                        {
                                            case "3":
                                                documentEstateSeparation.DocumentEstateSeparationPieceKindId = "2";      // آپارتمانی
                                                break;
                                            case "D1":    // قطعه
                                                documentEstateSeparation.DocumentEstateSeparationPieceKindId = "1";
                                                //if (documentEstateSeparation.Separation.FunctionType == EstateNatureChangeDetail.SeparationToApartment)
                                                //    documentEstateSeparation.DocumentEstateSeparationPieceKindId = "2";
                                                //else
                                                //    documentEstateSeparation.DocumentEstateSeparationPieceKindId = "1";
                                                break;
                                            case "4":    // انباری
                                                documentEstateSeparation.DocumentEstateSeparationPieceKindId = "4";
                                                break;
                                            case "5":    // پارکینگ
                                                documentEstateSeparation.DocumentEstateSeparationPieceKindId = "3";
                                                break;
                                            case "3.":   // منضمات
                                                documentEstateSeparation.DocumentEstateSeparationPieceKindId = "5";
                                                break;
                                            case "2":    // مشاعات
                                                documentEstateSeparation.DocumentEstateSeparationPieceKindId = "6";
                                                break;
                                        }
                                    }
                                    if (documentEstateSeparationViewModel.DocumentEstateSeparationPiecesQuotaList?.Count > 0)
                                    {

                                        foreach (DocumentStandardContractEstateSeparationPiecesQuotaViewModel documentEstateQuotumViewModel in
                                                 documentEstateSeparationViewModel.DocumentEstateSeparationPiecesQuotaList)
                                        {
                                            if (documentEstateQuotumViewModel.IsNew)
                                            {
                                                DocumentEstateSeparationPiecesQuotum documentEstateQuotum = new();
                                                DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateSeparationPiecesQuotam(ref documentEstateQuotum,
                                                    documentEstateQuotumViewModel);
                                                documentEstateQuotum.DocumentEstateSeparationPiecesId = documentEstateSeparation.Id;
                                                documentEstateQuotum.ScriptoriumId = document.ScriptoriumId;
                                                documentEstateQuotum.Ilm = DocumentConstants.CreateIlm;
                                                documentEstateSeparation.DocumentEstateSeparationPiecesQuota?.Add(documentEstateQuotum);
                                            }
                                        }

                                    }
                                    newDocumentEstate.DocumentEstateSeparationPieces?.Add(documentEstateSeparation);
                                }
                            }

                        }

                        if (DocumentStandardContractEstateViewModel.DocumentEstateQuotums?.Count > 0)
                        {

                            foreach (DocumentStandardContractEstateQuotumViewModel documentEstateQuotumViewModel in
                                     DocumentStandardContractEstateViewModel.DocumentEstateQuotums)
                            {
                                if (documentEstateQuotumViewModel.IsNew)
                                {
                                    DocumentEstateQuotum documentEstateQuotum = new();
                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateQuotum(ref documentEstateQuotum,
                                        documentEstateQuotumViewModel);
                                    documentEstateQuotum.DocumentEstateId = newDocumentEstate.Id;
                                    documentEstateQuotum.ScriptoriumId = document.ScriptoriumId;
                                    documentEstateQuotum.Ilm = DocumentConstants.CreateIlm;
                                    newDocumentEstate.DocumentEstateQuota?.Add(documentEstateQuotum);
                                }
                            }

                        }

                        if (DocumentStandardContractEstateViewModel.DocumentEstateQuotaDetails?.Count > 0)
                        {

                            foreach (DocumentStandardContractEstateQuotaDetailViewModel documentEstateQuotaDetailViewModel in
                                     DocumentStandardContractEstateViewModel.DocumentEstateQuotaDetails)
                            {
                                if (documentEstateQuotaDetailViewModel.IsNew)
                                {
                                    DocumentEstateQuotaDetail documentEstateQuotaDetail = new();
                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateQuotaDetail(ref documentEstateQuotaDetail,
                                        documentEstateQuotaDetailViewModel);
                                    documentEstateQuotaDetail.DocumentEstateId = newDocumentEstate.Id;
                                    documentEstateQuotaDetail.ScriptoriumId = document.ScriptoriumId;
                                    documentEstateQuotaDetail.Ilm = DocumentConstants.CreateIlm;
                                    newDocumentEstate.DocumentEstateQuotaDetails?.Add(documentEstateQuotaDetail);
                                }
                            }

                        }

                        document.DocumentEstates.Add(newDocumentEstate);
                        newCounter--;

                    }
                    else if (DocumentStandardContractEstateViewModel.IsDelete)
                    {
                        document.DocumentEstates.First(x => x.Id == DocumentStandardContractEstateViewModel.EstateId.ToGuid())
                            .DocumentEstateOwnershipDocuments.Clear();
                        document.DocumentEstates.First(x => x.Id == DocumentStandardContractEstateViewModel.EstateId.ToGuid())
                            .DocumentEstateAttachments.Clear();
                        document.DocumentEstates.First(x => x.Id == DocumentStandardContractEstateViewModel.EstateId.ToGuid())
                            .DocumentEstateQuota.Clear();
                        document.DocumentEstates.First(x => x.Id == DocumentStandardContractEstateViewModel.EstateId.ToGuid())
                            .DocumentEstateQuotaDetails.Clear();
                        _ = document.DocumentEstates.Remove(
                            document.DocumentEstates.First(x => x.Id == DocumentStandardContractEstateViewModel.EstateId.ToGuid()));
                        isDeletePersonRequest = true;
                    }

                    else if (!DocumentStandardContractEstateViewModel.IsDirty)
                    {
                        DocumentEstate updatingDocumentEstate =
                            document.DocumentEstates.First(x => x.Id == DocumentStandardContractEstateViewModel.EstateId.ToGuid());

                        //           updatingDocumentEstate.RowNo = (byte)rowNoCounter;
                        rowNoCounter++;

                    }

                    List<Guid> documentEstateAttchmentlList = new List<Guid>();
                    foreach (var item in DocumentStandardContractEstateViewModel.DocumentEstateAttachments)
                    {
                        if (item.IsDelete == true)
                        {

                            documentEstateAttchmentlList.Add(item.EstateAttachmentId.ToGuid());

                        }
                    }

                    if (documentEstateAttchmentlList.Count > 0)
                    {
                        await documentRepository.RemoveEstateAttchments(documentEstateAttchmentlList);
                    }

                    List<Guid> documentEstateOwnerShiplList = new List<Guid>();
                    foreach (var documentEstateOwnerShip in DocumentStandardContractEstateViewModel.DocumentEstateOwnerShips)
                    {
                        if (documentEstateOwnerShip.IsDelete == true)
                        {

                            documentEstateOwnerShiplList.Add(documentEstateOwnerShip.EstateOwnerShipId.ToGuid());

                        }
                    }

                    if (documentEstateOwnerShiplList.Count > 0)
                    {
                        await documentRepository.RemoveEstateOwnerShips(documentEstateOwnerShiplList);

                    }

                    List<Guid> documentEstateSeparationList = new List<Guid>();
                    foreach (var documentEstateSeparation in DocumentStandardContractEstateViewModel.DocumentEstateSeparationPieces)
                    {
                        if (documentEstateSeparation.IsDelete == true)
                        {

                            documentEstateSeparationList.Add(documentEstateSeparation.EstateSeparationPieceId.ToGuid());

                        }
                    }

                    if (documentEstateSeparationList.Count > 0)
                    {
                        await documentRepository.RemoveEstateSeparations(documentEstateSeparationList);

                    }

                    List<Guid> documentEstateQuotaDetailList = new List<Guid>();
                    List<Guid> documentEstateQuotumsList = new List<Guid>();
                    foreach (var documentEstateQuotaDetail in DocumentStandardContractEstateViewModel.DocumentEstateQuotaDetails)
                    {
                        if (documentEstateQuotaDetail.IsDelete == true)
                        {

                            documentEstateQuotaDetailList.Add(documentEstateQuotaDetail.DocumentEstateQuotaDetailId
                                .ToGuid());

                        }
                    }

                    foreach (var documentEstateQuotum in DocumentStandardContractEstateViewModel.DocumentEstateQuotums)
                    {
                        if (documentEstateQuotum.IsDelete == true)
                        {
                            documentEstateQuotumsList.Add(documentEstateQuotum.DocumentEstateQuotumId.ToGuid());

                        }
                    }

                    if (documentEstateQuotaDetailList.Count > 0)
                    {
                        await documentRepository.RemoveEstateQuotaDetails(documentEstateQuotaDetailList);

                    }

                    if (documentEstateQuotumsList.Count > 0)
                    {
                        await documentRepository.RemoveEstateQuota(documentEstateQuotumsList);

                    }

                }

                IList<DocumentStandardContractEstateViewModel> documentEstatesDirty =
                    request.DocumentEstates.Where(p => p.IsDirty && !p.IsNew && !p.IsDelete).ToList();
                ICollection<DocumentEstate> dbDocumentEstates;
                IList<Guid> documentEstatesDeleteId;
                IList<Guid> documentEstatesDirtyId;
                if (isDeletePersonRequest)
                {
                    documentEstatesDeleteId = request.DocumentEstates.Where(p => p.IsDelete)
                        .Select(p => Guid.Parse(p.EstateId)).ToList();
                    dbDocumentEstates = document.DocumentEstates.Where(x => !documentEstatesDeleteId.Contains(x.Id))
                        .OrderBy(x => x.RowNo).ToList();

                }
                else
                {
                    documentEstatesDirtyId = documentEstatesDirty.Select(p => Guid.Parse(p.EstateId)).ToList();
                    dbDocumentEstates = document.DocumentEstates.Where(x => documentEstatesDirtyId.Contains(x.Id))
                        .OrderBy(x => x.RowNo).ToList();
                }

                foreach (DocumentEstate dbDocumentEstate in dbDocumentEstates)
                {
                    DocumentEstate updatingDocumentEstate = dbDocumentEstate;
                    DocumentStandardContractEstateViewModel? documentEstateViewModel =
                        documentEstatesDirty.FirstOrDefault(pd => pd.EstateId == updatingDocumentEstate.Id.ToString());
                    if (documentEstateViewModel != null)
                    {
                        DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstate(ref updatingDocumentEstate, documentEstateViewModel,request.IsRemoteRequest);

                        if (updatingDocumentEstate != null &&
                            documentEstateViewModel.DocumentEstateOwnerShips?.Count > 0)
                        {
                            bool isDeleteOwnerShipRequest = false;
                            short rowNoOwnerShipCounter = 1;
                            int rowNoMaxOwnerShip = 0;
                            if (updatingDocumentEstate.DocumentEstateOwnershipDocuments?.Count > 0)
                                rowNoMaxOwnerShip = updatingDocumentEstate.DocumentEstateOwnershipDocuments.Count <=
                                                    updatingDocumentEstate.DocumentEstateOwnershipDocuments.Max(x =>
                                                        x.RowNo)
                                    ? updatingDocumentEstate.DocumentEstateOwnershipDocuments.Max(x => x.RowNo) -
                                      documentEstateViewModel.DocumentEstateOwnerShips.Where(p => p.IsDelete).Count()
                                    : updatingDocumentEstate.DocumentEstateOwnershipDocuments.Count -
                                      documentEstateViewModel.DocumentEstateOwnerShips.Where(p => p.IsDelete).Count();
                            int newCounterOwnerShip =
                                (short)(documentEstateViewModel.DocumentEstateOwnerShips.Where(x => x.IsNew).Count() +
                                        rowNoMaxOwnerShip);

                            foreach (DocumentStandardContractEstateOwnerShipViewModel documentEstateOwnerShipViewModel in
                                     documentEstateViewModel.DocumentEstateOwnerShips)
                            {
                                if (documentEstateOwnerShipViewModel.IsNew)
                                {
                                    DocumentEstateOwnershipDocument documentEstateOwnershipDocument = new();
                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstate(ref documentEstateOwnershipDocument,
                                        documentEstateOwnerShipViewModel,request.IsRemoteRequest);
                                    if (updatingDocumentEstate != null)
                                        documentEstateOwnershipDocument.DocumentEstateId = updatingDocumentEstate.Id;
                                    documentEstateOwnershipDocument.RowNo = (byte)newCounterOwnerShip;
                                    documentEstateOwnershipDocument.ScriptoriumId = document.ScriptoriumId;
                                    documentEstateOwnershipDocument.Ilm = DocumentConstants.CreateIlm;
                                    updatingDocumentEstate?.DocumentEstateOwnershipDocuments?.Add(
                                        documentEstateOwnershipDocument);
                                    newCounterOwnerShip--;
                                }
                                else if (documentEstateOwnerShipViewModel.IsDelete)
                                {
                                    updatingDocumentEstate?.DocumentEstateOwnershipDocuments?.Remove(updatingDocumentEstate.DocumentEstateOwnershipDocuments.First(x => x.Id == documentEstateOwnerShipViewModel.EstateOwnerShipId.ToGuid()));
                                    isDeleteOwnerShipRequest = true;
                                }

                                else if (!documentEstateOwnerShipViewModel.IsDirty)
                                {
                                    //  DocumentEstateOwnershipDocument? updatingDocumentEstateOwnership = updatingDocumentEstate?.DocumentEstateOwnershipDocuments?.First(x => x.Id == documentEstateOwnerShipViewModel.EstateOwnerShipId.ToGuid());

                                    //           updatingDocumentEstate.RowNo = (byte)rowNoCounter;
                                    rowNoOwnerShipCounter++;

                                }
                            }

                            IList<DocumentStandardContractEstateOwnerShipViewModel> documentEstateOwnerShipsDirty =
                                documentEstateViewModel.DocumentEstateOwnerShips
                                    .Where(p => p.IsDirty && !p.IsNew && !p.IsDelete).ToList();
                            ICollection<DocumentEstateOwnershipDocument> dbDocumentEstateOwnerShips =
                                new List<DocumentEstateOwnershipDocument>() { };
                            IList<Guid> documentEstateOwnerShipsDeleteId;
                            IList<Guid> documentEstateOwnerShipsDirtyId;
                            if (isDeleteOwnerShipRequest)
                            {
                                documentEstateOwnerShipsDeleteId = documentEstateViewModel.DocumentEstateOwnerShips
                                    .Where(p => p.IsDelete).Select(p => Guid.Parse(p.EstateOwnerShipId)).ToList();

                                if (updatingDocumentEstate != null &&
                                    updatingDocumentEstate?.DocumentEstateOwnershipDocuments != null)
                                {
                                    dbDocumentEstateOwnerShips = updatingDocumentEstate.DocumentEstateOwnershipDocuments
                                        .Where(x => !documentEstateOwnerShipsDeleteId.Contains(x.Id))
                                        .OrderBy(x => x.RowNo).ToList();

                                }

                            }
                            else
                            {
                                documentEstateOwnerShipsDirtyId = documentEstateOwnerShipsDirty
                                    .Select(p => Guid.Parse(p.EstateOwnerShipId)).ToList();
                                if (updatingDocumentEstate != null &&
                                    updatingDocumentEstate.DocumentEstateOwnershipDocuments != null)
                                {
                                    dbDocumentEstateOwnerShips = updatingDocumentEstate.DocumentEstateOwnershipDocuments
                                        .Where(x => documentEstateOwnerShipsDirtyId.Contains(x.Id))
                                        .OrderBy(x => x.RowNo).ToList();

                                }
                            }

                            if (dbDocumentEstateOwnerShips != null)
                            {
                                foreach (DocumentEstateOwnershipDocument dbDocumentEstateOwnerShip in
                                         dbDocumentEstateOwnerShips)
                                {
                                    DocumentEstateOwnershipDocument updatingDocumentEstateOwnerShip =
                                        dbDocumentEstateOwnerShip;
                                    if (documentEstateOwnerShipsDirty != null)
                                    {
                                        DocumentStandardContractEstateOwnerShipViewModel? documentEstateOwnerShipViewModel =
                                            documentEstateOwnerShipsDirty.FirstOrDefault(pd =>
                                                pd.EstateOwnerShipId == updatingDocumentEstateOwnerShip.Id.ToString());

                                        if (documentEstateViewModel != null)
                                        {
                                            DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstate(
                                                ref updatingDocumentEstateOwnerShip, documentEstateOwnerShipViewModel,request.IsRemoteRequest);

                                        }
                                    }

                                    if (isDeletePersonRequest)
                                    {

                                        updatingDocumentEstateOwnerShip.RowNo = rowNoOwnerShipCounter;
                                        rowNoOwnerShipCounter++;
                                    }
                                }
                            }

                        }

                        if (updatingDocumentEstate != null &&
                            documentEstateViewModel.DocumentEstateSeparationPieces?.Count > 0)
                        {
                            foreach (DocumentStandardContractEstateSeparationPieceViewModel documentEstateSeparationViewModel in
                                     documentEstateViewModel.DocumentEstateSeparationPieces)
                            {
                                if (documentEstateSeparationViewModel.IsNew)
                                {
                                    DocumentEstateSeparationPiece documentEstateSeparation = new();
                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstate(ref documentEstateSeparation,
                                        documentEstateSeparationViewModel);
                                    if (updatingDocumentEstate != null)
                                        documentEstateSeparation.DocumentEstateId = updatingDocumentEstate.Id;
                                    documentEstateSeparation.ScriptoriumId = document.ScriptoriumId;
                                    documentEstateSeparation.Ilm = DocumentConstants.CreateIlm;
                                    documentEstateSeparation.DocumentId = document.Id;
                                    if (updatingDocumentEstate?.DocumentEstateSeparationPieces != null && documentEstateSeparationViewModel.DocumentEstateSeparationPiecesQuotaList?.Count > 0)
                                    {
                                        foreach (DocumentStandardContractEstateSeparationPiecesQuotaViewModel documentEstateQuotumViewModel in
                                                 documentEstateSeparationViewModel.DocumentEstateSeparationPiecesQuotaList)
                                        {
                                            if (documentEstateQuotumViewModel.IsNew)
                                            {
                                                DocumentEstateSeparationPiecesQuotum documentEstateQuotum = new();
                                                DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateSeparationPiecesQuotam(ref documentEstateQuotum,
                                                    documentEstateQuotumViewModel);
                                                if (updatingDocumentEstate != null)
                                                    documentEstateQuotum.DocumentEstateSeparationPiecesId = documentEstateSeparation.Id;
                                                documentEstateQuotum.ScriptoriumId = document.ScriptoriumId;
                                                documentEstateQuotum.Ilm = DocumentConstants.CreateIlm;
                                                documentEstateSeparation?.DocumentEstateSeparationPiecesQuota?.Add(documentEstateQuotum);
                                            }
                                            else if (documentEstateQuotumViewModel.IsDirty)
                                            {
                                                DocumentEstateSeparationPiecesQuotum updatingDocumentEstateQuotum =
                                                    documentEstateSeparation.DocumentEstateSeparationPiecesQuota.First(x =>
                                                        x.Id == documentEstateQuotumViewModel.EstateSeparationPiecesQuotaId.ToGuid());

                                                DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateSeparationPiecesQuotam(ref updatingDocumentEstateQuotum,
                                                    documentEstateQuotumViewModel);

                                            }
                                        }

                                    }

                                    updatingDocumentEstate?.DocumentEstateSeparationPieces?.Add(documentEstateSeparation);
                                }
                                else if (documentEstateSeparationViewModel.IsDelete)
                                {
                                    updatingDocumentEstate?.DocumentEstateSeparationPieces?.Remove(updatingDocumentEstate.DocumentEstateSeparationPieces.First(x => x.Id == documentEstateSeparationViewModel.EstateSeparationPieceId.ToGuid()));
                                }

                                else if (documentEstateSeparationViewModel.IsDirty)
                                {
                                    DocumentEstateSeparationPiece updatingDocumentEstateSeparation = updatingDocumentEstate.DocumentEstateSeparationPieces.First(x => x.Id == documentEstateSeparationViewModel.EstateSeparationPieceId.ToGuid());
                                    if (updatingDocumentEstate?.DocumentEstateSeparationPieces != null && documentEstateSeparationViewModel.DocumentEstateSeparationPiecesQuotaList?.Count > 0)
                                    {
                                        foreach (DocumentStandardContractEstateSeparationPiecesQuotaViewModel documentEstateQuotumViewModel in
                                                 documentEstateSeparationViewModel.DocumentEstateSeparationPiecesQuotaList)
                                        {
                                            if (documentEstateQuotumViewModel.IsNew)
                                            {
                                                DocumentEstateSeparationPiecesQuotum documentEstateQuotum = new();
                                                DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateSeparationPiecesQuotam(ref documentEstateQuotum,
                                                    documentEstateQuotumViewModel);
                                                if (updatingDocumentEstate != null)
                                                    documentEstateQuotum.DocumentEstateSeparationPiecesId = updatingDocumentEstateSeparation.Id;
                                                documentEstateQuotum.ScriptoriumId = document.ScriptoriumId;
                                                documentEstateQuotum.Ilm = DocumentConstants.CreateIlm;
                                                updatingDocumentEstateSeparation?.DocumentEstateSeparationPiecesQuota?.Add(documentEstateQuotum);
                                            }
                                            else if (documentEstateQuotumViewModel.IsDirty)
                                            {
                                                DocumentEstateSeparationPiecesQuotum updatingDocumentEstateQuotum =
                                                    updatingDocumentEstateSeparation.DocumentEstateSeparationPiecesQuota.First(x =>
                                                        x.Id == documentEstateQuotumViewModel.EstateSeparationPiecesQuotaId.ToGuid());

                                                DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateSeparationPiecesQuotam(ref updatingDocumentEstateQuotum,
                                                    documentEstateQuotumViewModel);

                                            }
                                        }

                                    }
                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstate(ref updatingDocumentEstateSeparation, documentEstateSeparationViewModel);
                                    updatingDocumentEstateSeparation.DocumentId = document.Id;

                                }
                            }

                        }

                        if (updatingDocumentEstate != null &&
                            documentEstateViewModel.DocumentEstateAttachments?.Count > 0)
                        {

                            int rowNoMaxAttachment = 0;
                            if (updatingDocumentEstate.DocumentEstateAttachments?.Count > 0)
                                rowNoMaxAttachment = updatingDocumentEstate.DocumentEstateAttachments.Max(x => x.RowNo);
                            short newCounterAttachment = (short)(rowNoMaxAttachment + 1);
                            foreach (var item in dbDocumentEstate.DocumentEstateAttachments)
                            {
                                item.ScriptoriumId = document.ScriptoriumId;
                                item.DocumentEstateId = dbDocumentEstate.Id;
                                if (item.RowNo <= 0)
                                {
                                    item.RowNo = newCounterAttachment;
                                    newCounterAttachment--;
                                }
                            }

                            foreach (DocumentStandardContractEstateAttachmentViewModel documentEstateAttachmentViewModel in
                                     documentEstateViewModel.DocumentEstateAttachments)
                            {
                                if (documentEstateAttachmentViewModel.IsNew)
                                {
                                    DocumentEstateAttachment documentEstateAttachment = new();
                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateAttachment(ref documentEstateAttachment,
                                        documentEstateAttachmentViewModel);
                                    if (updatingDocumentEstate != null)
                                        documentEstateAttachment.DocumentEstateId = updatingDocumentEstate.Id;
                                    documentEstateAttachment.ScriptoriumId = document.ScriptoriumId;
                                    documentEstateAttachment.Ilm = DocumentConstants.CreateIlm;
                                    documentEstateAttachment.RowNo = newCounterAttachment;
                                    updatingDocumentEstate?.DocumentEstateAttachments?.Add(documentEstateAttachment);
                                    newCounterAttachment++;
                                }
                                else if (documentEstateAttachmentViewModel.IsDelete)
                                {
                                    // updatingDocumentEstate?.DocumentEstateAttachments?.Remove(updatingDocumentEstate.DocumentEstateAttachments.First(x => x.Id == documentEstateAttachmentViewModel.EstateAttachmentId.ToGuid()));
                                }

                                else if (documentEstateAttachmentViewModel.IsDirty)
                                {
                                    DocumentEstateAttachment updatingDocumentEstateAttachment =
                                        updatingDocumentEstate.DocumentEstateAttachments.First(x =>
                                            x.Id == documentEstateAttachmentViewModel.EstateAttachmentId.ToGuid());
                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateAttachment(
                                        ref updatingDocumentEstateAttachment, documentEstateAttachmentViewModel);
                                    updatingDocumentEstateAttachment.DocumentEstateId = updatingDocumentEstate.Id;
                                    updatingDocumentEstateAttachment.ScriptoriumId = document.ScriptoriumId;
                                    updatingDocumentEstateAttachment.Ilm = DocumentConstants.UpdateIlm;
                                }
                            }

                        }

                        if (updatingDocumentEstate != null && documentEstateViewModel.DocumentEstateQuotums?.Count > 0)
                        {
                            foreach (DocumentStandardContractEstateQuotumViewModel documentEstateQuotumViewModel in
                                     documentEstateViewModel.DocumentEstateQuotums)
                            {
                                if (documentEstateQuotumViewModel.IsNew)
                                {
                                    DocumentEstateQuotum documentEstateQuotum = new();
                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateQuotum(ref documentEstateQuotum,
                                        documentEstateQuotumViewModel);
                                    if (updatingDocumentEstate != null)
                                        documentEstateQuotum.DocumentEstateId = updatingDocumentEstate.Id;
                                    documentEstateQuotum.ScriptoriumId = document.ScriptoriumId;
                                    documentEstateQuotum.Ilm = DocumentConstants.CreateIlm;
                                    updatingDocumentEstate?.DocumentEstateQuota?.Add(documentEstateQuotum);
                                }
                                else if (documentEstateQuotumViewModel.IsDirty)
                                {
                                    DocumentEstateQuotum updatingDocumentEstateQuotum =
                                        updatingDocumentEstate.DocumentEstateQuota.First(x =>
                                            x.Id == documentEstateQuotumViewModel.DocumentEstateQuotumId.ToGuid());

                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateQuotum(ref updatingDocumentEstateQuotum,
                                        documentEstateQuotumViewModel);

                                }
                            }

                        }

                        if (updatingDocumentEstate != null &&
                            documentEstateViewModel.DocumentEstateQuotaDetails?.Count > 0)
                        {
                            foreach (DocumentStandardContractEstateQuotaDetailViewModel documentEstateQuotaDetailViewModel in
                                     documentEstateViewModel.DocumentEstateQuotaDetails)
                            {
                                if (documentEstateQuotaDetailViewModel.IsNew)
                                {
                                    DocumentEstateQuotaDetail documentEstateQuotaDetail = new();
                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateQuotaDetail(ref documentEstateQuotaDetail,
                                        documentEstateQuotaDetailViewModel);
                                    if (updatingDocumentEstate != null)
                                        documentEstateQuotaDetail.DocumentEstateId = updatingDocumentEstate.Id;
                                    documentEstateQuotaDetail.ScriptoriumId = document.ScriptoriumId;
                                    documentEstateQuotaDetail.Ilm = DocumentConstants.CreateIlm;
                                    updatingDocumentEstate?.DocumentEstateQuotaDetails?.Add(documentEstateQuotaDetail);
                                }
                                else if (documentEstateQuotaDetailViewModel.IsDirty)
                                {
                                    DocumentEstateQuotaDetail updatingDocumentEstateQuotaDetail =
                                        updatingDocumentEstate.DocumentEstateQuotaDetails.First(x =>
                                            x.Id == documentEstateQuotaDetailViewModel.DocumentEstateQuotaDetailId
                                                .ToGuid());

                                    DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstateQuotaDetail(
                                        ref updatingDocumentEstateQuotaDetail, documentEstateQuotaDetailViewModel);

                                }
                            }

                        }
                    }

                    if (isDeletePersonRequest)
                    {
                        if (updatingDocumentEstate != null)
                        {
                            updatingDocumentEstate.RowNo = rowNoCounter;

                        }

                        rowNoCounter++;
                    }

                }

            }

            foreach (var documentVehicle in request.DocumentVehicles)
            {
                foreach (var documentVehicleQuotaDetail in documentVehicle.DocumentVehicleQuotaDetails)
                {
                    if (documentVehicleQuotaDetail.IsDirty == true || documentVehicleQuotaDetail.IsNew == true)
                    {
                        documentVehicle.IsDirty = true;
                    }
                }

                foreach (var documentVehicleQuotum in documentVehicle.DocumentVehicleQuotums)
                {
                    if (documentVehicleQuotum.IsDirty == true || documentVehicleQuotum.IsNew == true)
                    {
                        documentVehicle.IsDirty = true;
                    }
                }
            }

            if (request.DocumentVehicles?.Count > 0)
            {
                isDeletePersonRequest = false;
                rowNoCounter = 1;
                rowNoMax = 0;
                if (document.DocumentVehicles.Count > 0)
                    rowNoMax = document.DocumentVehicles.Count <= document.DocumentVehicles.Max(x => x.RowNo)
                        ? document.DocumentVehicles.Max(x => x.RowNo) -
                          request.DocumentVehicles.Where(p => p.IsDelete).Count()
                        : document.DocumentVehicles.Count - request.DocumentVehicles.Where(p => p.IsDelete).Count();
                newCounter = (short)(request.DocumentVehicles.Where(x => x.IsNew).Count() + rowNoMax);

                foreach (DocumentStandardContractVehicleViewModel DocumentStandardContractVehicleViewModel in request.DocumentVehicles)
                {
                    if (DocumentStandardContractVehicleViewModel.IsNew)
                    {
                        DocumentVehicle newDocumentVehicle = new();
                        DocumentStandardContractVehicleMapper.MapToDocumentStandardContractVehicle(ref newDocumentVehicle, DocumentStandardContractVehicleViewModel);
                        foreach (var item in newDocumentVehicle.DocumentVehicleQuotaDetails)
                        {
                            item.DocumentVehicleId = newDocumentVehicle.Id;
                            item.ScriptoriumId = document.ScriptoriumId;
                        }

                        foreach (var item in newDocumentVehicle.DocumentVehicleQuota)
                        {
                            item.DocumentVehicleId = newDocumentVehicle.Id;
                            item.ScriptoriumId = document.ScriptoriumId;

                        }

                        newDocumentVehicle.ScriptoriumId = document.ScriptoriumId;
                        newDocumentVehicle.RowNo = (byte)newCounter;
                        newDocumentVehicle.Ilm = DocumentConstants.UpdateIlm;
                        if (DocumentStandardContractVehicleViewModel.IsDirty)
                        {

                        }

                        document.DocumentVehicles.Add(newDocumentVehicle);
                        newCounter--;

                    }
                    else if (DocumentStandardContractVehicleViewModel.IsDelete)
                    {

                        _ = document.DocumentVehicles.Remove(
                            document.DocumentVehicles.First(x => x.Id == DocumentStandardContractVehicleViewModel.VehicleId.ToGuid()));
                        isDeletePersonRequest = true;

                    }

                    else if (!DocumentStandardContractVehicleViewModel.IsDirty)
                    {
                        DocumentVehicle updatingDocumentVehicle =
                            document.DocumentVehicles.First(x => x.Id == DocumentStandardContractVehicleViewModel.VehicleId.ToGuid());
                        //   updatingDocumentVehicle.RowNo = (byte)rowNoCounter;
                        rowNoCounter++;

                    }

                    List<Guid> documentVehicleQuotaDetailList = new List<Guid>();
                    List<Guid> documentVehicleQuotumsList = new List<Guid>();
                    foreach (var documentVehicleQuotaDetail in DocumentStandardContractVehicleViewModel.DocumentVehicleQuotaDetails)
                    {
                        if (documentVehicleQuotaDetail.IsDelete == true)
                        {

                            documentVehicleQuotaDetailList.Add(documentVehicleQuotaDetail.DocumentVehicleQuotaDetailId
                                .ToGuid());
                            //document.DocumentVehicles
                            //  .Where ( x => x.Id == DocumentStandardContractVehicleViewModel.VehicleId.ToGuid ()  )
                            //  .FirstOrDefault ().DocumentVehicleQuotaDetails.Remove ( document.DocumentVehicles
                            //  .Where ( x => x.Id == DocumentStandardContractVehicleViewModel.VehicleId.ToGuid () ).FirstOrDefault ()
                            //  .DocumentVehicleQuotaDetails.FirstOrDefault ( q => q.Id == documentVehicleQuotaDetail.DocumentVehicleQuotaDetailId.ToGuid () ) );

                        }
                    }

                    foreach (var documentVehicleQuotum in DocumentStandardContractVehicleViewModel.DocumentVehicleQuotums)
                    {
                        if (documentVehicleQuotum.IsDelete == true)
                        {
                            documentVehicleQuotumsList.Add(documentVehicleQuotum.DocumentVehicleQuotumId.ToGuid());

                        }
                    }

                    if (documentVehicleQuotaDetailList.Count > 0)
                    {
                        await documentRepository.RemoveVehicleQuotaDetails(documentVehicleQuotaDetailList);

                    }

                    if (documentVehicleQuotumsList.Count > 0)
                    {
                        await documentRepository.RemoveVehicleQuota(documentVehicleQuotumsList);

                    }

                }

                IList<DocumentStandardContractVehicleViewModel> DocumentVehiclesDirty =
                    request.DocumentVehicles.Where(p => p.IsDirty && !p.IsNew && !p.IsDelete).ToList();
                ICollection<DocumentVehicle> dbDocumentVehicles;
                IList<Guid> DocumentVehiclesDeleteId;
                IList<Guid> DocumentVehiclesDirtyId;
                if (isDeletePersonRequest)
                {
                    DocumentVehiclesDeleteId = request.DocumentVehicles.Where(p => p.IsDelete)
                        .Select(p => Guid.Parse(p.VehicleId)).ToList();
                    dbDocumentVehicles = document.DocumentVehicles.Where(x => !DocumentVehiclesDeleteId.Contains(x.Id))
                        .OrderBy(x => x.RowNo).ToList();

                }
                else
                {
                    DocumentVehiclesDirtyId = DocumentVehiclesDirty.Select(p => Guid.Parse(p.VehicleId)).ToList();
                    dbDocumentVehicles = document.DocumentVehicles.Where(x => DocumentVehiclesDirtyId.Contains(x.Id))
                        .OrderBy(x => x.RowNo).ToList();
                }

                foreach (DocumentVehicle dbDocumentVehicle in dbDocumentVehicles)
                {
                    DocumentVehicle updatingDocumentVehicle = dbDocumentVehicle;
                    DocumentStandardContractVehicleViewModel? DocumentStandardContractVehicleViewModel =
                        DocumentVehiclesDirty.FirstOrDefault(
                            pd => pd.VehicleId == updatingDocumentVehicle.Id.ToString());
                    if (DocumentStandardContractVehicleViewModel != null)
                    {
                        DocumentStandardContractVehicleMapper.MapToDocumentStandardContractVehicle(ref updatingDocumentVehicle,
                            DocumentStandardContractVehicleViewModel);

                    }

                    if (isDeletePersonRequest)
                    {

                        updatingDocumentVehicle.RowNo = rowNoCounter;
                        rowNoCounter++;
                    }

                    foreach (var item in dbDocumentVehicle.DocumentVehicleQuota)
                    {
                        item.DocumentVehicleId = dbDocumentVehicle.Id;
                        item.ScriptoriumId = document.ScriptoriumId;
                    }

                    foreach (var item in dbDocumentVehicle.DocumentVehicleQuotaDetails)
                    {
                        item.DocumentVehicleId = dbDocumentVehicle.Id;
                        item.ScriptoriumId = document.ScriptoriumId;
                    }
                }

            }

            if (request.DocumentCases?.Count > 0)
            {
                isDeletePersonRequest = false;
                rowNoCounter = 1;
                rowNoMax = 0;
                if (document.DocumentCases.Count > 0)
                    rowNoMax = document.DocumentCases.Count <= document.DocumentCases.Max(x => x.RowNo)
                        ? document.DocumentCases.Max(x => x.RowNo) -
                          request.DocumentCases.Where(p => p.IsDelete).Count()
                        : document.DocumentCases.Count - request.DocumentCases.Where(p => p.IsDelete).Count();
                newCounter = (short)(request.DocumentCases.Where(x => x.IsNew).Count() + rowNoMax);

                foreach (DocumentStandardContractCaseViewModel DocumentCaseViewModel in request.DocumentCases)
                {
                    if (DocumentCaseViewModel.IsNew)
                    {
                        DocumentCase newDocumentCase = new();
                        DocumentStandardContractCaseMapper.MapToDocumentStandardContractCase(ref newDocumentCase, DocumentCaseViewModel,    request.IsRemoteRequest);
                        newDocumentCase.ScriptoriumId = document.ScriptoriumId;
                        newDocumentCase.RowNo = (byte)newCounter;
                        newDocumentCase.Ilm = DocumentConstants.UpdateIlm;
                        if (DocumentCaseViewModel.IsDirty)
                        {

                        }

                        document.DocumentCases.Add(newDocumentCase);
                        newCounter--;

                    }
                    else if (DocumentCaseViewModel.IsDelete)
                    {
                        _ = document.DocumentCases.Remove(document.DocumentCases.First(x =>
                            x.Id == DocumentCaseViewModel.DocumentCaseId.ToGuid()));
                        isDeletePersonRequest = true;
                    }

                    else if (!DocumentCaseViewModel.IsDirty)

                    {
                        DocumentCase updatingDocumentCase =
                            document.DocumentCases.First(x => x.Id == DocumentCaseViewModel.DocumentCaseId.ToGuid());
                        //    updatingDocumentCase.RowNo = (byte)rowNoCounter;
                        rowNoCounter++;

                    }

                }

                IList<DocumentStandardContractCaseViewModel> DocumentCasesDirty =
                    request.DocumentCases.Where(p => p.IsDirty && !p.IsNew && !p.IsDelete).ToList();
                ICollection<DocumentCase> dbDocumentCases;
                IList<Guid> DocumentCasesDeleteId;
                IList<Guid> DocumentCasesDirtyId;
                if (isDeletePersonRequest)
                {
                    DocumentCasesDeleteId = request.DocumentCases.Where(p => p.IsDelete)
                        .Select(p => Guid.Parse(p.DocumentCaseId)).ToList();
                    dbDocumentCases = document.DocumentCases.Where(x => !DocumentCasesDeleteId.Contains(x.Id))
                        .OrderBy(x => x.RowNo).ToList();

                }
                else
                {
                    DocumentCasesDirtyId = DocumentCasesDirty.Select(p => Guid.Parse(p.DocumentCaseId)).ToList();
                    dbDocumentCases = document.DocumentCases.Where(x => DocumentCasesDirtyId.Contains(x.Id))
                        .OrderBy(x => x.RowNo).ToList();
                }

                foreach (DocumentCase dbDocumentCase in dbDocumentCases)
                {
                    DocumentCase updatingDocumentCase = dbDocumentCase;
                    DocumentStandardContractCaseViewModel? DocumentCaseViewModel =
                        DocumentCasesDirty.FirstOrDefault(pd =>
                            pd.DocumentCaseId == updatingDocumentCase.Id.ToString());
                    if (DocumentCaseViewModel != null)
                    {
                        DocumentStandardContractCaseMapper.MapToDocumentStandardContractCase(ref updatingDocumentCase, DocumentCaseViewModel,request.IsRemoteRequest);

                    }

                    if (isDeletePersonRequest)
                    {

                        updatingDocumentCase.RowNo = rowNoCounter;
                        rowNoCounter++;
                    }
                }

            }

            if (request.DocumentInfoJudgment != null)
            {

                if (request.DocumentInfoJudgment.IsNew)
                {
                    DocumentInfoJudgement newDocumentInfoJudgment = new();
                    DocumentStandardContractInfoJudgmentMapper.MapToDocumentStandardContractInfoJudgment(ref newDocumentInfoJudgment,
                        request.DocumentInfoJudgment, request.IsRemoteRequest);
                    newDocumentInfoJudgment.ScriptoriumId = document.ScriptoriumId;
                    newDocumentInfoJudgment.Ilm = DocumentConstants.UpdateIlm;
                    document.DocumentInfoJudgement = newDocumentInfoJudgment;

                }
                else if (request.DocumentInfoJudgment.IsDirty)
                {
                    DocumentInfoJudgement updatingDocumentInfoJudgment = document.DocumentInfoJudgement;
                    DocumentStandardContractInfoJudgmentMapper.MapToDocumentStandardContractInfoJudgment(ref updatingDocumentInfoJudgment,
                        request.DocumentInfoJudgment,request.IsRemoteRequest);
                }
                else
                {
                    DocumentInfoJudgement updatingDocumentInfoJudgment = document.DocumentInfoJudgement;
                    rowNoCounter++;
                }

            }

            if (!(request.IsCopyDocumentInfoText == true && !string.IsNullOrEmpty(request.DocumentCopyId)))
            {
                if (request.DocumentInfoText != null)
                {

                    if (request.DocumentInfoText.IsNew)
                    {
                        DocumentInfoText newDocumentInfoText = new();
                        DocumentStandardContractInfoTextMapper.MapToDocumentStandardContractInfoText(ref newDocumentInfoText, request.DocumentInfoText);
                        newDocumentInfoText.ScriptoriumId = document.ScriptoriumId;
                        newDocumentInfoText.DocumentDescription = request.DocumentInfoOther?.DocumentDescription;
                        newDocumentInfoText.Ilm = DocumentConstants.UpdateIlm;
                        if (request.DocumentInfoText.IsDirty)
                        {

                        }

                        document.DocumentInfoText = newDocumentInfoText;

                    }

                    else if (request.DocumentInfoText.IsDirty)
                    {
                        DocumentInfoText updatingDocumentInfoText = document.DocumentInfoText;
                        DocumentStandardContractInfoTextMapper.MapToDocumentStandardContractInfoText(ref updatingDocumentInfoText,
                            request.DocumentInfoText);
                        if (request.DocumentInfoOther != null)
                        {
                            updatingDocumentInfoText.DocumentDescription =
                                request.DocumentInfoOther.DocumentDescription;
                        }
                    }

                    else
                    {
                        DocumentInfoText updatingDocumentInfoText = document.DocumentInfoText;
                        rowNoCounter++;
                    }

                }
                else
                {
                    if (document.DocumentInfoText == null && request.DocumentInfoOther != null)
                    {
                        DocumentInfoText newDocumentInfoText = new();
                        newDocumentInfoText.ScriptoriumId = document.ScriptoriumId;
                        newDocumentInfoText.DocumentId = document.Id;
                        newDocumentInfoText.Id = Guid.NewGuid();
                        newDocumentInfoText.DocumentDescription = request.DocumentInfoOther.DocumentDescription;
                        newDocumentInfoText.Ilm = DocumentConstants.UpdateIlm;

                        document.DocumentInfoText = newDocumentInfoText;
                    }
                    else if (document.DocumentInfoText != null && request.DocumentInfoOther != null)
                    {
                        DocumentInfoText updatingDocumentInfoText = document.DocumentInfoText;

                        updatingDocumentInfoText.DocumentDescription = request.DocumentInfoOther.DocumentDescription;

                    }
                }
            }

            if (request.IsNew == true && document.DocumentInfoText == null)
            {
                DocumentInfoText newDocumentInfoText = new();
                newDocumentInfoText.ScriptoriumId = document.ScriptoriumId;
                newDocumentInfoText.DocumentId = document.Id;
                newDocumentInfoText.Id = Guid.NewGuid();
                newDocumentInfoText.Ilm = DocumentConstants.UpdateIlm;
                document.DocumentInfoText = newDocumentInfoText;

            }

            if (request.DocumentInfoOther != null)
            {
                if (request.DocumentInfoOther.IsNew)
                {
                    DocumentInfoOther newDocumentInfoOther = new();
                    DocumentStandardContractInfoOtherMapper.MapToDocumentStandardContractInfoOther(ref newDocumentInfoOther, request.DocumentInfoOther);
                    newDocumentInfoOther.ScriptoriumId = document.ScriptoriumId;
                    newDocumentInfoOther.Ilm = DocumentConstants.UpdateIlm;
                    if (request.DocumentCostQuestion != null)
                    {
                        if (request.DocumentCostQuestion.IsDirty || request.DocumentCostQuestion.IsNew)
                        {
                            newDocumentInfoOther.IsEstateRegistered =
                                request.DocumentCostQuestion.IsEstateRegister.ToYesNo();
                        }
                    }

                    if (request.DocumentInfoOther.IsDirty)
                    {

                    }

                    document.DocumentInfoOther = newDocumentInfoOther;

                }

                else if (request.DocumentInfoOther.IsDirty)
                {
                    DocumentInfoOther updatingDocumentInfoOther = document.DocumentInfoOther;
                    DocumentStandardContractInfoOtherMapper.MapToDocumentStandardContractInfoOther(ref updatingDocumentInfoOther,
                        request.DocumentInfoOther);
                    if (request.DocumentCostQuestion != null)
                    {
                        if (request.DocumentCostQuestion.IsDirty || request.DocumentCostQuestion.IsNew)
                        {
                            updatingDocumentInfoOther.IsEstateRegistered =
                                request.DocumentCostQuestion.IsEstateRegister.ToYesNo();
                        }
                    }
                }

                else
                {
                    DocumentInfoOther updatingDocumentInfoOther = document.DocumentInfoOther;
                    rowNoCounter++;
                }

            }
            else if (request.DocumentCostQuestion != null)
            {
                if (request.DocumentCostQuestion.IsDirty || request.DocumentCostQuestion.IsNew)
                {
                    DocumentInfoOther updatingDocumentInfoOther = document.DocumentInfoOther;
                    updatingDocumentInfoOther.IsEstateRegistered =
                        request.DocumentCostQuestion.IsEstateRegister.ToYesNo();
                }
            }

            if (request.DocumentRelations?.Count > 0)
            {
                foreach (DocumentStandardContractRelationViewModel DocumentStandardContractRelationViewModel in request.DocumentRelations)
                {

                    if (DocumentStandardContractRelationViewModel.IsNew)
                    {

                        DocumentRelation newDocumentRelation = new();
                        DocumentStandardContractRelationMapper.MapToDocumentStandardContractRelation(ref newDocumentRelation,
                            DocumentStandardContractRelationViewModel,request.IsRemoteRequest);

                        newDocumentRelation.ScriptoriumId = document.ScriptoriumId;
                        newDocumentRelation.Ilm = "1";
                        newDocumentRelation.RecordDate = DateTime.Now;
                        document.DocumentRelationDocuments.Add(newDocumentRelation);
                    }
                    else if (DocumentStandardContractRelationViewModel.IsDelete)
                    {
                        _ = document.DocumentRelationDocuments.Remove(
                            document.DocumentRelationDocuments.First(x =>
                                x.Id == DocumentStandardContractRelationViewModel.RelatedtId.ToGuid()));
                    }
                    else if (DocumentStandardContractRelationViewModel.IsDirty)
                    {

                        DocumentRelation updatingDocumentRelation =
                            document.DocumentRelationDocuments.First(x =>
                                x.Id == DocumentStandardContractRelationViewModel.RelatedtId.ToGuid());
                        DocumentStandardContractRelationMapper.MapToDocumentStandardContractRelation(ref updatingDocumentRelation,
                            DocumentStandardContractRelationViewModel, request.IsRemoteRequest);

                    }
                    else
                    {
                        continue;
                    }
                }

            }

            if (request.DocumentMainRelation != null)
            {
                if (request.DocumentMainRelation.IsDirty == true)
                {

                    DocumentStandardContractRelationMapper.MapToDocumentStandardContractMainRelation(ref document, request.DocumentMainRelation);

                }
            }

            if (request.DocumentInquiries?.Count > 0)
            {
                foreach (DocumentStandardContractInquiryViewModel documentInquiryViewModel in request.DocumentInquiries)
                {
                    if (documentInquiryViewModel.IsNew)
                    {
                        DocumentInquiry newDocumentInquiry = new();
                        DocumentStandardContractInquiriesMapper.MapToDocumentStandardContractInquiry(ref newDocumentInquiry, documentInquiryViewModel,request.IsRemoteRequest);
                        newDocumentInquiry.ScriptoriumId = document.ScriptoriumId;
                        newDocumentInquiry.Ilm = "1";
                        newDocumentInquiry.RecordDate = DateTime.Now;
                        document.DocumentInquiries.Add(newDocumentInquiry);
                    }
                    else if (documentInquiryViewModel.IsDelete)
                    {
                        _ = document.DocumentInquiries.Remove(document.DocumentInquiries.First(x =>
                            x.Id == documentInquiryViewModel.DocumentInquiryId.ToGuid()));
                    }
                    else if (documentInquiryViewModel.IsDirty)
                    {
                        DocumentInquiry updatingDocumentInquiry =
                            document.DocumentInquiries.First(x =>
                                x.Id == documentInquiryViewModel.DocumentInquiryId.ToGuid());
                        DocumentStandardContractInquiriesMapper.MapToDocumentStandardContractInquiry(ref updatingDocumentInquiry,
                            documentInquiryViewModel);
                    }
                }

            }

            if (request.DocumentCosts?.Count > 0)
            {
                foreach (DocumentStandardContractCostViewModel DocumentCostViewModel in request.DocumentCosts)
                {
                    if (DocumentCostViewModel.IsNew)
                    {
                        DocumentCost newDocumentCost = new();
                        DocuemntStandardContractCostMapper.MapToDocumentStandardContractCost(ref newDocumentCost, DocumentCostViewModel);
                        newDocumentCost.ScriptoriumId = document.ScriptoriumId;
                        newDocumentCost.Ilm = "1";
                        newDocumentCost.RecordDate = DateTime.Now;
                        document.DocumentCosts.Add(newDocumentCost);
                        if (!string.IsNullOrEmpty(DocumentCostViewModel.RequestPriceUnchanged))
                        {
                            DocumentCostUnchanged documentCostUnchanged = new();
                            DocuemntStandardContractCostMapper.MapToDocumentStandardContractCostUnchanged(ref documentCostUnchanged,
                                DocumentCostViewModel);
                            documentCostUnchanged.DocumentId = document.Id;
                            documentCostUnchanged.ScriptoriumId = document.ScriptoriumId;
                            documentCostUnchanged.Ilm = DocumentConstants.CreateIlm;
                            documentCostUnchanged.RecordDate = DateTime.Now;
                            document.DocumentCostUnchangeds.Add(documentCostUnchanged);
                        }
                    }
                    else if (DocumentCostViewModel.IsDelete)
                    {
                        var x = document.DocumentCosts.FirstOrDefault(x =>
                            x.Id == DocumentCostViewModel.RequestId.ToGuid());
                        document.DocumentCosts.Remove(document.DocumentCosts.First(x =>
                            x.Id == DocumentCostViewModel.RequestId.ToGuid()));
                        if (!string.IsNullOrEmpty(DocumentCostViewModel.RequestPriceUnchanged))
                        {
                            document.DocumentCostUnchangeds.Remove(document.DocumentCostUnchangeds.First(x =>
                                x.Id == DocumentCostViewModel.RequestUnchangedId.ToGuid()));

                        }
                    }
                    else if (DocumentCostViewModel.IsDirty)
                    {
                        DocumentCost updatingDocumentCost =
                            document.DocumentCosts.First(x => x.Id == DocumentCostViewModel.RequestId.ToGuid());
                        DocuemntStandardContractCostMapper.MapToDocumentStandardContractCost(ref updatingDocumentCost, DocumentCostViewModel);
                        if (!string.IsNullOrEmpty(DocumentCostViewModel.RequestPriceUnchanged))
                        {
                            DocumentCostUnchanged updatingDocumentCostUnchanged =
                                document.DocumentCostUnchangeds.First(x =>
                                    x.Id == DocumentCostViewModel.RequestUnchangedId.ToGuid());
                            DocuemntStandardContractCostMapper.MapToDocumentStandardContractCostUnchanged(ref updatingDocumentCostUnchanged,
                                DocumentCostViewModel);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }
            if (request.DocumentPayments?.Count > 0)
            {
                foreach (DocumentStandardContractPaymentViewModel documentPaymentViewModel in request.DocumentPayments)
                {
                    DocumentPayment documentPayment = new();

                    if (documentPaymentViewModel.IsNew)
                    {
                        DocumentStandardContractPaymentMapper.MapToDocumentStandardContractPayment(ref documentPayment, documentPaymentViewModel, document.ScriptoriumId);
                        document.DocumentPayments.Add(documentPayment);
                    }
                    else if (documentPaymentViewModel.IsDelete)
                    {
                        documentPayment = document.DocumentPayments.Where(y => y.Id == Guid.Parse(documentPaymentViewModel.Id)).First();
                        document.DocumentPayments.Remove(documentPayment);
                    }
                    else if (documentPaymentViewModel.IsDirty)
                    {
                        documentPayment = document.DocumentPayments.Where(y => y.Id == Guid.Parse(documentPaymentViewModel.Id)).First();
                        DocumentStandardContractPaymentMapper.MapToDocumentStandardContractPayment(ref documentPayment, documentPaymentViewModel, document.ScriptoriumId);
                    }

                }
                ;
            }
            //if (request.DocumentSms?.Count > 0)
            //{
            //    foreach (DocumentSmsViewModel documentSmsViewModel in request.DocumentSms)
            //    {
            //        DocumentSm documentSms = new();

            //        if (documentSmsViewModel.IsNew)
            //        {

            //            DocumentSmsMapper.MapToDocumentSms(ref documentSms, documentSmsViewModel);
            //            documentSms.ScriptoriumId = document.ScriptoriumId;
            //            documentSms.Ilm = DocumentConstants.CreateIlm;
            //            documentSms.RecordDate = dateTimeService.CurrentDateTime;
            //            documentSms.CreateDate = dateTimeService.CurrentPersianDate;
            //            documentSms.CreateTime = dateTimeService.CurrentTime.Substring(0,5);
            //            documentSms.DocumentId = document.Id;
            //            document.DocumentSms.Add(documentSms);
            //        }
            //        else if (documentSmsViewModel.IsDelete)
            //        {
            //            documentSms = document.DocumentSms.Where(y => y.Id == Guid.Parse(documentSmsViewModel.Id)).First();
            //            document.DocumentSms.Remove(documentSms);
            //        }
            //        else if (documentSmsViewModel.IsDirty)
            //        {
            //            documentSms = document.DocumentSms.Where(y => y.Id == Guid.Parse(documentSmsViewModel.Id)).First();
            //            DocumentSmsMapper.MapToDocumentSms(ref documentSms, documentSmsViewModel);
            //            documentSms.Ilm = DocumentConstants.UpdateIlm;
            //        }

            //    }
            //   ;
            //}
            if (document.DocumentInfoConfirm != null)
            {
                DocumentInfoConfirm updatingDocumentInfoConfirm = document.DocumentInfoConfirm;
                updatingDocumentInfoConfirm.ScriptoriumId = document.ScriptoriumId;
                updatingDocumentInfoConfirm.DocumentId = document.Id;
                updatingDocumentInfoConfirm.Id = document.DocumentInfoConfirm.Id;
                updatingDocumentInfoConfirm.Ilm = DocumentConstants.UpdateIlm;
                updatingDocumentInfoConfirm.CreateDate = dateTimeService.CurrentPersianDate;
                updatingDocumentInfoConfirm.CreateTime = dateTimeService.CurrentTime;
                updatingDocumentInfoConfirm.CreatorNameFamily = userService.UserApplicationContext.User.Name + " " +
                                                                userService.UserApplicationContext.User.Family;
            }
            else if (document.DocumentInfoConfirm == null)
            {
                DocumentInfoConfirm newDocumentInfoConfirm = new();
                newDocumentInfoConfirm.Id = Guid.NewGuid();
                newDocumentInfoConfirm.DocumentId = document.Id;
                newDocumentInfoConfirm.ScriptoriumId = document.ScriptoriumId;
                newDocumentInfoConfirm.Ilm = DocumentConstants.UpdateIlm;
                newDocumentInfoConfirm.RecordDate = dateTimeService.CurrentDateTime;
                newDocumentInfoConfirm.CreateDate = dateTimeService.CurrentPersianDate;
                newDocumentInfoConfirm.CreateTime = dateTimeService.CurrentTime;
                newDocumentInfoConfirm.CreatorNameFamily = userService.UserApplicationContext.User.Name + " " +
                                                           userService.UserApplicationContext.User.Family;

                document.DocumentInfoConfirm = newDocumentInfoConfirm;

            }

            if (request.IsNew == true && request.DocumentInfoOther == null)
            {
                DocumentInfoOther documentInfoOther = new();
                documentInfoOther.Id = Guid.NewGuid();
                documentInfoOther.DocumentId = document.Id;
                documentInfoOther.ScriptoriumId = document.ScriptoriumId;
                documentInfoOther.Ilm = DocumentConstants.CreateIlm;
                documentInfoOther.RecordDate = dateTimeService.CurrentDateTime;
                documentInfoOther.DocumentTypeSubjectId = request.DocumentTypeSubjectId != null
                    ? request.DocumentTypeSubjectId.FirstOrDefault()
                    : null;
                documentInfoOther.DocumentAssetTypeId = request.DocumentAssetTypeId != null
                    ? request.DocumentAssetTypeId.FirstOrDefault()
                    : null;
                document.DocumentInfoOther = documentInfoOther;
            }

            List<PersonPair> persons = new List<PersonPair>();

            if (request.IsNew == true && !string.IsNullOrEmpty(request.DocumentCopyId))
            {
                var masterCopyEntity = await documentRepository.GetDocumentById(request.DocumentCopyId.ToGuid(),
                    loadDocumentCoordinator.GetCopyDetails(request), cancellationToken);
                if (request.IsCopyDocumentPeople == true)
                {
                    IList<DocumentPerson> DocumentCopyPeople = masterCopyEntity.DocumentPeople.ToList();
                    for (int i = 0; i < DocumentCopyPeople.Count; i++)
                    {
                        DocumentPerson documentPerson = new();
                        documentPerson.MobileNo = DocumentCopyPeople[i].MobileNo;
                        documentPerson.IsAlive = DocumentCopyPeople[i].IsAlive;
                        documentPerson.IsSabtahvalCorrect = DocumentCopyPeople[i].IsSabtahvalCorrect;
                        documentPerson.IsSabtahvalChecked = DocumentCopyPeople[i].IsSabtahvalChecked;
                        documentPerson.MobileNoState = DocumentCopyPeople[i].MobileNoState;
                        documentPerson.IsOriginal = DocumentCopyPeople[i].IsOriginal;
                        documentPerson.SanaState = DocumentCopyPeople[i].SanaState;
                        documentPerson.Serial = DocumentCopyPeople[i].Serial;
                        documentPerson.Seri = DocumentCopyPeople[i].Seri;
                        documentPerson.SeriAlpha = DocumentCopyPeople[i].SeriAlpha;
                        documentPerson.IdentityNo = DocumentCopyPeople[i].IdentityNo;
                        documentPerson.IdentityIssueLocation = DocumentCopyPeople[i].IdentityIssueLocation;
                        documentPerson.FatherName = DocumentCopyPeople[i].FatherName;
                        documentPerson.BirthDate = DocumentCopyPeople[i].BirthDate;
                        documentPerson.Family = DocumentCopyPeople[i].Family;
                        documentPerson.Name = DocumentCopyPeople[i].Name;
                        documentPerson.NationalNo = DocumentCopyPeople[i].NationalNo;
                        documentPerson.NationalityId = DocumentCopyPeople[i].NationalityId;
                        documentPerson.IsIranian = DocumentCopyPeople[i].IsIranian;
                        documentPerson.IsRelated = DocumentCopyPeople[i].IsRelated;
                        documentPerson.IsFingerprintGotten = DocumentCopyPeople[i].IsFingerprintGotten;
                        documentPerson.PassportNo = DocumentCopyPeople[i].PassportNo;
                        documentPerson.Description = DocumentCopyPeople[i].Description;
                        documentPerson.Tel = DocumentCopyPeople[i].Tel;
                        documentPerson.AddressType = DocumentCopyPeople[i].AddressType;
                        documentPerson.Address = DocumentCopyPeople[i].Address;
                        documentPerson.PostalCode = DocumentCopyPeople[i].PostalCode;
                        documentPerson.Email = DocumentCopyPeople[i].Email;
                        documentPerson.SexType = DocumentCopyPeople[i].SexType;
                        documentPerson.DocumentPersonTypeId = masterCopyEntity.DocumentTypeId == document.DocumentTypeId
                            ? DocumentCopyPeople[i].DocumentPersonTypeId
                            : null;
                        documentPerson.LegalpersonTypeId = DocumentCopyPeople[i].LegalpersonTypeId;
                        documentPerson.LegalpersonNatureId = DocumentCopyPeople[i].LegalpersonNatureId;
                        documentPerson.CompanyTypeId = DocumentCopyPeople[i].CompanyTypeId;
                        documentPerson.PersonType = DocumentCopyPeople[i].PersonType;
                        documentPerson.Id = Guid.NewGuid();
                        documentPerson.DocumentId = document.Id;
                        documentPerson.RowNo = (byte)(DocumentCopyPeople.Count - i);
                        documentPerson.ScriptoriumId = document.ScriptoriumId;
                        documentPerson.FaxNo = DocumentCopyPeople[i].FaxNo;
                        documentPerson.Ilm = DocumentConstants.CreateIlm;
                        documentPerson.HasGrowthJudgment = DocumentCopyPeople[i].HasGrowthJudgment;
                        documentPerson.GrowthDescription = DocumentCopyPeople[i].GrowthDescription;
                        documentPerson.GrowthJudgmentDate = DocumentCopyPeople[i].GrowthJudgmentDate;
                        documentPerson.GrowthJudgmentNo = DocumentCopyPeople[i].GrowthJudgmentNo;
                        documentPerson.GrowthLetterDate = DocumentCopyPeople[i].GrowthLetterDate;
                        documentPerson.GrowthLetterNo = DocumentCopyPeople[i].GrowthLetterNo;
                        documentPerson.IsMartyrApplicant = DocumentCopyPeople[i].IsMartyrApplicant;
                        documentPerson.MartyrCode = DocumentCopyPeople[i].MartyrCode;
                        documentPerson.MartyrDescription = DocumentCopyPeople[i].MartyrDescription;
                        documentPerson.MartyrInquiryDate = DocumentCopyPeople[i].MartyrInquiryDate;
                        documentPerson.MartyrInquiryTime = DocumentCopyPeople[i].MartyrInquiryTime;
                        documentPerson.IsMartyrIncluded = DocumentCopyPeople[i].IsMartyrIncluded;
                        document.DocumentPeople.Add(documentPerson);
                        persons.Add(new PersonPair(documentPerson.Id, DocumentCopyPeople[i].Id));
                    }
                }

                if (request.IsCopyDocumentCases == true)
                {
                    IList<DocumentCase> DocumentCopyCase = masterCopyEntity.DocumentCases.ToList();
                    for (int i = 0; i < DocumentCopyCase.Count; i++)
                    {
                        DocumentCase documentCase = new();
                        documentCase.Description = DocumentCopyCase[i].Description;
                        documentCase.Title = DocumentCopyCase[i].Title;
                        documentCase.Id = Guid.NewGuid();
                        documentCase.DocumentId = document.Id;
                        documentCase.RowNo = (byte)(DocumentCopyCase.Count - i);
                        documentCase.ScriptoriumId = document.ScriptoriumId;
                        documentCase.Ilm = DocumentConstants.CreateIlm;
                        document.DocumentCases.Add(documentCase);
                    }

                    IList<DocumentVehicle> DocumentCopyVehicles = masterCopyEntity.DocumentVehicles.ToList();
                    foreach (var copyVehicle in DocumentCopyVehicles)

                    {
                        DocumentVehicle documentVehicle = new();
                        documentVehicle.Description = copyVehicle.Description;
                        documentVehicle.Id = Guid.NewGuid();
                        documentVehicle.DocumentId = document.Id;
                        documentVehicle.ScriptoriumId = document.ScriptoriumId;
                        documentVehicle.Ilm = DocumentConstants.CreateIlm;
                        documentVehicle.OldDocumentDate = copyVehicle.OldDocumentDate;
                        documentVehicle.OldDocumentIssuer = copyVehicle.OldDocumentIssuer;
                        documentVehicle.OldDocumentNo = copyVehicle.OldDocumentNo;
                        documentVehicle.OwnershipPrintedDocumentNo = copyVehicle.OwnershipPrintedDocumentNo;
                        documentVehicle.InssuranceNo = copyVehicle.InssuranceNo;
                        documentVehicle.InssuranceCo = copyVehicle.InssuranceCo;
                        documentVehicle.OtherInfo = copyVehicle.OtherInfo;
                        documentVehicle.FuelCardNo = copyVehicle.FuelCardNo;
                        documentVehicle.DutyFicheNo = copyVehicle.DutyFicheNo;
                        documentVehicle.CardNo = copyVehicle.CardNo;
                        documentVehicle.CylinderCount = copyVehicle.CylinderCount;
                        documentVehicle.Color = copyVehicle.Color;
                        documentVehicle.EngineCapacity = copyVehicle.EngineCapacity;
                        documentVehicle.ChassisNo = copyVehicle.ChassisNo;
                        documentVehicle.EngineNo = copyVehicle.EngineNo;
                        documentVehicle.Model = copyVehicle.Model;
                        documentVehicle.Tip = copyVehicle.Tip;
                        documentVehicle.System = copyVehicle.System;
                        documentVehicle.Type = copyVehicle.Type;
                        documentVehicle.MadeInIran = copyVehicle.MadeInIran;
                        documentVehicle.IsInTaxList = copyVehicle.IsInTaxList;
                        documentVehicle.RowNo = copyVehicle.RowNo;
                        documentVehicle.Price = copyVehicle.Price;
                        documentVehicle.PlaqueBuyer = copyVehicle.PlaqueBuyer;
                        documentVehicle.PlaqueNoAlphaBuyer = copyVehicle.PlaqueNoAlphaBuyer;
                        documentVehicle.PlaqueSeriBuyer = copyVehicle.PlaqueSeriBuyer;
                        documentVehicle.PlaqueNo2Buyer = copyVehicle.PlaqueNo2Buyer;
                        documentVehicle.PlaqueNo1Buyer = copyVehicle.PlaqueNo1Buyer;
                        documentVehicle.PlaqueSeller = copyVehicle.PlaqueSeller;
                        documentVehicle.PlaqueNoAlphaSeller = copyVehicle.PlaqueNoAlphaSeller;
                        documentVehicle.PlaqueSeriSeller = copyVehicle.PlaqueSeriSeller;
                        documentVehicle.PlaqueNo2Seller = copyVehicle.PlaqueNo2Seller;
                        documentVehicle.PlaqueNo1Seller = copyVehicle.PlaqueNo1Seller;
                        documentVehicle.NumberingLocation = copyVehicle.NumberingLocation;
                        documentVehicle.IsVehicleNumbered = copyVehicle.IsVehicleNumbered;
                        documentVehicle.QuotaText = copyVehicle.QuotaText;
                        documentVehicle.Description = copyVehicle.Description;
                        documentVehicle.SellTotalQuota = copyVehicle.SellTotalQuota;
                        documentVehicle.SellDetailQuota = copyVehicle.SellDetailQuota;
                        documentVehicle.OwnershipTotalQuota = copyVehicle.OwnershipTotalQuota;
                        documentVehicle.OwnershipDetailQuota = copyVehicle.OwnershipDetailQuota;
                        documentVehicle.OwnershipType = copyVehicle.OwnershipType;
                        documentVehicle.DocumentVehicleTipId = copyVehicle.DocumentVehicleTipId;
                        documentVehicle.DocumentVehicleTypeId = copyVehicle.DocumentVehicleTypeId;
                        documentVehicle.DocumentVehicleSystemId = copyVehicle.DocumentVehicleSystemId;

                        if (request.IsCopyDocumentPeople == true)
                        {
                            foreach (var item in copyVehicle.DocumentVehicleQuotaDetails)
                            {
                                DocumentVehicleQuotaDetail newDocumentVehicleQuotaDetail =
                                    new DocumentVehicleQuotaDetail();
                                newDocumentVehicleQuotaDetail.DocumentVehicleId = documentVehicle.Id;
                                newDocumentVehicleQuotaDetail.Id = Guid.NewGuid();
                                newDocumentVehicleQuotaDetail.QuotaText = item.QuotaText;
                                if (item.DocumentPersonBuyerId != null)
                                {
                                    var person = persons.Where(p => p.PersonCopyId == item.DocumentPersonBuyerId)
                                        .FirstOrDefault();
                                    newDocumentVehicleQuotaDetail.DocumentPersonBuyerId = person?.PersonId;

                                }

                                newDocumentVehicleQuotaDetail.DocumentPersonSellerId = persons
                                    .Where(p => p.PersonCopyId == item.DocumentPersonSellerId).First().PersonId;

                                newDocumentVehicleQuotaDetail.OwnershipDetailQuota = item.OwnershipDetailQuota;
                                newDocumentVehicleQuotaDetail.OwnershipTotalQuota = item.OwnershipTotalQuota;
                                newDocumentVehicleQuotaDetail.ScriptoriumId = document.ScriptoriumId;
                                newDocumentVehicleQuotaDetail.Ilm = item.Ilm;
                                documentVehicle.DocumentVehicleQuotaDetails.Add(newDocumentVehicleQuotaDetail);
                            }

                            foreach (var item in copyVehicle.DocumentVehicleQuota)
                            {
                                DocumentVehicleQuotum NewDocumentVehicleQuotum = new DocumentVehicleQuotum();
                                NewDocumentVehicleQuotum.DocumentVehicleId = documentVehicle.Id;
                                NewDocumentVehicleQuotum.Id = Guid.NewGuid();
                                NewDocumentVehicleQuotum.QuotaText = item.QuotaText;
                                var person = persons.Where(p => p.PersonCopyId == item.DocumentPersonId).First();
                                NewDocumentVehicleQuotum.DocumentPersonId = person.PersonId;
                                NewDocumentVehicleQuotum.DetailQuota = item.DetailQuota;
                                NewDocumentVehicleQuotum.TotalQuota = item.TotalQuota;
                                NewDocumentVehicleQuotum.ScriptoriumId = document.ScriptoriumId;
                                NewDocumentVehicleQuotum.Ilm = item.Ilm;
                                documentVehicle.DocumentVehicleQuota.Add(NewDocumentVehicleQuotum);
                            }
                        }

                        document.DocumentVehicles.Add(documentVehicle);
                    }
                }

                if (request.IsCopyDocumentInfoText == true)
                {
                    DocumentInfoText DocumentCopyInfoText = masterCopyEntity.DocumentInfoText;
                    DocumentInfoText documentInfoText = new();
                    documentInfoText.DocumentText = DocumentCopyInfoText?.DocumentText;
                    documentInfoText.LegalText = DocumentCopyInfoText?.LegalText;
                    documentInfoText.DocumentDescription = DocumentCopyInfoText?.DocumentDescription;
                    documentInfoText.Description = DocumentCopyInfoText?.Description;
                    documentInfoText.DocumentId = document.Id;
                    documentInfoText.Id = Guid.NewGuid();
                    documentInfoText.ScriptoriumId = document.ScriptoriumId;
                    documentInfoText.Ilm = DocumentConstants.CreateIlm;
                    documentInfoText.RecordDate = dateTimeService.CurrentDateTime;
                    document.DocumentInfoText = documentInfoText;
                }
            }
            else if (!string.IsNullOrEmpty(request.DocumentTemplateId))
            {
                var documentTemplateEntity =
                    await documentTemplateRepository.GetByIdAsync(cancellationToken,
                        request.DocumentTemplateId.ToGuid());
                if (documentTemplateEntity != null)
                {
                    DocumentInfoText documentInfoText = new();
                    documentInfoText.DocumentId = document.Id;
                    documentInfoText.Id = Guid.NewGuid();
                    documentInfoText.ScriptoriumId = document.ScriptoriumId;
                    documentInfoText.Ilm = DocumentConstants.CreateIlm;
                    documentInfoText.LegalText = documentTemplateEntity.Text;
                    documentInfoText.RecordDate = dateTimeService.CurrentDateTime;
                    document.DocumentInfoText = documentInfoText;

                }
            }
        }

        /// <summary>
        /// The ValidateDocumentBeforeSave
        /// </summary>
        /// <returns>The <see cref="Task{(bool, List{string})}"/></returns>
        public async Task<(bool, List<string>)> ValidateDocumentBeforeSave()
        {
            string response = "";
            bool isValid = true;
            List<string> errorMessages = new List<string>();
            List<string> validationAgentTypes = new List<string>()
            {
                "1", // وکالتنامه 
                "13" // وصیت نامه
            };

            if (document != null && document.DocumentPersonRelatedDocuments.Any(d =>
                    d.IsRelatedDocumentInSsar == YesNo.Yes.GetString() && validationAgentTypes.Contains(d.AgentTypeId)))
            {
                var validateRelatedPersonResult = await validateRelatedPerson(document);
                var messages = validateRelatedPersonResult?.Where(v => v.Response == false)
                    .Select(v => v.ResponseMessage).ToList();

                if (messages != null && messages.Any())
                {
                    isValid = false;
                    foreach (var message in messages)
                    {
                        if (message.Contains("-"))
                        {
                            foreach (var item in message.Split("-"))
                            {
                                errorMessages.Add(item);
                            }
                        }
                        else
                        {
                            errorMessages.Add(message);
                        }
                    }

                }
                else
                {
                    foreach (var relatedPerson in document.DocumentPersonRelatedDocuments)
                    {
                        if (validateRelatedPersonResult != null)
                        {
                            string? docId = validateRelatedPersonResult
                                .Where(v => v.CurrentDocAgentObjectID == relatedPerson.Id.ToString())
                                .Select(v => v.CorrespondingRegisterServiceReqObjectID).FirstOrDefault();
                            if (docId != null)
                            {
                                relatedPerson.AgentDocumentId = Guid.Parse(docId);

                            }
                        }

                    }

                }

            }

            bool isCentralizedValidatorEnabled = SSAA.BO.Configuration.Settings.isCentralizedValidatorEnabled();
            RelatedDocumentValidationRequest relatedDocumentValidationRequest = new RelatedDocumentValidationRequest();

            if (document != null && document.State != NotaryRegServiceReqState.CanceledAfterGetCode.GetString()
                                 && document.State != NotaryRegServiceReqState.CanceledBeforeGetCode.GetString() &&
                                 document?.IsRelatedDocAbroad != YesNo.Yes.GetString() &&
                                 document?.RelatedDocumentIsInSsar == YesNo.Yes.GetString())
            {

                relatedDocumentValidationRequest.DocumentDate = document.RelatedDocumentDate;
                relatedDocumentValidationRequest.DocumentNationalNo = document.RelatedDocumentNo;
                relatedDocumentValidationRequest.DocumentScriptoriumId =
                    document.RelatedScriptoriumId != null ? document.RelatedScriptoriumId : null;
                relatedDocumentValidationRequest.DocumentTypeID = document.RelatedDocumentTypeId;
                relatedDocumentValidationRequest.IsRelatedDocumentInSSAR =
                    document.RelatedDocumentIsInSsar == YesNo.Yes.GetString() ? YesNo.Yes : YesNo.No;
                RelatedDocumentValidationResponse relatedDocumentValidationResponse =
                    new RelatedDocumentValidationResponse();
                if (isCentralizedValidatorEnabled)
                {
                    (relatedDocumentValidationResponse, response) =
                        await validatorGateway.ValidateRelatedDocument(relatedDocumentValidationRequest, response);
                    if (relatedDocumentValidationResponse.ValidationResult)
                    {
                        if (relatedDocumentValidationResponse.registerServiceReqObjectID != null)
                            document.RelatedDocumentId =
                                Guid.Parse(relatedDocumentValidationResponse.registerServiceReqObjectID);

                    }
                    else
                    {
                        if (relatedDocumentValidationResponse.ValidationResponseMessage.Contains("-"))
                        {
                            relatedDocumentValidationResponse.ValidationResponseMessage.Split("-").ToList().ForEach(x =>

                                errorMessages.Add(x)

                            );

                        }
                        else
                        {
                            errorMessages.Add(relatedDocumentValidationResponse.ValidationResponseMessage);

                        }

                        isValid = false;
                    }
                }
            }

            if (document != null && document.DocumentRelationDocuments.Any())
            {
                foreach (var documentRelation in document.DocumentRelationDocuments)
                {
                    if (documentRelation.IsRelatedDocAbroad != YesNo.Yes.GetString() &&
                        documentRelation.RelatedDocumentIsInSsar == YesNo.Yes.GetString())
                    {
                        relatedDocumentValidationRequest.DocumentDate = documentRelation.RelatedDocumentDate;
                        relatedDocumentValidationRequest.DocumentNationalNo = documentRelation.RelatedDocumentNo;
                        relatedDocumentValidationRequest.DocumentScriptoriumId =
                            documentRelation.RelatedScriptoriumId != null
                                ? documentRelation.RelatedScriptoriumId
                                : null;
                        relatedDocumentValidationRequest.DocumentTypeID = documentRelation.RelatedDocumentTypeId;
                        relatedDocumentValidationRequest.IsRelatedDocumentInSSAR =
                            documentRelation.RelatedDocumentIsInSsar == YesNo.Yes.GetString() ? YesNo.Yes : YesNo.No;
                        RelatedDocumentValidationResponse relatedDocumentValidationResponse =
                            new RelatedDocumentValidationResponse();
                        if (isCentralizedValidatorEnabled)
                        {
                            (relatedDocumentValidationResponse, response) =
                                await validatorGateway.ValidateRelatedDocument(relatedDocumentValidationRequest,
                                    response);
                            if (relatedDocumentValidationResponse.ValidationResult)
                            {
                                if (relatedDocumentValidationResponse.registerServiceReqObjectID != null)
                                    documentRelation.RelatedDocumentId =
                                        Guid.Parse(relatedDocumentValidationResponse.registerServiceReqObjectID);

                            }
                            else
                            {
                                if (relatedDocumentValidationResponse.ValidationResponseMessage.Contains("-"))
                                {
                                    relatedDocumentValidationResponse.ValidationResponseMessage.Split("-").ToList()
                                        .ForEach(x =>

                                            errorMessages.Add(x)

                                        );

                                }
                                else
                                {
                                    errorMessages.Add(relatedDocumentValidationResponse.ValidationResponseMessage);

                                }

                                isValid = false;
                            }
                        }
                    }

                }

            }

            return (isValid, errorMessages);
        }

        /// <summary>
        /// The CreateDocumentNo
        /// </summary>
        /// <param name="Scriptoriumid">The Scriptoriumid<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{string}"/></returns>
        private async Task<string> CreateDocumentNo(string Scriptoriumid, CancellationToken cancellationToken)
        {
            var beginReqNo = dateTimeService.CurrentPersianDate[..4];
            beginReqNo += "401";
            beginReqNo += Scriptoriumid;
            string docNo = await documentRepository.GetMaxDocNo(beginReqNo, cancellationToken);
            if (string.IsNullOrWhiteSpace(docNo))
            {
                docNo = dateTimeService.CurrentPersianDate[..4];
                docNo += "401";
                docNo += Scriptoriumid;
                docNo += "000001";
            }
            else
            {
                decimal numberdocNo = decimal.Parse(docNo);
                numberdocNo++;
                docNo = numberdocNo.ToString();
            }

            return docNo;
        }

        /// <summary>
        /// The GetNationalNo
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{string}"/></returns>
        public async Task<string> GetNationalNo(CancellationToken cancellationToken)
        {
            string result = "";
            string formCode = "";
            if (document.DocumentTypeId.Length >= 4)
                formCode = document.DocumentTypeId.Substring(1, 3);
            else
                formCode = document.DocumentTypeId;
            if (formCode.Length > 3)
            {
                formCode = formCode.Substring(1, 3);
            }

            string template =
                $"{dateTimeService.CurrentPersianDate[..4]}{formCode}{userService.UserApplicationContext.ScriptoriumInformation.Code}";

            string nationalNo = await documentRepository.GetMaxDocumentNationalNo(template, cancellationToken);

            if (string.IsNullOrEmpty(nationalNo))
            {

                result = $"{template}000001";
            }
            else
            {
                string strMaxNo = nationalNo.ToString().Substring(12);
                int maxNo = System.Convert.ToInt32(strMaxNo);
                maxNo++;
                result = $"{template}{maxNo.ToString().PadLeft(6, '0')}";
            }

            return result;
        }

        /// <summary>
        /// The GetInquiries
        /// </summary>
        /// <param name="documentRelatedDataQuery">The documentRelatedDataQuery<see cref="GetDocumentRelatedDataQuery"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{(string,bool)}"/></returns>
        public async Task<(string?, bool)> GetInquiries(
            GetDocumentRelatedDataQuery documentRelatedDataQuery, CancellationToken cancellationToken)
        {
            string errorMessage = null;
            bool HasAllarmMessage = false;

            try
            {
                ApiResult<
                        SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry.GetDocumentRelatedDataViewModel>
                    documentRelatedData = await _mediator.Send(documentRelatedDataQuery, cancellationToken);
                if (documentRelatedData.IsSuccess)
                {

                    HasAllarmMessage = documentRelatedData.Data.HasAlarmMessage;
                    if (HasAllarmMessage)
                    {
                        errorMessage = documentRelatedData.Data.AlarmMessage;
                    }
                    else if (documentRelatedData.Data.DocumentRelatedData != null)
                    {

                        var relatedData = documentRelatedData.Data.DocumentRelatedData;
                        short personRowNo = 1;
                        short estateRowNo = 1;
                        short estateOwnershipRowNo = 1;

                        relatedData.DocumentPersonList.ForEach((person) =>
                        {
                            SSAA.BO.Domain.Entities.DocumentPerson documentPerson =
                               new SSAA.BO.Domain.Entities.DocumentPerson();

                            documentPerson.Id = person.Id;
                            documentPerson.DocumentId = document.Id;
                            documentPerson.BirthDate = person.BirthDate;
                            documentPerson.Name = person.Name;
                            documentPerson.Family = person.Family;
                            documentPerson.NationalNo = person.NationalNo;
                            ;
                            documentPerson.NationalityId = person.NationalityId;
                            documentPerson.IsRelated = person.IsRelated;
                            documentPerson.PersonType = person.PersonType;
                            documentPerson.IsIranian = person.IsIranian;
                            documentPerson.IsOriginal = person.IsOriginal;
                            documentPerson.SexType = person.SexType;
                            documentPerson.FatherName = person.FatherName;
                            documentPerson.IdentityIssueLocation = person.IdentityIssueLocation;
                            documentPerson.IdentityIssueGeoLocationId = person.IdentityIssueGeoLocationId;
                            documentPerson.IdentityNo = person.IdentityNo;
                            documentPerson.SeriAlpha = person.SeriAlpha;
                            documentPerson.Seri = person.Seri;
                            documentPerson.Serial = person.Serial;
                            documentPerson.Address = person.Address;
                            documentPerson.PostalCode = person.PostalCode;
                            documentPerson.CompanyRegisterNo = person.CompanyRegisterNo;
                            documentPerson.CompanyRegisterDate = person.CompanyRegisterDate;
                            //documentPerson.IsSabtahvalChecked = person.IsSabtahvalChecked;
                            //documentPerson.IsSabtahvalCorrect = person.IsSabtahvalCorrect;
                            documentPerson.MobileNo = person.MobileNo;
                            // documentPerson.MobileNoState = person.MobileNoState;
                            //documentPerson.SanaState = person.SanaState;
                            documentPerson.ScriptoriumId = person.ScriptoriumId;
                            documentPerson.EstateInquiryId = person.EstateInquiryId;
                            documentPerson.Ilm = DocumentConstants.CreateIlm;
                            documentPerson.RowNo = personRowNo;
                            personRowNo++;
                            document.DocumentPeople.Add(documentPerson);

                        });
                        relatedData.DocumentEstateList.ForEach((estate) =>
                        {
                            SSAA.BO.Domain.Entities.DocumentEstate documentEstate =
                               new SSAA.BO.Domain.Entities.DocumentEstate();
                            documentEstate.Id = estate.Id;
                            documentEstate.DocumentId = document.Id;
                            documentEstate.Address = estate.Address;
                            documentEstate.Area = estate.Area;
                            documentEstate.GeoLocationId = estate.GeoLocationId;
                            documentEstate.IsAttachment = estate.IsAttachment;
                            documentEstate.IsRegistered = estate.IsRegistered;
                            documentEstate.IsProportionateQuota = estate.IsProportionateQuota;
                            documentEstate.BasicPlaque = estate.BasicPlaque;
                            documentEstate.BasicPlaqueHasRemain = estate.BasicPlaqueHasRemain;
                            documentEstate.SecondaryPlaqueHasRemain = estate.SecondaryPlaqueHasRemain;
                            documentEstate.SecondaryPlaque = estate.SecondaryPlaque;
                            documentEstate.UnitId = estate.UnitId;
                            documentEstate.EstateSectionId = estate.EstateSectionId;
                            documentEstate.EstateSubsectionId = estate.EstateSubsectionId;
                            documentEstate.PostalCode = estate.PostalCode;
                            documentEstate.EstateInquiryId = estate.EstateInquiryId;
                            documentEstate.Ilm = DocumentConstants.CreateIlm;
                            documentEstate.ScriptoriumId = estate.ScriptoriumId;
                            documentEstate.RowNo = estateRowNo;
                            documentEstate.OwnershipDetailQuota = estate.OwnershipDetailQuota;
                            documentEstate.OwnershipTotalQuota = estate.OwnershipTotalQuota;
                            estateRowNo++;
                            document.DocumentEstates.Add(documentEstate);

                            var estateInquiryIdList = documentEstate.EstateInquiryId.Split(',').Select(x => x.ToGuid()).ToList();
                            var estateInquiryList = relatedData.DocumentEstateInquiryList.Where(x => estateInquiryIdList.Contains(x.EstateInquiryId)).ToList();
                            estateInquiryList.ForEach((estateInquiry) =>
                            {
                                var inquiry = new SSAA.BO.Domain.Entities.DocumentEstateInquiry();
                                inquiry.Id = estateInquiry.Id;
                                inquiry.EstateInquiryId = estateInquiry.EstateInquiryId;
                                inquiry.ScriptoriumId = estateInquiry.ScriptoriumId;
                                inquiry.Ilm = DocumentConstants.CreateIlm;
                                inquiry.DocumentEstateId = documentEstate.Id;
                                documentEstate.DocumentEstateInquiries.Add(inquiry);
                            });

                        });
                        relatedData.DocumentEstateOwnershipDocumentList.ForEach((estateOwnership) =>
                        {
                            SSAA.BO.Domain.Entities.DocumentEstateOwnershipDocument estateOwnershipDocument =
                               new SSAA.BO.Domain.Entities.DocumentEstateOwnershipDocument();


                            estateOwnershipDocument.Id = estateOwnership.Id;
                            estateOwnershipDocument.DocumentEstateId = estateOwnership.DocumentEstateId;
                            estateOwnershipDocument.DocumentPersonId = estateOwnership.DocumentPersonId;
                            estateOwnershipDocument.OwnershipDocumentType = estateOwnership.OwnershipDocumentType;
                            estateOwnershipDocument.EstateSabtNo = estateOwnership.EstateSabtNo;
                            estateOwnershipDocument.EstateDocumentNo = estateOwnership.EstateDocumentNo;
                            estateOwnershipDocument.EstateBookNo = estateOwnership.EstateBookNo;
                            estateOwnershipDocument.EstateBookPageNo = estateOwnership.EstateBookPageNo;
                            estateOwnershipDocument.EstateBookType = estateOwnership.EstateBookType;
                            estateOwnershipDocument.EstateElectronicPageNo = estateOwnership.EstateElectronicPageNo;
                            estateOwnershipDocument.EstateSeridaftarId = estateOwnership.EstateSeridaftarId;
                            estateOwnershipDocument.EstateIsReplacementDocument =
                                estateOwnership.EstateIsReplacementDocument;
                            estateOwnershipDocument.MortgageText = estateOwnership.MortgageText;
                            estateOwnershipDocument.EstateDocumentType = estateOwnership.EstateDocumentType;
                            estateOwnershipDocument.ScriptoriumId = estateOwnership.ScriptoriumId;
                            estateOwnershipDocument.Ilm = DocumentConstants.CreateIlm;
                            estateOwnershipDocument.EstateInquiriesId = estateOwnership.EstateInquiriesId;
                            estateOwnershipDocument.RowNo = estateOwnershipRowNo;
                            estateOwnershipRowNo++;

                            foreach (var documentDocumentEstate in document.DocumentEstates)
                            {
                                if (documentDocumentEstate.Id == estateOwnershipDocument.DocumentEstateId)
                                {
                                    documentDocumentEstate.DocumentEstateOwnershipDocuments
                                        .Add(estateOwnershipDocument);
                                }
                            }

                        });

                        relatedData.DocumentInquiryList.ForEach((inquiry) =>
                        {
                            SSAA.BO.Domain.Entities.DocumentInquiry documentInquiry =
                               new SSAA.BO.Domain.Entities.DocumentInquiry();
                            documentInquiry.DocumentId = document.Id;
                            documentInquiry.Id = inquiry.Id;
                            documentInquiry.DocumentInquiryOrganizationId = inquiry.DocumentInquiryOrganizationId;
                            documentInquiry.EstateInquiriesId = inquiry.EstateInquiriesId;
                            documentInquiry.DocumentId = inquiry.DocumentId;
                            documentInquiry.RequestNo = inquiry.RequestNo;
                            documentInquiry.RequestDate = inquiry.RequestDate;
                            documentInquiry.ReplyNo = inquiry.ReplyNo;
                            documentInquiry.ReplyDate = inquiry.ReplyDate;
                            documentInquiry.ReplyText = inquiry.ReplyText;
                            documentInquiry.ReplyDetailQuota = inquiry.ReplyDetailQuota;
                            documentInquiry.ReplyTotalQuota = inquiry.ReplyTotalQuota;
                            documentInquiry.ReplyQuotaText = inquiry.ReplyQuotaText;
                            documentInquiry.Price = inquiry.Price;
                            documentInquiry.State = inquiry.State;
                            documentInquiry.ScriptoriumId = inquiry.ScriptoriumId;
                            documentInquiry.Ilm = DocumentConstants.CreateIlm;
                            documentInquiry.DocumentEstateId = inquiry.DocumentEstateId;
                            documentInquiry.DocumentEstateOwnershipDocumentId = inquiry.DocumentEstateOwnershipDocumentId;
                            documentInquiry.DocumentPersonId = inquiry.DocumentPersonId;
                            document.DocumentInquiries.Add(documentInquiry);

                        });

                        relatedData.DocumentEstateQuotaList.ForEach((quata) =>
                        {
                            SSAA.BO.Domain.Entities.DocumentEstateQuotum documentEstateQuotum =
                               new SSAA.BO.Domain.Entities.DocumentEstateQuotum();

                            documentEstateQuotum.Id = quata.Id;
                            documentEstateQuotum.DocumentEstateId = quata.DocumentEstateId;
                            documentEstateQuotum.DocumentPersonId = quata.DocumentPersonId;
                            documentEstateQuotum.DetailQuota = quata.DetailQuota;
                            documentEstateQuotum.TotalQuota = quata.TotalQuota;
                            documentEstateQuotum.QuotaText = quata.QuotaText;
                            documentEstateQuotum.ScriptoriumId = quata.ScriptoriumId;
                            documentEstateQuotum.Ilm = DocumentConstants.CreateIlm;
                            foreach (var documentDocumentEstate in document.DocumentEstates)
                            {
                                if (documentDocumentEstate.Id == documentEstateQuotum.DocumentEstateId)
                                {
                                    documentDocumentEstate.DocumentEstateQuota.Add(documentEstateQuotum);
                                }
                            }

                        });

                        relatedData.DocumentEstateQuotaDetailsList.ForEach((quataDetail) =>
                        {
                            SSAA.BO.Domain.Entities.DocumentEstateQuotaDetail documentEstateQuotaDetail =
                               new SSAA.BO.Domain.Entities.DocumentEstateQuotaDetail();
                            documentEstateQuotaDetail.Id = quataDetail.Id;
                            documentEstateQuotaDetail.DocumentPersonSellerId = quataDetail.DocumentPersonSellerId;
                            documentEstateQuotaDetail.DocumentEstateId = quataDetail.DocumentEstateId;
                            documentEstateQuotaDetail.DocumentEstateOwnershipDocumentId =
                                quataDetail.DocumentEstateOwnershipDocumentId;
                            documentEstateQuotaDetail.OwnershipDetailQuota = quataDetail.OwnershipDetailQuota;
                            documentEstateQuotaDetail.OwnershipTotalQuota = quataDetail.OwnershipTotalQuota;
                            documentEstateQuotaDetail.SellTotalQuota = quataDetail.SellTotalQuota;
                            documentEstateQuotaDetail.QuotaText = quataDetail.QuotaText;
                            documentEstateQuotaDetail.ScriptoriumId = quataDetail.ScriptoriumId;
                            documentEstateQuotaDetail.Ilm = DocumentConstants.CreateIlm;
                            documentEstateQuotaDetail.EstateInquiriesId = quataDetail.EstateInquiriesId;
                            foreach (var documentDocumentEstate in document.DocumentEstates)
                            {
                                if (documentDocumentEstate.Id == documentEstateQuotaDetail.DocumentEstateId)
                                {
                                    documentDocumentEstate.DocumentEstateQuotaDetails.Add(documentEstateQuotaDetail);
                                }
                            }

                        });

                    }
                }
                else
                {
                    errorMessage = documentRelatedData.message[0];
                }
            }
            catch (Exception e)
            {
                errorMessage = "خطا در ارتباط با استعلام";

            }

            return (errorMessage, HasAllarmMessage);
        }

        /// <summary>
        /// The SetDocumentSecretCode
        /// </summary>
        /// <param name="randSeed">The randSeed<see cref="long"/></param>
        /// <returns>The <see cref="string"/></returns>
        public string SetDocumentSecretCode(long randSeed)
        {
            Random rand = new Random((int)randSeed);
            return rand.Next(100000, 999999).ToString();
        }

        /// <summary>
        /// The SetDocNationalNo
        /// </summary>
        /// <param name="registerServiceReq">The registerServiceReq<see cref="Document"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public async Task<bool> SetDocNationalNo(Document registerServiceReq, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(registerServiceReq.NationalNo))
            {
                try
                {

                    registerServiceReq.NationalNo =
                        await GetNationalNo(
                            cancellationToken);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// The GetClassifyNo
        /// </summary>
        /// <param name="registerServiceReq">The registerServiceReq<see cref="Document"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{int?}"/></returns>
        public async Task<int?> GetClassifyNo(Document registerServiceReq, CancellationToken cancellationToken)
        {
            if (registerServiceReq.ClassifyNo == null || registerServiceReq.ClassifyNo == 0)
            {
                var objMax = await documentRepository.GetMaxClassifyNo(registerServiceReq.ScriptoriumId,
                    registerServiceReq.DocumentTypeId, cancellationToken);
                if (objMax == null || objMax.ToString() == "0")
                {
                    //objMax = await scriptoriumSetupRepository.GetDocClassifyNo(registerServiceReq.ScriptoriumId,
                    //registerServiceReq.DocumentTypeId, cancellationToken);
                    if (objMax == null || objMax.ToString() == "0")
                        objMax = 0;

                }

                return objMax;
            }

            return null;
        }

        /// <summary>
        /// The SetDocClassifyNo
        /// </summary>
        /// <param name="registerServiceReq">The registerServiceReq<see cref="Document"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public async Task<bool> SetDocClassifyNo(Document registerServiceReq, CancellationToken cancellationToken)
        {
            return true;
            try
            {
                var result = await GetClassifyNo(registerServiceReq, cancellationToken);
                if (result == null) return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// The HasAgent
        /// </summary>
        /// <param name="mainPerson">The mainPerson<see cref="DocumentPerson"/></param>
        /// <returns>The <see cref="bool"/></returns>
        internal bool HasAgent(DocumentPerson mainPerson)
        {
            foreach (DocumentPerson onePerson in mainPerson.Document.DocumentPeople)
            {
                foreach (DocumentPersonRelated oneDocAgent in onePerson.DocumentPersonRelatedAgentPeople)
                {
                    if (oneDocAgent.AgentTypeId != "10" && // معتمد
                        oneDocAgent.AgentTypeId != "11" && // معرف
                        oneDocAgent.AgentTypeId != "12" && // مترجم
                        oneDocAgent.AgentTypeId != "15" && // شاهد
                        oneDocAgent.AgentTypeId != "13" && // موصي
                        oneDocAgent.AgentTypeId != "9" && // مورث
                        string.Compare(oneDocAgent.MainPersonId.ToString(), mainPerson.Id.ToString()) == 0
                       )
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The ControlSanaForOnePerson
        /// </summary>
        /// <param name="person">The person<see cref="DocumentPerson"/></param>
        /// <returns>The <see cref="string"/></returns>
        internal string ControlSanaForOnePerson(DocumentPerson person)
        {
            if (person.SanaState == YesNo.Yes.GetString())
                return "";

            // شخص زیر 18 سال ثنا ندارد
            if (string.Compare(person.BirthDate,
                    (Int32.Parse(dateTimeService.CurrentPersianDate.Substring(0, 4)) - 18).ToString() + "/" +
                    dateTimeService.CurrentPersianDate.Substring(5)) > 0)
                return "";

            // مرده ثنا ندارد
            if (person.IsAlive == YesNo.No.GetString())
                return "";

            // اتباع بیگانه ثنا ندارد
            if (person.IsIranian == YesNo.No.GetString())
                return "";

            if (person.PersonType == PersonType.NaturalPerson.GetString() && HasAgent(person))
                return "";

            string messageText = "";

            if (person.IsOriginal == YesNo.Yes.GetString() && person.DocumentPersonType != null)
            {
                if (person.DocumentPersonType.IsSanaRequired != YesNo.None.ToString() &&
                    person.SanaState == YesNo.None.GetString())
                    messageText = "لطفاً با استفاده از دکمه تعبیه شده در بخش اطلاعات اشخاص، وضعیت حساب کاربری ثنا " +
                                  person.FullName + " را بررسی کنید.";

                if (person.DocumentPersonType.IsSanaRequired == YesNo.Yes.GetString() &&
                    person.SanaState == YesNo.No.GetString())
                    messageText = person.FullName +
                                  " در سامانه ثنا دارای حساب کاربری نمی باشد.\nلذا لازم است مراحل ثبت نام وی در سامانه ثنا انجام شود.\n";

                if (person.PersonType == PersonType.Legal.GetString() &&
                    person.DocumentPersonType.IsSanaRequired != YesNo.None.GetString() &&
                    person.SanaHasOrganizationChart == YesNo.Yes.GetString() &&
                    (string.IsNullOrEmpty(person.SanaOrganizationCode) ||
                     string.IsNullOrEmpty(person.SanaOrganizationName)))
                    messageText += "لطفاً واحد سازمانی " + person.FullName + " را انتخاب نمایید.";
            }
            else
            {
                foreach (DocumentPersonRelated oneSanaDocAgent in person.DocumentPersonRelatedAgentPeople)
                {
                    if (oneSanaDocAgent.AgentTypeId != "10" && // معتمد
                        oneSanaDocAgent.AgentTypeId != "11" && // معرف
                        oneSanaDocAgent.AgentTypeId != "12" && // مترجم
                        oneSanaDocAgent.AgentTypeId != "15" && // شاهد
                        oneSanaDocAgent.AgentTypeId != "13" && // موصي
                        oneSanaDocAgent.AgentTypeId != "9" // مورث
                       )
                    {
                        messageText += ControlSanaForOnePerson(oneSanaDocAgent.MainPerson);
                    }
                }
            }

            return messageText;
        }

        /// <summary>
        /// The ControlShahkarForOnePerson
        /// </summary>
        /// <param name="person">The person<see cref="DocumentPerson"/></param>
        /// <returns>The <see cref="string"/></returns>
        internal string ControlShahkarForOnePerson(DocumentPerson person)
        {
            if (person.MobileNoState == MobileNoState.GetFromSana.GetString() ||
                person.MobileNoState == MobileNoState.IsOwner.GetString())
                return "";

            // شخص زیر 18 سال مالکیت شماره موبایل ندارد
            if (string.Compare(person.BirthDate,
                    (Int32.Parse(dateTimeService.CurrentPersianDate.Substring(0, 4)) - 18).ToString() + "/" +
                    dateTimeService.CurrentPersianDate.Substring(5)) > 0)
                return "";

            // مرده مالکیت شماره موبایل ندارد
            if (person.IsAlive == YesNo.No.GetString())
                return "";

            // اتباع بیگانه مالکیت شماره موبایل ندارد
            if (person.IsIranian == YesNo.No.GetString())
                return "";

            // اشخاص حقوقی نیازی به کنترل موبایل ندارند
            if (person.PersonType == PersonType.Legal.GetString())
                return "";

            if (person.PersonType == PersonType.NaturalPerson.GetString() && HasAgent(person))
                return "";

            string messageText = "";

            if (person.IsOriginal == YesNo.Yes.GetString() && person.DocumentPersonType != null)
            {
                if (person.DocumentPersonType.IsShahkarRequired != YesNo.None.GetString() &&
                    string.IsNullOrEmpty(person.MobileNo))
                    messageText = "لطفاً شماره موبایل " + person.FullName + " را ثبت کنید.";

                if (person.DocumentPersonType.IsShahkarRequired != YesNo.None.GetString() &&
                    person.MobileNoState == MobileNoState.None.GetString())
                    messageText +=
                        "لطفاً با استفاده از دکمه مربوط به کنترل مالکیت خط، وضعیت مالکیت شماره موبایل مربوط به " +
                        person.FullName + " را استعلام نمایید.";

                if (person.DocumentPersonType.IsShahkarRequired == YesNo.Yes.GetString() &&
                    person.MobileNoState == MobileNoState.IsNotOwner.GetString())
                    messageText += "شماره موبایل ثبت شده برای " + person.FullName + " بنام خود این شخص نیست.\n" +
                                   "لطفاً یک شماره موبایل که بنام خود این شخص باشد، ثبت کنید.";
            }
            else
            {
                foreach (DocumentPersonRelated oneShahkarDocAgent in person.DocumentPersonRelatedAgentPeople)
                {
                    if (oneShahkarDocAgent.AgentTypeId != "10" && // معتمد
                        oneShahkarDocAgent.AgentTypeId != "11" && // معرف
                        oneShahkarDocAgent.AgentTypeId != "12" && // مترجم
                        oneShahkarDocAgent.AgentTypeId != "15" && // شاهد
                        oneShahkarDocAgent.AgentTypeId != "13" && // موصي
                        oneShahkarDocAgent.AgentTypeId != "9" // مورث
                       )
                    {
                        messageText += ControlShahkarForOnePerson(oneShahkarDocAgent.MainPerson);
                    }
                }
            }

            return messageText;
        }

        /// <summary>
        /// The ControlShahkar
        /// </summary>
        /// <param name="regServiceReq">The regServiceReq<see cref="Document"/></param>
        /// <returns>The <see cref="string"/></returns>
        internal string ControlShahkar(Document regServiceReq)
        {
            string errorMessage = "";

            if (!IsCurrentOragnizationPermitted("IsShahkarCheckInAllDocTypesEnable", regServiceReq.ScriptoriumId))
                return "";

            foreach (DocumentPerson oneDocPerson in regServiceReq.DocumentPeople)
            {
                errorMessage += ControlShahkarForOnePerson(oneDocPerson);
            }

            return errorMessage;
        }

        /// <summary>
        /// The ControlSana
        /// </summary>
        /// <param name="regServiceReq">The regServiceReq<see cref="Document"/></param>
        /// <returns>The <see cref="string"/></returns>
        internal string ControlSana(Document regServiceReq)
        {
            if (regServiceReq.DocumentTypeId == "0023" ||
                regServiceReq.DocumentTypeId == "0024" ||
                regServiceReq.DocumentTypeId == "0025" ||
                regServiceReq.DocumentTypeId == "0026" ||
                regServiceReq.DocumentTypeId == "0027" ||
                regServiceReq.DocumentTypeId == "0028" ||
                regServiceReq.DocumentTypeId == "0029" ||
                regServiceReq.DocumentTypeId == "0030" ||
                regServiceReq.DocumentTypeId == "0031" ||
                regServiceReq.DocumentTypeId == "0032" ||
                regServiceReq.DocumentTypeId == "0033" ||
                regServiceReq.DocumentTypeId == "0035" ||
                regServiceReq.DocumentTypeId == "0050")
                return "";

            string errorMessage = "";

            if (!IsCurrentOragnizationPermitted("SanaIsRequied", regServiceReq.ScriptoriumId))
                return "";

            foreach (DocumentPerson oneDocPerson in regServiceReq.DocumentPeople)
            {
                errorMessage += ControlSanaForOnePerson(oneDocPerson);
            }

            return errorMessage;
        }

        /// <summary>
        /// The SetDocNoService
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{ONotaryRegisterServiceReqOutputMessage}"/></returns>
        public async Task<ONotaryRegisterServiceReqOutputMessage> SetDocNoService(/* Guid entityID,*/ CancellationToken cancellationToken)
        {
            string messageText = "";
            //try
            //{
            //document = await this.GetRegisterServiceReqEntity ( entityID, new List<string> { "DocumentCosts", "DocumentInfoConfirm", "DocumentPeople" }, cancellationToken );
            if (document == null)
            {
                messageText = "خطا در اخذ اطلاعات پرونده از سرور مرکزی. لطفاً مجدداً تلاش نمایید.";
                var outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
                return outputError;
            }

            if (document.State != NotaryRegServiceReqState.CostCalculated.GetString() ||
                 ((!document.DocumentCosts.Any()) && (
                   document.DocumentTypeId != "0023" &&
                   document.DocumentTypeId != "0024" &&
                   document.DocumentTypeId != "0025" &&
                   document.DocumentTypeId != "0026" &&
                   document.DocumentTypeId != "0027" &&
                   document.DocumentTypeId != "0028" &&
                   document.DocumentTypeId != "0029" &&
                   document.DocumentTypeId != "0030" &&
                   document.DocumentTypeId != "0031" &&
                   document.DocumentTypeId != "0032" &&
                   document.DocumentTypeId != "0033" &&
                   document.DocumentTypeId != "0035" &&
                   document.DocumentTypeId != "0050" &&
                   document.DocumentTypeId != "0034"
                 )))
            {
                messageText = "لطفاً ابندا هزینه ها را محاسبه نموده، سپس نسبت به اخذ شناسه یکتا اقدام نمایید.";
                var outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
                return outputError;
            }

            await _clientConfiguration.initializeScriptorium(document
                .ScriptoriumId);

            // کنترل های مربوط به ثنا
            IList<string> standardContractCode = new List<string>() { "121", "531", "241", "521", "531" };
            if (standardContractCode.Contains(document.DocumentTypeId))
            {
                

            }
            else
            {
            messageText = ControlSana(document);
            if (!string.IsNullOrEmpty(messageText))
            {
                var outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
                return outputError;
            }

            }

            // کنترل های مربوط به شاهکار
            messageText = ControlShahkar(document);
            if (!string.IsNullOrEmpty(messageText))
            {
                var outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
                return outputError;
            }

            if (document.DocumentTypeId == "007" ||   // اجرائیه
                 document.DocumentTypeId == "0034")    // رفع نقص اجرائیه
            {
                //Criteria crAttachRelatedObject = new Criteria();
                //crAttachRelatedObject.AddEqualTo ( BaseInfoQuery.GAttachmentRelatedObject.RelatedObjectId, input.EntityID );
                //crAttachRelatedObject.AddIsNull ( BaseInfoQuery.GAttachmentRelatedObject.DeleteTime );
                //IList gAttachRelatedObjectList = Rad.CMS.InstanceBuilder.GetEntityListByCriteria<IGAttachmentRelatedObject>(crAttachRelatedObject);
                //if ( gAttachRelatedObjectList.Count == 0 )
                //{
                //    messageText = "لطفاً ابندا تصاویر پیوست اجرائیه را ثبت کنید، سپس نسبت به اخذ شناسه یکتا اقدام نمایید.";
                //    var outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
                //    outputError.Pack ( this.ExecutionContext.SerializationContext );
                //    return outputError;
                //}
            }

            ONotaryRegisterServiceReqOutputMessage output = new ONotaryRegisterServiceReqOutputMessage();
            ApiResult<ONotaryAccessTimeValidationOutputMessage> accessTimeResponse = await _mediator.Send(new IsAccessTimeValidًQuery("1", new string[] { }), cancellationToken);
            if (!accessTimeResponse.IsSuccess || !accessTimeResponse.Data.IsAccess)
            {
                messageText = accessTimeResponse.Data.Message;
                var outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
                return outputError;
            }

            bool result1 = false;
            bool result2 = false;

            result1 = await SetDocNationalNo(document, cancellationToken);
            result2 = await SetDocClassifyNo(document, cancellationToken);

            document.DocumentDate = dateTimeService.CurrentPersianDate;
            document.DocumentInfoConfirm.ConfirmDate = dateTimeService.CurrentPersianDate;
            document.DocumentInfoConfirm.ConfirmTime = dateTimeService.CurrentTime;

            document.IsFinalVerificationVisited = YesNo.Yes.GetString();
            document.DocumentSecretCode = SetDocumentSecretCode(long.Parse(document.RequestNo));

            document.GetDocumentNoDate = dateTimeService.CurrentPersianDate;

            if (document.DocumentDate != "0034")
            {
                document.State = NotaryRegServiceReqState.SetNationalDocumentNo.GetString();
            }

            if (document.DocumentTypeId == "0034")
            {

                document.State = NotaryRegServiceReqState.SetNationalDocumentNo.GetString();
                document.IsCostPaymentConfirmed = YesNo.Yes.GetString();

                document.State = NotaryRegServiceReqState.CostPaid.GetString();
            }

            if (userService.UserApplicationContext.User != null)
                document.DocumentInfoConfirm.ConfirmerNameFamily = userService.UserApplicationContext.User.Name + " " + userService.UserApplicationContext.User.Family;

            MessageInputPacket messageInputPacket = new MessageInputPacket();
            messageInputPacket.MainEntity = document;
            await _messagingCore.CreateSMS(SMSUsageContext.CostOfDocumentForPay, messageInputPacket, false);

            {
                output.Message = (result1 && result2);

                if (
                    !output.Message ||
                    string.IsNullOrWhiteSpace(document.NationalNo) ||
                    string.IsNullOrWhiteSpace(document.DocumentSecretCode) ||
                    string.IsNullOrWhiteSpace(document.DocumentDate) ||
                    string.IsNullOrWhiteSpace(document.DocumentInfoConfirm.ConfirmDate) ||
                    string.IsNullOrWhiteSpace(document.GetDocumentNoDate) ||
                    string.IsNullOrWhiteSpace(document.DocumentInfoConfirm.ConfirmerNameFamily)
                    )
                {
                    output.Message = false;
                    output.MessageText = "اطلاعات اجباری مورد نیاز برای اخذ شناسه یکتا دریافت نشد. لطفاً مجدداً تلاش نمایید.";
                }
                else
                {
                    var req = document;
                    foreach (DocumentPerson theONotaryDocPerson in req.DocumentPeople)

                        if (theONotaryDocPerson.HasGrowthJudgment == YesNo.Yes.GetString())
                        {

                            if (theONotaryDocPerson.GrowthJudgmentDate != null && theONotaryDocPerson.GrowthJudgmentNo != null/* && m.IssuerBranchId != null*/ && theONotaryDocPerson.GrowthLetterDate != null && theONotaryDocPerson.GrowthLetterNo != null && theONotaryDocPerson.GrowthSenderId != null)
                            {
                                if (!string.IsNullOrWhiteSpace(theONotaryDocPerson.Description))
                                    theONotaryDocPerson.Description += "\n";
                                theONotaryDocPerson.Description +=
                                "این سند به موجب حکم رشد به شماره " + theONotaryDocPerson.GrowthJudgmentNo + " تاریخ " + theONotaryDocPerson.GrowthLetterDate + " و مرجع صدور حکم "
                                + theONotaryDocPerson.GrowthIssuer.Name + " برای " + (theONotaryDocPerson.SexType == SexType.Male.GetString() ? "آقای " : "خانم ") +
                                theONotaryDocPerson.FullName() + " تنظیم گردیده است.";
                            }

                        }
                }
            }

            return output;
            //}
            //catch (Exception ex)
            //{
            //    _logger
            //    messageText = "خطا در اجرای سرویس.\n" + ex.ToString();
            //    var outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
            //    return outputError;
            //}
        }

        /// <summary>
        /// The GetRegisterServiceReqEntity
        /// </summary>
        /// <param name="objectID">The objectID<see cref="Guid"/></param>
        /// <param name="details">The details<see cref="List{string}"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        private async Task<Document> GetRegisterServiceReqEntity(Guid objectID, List<string> details, CancellationToken cancellationToken)
        {
            if (objectID == null)
                return null;

            Document oNotaryRegisterServiceReq =
               await documentRepository.GetDocumentById(objectID, details, cancellationToken);

            return oNotaryRegisterServiceReq;
        }

        /// <summary>
        /// The ValidateDocPersonsCollection
        /// </summary>
        /// <param name="personsCollection">The personsCollection<see cref="List{DocumentPerson}"/></param>
        /// <param name="message">The message<see cref="string"/></param>
        /// <returns>The <see cref="Task{(bool, string)}"/></returns>
        public async Task<(bool, string)> ValidateDocPersonsCollection(List<DocumentPerson> personsCollection, string message)
        {
            string messageRef = message;
            if (!personsCollection.Any())
            {
                messageRef = "اشخاص سند مشخص تعریف نشده اند.";
                return (false, messageRef);
            }

            string nonCellNumberPersons = string.Empty;
            //List<IONotaryDocPerson> fingerPrintNeededPersons = null;
            await _clientConfiguration.initializeScriptorium(document.ScriptoriumId);

            foreach (DocumentPerson theOneDocPerson in personsCollection)
            {

                if (
    theOneDocPerson.PersonType == PersonType.NaturalPerson.GetString() &&
    theOneDocPerson.IsIranian == YesNo.Yes.GetString() &&
    theOneDocPerson.IsAlive == YesNo.Yes.GetString()
    )
                {
                    if (
                        string.IsNullOrWhiteSpace(theOneDocPerson.MobileNo) ||
                        !ValidatorsUtility.CheckCellPhoneFormat(theOneDocPerson.MobileNo)
                        )
                    {
                        if (!string.IsNullOrWhiteSpace(nonCellNumberPersons) && nonCellNumberPersons.GetStandardFarsiString().Contains(theOneDocPerson.FullName().GetStandardFarsiString()))
                            continue;

                        nonCellNumberPersons += " - " + theOneDocPerson.FullName() + System.Environment.NewLine;
                        continue;
                    }
                }

                if (!string.IsNullOrEmpty(theOneDocPerson.PostalCode) && !ValidatorsUtility.checkPostalCode(theOneDocPerson.PostalCode))
                {
                    messageRef = "کد پستی وارد شده برای  " + theOneDocPerson.FullName() + " معتبر نیست. ";
                    return (false, messageRef);
                }

                if (theOneDocPerson.DocumentPersonRelatedAgentPeople.Any())
                {
                    foreach (DocumentPersonRelated theOneDocAgent in theOneDocPerson.DocumentPersonRelatedAgentPeople)
                    {
                        if (theOneDocAgent.AgentTypeId == "3")
                            continue;

                        if (string.IsNullOrWhiteSpace(theOneDocAgent.AgentDocumentNo) || string.IsNullOrWhiteSpace(theOneDocAgent.AgentDocumentDate))
                        {
                            messageRef =
                                "لطفاً اشخاص وابسته مربوط به " + theOneDocPerson.FullName() + " را بررسی نموده و از پر بودن فیلدهای اجباری اطمینان حاصل نمایید.";
                            return (false, messageRef);
                        }
                    }
                }

                if (theOneDocPerson.PersonType == PersonType.Legal.GetString())
                {
                    bool isLegalPersonAgentsValid;
                    (isLegalPersonAgentsValid) = this.IsLegalPersonAgentValid(theOneDocPerson, ref messageRef);
                    if (!isLegalPersonAgentsValid)
                        return (false, messageRef);
                }

                List<string>? personAgentsIDsCollection = null;
                string? relationsGraph = null;
                bool isAgentsRelationsGraphSingleSide = PersonsLogicValidator.IsRelationsGraphSingleSide(theOneDocPerson, ref personAgentsIDsCollection, ref relationsGraph, ref messageRef);
                if (!isAgentsRelationsGraphSingleSide)
                    return (false, messageRef);
            }

            if (!string.IsNullOrWhiteSpace(nonCellNumberPersons))
            {
                messageRef = "شماره تلفن همراه اشخاص زیر وارد نشده و یا فاقد اعتبار است:";

                messageRef =
                    messageRef +
                    System.Environment.NewLine +
                    nonCellNumberPersons;

                return (false, messageRef);
            }

            return (true, messageRef);
        }

        /// <summary>
        /// The IsLegalPersonAgentValid
        /// </summary>
        /// <param name="theOneLegalDocPerson">The theOneLegalDocPerson<see cref="DocumentPerson"/></param>
        /// <param name="message">The message<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool IsLegalPersonAgentValid(DocumentPerson theOneLegalDocPerson, ref string message)
        {
            //در اینجا برای استفاده از این تابع، پارامتر سوم را که تعیین کننده شروع گراف است، به گونه ای ست میکنیم که اشخاص حقوقی هم در داخال تابع مورد بررسی قرار گیرند.
            if (FingerprintAquisitionManager.IsFingerprintAquisitionPermitted(theOneLegalDocPerson.Document, theOneLegalDocPerson, false) != FingerprintAquisitionPermission.Mandatory)
                return true;

            if (
                theOneLegalDocPerson.PersonType == PersonType.Legal.GetString() &&
                theOneLegalDocPerson.IsOriginal == YesNo.Yes.GetString()
                )
            {


                foreach (DocumentPerson theOnePersonToCheck4Agent in theOneLegalDocPerson.Document.DocumentPeople)
                {
                    if (!theOnePersonToCheck4Agent.DocumentPersonRelatedAgentPeople.Any())
                        continue;

                    foreach (var theOneAgent in theOnePersonToCheck4Agent.DocumentPersonRelatedAgentPeople)
                    {
                        if (
                            theOneAgent.MainPerson.Id == theOneLegalDocPerson.Id &&
                            theOneAgent.AgentPerson.PersonType == PersonType.NaturalPerson.GetString()
                            )
                            return true;

                        if (
                            theOneAgent.MainPerson.Id == theOneLegalDocPerson.Id &&
                            theOneAgent.AgentPerson.PersonType == PersonType.Legal.GetString()
                            )
                            return this.IsLegalPersonAgentValid(theOneAgent.AgentPerson, ref message);
                    }
                }


                message =
                    "برای " +
                    theOneLegalDocPerson.FullName() +
                    "، هیچ شخص وابسته حقیقی معرفی نشده است. لطفاً حداقل یک شخص وابسته حقیقی برای این شخص مشخص نمایید.";


                return false;
            }

            return true;
        }

        /// <summary>
        /// The initClientConfiguration
        /// </summary>
        /// <param name="scriptoriumId">The scriptoriumId<see cref="string"/></param>
        /// <returns>The <see cref="Task"/></returns>
        internal async Task initClientConfiguration(string scriptoriumId)
        {
            await _clientConfiguration.initializeScriptorium(scriptoriumId);
        }

        /// <summary>
        /// The IsCurrentOragnizationPermitted
        /// </summary>
        /// <param name="configKey">The configKey<see cref="string"/></param>
        /// <param name="scriptoriumId">The scriptoriumId<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        internal bool IsCurrentOragnizationPermitted(string configKey, string scriptoriumId)
        {
            string masterConfigString = null;
            bool EnableExceptExp = false;

            if (configKey == "SanaIsRequied")
            {
                masterConfigString = Settings.SanaIsRequied;
            }
            else
            if (configKey == "IsShahkarCheckInAllDocTypesEnable")
            {
                masterConfigString = Settings.IsShahkarCheckInAllDocTypesEnable;
            }

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

            foreach (var configCouple in configCoupleCollection)
            {
                // شرط پایه
                if (configCouple.Key == "E" && configCouple.Value == "0")
                    return false;

                if (configCouple.Key == "E" && configCouple.Value == "00")
                    EnableExceptExp = true;

                // پاک‌سازی Key در صورت وجود "-"
                var key = configCouple.Key;
                var isDenyingKey = false;
                if (key.Contains("-"))
                {
                    key = key.Replace("-", "");
                    isDenyingKey = true;
                }

                // اگر مقدار null یا * بود
                if (string.IsNullOrEmpty(configCouple.Value) || configCouple.Value == "*")
                    return true;

                // بررسی سطح سازمان جاری
                var currentOrg = _clientConfiguration.currentCMSOrganization;
                if (currentOrg != null)
                {
                    if (currentOrg.Unit != null)
                    {
                        var currentLevelCode = currentOrg.Unit.LevelCode;

                        if (currentLevelCode != null)
                        {
                            var levelPrefix = currentLevelCode.Substring(0, 4);

                            if (isDenyingKey && key == levelPrefix)
                                return false;

                            if (key == "*")
                                return true;

                            if (key != levelPrefix)
                                continue;
                        }

                    }
                }


                // تجزیه subLevels
                var subLevels = configCouple.Value.Contains(",")
                    ? configCouple.Value.Split(',')
                    : new[] { configCouple.Value };

                foreach (var subLevelRaw in subLevels)
                {
                    var subLevel = subLevelRaw;
                    var returnValue = true;

                    if (subLevel.Contains("-"))
                    {
                        subLevel = subLevel.Replace("-", "");
                        returnValue = true;
                    }

                    // بررسی بر اساس scriptoriumId یا کد سازمان
                    if (currentOrg != null)
                    {
                        if (subLevel == scriptoriumId)
                            return returnValue;

                        if (subLevel == "*")
                            return true;
                    }

                    if (currentOrg != null && currentOrg?.Unit != null)
                    {
                        if (subLevel == currentOrg.Unit.Code)
                            return returnValue;

                        if (subLevel == "*")
                            return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// The PrepareSardaftarConfirmInput
        /// </summary>
        /// <param name="saveDocumentCommand">The saveDocumentCommand<see cref="SaveDocumentStandardContractCommand"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{ONotaryRegisterServiceReqInputMessage}"/></returns>
        public async Task<ONotaryRegisterServiceReqInputMessage> PrepareSardaftarConfirmInput(
         SaveDocumentStandardContractCommand saveDocumentCommand, CancellationToken cancellationToken)
        {
            if (saveDocumentCommand == null)
                throw new ArgumentNullException(nameof(saveDocumentCommand));

            var input = new ONotaryRegisterServiceReqInputMessage
            {
                EntityID = saveDocumentCommand.RequestId
            };

            if (saveDocumentCommand.SignedDSUDealSummaryCollection?.Any() == true)
            {
                var signedResult = await _mediator.Send(
                    new GetSignValueListQuery { IdList = saveDocumentCommand.SignedDSUDealSummaryCollection },
                    cancellationToken
                );

                input.SignedDSUDealSummaryCollection = signedResult?.IsSuccess == true
                    ? signedResult.Data?.Select(t => new DSUDealSummarySignPacket
                    {
                        SignB64 = t.SignValue,
                        RawDataB64 = t.RawDataBase64,
                        CertificateB64 = t.Certificate
                    }).ToList()
                    : new List<DSUDealSummarySignPacket>();
            }

            if (saveDocumentCommand.DigitalBookSignatureCollection?.Any() == true)
            {
                var digitalResult = await _mediator.Send(
                    new GetSignValueListQuery { IdList = saveDocumentCommand.DigitalBookSignatureCollection },
                    cancellationToken
                );

                if (digitalResult?.IsSuccess == true && digitalResult.Data != null)
                {
                    input.DigitalBookSignatureCollection = digitalResult.Data
                        .OrderBy(t => t.MainObjectId)
                        .Select(t => t.SignValue)
                        .ToList();

                    input.DigitalBookIds = digitalResult.Data
                        .OrderBy(t => t.MainObjectId)
                        .Select(t => t.MainObjectId)
                        .ToList();
                }
            }

            if (saveDocumentCommand.SignedDocumentId != null)
            {
                var signResult = await _mediator.Send(
                    new GetSignValueQuery { Id = saveDocumentCommand.SignedDocumentId },
                    cancellationToken
                );

                if (signResult?.IsSuccess == true && signResult.Data != null)
                {
                    input.signData = signResult.Data.SignValue;
                    input.signCertificate = signResult.Data.Certificate;
                }
            }

            return input;
        }

        /// <summary>
        /// The PrepareDaftaryarConfirmInput
        /// </summary>
        /// <param name="saveDocumentCommand">The saveDocumentCommand<see cref="SaveDocumentStandardContractCommand"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{ONotaryRegisterServiceReqInputMessage}"/></returns>
        public async Task<ONotaryRegisterServiceReqInputMessage> PrepareDaftaryarConfirmInput(
        SaveDocumentStandardContractCommand saveDocumentCommand, CancellationToken cancellationToken)
        {
            ONotaryRegisterServiceReqInputMessage input =
                new ONotaryRegisterServiceReqInputMessage();
            input.EntityID = saveDocumentCommand.RequestId;
            if (saveDocumentCommand.SignedDocumentId != null)
            {
                var signDataIdInput = new GetSignValueQuery()
                {
                    Id = saveDocumentCommand.SignedDocumentId

                };
                var signDataResult = await _mediator.Send(signDataIdInput, cancellationToken);
                if (signDataResult.IsSuccess)
                {
                    input.signData = signDataResult.Data.SignValue;
                    input.signCertificate = signDataResult.Data.Certificate;
                }
            }

            return input;
        }

        /// <summary>
        /// The SardaftarConfirm
        /// </summary>
        /// <param name="input">The input<see cref="ONotaryRegisterServiceReqInputMessage"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{ONotaryRegisterServiceReqOutputMessage}"/></returns>
        public async Task<ONotaryRegisterServiceReqOutputMessage> SardaftarConfirm(ONotaryRegisterServiceReqInputMessage input, CancellationToken cancellationToken)
        {
            string messageText = "";

            if (input == null && input.EntityID == null && input.Entity == null)
            {
                messageText = "ارتباط با سامانه مرکزی برقرار نشد. لطفاً مجدداً تلاش نمایید.";
                ONotaryRegisterServiceReqOutputMessage outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };

                return outputError;
            }

            try
            {
                IsAccessTimeValidًQuery isAccessTimeValidًQuery = new IsAccessTimeValidًQuery("4", new string[] { });
                var result = await _mediator.Send(isAccessTimeValidًQuery, cancellationToken);

                if (result.IsSuccess && !result.Data.IsAccess)
                {
                    messageText = string.Join(',', result.message);
                    var outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
                    return outputError;
                }

                ONotaryRegisterServiceReqOutputMessage output = new ONotaryRegisterServiceReqOutputMessage();

                Document theCurrentRegisterServiceReqEntity = (input.Entity != null) ? input.Entity : await this.GetRegisterServiceReqEntity(Guid.Parse(input.EntityID), new List<string>() { "DocumentPeople", "DocumentInfoConfirm", "DocumentInfoText", "DocumentEstates" }, cancellationToken);
                if (theCurrentRegisterServiceReqEntity == null)
                {
                    messageText = "خطا در اخذ اطلاعات پرونده از سرور مرکزی. لطفاً مجدداً تلاش نمایید.";
                    ONotaryRegisterServiceReqOutputMessage outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };

                    return outputError;
                }

                _applicationContextService.BeginTransactionAsync(cancellationToken);

                //Added By B.Farahani
                // ICMSOrganization theCMSOrganization = theCurrentRegisterServiceReqEntity.TheScriptorium.TheCMSOrganization;
                //if ( IsCurrentOragnizationPermitted ( "MaxDoc", theCurrentRegisterServiceReqEntity.ScriptoriumId ) )
                ////if (theCurrentRegisterServiceReqEntity.IsRelatedToGovCommission == YesNo.Yes)
                //{
                //    //آیا این نوع سند شامل کمیسیون تقسیم می باشد یا نه
                //    Criteria criteria = new Criteria();
                //    criteria.AddEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryCommissionDocType.ONotaryDocumentTypeId, theCurrentRegisterServiceReqEntity.ONotaryDocumentTypeId );
                //    IONotaryCommissionDocType ONotaryCommissionDocTypeObj = Rad.CMS.InstanceBuilder.GetEntityByCriteria<IONotaryCommissionDocType>(criteria);

                //    if ( ONotaryCommissionDocTypeObj != null )
                //    {
                //        Criteria cr = new Criteria();
                //        cr.AddEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryCommissionNotaryInfo.ScriptoriumId, theCurrentRegisterServiceReqEntity.ScriptoriumId );
                //        cr.AddEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryCommissionNotaryInfo.Year, theCurrentRegisterServiceReqEntity.ReqNo.Substring ( 0, 4 ) );
                //        IONotaryCommissionNotaryInfo CommissionObj = Rad.CMS.InstanceBuilder.GetEntityByCriteria<IONotaryCommissionNotaryInfo>(cr);

                //        //به دست آوردن حق التحریر
                //        Criteria cr2 = new Criteria();
                //        cr2.AddEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryRegServiceCost.ONotaryRegServiceCostTypeId, "5D42EE330BAC4B5A8DB2F1A1EBADE104" );
                //        cr2.AddEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryRegServiceCost.ONotaryRegisterServiceReqId, theCurrentRegisterServiceReqEntity.Id );
                //        IONotaryRegServiceCost Obj = Rad.CMS.InstanceBuilder.GetEntityByCriteria<IONotaryRegServiceCost>(cr2);

                //        if ( CommissionObj != null )
                //        {
                //            if ( Obj != null )
                //            {
                //                CommissionObj.GetPrice += Obj.Price;
                //            }

                //        }
                //    }

                //}

                //Added By B.Farahani

                // apiResult//ConfigurationManager.TypeDefinitions.ClientConfiguration clientConfiguration = new ConfigurationManager.TypeDefinitions.ClientConfiguration(theCurrentRegisterServiceReqEntity.TheScriptorium.TheCMSOrganization);

                await _clientConfiguration.initializeScriptorium(theCurrentRegisterServiceReqEntity.ScriptoriumId);

                //طی جلسه با جناب انجام شعاع مصوب گردید، امضای دفتریار بصورت اختیاری در سامانه دریافت گردد.
                //تاریخ جلسه : 1394/12/09
                //if (
                //    string.Compare(theCurrentRegisterServiceReqEntity.DocDate, clientConfiguration.ENoteBookEnabledDate) >= 0 &&
                //    string.IsNullOrWhiteSpace(theCurrentRegisterServiceReqEntity.DaftaryarDocumentDigitalSign)
                //    )
                //{
                //    Rad.CMS.OjbBridge.TransactionContext.Current.RollBack();
                //    output.MessageText = "سند باید ابتدا توسط دفتریار امضای الکترونیک گردد.";
                //    output.Message = false;
                //    return output;
                //}

                string operationMessage = string.Empty;
                bool isPersonCollectionValid;
                (isPersonCollectionValid, operationMessage) = await IsFingerprintGottenOfAllPersons(theCurrentRegisterServiceReqEntity.DocumentPeople, operationMessage, cancellationToken, input.UnSignedPersonsList);
                if (!isPersonCollectionValid)
                {
                    await _applicationContextService.RollbackTransactionAsync(cancellationToken);
                    // Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();
                    output.Message = false;

                    if (!string.IsNullOrWhiteSpace(operationMessage))
                    {
                        output.MessageText = "تایید نهایی سند لغو گردید." + System.Environment.NewLine + System.Environment.NewLine;
                        output.MessageText += operationMessage;
                    }
                    else
                    {
                        output.MessageText = "تایید نهایی سند لغو گردید." + System.Environment.NewLine + System.Environment.NewLine;
                        output.MessageText += "اثرانگشت اصحاب سند ناقص می باشد.";
                    }

                    return output;
                }

                if (false && DigitalBookUtility.IsDigitalBookGeneratingPermitted(theCurrentRegisterServiceReqEntity, _clientConfiguration.ENoteBookEnabledDate, _clientConfiguration.IsENoteBookAutoClassifyNoEnabled, ref operationMessage) == DigitalBookGeneratingPermissionStatus.Needed)
                {
                    var isExistDocumentFile = await _documentFileRepository.TableNoTracking.CountAsync(t =>
                        t.DocumentId == theCurrentRegisterServiceReqEntity.Id && t.LastFile != null, cancellationToken) > 0;

                    if (!isExistDocumentFile)
                    {
                        await _applicationContextService.RollbackTransactionAsync(cancellationToken);

                        output.MessageText =
                            "دریافت چاپ نسخه پشتیبان سند، قبل از تایید و امضای الکترونیک سند اجباری می باشد.";

                        output.Message = false;
                        return output;
                    }
                }

                //==================================================================================================================================================
                //=========================================Digital_Signature_Verification===========================================================================
                //==================================================================================================================================================
                bool signVerified = true;
                // X509Certificate2 clientSideCertificateObject = null;
                // signVerified = this.VerifyDataSignature ( theCurrentRegisterServiceReqEntity, input.signCertificate, input.sign, ref clientSideCertificateObject, ref operationMessage );
                //output.MainObjectHashedDataToSign = input.signData;
                //==================================================================================================================================================
                if (signVerified)
                {
                    var cer = new X509Certificate2(Convert.FromBase64String(input.signCertificate));
                    theCurrentRegisterServiceReqEntity.DocumentInfoConfirm.SardaftarSignCertificateDn = cer.Subject;
                    //theCurrentRegisterServiceReqEntity.DocumentInfoConfirm.SardaftarSignCertificateDn =
                    //    input.signCertificate;// clientSideCertificateObject.Subject;

                    foreach (DocumentPerson docPerson in theCurrentRegisterServiceReqEntity.DocumentPeople)
                        docPerson.IsSignedDocument = YesNo.Yes.GetString();

                    theCurrentRegisterServiceReqEntity.DocumentInfoConfirm.SardaftarDocumentDigitalSign = input.signData;

                    if (input.UnSignedPersonsList != null && input.UnSignedPersonsList.Any())
                    {
                        theCurrentRegisterServiceReqEntity.DocumentInfoText.DocumentDescription += "اشخاصی که سند را امضاء نکرده اند: ";
                        int idx = 1;
                        foreach (DocumentPerson docPerson in input.UnSignedPersonsList)
                        {
                            docPerson.IsSignedDocument = YesNo.No.GetString();
                            theCurrentRegisterServiceReqEntity.DocumentInfoText.DocumentDescription += docPerson.FullName();
                            if (idx < input.UnSignedPersonsList.Count)
                                theCurrentRegisterServiceReqEntity.DocumentInfoText.DocumentDescription += " - ";
                            ++idx;
                        }
                    }

                    theCurrentRegisterServiceReqEntity.DocumentInfoConfirm.CreateDate =
                        dateTimeService.CurrentPersianDate;//Rad.CMS.BaseInfoContext.Instance.CurrentDateTime;

                    theCurrentRegisterServiceReqEntity.DocumentInfoConfirm.CreateTime =
                        dateTimeService.CurrentTime;//Rad.CMS.BaseInfoContext.Instance.CurrentDateTime;

                    ///- The DigitalBookEntity Is Being Created In ONotaryPKIServices.cs in NotaryOfficeServices.dll
                    ///- The Created DigitalBookEntity Is Being Transfered between client and server until sign process is being done by user seleted certificate.
                    ///- Here The DigitalBookEntity Should be commited;

                    if (DigitalBookUtility.IsDigitalBookGeneratingPermitted(theCurrentRegisterServiceReqEntity, _clientConfiguration.ENoteBookEnabledDate, _clientConfiguration.IsENoteBookAutoClassifyNoEnabled, ref operationMessage) == DigitalBookGeneratingPermissionStatus.Needed)
                    {
                        var electronicBooks = await _documentElectronicBookRepository.GetDocumentElectronicBooks(
                            input.DigitalBookIds, cancellationToken);
                        input.TheCurrentDigitalBookEntityCollection = electronicBooks;
                        if (!input.TheCurrentDigitalBookEntityCollection.Any())
                        {
                            await _applicationContextService.RollbackTransactionAsync(cancellationToken);

                            output.Message = false;
                            output.MessageText = "صفحه دفتر الکترونیک ایجاد نشده است. امکان تایید نهایی بدون درج در دفتر الکترونیک وجود ندارد. لطفاً مجدداً تلاش نمایید.";
                            return output;
                        }

                        //if (theCurrentRegisterServiceReqEntity.TheONotaryDocumentType.Is4RegisterService == YesNo.No)
                        //    theCurrentRegisterServiceReqEntity.ClassifyNo = input.TheCurrentDigitalBookEntity.ClassifyNo;

                        if (!await IsValidClassifyNo(theCurrentRegisterServiceReqEntity.ClassifyNoReserved, theCurrentRegisterServiceReqEntity, cancellationToken))
                        {
                            await _applicationContextService.RollbackTransactionAsync(cancellationToken);
                            await _applicationContextService.BeginTransactionAsync(cancellationToken);

                            _documentElectronicBookRepository.DeleteRangeAsync(electronicBooks, cancellationToken);

                            await _applicationContextService.SaveChangesAsync(cancellationToken);
                            await _applicationContextService.CommitTransactionAsync(cancellationToken);



                        }

                        for (int i = 0; i < input.TheCurrentDigitalBookEntityCollection.Count; i++)
                        {
                            input.TheCurrentDigitalBookEntityCollection[i].ExordiumDigitalSign = input.DigitalBookSignatureCollection[i];


                            if (input.TheCurrentDigitalBookEntityCollection[i].NationalNo ==
                                theCurrentRegisterServiceReqEntity.NationalNo)
                            {
                                theCurrentRegisterServiceReqEntity.WriteInBookDate = input.TheCurrentDigitalBookEntityCollection[i].EnterBookDateTime.Substring(0, 10);

                            }

                        }



                        foreach (var electronicbook in electronicBooks)
                        {
                            var digitalBookSignature = input.DigitalBookSignatureCollection.ElementAt(input.DigitalBookIds.FindIndex(item => item == electronicbook.Id.ToString()));
                            electronicbook.ExordiumDigitalSign = digitalBookSignature;
                            electronicbook.ExordiumConfirmDateTime = dateTimeService.CurrentPersianDateTime;
                            electronicbook.ClassifyNo = electronicbook.ClassifyNoReserved;

                        }
                        await _documentElectronicBookRepository.UpdateRangeAsync(electronicBooks, cancellationToken, false);
                        //theCurrentRegisterServiceReqEntity.WriteInBookDate = input.TheCurrentDigitalBookEntityCollection[0].EnterBookDateTime.Substring(0, 10);
                    }

                    if (input.SignedDSUDealSummaryCollection != null && input.SignedDSUDealSummaryCollection.Any())
                    {
                        string dealSummaryMessages = string.Empty;
                        string finalizationActionMessage = null;
                        bool finalizationActionStatus = false;

                        NotaryGeneratedDealSummary dsuDealSummarySent = new NotaryGeneratedDealSummary();

                        if (theCurrentRegisterServiceReqEntity.DocumentTypeId == "611")
                        {
                            //dsuDealSummarySent = EstateInquiryManager.Common.SeparationDealSummaryValidator.SendSDS(theCurrentRegisterServiceReqEntity, input.SignedDSUDealSummaryCollection, ref dealSummaryMessages);
                            if (CanSendDS())
                            {
                                (dsuDealSummarySent, dealSummaryMessages) = await _separationDealSummaryValidator.SendSDS(theCurrentRegisterServiceReqEntity, input.SignedDSUDealSummaryCollection, cancellationToken, dealSummaryMessages);
                            }
                            else
                            {
                                dsuDealSummarySent = NotaryGeneratedDealSummary.Sent;
                            }

                        }
                        else
                        {
                            (dsuDealSummarySent, dealSummaryMessages) = await _estateInquiryEngine.SendDSUDealSummaries(theCurrentRegisterServiceReqEntity, input.SignedDSUDealSummaryCollection, cancellationToken, dealSummaryMessages);
                        }

                        switch (dsuDealSummarySent)
                        {
                            case NotaryGeneratedDealSummary.NotSent:
                                if (true/*_clientConfiguration.RejectFinalConfirmOnDSUFail*/)
                                {
                                    //Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();
                                    await _applicationContextService.RollbackTransactionAsync(cancellationToken);

                                    output.Message = false;
                                    output.MessageText =
                                        "خطا در ارسال خلاصه معامله! عملیات تایید نهایی لغو گردید!" +
                                        System.Environment.NewLine +
                                        dealSummaryMessages;
                                }
                                //else
                                //{
                                //    finalizationActionStatus = await this.DoFinalizationActionSet ( theCurrentRegisterServiceReqEntity, ref finalizationActionMessage,cancellationToken );

                                //    output.Message = finalizationActionStatus;

                                //    if ( finalizationActionStatus )
                                //    {
                                //        output.MessageText =
                                //        "سند با موفقیت تایید نهایی گردید." +
                                //        System.Environment.NewLine +
                                //        "عملیات ارسال خلاصه معامله خودکار برای این سند، با خطا مواجه شد." +
                                //        System.Environment.NewLine +
                                //        dealSummaryMessages;
                                //    }
                                //    else
                                //    {
                                //        // Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();

                                //        theCurrentRegisterServiceReqEntity.ClassifyNo = null;

                                //        IONotaryDigitalDocumentsBook documentBook = Rad.CMS.InstanceBuilder.GetEntityByCode<IONotaryDigitalDocumentsBook>(theCurrentRegisterServiceReqEntity.NationalNo, NotaryOfficeQuery.ONotaryDigitalDocumentsBook.NationalNo);
                                //        if ( documentBook != null )
                                //            documentBook.MarkForDelete ();

                                //        IONotaryESBDoc esbDoc = Rad.CMS.InstanceBuilder.GetEntityByCode<IONotaryESBDoc>(theCurrentRegisterServiceReqEntity.NationalNo, NotaryOfficeQuery.ONotaryESBDoc.DocumentNationalNo);
                                //        if ( esbDoc != null )
                                //            esbDoc.MarkForDelete ();

                                //        Rad.CMS.OjbBridge.TransactionContext.Current.Commit ();

                                //        output.MessageText = ( string.IsNullOrWhiteSpace ( finalizationActionMessage ) ) ? "خطا در تایید نهایی سند. لطفاً مجدداً تلاش نمایید." : finalizationActionMessage;
                                //    }
                                //}
                                break;

                            case NotaryGeneratedDealSummary.Sent:

                                (finalizationActionStatus, finalizationActionMessage, theCurrentRegisterServiceReqEntity) = await this.DoFinalizationActionSet(theCurrentRegisterServiceReqEntity, finalizationActionMessage, cancellationToken);
                                theCurrentRegisterServiceReqEntity.ClassifyNo = theCurrentRegisterServiceReqEntity.ClassifyNoReserved;
                                theCurrentRegisterServiceReqEntity.WriteInBookDate = theCurrentRegisterServiceReqEntity.WriteInBookDateReserved;
                                await documentRepository.UpdateAsync(theCurrentRegisterServiceReqEntity, cancellationToken, false);
                                output.Message = finalizationActionStatus;

                                if (finalizationActionStatus)
                                {
                                    output.MessageText =
                                        dealSummaryMessages +
                                        System.Environment.NewLine +
                                        "سند با موفقیت تایید نهایی گردید";
                                    await _applicationContextService.SaveChangesAsync(cancellationToken);
                                    await _applicationContextService.CommitTransactionAsync(cancellationToken);
                                }
                                else
                                {
                                    await _applicationContextService.RollbackTransactionAsync(cancellationToken);

                                    //Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();

                                    theCurrentRegisterServiceReqEntity.ClassifyNo = null;

                                    await _applicationContextService.BeginTransactionAsync(cancellationToken);

                                    DocumentElectronicBook documentBook =
                                       await _documentElectronicBookRepository.GetAsync(t =>
                                            t.NationalNo == theCurrentRegisterServiceReqEntity.NationalNo, cancellationToken); // Rad.CMS.InstanceBuilder.GetEntityByCode<IONotaryDigitalDocumentsBook>(theCurrentRegisterServiceReqEntity.NationalNo, NotaryOfficeQuery.ONotaryDigitalDocumentsBook.NationalNo);

                                    if (documentBook != null)
                                        await _documentElectronicBookRepository.DeleteAsync(documentBook, cancellationToken, false);

                                    await _applicationContextService.SaveChangesAsync(cancellationToken);
                                    await _applicationContextService.CommitTransactionAsync(cancellationToken);

                                    //IONotaryESBDoc esbDoc = Rad.CMS.InstanceBuilder.GetEntityByCode<IONotaryESBDoc>(theCurrentRegisterServiceReqEntity.NationalNo, NotaryOfficeQuery.ONotaryESBDoc.DocumentNationalNo);
                                    //if ( esbDoc != null )
                                    //    esbDoc.MarkForDelete ();

                                    // Rad.CMS.OjbBridge.TransactionContext.Current.Commit ();

                                    output.MessageText = (string.IsNullOrWhiteSpace(finalizationActionMessage)) ? "خطا در تایید نهایی سند. لطفاً مجدداً تلاش نمایید." : finalizationActionMessage;
                                }

                                break;

                            case NotaryGeneratedDealSummary.TimedOut:
                                output.ClientConfirmationIsNeeded = true;
                                output.Message = true;
                                output.MessageText = dealSummaryMessages;
                                // output.Pack ( this.ExecutionContext.SerializationContext );
                                return output;
                        }
                    }

                    else
                    {
                        string finalizationActionMessage = null;
                        bool finalizationActionStatus;
                        (finalizationActionStatus, finalizationActionMessage, theCurrentRegisterServiceReqEntity) = await this.DoFinalizationActionSet(theCurrentRegisterServiceReqEntity, finalizationActionMessage, cancellationToken);

                        output.Message = finalizationActionStatus;

                        if (finalizationActionStatus)
                        {
                            theCurrentRegisterServiceReqEntity.SignDate = theCurrentRegisterServiceReqEntity.DocumentDate;

                            output.MessageText = "سند با موفقیت تایید نهایی گردید";
                            await _applicationContextService.SaveChangesAsync(cancellationToken);
                            await _applicationContextService.CommitTransactionAsync(cancellationToken);
                        }
                        else
                        {
                            await _applicationContextService.RollbackTransactionAsync(cancellationToken);

                            // Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();
                            await _applicationContextService.BeginTransactionAsync(cancellationToken);

                            theCurrentRegisterServiceReqEntity.ClassifyNo = null;

                            DocumentElectronicBook documentBook = await _documentElectronicBookRepository.GetAsync(t =>
                                t.NationalNo == theCurrentRegisterServiceReqEntity.NationalNo, cancellationToken);
                            await _documentElectronicBookRepository.DeleteAsync(documentBook, cancellationToken, false);
                            //if ( documentBook != null )
                            //    documentBook.MarkForDelete ();

                            //IONotaryESBDoc esbDoc = Rad.CMS.InstanceBuilder.GetEntityByCode<IONotaryESBDoc>(theCurrentRegisterServiceReqEntity.NationalNo, NotaryOfficeQuery.ONotaryESBDoc.DocumentNationalNo);
                            //if ( esbDoc != null )
                            //    esbDoc.MarkForDelete ();

                            //Rad.CMS.OjbBridge.TransactionContext.Current.Commit ();
                            await _applicationContextService.SaveChangesAsync(cancellationToken);
                            await _applicationContextService.CommitTransactionAsync(cancellationToken);
                            output.MessageText = (string.IsNullOrWhiteSpace(finalizationActionMessage)) ? "خطا در تایید نهایی سند. لطفاً مجدداً تلاش نمایید." : finalizationActionMessage;
                        }
                    }
                }

                else
                {
                    //output.ClientRawTextSignIsNeeded = true;
                    //output.MessageText = "پرونده جاری مجدداً امضای الکترونیک خواهد شد. آیا ادامه می دهید؟";

                    // Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();
                    await _applicationContextService.RollbackTransactionAsync(cancellationToken);

                    output.Message = false;

                    if (!string.IsNullOrWhiteSpace(operationMessage))
                        output.MessageText = operationMessage;
                    else
                        output.MessageText = "امضای سند دریافت شده در سمت سرور با محتوای سند تطابق نداشته و فاقد اعتبار است.";

                }

                // output.Pack ( this.ExecutionContext.SerializationContext );
                return output;
            }
            catch (Exception ex)
            {
                await _applicationContextService.RollbackTransactionAsync(cancellationToken);

                messageText = "خطا در اجرای سرویس.\n" + ex.ToString();
                var outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
                // outputError.Pack ( this.ExecutionContext.SerializationContext );
                return outputError;
            }
        }

        /// <summary>
        /// The DaftaryarConfirm
        /// </summary>
        /// <param name="input">The input<see cref="ONotaryRegisterServiceReqInputMessage"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{ONotaryRegisterServiceReqOutputMessage}"/></returns>
        public async Task<ONotaryRegisterServiceReqOutputMessage> DaftaryarConfirm(ONotaryRegisterServiceReqInputMessage input, CancellationToken cancellationToken)
        {
            string messageText = "";

            if (input == null || input.EntityID == null)
            {
                messageText = "ارتباط با سامانه مرکزی برقرار نشد. لطفاً مجدداً تلاش نمایید.";
                ONotaryRegisterServiceReqOutputMessage outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };

                return outputError;
            }

            try
            {
                IsAccessTimeValidًQuery isAccessTimeValidًQuery = new IsAccessTimeValidًQuery("4", new string[] { });
                var result = await _mediator.Send(isAccessTimeValidًQuery, cancellationToken);

                if (result.IsSuccess && !result.Data.IsAccess)
                {
                    messageText = string.Join(',', result.message);
                    var outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
                    return outputError;
                }

                ONotaryRegisterServiceReqOutputMessage output = new ONotaryRegisterServiceReqOutputMessage() { ReSignIsNeeded = false, Message = false };

                Document theCurrentNotaryRegisterServiceReqEntity = (input.Entity != null) ? input.Entity : await this.GetRegisterServiceReqEntity(Guid.Parse(input.EntityID), new List<string>() { "DocumentPeople", "DocumentInfoConfirm", "DocumentInfoText", "DocumentEstates" }, cancellationToken);


                if (theCurrentNotaryRegisterServiceReqEntity == null)
                {
                    messageText = "خطا در اخذ اطلاعات پرونده از سرور مرکزی. لطفاً مجدداً تلاش نمایید.";
                    ONotaryRegisterServiceReqOutputMessage outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };

                    return outputError;
                }

                string operationMessage = string.Empty;
                bool isPersonCollectionValid;
                (isPersonCollectionValid, operationMessage) = await IsFingerprintGottenOfAllPersons(theCurrentNotaryRegisterServiceReqEntity.DocumentPeople, operationMessage, cancellationToken, input.UnSignedPersonsList);

                if (!isPersonCollectionValid)
                {
                    //Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();
                    output.Message = false;

                    if (!string.IsNullOrWhiteSpace(operationMessage))
                        output.MessageText = operationMessage;
                    else
                        output.MessageText = "اثرانگشت اصحاب سند ناقص می باشد.";

                    return output;
                }

                await _clientConfiguration.initializeScriptorium(theCurrentNotaryRegisterServiceReqEntity.ScriptoriumId);
                string message = null;
                if (false && DigitalBookUtility.IsDigitalBookGeneratingPermitted(theCurrentNotaryRegisterServiceReqEntity, _clientConfiguration.ENoteBookEnabledDate, _clientConfiguration.IsENoteBookAutoClassifyNoEnabled, ref message) == DigitalBookGeneratingPermissionStatus.Needed)
                {
                    var isExistDocumentFile = await _documentFileRepository.TableNoTracking.CountAsync(t =>
                        t.DocumentId == theCurrentNotaryRegisterServiceReqEntity.Id && t.LastFile != null, cancellationToken) > 0;
                    if (!isExistDocumentFile)
                    {
                        output.MessageText =
                            "دریافت چاپ نسخه پشتیبان سند، قبل از امضای الکترونیک سند اجباری می باشد.";

                        output.Message = false;
                        return output;
                    }
                }
                //==================================================================================================================================================
                //=========================================Digital_Signature_Verification===========================================================================
                //==================================================================================================================================================
                bool signVerified = true;
                await _applicationContextService.BeginTransactionAsync(cancellationToken);

                //signVerified = this.VerifyDataSignature ( theCurrentNotaryRegisterServiceReqEntity, input.signCertificate, splittedSign, ref clientSideCertificateObject, ref operationMessage, NotaryOfficeImplementation.CustomClassesDefinitions.Enumerations.TokenValidationCallBackActionType.DaftaryarConfirm, input.SignCounter, clientData );
                //==================================================================================================================================================
                if (signVerified)
                {
                    var cer = new X509Certificate2(Convert.FromBase64String(input.signCertificate));

                    theCurrentNotaryRegisterServiceReqEntity.DocumentInfoConfirm.DaftaryarSignCertificateDn = cer.Subject;
                    // theCurrentNotaryRegisterServiceReqEntity.DocumentInfoConfirm.DaftaryarSignCertificateDn = input.signCertificate;
                    theCurrentNotaryRegisterServiceReqEntity.DocumentInfoConfirm.DaftaryarDocumentDigitalSign = input.signData;
                    theCurrentNotaryRegisterServiceReqEntity.DocumentInfoConfirm.DaftaryarAccessId = userService.UserApplicationContext.BranchAccess.BranchAccessId;
                    theCurrentNotaryRegisterServiceReqEntity.DocumentInfoConfirm.DaftaryarConfirmDate =
                        dateTimeService.CurrentPersianDate;
                    theCurrentNotaryRegisterServiceReqEntity.DocumentInfoConfirm.DaftaryarConfirmTime = dateTimeService.CurrentTime;
                    theCurrentNotaryRegisterServiceReqEntity.DocumentInfoConfirm.DaftaryarNameFamily = userService.UserApplicationContext.User.Name + " " + userService.UserApplicationContext.User.Family;

                    theCurrentNotaryRegisterServiceReqEntity.DocumentInfoConfirm.CreateDate = dateTimeService.CurrentPersianDate;
                    theCurrentNotaryRegisterServiceReqEntity.DocumentInfoConfirm.CreateTime = dateTimeService.CurrentTime;
                    theCurrentNotaryRegisterServiceReqEntity.DocumentInfoConfirm.CreateTime = dateTimeService.CurrentTime;
                    await documentRepository.UpdateAsync(theCurrentNotaryRegisterServiceReqEntity, cancellationToken, false);
                    await _applicationContextService.SaveChangesAsync(cancellationToken);
                    await _applicationContextService.CommitTransactionAsync(cancellationToken);

                    //TransactionContext.Current.Commit ();
                    output.ReSignIsNeeded = false;
                    output.Message = true;
                    output.MessageText = "امضای الکترونیکی دفتریار با موفقیت انجام گردید.";

                }

                else
                {
                    ///در صورتی که امضای سند باشکست مواجه گردد باید سند را دوباره امضا نماییم در این صورت امضا درست انجام خواهد شد.
                    if (input.SignCounter <= 1)
                    {
                        output.SignCounter = ++input.SignCounter;
                        output.ReSignIsNeeded = true;
                        output.Message = true;

                        //RDev.DataServices.OJB.FormSignInfoDataGraphProvider formSignInfoDataGraphProvider = new RDev.DataServices.OJB.FormSignInfoDataGraphProvider();
                        //RDev.Framework.DigitalSignature.SignInfoDataGraph signInfoDataGraph = formSignInfoDataGraphProvider.GetFormSignInfoDataGraph(Rad.CMS.BaseInfoContext.Instance.CurrentForm.ObjectId);
                        //output.SignInfoDataGraph = signInfoDataGraph;

                        //output.Pack ( this.ExecutionContext.SerializationContext );
                        return output;
                    }
                    await _applicationContextService.RollbackTransactionAsync(cancellationToken);
                    // Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();
                    output.Message = false;
                    output.ReSignIsNeeded = false;

                    if (!string.IsNullOrWhiteSpace(operationMessage))
                        output.MessageText = operationMessage;
                    else
                        output.MessageText = "امضای سند دریافت شده در سمت سرور با محتوای سند تطابق نداشته و فاقد اعتبار است.";
                }

                // output.Pack ( this.ExecutionContext.SerializationContext );
                return output;
            }
            catch (Exception ex)
            {

                messageText = "خطا در اجرای سرویس.\n" + ex.ToString();
                var outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
                // outputError.Pack ( this.ExecutionContext.SerializationContext );
                return outputError;
            }
        }

        /// <summary>
        /// The DoFinalizationActionSet
        /// </summary>
        /// <param name="theCurrentReq">The theCurrentReq<see cref="Document"/></param>
        /// <param name="message">The message<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{(bool,string, Document)}"/></returns>
        private async Task<(bool, string, Document)> DoFinalizationActionSet(Document theCurrentReq, string message, CancellationToken cancellationToken)
        {
            string messageRef = message;
            try
            {
                //Mr. Fallah Will Call The Desiring ESB-Library Operations Here.
                // ESBManager.ESBResult esbResult = ESBManager.ESBNotaryOfficeDocument.CreatePackage(theCurrentReq);

                //bool esbProccessSucceeded = esbResult.Successfull;
                //if (!esbProccessSucceeded)
                //{
                //    message = "خطا در ایجاد محتویات خلاصه شده سند جاری.";
                //    message += Environment.NewLine + esbResult.ErrorMessage;
                //    Rad.CMS.OjbBridge.TransactionContext.Current.RollBack();
                //    return false;
                //}
                theCurrentReq.SignDate = theCurrentReq.DocumentDate;
                theCurrentReq.State = NotaryRegServiceReqState.Finalized.GetString();
                theCurrentReq.DocumentInfoConfirm.CreateDate = dateTimeService.CurrentPersianDate;
                theCurrentReq.DocumentInfoConfirm.CreateTime = dateTimeService.CurrentTime;
                theCurrentReq.DocumentInfoConfirm.SardaftarConfirmDate = dateTimeService.CurrentPersianDate;
                theCurrentReq.DocumentInfoConfirm.SardaftarConfirmTime = dateTimeService.CurrentTime;
                theCurrentReq.DocumentInfoConfirm.ConfirmDate = theCurrentReq.DocumentInfoConfirm.SardaftarConfirmDate;
                theCurrentReq.DocumentInfoConfirm.ConfirmTime = theCurrentReq.DocumentInfoConfirm.SardaftarConfirmTime;
                theCurrentReq.DocumentInfoConfirm.SardaftarAccessId = userService.UserApplicationContext.BranchAccess.BranchAccessId;
                theCurrentReq.DocumentInfoConfirm.SardaftarNameFamily = userService.UserApplicationContext.User.Name + " " + userService.UserApplicationContext.User.Family;

                {
                    MessageInputPacket messageInputPacket = new MessageInputPacket();
                    messageInputPacket.MainEntity = theCurrentReq;

                    await _messagingCore.CreateSMS(SMSUsageContext.FinalVerification, messageInputPacket, false);
                    //===================================Azl_EstefaConfirmationSMS============================================================
                    await this.SendAzl_EstefaConfirmationSMS(theCurrentReq, cancellationToken);

                    DocumentSemaphore semaphore =
                        await _documentSemaphoreRepository.GetAsync(t => t.ScriptoriumId == theCurrentReq.ScriptoriumId, cancellationToken); // Rad.CMS.InstanceBuilder.GetEntityByCode<IONotarySemaphor>(theCurrentReq.ScriptoriumId, NotaryOfficeQuery.ONotarySemaphor.ScriptoriumId);
                    if (semaphore == null)
                    {
                        messageRef = "قفل موقت جهت ادامه فرایند یافت نشد. لطفاً با پشتیبانی تماس حاصل نمایید.";
                        await _applicationContextService.RollbackTransactionAsync(cancellationToken);
                        // Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();
                        return (false, messageRef, theCurrentReq);
                    }
                    else
                    {
                        await _documentSemaphoreRepository.DeleteAsync(semaphore, cancellationToken, false); // Rad.CMS.InstanceBuilder.GetEntityByCode<IONotarySemaphor>(theCurrentReq.ScriptoriumId, NotaryOfficeQuery.ONotarySemaphor.ScriptoriumId);

                        // semaphore.MarkForDelete ();
                    }


                    // Rad.CMS.OjbBridge.TransactionContext.Current.Commit ();

                    if (theCurrentReq.DocumentTypeId == "007" || theCurrentReq.DocumentTypeId == "0034")
                    {
                        theCurrentReq.DocumentInfoText.DocumentDescription = theCurrentReq.DocumentInfoText.DocumentDescription + "\n";

                        //Rad.CMS.OjbBridge.TransactionContext.Current.Commit ();
                    }

                    return (true, messageRef, theCurrentReq);
                }
            }
            catch (Exception ex)
            {
                messageRef = "خطا در ثبت وضعیت تایید نهایی سند. لطفاً مجدداً تلاش نمایید.";

                //Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();
                return (false, messageRef, theCurrentReq);
            }
        }

        /// <summary>
        /// The SendAzl_EstefaConfirmationSMS
        /// </summary>
        /// <param name="theCurrentNotaryRegisterServiceReqEntity">The theCurrentNotaryRegisterServiceReqEntity<see cref="Document"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        private async Task SendAzl_EstefaConfirmationSMS(Document theCurrentNotaryRegisterServiceReqEntity, CancellationToken cancellationToken)
        {
            try
            {
                if ((theCurrentNotaryRegisterServiceReqEntity.DocumentTypeId == "0022" || //استعفا
                    theCurrentNotaryRegisterServiceReqEntity.DocumentTypeId == "006") &&
                    theCurrentNotaryRegisterServiceReqEntity.RelatedDocumentIsInSsar == YesNo.Yes.GetString()
                    )
                {
                    List<SMSRecipient> smsRecipientPacketCollection = await validatorGateway.CollectSMSData(theCurrentNotaryRegisterServiceReqEntity.RelatedDocumentNo);

                    if (smsRecipientPacketCollection != null && smsRecipientPacketCollection.Any())
                    {
                        MessageInputPacket messageInputPacket = new MessageInputPacket();

                        switch (theCurrentNotaryRegisterServiceReqEntity.DocumentTypeId)
                        {
                            case "0022": //استعفا
                                foreach (SMSRecipient theOneSMSRecipient in smsRecipientPacketCollection)
                                {
                                    if (theOneSMSRecipient.PersonTypeCode == "59" || //موکل و اولین موکل
                                        theOneSMSRecipient.PersonTypeCode == "17")
                                    {
                                        messageInputPacket = new MessageInputPacket();
                                        messageInputPacket.RecipientPhoneNo = theOneSMSRecipient.RecipientMobileNo;
                                        messageInputPacket.RecipientFullName = theOneSMSRecipient.RecipientFullName;
                                        messageInputPacket.RecipientDocPersonTypeCode = theOneSMSRecipient.PersonTypeCode;
                                        messageInputPacket.MainEntityObjectID = theCurrentNotaryRegisterServiceReqEntity.Id.ToString();
                                        messageInputPacket.MainEntity = theCurrentNotaryRegisterServiceReqEntity;

                                        _messagingCore.CreateSMS(SMSUsageContext.AzlOrEstefa, messageInputPacket, false, false);
                                    }
                                }
                                break;
                            case "006": //عزل
                                foreach (SMSRecipient theOneSMSRecipient in smsRecipientPacketCollection)
                                {
                                    if (theOneSMSRecipient.PersonTypeCode == "16")
                                    {
                                        messageInputPacket = new MessageInputPacket();
                                        messageInputPacket.RecipientPhoneNo = theOneSMSRecipient.RecipientMobileNo;
                                        messageInputPacket.RecipientFullName = theOneSMSRecipient.RecipientFullName;
                                        messageInputPacket.RecipientDocPersonTypeCode = theOneSMSRecipient.PersonTypeCode;
                                        messageInputPacket.MainEntityObjectID = theCurrentNotaryRegisterServiceReqEntity.Id.ToString();
                                        messageInputPacket.MainEntity = theCurrentNotaryRegisterServiceReqEntity;

                                        _messagingCore.CreateSMS(SMSUsageContext.AzlOrEstefa, messageInputPacket, false, false);
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// The ValidateDocPersonsCollection
        /// </summary>
        /// <param name="personsCollection">The personsCollection<see cref="ICollection{DocumentPerson}"/></param>
        /// <param name="message">The message<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool ValidateDocPersonsCollection(ICollection<DocumentPerson> personsCollection, ref string message)
        {
            if (!personsCollection.Any())
            {
                message = "اشخاص سند مشخص تعریف نشده اند.";
                return false;
            }

            string nonCellNumberPersons = string.Empty;
            //List<IONotaryDocPerson> fingerPrintNeededPersons = null;

            foreach (DocumentPerson theOneDocPerson in personsCollection)
            {
                if (_clientConfiguration == null)
                    _clientConfiguration.initializeScriptorium(theOneDocPerson.ScriptoriumId);
                //  _clientConfiguration = new ConfigurationManager.TypeDefinitions.ClientConfiguration ( theOneDocPerson.TheONotaryRegisterServiceReq.TheScriptorium.TheCMSOrganization );

                if (
    theOneDocPerson.PersonType == PersonType.NaturalPerson.GetString() &&
    theOneDocPerson.IsIranian == YesNo.Yes.GetString() &&
    theOneDocPerson.IsAlive != YesNo.No.GetString()
    )
                {
                    if (
                        string.IsNullOrWhiteSpace(theOneDocPerson.MobileNo) ||
                        !ValidatorsUtility.CheckCellPhoneFormat(theOneDocPerson.MobileNo)
                        )
                    {
                        if (!string.IsNullOrWhiteSpace(nonCellNumberPersons) && nonCellNumberPersons.GetStandardFarsiString().Contains(theOneDocPerson.FullName().GetStandardFarsiString()))
                            continue;

                        nonCellNumberPersons += " - " + theOneDocPerson.FullName + System.Environment.NewLine;
                        continue;
                    }
                }

                if (!string.IsNullOrEmpty(theOneDocPerson.PostalCode) && !ValidatorsUtility.checkPostalCode(theOneDocPerson.PostalCode))
                {
                    message = "کد پستی وارد شده برای  " + theOneDocPerson.FullName + " معتبر نیست. ";
                    return false;
                }

                if (!theOneDocPerson.DocumentPersonRelatedAgentPeople.Any())
                {
                    foreach (DocumentPersonRelated theOneDocAgent in theOneDocPerson.DocumentPersonRelatedAgentPeople)
                    {
                        if (theOneDocAgent.AgentTypeId == "3")
                            continue;

                        if (string.IsNullOrWhiteSpace(theOneDocAgent.AgentDocumentNo) || string.IsNullOrWhiteSpace(theOneDocAgent.AgentDocumentDate))
                        {
                            message =
                                "لطفاً اشخاص وابسته مربوط به " + theOneDocPerson.FullName + " را بررسی نموده و از پر بودن فیلدهای اجباری اطمینان حاصل نمایید.";
                            return false;
                        }
                    }
                }

                if (theOneDocPerson.PersonType == PersonType.Legal.GetString())
                {
                    bool isLegalPersonAgentsValid = this.IsLegalPersonAgentValid(theOneDocPerson, ref message);
                    if (!isLegalPersonAgentsValid)
                        return false;
                }

                List<string> personAgentsIDsCollection = null;
                string relationsGraph = null;
                bool isAgentsRelationsGraphSingleSide = PersonsLogicValidator.IsRelationsGraphSingleSide(theOneDocPerson, ref personAgentsIDsCollection, ref relationsGraph, ref message);
                if (!isAgentsRelationsGraphSingleSide)
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(nonCellNumberPersons))
            {
                message = "شماره تلفن همراه اشخاص زیر وارد نشده و یا فاقد اعتبار است:";

                message =
                    message +
                    System.Environment.NewLine +
                    nonCellNumberPersons;

                return false;
            }

            return true;
        }

        /// <summary>
        /// The IsFingerprintGottenOfAllPersons
        /// </summary>
        /// <param name="personsCollection">The personsCollection<see cref="ICollection{DocumentPerson}"/></param>
        /// <param name="message">The message<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <param name="unsignedPersons">The unsignedPersons<see cref="List{DocumentPerson}"/></param>
        /// <returns>The <see cref="Task{(bool,string)}"/></returns>
        public async Task<(bool, string)> IsFingerprintGottenOfAllPersons(ICollection<DocumentPerson> personsCollection, string message, CancellationToken cancellationToken, List<DocumentPerson> unsignedPersons = null)
        {
            string messageRef = message;
            string docuemntId = null;
            if (!personsCollection.Any())
            {
                messageRef = "اشخاص سند مشخص تعریف نشده اند.";
                return (false, messageRef);
            }

            //Rad.NotaryOffice.Service.IONotaryRegisterServiceReq theCurrentReq = ((IONotaryDocPerson)personsCollection[0]).TheONotaryRegisterServiceReq;

            List<DocumentPerson> fingerPrintNeededPersons = null;
            string docDate = null;

            foreach (DocumentPerson theOneDocPerson in personsCollection)
            {
                if (_clientConfiguration._currentOrg == null)
                    _clientConfiguration.initializeScriptorium(theOneDocPerson.ScriptoriumId);//new ConfigurationManager.TypeDefinitions.ClientConfiguration ( theOneDocPerson.TheONotaryRegisterServiceReq.TheScriptorium.TheCMSOrganization );
                docuemntId = theOneDocPerson.DocumentId.ToString();
                bool personHasSignedDocument = true;
                if (unsignedPersons != null && unsignedPersons.Any())
                    foreach (DocumentPerson theOneUnsignedPerson in unsignedPersons)
                    {
                        if (
                            theOneDocPerson.Id == theOneUnsignedPerson.Id ||
                            (theOneDocPerson.NationalNo == theOneUnsignedPerson.NationalNo && theOneDocPerson.FullName().GetStandardFarsiString() == theOneUnsignedPerson.FullName().GetStandardFarsiString())
                            )
                        {
                            personHasSignedDocument = false;
                            break;
                        }
                    }

                if (!personHasSignedDocument)
                    continue;

                if (_clientConfiguration.IsFingerprintEnabled)
                {
                    bool isForced = this.IsFingerPrintForced(theOneDocPerson);
                    if (!isForced)
                        continue;

                    if (fingerPrintNeededPersons == null)
                        fingerPrintNeededPersons = new List<DocumentPerson>();

                    if (!fingerPrintNeededPersons.Any() || !fingerPrintNeededPersons.Contains(theOneDocPerson))
                        fingerPrintNeededPersons.Add(theOneDocPerson);
                }

                if (string.IsNullOrWhiteSpace(docDate))
                    docDate = theOneDocPerson.Document.DocumentDate; //تا قبل از تاریخ 23 دی 94 ، بجای این تاریخ (شناسه یکتا) از تاریخ تشکیل پرونده استفاده می شد.
            }

            if (!_clientConfiguration.IsFingerprintEnabled)
                return (true, messageRef);

            if (
                _clientConfiguration.IsFingerprintEnabled &&
                string.Compare(docDate, _clientConfiguration.FingerprintEnabledDate) >= 0 &&
                fingerPrintNeededPersons.Any()
                )
            {
                List<string> forcedFingerprintPersonIDs = new List<string>();
                foreach (DocumentPerson theOneDocPerson in fingerPrintNeededPersons)
                    forcedFingerprintPersonIDs.Add(theOneDocPerson.Id.ToString());
                var theFingerprintsCollection = await _personFingerprintRepository.GetAllAsync(p => p.UseCaseMainObjectId == docuemntId && personsCollection.Select(p => p.Id.ToString()).ToList().Contains(p.UseCasePersonObjectId),
                    cancellationToken);

                if (!theFingerprintsCollection.Any())
                {
                    messageRef = "اثرانگشت اشخاص در سند یافت نشد.";
                    return (false, messageRef);
                }

                messageRef = "";
                foreach (DocumentPerson theOneDocPersonToCheck in fingerPrintNeededPersons)
                {
                    bool fingerPrintEntityExists = false;
                    bool fingerprintFeaturesExists = false;
                    bool fingerprintDescriptionExists = false;

                    if (_clientConfiguration.ISMOCEnabledForScriptorium && _clientConfiguration.IsForcedMOC)
                    {
                        if (theOneDocPersonToCheck.MocState == MocState.None.GetString() ||
                        theOneDocPersonToCheck.MocState == MocState.NotDone.GetString() ||
                        theOneDocPersonToCheck.MocState == MocState.FingerprintNotMatched.GetString() ||
                        theOneDocPersonToCheck.MocState == MocState.PinNotMatched.GetString())
                        {
                            if (theOneDocPersonToCheck.HasSmartCard == YesNo.Yes.GetString())
                            {
                                messageRef = "اثر انگشت " + theOneDocPersonToCheck.FullName() + " با اثر انگشت مندرج در کارت هوشمند ملی وی تطابق ندارد.";
                                return (false, messageRef);
                            }
                        }
                    }

                    foreach (PersonFingerprint theOneFingerPrintEntity in theFingerprintsCollection)
                    {
                        DocumentPerson theEquivalantFingerPrintPerson = await _documentPersonRepository.GetAsync(p => p.Id == theOneFingerPrintEntity.UseCasePersonObjectId.ToGuid(), cancellationToken);

                        if (
                            theOneDocPersonToCheck.Id == Guid.Parse(theOneFingerPrintEntity.UseCasePersonObjectId) ||
                            theEquivalantFingerPrintPerson.NationalNo == theOneDocPersonToCheck.NationalNo
                            )
                        {
                            fingerPrintEntityExists = true;

                            if (!Equals(theOneFingerPrintEntity.FingerprintFeatures, null))
                                fingerprintFeaturesExists = true;

                            if (!string.IsNullOrEmpty(theOneFingerPrintEntity.Description))
                                fingerprintDescriptionExists = true;

                            break;
                        }
                    }

                    string digitalBookValidatorMessage = null;

                    if (DigitalBookUtility.IsDigitalBookGeneratingPermitted(theOneDocPersonToCheck.Document, _clientConfiguration.ENoteBookEnabledDate, _clientConfiguration.IsENoteBookAutoClassifyNoEnabled, ref digitalBookValidatorMessage) == DigitalBookGeneratingPermissionStatus.Needed)
                    {
                        if (fingerprintFeaturesExists)
                            continue;

                        bool isPersonDefinedAsReliable = this.IsReliablePerson(theOneDocPersonToCheck);

                        if (isPersonDefinedAsReliable)
                        {
                            messageRef +=
                                System.Environment.NewLine +
                                " - برای " + theOneDocPersonToCheck.FullName() + " با شماره ملی " + theOneDocPersonToCheck.NationalNo + " و شماره ردیف " + theOneDocPersonToCheck.RowNo +
                                "اثرانگشت ثبت نشده است. درج اثرانگشت برای فرد معتمد اجباری است.";
                        }
                        else
                        {
                            var theAgentsCollection = await this.GetReliablePersonsCollection(theOneDocPersonToCheck, cancellationToken);
                            if (theAgentsCollection.Any())
                                continue;
                            else
                            {
                                messageRef +=
                                System.Environment.NewLine +
                                " - برای " + theOneDocPersonToCheck.FullName() + " با شماره ملی " + theOneDocPersonToCheck.NationalNo + " و شماره ردیف " + theOneDocPersonToCheck.RowNo +
                                " اثرانگشت ثبت نشده است. در صورت عدم امکان اخذ اثرانگشت بایستی مطابق تبصره 2 ماده 4 شیوه نامه بهره برداری از دفتر الکترونیک اقدام شود.";
                            }
                        }
                    }

                    else
                    {
                        if (fingerprintFeaturesExists)
                            continue;

                        if (!fingerPrintEntityExists)
                            messageRef += " - برای " + theOneDocPersonToCheck.FullName + " با شماره ملی " + theOneDocPersonToCheck.NationalNo + " و شماره ردیف " + theOneDocPersonToCheck.RowNo + " هیچ اثرانگشت و یا توضیحی در مورد علت عدم اخذ اثرانگشت ثبت نشده است. " + System.Environment.NewLine;
                        else
                        {
                            if (fingerprintDescriptionExists)
                                continue;
                            else
                            {
                                messageRef +=
                                System.Environment.NewLine +
                                " - برای " + theOneDocPersonToCheck.FullName + " با شماره ملی " + theOneDocPersonToCheck.NationalNo + " و شماره ردیف " + theOneDocPersonToCheck.RowNo +
                                " هیچ توضیحی در مورد علت عدم اخذ اثرانگشت ثبت نشده است.";
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(messageRef))
                {
                    messageRef =
                        "لطفاً به موارد زیر در خصوص اثرانگشت ها توجه فرمایید : " +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        messageRef;

                    return (false, messageRef);
                }

            }

            return (true, messageRef);
        }

        /// <summary>
        /// The IsFingerPrintForced
        /// </summary>
        /// <param name="theSelectedDocPerson">The theSelectedDocPerson<see cref="DocumentPerson"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool IsFingerPrintForced(DocumentPerson theSelectedDocPerson)
        {
            //return false;

            if (theSelectedDocPerson.Document.State != NotaryRegServiceReqState.FinalPrinted.GetString())
                return false;

            if (theSelectedDocPerson.PersonType == PersonType.Legal.GetString())
                return false;

            //if (theSelectedDocPerson.TheONotaryRegServicePersonType == null)
            //    return true;

            FingerprintAquisitionPermission permission = FingerprintAquisitionManager.IsFingerprintAquisitionPermitted(theSelectedDocPerson.Document, theSelectedDocPerson);

            switch (permission)
            {
                case FingerprintAquisitionPermission.Forbidden:
                    return false;
                case FingerprintAquisitionPermission.Mandatory:
                    return true;
                case FingerprintAquisitionPermission.Optional:

                    if (theSelectedDocPerson.WouldSignedDocument == YesNo.No.GetString())
                        return false;
                    else
                        return true;
            }

            return true;
        }

        /// <summary>
        /// The IsReliablePerson
        /// </summary>
        /// <param name="theCurrentPerson">The theCurrentPerson<see cref="DocumentPerson"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool IsReliablePerson(DocumentPerson theCurrentPerson)
        {
            if (!theCurrentPerson.DocumentPersonRelatedAgentPeople.Any() || theCurrentPerson.IsRelated != YesNo.Yes.GetString())
                return false;

            foreach (DocumentPersonRelated theOneDocAgent in theCurrentPerson.DocumentPersonRelatedAgentPeople)
            {
                if (theOneDocAgent.AgentTypeId == "10")
                    return true;
            }

            return false;
        }

        /// <summary>
        /// The GetReliablePersonsCollection
        /// </summary>
        /// <param name="theOneDocPersonToCheck">The theOneDocPersonToCheck<see cref="DocumentPerson"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{List{DocumentPersonRelated}}"/></returns>
        private async Task<List<DocumentPersonRelated>> GetReliablePersonsCollection(DocumentPerson theOneDocPersonToCheck, CancellationToken cancellationToken)
        {

            var theAgentsCollection = await _documentPersonRelatedRepository.GetAllAsync(
                t => t.MainPersonId == theOneDocPersonToCheck.Id && t.AgentTypeId == "10" && t.ReliablePersonReasonId ==
                    NotaryNeedToReliableReason.WithoutFingerprint.GetString(), cancellationToken);

            return theAgentsCollection;
        }

        /// <summary>
        /// The AutoGenerateRestrictedOwnershipDocs
        /// </summary>
        /// <param name="RequestNo">The RequestNo<see cref="string"/></param>
        /// <param name="ONotaryRegisterServiceReqId">The ONotaryRegisterServiceReqId<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{(bool, string)}"/></returns>
        public async Task<(bool, string)> AutoGenerateRestrictedOwnershipDocs(string RequestNo, string ONotaryRegisterServiceReqId, CancellationToken cancellationToken)
        {

            string messages = null;
            bool isGenerated;
            (isGenerated, messages) = await _personOwnershipDocsInheritorCore.InheritDeterministicOwnershipDocs(RequestNo, ONotaryRegisterServiceReqId, cancellationToken, messages);

            return (isGenerated, messages);
        }

        /// <summary>
        /// The CanSendDS
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool CanSendDS()
        {

            /// ICMSOrganization org = Rad.CMS.InstanceBuilder.GetEntityById<ICMSOrganization>(Rad.CMS.BaseInfoContext.Instance.CurrentCMSOrganization.ObjectId);
            if (userService.UserApplicationContext.ScriptoriumInformation.Id != null)
            {
                if (IsCurrentOragnizationPermitted("CanSendDS", userService.UserApplicationContext.ScriptoriumInformation.Id))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The TryPrepareSeparationDealSummary
        /// </summary>
        /// <param name="input">The input<see cref="ONotaryRegServiceInquiryInputMessage"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{ONotaryRegServiceInquiryOutputMessage}"/></returns>
        public async Task<ONotaryRegServiceInquiryOutputMessage> TryPrepareSeparationDealSummary(ONotaryRegServiceInquiryInputMessage input, CancellationToken cancellationToken)
        {
            ONotaryRegServiceInquiryOutputMessage response = new ONotaryRegServiceInquiryOutputMessage();
            string dsuDealSummaryMessage = string.Empty;
            string SDSCount = string.Empty;
            bool isSentRecently = await _separationDealSummaryValidator.IsDSUSent4ThisRequest(input.registerServiceReq, cancellationToken);

            if (isSentRecently)
            {

                response.SignPacketCollection = null;
                response.VerificationMessage = null;
                response.MainObjectHashedData = input.MainObjectHashedData;
                return response;
            }

            List<DSUDealSummarySignPacket> pack = new List<DSUDealSummarySignPacket>();

            string ResultMessage = string.Empty;
            if (!_separationDealSummaryValidator.IsPermitedToGenerateSDS(input.registerServiceReq, ref ResultMessage))
            {
                response.SignPacketCollection = null;
                response.VerificationMessage = "تایید نهایی سند لغو گردید . امکان ساخت بسته های خلاصه معامله وجود ندارد  " + Environment.NewLine + ResultMessage;

                return response;
            }

            (DSUDealSummarySignPacket theOneSDS, dsuDealSummaryMessage, SDSCount) = await _separationDealSummaryValidator.PrepareSeparationPacksForSign(input.registerServiceReq.Id.ToString(),
                cancellationToken, dsuDealSummaryMessage, SDSCount);

            if (theOneSDS != null)
            {
                pack.Add(theOneSDS);
                response.SignPacketCollection = pack;
                response.SeparationDSCount = SDSCount;
                response.VerificationMessage = dsuDealSummaryMessage;
                response.MainObjectHashedData = input.MainObjectHashedData;
            }
            else
            {
                response.SignPacketCollection = null;
                response.SeparationDSCount = SDSCount;
                response.VerificationMessage = dsuDealSummaryMessage;
                response.MainObjectHashedData = input.MainObjectHashedData;

            }

            return response;
        }

        /// <summary>
        /// The PrepareDocumentConfirmation
        /// </summary>
        /// <param name="request">The request<see cref="PrepareDocumentConfirmationRequest"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{PrepareDocumentConfirmation}"/></returns>
        public async Task<PrepareDocumentConfirmation> PrepareDocumentConfirmation(PrepareDocumentConfirmationRequest request, CancellationToken cancellationToken)
        {
            PrepareDocumentConfirmation response = new PrepareDocumentConfirmation { ServiceExecutionSucceeded = true };
            ///=========================================
            ///****************SERVER*******************
            ///0. Confirmation Requirements Validation
            ///1. Validate Semaphore
            ///2. Insert Semaphore
            ///3. Validate Token And Certificate
            ///4. Generate DigitalBook
            ///5. Assign ( ClassifyNo , WriteInBook ) To Current RegisterServiceReq
            ///6. Get SignGraph & SignText
            ///7. Generate DSU-Packets
            ///
            ///****************CLIENT*******************
            ///8. Sign DSU, DigitalBook, CurrentRequest
            ///
            ///****************SERVER*******************
            ///9. Validate Sign
            ///10.Send DSU
            ///11.Final Confirm
            ///12.Validate Semaphore Existence
            ///13.Commit
            ///=========================================
            ///
            Document theCurrentReq = null;

            if (request.TheCurrentReq == null)
                theCurrentReq = await this.GetRegisterServiceReqEntity(Guid.Parse(request.CurrentReqObjectID),
                    ["DocumentType", "DocumentPeople", "DocumentInquiries"], cancellationToken);
            else
                theCurrentReq = request.TheCurrentReq;

            if (request.UnSignedPersonsListIds != null)
                request.UnSignedPersonsList = theCurrentReq?.DocumentPeople.Where(p => request.UnSignedPersonsListIds.Contains(p.Id.ToString())).ToList();

            await _clientConfiguration.initializeScriptorium(theCurrentReq?.ScriptoriumId);

            ONotaryRegisterServiceReqOutputMessage output = new ONotaryRegisterServiceReqOutputMessage();
            ApiResult<ONotaryAccessTimeValidationOutputMessage> accessTimeResponse = await _mediator.Send(new IsAccessTimeValidًQuery("1", new string[] { }), cancellationToken);
            if (!accessTimeResponse.IsSuccess || !accessTimeResponse.Data.IsAccess)
            {
                var messageText = accessTimeResponse.Data.Message;
                response.ServiceExecutionMessage = messageText;
                response.ServiceExecutionSucceeded = false;

                return response;
            }

            string operationMessage = string.Empty;
            bool isPersonCollectionValid;
            (isPersonCollectionValid, operationMessage) = await IsFingerprintGottenOfAllPersons(theCurrentReq.DocumentPeople, operationMessage, cancellationToken, request.UnSignedPersonsList);

            if (!isPersonCollectionValid)
            {
                // Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();

                if (!string.IsNullOrWhiteSpace(operationMessage))
                {
                    response.ServiceExecutionMessage = "تایید نهایی سند امکان پذیر نمی باشد. لطفاً به موارد زیر توجه نمایید." + System.Environment.NewLine + System.Environment.NewLine;
                    response.ServiceExecutionMessage += operationMessage;
                }
                else
                {
                    response.ServiceExecutionMessage = "تایید نهایی سند امکان پذیر نمی باشد. لطفاً به موارد زیر توجه نمایید." + System.Environment.NewLine + System.Environment.NewLine;
                    response.ServiceExecutionMessage += "اثرانگشت اصحاب سند ناقص می باشد.";
                }

                response.ServiceExecutionSucceeded = false;
                return response;
            }

            if (false && DigitalBookUtility.IsDigitalBookGeneratingPermitted(theCurrentReq, _clientConfiguration.ENoteBookEnabledDate, _clientConfiguration.IsENoteBookAutoClassifyNoEnabled, ref operationMessage) == DigitalBookGeneratingPermissionStatus.Needed)
            {
                var isExistDocumentFile = await _documentFileRepository.TableNoTracking.CountAsync(t =>
                    t.DocumentId == theCurrentReq.Id && t.LastFile != null, cancellationToken) > 0;

                if (!isExistDocumentFile)
                {
                    response.ServiceExecutionMessage =
                        "دریافت چاپ نسخه پشتیبان سند، قبل از تایید و امضای الکترونیک سند اجباری می باشد.";

                    response.ServiceExecutionSucceeded = false;
                    return response;
                }

            }

            await _applicationContextService.BeginTransactionAsync(cancellationToken);

            DocumentSemaphore theSemaphore =
                await _documentSemaphoreRepository.GetAsync(t => t.ScriptoriumId == theCurrentReq.ScriptoriumId, cancellationToken);
            if (theSemaphore != null)
            {
                var currentDateTimeNullable = dateTimeService.CurrentPersianDateTime.ToDateTime();
                if (currentDateTimeNullable == null)
                {
                    throw new InvalidOperationException("Current Persian date time is null.");
                }

                DateTime currentTime = currentDateTimeNullable.Value;
                var semaphoreDateTimeString = (theSemaphore.LastChangeDate + "-" + theSemaphore.LastChangeTime);
                var semaphoreDateTimeNullable = semaphoreDateTimeString.ToDateTimeString();
                if (semaphoreDateTimeNullable == null)
                {
                    throw new InvalidOperationException("Semaphore datetime is null or invalid.");
                }

                DateTime semaphoreDateTime = semaphoreDateTimeNullable.Value;
                TimeSpan timeDistance = (currentTime - semaphoreDateTime);

                if (timeDistance.TotalSeconds < _clientConfiguration.SemaphoreLifeTime)
                {
                    response.ServiceExecutionMessage =
                        "تایید نهایی سند با شکست مواجه شد." +
                        System.Environment.NewLine +
                        "لطفاً از اتصال صحیح توکن و همچنین ارتباط VPN و کیفیت خط اینترنتی خود اطمنیان حاصل نموده و پس از گذشت {0} ، مجدداً نسبت به تایید نهایی سند اقدام نمایید." +
                        System.Environment.NewLine +
                        "شایان ذکر است تایید نهایی همزمان بیش از یک سند امکان پذیر نمی باشد.";

                    const double epsilon = 0.0001;
                    string availableTimeDuration = null;
                    if (Math.Abs(timeDistance.TotalMinutes) > epsilon)
                        availableTimeDuration += timeDistance.TotalMinutes + " دقیقه";
                    else
                        availableTimeDuration += timeDistance.TotalSeconds + " ثانیه";

                    response.ServiceExecutionMessage = string.Format(response.ServiceExecutionMessage, availableTimeDuration);

                    response.ServiceExecutionSucceeded = false;

                    return response;
                }
                else
                {
                    await _documentSemaphoreRepository.DeleteAsync(theSemaphore, cancellationToken, false);
                    //theSemaphore.MarkForDelete ();
                }
            }

            theSemaphore = new DocumentSemaphore
            {
                Id = Guid.NewGuid(),
                ScriptoriumId = theCurrentReq.ScriptoriumId,
                LastChangeDate = dateTimeService.CurrentPersianDate,
                LastChangeTime = dateTimeService.CurrentTime,
                OriginalDocumentData = " ",
                NewDocumentData = " ",
                DocumentElectronicBookData = " ",
                OperationType = " ",
                RecordDate = dateTimeService.CurrentDateTime

                // RecordDate = (DateTime)dateTimeService.CurrentPersianDate.ToDateTime(),


            };
            await _documentSemaphoreRepository.AddAsync(theSemaphore, cancellationToken, false);



            try
            {
                await _applicationContextService.SaveChangesAsync(cancellationToken);
                await _applicationContextService.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }



            //TransactionContext.Current.Commit ();

            //ONotaryPKIServices certificateValidationService = new ONotaryPKIServices(this.ExecutionContext);

            //Sabt.AppServices.Definitions.PkiServicesInputMessage validateCertitificateInputMessage = new Sabt.AppServices.Definitions.PkiServicesInputMessage();
            //validateCertitificateInputMessage.CurrentActionType = request.CurrentActionType;
            //validateCertitificateInputMessage.CurrentReqID = request.CurrentReqObjectID;
            //validateCertitificateInputMessage.IsCurrentReq4RegisterService = ( theCurrentReq.TheONotaryDocumentType.Is4RegisterService == YesNo.Yes ) ? true : false;
            //validateCertitificateInputMessage.LogInChallengeSign = request.CertificateSign;

            //Sabt.AppServices.Definitions.PkiServicesOutputMessage certificateValidationResponse = certificateValidationService.ValidateCertificate(validateCertitificateInputMessage);
            //if ( !certificateValidationResponse.IsCertificateAuthorized )
            //{
            //    response.ServiceExecutionSucceeded = false;
            //    response.ServiceExecutionMessage = certificateValidationResponse.AuthorizationMessage;

            //    return response;
            //}

            await _applicationContextService.BeginTransactionAsync(cancellationToken);

            string digitalBookGeneratingMessage = null;
            if (DigitalBookUtility.IsDigitalBookGeneratingPermitted(theCurrentReq, _clientConfiguration.ENoteBookEnabledDate, _clientConfiguration.IsENoteBookAutoClassifyNoEnabled, ref operationMessage) == DigitalBookGeneratingPermissionStatus.Needed)
            {
                int documentChainIndex = 0;
                bool isExistsElectronicbook;
                (List<DocumentElectronicBook> generatedDigitalDocumentsBookEntityCollection, documentChainIndex, digitalBookGeneratingMessage, isExistsElectronicbook) = await _eNoteBookServerController.ProvideDigitalBookEntityWithReservedClassifyNo(theCurrentReq, cancellationToken, documentChainIndex, digitalBookGeneratingMessage);

                if (!generatedDigitalDocumentsBookEntityCollection.Any())
                {
                    if (string.IsNullOrWhiteSpace(digitalBookGeneratingMessage))
                        digitalBookGeneratingMessage = "خطا در تولید محتوای صفحه دفتر الکترونیک! لطفاً با پشتیبانی سامانه تماس حاصل نموده و شماره پرونده جاری را جهت بررسی اعلام نمایید.";
                    await _applicationContextService.RollbackTransactionAsync(cancellationToken);

                    // Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();

                    response.ServiceExecutionMessage = digitalBookGeneratingMessage;
                    response.ServiceExecutionSucceeded = false;

                    return response;
                }
                else
                {
                    List<string> digitalBookRecordHashedDataCollection = _signProvider.ProvideENoteBookHashedData(generatedDigitalDocumentsBookEntityCollection);
                    if (!digitalBookRecordHashedDataCollection.Any() || digitalBookRecordHashedDataCollection.Count < generatedDigitalDocumentsBookEntityCollection.Count)
                    {
                        digitalBookGeneratingMessage = "خطا در ساخت محتوای امضای الکترونیک برای درج در دفتر الکترونیک سردفتر. لطفاً شماره پرونده جاری را جهت بررسی به پشتیبانی سامانه اعلام نمایید.";
                        await _applicationContextService.RollbackTransactionAsync(cancellationToken);
                        // Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();

                        response.ServiceExecutionSucceeded = false;
                        response.ServiceExecutionMessage = digitalBookGeneratingMessage;

                        return response;
                    }
                    else
                    {
                        response.TheGeneratedDigitalBookEntityCollection = new List<DocumentElectronicBook>();
                        response.TheGeneratedDigitalBookEntityCollection.AddRange(generatedDigitalDocumentsBookEntityCollection);

                        response.TheDigitalBookHashedDataCollection = new List<string>();
                        response.TheDigitalBookHashedDataCollection.AddRange(digitalBookRecordHashedDataCollection);

                        foreach (DocumentElectronicBook theOneDigitalBook in response.TheGeneratedDigitalBookEntityCollection)
                        {
                            if (
                                theCurrentReq.ClassifyNo != null &&
                                !string.IsNullOrEmpty(theCurrentReq.WriteInBookDate) &&
                                theCurrentReq.DocumentType.IsSupportive == YesNo.Yes.GetString()
                                )
                                break;

                            if (
                                theCurrentReq.NationalNo == theOneDigitalBook.NationalNo &&
                                theCurrentReq.ScriptoriumId == theOneDigitalBook.ScriptoriumId &&
                                theCurrentReq.DocumentTypeId == theOneDigitalBook.DocumentTypeId
                                )
                            {
                                theCurrentReq.ClassifyNoReserved = theOneDigitalBook.ClassifyNoReserved;
                                theCurrentReq.WriteInBookDateReserved = theOneDigitalBook.EnterBookDateTime.Substring(0, 10);

                                break;
                            }
                        }

                        if (string.IsNullOrEmpty(theCurrentReq.WriteInBookDateReserved))
                            theCurrentReq.WriteInBookDateReserved = dateTimeService.CurrentPersianDate;

                        if (theCurrentReq.ClassifyNoReserved == null)
                        {
                            string sss2 = theCurrentReq.ClassifyNo.To_String();
                            response.ServiceExecutionSucceeded = false;
                            response.ServiceExecutionMessage = "خطا در تخصیص شماره ترتیب به سند جاری. لطفاً با پشتیبانی تماس حاصل نمایید.";
                            await _applicationContextService.RollbackTransactionAsync(cancellationToken);

                            return response;
                        }

                    }
                }
                if (isExistsElectronicbook)
                    await _documentElectronicBookRepository.UpdateRangeAsync(generatedDigitalDocumentsBookEntityCollection,
                        cancellationToken, false);
                else
                    await _documentElectronicBookRepository.AddRangeAsync(generatedDigitalDocumentsBookEntityCollection,
                          cancellationToken, false);
                await documentRepository.UpdateAsync(theCurrentReq, cancellationToken, false);
            }

            //RDev.DataServices.OJB.FormSignInfoDataGraphProvider formSignInfoDataGraphProvider = new RDev.DataServices.OJB.FormSignInfoDataGraphProvider();
            //RDev.Framework.DigitalSignature.SignInfoDataGraph signInfoDataGraph = formSignInfoDataGraphProvider.GetFormSignInfoDataGraph(Rad.CMS.BaseInfoContext.Instance.CurrentForm.ObjectId);
            //response.SignInfoDataGraph = signInfoDataGraph;

            //string rawDataSignText = RDev.Framework.DigitalSignature.DataGraphSignHelper.GetDataSignText(theCurrentReq, signInfoDataGraph);
            //if ( !string.IsNullOrWhiteSpace ( rawDataSignText ) )
            //{
            //    List<byte> byteList = new List<byte>();
            //    byteList.AddRange ( System.Text.Encoding.UTF8.GetBytes ( rawDataSignText ) );

            //    byte[] rawDataByte = byteList.ToArray();
            //    response.TheCurrentReqSignText = Convert.ToBase64String ( rawDataByte );
            //}

            if (theCurrentReq.DocumentTypeId != "611")
            {
                IList<string> standardContractCode = new List<string>() { "121","531","241","521","531" };

                ONotaryRegServiceInquiryInputMessage dsuInputMessage = new ONotaryRegServiceInquiryInputMessage();
                dsuInputMessage.registerServiceReq = theCurrentReq;
                dsuInputMessage.UnSignedPersonsList = request.UnSignedPersonsList;
                if (standardContractCode.Contains( theCurrentReq.DocumentTypeId))
                {
                    response.DSUSignPacketCollection = new List<DSUDealSummarySignPacket>();
                }
                else
                {
                    var dsuOutputMessage = await TryPrepareDSUDealSummary(dsuInputMessage, cancellationToken);
                    if (!string.IsNullOrWhiteSpace(dsuOutputMessage.VerificationMessage))
                    {
                        response.ServiceExecutionSucceeded = false;
                        response.ServiceExecutionMessage = dsuOutputMessage.VerificationMessage;
                        return response;
                    }

                    response.DSUSignPacketCollection = dsuOutputMessage.SignPacketCollection;

                }
            }
            else
            {
                ONotaryRegServiceInquiryInputMessage dsuInputMessage = new ONotaryRegServiceInquiryInputMessage();
                dsuInputMessage.registerServiceReq = theCurrentReq;
                dsuInputMessage.UnSignedPersonsList = request.UnSignedPersonsList;

                var dsuOutputMessage = await TryPrepareSeparationDealSummary(dsuInputMessage, cancellationToken);
                if (CanSendDS())
                {
                    if (!string.IsNullOrWhiteSpace(dsuOutputMessage.VerificationMessage))
                    {
                        response.ServiceExecutionSucceeded = false;
                        response.ServiceExecutionMessage = dsuOutputMessage.VerificationMessage;
                        return response;
                    }

                    response.DSUSignPacketCollection = dsuOutputMessage.SignPacketCollection;
                    response.SDSCount = dsuOutputMessage.SeparationDSCount;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(dsuOutputMessage.VerificationMessage))
                    {
                        //response.ServiceExecutionSucceeded = true;
                        response.ServiceExecutionMessage = "";// dsuOutputMessage.VerificationMessage;
                        response.TheCurrentReq = theCurrentReq;
                        response.ConfirmationTimeLimit = _clientConfiguration.SemaphoreLifeTime / 60;
                        response.ServiceExecutionSucceeded = true;
                        return response;
                    }

                    response.DSUSignPacketCollection = dsuOutputMessage.SignPacketCollection;
                    response.SDSCount = dsuOutputMessage.SeparationDSCount;
                }

            }

            response.TheCurrentReq = theCurrentReq;
            response.ConfirmationTimeLimit = _clientConfiguration.SemaphoreLifeTime / 60;
            response.ServiceExecutionSucceeded = true;
            try
            {

                await _applicationContextService.SaveChangesAsync(cancellationToken);
                await _applicationContextService.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }

            return response;
        }

        /// <summary>
        /// The PrepareDataryarConfirmation
        /// </summary>
        /// <param name="request">The request<see cref="PrepareDocumentConfirmationRequest"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{PrepareDocumentConfirmation}"/></returns>
        public async Task<PrepareDocumentConfirmation> PrepareDataryarConfirmation(PrepareDocumentConfirmationRequest request, CancellationToken cancellationToken)
        {
            PrepareDocumentConfirmation response = new PrepareDocumentConfirmation { ServiceExecutionSucceeded = true };
            ///=========================================
            ///****************SERVER*******************
            ///0. Confirmation Requirements Validation
            ///1. Validate Semaphore
            ///2. Insert Semaphore
            ///3. Validate Token And Certificate
            ///4. Generate DigitalBook
            ///5. Assign ( ClassifyNo , WriteInBook ) To Current RegisterServiceReq
            ///6. Get SignGraph & SignText
            ///7. Generate DSU-Packets
            ///
            ///****************CLIENT*******************
            ///8. Sign DSU, DigitalBook, CurrentRequest
            ///
            ///****************SERVER*******************
            ///9. Validate Sign
            ///10.Send DSU
            ///11.Final Confirm
            ///12.Validate Semaphore Existence
            ///13.Commit
            ///=========================================
            ///
            Document theCurrentReq = null;

            if (request.TheCurrentReq == null)
                theCurrentReq = await this.GetRegisterServiceReqEntity(Guid.Parse(request.CurrentReqObjectID),
                    ["DocumentType", "DocumentPeople", "DocumentInquiries"], cancellationToken);
            else
                theCurrentReq = request.TheCurrentReq;

            request.UnSignedPersonsList = theCurrentReq?.DocumentPeople.Where(p => request.UnSignedPersonsListIds.Contains(p.Id.ToString())).ToList();

            await _clientConfiguration.initializeScriptorium(theCurrentReq?.ScriptoriumId);

            ONotaryRegisterServiceReqOutputMessage output = new ONotaryRegisterServiceReqOutputMessage();
            ApiResult<ONotaryAccessTimeValidationOutputMessage> accessTimeResponse = await _mediator.Send(new IsAccessTimeValidًQuery("1", new string[] { }), cancellationToken);
            if (!accessTimeResponse.IsSuccess || !accessTimeResponse.Data.IsAccess)
            {
                var messageText = accessTimeResponse.Data.Message;
                response.ServiceExecutionMessage = messageText;
                response.ServiceExecutionSucceeded = false;

                return response;
            }

            string operationMessage = string.Empty;
            bool isPersonCollectionValid;
            (isPersonCollectionValid, operationMessage) = await IsFingerprintGottenOfAllPersons(theCurrentReq.DocumentPeople, operationMessage, cancellationToken, request.UnSignedPersonsList);

            if (!isPersonCollectionValid)
            {
                // Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();

                if (!string.IsNullOrWhiteSpace(operationMessage))
                {
                    response.ServiceExecutionMessage = "تایید نهایی سند امکان پذیر نمی باشد. لطفاً به موارد زیر توجه نمایید." + System.Environment.NewLine + System.Environment.NewLine;
                    response.ServiceExecutionMessage += operationMessage;
                }
                else
                {
                    response.ServiceExecutionMessage = "تایید نهایی سند امکان پذیر نمی باشد. لطفاً به موارد زیر توجه نمایید." + System.Environment.NewLine + System.Environment.NewLine;
                    response.ServiceExecutionMessage += "اثرانگشت اصحاب سند ناقص می باشد.";
                }

                response.ServiceExecutionSucceeded = false;
                return response;
            }

            if (false && DigitalBookUtility.IsDigitalBookGeneratingPermitted(theCurrentReq, _clientConfiguration.ENoteBookEnabledDate, _clientConfiguration.IsENoteBookAutoClassifyNoEnabled, ref operationMessage) == DigitalBookGeneratingPermissionStatus.Needed)
            {
                var isExistDocumentFile = await _documentFileRepository.TableNoTracking.CountAsync(t =>
                    t.DocumentId == theCurrentReq.Id && t.LastFile != null, cancellationToken) > 0;

                if (!isExistDocumentFile)
                {
                    response.ServiceExecutionMessage =
                        "دریافت چاپ نسخه پشتیبان سند، قبل از تایید و امضای الکترونیک سند اجباری می باشد.";

                    response.ServiceExecutionSucceeded = false;
                }
            }

            await _applicationContextService.BeginTransactionAsync(cancellationToken);

            DocumentSemaphore theSemaphore =
                await _documentSemaphoreRepository.GetAsync(t => t.ScriptoriumId == theCurrentReq.ScriptoriumId, cancellationToken);
            if (theSemaphore != null)
            {
                DateTime currentTime = (DateTime)dateTimeService.CurrentPersianDateTime.ToDateTime();
                TimeSpan timeDistance = currentTime - (DateTime)(theSemaphore.LastChangeDate + "-" + theSemaphore.LastChangeTime).ToDateTime();

                if (timeDistance.TotalSeconds < _clientConfiguration.SemaphoreLifeTime)
                {
                    response.ServiceExecutionMessage =
                        "تایید نهایی سند با شکست مواجه شد." +
                        System.Environment.NewLine +
                        "لطفاً از اتصال صحیح توکن و همچنین ارتباط VPN و کیفیت خط اینترنتی خود اطمنیان حاصل نموده و پس از گذشت {0} ، مجدداً نسبت به تایید نهایی سند اقدام نمایید." +
                        System.Environment.NewLine +
                        "شایان ذکر است تایید نهایی همزمان بیش از یک سند امکان پذیر نمی باشد.";

                    string availableTimeDuration = null;
                    if (timeDistance.TotalMinutes != 0)
                        availableTimeDuration += timeDistance.TotalMinutes + " دقیقه";
                    else
                        availableTimeDuration += timeDistance.TotalSeconds + " ثانیه";

                    response.ServiceExecutionMessage = string.Format(response.ServiceExecutionMessage, availableTimeDuration);

                    response.ServiceExecutionSucceeded = false;

                    return response;
                }
                else
                {
                    await _documentSemaphoreRepository.DeleteAsync(theSemaphore, cancellationToken);
                    //theSemaphore.MarkForDelete ();
                }
            }

            theSemaphore = new DocumentSemaphore
            {
                ScriptoriumId = theCurrentReq.ScriptoriumId,
                LastChangeDate = dateTimeService.CurrentPersianDate,
                LastChangeTime = dateTimeService.CurrentTime
            };
            await _applicationContextService.SaveChangesAsync(cancellationToken);
            await _applicationContextService.CommitTransactionAsync(cancellationToken);
            //TransactionContext.Current.Commit ();

            //ONotaryPKIServices certificateValidationService = new ONotaryPKIServices(this.ExecutionContext);

            //Sabt.AppServices.Definitions.PkiServicesInputMessage validateCertitificateInputMessage = new Sabt.AppServices.Definitions.PkiServicesInputMessage();
            //validateCertitificateInputMessage.CurrentActionType = request.CurrentActionType;
            //validateCertitificateInputMessage.CurrentReqID = request.CurrentReqObjectID;
            //validateCertitificateInputMessage.IsCurrentReq4RegisterService = ( theCurrentReq.TheONotaryDocumentType.Is4RegisterService == YesNo.Yes ) ? true : false;
            //validateCertitificateInputMessage.LogInChallengeSign = request.CertificateSign;

            //Sabt.AppServices.Definitions.PkiServicesOutputMessage certificateValidationResponse = certificateValidationService.ValidateCertificate(validateCertitificateInputMessage);
            //if ( !certificateValidationResponse.IsCertificateAuthorized )
            //{
            //    response.ServiceExecutionSucceeded = false;
            //    response.ServiceExecutionMessage = certificateValidationResponse.AuthorizationMessage;

            //    return response;
            //}

            await _applicationContextService.BeginTransactionAsync(cancellationToken);

            string digitalBookGeneratingMessage = null;
            if (DigitalBookUtility.IsDigitalBookGeneratingPermitted(theCurrentReq, _clientConfiguration.ENoteBookEnabledDate, _clientConfiguration.IsENoteBookAutoClassifyNoEnabled, ref operationMessage) == DigitalBookGeneratingPermissionStatus.Needed)
            {
                int documentChainIndex = 0;
                bool isExistsElectronicBook;
                (List<DocumentElectronicBook> generatedDigitalDocumentsBookEntityCollection, documentChainIndex, digitalBookGeneratingMessage, isExistsElectronicBook) = await _eNoteBookServerController.ProvideDigitalBookEntityWithReservedClassifyNo(theCurrentReq, cancellationToken, documentChainIndex, digitalBookGeneratingMessage);

                if (!generatedDigitalDocumentsBookEntityCollection.Any())
                {
                    if (string.IsNullOrWhiteSpace(digitalBookGeneratingMessage))
                        digitalBookGeneratingMessage = "خطا در تولید محتوای صفحه دفتر الکترونیک! لطفاً با پشتیبانی سامانه تماس حاصل نموده و شماره پرونده جاری را جهت بررسی اعلام نمایید.";
                    await _applicationContextService.RollbackTransactionAsync(cancellationToken);

                    // Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();

                    response.ServiceExecutionMessage = digitalBookGeneratingMessage;
                    response.ServiceExecutionSucceeded = false;

                    return response;
                }
                else
                {
                    List<string> digitalBookRecordHashedDataCollection = _signProvider.ProvideENoteBookHashedData(generatedDigitalDocumentsBookEntityCollection);
                    if (!digitalBookRecordHashedDataCollection.Any() || digitalBookRecordHashedDataCollection.Count < generatedDigitalDocumentsBookEntityCollection.Count)
                    {
                        digitalBookGeneratingMessage = "خطا در ساخت محتوای امضای الکترونیک برای درج در دفتر الکترونیک سردفتر. لطفاً شماره پرونده جاری را جهت بررسی به پشتیبانی سامانه اعلام نمایید.";
                        await _applicationContextService.RollbackTransactionAsync(cancellationToken);
                        // Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();

                        response.ServiceExecutionSucceeded = false;
                        response.ServiceExecutionMessage = digitalBookGeneratingMessage;

                        return response;
                    }
                    else
                    {
                        response.TheGeneratedDigitalBookEntityCollection = new List<DocumentElectronicBook>();
                        response.TheGeneratedDigitalBookEntityCollection.AddRange(generatedDigitalDocumentsBookEntityCollection);

                        response.TheDigitalBookHashedDataCollection = new List<string>();
                        response.TheDigitalBookHashedDataCollection.AddRange(digitalBookRecordHashedDataCollection);

                        foreach (DocumentElectronicBook theOneDigitalBook in response.TheGeneratedDigitalBookEntityCollection)
                        {
                            if (
                                theCurrentReq.ClassifyNo != null &&
                                !string.IsNullOrEmpty(theCurrentReq.WriteInBookDate) &&
                                theCurrentReq.DocumentType.IsSupportive == YesNo.Yes.GetString()
                                )
                                break;

                            if (
                                theCurrentReq.NationalNo == theOneDigitalBook.NationalNo &&
                                theCurrentReq.ScriptoriumId == theOneDigitalBook.ScriptoriumId &&
                                theCurrentReq.DocumentTypeId == theOneDigitalBook.DocumentTypeId
                                )
                            {
                                theCurrentReq.ClassifyNo = theOneDigitalBook.ClassifyNo;
                                string sss = theOneDigitalBook.ClassifyNo.ToString();
                                theCurrentReq.WriteInBookDate = theOneDigitalBook.EnterBookDateTime.Substring(0, 10);

                                break;
                            }
                        }

                        if (string.IsNullOrEmpty(theCurrentReq.WriteInBookDate))
                            theCurrentReq.WriteInBookDate = dateTimeService.CurrentPersianDate;

                        if (theCurrentReq.ClassifyNo == null)
                        {
                            string sss2 = theCurrentReq.ClassifyNo.To_String();
                            response.ServiceExecutionSucceeded = false;
                            response.ServiceExecutionMessage = "خطا در تخصیص شماره ترتیب به سند جاری. لطفاً با پشتیبانی تماس حاصل نمایید.";
                            await _applicationContextService.RollbackTransactionAsync(cancellationToken);

                            return response;
                        }

                    }
                }
                if (isExistsElectronicBook)
                    await _documentElectronicBookRepository.UpdateRangeAsync(generatedDigitalDocumentsBookEntityCollection,
                          cancellationToken, false);
                else
                {
                    await _documentElectronicBookRepository.AddRangeAsync(generatedDigitalDocumentsBookEntityCollection,
                        cancellationToken, false);
                }
                await documentRepository.UpdateAsync(theCurrentReq, cancellationToken);
            }

            //RDev.DataServices.OJB.FormSignInfoDataGraphProvider formSignInfoDataGraphProvider = new RDev.DataServices.OJB.FormSignInfoDataGraphProvider();
            //RDev.Framework.DigitalSignature.SignInfoDataGraph signInfoDataGraph = formSignInfoDataGraphProvider.GetFormSignInfoDataGraph(Rad.CMS.BaseInfoContext.Instance.CurrentForm.ObjectId);
            //response.SignInfoDataGraph = signInfoDataGraph;

            //string rawDataSignText = RDev.Framework.DigitalSignature.DataGraphSignHelper.GetDataSignText(theCurrentReq, signInfoDataGraph);
            //if ( !string.IsNullOrWhiteSpace ( rawDataSignText ) )
            //{
            //    List<byte> byteList = new List<byte>();
            //    byteList.AddRange ( System.Text.Encoding.UTF8.GetBytes ( rawDataSignText ) );

            //    byte[] rawDataByte = byteList.ToArray();
            //    response.TheCurrentReqSignText = Convert.ToBase64String ( rawDataByte );
            //}

            if (theCurrentReq.DocumentTypeId != "611")
            {

                ONotaryRegServiceInquiryInputMessage dsuInputMessage = new ONotaryRegServiceInquiryInputMessage();
                dsuInputMessage.registerServiceReq = theCurrentReq;
                dsuInputMessage.UnSignedPersonsList = request.UnSignedPersonsList;

                var dsuOutputMessage = await TryPrepareDSUDealSummary(dsuInputMessage, cancellationToken);
                if (!string.IsNullOrWhiteSpace(dsuOutputMessage.VerificationMessage))
                {
                    response.ServiceExecutionSucceeded = false;
                    response.ServiceExecutionMessage = dsuOutputMessage.VerificationMessage;
                    return response;
                }

                response.DSUSignPacketCollection = dsuOutputMessage.SignPacketCollection;
            }
            else
            {
                ONotaryRegServiceInquiryInputMessage dsuInputMessage = new ONotaryRegServiceInquiryInputMessage();
                dsuInputMessage.registerServiceReq = theCurrentReq;
                dsuInputMessage.UnSignedPersonsList = request.UnSignedPersonsList;

                var dsuOutputMessage = await TryPrepareSeparationDealSummary(dsuInputMessage, cancellationToken);
                if (CanSendDS())
                {
                    if (!string.IsNullOrWhiteSpace(dsuOutputMessage.VerificationMessage))
                    {
                        response.ServiceExecutionSucceeded = false;
                        response.ServiceExecutionMessage = dsuOutputMessage.VerificationMessage;
                        return response;
                    }

                    response.DSUSignPacketCollection = dsuOutputMessage.SignPacketCollection;
                    response.SDSCount = dsuOutputMessage.SeparationDSCount;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(dsuOutputMessage.VerificationMessage))
                    {
                        //response.ServiceExecutionSucceeded = true;
                        response.ServiceExecutionMessage = "";// dsuOutputMessage.VerificationMessage;
                        response.TheCurrentReq = theCurrentReq;
                        response.ConfirmationTimeLimit = _clientConfiguration.SemaphoreLifeTime / 60;
                        response.ServiceExecutionSucceeded = true;
                        return response;
                    }

                    response.DSUSignPacketCollection = dsuOutputMessage.SignPacketCollection;
                    response.SDSCount = dsuOutputMessage.SeparationDSCount;
                }

            }

            response.TheCurrentReq = theCurrentReq;
            response.ConfirmationTimeLimit = _clientConfiguration.SemaphoreLifeTime / 60;
            response.ServiceExecutionSucceeded = true;
            await _applicationContextService.CommitTransactionAsync(cancellationToken);
            return response;
        }

        /// <summary>
        /// The TryPrepareDSUDealSummary
        /// </summary>
        /// <param name="input">The input<see cref="ONotaryRegServiceInquiryInputMessage"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{ONotaryRegServiceInquiryOutputMessage}"/></returns>
        public async Task<ONotaryRegServiceInquiryOutputMessage> TryPrepareDSUDealSummary(ONotaryRegServiceInquiryInputMessage input, CancellationToken cancellationToken)
        {
            ONotaryRegServiceInquiryOutputMessage response = new ONotaryRegServiceInquiryOutputMessage();

            //estateInquiryEngineController.UnSignedPersonsList = input.UnSignedPersonsList;
            Document theCurrentRegisterServiceReq = null;
            if (input.registerServiceReq == null)
                theCurrentRegisterServiceReq =
                    await documentRepository.GetAsync(t => t.Id == Guid.Parse(input.ONotaryRegisterServiceReqID),
                        cancellationToken);
            else
                theCurrentRegisterServiceReq = input.registerServiceReq;

            string dsuDealSummaryMessage = string.Empty;

            if (DsuUtility.IsDSUGeneratingPermitted(ref dsuDealSummaryMessage, theCurrentRegisterServiceReq, null, false, _clientConfiguration))
            {
                string innerMessage = null;
                bool quotasAreValid = _quotasValidator.VerifyRegCasesInquiriesAndQuotas(theCurrentRegisterServiceReq, ref innerMessage);

                if (!quotasAreValid && IntelligantDocumentInheritor.IsAutoQuotaGeneratingPermitted(theCurrentRegisterServiceReq, _clientConfiguration, ref innerMessage))
                {
                    (bool generated, theCurrentRegisterServiceReq, innerMessage) = await _smartQuotaGeneratorEngine.GenerateQuotas(theCurrentRegisterServiceReq, cancellationToken, innerMessage, true);
                    if (generated)
                        theCurrentRegisterServiceReq = await documentRepository.GetAsync(t => t.Id == Guid.Parse(input.ONotaryRegisterServiceReqID),
                            cancellationToken); ;
                }

                bool simulationSucceeded = false;
                if (System.Diagnostics.Debugger.IsAttached)
                    (simulationSucceeded, dsuDealSummaryMessage) = await _estateInquiryEngine.SimulateDSUsSendingProcess(theCurrentRegisterServiceReq, dsuDealSummaryMessage, cancellationToken);

                (response.SignPacketCollection, dsuDealSummaryMessage) = await _estateInquiryEngine.CollectDSUDealSummariesRawDataB64(theCurrentRegisterServiceReq, dsuDealSummaryMessage, cancellationToken);
            }

            response.VerificationMessage = dsuDealSummaryMessage;
            response.MainObjectHashedData = input.MainObjectHashedData;

            return response;
        }

        private async Task<bool> IsValidClassifyNo(int? ReservedClassifyNo, Document theCurrentRegisterServiceReq, CancellationToken cancellationToken)
        {
            string digitalBookMessage = null;
            (decimal? nextClassifyNo, digitalBookMessage) =
                 await _eNoteBookServerController.ProvideNextClassifyNo(theCurrentRegisterServiceReq, cancellationToken,
                     digitalBookMessage);
            if (nextClassifyNo == ReservedClassifyNo) return true;
            return false;

        }
    }

}
