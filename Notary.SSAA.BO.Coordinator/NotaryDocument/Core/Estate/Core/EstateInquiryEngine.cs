using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Text;
using Notary.SSAA.BO.Configuration;
using MediatR;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.Coordinators.Estate;
using SSAA.Notary.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BO.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.Utilities;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.DealSummary;


namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core
{
    public class EstateInquiryEngine
    {


        private List<DocumentPerson> _unSignedPersonsList;
        public List<DocumentPerson> UnSignedPersonsList
        {
            get
            {
                return _unSignedPersonsList;
            }

            set
            {
                _unSignedPersonsList = value;
            }
        }

        private readonly ClientConfiguration _clientConfiguration;
        private readonly NotaryOfficeDSUEngineCore _notaryOfficeDSUEngineCore;
        private readonly EstateInquiryValidator _estateInquiryValidator;
        private readonly IRepository<DocumentEstateDealSummaryGenerated> _documentEstateDealSummaryGeneratedRepository;

        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IRepository<EstateInquiryPerson> _estateInquiryPersonRepository;
        private readonly IDealSummaryRepository _dealSummaryRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IMediator _mediator;
        private readonly DSULogger _dsuLogger;
        private readonly IDateTimeService _dateTimeService;

        public EstateInquiryEngine(ClientConfiguration clientConfiguration, NotaryOfficeDSUEngineCore notaryOfficeDSUEngineCore,
            EstateInquiryValidator estateInquiryValidator, IMediator mediator, DSULogger dsuLogger,
            IRepository<EstateInquiryPerson> estateInquiryPersonRepository
            , IEstateInquiryRepository estateInquiryRepository, IDealSummaryRepository dealSummaryRepository,
            IRepository<DocumentEstateDealSummaryGenerated> documentEstateDealSummaryGeneratedRepository
            , IDocumentRepository documentRepository
            , IDateTimeService dateTimeService)
        {
            _clientConfiguration = clientConfiguration;
            _notaryOfficeDSUEngineCore = notaryOfficeDSUEngineCore;
            _estateInquiryValidator = estateInquiryValidator;
            _mediator = mediator;
            _dsuLogger = dsuLogger;
            _estateInquiryRepository = estateInquiryRepository;
            _estateInquiryPersonRepository = estateInquiryPersonRepository;
            _documentEstateDealSummaryGeneratedRepository = documentEstateDealSummaryGeneratedRepository;
            _dealSummaryRepository = dealSummaryRepository;
            _documentRepository = documentRepository;
            _dateTimeService = dateTimeService;
        }


        public async Task<(List<DSUDealSummarySignPacket>, string)> CollectDSUDealSummariesRawDataB64(Document theCurrentRegisterServiceReq, string dealSummaryMessages, CancellationToken cancellationToken)
        {
            string dealSummaryMessagesRef = dealSummaryMessages;
            try
            {
                _notaryOfficeDSUEngineCore._DsuProcessActionType = ProcessActionType.DSUSending;
                _notaryOfficeDSUEngineCore.UnSignedPersonsList = this._unSignedPersonsList;

                bool isSentRecently = await this.IsDSUSent4ThisRequest(theCurrentRegisterServiceReq, cancellationToken);
                if (isSentRecently)
                {
                    return (null, dealSummaryMessagesRef);
                }
                _notaryOfficeDSUEngineCore._DsuProcessActionType = ProcessActionType.DSUSending;
                await _estateInquiryValidator.initialEstateInquiryValidator(theCurrentRegisterServiceReq,
                    cancellationToken);
                List<string> sentInquiriesCollection = new List<string>();
                bool isRemovingDSU = (theCurrentRegisterServiceReq.DocumentTypeId == "004") ? true : false;
                RequirmentsValidationStatus validationStatus = RequirmentsValidationStatus.Failed;

                if (theCurrentRegisterServiceReq.State == NotaryRegServiceReqState.Finalized.GetString())
                {
                    validationStatus = RequirmentsValidationStatus.Succeeded;
                }
                else
                {
                    if (isRemovingDSU)
                        validationStatus = _estateInquiryValidator.ValidateRemovingDSURequirments(theCurrentRegisterServiceReq, ref dealSummaryMessagesRef);
                    else
                        (validationStatus, sentInquiriesCollection, dealSummaryMessagesRef) = await _estateInquiryValidator.ValidateDSURequirments(theCurrentRegisterServiceReq, cancellationToken, sentInquiriesCollection, dealSummaryMessagesRef);
                }

                switch (validationStatus)
                {
                    case RequirmentsValidationStatus.Failed:
                    case RequirmentsValidationStatus.CompatibleCheckError:
                    case RequirmentsValidationStatus.SequenceCheckError:
                        if (string.IsNullOrWhiteSpace(dealSummaryMessagesRef))
                            dealSummaryMessagesRef = "خطا در بررسی پیش نیاز های ارسال خلاصه معامله توسط سیستم!";

                        return (null, dealSummaryMessagesRef);

                    case RequirmentsValidationStatus.InquiriesNotPermitted:
                        if (string.IsNullOrWhiteSpace(dealSummaryMessagesRef))
                        {
                            dealSummaryMessagesRef = "خطا در بررسی پیش نیاز های ارسال خلاصه معامله توسط سیستم!";
                        }

                        if (!sentInquiriesCollection.Any())
                            return (null, dealSummaryMessagesRef);

                        break;
                }

                isSentRecently = await this.IsDSUSent4ThisRequest(theCurrentRegisterServiceReq, cancellationToken);
                if (isSentRecently)
                {
                    if (!string.IsNullOrWhiteSpace(dealSummaryMessagesRef) && dealSummaryMessagesRef.GetStandardFarsiString().Contains("خلاصه معامله مالکیت برای این استعلام ثبت شده است"))
                    {
                        dealSummaryMessagesRef = null;
                    }

                    return (null, dealSummaryMessagesRef);
                }
                _notaryOfficeDSUEngineCore._DsuProcessActionType = ProcessActionType.DSUSending;
                bool isRestricted = Mapper.IsONotaryDocumentRestrictedType(theCurrentRegisterServiceReq.DocumentTypeId);

                List<DSUDealSummaryObject> dsuDealSummaryObjectsCollection = null;
                List<DSUDealSummarySignPacket> rawDataPacketCollection = null;
                DocumentEstateDealSummaryGenerated? theFailedDeterministicDSUXML = null;

                theFailedDeterministicDSUXML = await _notaryOfficeDSUEngineCore.CollectFailedDeterministicDealSummaries(theCurrentRegisterServiceReq, cancellationToken);

                #region FailedDSUs NOT Exist
                if (theFailedDeterministicDSUXML == null)
                {
                    (dsuDealSummaryObjectsCollection, dealSummaryMessagesRef) = await _notaryOfficeDSUEngineCore.CollectDSUDealSummaries(theCurrentRegisterServiceReq, cancellationToken, dealSummaryMessagesRef, true);

                    if (dsuDealSummaryObjectsCollection==null || !dsuDealSummaryObjectsCollection.Any())
                    {
                        if (string.IsNullOrWhiteSpace(dealSummaryMessagesRef))
                        {
                            bool generatedDSUsCountIsValid = _notaryOfficeDSUEngineCore.ValidateGeneratedDSUsCount(theCurrentRegisterServiceReq, dsuDealSummaryObjectsCollection, ref dealSummaryMessagesRef);
                            if (!generatedDSUsCountIsValid)
                            {
                                if (string.IsNullOrWhiteSpace(dealSummaryMessagesRef))
                                {
                                    dealSummaryMessagesRef = "خلاصه معامله های مورد نیاز برای ارسال، تولید نشد. لطفاً مجدداً تلاش نمایید.";
                                }
                            }
                        }

                        return (null, dealSummaryMessagesRef);
                    }

                    #region VerifyDSUs
                    string exceptionMessages = null;
                    bool isDSUCollectionValid = false;

                    if (isRemovingDSU && isRestricted)
                        isDSUCollectionValid = true;
                    else
                        (isDSUCollectionValid, dealSummaryMessagesRef, exceptionMessages) = await this.VerifyGeneratedDSUsCollection(dsuDealSummaryObjectsCollection, isRestricted, ProcessActionType.DSUSending, theCurrentRegisterServiceReq.DocumentTypeId, cancellationToken, dealSummaryMessagesRef, exceptionMessages);

                    //Removing And Verifying Sent DSUs
                    if (!isDSUCollectionValid && !string.IsNullOrWhiteSpace(dealSummaryMessagesRef))
                    {
                        string dsuError = dealSummaryMessagesRef.GetStandardFarsiString();

                        if ((dsuError.Contains("ثبت و ارسال شده است") || dsuError.Contains("قبلا خلاصه معامله محدودیت ثبت شده است")) && sentInquiriesCollection.Any())
                        {
                            dsuDealSummaryObjectsCollection = await _notaryOfficeDSUEngineCore.RemoveSentDSUObjectsFromSignedCollection(theCurrentRegisterServiceReq, cancellationToken, dsuDealSummaryObjectsCollection, sentInquiriesCollection);
                            (isDSUCollectionValid, dealSummaryMessagesRef, exceptionMessages) = await this.VerifyGeneratedDSUsCollection(dsuDealSummaryObjectsCollection, isRestricted, ProcessActionType.DSUSending, theCurrentRegisterServiceReq.DocumentTypeId, cancellationToken, dealSummaryMessagesRef, exceptionMessages);
                        }
                    }
                    #endregion

                    #region LogGeneratedDSUs
                    exceptionMessages = dealSummaryMessagesRef + "\n\n\n" + exceptionMessages;
                    DocumentEstateDealSummaryGenerated? generatedDSULog = null;

                    if (!dsuDealSummaryObjectsCollection.Any() && sentInquiriesCollection.Any())
                        await _dsuLogger.UpdateDSULogStatus(theCurrentRegisterServiceReq.Id.ToString(), NotaryGeneratedDealSummary.Sent, dealSummaryMessagesRef, cancellationToken);
                    else
                        generatedDSULog = await _dsuLogger.LogGeneratedDSUCollection(dsuDealSummaryObjectsCollection, theCurrentRegisterServiceReq.Id.ToString(), cancellationToken, exceptionMessages);
                    #endregion

                    if (isDSUCollectionValid && dsuDealSummaryObjectsCollection!=null && dsuDealSummaryObjectsCollection.Any())
                    {
                        //Important Tip: Because the Encodings on server and Database differ, it's needed to fetch logged xml from DB rather than using the one currently generated.
                        //So These Steps are being passed:
                        //1. Generate
                        //2. Log
                        //3. Fetch Again
                        //4. Provide Sign Packet using Fetched Object
                        if (generatedDSULog != null)
                        {
                            ExportManager exportManager = new ExportManager();
                            List<DSUDealSummaryObject> loggedDSUsCollection = exportManager.ExportDSUObjectFromXML(generatedDSULog.Xml);
                            rawDataPacketCollection = await _notaryOfficeDSUEngineCore.ProvideSignPacketCollection(loggedDSUsCollection, cancellationToken);
                        }
                    }
                }
                #endregion

                #region FailedDSUs Exist
                else
                {
                    dsuDealSummaryObjectsCollection = await _dsuLogger.ExportGeneratedDSULog(theFailedDeterministicDSUXML.DocumentId.ToString(), cancellationToken);

                    rawDataPacketCollection = await _notaryOfficeDSUEngineCore.ProvideSignPacketCollection(dsuDealSummaryObjectsCollection, cancellationToken, true);
                }
                #endregion

                return (rawDataPacketCollection, dealSummaryMessagesRef);
            }
            catch (Exception ex)
            {
                dealSummaryMessagesRef = "خطا در بررسی پیش نیاز های ارسال خلاصه معامله توسط سیستم!";
                return (null, dealSummaryMessagesRef);
            }
        }

        public async Task<(bool, string)> SimulateDSUsSendingProcess(Document theCurrentRegisterServiceReq, string responseMessages, CancellationToken cancellationToken)
        {
            string responseMessagesRef = responseMessages;
            if (theCurrentRegisterServiceReq.State == NotaryRegServiceReqState.Finalized.GetString())
                return (true, responseMessagesRef);

            bool simulationSucceeded = false;

            if (theCurrentRegisterServiceReq.DocumentTypeId == "901")     // پیش فروش ساختمان
            {
                //  simulationSucceeded = this.SimulatePreSellDSUsSendingProcess ( theCurrentRegisterServiceReq, ref responseMessages );
            }
            else
            {
                if (theCurrentRegisterServiceReq.DocumentTypeId == "004")     // فک رهن
                    (simulationSucceeded, responseMessagesRef) = await this.SimulateRemovingDSUsSendingProcess(theCurrentRegisterServiceReq, cancellationToken, responseMessagesRef);
                else
                    (simulationSucceeded, responseMessagesRef) = await this.SimulateMainDSUsSendingProcess(theCurrentRegisterServiceReq, cancellationToken, responseMessagesRef);
            }

            return (simulationSucceeded, responseMessagesRef);
        }

        public async Task<(NotaryGeneratedDealSummary, string)> SendDSUDealSummaries(Document theCurrentRegisterServiceReq, List<DSUDealSummarySignPacket> signPacketsCollection, CancellationToken cancellationToken, string messages, bool tagResend = false)
        {
            string messagesRef = messages;
            NotaryGeneratedDealSummary dsuSendingStatus = NotaryGeneratedDealSummary.NotSent;

            if (_clientConfiguration.IsDSUDealSummaryCreationEnabled != DSUActionLevel.FullFeature)
            {
                messagesRef = "ارسال اتوماتیک خلاصه معامله غیر فعال می باشد.";
                return (NotaryGeneratedDealSummary.NotSent, messagesRef);
            }

            if (!signPacketsCollection.Any())
            {
                messagesRef = "خلاصه معامله ای برای ارسال اتوماتیک تعریف نشده است.";
                return (NotaryGeneratedDealSummary.NotSent, messagesRef);
            }

            bool isDSUSent = false;

            try
            {
                _notaryOfficeDSUEngineCore._DsuProcessActionType = ProcessActionType.DSUSending;
                _estateInquiryValidator._theONotaryRegisterServiceReq = theCurrentRegisterServiceReq;
                List<string> sentInquiriesCollection = new List<string>();
                RequirmentsValidationStatus finalCheckStatus = RequirmentsValidationStatus.Failed;

                bool isRemovingDSU = (theCurrentRegisterServiceReq.DocumentTypeId == "004") ? true : false;

                if (!tagResend)
                {
                    if (isRemovingDSU)
                        finalCheckStatus = _estateInquiryValidator.ValidateRemovingDSURequirments(theCurrentRegisterServiceReq, ref messagesRef);
                    else
                        (finalCheckStatus, sentInquiriesCollection, messagesRef) = await _estateInquiryValidator.ValidateDSURequirments(theCurrentRegisterServiceReq, cancellationToken, sentInquiriesCollection, messagesRef);

                    if (finalCheckStatus != RequirmentsValidationStatus.Succeeded)
                        return (NotaryGeneratedDealSummary.NotSent, messagesRef);
                }

                bool isFailedOldDSUInProcess = false;
                foreach (DSUDealSummarySignPacket theOneSignedPacket in signPacketsCollection)
                {
                    if (theOneSignedPacket.FailedOldDeterministicDSU)
                    {
                        isFailedOldDSUInProcess = true;
                        break;
                    }
                }

                List<DSUDealSummaryObject> signedDSUDealSummariesCollection;
                (signedDSUDealSummariesCollection, messagesRef) = await _notaryOfficeDSUEngineCore.CollectSignedDSUDealSummaries(signPacketsCollection, theCurrentRegisterServiceReq, cancellationToken, messagesRef, isFailedOldDSUInProcess);
                if (signedDSUDealSummariesCollection == null || !signedDSUDealSummariesCollection.Any())
                {
                    messagesRef = "اشکال در دریافت بسته های خلاصه معامله های امضا شده! ارسال خلاصه معامله توسط سیستم لغو گردید.";
                    return (NotaryGeneratedDealSummary.NotSent, messagesRef);
                }


                if (!isFailedOldDSUInProcess)
                    await _dsuLogger.LogGeneratedDSUCollection(signedDSUDealSummariesCollection, theCurrentRegisterServiceReq.Id.ToString(), cancellationToken);
                else
                    await _dsuLogger.LogGeneratedDSUCollection(signedDSUDealSummariesCollection, signedDSUDealSummariesCollection[0].NotaryDocumentNo, cancellationToken);

                #region Verify DSUSending Deadline
                if (!isFailedOldDSUInProcess)
                {
                    string dsuSendingDeadLineDate = null;
                    if (!string.IsNullOrWhiteSpace(theCurrentRegisterServiceReq.SignDate))
                        dsuSendingDeadLineDate = theCurrentRegisterServiceReq.SignDate.AddDays(_clientConfiguration.DSUSendingTimeLimit);

                    if (!string.IsNullOrWhiteSpace(dsuSendingDeadLineDate) && string.Compare(dsuSendingDeadLineDate, _dateTimeService.CurrentPersianDate) < 0)
                    {
                        messagesRef =
                            "توجه! " +
                            System.Environment.NewLine +
                            "با توجه به اینکه مهلت قانونی برای تایید نهایی سند( ارسال خلاصه معامله ) رعایت نشده است،" +
                            System.Environment.NewLine +
                            "خلاصه معامله مربوط به این سند، بصورت خودکار ارسال نخواهد شد.";

                        await _dsuLogger.UpdateDSULogStatus(theCurrentRegisterServiceReq.Id.ToString(), NotaryGeneratedDealSummary.TimedOut, messagesRef, cancellationToken, signedDSUDealSummariesCollection);
                        return (NotaryGeneratedDealSummary.TimedOut, messagesRef);
                    }
                }
                #endregion

                SendDealSummaryBOServiceInput dsuDealSummarySendingMessagePacket = new SendDealSummaryBOServiceInput();
                dsuDealSummarySendingMessagePacket.SendDealSummaryInput =
                    new SendDealSummaryInput<DealSummaryServiceOutputViewModel>();
                dsuDealSummarySendingMessagePacket.SendDealSummaryInput.DsuDealSummary = signedDSUDealSummariesCollection;

                if (tagResend)
                    dsuDealSummarySendingMessagePacket.SendDealSummaryInput.Tag = "Resend";

                //======================================================TEST=SCENARIO========================================================
                //ExportManager exportManager = new ExportManager();
                //string generatedDSUXML = exportManager.GenerateXML(dsuDealSummarySendingMessagePacket.DsuDealSummary);
                //DealSummaryVerificationResultMessage verifyResponse = dsuProxy.DealSummaryVerification(dsuDealSummarySendingMessagePacket);
                //===========================================================================================================================
                //bool isRemovingDSU = (theCurrentRegisterServiceReq.TheONotaryDocumentType.Code == "004") ? true : false;


                ApiResult<DealSummaryServiceOutputViewModel> sendingResponse = null;




                if (isRemovingDSU)
                {
                    dsuDealSummarySendingMessagePacket.SendDealSummaryInput.IsRemoveRestrictionDealSummary = true;
                    sendingResponse = await _mediator.Send(dsuDealSummarySendingMessagePacket.SendDealSummaryInput, cancellationToken);

                }

                else
                {
                    dsuDealSummarySendingMessagePacket.SendDealSummaryInput.IsRemoveRestrictionDealSummary = false;
                    sendingResponse = await _mediator.Send(dsuDealSummarySendingMessagePacket.SendDealSummaryInput, cancellationToken);

                }


                if (sendingResponse == null || sendingResponse.IsSuccess == false)
                {
                    messagesRef = "ارتباط با سرویس الکترونیک خلاصه معامله املاک برقرار نشد. لطفاً مجدداً تلاش نمایید.";
                    dsuSendingStatus = NotaryGeneratedDealSummary.NotSent;


                    return (NotaryGeneratedDealSummary.NotSent, messagesRef);
                }

                isDSUSent = sendingResponse.Data != null ? sendingResponse.Data.Result : false;

                if (!isDSUSent)
                {
                    while (!isDSUSent && (sendingResponse.Data.SendedInquiryList.Any() || sendingResponse.Data.SendedRemoveRestrictionDealSummaries.Any()))
                    {
                        List<string> idsCollectionToRemove = (sendingResponse.Data.SendedInquiryList.Any()) ? sendingResponse.Data.SendedInquiryList : sendingResponse.Data.SendedRemoveRestrictionDealSummaries;

                        //Remove Extra DSUObjects
                        signedDSUDealSummariesCollection = await _notaryOfficeDSUEngineCore.RemoveSentDSUObjectsFromSignedCollection(theCurrentRegisterServiceReq, cancellationToken, signedDSUDealSummariesCollection, idsCollectionToRemove);

                        //After Removing Repeated DSUObjects, it may occure that no dsuobject remains to send. 
                        //So it means that all itmes of current dsucollection is being sent recently and 
                        //the whole process is done. So The Action result is 'TRUE'
                        //CAUTION: In such scenario, no log is needed and the last log is correct.
                        if (signedDSUDealSummariesCollection.Any())
                        {
                            dsuDealSummarySendingMessagePacket.SendDealSummaryInput.DsuDealSummary = signedDSUDealSummariesCollection;

                            //Log New DSUObject Collection
                            //dsuLogController.LogGeneratedDSUCollection(signedDSUDealSummariesCollection, theCurrentRegisterServiceReq.ObjectId);

                            //Send New DSUObject Collection
                            if (isRemovingDSU)
                            {
                                dsuDealSummarySendingMessagePacket.SendDealSummaryInput.IsRemoveRestrictionDealSummary = true;
                                sendingResponse = await _mediator.Send(dsuDealSummarySendingMessagePacket.SendDealSummaryInput, cancellationToken);
                            }
                            else
                            {
                                dsuDealSummarySendingMessagePacket.SendDealSummaryInput.IsRemoveRestrictionDealSummary = false;
                                sendingResponse = await _mediator.Send(dsuDealSummarySendingMessagePacket.SendDealSummaryInput, cancellationToken);

                            }


                            if (sendingResponse == null)
                            {
                                messagesRef = "ارتباط با سرویس الکترونیک خلاصه معامله املاک برقرار نشد. لطفاً مجدداً تلاش نمایید.";
                                dsuSendingStatus = NotaryGeneratedDealSummary.NotSent;


                                return (NotaryGeneratedDealSummary.NotSent, messagesRef);
                            }

                            isDSUSent = (sendingResponse.Data != null) ? sendingResponse.Data.Result : false;
                        }
                        else
                        {
                            isDSUSent = true;
                        }
                    }
                }

                string dsuError = (sendingResponse.Data != null) ? sendingResponse.Data.ErrorMessage : "خطا در ارسال خلاصه معامله";
                if (!string.IsNullOrWhiteSpace(dsuError))
                    dsuError = dsuError.GetStandardFarsiString();

                if (isDSUSent)
                {
                    dsuSendingStatus = NotaryGeneratedDealSummary.Sent;
                }
                else
                {
                    dsuSendingStatus = NotaryGeneratedDealSummary.NotSent;
                }

                string mainObjectId = null;
                if (!isFailedOldDSUInProcess)
                    mainObjectId = theCurrentRegisterServiceReq.Id.ToString();
                else
                    mainObjectId = signedDSUDealSummariesCollection[0].NotaryDocumentNo;


                _dsuLogger.UpdateDSULogStatus(mainObjectId, dsuSendingStatus, sendingResponse.Data.ErrorMessage, cancellationToken, signedDSUDealSummariesCollection);

                if (isFailedOldDSUInProcess)
                {
                    messagesRef = "ارسال خلاصه معامله سند رهنی قطعی با اشکال مواجه شد. لطفاً مجدداً تلاش نمایید.";
                    return (NotaryGeneratedDealSummary.NotSent, messagesRef);
                }

                if (isDSUSent)
                    messagesRef = signedDSUDealSummariesCollection.Count + " خلاصه معامله برای سند جاری، تولید و با موفقیت ارسال گردید.";
                else
                    messagesRef = (sendingResponse.Data != null) ? sendingResponse.Data.ErrorMessage : "خطا در ارسال خلاصه معامله";
            }
            catch (Exception ex)
            {
                messagesRef = "خطای سیستمی در لحظه نهایی ارسال خلاصه معامله! لطفاً مجدداً تلاش نمایید.";
                dsuSendingStatus = NotaryGeneratedDealSummary.NotSent;
            }

            return (dsuSendingStatus, messagesRef);
        }

        public async Task<NotaryGeneratedDealSummary> ReSendDSUDealSummaries(DocumentEstateDealSummaryGenerated theLoggedGeneratedDealSummaries, CancellationToken cancellationToken, string messages)
        {
            string messagesRef = messages;
            NotaryGeneratedDealSummary dsuSendingStatus = NotaryGeneratedDealSummary.NotSent;


            _notaryOfficeDSUEngineCore._DsuProcessActionType = ProcessActionType.DSUSending;
            _estateInquiryValidator._theONotaryRegisterServiceReq = theLoggedGeneratedDealSummaries.Document;

            List<string> sentInquiriesCollection = new List<string>();
            RequirmentsValidationStatus validationStatus;

            (validationStatus, sentInquiriesCollection, messagesRef) = await _estateInquiryValidator.ValidateDSURequirments(theLoggedGeneratedDealSummaries.Document, cancellationToken, sentInquiriesCollection, messagesRef);
            if (validationStatus != RequirmentsValidationStatus.Succeeded)
                return NotaryGeneratedDealSummary.NotSent;

            ExportManager exportManager = new ExportManager();
            List<DSUDealSummaryObject> loggedDSUs = exportManager.ExportDSUObjectFromXML(theLoggedGeneratedDealSummaries.Xml);

            loggedDSUs = this.AppendSignDateToDSUObjects(theLoggedGeneratedDealSummaries.Document, loggedDSUs);
            loggedDSUs = await this.CorrectInvalidDataOnDSUObjects(loggedDSUs, cancellationToken);


            await _dsuLogger.LogGeneratedDSUCollection(loggedDSUs, theLoggedGeneratedDealSummaries.DocumentId.ToString(), cancellationToken);


            SendDealSummaryBOServiceInput dsuDealSummarySendingMessagePacket = new SendDealSummaryBOServiceInput();
            dsuDealSummarySendingMessagePacket.SendDealSummaryInput =
                new SendDealSummaryInput<DealSummaryServiceOutputViewModel>();


            dsuDealSummarySendingMessagePacket.SendDealSummaryInput.DsuDealSummary = loggedDSUs;
            var sendingResponse = await _mediator.Send(dsuDealSummarySendingMessagePacket.SendDealSummaryInput, cancellationToken);





            if (sendingResponse == null || sendingResponse.IsSuccess == false)
            {
                messagesRef = "ارتباط با سرویس الکترونیک خلاصه معامله املاک برقرار نشد. لطفاً مجدداً تلاش نمایید.";
                dsuSendingStatus = NotaryGeneratedDealSummary.NotSent;


                return NotaryGeneratedDealSummary.NotSent;
            }

            bool isDSUSent = sendingResponse.Data.Result;

            if (!isDSUSent)
            {
                if (sendingResponse.Data.SendedInquiryList.Any())
                {
                    //Remove Extra DSUObjects
                    loggedDSUs = await _notaryOfficeDSUEngineCore.RemoveSentDSUObjectsFromSignedCollection(theLoggedGeneratedDealSummaries.Document, cancellationToken, loggedDSUs, sendingResponse.Data.SendedInquiryList);

                    //After Removing Repeated DSUObjects, it may occure that no dsuobject remains to send. 
                    //So it means that all itmes of current dsucollection is being sent recently and 
                    //the whole process is done. So The Action result is 'TRUE'
                    //CAUTION: In such scenario, no log is needed and the last log is correct.
                    if (loggedDSUs.Any())
                    {
                        dsuDealSummarySendingMessagePacket.SendDealSummaryInput.DsuDealSummary = loggedDSUs;

                        //Log New DSUObject Collection
                        //dsuLogController.LogGeneratedDSUCollection(signedDSUDealSummariesCollection, theCurrentRegisterServiceReq.ObjectId);

                        //Send New DSUObject Collection
                        sendingResponse = await _mediator.Send(dsuDealSummarySendingMessagePacket.SendDealSummaryInput, cancellationToken);

                        if (sendingResponse == null)
                        {
                            messagesRef = "ارتباط با سرویس الکترونیک خلاصه معامله املاک برقرار نشد. لطفاً مجدداً تلاش نمایید.";
                            dsuSendingStatus = NotaryGeneratedDealSummary.NotSent;


                            return NotaryGeneratedDealSummary.NotSent;
                        }

                        isDSUSent = sendingResponse.Data.Result;
                    }
                    else
                    {
                        isDSUSent = true;
                    }
                }
            }

            string dsuError = sendingResponse.Data.ErrorMessage;
            if (!string.IsNullOrWhiteSpace(dsuError))
                dsuError = dsuError.GetStandardFarsiString();

            messagesRef = dsuError;

            if (isDSUSent)
            {
                dsuSendingStatus = NotaryGeneratedDealSummary.Sent;

                await _dsuLogger.UpdateDSULogStatus(theLoggedGeneratedDealSummaries.DocumentId.ToString(), dsuSendingStatus, sendingResponse.Data.ErrorMessage, cancellationToken, loggedDSUs);
            }
            else
            {
                dsuSendingStatus = NotaryGeneratedDealSummary.NotSent;
            }

            return dsuSendingStatus;
        }

        public async Task<(HealthyCheckStatus, string)> CompareSimulatedDSUHealthyToMainDSU(Document theCurrentRegisterServiceReq, string responseMessages, CancellationToken cancellationToken)
        {
            string responseMessagesRef = responseMessages;
            HealthyCheckStatus healthyCheckStatus = HealthyCheckStatus.OK;

            try
            {
                _notaryOfficeDSUEngineCore._DsuProcessActionType = ProcessActionType.DSUSending;
                List<DSUDealSummaryObject> generatedDSUsCollection;
                (generatedDSUsCollection, responseMessagesRef) = await _notaryOfficeDSUEngineCore.CollectDSUDealSummaries(theCurrentRegisterServiceReq, cancellationToken, responseMessagesRef);

                if (!generatedDSUsCollection.Any())
                {
                    return (HealthyCheckStatus.NoDSUCreated, responseMessagesRef);
                }

                var originalDSUs = await _dealSummaryRepository.GetAllAsync(t => t.NotaryDocumentId ==
                    theCurrentRegisterServiceReq.Id.ToString(), cancellationToken);


                foreach (DSUDealSummaryObject theOneGeneratedDSU in generatedDSUsCollection)
                {
                    string tempMSG = null;
                    var theOneMainDSU = this.FindEquivalantDSU(originalDSUs, theOneGeneratedDSU, ref tempMSG);
                    if (theOneMainDSU == null)
                    {
                        string temp =
                            System.Environment.NewLine +
                            "خلاصه معادل یافت نشد! " + "خلاصه تولید شده و مورد نظر : " + "شماره معامله : " + theOneGeneratedDSU.DealNo + " - شناسه دفترخانه : " + theOneGeneratedDSU.DealSummeryIssuerId +
                            System.Environment.NewLine +
                            tempMSG +
                            System.Environment.NewLine;

                        if (!string.IsNullOrWhiteSpace(responseMessagesRef))
                        {
                            if (!responseMessagesRef.GetStandardFarsiString().Contains(temp.GetStandardFarsiString()))
                            {
                                responseMessagesRef += temp;
                            }
                        }
                        else
                        {
                            responseMessagesRef += temp;
                        }

                        healthyCheckStatus = HealthyCheckStatus.NoDSUFound;
                        continue;
                    }

                    foreach (DSURealLegalPersonObject theOneGeneratedDSUPerson in theOneGeneratedDSU.TheDSURealLegalPersonList)
                    {
                        DealSummaryPerson originalDSUPerson = this.FindEquivalantDSUPerson(theOneMainDSU.DealSummaryPeople, theOneGeneratedDSUPerson);

                        if (originalDSUPerson == null)
                        {
                            responseMessagesRef +=
                                System.Environment.NewLine +
                                "شخص معادل " + theOneGeneratedDSUPerson.Name + " " + theOneGeneratedDSUPerson.Family + "، در خلاصه ارسال شده یافت نشد." +
                                System.Environment.NewLine;

                            healthyCheckStatus = HealthyCheckStatus.NoPersonFound;

                            continue;
                        }


                        decimal? originalSharePart = originalDSUPerson.SharePart.TrimDoubleValue();
                        decimal? originalShareTotal = originalDSUPerson.ShareTotal.TrimDoubleValue();
                        decimal? generatedSharePart = (decimal?)theOneGeneratedDSUPerson.SharePart;
                        decimal? generatedShareTotal = (decimal?)theOneGeneratedDSUPerson.ShareTotal;

                        if (originalSharePart != null && originalShareTotal != null && generatedSharePart != null && generatedShareTotal != null)
                            if ((generatedSharePart * originalShareTotal) != (generatedShareTotal * originalSharePart))
                            {
                                responseMessagesRef +=
                                    "عدم تطابق سهم های " + theOneGeneratedDSUPerson.Name + " " + theOneGeneratedDSUPerson.Family +
                                    System.Environment.NewLine +
                                    "جزء سهم در سند: " + ((decimal?)theOneGeneratedDSUPerson.SharePart).To_String() + " جزء سهم در خلاصه ارسال شده : " + originalDSUPerson.SharePart.To_String() +
                                    System.Environment.NewLine +
                                    "کل سهم در سند: " + ((decimal?)theOneGeneratedDSUPerson.ShareTotal).To_String() + " کل سهم در خلاصه ارسال شده : " + originalDSUPerson.ShareTotal.To_String() +
                                    System.Environment.NewLine;


                                healthyCheckStatus = HealthyCheckStatus.SharePartsConflict;

                            }

                        //if (theOneGeneratedDSUPerson.SharePart.TrimDoubleValue() != originalDSUPerson.SharePart.TrimDoubleValue())
                        //{
                        //    responseMessages +=
                        //        "عدم تطابق سهم های " + theOneGeneratedDSUPerson.Name + " " + theOneGeneratedDSUPerson.Family +
                        //        System.Environment.NewLine +
                        //        "جزء سهم در سند: " + theOneGeneratedDSUPerson.SharePart.To_String() + " جزء سهم در خلاصه ارسال شده : " + originalDSUPerson.SharePart.To_String() +
                        //        System.Environment.NewLine;

                        //    isHealthy = false;
                        //}


                        //if (theOneGeneratedDSUPerson.ShareTotal.TrimDoubleValue() != originalDSUPerson.ShareTotal.TrimDoubleValue())
                        //{
                        //    responseMessages +=
                        //        "عدم تطابق سهم ها!" +
                        //        System.Environment.NewLine +
                        //        "کل سهم در سند: " + theOneGeneratedDSUPerson.ShareTotal.To_String() + " کل سهم در خلاصه ارسال شده : " + originalDSUPerson.ShareTotal.To_String() +
                        //        System.Environment.NewLine;

                        //    isHealthy = false;
                        //}

                        string shareContext = originalDSUPerson.ShareText;
                        if (!string.IsNullOrWhiteSpace(theOneGeneratedDSUPerson.ShareContext) && !string.IsNullOrWhiteSpace(originalDSUPerson.ShareText))
                            if (theOneGeneratedDSUPerson.ShareContext.GetStandardFarsiString() != originalDSUPerson.ShareText.GetStandardFarsiString())
                            {
                                responseMessagesRef +=
                                    "عدم تطابق سهم ها!" +
                                    System.Environment.NewLine +
                                    "متن سهم در سند: " + theOneGeneratedDSUPerson.ShareContext + " متن سهم در خلاصه ارسال شده : " + originalDSUPerson.ShareText +
                                    System.Environment.NewLine;

                                healthyCheckStatus = HealthyCheckStatus.ShareContextConflict;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                healthyCheckStatus = HealthyCheckStatus.Exception;
                responseMessagesRef = ex.ToCompleteString() + System.Environment.NewLine;
            }

            return (healthyCheckStatus, responseMessagesRef);
        }
        public async Task<bool> IsDSUSent4ThisRequest(string onotaryRegisterServiceReqObjectID, CancellationToken cancellationToken)
        {
            Document currentOnotaryRegisterServiceReq = await _documentRepository.GetDocumentById(Guid.Parse(onotaryRegisterServiceReqObjectID), new List<string>() { "DocumentInquiries" }, cancellationToken);
            _notaryOfficeDSUEngineCore._DsuProcessActionType = ProcessActionType.None;


            bool isSent = await _notaryOfficeDSUEngineCore.IsDSUSent4ThisRequest(currentOnotaryRegisterServiceReq, cancellationToken);

            return isSent;
        }

        public async Task<bool> IsDSUSent4ThisRequest(Document currentOnotaryRegisterServiceReq, CancellationToken cancellationToken)
        {
            _notaryOfficeDSUEngineCore._DsuProcessActionType = ProcessActionType.None;
            bool isSent = await _notaryOfficeDSUEngineCore.IsDSUSent4ThisRequest(currentOnotaryRegisterServiceReq, cancellationToken);

            return isSent;
        }

        internal async Task<(bool, string, string)> VerifyGeneratedDSUsCollection(List<DSUDealSummaryObject> generatedDSUsCollection, bool isRestricted, ProcessActionType processActionType
            , string documentTypeCode, CancellationToken cancellationToken, string verificationMessage, string exceptionMessage)
        {
            string verificationMessageRef = verificationMessage;
            string exceptionMessageRef = exceptionMessage;

            try
            {
                foreach (DSUDealSummaryObject theOneDSU in generatedDSUsCollection)
                {
                    if (!string.IsNullOrWhiteSpace(theOneDSU.DSURemoveRestirctionTypeId) && !string.IsNullOrWhiteSpace(theOneDSU.unrestrictedOrganizationId))
                        return (true, verificationMessageRef, exceptionMessageRef);
                }

                if (documentTypeCode == "115")   // قطعی مشتمل بر رهن - غیرمنقول
                {
                    foreach (DSUDealSummaryObject theOneDSU in generatedDSUsCollection)
                    {
                        var dealSummaryInputMessage =
                                new  DealSummaryVerificationWithoutOwnerCheckingQuery<DealSummaryVerificationResultViewModel>();


                        var dealSummaryInput =
                            new  DealSummaryVerificationQuery<DealSummaryVerificationResultViewModel>();


                        dynamic response;

                        dealSummaryInputMessage.DsuDealSummary = new List<DSUDealSummaryObject>();
                        dealSummaryInputMessage.DsuDealSummary.Add(theOneDSU);

                        dealSummaryInput.DsuDealSummary = new List<DSUDealSummaryObject>();
                        dealSummaryInput.DsuDealSummary.Add(theOneDSU);

                        if (theOneDSU.DSUTransferTypeId == "1")
                            response = await _mediator.Send(dealSummaryInputMessage, cancellationToken);

                        else
                            response = await _mediator.Send(dealSummaryInput, cancellationToken);




                        string dsuError = response.Data.ErrorMessage;

                        verificationMessageRef = response.Result.ErrorMessage;

                        if (!response.Data.Result)
                            return response.Result;
                    }

                    return (true, verificationMessageRef, exceptionMessageRef);
                }
                else
                {

                    var dealSummaryInputMessage =
                        new  DealSummaryVerificationWithoutOwnerCheckingQuery<DealSummaryVerificationResultViewModel>();

                    

                    var dealSummaryInput =
                        new  DealSummaryVerificationQuery<DealSummaryVerificationResultViewModel>();
                    


                    ApiResult<DealSummaryVerificationResultViewModel> response;

                    dealSummaryInputMessage.DsuDealSummary = new List<DSUDealSummaryObject>();

                    dealSummaryInput.DsuDealSummary = new List<DSUDealSummaryObject>();


                    dealSummaryInputMessage.DsuDealSummary = generatedDSUsCollection;
                    dealSummaryInput.DsuDealSummary = generatedDSUsCollection;





                    if (isRestricted && processActionType == ProcessActionType.DSUSimulation)
                        response = await _mediator.Send(dealSummaryInputMessage, cancellationToken);
                    else
                        response = await _mediator.Send(dealSummaryInput, cancellationToken);

                    string dsuError = string.Empty;
                    if (response.message.Count > 0)
                        dsuError = response.message[0];

                    verificationMessageRef = dsuError;
                    return (response.IsSuccess, verificationMessageRef, exceptionMessageRef);
                }
            }
            catch (Exception ex)
            {
                exceptionMessageRef = ex.ToCompleteString();
                verificationMessageRef = "خطا در تصدیق اطلاعات خلاصه های ایجاد شده آماده ارسال. لطفاً مجدداً تلاش نمایید.";
                return (false, verificationMessageRef, exceptionMessageRef);
            }

        }

        #region Private Section
        private async Task<(bool, string)> SimulateRemovingDSUsSendingProcess(Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, string responseMessages)
        {
            string responseMessagesRef = responseMessages;
            try
            {
                //ConfigurationManager.TypeDefinitions.ClientConfiguration clientConfiguration = new ConfigurationManager.TypeDefinitions.ClientConfiguration();
                _notaryOfficeDSUEngineCore._DsuProcessActionType = ProcessActionType.DSUSimulation;

                #region ValidateDSURequirments
                //========================Verify Sequences And Compatiblity Checks==========================================================================================
                //1. Sequence Check
                //2. Compatiblity Check
                //3. Inquires Usage Count Check
                //4. Content Valiadtion To Original Data On Inquiry Response
                _estateInquiryValidator._theONotaryRegisterServiceReq = theCurrentRegisterServiceReq;
                RequirmentsValidationStatus validationStatus = _estateInquiryValidator.ValidateRemovingDSURequirments(theCurrentRegisterServiceReq, ref responseMessagesRef);

                if (validationStatus != RequirmentsValidationStatus.Succeeded)
                    return (false, responseMessagesRef);

                #endregion

                //=========================Collect DSUs============================================================================================
                #region Collect DSUs

                List<DSUDealSummaryObject> generatedDSUsCollection;
                (generatedDSUsCollection, responseMessagesRef) = await _notaryOfficeDSUEngineCore.CollectDSUDealSummaries(theCurrentRegisterServiceReq, cancellationToken, responseMessagesRef);
                if (!generatedDSUsCollection.Any())
                {
                    if (string.IsNullOrWhiteSpace(responseMessagesRef))
                        responseMessagesRef = "هیچ خلاصه معامله ای برای ارسال ایجاد نگردید.";

                    return (false, responseMessagesRef);
                }
                #endregion

                //==========================Log DSUs==============================================================================================================================
                DocumentEstateDealSummaryGenerated generatedDSULog = await _dsuLogger.LogGeneratedDSUCollection(generatedDSUsCollection, theCurrentRegisterServiceReq.Id.ToString(), cancellationToken, responseMessagesRef);

                if (generatedDSUsCollection.Any())
                    return (true, responseMessagesRef);
                else
                    return (false, responseMessagesRef);

            }
            catch (Exception ex)
            {
                if (string.IsNullOrWhiteSpace(responseMessagesRef))
                    responseMessagesRef = "خطا در آماده سازی بسته های خلاصه معامله!";

                return (false, responseMessagesRef);
            }
        }

        private async Task<(bool, string)> SimulateMainDSUsSendingProcess(Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, string responseMessages)
        {
            string responseMessagesRef = responseMessages;
            try
            {
                //.TypeDefinitions.ClientConfiguration clientConfiguration = new ConfigurationManager.TypeDefinitions.ClientConfiguration();

                //NotaryOfficeDSUEngineCore dsuEngineCore = new NotaryOfficeDSUEngineCore(InquiryManagerTypeDefinitions.ProcessActionType.DSUSimulation);
                _notaryOfficeDSUEngineCore._DsuProcessActionType = ProcessActionType.DSUSimulation;
                bool isRestricted = Mapper.IsONotaryDocumentRestrictedType(theCurrentRegisterServiceReq.DocumentTypeId);

                //========================Verify Sequences And Compatiblity Checks==========================================================================================
                //1. Sequence Check
                //2. Compatiblity Check
                //3. Inquires Usage Count Check
                //4. Content Valiadtion To Original Data On Inquiry Response
                #region ValidateDSURequirments

                _estateInquiryValidator._theONotaryRegisterServiceReq = theCurrentRegisterServiceReq;
                List<string> sentInquiriesCollection = new List<string>();
                RequirmentsValidationStatus validationStatus;
                (validationStatus, sentInquiriesCollection, responseMessagesRef) = await _estateInquiryValidator.ValidateDSURequirments(theCurrentRegisterServiceReq, cancellationToken, sentInquiriesCollection, responseMessagesRef);

                //OOOOOOOOOOPS! No Way! Stop And Recheck what is required :/ :/ :/ :\ :\ :\ :| :| :|
                if (validationStatus != RequirmentsValidationStatus.Succeeded)
                    return (false, responseMessagesRef);

                #endregion

                //=========================Collect DSUs============================================================================================
                #region Collect DSUs

                List<DSUDealSummaryObject> generatedDSUsCollection;
                (generatedDSUsCollection, responseMessagesRef) = await _notaryOfficeDSUEngineCore.CollectDSUDealSummaries(theCurrentRegisterServiceReq, cancellationToken, responseMessagesRef);
                if (!generatedDSUsCollection.Any())
                {
                    if (string.IsNullOrWhiteSpace(responseMessagesRef))
                        responseMessagesRef = "هیچ خلاصه معامله ای برای ارسال ایجاد نگردید.";

                    await _dsuLogger.LogVerificationMessages(theCurrentRegisterServiceReq.Id.ToString(), responseMessagesRef, cancellationToken);

                    return (false, responseMessagesRef);
                }
                #endregion

                bool isAllDsuSent = false;
                var foundGeneratedDealSummaryList =
                    await _documentEstateDealSummaryGeneratedRepository.GetAllAsync(t =>
                        t.Id == theCurrentRegisterServiceReq.Id, cancellationToken);

                foreach (DocumentEstateDealSummaryGenerated oneFoundGeneratedDealSummary in foundGeneratedDealSummaryList)
                {
                    if (oneFoundGeneratedDealSummary.IsSent == NotaryGeneratedDealSummary.Sent.GetString())
                        isAllDsuSent = true;
                    else
                        isAllDsuSent = false;
                }
                if (isAllDsuSent)
                    return (true, responseMessagesRef);

                //=========================Verify DSUs Count============================================================================================================================
                string exceptionMessages = null;
                bool isDSUCollectionValid;
                (isDSUCollectionValid, responseMessagesRef, exceptionMessages) = await this.VerifyGeneratedDSUsCollection(generatedDSUsCollection, isRestricted, ProcessActionType.DSUSimulation,
                    theCurrentRegisterServiceReq.DocumentTypeId, cancellationToken, responseMessagesRef, exceptionMessages);

                //==========================Log DSUs==============================================================================================================================                
                var generatedDSULog = await _dsuLogger.LogGeneratedDSUCollection(generatedDSUsCollection, theCurrentRegisterServiceReq.Id.ToString(), cancellationToken, responseMessagesRef + System.Environment.NewLine + exceptionMessages);

                if (isDSUCollectionValid)
                    return (true, responseMessagesRef);
                else
                    return (false, responseMessagesRef);
            }
            catch (Exception ex)
            {
                if (string.IsNullOrWhiteSpace(responseMessagesRef))
                    responseMessagesRef = "خطا در آماده سازی بسته های خلاصه معامله!";

                return (false, responseMessagesRef);
            }
        }

        //private async Task<bool,string> SimulatePreSellDSUsSendingProcess ( Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, string responseMessages1 )
        //{
        //    string responseMessagesRef = responseMessages;
        //    try
        //    {
        //       // ConfigurationManager.TypeDefinitions.ClientConfiguration clientConfiguration = new ConfigurationManager.TypeDefinitions.ClientConfiguration();

        //        #region ValidateDSURequirments
        //        EstateInquiryManager.EstateInquiryValidator validatorEngine = new EstateInquiryManager.EstateInquiryValidator(theCurrentRegisterServiceReq);
        //        List<string> sentInquiriesCollection = new List<string>();
        //        InquiryManagerTypeDefinitions.RequirmentsValidationStatus validationStatus = validatorEngine.ValidateDSURequirments(theCurrentRegisterServiceReq, ref sentInquiriesCollection, ref responseMessages);

        //        if ( validationStatus != InquiryManagerTypeDefinitions.RequirmentsValidationStatus.Succeeded )
        //            return false;
        //        return true;
        //        ///return true  سطر بالا مربوط به عدم چک شدن استعلام پیش فروش می باشد- 970911


        //        #endregion

        //        foreach ( DocumentInquiry theOneInquiry in theCurrentRegisterServiceReq.DocumentInquiries )
        //        {
        //            if ( string.IsNullOrEmpty ( theOneInquiry.EstateInquiriesId ) )
        //                continue;

        //            EstateInquiry estateInquiry =
        //                await _estateInquiryRepository.GetAsync(
        //                    t => t.EstateInquiryId == Guid.Parse(theOneInquiry.EstateInquiriesId) , cancellationToken); //Rad.CMS.InstanceBuilder.GetEntityById<IESTEstateInquiry>(theOneInquiry.ESTEstateInquiryId);
        //            if ( estateInquiry == null )
        //            {
        //                responseMessagesRef = "امکان ارسال خلاصه معامله برای استعلام با شماره " + theOneInquiry.ReplyNo + " وجود ندارد.";
        //                return false;
        //            }

        //            var prsEstateInquiry =
        //                await _estateInquiryPersonRepository.GetAllAsync(t => t.EstateInquiryId == estateInquiry.Id,
        //                    cancellationToken);


        //            if ( prsEstateInquiry == null || prsEstateInquiry.Count == 0 )
        //            {
        //                responseMessages = "امکان ارسال خلاصه معامله برای استعلام با شماره " + theOneInquiry.ReplyNo + " وجود ندارد.";
        //                return false;
        //            }

        //            PresellEstateObject newPresellEstate = new PresellEstateObject();
        //            newPresellEstate.CmsOrganizationId = "F4C02E98FD6D4F2A81098E4FE27FC5E6";// estateInquiry.ProducerInquiryId;
        //            newPresellEstate.CountUndergroundFloor = ( long? ) (  prsEstateInquiry.ElementAt(0). ).CountUndergroundFloor;
        //            newPresellEstate.CreditDeadline = (  prsEstateInquiry [ 0 ] ).CreditDeadline;
        //            newPresellEstate.DescriptionAddress = (  prsEstateInquiry [ 0 ] ).DescriptionAddress;
        //            newPresellEstate.EstateBlock = ( long? ) (  prsEstateInquiry [ 0 ] ).EstateBlock;
        //            newPresellEstate.EstEstateInquiryId = (  prsEstateInquiry [ 0 ] ).ESTEstateInquiryId;
        //            newPresellEstate.FacadesMaterials = (  prsEstateInquiry [ 0 ] ).FacadesMaterials;
        //            newPresellEstate.FireAlarmSystem = (  prsEstateInquiry [ 0 ] ).FireAlarmSystem;
        //            newPresellEstate.FloorAccessingSystem = (  prsEstateInquiry [ 0 ] ).FloorAccessingSystem;
        //            newPresellEstate.LicenseDate = (  prsEstateInquiry [ 0 ] ).LicenseDate;
        //            newPresellEstate.LicenseNumber = (  prsEstateInquiry [ 0 ] ).LicenseNumber;
        //            newPresellEstate.OriginalPlaque = (  prsEstateInquiry [ 0 ] ).OriginalPlaque;
        //            newPresellEstate.ReceiverCmsOrganization = estateInquiry.SelfInquiryId;
        //            newPresellEstate.SectionId = estateInquiry.SectionId;
        //            newPresellEstate.SidewayPlaque = (  prsEstateInquiry [ 0 ] ).SidewayPlaque;
        //            newPresellEstate.StructuralType = (  prsEstateInquiry [ 0 ] ).StructuralType;
        //            newPresellEstate.SubsectionId = estateInquiry.SubSectionId;
        //            newPresellEstate.TotalFloorOnGround = ( long? ) (  prsEstateInquiry [ 0 ] ).TotalFloorOnGround;
        //            newPresellEstate.TotalPiecesAnnexation = ( long? ) (  prsEstateInquiry [ 0 ] ).TotalPiecesAnnexation;
        //            newPresellEstate.TotalPiecesApartment = ( long? ) (  prsEstateInquiry [ 0 ] ).TotalPiecesApartment;
        //            newPresellEstate.TotalPiecesParking = ( long? ) (  prsEstateInquiry [ 0 ] ).TotalPiecesParking;
        //            newPresellEstate.TotalPiecesStoreroom = ( long? ) (  prsEstateInquiry [ 0 ] ).TotalPiecesStoreroom;

        //            PreSellServiceProxy.PresellInputMessage pieceRequest = new PreSellServiceProxy.PresellInputMessage();
        //            pieceRequest.InquiryId = estateInquiry.ObjectId;
        //            pieceRequest.UserName = "PresellDeal18Ssaa";
        //            pieceRequest.UserPass = "%SsaaTo1460&";
        //            PreSellServiceProxy.PresellOutputMessage pieceRequestResponsePacket = EstateInquiryManager.ServicesGateway.EstateServiceInvokerGateway.GetValidListForPresell(pieceRequest);

        //            newPresellEstate.PresellPieceObjects = pieceRequestResponsePacket.PresellEstate.PresellPieceObjects;

        //            PreSellServiceProxy.PresellSummaryObject[] summaryObject = new PreSellServiceProxy.PresellSummaryObject[1];
        //            summaryObject.Initialize ();
        //            summaryObject [ 0 ] = new PreSellServiceProxy.PresellSummaryObject ();
        //            summaryObject [ 0 ].DemandingTerminate = true;
        //            summaryObject [ 0 ].No = "1";
        //            summaryObject [ 0 ].PresellState = 1;
        //            summaryObject [ 0 ].ReceiverOrgId = estateInquiry.SelfInquiryId;
        //            summaryObject [ 0 ].RequestDate = Rad.CMS.BaseInfoContext.Instance.CurrentDate;
        //            summaryObject [ 0 ].SenderOrgId = estateInquiry.ProducerInquiryId;

        //            PreSellServiceProxy.PresellPersonObject[] personObject = new PreSellServiceProxy.PresellPersonObject[1];
        //            personObject.Initialize ();
        //            personObject [ 0 ] = new PreSellServiceProxy.PresellPersonObject ();
        //            personObject [ 0 ].Address = ( ( IONotaryDocPerson ) ( theCurrentRegisterServiceReq.TheONotaryDocPersonList [ 0 ] ) ).Address;
        //            personObject [ 0 ].CanAssignment = "1";
        //            personObject [ 0 ].DeliveryDate = Rad.CMS.BaseInfoContext.Instance.CurrentDate;
        //            if ( ( ( IONotaryDocPerson ) ( theCurrentRegisterServiceReq.TheONotaryDocPersonList [ 0 ] ) ).TheONotaryRegServicePersonType.IsMalek == Rad.CMS.Enumerations.YesNo.Yes )
        //                personObject [ 0 ].DsuRelationKindId = "100";    // معامل
        //            else
        //                personObject [ 0 ].DsuRelationKindId = "101";    // متعامل
        //            personObject [ 0 ].DsuTransferTypeId = "214FD154DF05468BAE26B25057F73E86";     // پیش فروش
        //            personObject [ 0 ].EnumPersonType = ( int ) ( ( IONotaryDocPerson ) ( theCurrentRegisterServiceReq.TheONotaryDocPersonList [ 0 ] ) ).PersonType;
        //            personObject [ 0 ].EnumSex = ( int ) ( ( IONotaryDocPerson ) ( theCurrentRegisterServiceReq.TheONotaryDocPersonList [ 0 ] ) ).SexType;
        //            personObject [ 0 ].FatherName = ( ( IONotaryDocPerson ) ( theCurrentRegisterServiceReq.TheONotaryDocPersonList [ 0 ] ) ).FatherName;
        //            personObject [ 0 ].IdentityNo = ( ( IONotaryDocPerson ) ( theCurrentRegisterServiceReq.TheONotaryDocPersonList [ 0 ] ) ).IdentityNo;
        //            personObject [ 0 ].Name = ( ( IONotaryDocPerson ) ( theCurrentRegisterServiceReq.TheONotaryDocPersonList [ 0 ] ) ).Name;
        //            personObject [ 0 ].NationalCode = ( ( IONotaryDocPerson ) ( theCurrentRegisterServiceReq.TheONotaryDocPersonList [ 0 ] ) ).NationalNo;
        //            personObject [ 0 ].PersonDescription = ( ( IONotaryDocPerson ) ( theCurrentRegisterServiceReq.TheONotaryDocPersonList [ 0 ] ) ).Description;
        //            personObject [ 0 ].PostalCode = long.Parse ( ( ( IONotaryDocPerson ) ( theCurrentRegisterServiceReq.TheONotaryDocPersonList [ 0 ] ) ).PostalCode );
        //            personObject [ 0 ].PrsPresellSummaryId = "-";
        //            personObject [ 0 ].RegisterDate = ( ( IONotaryDocPerson ) ( theCurrentRegisterServiceReq.TheONotaryDocPersonList [ 0 ] ) ).CompanyRegisterDate;
        //            personObject [ 0 ].Surname = "-";
        //            personObject [ 0 ].TheShareOf = 6;
        //            personObject [ 0 ].TheShareText = "-";

        //            summaryObject [ 0 ].PresellPersonObjects = personObject;

        //            newPresellEstate.PresellPieceObjects [ 0 ].PresellSummaryObjects = summaryObject;

        //            PreSellServiceProxy.PresellInputMessage validationRequest = new PreSellServiceProxy.PresellInputMessage();
        //            validationRequest.InquiryId = theOneInquiry.ESTEstateInquiryId;
        //            validationRequest.PresellEstate = newPresellEstate;
        //            validationRequest.UserName = "PresellDeal18Ssaa";
        //            validationRequest.UserPass = "%SsaaTo1460&";
        //            PreSellServiceProxy.PresellOutputMessage response = EstateInquiryManager.ServicesGateway.EstateServiceInvokerGateway.IsValidInquiryForPreSell(validationRequest);

        //            if ( !response.Result )
        //            {
        //                responseMessages = "امکان ارسال خلاصه معامله برای استعلام با شماره " + theOneInquiry.ReplyNo + " وجود ندارد.";

        //                foreach ( string oneMessage in response.ErrorMessageList )
        //                    responseMessages += "\n" + oneMessage;

        //                return false;
        //            }
        //        }

        //        return true;
        //    }
        //    catch ( Exception ex )
        //    {
        //        ZCommonUtility.LoggerHelper.LogError ( "SimulatePreSellDSUsSendingProcess", ex, theCurrentRegisterServiceReq.ObjectId );
        //        if ( string.IsNullOrWhiteSpace ( responseMessages ) )
        //            responseMessages = "خطا در امکان سنجی آماده سازی بسته های خلاصه معامله!";

        //        return false;
        //    }
        //}

        private List<DSUDealSummaryObject> AppendSignDateToDSUObjects(Document theCurrentRegisterServiceReq, List<DSUDealSummaryObject> dsuDealSummaryCollection)
        {
            if (dsuDealSummaryCollection == null || !dsuDealSummaryCollection.Any())
                return dsuDealSummaryCollection ?? new List<DSUDealSummaryObject>();
            foreach (DSUDealSummaryObject theOneDSUObject in dsuDealSummaryCollection)
                theOneDSUObject.SignDate = theCurrentRegisterServiceReq.SignDate;

            return dsuDealSummaryCollection;
        }

        private async Task<List<DSUDealSummaryObject>> CorrectInvalidDataOnDSUObjects(List<DSUDealSummaryObject> dsuDealSummaryCollection, CancellationToken cancellationToken)
        {

            if (dsuDealSummaryCollection == null || !dsuDealSummaryCollection.Any())
                return dsuDealSummaryCollection ?? new List<DSUDealSummaryObject>();
            List<DSUDealSummaryObject> dsuDealSummaryCollectionRef = dsuDealSummaryCollection;
            foreach (DSUDealSummaryObject theOneDSUObject in dsuDealSummaryCollectionRef)
            {
                #region Invalid PostalCode
                foreach (DSURealLegalPersonObject theOneDSUPerson in theOneDSUObject.TheDSURealLegalPersonList)
                {
                    if (!string.IsNullOrWhiteSpace(theOneDSUPerson.PostalCode) && !ValidatorsUtility.checkPostalCode(theOneDSUPerson.PostalCode))
                        theOneDSUPerson.PostalCode = "0000000000";
                }
                #endregion

                #region Invalid SeriDaftar
                if (theOneDSUObject.NotebookSeri.Length > 20)
                {
                    var mainESTEStateInquiry =
                        await _estateInquiryRepository.GetAsync(t => t.Id == Guid.Parse(theOneDSUObject.Id),
                            cancellationToken);//  Rad.CMS.InstanceBuilder.GetEntityById<Rad.ssaa.ssaaClass.IESTEstateInquiry>(theOneDSUObject.ESTEstateInquiryId, true);
                    theOneDSUObject.NotebookSeri = mainESTEStateInquiry.EstateSeridaftar.SsaaCode;
                }
                #endregion
            }

            return dsuDealSummaryCollectionRef;
        }

        private DealSummary FindEquivalantDSU(ICollection<DealSummary> theDSUsCollection, DSUDealSummaryObject theTargetDSUObject, ref string message)
        {
            foreach (DealSummary theOneDBDSU in theDSUsCollection)
            {
                if (theOneDBDSU.EstateInquiryId.ToString() == theTargetDSUObject.ESTEstateInquiryId)
                {
                    bool personsAreMatched = true;
                    foreach (DSURealLegalPersonObject theOneGenDSUPerson in theTargetDSUObject.TheDSURealLegalPersonList)
                    {
                        var theEquivalantDSUPerson = this.FindEquivalantDSUPerson(theOneDBDSU.DealSummaryPeople, theOneGenDSUPerson);
                        if (theEquivalantDSUPerson == null)
                        {
                            message = theOneGenDSUPerson.Name + " " + theOneGenDSUPerson.Family + "-" + theOneGenDSUPerson.NationalCode + "-" + "در خلاصه معامله های ارسال شده یافت نشد. ";
                            personsAreMatched = false;
                            break;
                        }
                    }

                    if (!personsAreMatched)
                        continue;
                    else
                        return theOneDBDSU;
                }
            }
            return null;
        }

        private DealSummaryPerson FindEquivalantDSUPerson(ICollection<DealSummaryPerson> theDSUPersons, DSURealLegalPersonObject theTargetDSUPerson)
        {
            foreach (DealSummaryPerson theOneDSUPerson in theDSUPersons)
            {
                if (!string.IsNullOrWhiteSpace(theTargetDSUPerson.NationalCode))
                    if (theOneDSUPerson.NationalityCode == theTargetDSUPerson.NationalCode)
                        return theOneDSUPerson;

                if (
                    theOneDSUPerson.Name.GetStandardFarsiString() == theTargetDSUPerson.Name.GetStandardFarsiString() &&
                    theOneDSUPerson.Family.GetStandardFarsiString() == theTargetDSUPerson.Family.GetStandardFarsiString()
                    )
                    return theOneDSUPerson;
            }


            return null;
        }

        //private List<DSUDealSummaryObject> GetDSUObjectsFromCPMS ( string nationalno, string signDate, ref string message )
        //{
        //    if ( string.IsNullOrWhiteSpace ( nationalno ) || ( !nationalno.IsDigit ( 18 ) && nationalno.Length != 20 ) )
        //        return null;

        //    if ( string.IsNullOrWhiteSpace ( signDate ) || signDate.Length < 10 || string.Compare ( signDate, "1392/06/26" ) < 0 )
        //        return null;

        //    Criteria criteria = new Criteria();
        //    criteria.AddEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.NationalNo, nationalno );
        //    criteria.AddEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.SignDate, signDate );
        //    criteria.AddEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.TheScriptorium.TheCMSOrganization.ObjectId, Rad.CMS.BaseInfoContext.Instance.CurrentCMSOrganization.ObjectId );
        //    criteria.AddEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.State, Rad.CMS.Enumerations.NotaryRegServiceReqState.Finalized );
        //    IONotaryRegisterServiceReq theRecentRelatedReq = Rad.CMS.InstanceBuilder.GetEntityByCriteria<IONotaryRegisterServiceReq>(criteria);

        //    if ( theRecentRelatedReq == null )
        //    {
        //        message = "سند تایید شده با شناسه یکتای  " + nationalno + " و تاریخ امضاء " + signDate + "یافت نشد. ";
        //        return null;
        //    }

        //    bool isRestricted = NotaryOfficeCommons.EstateInquiryManager.Mapper.IsONotaryDocumentRestrictedType(theRecentRelatedReq.TheONotaryDocumentType.Code);
        //    if ( !isRestricted )
        //    {
        //        message = "شناسه سند وارد شده مربوط سند از نوع رهنی نمی باشد. لطفاً در ورود اطلاعات دقت فرمایید.";
        //        return null;
        //    }

        //    EstateInquiryManager.InquiryManagerTypeDefinitions.DSUAquisitionRequestPacket dsuAquisitionRequestPacket = new InquiryManagerTypeDefinitions.DSUAquisitionRequestPacket();
        //    dsuAquisitionRequestPacket.OrganizationID = theRecentRelatedReq.TheScriptorium.TheCMSOrganization.ObjectId;
        //    dsuAquisitionRequestPacket.SignDate = signDate;
        //    dsuAquisitionRequestPacket.ClassifyNo = theRecentRelatedReq.ClassifyNo.ToString ();

        //    if ( theRecentRelatedReq.TheONotaryRegServiceInquiryList.CollectionHasElement () )
        //    {
        //        foreach ( IONotaryRegServiceInquiry theOneInquiry in theRecentRelatedReq.TheONotaryRegServiceInquiryList )
        //        {
        //            if ( string.IsNullOrWhiteSpace ( theOneInquiry.ESTEstateInquiryId ) )
        //                continue;

        //            if ( dsuAquisitionRequestPacket.InquiryIDsCollection == null )
        //                dsuAquisitionRequestPacket.InquiryIDsCollection = new List<string> ();

        //            if ( !dsuAquisitionRequestPacket.InquiryIDsCollection.Contains ( theOneInquiry.ESTEstateInquiryId ) )
        //                dsuAquisitionRequestPacket.InquiryIDsCollection.Add ( theOneInquiry.ESTEstateInquiryId );
        //        }
        //    }

        //    //Get DSUs By Proxy From CPMS then Pass To Client

        //    return null;
        //}
        #endregion
    }

}
