namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Messaging
{
    using Microsoft.Extensions.Logging;
    using Notary.SSAA.BO.Configuration;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="MessagingCore" />
    /// </summary>
    public class MessagingCore
    {
        /// <summary>
        /// Defines the messagingConfiguration
        /// </summary>
        private MessagingConfiguration messagingConfiguration;

        /// <summary>
        /// Defines the messagingCoreOperations
        /// </summary>
        private MessagingCoreOperations messagingCoreOperations;

        /// <summary>
        /// Defines the documentRepository
        /// </summary>
        private IDocumentRepository documentRepository;
        private readonly ILogger<MessagingCore> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingCore"/> class.
        /// </summary>
        /// <param name="_messagingConfiguration">The _messagingConfiguration<see cref="MessagingConfiguration"/></param>
        /// <param name="_messagingCoreOperations">The _messagingCoreOperations<see cref="MessagingCoreOperations"/></param>
        /// <param name="_documentRepository">The _documentRepository<see cref="IDocumentRepository"/></param>
        public MessagingCore(MessagingConfiguration _messagingConfiguration, MessagingCoreOperations _messagingCoreOperations, IDocumentRepository _documentRepository, ILogger<MessagingCore> logger)
        {
            messagingConfiguration = _messagingConfiguration;
            messagingCoreOperations = _messagingCoreOperations;
            documentRepository = _documentRepository;
            _logger = logger;
        }

        /// <summary>
        /// The CreateSMS
        /// </summary>
        /// <param name="smsUsageContext">The smsUsageContext<see cref="SMSUsageContext"/></param>
        /// <param name="messageInputPacket">The messageInputPacket<see cref="MessageInputPacket?"/></param>
        /// <param name="innerTransaction">The innerTransaction<see cref="bool"/></param>
        /// <param name="autoPreparation">The autoPreparation<see cref="bool"/></param>
        /// <returns>The <see cref="Task{MessageOutputPacket?}"/></returns>
        public async Task<MessageOutputPacket?> CreateSMS(
            SMSUsageContext smsUsageContext,
            MessageInputPacket? messageInputPacket,
            bool innerTransaction = true,
            bool autoPreparation = false)
        {
            MessageOutputPacket messageOutputPacket = new MessageOutputPacket();
            string errorMessage = string.Empty;

            try
            {
                if (!messagingConfiguration.SMSServiceEnabled)
                {
                    messageOutputPacket.ResponseMessage = "سرویس ارسال پیامک غیرفعال می باشد.";
                    messageOutputPacket.MessageGenerated = false;
                    return messageOutputPacket;
                }

                bool requestPacketValidated = this.ValidateRequestPacket(messageInputPacket, ref errorMessage);
                if (!requestPacketValidated)
                {
                    messageOutputPacket.ResponseMessage = errorMessage;
                    messageOutputPacket.MessageGenerated = false;
                    return messageOutputPacket;
                }

                //using (new TransactedOperation(innerTransaction))
                {
                    if (autoPreparation)
                    {
                        bool preparationProcess;
                        (preparationProcess, messageInputPacket, errorMessage) = await this.PrepareRequestPacket(messageInputPacket, errorMessage);
                        if (!preparationProcess)
                        {
                            messageOutputPacket.MessageGenerated = false;
                            messageOutputPacket.ResponseMessage = errorMessage;
                            return messageOutputPacket;
                        }
                    }

                    switch (smsUsageContext)
                    {
                        case SMSUsageContext.AgentDocument:
                            if (messagingConfiguration.DocAgentValidationMechanizedMessages)
                            {
                                (messageOutputPacket, messageInputPacket) =
                                    await messagingCoreOperations.CreateAgentDocValidationMechanizedSMS(messageInputPacket);
                            }
                            break;
                        case SMSUsageContext.RelatedDocument:
                            if (messagingConfiguration.RelatedDocumentMechanizedMessages)
                            {
                                (messageOutputPacket, messageInputPacket) =
                                    messagingCoreOperations.CreateRelatedDocMechanizedSMS(messageInputPacket);
                            }
                            break;
                        case SMSUsageContext.FinalVerification:
                            if (messagingConfiguration.FinalConfirmationMechanizedMessages)
                            {
                                (messageOutputPacket, messageInputPacket) =
                                    await messagingCoreOperations.CreateFinalConfirmationMechanizedSMS(messageInputPacket);
                            }
                            break;
                        case SMSUsageContext.UserDefined:
                            if (messagingConfiguration.UserDefinedMessages)
                            {
                                messageOutputPacket = messagingCoreOperations.CreateUserDefinedSMS(ref messageInputPacket);
                            }
                            break;
                        case SMSUsageContext.AzlOrEstefa:
                            if (messagingConfiguration.AzlOrEstefaMessages)
                            {
                                (messageOutputPacket, messageInputPacket) =
                                    await messagingCoreOperations.CreateAzl_EstefaSMSOnFinalConfirmation(messageInputPacket);
                            }
                            break;
                        case SMSUsageContext.SSAADefined:
                            if (messagingConfiguration.SSAADefinedMessages)
                            {
                                (messageOutputPacket, messageInputPacket) =
                                    messagingCoreOperations.CreateSSAAUserSMS(messageInputPacket);
                            }
                            break;
                        case SMSUsageContext.CostOfDocumentForPay:
                            if (messagingConfiguration.CostOfDocumentForPayMessages)
                            {
                                (messageOutputPacket, messageInputPacket) =
                                    await messagingCoreOperations.CreateCostOfDocumentForPaySMS(messageInputPacket);
                            }
                            break;
                    }

                    if (messageOutputPacket.MessageGenerated)
                    {
                        try
                        {
                            //if (innerTransaction)
                            //    Rad.CMS.OjbBridge.TransactionContext.Current.Commit();
                        }
                        catch (System.Exception ex)
                        {
                            // 🔸 لاگ کردن خطا درون بلاک داخلی
                            _logger.LogError(ex, "خطا در Commit تراکنش پیامک.");
                            errorMessage = ex.ToString();
                            messageOutputPacket.ResponseMessage = errorMessage;
                            messageOutputPacket.MessageGenerated = false;
                        }
                    }
                    else
                    {
                        messageOutputPacket.MessageGenerated = false;
                    }

                    return messageOutputPacket;
                }
            }
            catch (Exception ex)
            {
                // 🔸 لاگ کردن خطای اصلی
                _logger.LogError(ex, "خطای غیرمنتظره در متد CreateSMS رخ داده است.");
                return null;
            }
        }
        /// <summary>
        /// The ValidateRequestPacket
        /// </summary>
        /// <param name="messageInputPacket">The messageInputPacket<see cref="MessageInputPacket?"/></param>
        /// <param name="validationMessage">The validationMessage<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool ValidateRequestPacket(MessageInputPacket? messageInputPacket, ref string validationMessage)
        {
            if (messageInputPacket != null && messageInputPacket.MainEntity != null || messageInputPacket?.MainEntityObjectID != null)
                return true;

            if (string.IsNullOrWhiteSpace(messageInputPacket?.RecipientPhoneNo))
            {
                validationMessage = "شماره گیرنده پیام تعیین نشده است!";
                return false;
            }

            return true;
        }

        /// <summary>
        /// The PrepareRequestPacket
        /// </summary>
        /// <param name="requestPacket">The requestPacket<see cref="MessageInputPacket?"/></param>
        /// <param name="preparationErrorMessage">The preparationErrorMessage<see cref="string"/></param>
        /// <returns>The <see cref="Task{(bool, MessageInputPacket?, string)}"/></returns>
        private async Task<(bool, MessageInputPacket?, string)> PrepareRequestPacket(MessageInputPacket? requestPacket, string preparationErrorMessage)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = source.Token;
            bool preparationResult = false;

            if (requestPacket == null)
                preparationResult = false;

            if (requestPacket != null && requestPacket.MainEntity != null)
                preparationResult = true;
            else
            {
                if (requestPacket != null)
                    requestPacket.MainEntity = await documentRepository.GetDocumentById(Guid.Parse(requestPacket.MainEntityObjectID), new List<string>() { "DocumentType", "DocumentPeople" }, cancellationToken);
                if (requestPacket != null && requestPacket.MainEntity != null)
                    preparationResult = true;
            }

            return (preparationResult, requestPacket, preparationErrorMessage);
        }
    }
}
