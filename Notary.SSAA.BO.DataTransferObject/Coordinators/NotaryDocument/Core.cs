
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument
{
    public class SMSRecipient
    {
        public string RecipientFullName { get; set; }
        public string RecipientMobileNo { get; set; }
        public string PersonTypeCode { get; set; }
    }
    public class DocAgentValidationResponsPacket
    {
        private string _correspondingRequestPacketID;
        private DocAgentValidationRequestPacket _requestPacket;

        public DocAgentValidationResponsPacket ( string correspondingRequestPacketID )
        {
            _correspondingRequestPacketID = correspondingRequestPacketID;
        }

        public DocAgentValidationResponsPacket ( DocAgentValidationRequestPacket requestPacket )
        {
            _requestPacket = requestPacket;
        }

        public DocAgentValidationResponsPacket ( )
        {

        }

        public DocAgentValidationRequestPacket RequestPacket
        {
            get
            {
                return _requestPacket;
            }
        }

        public string CorrespondingRequestPacketID
        {
            get { return _correspondingRequestPacketID; }
        }

        public string CurrentDocAgentObjectID { get; set; }

        public string SMSObjectID { get; set; }

        public string ResponseMessage { get; set; }

        public string ResponseExceptionMessages { get; set; }

        public string CorrespondingRegisterServiceReqObjectID { get; set; }

        public Document CorrespondingRegisterServiceReqObject { get; set; }

        public List<SMSRecipient> SMSRecipientsCollection { get; set; }

        public bool Response { get; set; }
    }
    public class DocAgentValidationRequestPacket
    {
        //private string _ID;

        public DocAgentValidationRequestPacket ( )
        {
            //_ID = System.Guid.NewGuid().ToString();
        }

        public string ID { get; set; }

        public bool SMSIsRequired { get; set; }

        /// <summary>
        /// This Property Is Used In CallBack. 
        /// If the validation result is "Not-Allowed", the object is being found using this object id on client-side         
        /// </summary>

        public string CurrentDocAgentObjectID { get; set; }

        public string MobileNo4SMS { get; set; }

        public string DocumentNationalNo { get; set; }

        public string DocumentScriptoriumId { get; set; }

        public string DocumentSecretCode { get; set; }

        public string DocumentDate { get; set; }

        public string VakilNationalNo { get; set; }

        public string MovakelNationalNo { get; set; }

        public string VakilFullName { get; set; }

        public string MovakelFullName { get; set; }

        public string DocumentTypeID { get; set; }

        public System.Collections.Generic.List<DocumentCase> CurrentRegCasesCollection { get; set; }
        public System.Collections.Generic.List<DocumentVehicle> CurrentRegVehiclesCollection { get; set; }
        public System.Collections.Generic.List<DocumentEstate> CurrentRegEstateCollection { get; set; }
    }
    public class SMSRecipientPacket
    {
        public string RecipientFullName { get; set; }
        public string RecipientMobileNo { get; set; }
        public string RecipientPersonTypeCode { get; set; }
        public RelatedPersonType RecipientPersonType { get; set; }
        public string RecipientSMSContext { get; set; }
    }
    public class AgentDocValidationResult
    {
        public string CurrentDocAgentObjectID { get; set; }
        public string ValidationRequestID { get; set; }
        public bool DocExists { get; set; }
        public RestrictionLevel ResultRestrictionLevel { get; set; }
        public object FoundObjectTypeCode { get; set; }
        public string MessageText { get; set; }
        public string ExceptionMessageText { get; set; }
        public string RegisterServiceReqID { get; set; }
        public List<SMSRecipientPacket> SMSRecipientPacketCollection { get; set; }
    }
    public class AgentDocValidationRequestPacket
    {
        private string _ValidationRequestID = string.Empty;


        public string ValidationRequestID
        {
            get
            {
                return _ValidationRequestID;
            }
            set
            {
                _ValidationRequestID = value;
            }
        }

        public bool SMSIsRequired { get; set; }

        /// <summary>
        /// This Property Is Used In CallBack. 
        /// If the validation result is "Not-Allowed", the object is being found using this object id on client-side         
        /// </summary>
        public string CurrentDocAgentObjectID { get; set; }

        public string MobileNo4SMS { get; set; }

        public string AgentDocumentNationalNo { get; set; }

        public string AgentDocumentScriptoriumId { get; set; }

        public string AgentDocumentDate { get; set; }

        public string MovakelNationalNo { get; set; }

        public string MovakelFullName { get; set; }

        public string VakilNationalNo { get; set; }

        public string VakilFullName { get; set; }

        public string DocumentTypeID { get; set; }

        public List<RegCasePacket> RegCasesCollection { get; set; }
    }
    public class RegCasePacket
    {
        public string BasicPlaqueNo { get; set; }
        public string SecondaryPlaqueNo { get; set; }
        public string vehicleChassisNo { get; set; }
        public string VehicleEngineNo { get; set; }
    }
    public class RelatedDocumentValidationResponse
    {
        public bool ValidationResult { get; set; }
        public bool DigitalBookBaseInfoInitialized { get; set; }
        public string ValidationResponseMessage { get; set; }
        public string registerServiceReqObjectID { get; set; }
        public string RelatedDocumentClassifyNo { get; set; }
        public Document RelatedDocumentObject { get; set; }
    }
    public class RelatedDocValidationResult
    {
        public bool IsValid { get; set; }
        public string ValidationMessage { get; set; }
        public string RelatedRegisterServiceReqObjectID { get; set; }
    }

    public class DocValidationCommonData
    {
        public List<Document> TheFoundRegisterServiceReqsCollection { get; set; }
        public Domain.Entities.DocumentType TheParentRegisterServiceReqDocumentType { get; set; }
    }

    public class DataCollectionRequestPacket
    {
        public string DocumentNationalNo;
        public string DocumentSecretCode;
        public bool IncludeRegCaseDescription;
    }
    public class DocumentResponseMessage
    {
        public object MainEntity;
        public string Message { get; set; }

    }

}
