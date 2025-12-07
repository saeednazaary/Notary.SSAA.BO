using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Messaging;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core
{
    using Notary.SSAA.BO.Configuration;
    using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataCollectionResponsePacket = Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document.DataCollectionResponsePacket;
    using MessagingCore = Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Messaging.MessagingCore;

    /// <summary>
    /// Defines the <see cref="ValidatorGateway" />
    /// </summary>
    public class ValidatorGateway
    {
        /// <summary>
        /// Defines the documentRepository
        /// </summary>
        private readonly IDocumentRepository documentRepository;

        /// <summary>
        /// Defines the documentElectronicBookBaseInfoRepository
        /// </summary>
        private readonly IDocumentElectronicBookBaseInfoRepository documentElectronicBookBaseInfoRepository;

        /// <summary>
        /// Defines the documentTypeRepository
        /// </summary>
        private readonly IDocumentTypeRepository documentTypeRepository;

        /// <summary>
        /// Defines the centralizedValidator
        /// </summary>
        private readonly CentralizedValidator centralizedValidator;

        /// <summary>
        /// Defines the clientConfiguration
        /// </summary>
        private readonly ClientConfiguration clientConfiguration;

        /// <summary>
        /// Defines the smsController
        /// </summary>
        private readonly MessagingCore smsController;

        /// <summary>
        /// Defines the userService
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorGateway"/> class.
        /// </summary>
        /// <param name="_documentRepository">The _documentRepository<see cref="IDocumentRepository"/></param>
        /// <param name="_documentTypeRepository">The _documentTypeRepository<see cref="IDocumentTypeRepository"/></param>
        /// <param name="_userService">The _userService<see cref="IUserService"/></param>
        /// <param name="_documentElectronicBookBaseInfoRepository">The _documentElectronicBookBaseInfoRepository<see cref="IDocumentElectronicBookBaseInfoRepository"/></param>
        /// <param name="_centralizedValidator">The _centralizedValidator<see cref="CentralizedValidator"/></param>
        /// <param name="_clientConfiguration">The _clientConfiguration<see cref="ClientConfiguration"/></param>
        /// <param name="_smsController">The _smsController<see cref="MessagingCore"/></param>
        public ValidatorGateway ( IDocumentRepository _documentRepository, IDocumentTypeRepository _documentTypeRepository,
            IUserService _userService, IDocumentElectronicBookBaseInfoRepository _documentElectronicBookBaseInfoRepository,
            CentralizedValidator _centralizedValidator,
            ClientConfiguration _clientConfiguration, MessagingCore _smsController
            )
        {
            documentRepository = _documentRepository;
            documentElectronicBookBaseInfoRepository = _documentElectronicBookBaseInfoRepository;
            documentTypeRepository = _documentTypeRepository;
            userService = _userService;
            centralizedValidator = _centralizedValidator;
            clientConfiguration = _clientConfiguration;
            smsController = _smsController;
        }

        /// <summary>
        /// The ValidateAgentDocumentsCollection
        /// </summary>
        /// <param name="clientValidationRequests">The clientValidationRequests<see cref="List{DocAgentValidationRequestPacket}?"/></param>
        /// <param name="overalMessageText">The overalMessageText<see cref="string?"/></param>
        /// <param name="mainEntityID">The mainEntityID<see cref="string?"/></param>
        /// <param name="mainEntityDocumentTypeTitle">The mainEntityDocumentTypeTitle<see cref="string?"/></param>
        /// <returns>The <see cref="Task{(List{DocAgentValidationResponsPacket}?,string?)}"/></returns>
        public async Task<(List<DocAgentValidationResponsPacket>?, string?)> ValidateAgentDocumentsCollection ( List<DocAgentValidationRequestPacket>? clientValidationRequests, string? overalMessageText, string? mainEntityID = null, string? mainEntityDocumentTypeTitle = null )
        {
            List<AgentDocValidationResult> serviceResponsesCollection = new List<AgentDocValidationResult>{ };
            List<AgentDocValidationRequestPacket> serviceRequestsCollection =new List<AgentDocValidationRequestPacket>{ };
            List<DocAgentValidationResponsPacket>? clientResponseCollection = new List<DocAgentValidationResponsPacket>{ };
            bool requestPacketsAreValid;
            (requestPacketsAreValid, overalMessageText) = this.ValidateRequestsPackets ( clientValidationRequests, overalMessageText );
            if ( !requestPacketsAreValid )
                return (this.GenerateAgentDocErrorResponsePacket ( overalMessageText ), overalMessageText);


            try
            {
                if ( clientValidationRequests == null || clientValidationRequests.Count == 0 )
                    return (this.GenerateAgentDocErrorResponsePacket ( "بسته اطلاعات ورودی خالی می باشد و تصدیق اطلاعات سند وکالتنامه مربوطه امکان پذیر نمی باشد." ), overalMessageText);

                this.RemoveRedundantRequests ( ref clientValidationRequests );

                serviceRequestsCollection = Convertor.ConvertRequestsCollection ( clientValidationRequests );

                if (  serviceRequestsCollection.Count == 0 )
                    return (this.GenerateAgentDocErrorResponsePacket ( "بسته اطلاعات ورودی خالی می باشد و تصدیق اطلاعات سند وکالتنامه مربوطه امکان پذیر نمی باشد." ), overalMessageText);

                System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
                serviceResponsesCollection = await centralizedValidator.ValidateAgentDocumentsCollection ( serviceRequestsCollection );

                clientResponseCollection = Convertor.ConvertResponsesCollection ( serviceResponsesCollection, clientValidationRequests );

            }
            finally
            {


                if ( clientResponseCollection == null )
                    clientResponseCollection = this.GenerateAgentDocErrorResponsePacket ( "سرویس تصدیق اطلاعات وکالتنامه ها با خطای سیستمی مواجه گردید. لطفاً مجدداً تلاش نمایید." );
                else
                {
                    clientResponseCollection = await this.GenerateSMS ( clientResponseCollection, mainEntityID, mainEntityDocumentTypeTitle );
                }

                overalMessageText = this.GetOveralValidationMessageText ( clientResponseCollection );
            }

            return (clientResponseCollection, overalMessageText);
        }

        /// <summary>
        /// The ValidateRelatedDocument
        /// </summary>
        /// <param name="validationRequest">The validationRequest<see cref="RelatedDocumentValidationRequest"/></param>
        /// <param name="overalMessageText">The overalMessageText<see cref="string"/></param>
        /// <returns>The <see cref="Task{(RelatedDocumentValidationResponse,string)}"/></returns>
        public async Task<(RelatedDocumentValidationResponse, string)> ValidateRelatedDocument ( RelatedDocumentValidationRequest validationRequest, string overalMessageText )
        {

            RelatedDocumentValidationResponse? clientResponse = null;
            RelatedDocValidationResult? serviceResponse = null;
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = source.Token;

            try
            {
                if ( validationRequest == null )
                    return (this.GenerateRelatedDocErrorResponsePacket ( "بسته اطلاعات وروردی برای تصدیق سند وابسته خالی می باشد و امکان تصدیق اطلاعات سند وابسته وجود ندارد." ), overalMessageText);

                clientResponse = new RelatedDocumentValidationResponse ();
                var currentScriptoriumDigitalBookBaseInfos=await documentElectronicBookBaseInfoRepository.GetElectronicBooks(new List<string>{ },cancellationToken,userService.UserApplicationContext.BranchAccess.BranchCode );

                if ( currentScriptoriumDigitalBookBaseInfos == null || currentScriptoriumDigitalBookBaseInfos.Count == 0 )
                {
                    clientResponse.ValidationResult = false;
                    clientResponse.DigitalBookBaseInfoInitialized = false;
                    clientResponse.ValidationResponseMessage = "خطا در تصدیق اطلاعات سند وابسته. اطلاعات مربوط به فرم راه اندازی دفتر الکترونیک تکمیل نشده است.";

                    return (clientResponse, overalMessageText);
                }

                clientResponse.DigitalBookBaseInfoInitialized = true;

                if ( validationRequest.IsRelatedDocumentInSSAR != YesNo.Yes )
                {
                    Notary.SSAA.BO.Domain.Entities.DocumentType theCurrentDocumentType =  await documentTypeRepository.GetDocumentType (validationRequest.DocumentTypeID,cancellationToken);

                    if (
                        theCurrentDocumentType.IsSupportive == YesNo.Yes.GetString () &&
                        validationRequest.DocumentNationalNo.IsDigit () &&
                        long.Parse ( validationRequest.DocumentNationalNo ) > currentScriptoriumDigitalBookBaseInfos [ 0 ].LastClassifyNo
                        )
                    {
                        clientResponse.ValidationResult = false;
                        clientResponse.ValidationResponseMessage = "شماره سند وابسته در مورد اسناد سابق، نمی تواند بزرگتر از اولین شماره ترتیب دفترالکترونیک باشد. ";
                    }
                    else
                    {
                        clientResponse.ValidationResult = true;
                    }

                    return (clientResponse, overalMessageText);
                }
                else if ( userService.UserApplicationContext.BranchAccess.BranchCode == validationRequest.DocumentScriptoriumId )
                {
                    var documents=await documentRepository.GetDocuments(new List<string>{ },cancellationToken,nationalNoes:new List<string>{ validationRequest.DocumentNationalNo },state:"6",scriptoriumId:userService.UserApplicationContext.BranchAccess.BranchCode );
                    if ( documents == null || documents.Count == 0 )
                    {
                        clientResponse.ValidationResult = false;
                        clientResponse.ValidationResponseMessage = "سند وابسته یافت نشد.";

                        return (clientResponse, overalMessageText);
                    }
                    else
                    {
                        int? relatedDocClassifyNo = documents[0].ClassifyNo ;

                        if (
                            relatedDocClassifyNo > currentScriptoriumDigitalBookBaseInfos [ 0 ].LastClassifyNo &&
                            string.Compare ( validationRequest.DocumentDate, clientConfiguration.ENoteBookEnabledDate ) < 0
                            )
                        {
                            clientResponse.ValidationResult = false;
                            clientResponse.ValidationResponseMessage = "شماره سند وابسته در مورد اسناد سابق، نمی تواند بزرگتر از اولین شماره ترتیب دفترالکترونیک باشد. ";

                            return (clientResponse, overalMessageText);
                        }
                    }

                    clientResponse.RelatedDocumentObject = documents [ 0 ];
                    clientResponse.registerServiceReqObjectID = documents [ 0 ].Id.ToString ();
                    clientResponse.RelatedDocumentClassifyNo = documents [ 0 ].ClassifyNo?.ToString ();
                }

                System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
                serviceResponse = await centralizedValidator.ValidateRelatedDocument ( validationRequest.DocumentNationalNo, validationRequest.DocumentScriptoriumId, validationRequest.DocumentDate, validationRequest.DocumentTypeID );
            }
            finally
            {

                RelatedDocValidationRequest requestObject = Convertor.GenerateRelatedDocValidationRequest(validationRequest.DocumentNationalNo, validationRequest.DocumentScriptoriumId, validationRequest.DocumentDate, validationRequest.DocumentTypeID);

                if ( serviceResponse != null )
                {
                    clientResponse = Convertor.ConvertResponse ( clientResponse, serviceResponse );
                }
                else
                {
                    if ( clientResponse == null )
                        clientResponse = this.GenerateRelatedDocErrorResponsePacket ( "بروز خطای سیستمی در تصدیق اطلاعات سند وابسته. لطفاً مجدداً تلاش نمایید." );
                }

                if ( !string.IsNullOrWhiteSpace ( clientResponse.ValidationResponseMessage ) )
                    overalMessageText =
                        System.Environment.NewLine +
                        "هشدارهای مربوط به اسناد وابسته ذکر شده در بخش سایر اطلاعات: " +
                        System.Environment.NewLine +
                        clientResponse.ValidationResponseMessage;
            }

            return (clientResponse, overalMessageText);
        }

        /// <summary>
        /// The GetLegalText
        /// </summary>
        /// <param name="nationalNo">The nationalNo<see cref="string"/></param>
        /// <param name="secretCode">The secretCode<see cref="string"/></param>
        /// <param name="includeRegCases">The includeRegCases<see cref="bool"/></param>
        /// <returns>The <see cref="Task{DataCollectionResponse}"/></returns>
        public async Task<DataCollectionResponse> GetLegalText ( string nationalNo, string secretCode, bool includeRegCases = true )
        {
            DataCollectionResponse serviceResponse = new DataCollectionResponse();
            if ( !string.IsNullOrWhiteSpace ( nationalNo ) )
            {
                if ( nationalNo.Substring ( 4, 1 ) != "3" )
                {
                    serviceResponse.Succeeded = false;
                    serviceResponse.ServiceMessage = "شناسه وارد شده مربوط به وکالتنامه نمی باشد. لطفاً شناسه وکالتنامه را وارد نمایید.";
                    return serviceResponse;
                }
            }

            try
            {
                var dataCollectionResponsePacket =
                    await centralizedValidator.CollectData(new DataCollectionRequestPacket
                    {
                        DocumentNationalNo = nationalNo,
                        DocumentSecretCode = secretCode,
                        IncludeRegCaseDescription = includeRegCases
                    }) ?? new DataCollectionResponsePacket();

                serviceResponse.Succeeded = dataCollectionResponsePacket.Succeeded;

                if (serviceResponse.Succeeded)
                {
                    serviceResponse.LegalText = dataCollectionResponsePacket.Context;
                }
                else
                {
                    serviceResponse.ServiceMessage = dataCollectionResponsePacket.ServiceResponse;
                }
            }
            catch ( Exception )
            {
                serviceResponse.Succeeded = false;
                serviceResponse.ServiceMessage = "خطا در دریافت متن حقوقی از سرور مقصد!";
            }

            return serviceResponse;
        }

        /// <summary>
        /// The CollectSMSData
        /// </summary>
        /// <param name="nationalNo">The nationalNo<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{SMSRecipient}?}"/></returns>
        public async Task<List<SMSRecipient>?> CollectSMSData ( string nationalNo )
        {
            List<SMSRecipientPacket>? serviceResponseCollection = await centralizedValidator.CollectSMSData(nationalNo);

            List<SMSRecipient>? clientResponseCollection = Convertor.ConvertResponsesCollection(serviceResponseCollection);

            return clientResponseCollection;
        }

        /// <summary>
        /// The GetOveralValidationMessageText
        /// </summary>
        /// <param name="clientResponses">The clientResponses<see cref="List{DocAgentValidationResponsPacket}?"/></param>
        /// <returns>The <see cref="string?"/></returns>
        private string? GetOveralValidationMessageText ( List<DocAgentValidationResponsPacket>? clientResponses )
        {
            if ( clientResponses == null || clientResponses.Count == 0 )
                return null;
            string overalMessage = string.Empty;

            foreach ( DocAgentValidationResponsPacket theOneResponse in clientResponses )
            {
                if ( !string.IsNullOrWhiteSpace ( theOneResponse.ResponseMessage ) )
                {
                    if ( !string.IsNullOrWhiteSpace ( overalMessage ) && overalMessage.Trim ().Contains ( theOneResponse.ResponseMessage.Trim () ) )
                        continue;

                    if ( !string.IsNullOrWhiteSpace ( overalMessage ) )
                        overalMessage += System.Environment.NewLine;

                    overalMessage += theOneResponse.ResponseMessage;
                }
            }

            if ( !string.IsNullOrWhiteSpace ( overalMessage ) )
                overalMessage = System.Environment.NewLine + "هشدارهای مربوط به وکالتنامه مورد استناد در بخش اشخاص: " + System.Environment.NewLine + overalMessage;

            this.RemoveRedundantMessages ( ref overalMessage );

            return overalMessage;
        }

        /// <summary>
        /// The RemoveRedundantRequests
        /// </summary>
        /// <param name="clientValidationRequests">The clientValidationRequests<see cref="List{DocAgentValidationRequestPacket}"/></param>
        private void RemoveRedundantRequests ( ref List<DocAgentValidationRequestPacket> clientValidationRequests )
        {
            foreach ( DocAgentValidationRequestPacket theOneRequest in clientValidationRequests )
            {
                bool isUnit = this.isCurrentRequestDefinedUnit(clientValidationRequests, theOneRequest);
                if ( !isUnit )
                {
                    clientValidationRequests.Remove ( theOneRequest );
                    this.RemoveRedundantRequests ( ref clientValidationRequests );
                    break;
                }
            }
        }

        /// <summary>
        /// The RemoveRedundantMessages
        /// </summary>
        /// <param name="overalMessages">The overalMessages<see cref="string"/></param>
        private void RemoveRedundantMessages ( ref string overalMessages )
        {
            string[] splittedMessage = overalMessages.Split('\n');
            List<string> messagesList = new List<string>();
            foreach ( string theOneMessage in splittedMessage )
            {
                if ( !messagesList.Contains ( theOneMessage ) )
                    messagesList.Add ( theOneMessage );
            }

            StringBuilder sb = new StringBuilder();
            foreach ( string theOneMessage in messagesList )
                sb.AppendLine ( theOneMessage );

            overalMessages = sb.ToString ();
        }

        /// <summary>
        /// The isCurrentRequestDefinedUnit
        /// </summary>
        /// <param name="clientValidationRequests">The clientValidationRequests<see cref="List{DocAgentValidationRequestPacket}"/></param>
        /// <param name="currentRequest">The currentRequest<see cref="DocAgentValidationRequestPacket"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool isCurrentRequestDefinedUnit ( List<DocAgentValidationRequestPacket> clientValidationRequests, DocAgentValidationRequestPacket currentRequest )
        {
            List<DocAgentValidationRequestPacket> selectedReqs = clientValidationRequests.Where(q =>
                            q.DocumentNationalNo == currentRequest.DocumentNationalNo &&
                            q.DocumentScriptoriumId == currentRequest.DocumentScriptoriumId &&
                            q.DocumentDate == currentRequest.DocumentDate &&
                            q.DocumentTypeID == currentRequest.DocumentTypeID &&
                            q.MovakelFullName == currentRequest.MovakelFullName &&
                            q.MovakelNationalNo == currentRequest.MovakelNationalNo &&
                            q.VakilFullName == currentRequest.VakilFullName &&
                            q.VakilNationalNo == currentRequest.VakilNationalNo &&
                            q.CurrentRegCasesCollection == currentRequest.CurrentRegCasesCollection
                            ).ToList();

            if ( selectedReqs.Count > 1 )
                return false;
            else
                return true;
        }

        /// <summary>
        /// The GenerateAgentDocErrorResponsePacket
        /// </summary>
        /// <param name="message">The message<see cref="string?"/></param>
        /// <returns>The <see cref="List{DocAgentValidationResponsPacket}"/></returns>
        private List<DocAgentValidationResponsPacket> GenerateAgentDocErrorResponsePacket ( string? message = null )
        {
            if ( string.IsNullOrWhiteSpace ( message ) )
                message = "خطا در تصدیق اطلاعات وارد شده مربوط به سند وابسته مورد استفاده!";

            List<DocAgentValidationResponsPacket> mainResponse = new List<DocAgentValidationResponsPacket>()
            {
                new DocAgentValidationResponsPacket(){ Response = false, ResponseMessage = message}
            };

            return mainResponse;
        }

        /// <summary>
        /// The GenerateRelatedDocErrorResponsePacket
        /// </summary>
        /// <param name="message">The message<see cref="string?"/></param>
        /// <returns>The <see cref="RelatedDocumentValidationResponse"/></returns>
        private RelatedDocumentValidationResponse GenerateRelatedDocErrorResponsePacket ( string? message = null )
        {
            if ( string.IsNullOrWhiteSpace ( message ) )
                message = "خطا در تصدیق اطلاعات وارد شده مربوط به سند وابسته مورد استفاده!";

            RelatedDocumentValidationResponse mainResponse = new RelatedDocumentValidationResponse()
            {
                ValidationResult = false,
                ValidationResponseMessage = message
            };

            return mainResponse;
        }

        /// <summary>
        /// The ValidateRequestsPackets
        /// </summary>
        /// <param name="clientValidationRequests">The clientValidationRequests<see cref="List{DocAgentValidationRequestPacket}?"/></param>
        /// <param name="message">The message<see cref="string?"/></param>
        /// <returns>The <see cref="(bool,string?)"/></returns>
        private (bool, string?) ValidateRequestsPackets ( List<DocAgentValidationRequestPacket>? clientValidationRequests, string? message )
        {
            bool isValid = true;

            if ( clientValidationRequests == null || clientValidationRequests.Count == 0 )
            {
                message = "بسته اطلاعات ورودی خالی می باشد و تصدیق اطلاعات سند وکالتنامه مربوطه امکان پذیر نمی باشد.";
                return (false, message);
            }

            foreach ( DocAgentValidationRequestPacket theOneRequest in clientValidationRequests )
            {
                if ( string.IsNullOrWhiteSpace ( theOneRequest.DocumentNationalNo ) )
                {
                    message =
                        "در فهرست اشخاص وابسته شماره شناسه سند وکالتنامه مورد استناد وارد نشده است" +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "جزئیات خطا : " +
                        System.Environment.NewLine +
                        " - وکیل : " + theOneRequest.VakilFullName +
                        System.Environment.NewLine +
                        " - موکل : " + theOneRequest.MovakelFullName +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "**لطفاً اطلاعات را بازبینی نموده و مجدداً تلاش نمایید**";

                    return (false, message);
                }

                if ( string.IsNullOrWhiteSpace ( theOneRequest.DocumentScriptoriumId ) )
                {
                    message =
                        "در فهرست اشخاص وابسته دفترخانه صادر کننده سند وکالتنامه مورد استناد مشخص نشده است" +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "جزئیات خطا : " +
                        System.Environment.NewLine +
                        " - وکیل : " + theOneRequest.VakilFullName +
                        System.Environment.NewLine +
                        " - موکل : " + theOneRequest.MovakelFullName +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "**لطفاً اطلاعات را بازبینی نموده و مجدداً تلاش نمایید**";
                    return (false, message);
                }

                if ( string.IsNullOrWhiteSpace ( theOneRequest.DocumentDate ) )
                {
                    message =
                        "در فهرست اشخاص وابسته تاریخ سند وکالتنامه مورد استناد وارد نشده است" +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "جزئیات خطا : " +
                        System.Environment.NewLine +
                        " - وکیل : " + theOneRequest.VakilFullName +
                        System.Environment.NewLine +
                        " - موکل : " + theOneRequest.MovakelFullName +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "**لطفاً اطلاعات را بازبینی نموده و مجدداً تلاش نمایید**";
                    return (false, message);
                }
            }

            return (isValid, message);
        }

        /// <summary>
        /// The GenerateSMS
        /// </summary>
        /// <param name="clientResponseCollection">The clientResponseCollection<see cref="List{DocAgentValidationResponsPacket}"/></param>
        /// <param name="mainEntityID">The mainEntityID<see cref="string?"/></param>
        /// <param name="mainEntityDocumentTitle">The mainEntityDocumentTitle<see cref="string?"/></param>
        /// <returns>The <see cref="Task{List{DocAgentValidationResponsPacket}?}"/></returns>
        private async Task<List<DocAgentValidationResponsPacket>?> GenerateSMS ( List<DocAgentValidationResponsPacket> clientResponseCollection, string? mainEntityID, string? mainEntityDocumentTitle )
        {
            if ( clientResponseCollection == null || clientResponseCollection.Count == 0 )
                return clientResponseCollection;
            try
            {

                foreach ( DocAgentValidationResponsPacket theOneResponse in clientResponseCollection )
                {
                    if ( theOneResponse.SMSRecipientsCollection == null || theOneResponse.SMSRecipientsCollection.Count == 0 )
                        continue;

                    if ( theOneResponse.RequestPacket.SMSIsRequired )
                    {
                        // Rad.BaseInfo.SystemConfiguration.ICMSOrganization currentOrganization = Rad.CMS.InstanceBuilder.GetEntityById<Rad.BaseInfo.SystemConfiguration.ICMSOrganization>(Rad.CMS.BaseInfoContext.Instance.CurrentCMSOrganization.ObjectId);
                        var organizationName=userService.UserApplicationContext.BranchAccess.BranchName;
                        List<MessageOutputPacket> smsSentStatusCollection =await this.SendAgentDocValidationMechanizedSMS(theOneResponse.SMSRecipientsCollection, mainEntityID, mainEntityDocumentTitle, organizationName, theOneResponse.RequestPacket);

                        if ( smsSentStatusCollection.Count == 0 )
                            continue;

                        foreach ( MessageOutputPacket theOneSMSStatus in smsSentStatusCollection )
                        {
                            if ( !string.IsNullOrWhiteSpace ( theOneResponse.SMSObjectID ) )
                                theOneResponse.SMSObjectID += ",";

                            if ( theOneSMSStatus.TrmSMSIDCollection != null )
                                theOneResponse.SMSObjectID += theOneSMSStatus.TrmSMSIDCollection [ 0 ];
                        }
                    }
                }
                return clientResponseCollection;
            }
            catch
            {
                return clientResponseCollection;
            }
        }

        /// <summary>
        /// The SendAgentDocValidationMechanizedSMS
        /// </summary>
        /// <param name="recipientsCollection">The recipientsCollection<see cref="List{SMSRecipient}"/></param>
        /// <param name="mainEntityID">The mainEntityID<see cref="string?"/></param>
        /// <param name="documentTypeTitle">The documentTypeTitle<see cref="string?"/></param>
        /// <param name="organizationName">The organizationName<see cref="string?"/></param>
        /// <param name="mainValidationRequest">The mainValidationRequest<see cref="DocAgentValidationRequestPacket?"/></param>
        /// <returns>The <see cref="Task{ List{MessageOutputPacket}}"/></returns>
        private async Task<List<MessageOutputPacket>> SendAgentDocValidationMechanizedSMS ( List<SMSRecipient> recipientsCollection, string? mainEntityID, string? documentTypeTitle, string? organizationName = null, DocAgentValidationRequestPacket? mainValidationRequest = null )
        {
            List<MessageOutputPacket> smsStatusCollection = new List<MessageOutputPacket>();
            if ( organizationName == null )
                //currentOrganization = Rad.CMS.InstanceBuilder.GetEntityById<Rad.BaseInfo.SystemConfiguration.ICMSOrganization> ( Rad.CMS.BaseInfoContext.Instance.CurrentCMSOrganization.ObjectId );
                organizationName = userService.UserApplicationContext.BranchAccess.BranchName;

            if ( organizationName == null )
                return smsStatusCollection;

            if ( recipientsCollection == null || recipientsCollection.Count == 0 )
                return smsStatusCollection;

            foreach ( SMSRecipient theOneRecipient in recipientsCollection )
            {
                MessageInputPacket smsRequest = new MessageInputPacket();

                smsRequest.RecipientFullName = theOneRecipient.RecipientFullName;
                smsRequest.RecipientPhoneNo = theOneRecipient.RecipientMobileNo;
                smsRequest.MainEntityObjectID = mainEntityID;
                smsRequest.DocumentTypeTitle = documentTypeTitle;
                smsRequest.ScriptoriumFullName = organizationName;
                smsRequest.DocNationalNo = mainValidationRequest?.DocumentNationalNo;

                MessageOutputPacket? smsStatusPacket =await  smsController.CreateSMS(SMSUsageContext.AgentDocument, smsRequest, false, false);
                if ( smsStatusPacket != null )
                    smsStatusCollection.Add ( smsStatusPacket );
            }

            return smsStatusCollection;
        }
    }
}
