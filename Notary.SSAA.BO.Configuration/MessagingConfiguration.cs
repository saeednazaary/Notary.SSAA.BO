using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notary.SSAA.BO.Configuration
{
    public class MessagingConfiguration
    {
        public bool SMSServiceEnabled 
        {
            get { return _smsServiceEnabled; }
        }

        public bool DocAgentValidationMechanizedMessages 
        {
            get { return _docAgentValidationMechanizedMessages; }
        }

        public bool RelatedDocumentMechanizedMessages 
        {
            get { return _relatedDocumentMechanizedMessages; }
        }

        public bool FinalConfirmationMechanizedMessages 
        {
            get { return _finalConfirmationMechanizedMessages; }
        }

        public bool UserDefinedMessages 
        {
            get { return _userDefinedMessages; }
        }

        public bool AzlOrEstefaMessages
        {
            get { return _AzlOrEstefaMessages; }
        }

        public bool SSAADefinedMessages
        {
            get { return _SSAADefinedMessage; }
        }

        public bool CostOfDocumentForPayMessages
        {
            get { return _CostOfDocumentForPayMessage; }
        }

        private bool _smsServiceEnabled = false;
        private bool _docAgentValidationMechanizedMessages = false;
        private bool _relatedDocumentMechanizedMessages = false;
        private bool _finalConfirmationMechanizedMessages = false;
        private bool _userDefinedMessages = false;
        private bool _AzlOrEstefaMessages = false;
        private bool _SSAADefinedMessage = false;
        private bool _CostOfDocumentForPayMessage = false;

        public MessagingConfiguration()
        {
            _smsServiceEnabled = Settings.SMSServiceEnabled;
            _docAgentValidationMechanizedMessages = Settings.DocAgentValidationMechanizedMessages;
            _relatedDocumentMechanizedMessages= Settings.RelatedDocumentMechanizedMessages;
            _finalConfirmationMechanizedMessages=Settings.FinalConfirmationMechanizedMessages;
            _userDefinedMessages=Settings.UserDefinedMessages;
            _AzlOrEstefaMessages=Settings.AzlOrEstefaMessages;
            _SSAADefinedMessage=Settings.SSAADefinedMessages;
            _CostOfDocumentForPayMessage = Settings.CostOfDocumentForPayMessages;
            //Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings["SMSServiceEnabled"], out _smsServiceEnabled);

            //Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings["DocAgentValidationMechanizedMessages"], out _docAgentValidationMechanizedMessages);

            //Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings["RelatedDocumentMechanizedMessages"], out _relatedDocumentMechanizedMessages);

            //Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings["FinalConfirmationMechanizedMessages"], out _finalConfirmationMechanizedMessages);

            //Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings["UserDefinedMessages"], out _userDefinedMessages);

            //Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings["AzlOrEstefaMessages"], out _AzlOrEstefaMessages);

            //Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings["SSAADefinedMessages"], out _SSAADefinedMessage);

            //Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings["CostOfDocumentForPayMessages"], out _CostOfDocumentForPayMessage);
        }
    }
}
