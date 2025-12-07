namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core
{
    using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using System.Collections.Generic;
    using SMSRecipient = Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument.SMSRecipient;

    /// <summary>
    /// Defines the <see cref="Convertor" />
    /// </summary>
    internal static class Convertor
    {
        /// <summary>
        /// The ConvertResponsesCollection
        /// </summary>
        /// <param name="serviceResponseCollection">The serviceResponseCollection<see cref="List{SMSRecipientPacket}?"/></param>
        /// <returns>The <see cref="List{SMSRecipient}?"/></returns>
        internal static List<SMSRecipient>? ConvertResponsesCollection ( List<SMSRecipientPacket>? serviceResponseCollection )
        {
            if ( serviceResponseCollection == null || serviceResponseCollection.Count == 0 )
                return null;

            List<SMSRecipient> clientResponseCollection = new List<SMSRecipient>();
            foreach ( SMSRecipientPacket theOneServiceResponse in serviceResponseCollection )
            {
                SMSRecipient smsRecipient = new SMSRecipient();
                smsRecipient.RecipientMobileNo = theOneServiceResponse.RecipientMobileNo;
                smsRecipient.RecipientFullName = theOneServiceResponse.RecipientFullName;
                smsRecipient.PersonTypeCode = theOneServiceResponse.RecipientPersonTypeCode;

                clientResponseCollection.Add ( smsRecipient );
            }

            return clientResponseCollection;
        }

        /// <summary>
        /// The ConvertResponsesCollection
        /// </summary>
        /// <param name="serviceResponseCollection">The serviceResponseCollection<see cref="List{AgentDocValidationResult}"/></param>
        /// <param name="clientRequestsCollection">The clientRequestsCollection<see cref="List{DocAgentValidationRequestPacket}"/></param>
        /// <returns>The <see cref="List{DocAgentValidationResponsPacket}"/></returns>
        internal static List<DocAgentValidationResponsPacket> ConvertResponsesCollection ( List<AgentDocValidationResult> serviceResponseCollection, List<DocAgentValidationRequestPacket> clientRequestsCollection )
        {
            List<DocAgentValidationResponsPacket> clientResponseCollection = new List<DocAgentValidationResponsPacket>{ };

            if ( serviceResponseCollection != null && serviceResponseCollection.Count > 0 )
                foreach ( AgentDocValidationResult theOneServiceResponse in serviceResponseCollection )
                {


                    DocAgentValidationRequestPacket? correspondingRequest = FindCorrespondingRequest(theOneServiceResponse.ValidationRequestID, clientRequestsCollection);

                    DocAgentValidationResponsPacket theOneClientResponse = new DocAgentValidationResponsPacket(correspondingRequest);

                    //theOneClientResponse.RequestPacket = correspondingRequest;

                    theOneClientResponse.CurrentDocAgentObjectID = theOneServiceResponse.CurrentDocAgentObjectID;
                    theOneClientResponse.CorrespondingRegisterServiceReqObjectID = theOneServiceResponse.RegisterServiceReqID;
                    theOneClientResponse.ResponseMessage = theOneServiceResponse.MessageText;
                    theOneClientResponse.ResponseExceptionMessages = theOneServiceResponse.ExceptionMessageText;

                    if ( theOneServiceResponse.SMSRecipientPacketCollection != null && theOneServiceResponse.SMSRecipientPacketCollection.Count > 0 )
                    {
                        theOneClientResponse.SMSRecipientsCollection = new List<SMSRecipient> ();
                        foreach ( SMSRecipientPacket theOneServiceSMSPacket in theOneServiceResponse.SMSRecipientPacketCollection )
                        {
                            SMSRecipient theOneSMSRecipient = new SMSRecipient();

                            theOneSMSRecipient.RecipientFullName = theOneServiceSMSPacket.RecipientFullName;
                            theOneSMSRecipient.RecipientMobileNo = theOneServiceSMSPacket.RecipientMobileNo;

                            theOneClientResponse.SMSRecipientsCollection.Add ( theOneSMSRecipient );
                        }
                    }

                    switch ( theOneServiceResponse.ResultRestrictionLevel )
                    {
                        case RestrictionLevel.Warning:
                            theOneClientResponse.Response = true;
                            break;
                        case RestrictionLevel.Avoidance:
                            theOneClientResponse.Response = false;
                            break;
                        case RestrictionLevel.Pass:
                            theOneClientResponse.Response = true;
                            break;
                    }

                    clientResponseCollection.Add ( theOneClientResponse );
                }

            return clientResponseCollection;
        }

        /// <summary>
        /// The ConvertRequestsCollection
        /// </summary>
        /// <param name="clientRequestsCollection">The clientRequestsCollection<see cref="List{DocAgentValidationRequestPacket}"/></param>
        /// <returns>The <see cref="List{AgentDocValidationRequestPacket}"/></returns>
        internal static List<AgentDocValidationRequestPacket> ConvertRequestsCollection ( List<DocAgentValidationRequestPacket> clientRequestsCollection )
        {
            List<AgentDocValidationRequestPacket> serviceRequestsCollection = new List<AgentDocValidationRequestPacket>{ };

            if ( clientRequestsCollection != null && clientRequestsCollection.Count > 0 )
                foreach ( DocAgentValidationRequestPacket theOneClientRequest in clientRequestsCollection )
                {


                    AgentDocValidationRequestPacket theOneServiceRequest = new AgentDocValidationRequestPacket();

                    theOneServiceRequest.ValidationRequestID = theOneClientRequest.ID;
                    theOneServiceRequest.CurrentDocAgentObjectID = theOneClientRequest.CurrentDocAgentObjectID;

                    theOneServiceRequest.AgentDocumentNationalNo = theOneClientRequest.DocumentNationalNo;
                    theOneServiceRequest.AgentDocumentDate = theOneClientRequest.DocumentDate;
                    theOneServiceRequest.AgentDocumentScriptoriumId = theOneClientRequest.DocumentScriptoriumId;
                    theOneServiceRequest.DocumentTypeID = theOneClientRequest.DocumentTypeID;

                    theOneServiceRequest.VakilNationalNo = theOneClientRequest.VakilNationalNo;
                    theOneServiceRequest.VakilFullName = theOneClientRequest.VakilFullName;
                    theOneServiceRequest.MovakelNationalNo = theOneClientRequest.MovakelNationalNo;
                    theOneServiceRequest.MovakelFullName = theOneClientRequest.MovakelFullName;

                    theOneServiceRequest.SMSIsRequired = theOneClientRequest.SMSIsRequired;
                    theOneServiceRequest.MobileNo4SMS = theOneClientRequest.MobileNo4SMS;
                    if ( theOneClientRequest.CurrentRegVehiclesCollection != null && theOneClientRequest.CurrentRegVehiclesCollection.Count > 0 )
                    {
                        theOneServiceRequest.RegCasesCollection = ConvertRegVehiclesCollection ( theOneClientRequest.CurrentRegVehiclesCollection );

                    }
                    if ( theOneClientRequest.CurrentRegEstateCollection != null && theOneClientRequest.CurrentRegEstateCollection.Count > 0 )
                    {
                        theOneServiceRequest.RegCasesCollection = ConvertRegEstatesCollection ( theOneClientRequest.CurrentRegEstateCollection );

                    }

                    serviceRequestsCollection.Add ( theOneServiceRequest );
                }

            return serviceRequestsCollection;
        }

        /// <summary>
        /// The ConvertServiceRequestToClientRequest
        /// </summary>
        /// <param name="serviceRequest">The serviceRequest<see cref="AgentDocValidationRequestPacket"/></param>
        /// <returns>The <see cref="DocAgentValidationRequestPacket?"/></returns>
        internal static DocAgentValidationRequestPacket? ConvertServiceRequestToClientRequest ( AgentDocValidationRequestPacket serviceRequest )
        {
            if ( serviceRequest == null )
                return null;

            DocAgentValidationRequestPacket clientRequest = new DocAgentValidationRequestPacket();

            clientRequest.CurrentDocAgentObjectID = serviceRequest.CurrentDocAgentObjectID;
            clientRequest.DocumentDate = serviceRequest.AgentDocumentDate;
            clientRequest.DocumentNationalNo = serviceRequest.AgentDocumentNationalNo;
            clientRequest.DocumentScriptoriumId = serviceRequest.AgentDocumentScriptoriumId;
            clientRequest.DocumentTypeID = serviceRequest.DocumentTypeID;
            clientRequest.MobileNo4SMS = serviceRequest.MobileNo4SMS;
            clientRequest.MovakelFullName = serviceRequest.MovakelFullName;
            clientRequest.MovakelNationalNo = serviceRequest.MovakelNationalNo;
            clientRequest.SMSIsRequired = serviceRequest.SMSIsRequired;
            clientRequest.VakilFullName = serviceRequest.VakilFullName;
            clientRequest.VakilNationalNo = serviceRequest.VakilNationalNo;

            return clientRequest;
        }

        /// <summary>
        /// The ConvertResponse
        /// </summary>
        /// <param name="clientResponse">The clientResponse<see cref="RelatedDocumentValidationResponse?"/></param>
        /// <param name="serviceResponse">The serviceResponse<see cref="RelatedDocValidationResult"/></param>
        /// <returns>The <see cref="RelatedDocumentValidationResponse"/></returns>
        internal static RelatedDocumentValidationResponse ConvertResponse ( RelatedDocumentValidationResponse? clientResponse, RelatedDocValidationResult serviceResponse )
        {
            if ( clientResponse == null )
                clientResponse = new RelatedDocumentValidationResponse ();

            clientResponse.registerServiceReqObjectID = serviceResponse.RelatedRegisterServiceReqObjectID;
            clientResponse.ValidationResult = serviceResponse.IsValid;
            clientResponse.ValidationResponseMessage = serviceResponse.ValidationMessage;

            return clientResponse;
        }

        /// <summary>
        /// The GenerateRelatedDocValidationRequest
        /// </summary>
        /// <param name="documentNationalNo">The documentNationalNo<see cref="string"/></param>
        /// <param name="documentScriptoriumId">The documentScriptoriumId<see cref="string"/></param>
        /// <param name="documentDate">The documentDate<see cref="string"/></param>
        /// <param name="documentTypeID">The documentTypeID<see cref="string"/></param>
        /// <returns>The <see cref="RelatedDocValidationRequest"/></returns>
        internal static RelatedDocValidationRequest GenerateRelatedDocValidationRequest ( string documentNationalNo, string documentScriptoriumId, string documentDate, string documentTypeID )
        {
            RelatedDocValidationRequest request = new RelatedDocValidationRequest();

            request.DocumentNationalNo = documentNationalNo;
            request.DocumentScriptoriumId = documentScriptoriumId;
            request.DocumentDate = documentDate;
            request.DocumentTypeID = documentTypeID;

            return request;
        }

        /// <summary>
        /// The ConvertRegEstatesCollection
        /// </summary>
        /// <param name="inputRegCaseEntityCollection">The inputRegCaseEntityCollection<see cref="IList{Notary.SSAA.BO.Domain.Entities.DocumentEstate}"/></param>
        /// <returns>The <see cref="List{RegCasePacket}?"/></returns>
        private static List<RegCasePacket>? ConvertRegEstatesCollection ( IList<Notary.SSAA.BO.Domain.Entities.DocumentEstate> inputRegCaseEntityCollection )
        {
            if ( inputRegCaseEntityCollection == null || inputRegCaseEntityCollection.Count == 0 )
                return null;

            List<RegCasePacket> regCaseServiceContractCollection = new List<RegCasePacket>();
            foreach ( Notary.SSAA.BO.Domain.Entities.DocumentEstate theOneONotaryRegCaseEntity in inputRegCaseEntityCollection )
            {
                RegCasePacket serviceDataContractRegCasePacket = new RegCasePacket();

                serviceDataContractRegCasePacket.BasicPlaqueNo = theOneONotaryRegCaseEntity.BasicPlaque;
                serviceDataContractRegCasePacket.SecondaryPlaqueNo = theOneONotaryRegCaseEntity.SecondaryPlaque;

                regCaseServiceContractCollection.Add ( serviceDataContractRegCasePacket );
            }

            return regCaseServiceContractCollection;
        }

        /// <summary>
        /// The ConvertRegVehiclesCollection
        /// </summary>
        /// <param name="inputRegCaseEntityCollection">The inputRegCaseEntityCollection<see cref="IList{Notary.SSAA.BO.Domain.Entities.DocumentVehicle}"/></param>
        /// <returns>The <see cref="List{RegCasePacket}?"/></returns>
        private static List<RegCasePacket>? ConvertRegVehiclesCollection ( IList<Notary.SSAA.BO.Domain.Entities.DocumentVehicle> inputRegCaseEntityCollection )
        {
            if ( inputRegCaseEntityCollection == null || inputRegCaseEntityCollection.Count == 0 )
                return null;

            List<RegCasePacket> regCaseServiceContractCollection = new List<RegCasePacket>();
            foreach ( Notary.SSAA.BO.Domain.Entities.DocumentVehicle theOneONotaryRegCaseEntity in inputRegCaseEntityCollection )
            {
                RegCasePacket serviceDataContractRegCasePacket = new RegCasePacket();

                serviceDataContractRegCasePacket.vehicleChassisNo = theOneONotaryRegCaseEntity.ChassisNo;
                serviceDataContractRegCasePacket.VehicleEngineNo = theOneONotaryRegCaseEntity.EngineNo;

                regCaseServiceContractCollection.Add ( serviceDataContractRegCasePacket );
            }

            return regCaseServiceContractCollection;
        }

        /// <summary>
        /// The FindCorrespondingRequest
        /// </summary>
        /// <param name="requestID">The requestID<see cref="string"/></param>
        /// <param name="requestsCollection">The requestsCollection<see cref="List{DocAgentValidationRequestPacket}"/></param>
        /// <returns>The <see cref="DocAgentValidationRequestPacket?"/></returns>
        private static DocAgentValidationRequestPacket? FindCorrespondingRequest ( string requestID, List<DocAgentValidationRequestPacket> requestsCollection )
        {
            if ( requestsCollection == null || requestsCollection.Count == 0 )
                return null;

            foreach ( DocAgentValidationRequestPacket theOneRequest in requestsCollection )
            {
                if ( theOneRequest.ID == requestID )
                    return theOneRequest;
            }

            return null;
        }
    }

    /// <summary>
    /// Defines the <see cref="RelatedDocValidationRequest" />
    /// </summary>
    public class RelatedDocValidationRequest
    {
        /// <summary>
        /// Defines the _DocumentNationalNo
        /// </summary>
        private string? _DocumentNationalNo;

        /// <summary>
        /// Defines the _DocumentScriptoriumId
        /// </summary>
        private string? _DocumentScriptoriumId;

        /// <summary>
        /// Defines the _DocumentDate
        /// </summary>
        private string? _DocumentDate;

        /// <summary>
        /// Defines the _DocumentTypeID
        /// </summary>
        private string? _DocumentTypeID;

        /// <summary>
        /// Gets or sets the DocumentTypeID
        /// </summary>
        public string? DocumentTypeID
        {
            get { return _DocumentTypeID; }
            set { _DocumentTypeID = value; }
        }

        /// <summary>
        /// Gets or sets the DocumentDate
        /// </summary>
        public string? DocumentDate
        {
            get { return _DocumentDate; }
            set { _DocumentDate = value; }
        }

        /// <summary>
        /// Gets or sets the DocumentScriptoriumId
        /// </summary>
        public string? DocumentScriptoriumId
        {
            get { return _DocumentScriptoriumId; }
            set { _DocumentScriptoriumId = value; }
        }

        /// <summary>
        /// Gets or sets the DocumentNationalNo
        /// </summary>
        public string? DocumentNationalNo
        {
            get { return _DocumentNationalNo; }
            set { _DocumentNationalNo = value; }
        }
    }
}
