namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core
{
    using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Validators.Shell;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CentralizedValidator" />
    /// </summary>
    public class CentralizedValidator
    {
        /// <summary>
        /// Defines the dataCollectorController
        /// </summary>
        private readonly DataCollector dataCollectorController;

        /// <summary>
        /// Defines the agentDocumentValidatorController
        /// </summary>
        internal AgentDocumentValidator agentDocumentValidatorController;

        /// <summary>
        /// Defines the relatedDocumentValidator
        /// </summary>
        internal RelatedDocumentValidator relatedDocumentValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CentralizedValidator"/> class.
        /// </summary>
        /// <param name="_dataCollectorController">The _dataCollectorController<see cref="DataCollector"/></param>
        /// <param name="_agentDocumentValidatorController">The _agentDocumentValidatorController<see cref="AgentDocumentValidator"/></param>
        /// <param name="_relatedDocumentValidator">The _relatedDocumentValidator<see cref="RelatedDocumentValidator"/></param>
        public CentralizedValidator ( DataCollector _dataCollectorController, AgentDocumentValidator _agentDocumentValidatorController, RelatedDocumentValidator _relatedDocumentValidator )
        {
            dataCollectorController = _dataCollectorController;
            agentDocumentValidatorController = _agentDocumentValidatorController;
            relatedDocumentValidator = _relatedDocumentValidator;
        }

        /// <summary>
        /// The ValidateAgentDocumentsCollection
        /// </summary>
        /// <param name="requestsCollection">The requestsCollection<see cref="List{AgentDocValidationRequestPacket}"/></param>
        /// <returns>The <see cref="Task{List{AgentDocValidationResult}}"/></returns>
        public async Task<List<AgentDocValidationResult>> ValidateAgentDocumentsCollection ( List<AgentDocValidationRequestPacket> requestsCollection )
        {
            List<AgentDocValidationResult> responsesCollection = new List<AgentDocValidationResult>();

            if ( requestsCollection == null || requestsCollection.Count == 0 )
                responsesCollection = agentDocumentValidatorController.GenerateErrorPackResult ( "لیست وکالتنامه های ورودی برای تصدیق اطلاعات، کامل نمی باشد." );
            else
                responsesCollection = await agentDocumentValidatorController.ValidateAgentDocumentsCollection ( requestsCollection );

            return responsesCollection;
        }

        /// <summary>
        /// The ValidateAgentDocumentPacket
        /// </summary>
        /// <param name="agentDocValidationRequestPacket">The agentDocValidationRequestPacket<see cref="AgentDocValidationRequestPacket"/></param>
        /// <returns>The <see cref="Task{AgentDocValidationResult}"/></returns>
        public async Task<AgentDocValidationResult> ValidateAgentDocumentPacket ( AgentDocValidationRequestPacket agentDocValidationRequestPacket )
        {
            AgentDocValidationResult agentDocValidationResult = new AgentDocValidationResult();

            agentDocValidationResult = await agentDocumentValidatorController.ValidateAgentDocumentPacket ( agentDocValidationRequestPacket );

            agentDocValidationResult.ValidationRequestID = agentDocValidationRequestPacket.ValidationRequestID;
            agentDocValidationResult.CurrentDocAgentObjectID = agentDocValidationRequestPacket.CurrentDocAgentObjectID;

            return agentDocValidationResult;
        }

        /// <summary>
        /// The ValidateAgentDocument
        /// </summary>
        /// <param name="pAgentDocumentNationalNo">The pAgentDocumentNationalNo<see cref="string"/></param>
        /// <param name="pAgentDocumentScriptoriumId">The pAgentDocumentScriptoriumId<see cref="string"/></param>
        /// <param name="pAgentDocumentDate">The pAgentDocumentDate<see cref="string"/></param>
        /// <param name="pDocumentTypeID">The pDocumentTypeID<see cref="string"/></param>
        /// <param name="pMovakelNationalNo">The pMovakelNationalNo<see cref="string"/></param>
        /// <param name="pVakilNationalNo">The pVakilNationalNo<see cref="string"/></param>
        /// <param name="pMovakelFullName">The pMovakelFullName<see cref="string"/></param>
        /// <param name="pVakilFullName">The pVakilFullName<see cref="string"/></param>
        /// <param name="pRegCaseList">The pRegCaseList<see cref="List{RegCasePacket}"/></param>
        /// <returns>The <see cref="Task{AgentDocValidationResult?}"/></returns>
        public async Task<AgentDocValidationResult?> ValidateAgentDocument (
                                                              string pAgentDocumentNationalNo,
                                                              string pAgentDocumentScriptoriumId,
                                                              string pAgentDocumentDate,
                                                              string pDocumentTypeID,
                                                              string pMovakelNationalNo,
                                                              string pVakilNationalNo,
                                                              string pMovakelFullName,
                                                              string pVakilFullName,
                                                              List<RegCasePacket> pRegCaseList
                                                              )
        {
            try
            {
                AgentDocValidationResult? agentDocValidationResult = null;
                //List<RegCasePacket> regCasesCollection = this.ConvertRegCasePacketCollection(pRegCaseList);
                agentDocValidationResult = await agentDocumentValidatorController.ValidateAgentDocument (
                                                                                        pAgentDocumentNationalNo,
                                                                                        pAgentDocumentScriptoriumId,
                                                                                        pAgentDocumentDate,
                                                                                        pDocumentTypeID,
                                                                                        pMovakelNationalNo,
                                                                                        pVakilNationalNo,
                                                                                        pMovakelFullName,
                                                                                        pVakilFullName,
                                                                                        pRegCaseList
                                                                                        );
                if ( agentDocValidationResult != null )
                {
                    if ( !string.IsNullOrWhiteSpace ( agentDocValidationResult.MessageText ) )
                        agentDocValidationResult.MessageText = System.Environment.NewLine + "هشدارهای مربوط به وکالتنامه مورد استناد در بخش اشخاص: " + System.Environment.NewLine + agentDocValidationResult.MessageText;
                }

                //return ConvertToServiceDataContractResult(agentDocValidationResult);
                return agentDocValidationResult;
            }
            catch ( System.Exception )
            {
                AgentDocValidationResult result = new AgentDocValidationResult();

                result.DocExists = false;
                result.MessageText = "خطا در اخذ اطلاعات وکالتنامه مورد نظر!";

                return result;
            }
        }

        /// <summary>
        /// The ValidateRelatedDocument
        /// </summary>
        /// <param name="pRelatedDocumentNationalNo">The pRelatedDocumentNationalNo<see cref="string"/></param>
        /// <param name="pRelatedDocumentScriptoriumId">The pRelatedDocumentScriptoriumId<see cref="string"/></param>
        /// <param name="pRelatedDocumentDate">The pRelatedDocumentDate<see cref="string"/></param>
        /// <param name="pDocumentTypeID">The pDocumentTypeID<see cref="string"/></param>
        /// <returns>The <see cref="Task{RelatedDocValidationResult}"/></returns>
        public async Task<RelatedDocValidationResult> ValidateRelatedDocument (
                                                                  string pRelatedDocumentNationalNo,
                                                                  string pRelatedDocumentScriptoriumId,
                                                                  string pRelatedDocumentDate,
                                                                  string pDocumentTypeID
                                                                  )
        {
            string relatedRegisterServiceReq = string.Empty;
            Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document.RelatedDocValidationRequest  validationRequest = new Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document.RelatedDocValidationRequest ();
            validationRequest.RelatedDocumentNationalNo = pRelatedDocumentNationalNo;
            validationRequest.RelatedDocumentScriptoriumId = pRelatedDocumentScriptoriumId;
            validationRequest.RelatedDocumentDate = pRelatedDocumentDate;
            validationRequest.DocumentTypeID = pDocumentTypeID;

            RelatedDocValidationResult validationResponsePacket =await relatedDocumentValidator.ValidateRelatedDocument(validationRequest);

            return validationResponsePacket;
        }

        /// <summary>
        /// The CollectData
        /// </summary>
        /// <param name="request">The request<see cref="DataCollectionRequestPacket"/></param>
        /// <returns>The <see cref="Task{DataCollectionResponsePacket}"/></returns>
        public async Task<DataCollectionResponsePacket> CollectData ( DataCollectionRequestPacket request )
        {
            DataCollectionResponsePacket responsePacket = await dataCollectorController.CollectData(request);

            return responsePacket;
        }

        /// <summary>
        /// The CollectSMSData
        /// </summary>
        /// <param name="nationalNo">The nationalNo<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{SMSRecipientPacket}?}"/></returns>
        public async Task<List<SMSRecipientPacket>?> CollectSMSData ( string nationalNo )
        {
            List<SMSRecipientPacket>? smsRecipientsPackets =await dataCollectorController.CollectSMSRecipients(nationalNo);

            return smsRecipientsPackets;
        }

        /// <summary>
        /// The GetExceptionDetail
        /// </summary>
        /// <param name="ex">The ex<see cref="Exception"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string GetExceptionDetail ( Exception ex )
        {
            StringBuilder sb = new StringBuilder();
            while ( ex != null )
            {
                sb.AppendLine ( ex.Message );
                sb.AppendLine ( ex.StackTrace );
                if ( ex.InnerException != null )
                    ex = ex.InnerException;
            }
            return sb.ToString ();
        }

        /// <summary>
        /// The ConvertRegCasePacketCollection
        /// </summary>
        /// <param name="regCaseServiceContractCollection">The regCaseServiceContractCollection<see cref="List{RegCasePacket}"/></param>
        /// <returns>The <see cref="List{RegCasePacket}"/></returns>
        private List<RegCasePacket> ConvertRegCasePacketCollection ( List<RegCasePacket> regCaseServiceContractCollection )
        {
            List<RegCasePacket> validatorRegCaseInputCollection = new List<RegCasePacket>();
            foreach ( RegCasePacket theOneContractMember in regCaseServiceContractCollection )
            {
                RegCasePacket theOneNewInputPacket = new RegCasePacket();

                theOneNewInputPacket.BasicPlaqueNo = theOneContractMember.BasicPlaqueNo;
                theOneNewInputPacket.SecondaryPlaqueNo = theOneContractMember.SecondaryPlaqueNo;
                theOneNewInputPacket.vehicleChassisNo = theOneContractMember.vehicleChassisNo;
                theOneNewInputPacket.VehicleEngineNo = theOneContractMember.VehicleEngineNo;

                validatorRegCaseInputCollection.Add ( theOneNewInputPacket );
            }

            return validatorRegCaseInputCollection;
        }

        /// <summary>
        /// The ConvertSMSPacketsToServiceDataTypes
        /// </summary>
        /// <param name="nonConvertedPacketCollection">The nonConvertedPacketCollection<see cref="List{SMSRecipientPacket}"/></param>
        /// <returns>The <see cref="List{SMSRecipientPacket}?"/></returns>
        private List<SMSRecipientPacket>? ConvertSMSPacketsToServiceDataTypes ( List<SMSRecipientPacket> nonConvertedPacketCollection )
        {
            List<SMSRecipientPacket> convertedCollection = new List<SMSRecipientPacket>(){ };

            if ( nonConvertedPacketCollection == null || nonConvertedPacketCollection.Count == 0 )
                return null;

            foreach ( SMSRecipientPacket theOneNotConvertedElement in nonConvertedPacketCollection )
            {
                SMSRecipientPacket singleServiceSMSPacket = new SMSRecipientPacket();
                singleServiceSMSPacket.RecipientMobileNo = theOneNotConvertedElement.RecipientMobileNo;
                singleServiceSMSPacket.RecipientFullName = theOneNotConvertedElement.RecipientFullName;
                singleServiceSMSPacket.RecipientPersonTypeCode = theOneNotConvertedElement.RecipientPersonTypeCode;
                singleServiceSMSPacket.RecipientSMSContext = theOneNotConvertedElement.RecipientSMSContext;

                convertedCollection.Add ( singleServiceSMSPacket );
            }

            return convertedCollection;
        }

        /// <summary>
        /// The CollectDocumentAnnotations
        /// </summary>
        /// <param name="classifyNo">The classifyNo<see cref="string"/></param>
        /// <param name="nationalNo">The nationalNo<see cref="string"/></param>
        /// <param name="scriptoriumID">The scriptoriumID<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{string}?}"/></returns>
        public async Task<List<string>?> CollectDocumentAnnotations ( string classifyNo, string nationalNo, string scriptoriumID )
        {
            List<string>? annotaions =await dataCollectorController.CollectDocumentAnnotations(classifyNo, nationalNo, scriptoriumID);
            return annotaions;
        }
    }

}
