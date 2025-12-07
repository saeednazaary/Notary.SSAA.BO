namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Messaging
{
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;

    /// <summary>
    /// Defines the <see cref="MessagingCoreOperations" />
    /// </summary>
    public class MessagingCoreOperations
    {
        private readonly IUserService _userService;
        /// <summary>
        /// Defines the smsGeneratorEngine
        /// </summary>
        internal SMSGeneratorEngine smsGeneratorEngine;

        /// <summary>
        /// Defines the dateTimeService
        /// </summary>
        internal IDateTimeService dateTimeService;

        /// <summary>
        /// Defines the documentSMSRepository
        /// </summary>
        internal IDocumentSMSRepository documentSMSRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingCoreOperations"/> class.
        /// </summary>
        /// <param name="_smsGeneratorEngine">The _smsGeneratorEngine<see cref="SMSGeneratorEngine"/></param>
        /// <param name="_dateTimeService">The _dateTimeService<see cref="IDateTimeService"/></param>
        /// <param name="_documentSMSRepository">The _documentSMSRepository<see cref="IDocumentSMSRepository"/></param>
        public MessagingCoreOperations(SMSGeneratorEngine _smsGeneratorEngine, IDateTimeService _dateTimeService, IDocumentSMSRepository _documentSMSRepository, IUserService userService)
        {

            smsGeneratorEngine = _smsGeneratorEngine;
            dateTimeService = _dateTimeService;
            documentSMSRepository = _documentSMSRepository;
            _userService = userService;
        }

        /// <summary>
        /// The CreateAgentDocValidationMechanizedSMS
        /// </summary>
        /// <param name="mainSMSRequest">The mainSMSRequest<see cref="MessageInputPacket?"/></param>
        /// <returns>The <see cref="Task{(MessageOutputPacket, MessageInputPacket?)}"/></returns>
        internal async Task<(MessageOutputPacket, MessageInputPacket?)> CreateAgentDocValidationMechanizedSMS(MessageInputPacket? mainSMSRequest)
        {
            MessageOutputPacket messageOutputPacket = new MessageOutputPacket();
            try
            {
                if (mainSMSRequest != null)
                {
                    mainSMSRequest.MessageContext = mainSMSRequest.DocumentTypeTitle +
                                                " توسط " + mainSMSRequest.ScriptoriumFullName +
                                                " بر اساس سند وکالتنامه شما به شماره " +
                                                mainSMSRequest.DocNationalNo +
                                                " در حال  تنظیم است. " +
                                                "در صورت نیاز به کسب اطلاعات بیشتر به دفترخانه مذکور تماس حاصل فرمایید." +
                                                " \nسازمان ثبت اسناد و املاک کشور" +
                                                " \nwww.SSAA.ir";

                    bool smsIsSent = await smsGeneratorEngine.GenerateCoreSMS(mainSMSRequest, SMSCostPolicy.Non_Free);

                    if (smsIsSent)
                    {
                        DocumentSm theOneRegisterSerivceSMS = new();
                        theOneRegisterSerivceSMS.DocumentId = mainSMSRequest.MainEntity.Id; ;
                        theOneRegisterSerivceSMS.IsMechanized = "1";
                        theOneRegisterSerivceSMS.IsSent = "1";
                        theOneRegisterSerivceSMS.Ilm = "1";
                        theOneRegisterSerivceSMS.Id = Guid.NewGuid();
                        theOneRegisterSerivceSMS.SmsText = mainSMSRequest.MessageContext;
                        theOneRegisterSerivceSMS.ReceiverName = mainSMSRequest.RecipientFullName;
                        theOneRegisterSerivceSMS.MobileNo = mainSMSRequest.RecipientPhoneNo;
                        theOneRegisterSerivceSMS.CreateDate = dateTimeService.CurrentPersianDate;
                        theOneRegisterSerivceSMS.CreateTime = dateTimeService.CurrentTime.Substring(0, 5);
                        theOneRegisterSerivceSMS.RecordDate = dateTimeService.CurrentDateTime;
                        theOneRegisterSerivceSMS.ScriptoriumId =
                            _userService.UserApplicationContext.ScriptoriumInformation.Code;
                        CancellationTokenSource cts = new();
                        CancellationToken cancellationToken = cts.Token;
                        await documentSMSRepository.AddAsync(theOneRegisterSerivceSMS, cancellationToken, false);
                        messageOutputPacket.MessageGenerated = true;
                    }
                    else
                    {
                        DocumentSm theOneRegisterSerivceSMS = new();
                        theOneRegisterSerivceSMS.DocumentId = mainSMSRequest.MainEntity.Id; ;
                        theOneRegisterSerivceSMS.IsMechanized = "1";
                        theOneRegisterSerivceSMS.IsSent = "2";
                        theOneRegisterSerivceSMS.Ilm = "1";
                        theOneRegisterSerivceSMS.Id = Guid.NewGuid();
                        theOneRegisterSerivceSMS.SmsText = mainSMSRequest.MessageContext;
                        theOneRegisterSerivceSMS.ReceiverName = mainSMSRequest.RecipientFullName;
                        theOneRegisterSerivceSMS.MobileNo = mainSMSRequest.RecipientPhoneNo;
                        theOneRegisterSerivceSMS.CreateDate = dateTimeService.CurrentPersianDate;
                        theOneRegisterSerivceSMS.CreateTime = dateTimeService.CurrentTime.Substring(0, 5);
                        theOneRegisterSerivceSMS.RecordDate = dateTimeService.CurrentDateTime;
                        theOneRegisterSerivceSMS.ScriptoriumId =
                            _userService.UserApplicationContext.ScriptoriumInformation.Code;
                        CancellationTokenSource cts = new();
                        CancellationToken cancellationToken = cts.Token;
                        await documentSMSRepository.AddAsync(theOneRegisterSerivceSMS, cancellationToken, false);
                        messageOutputPacket.MessageGenerated = true;
                    }
                }

            }
            catch (Exception)
            {
            }

            return (messageOutputPacket, mainSMSRequest);
        }

        /// <summary>
        /// The CreateRelatedDocMechanizedSMS
        /// </summary>
        /// <param name="messageInputPacket">The messageInputPacket<see cref="MessageInputPacket?"/></param>
        /// <returns>The <see cref="(MessageOutputPacket,MessageInputPacket?)"/></returns>
        internal (MessageOutputPacket, MessageInputPacket?) CreateRelatedDocMechanizedSMS(MessageInputPacket? messageInputPacket)
        {
            MessageOutputPacket messageOutputPacket = new MessageOutputPacket();
            try
            {

            }
            catch (Exception)
            {
            }

            return (messageOutputPacket, messageInputPacket);
        }

        /// <summary>
        /// The CreateFinalConfirmationMechanizedSMS
        /// </summary>
        /// <param name="mainSMSRequest">The mainSMSRequest<see cref="MessageInputPacket?"/></param>
        /// <returns>The <see cref="Task{(MessageOutputPacket, MessageInputPacket?)}"/></returns>
        internal async Task<(MessageOutputPacket, MessageInputPacket?)> CreateFinalConfirmationMechanizedSMS(MessageInputPacket? mainSMSRequest)
        {
            MessageOutputPacket messageOutputPacket = new MessageOutputPacket();
            try
            {
                if (mainSMSRequest != null)
                {
                    foreach (DocumentPerson theOnePerson in mainSMSRequest.MainEntity.DocumentPeople)
                    {
                        if (string.IsNullOrWhiteSpace(theOnePerson.MobileNo))
                            continue;

                        MessageInputPacket theOneMessageInputPacket = new MessageInputPacket();
                        if (mainSMSRequest.MainEntity.DocumentTypeId == "007" ||     // اجراييه
                            mainSMSRequest.MainEntity.DocumentTypeId == "0034")     // رفع نقص اجراييه
                        {
                            theOneMessageInputPacket.MessageContext = mainSMSRequest.MainEntity.DocumentType.Title +
                                            " شما با شناسه " + mainSMSRequest.MainEntity.NationalNo +
                                            " در تاریخ " + mainSMSRequest.MainEntity.DocumentDate +
                                            " توسط دفترخانه " + /*mainSMSRequest.MainEntity.TheScriptorium.ScriptoriumNo*/_userService.UserApplicationContext?.ScriptoriumInformation?.ScriptoriumNo + " " + _userService.UserApplicationContext?.ScriptoriumInformation?.GeoLocationName +// mainSMSRequest.MainEntity.TheScriptorium.TheGeoLocation.LocationName +
                                            " به واحد اجرا ارسال گردید. اطلاع رسانی های بعدی متعاقباً از طریق پیامک اعلام خواهد شد." +
                                            " \nتصدیق اصالت سند مذکور از طریق پورتال سازمان ثبت اسناد و املاک کشور به نشانی www.ssaa.ir مقدور می باشد." +
                                            " \nتلفن دفترخانه: " + _userService.UserApplicationContext?.ScriptoriumInformation?.Tel /*+ mainSMSRequest.MainEntity.TheScriptorium.Tel*/;
                        }
                        else
                        {
                            theOneMessageInputPacket.MessageContext = mainSMSRequest.MainEntity.DocumentType.Title +
                                                                  " شما با شناسه " + mainSMSRequest.MainEntity.NationalNo +
                                                                  " در تاریخ " + mainSMSRequest.MainEntity.DocumentDate +
                                                                  " توسط دفترخانه " + /*mainSMSRequest.MainEntity.TheScriptorium.ScriptoriumNo*/_userService.UserApplicationContext?.ScriptoriumInformation?.ScriptoriumNo + " " + _userService.UserApplicationContext?.ScriptoriumInformation?.GeoLocationName /*mainSMSRequest.MainEntity.TheScriptorium.TheGeoLocation.LocationName*/ +
                                                                  " در سامانه ثبت الکترونیک اسناد ثبت شد." +
                                                                  " \nتصدیق اصالت سند مذکور از طریق پورتال سازمان ثبت اسناد و املاک کشور به نشانی www.ssaa.ir مقدور می باشد." +
                                                                  " \nتلفن: " + _userService.UserApplicationContext?.ScriptoriumInformation?.Tel/* mainSMSRequest.MainEntity.TheScriptorium.Tel*/;
                        }

                        theOneMessageInputPacket.RecipientPhoneNo = theOnePerson.MobileNo;
                        theOneMessageInputPacket.RecipientFullName = theOnePerson.FullName();
                        theOneMessageInputPacket.DocNationalNo = mainSMSRequest.MainEntity.NationalNo;
                        theOneMessageInputPacket.DocObjectID = mainSMSRequest.MainEntity.Id.ToString();
                        theOneMessageInputPacket.MainEntity = mainSMSRequest.MainEntity;

                        bool smsIsSent = await smsGeneratorEngine.GenerateCoreSMS(theOneMessageInputPacket, SMSCostPolicy.Non_Free);

                        if (smsIsSent)
                        {
                            DocumentSm theOneRegisterSerivceSMS = new();
                            theOneRegisterSerivceSMS.DocumentId = mainSMSRequest.MainEntity.Id;
                            theOneRegisterSerivceSMS.IsMechanized = "1";
                            theOneRegisterSerivceSMS.IsSent = "1";
                            theOneRegisterSerivceSMS.Ilm = "1";
                            theOneRegisterSerivceSMS.Id = Guid.NewGuid();
                            theOneRegisterSerivceSMS.SmsText = theOneMessageInputPacket.MessageContext;
                            theOneRegisterSerivceSMS.ReceiverName = theOneMessageInputPacket.RecipientFullName;
                            theOneRegisterSerivceSMS.MobileNo = theOneMessageInputPacket.RecipientPhoneNo;
                            theOneRegisterSerivceSMS.CreateDate = dateTimeService.CurrentPersianDate;
                            theOneRegisterSerivceSMS.CreateTime = dateTimeService.CurrentTime.Substring(0, 5);
                            theOneRegisterSerivceSMS.RecordDate = dateTimeService.CurrentDateTime;
                            theOneRegisterSerivceSMS.ScriptoriumId =
                                _userService.UserApplicationContext.ScriptoriumInformation.Code;
                            CancellationTokenSource cts = new();
                            CancellationToken cancellationToken = cts.Token;
                            await documentSMSRepository.AddAsync(theOneRegisterSerivceSMS, cancellationToken, false);

                        }
                        else
                        {
                            DocumentSm theOneRegisterSerivceSMS = new();
                            theOneRegisterSerivceSMS.DocumentId = mainSMSRequest.MainEntity.Id;
                            theOneRegisterSerivceSMS.IsMechanized = "1";
                            theOneRegisterSerivceSMS.IsSent = "2";
                            theOneRegisterSerivceSMS.Ilm = "1";
                            theOneRegisterSerivceSMS.Id = Guid.NewGuid();
                            theOneRegisterSerivceSMS.SmsText = theOneMessageInputPacket.MessageContext;
                            theOneRegisterSerivceSMS.ReceiverName = theOneMessageInputPacket.RecipientFullName;
                            theOneRegisterSerivceSMS.MobileNo = theOneMessageInputPacket.RecipientPhoneNo;
                            theOneRegisterSerivceSMS.CreateDate = dateTimeService.CurrentPersianDate;
                            theOneRegisterSerivceSMS.CreateTime = dateTimeService.CurrentTime.Substring(0, 5);
                            theOneRegisterSerivceSMS.RecordDate = dateTimeService.CurrentDateTime;
                            theOneRegisterSerivceSMS.ScriptoriumId =
                                _userService.UserApplicationContext.ScriptoriumInformation.Code;
                            CancellationTokenSource cts = new();
                            CancellationToken cancellationToken = cts.Token;
                            await documentSMSRepository.AddAsync(theOneRegisterSerivceSMS, cancellationToken, false);
                        }
                    }

                }

                messageOutputPacket.MessageGenerated = true;
            }
            catch (Exception)
            {
                //Implement Exception Code Here......
                messageOutputPacket.MessageGenerated = false;
            }

            return (messageOutputPacket, mainSMSRequest);
        }

        /// <summary>
        /// The CreateUserDefinedSMS
        /// </summary>
        /// <param name="messageInputPacket">The messageInputPacket<see cref="MessageInputPacket?"/></param>
        /// <returns>The <see cref="MessageOutputPacket"/></returns>
        internal MessageOutputPacket CreateUserDefinedSMS(ref MessageInputPacket? messageInputPacket)
        {
            MessageOutputPacket messageOutputPacket = new MessageOutputPacket();
            try
            {
                //string trmSMSID = GenerateSMSEntity(messageInputPacket);

                //if (!string.IsNullOrWhiteSpace(trmSMSID))
                //{
                //    messageOutputPacket.MessageGenerated = true;
                //    messageOutputPacket.TrmSMSIDCollection = new List<string>();
                //    messageOutputPacket.TrmSMSIDCollection.Add(trmSMSID);
                //}
            }
            catch (Exception)
            {
                messageOutputPacket.MessageGenerated = false;
            }

            return messageOutputPacket;
        }

        /// <summary>
        /// The CreateAzl_EstefaSMSOnFinalConfirmation
        /// </summary>
        /// <param name="mainSMSRequest">The mainSMSRequest<see cref="MessageInputPacket?"/></param>
        /// <returns>The <see cref="Task{(MessageOutputPacket, MessageInputPacket?)}"/></returns>
        internal async Task<(MessageOutputPacket, MessageInputPacket?)> CreateAzl_EstefaSMSOnFinalConfirmation(MessageInputPacket? mainSMSRequest)
        {
            MessageOutputPacket messageOutputPacket = new MessageOutputPacket();

            this.ImplementAzl_EstefaMessageContext(ref mainSMSRequest);

            bool smsIsSentSent = await smsGeneratorEngine.GenerateCoreSMS(mainSMSRequest, SMSCostPolicy.Non_Free);

            if (smsIsSentSent && mainSMSRequest != null)
            {
                DocumentSm theOneRegisterSerivceSMS = new();
                theOneRegisterSerivceSMS.DocumentId = mainSMSRequest.MainEntity.Id; ;
                theOneRegisterSerivceSMS.IsMechanized = "1";
                theOneRegisterSerivceSMS.Ilm = "1";
                theOneRegisterSerivceSMS.Id = Guid.NewGuid();
                theOneRegisterSerivceSMS.IsSent = "1";
                theOneRegisterSerivceSMS.SmsText = mainSMSRequest.MessageContext;
                theOneRegisterSerivceSMS.ReceiverName = mainSMSRequest.RecipientFullName;
                theOneRegisterSerivceSMS.MobileNo = mainSMSRequest.RecipientPhoneNo;
                theOneRegisterSerivceSMS.CreateDate = dateTimeService.CurrentPersianDate;
                theOneRegisterSerivceSMS.CreateTime = dateTimeService.CurrentTime.Substring(0, 5);
                theOneRegisterSerivceSMS.RecordDate = dateTimeService.CurrentDateTime;
                theOneRegisterSerivceSMS.ScriptoriumId =
                    _userService.UserApplicationContext.ScriptoriumInformation.Code;
                documentSMSRepository.Add(theOneRegisterSerivceSMS, false);
                messageOutputPacket.MessageGenerated = true;
            }

            return (messageOutputPacket, mainSMSRequest);
        }

        /// <summary>
        /// The CreateSSAAUserSMS
        /// </summary>
        /// <param name="mainSMSRequest">The mainSMSRequest<see cref="MessageInputPacket?"/></param>
        /// <returns>The <see cref="(MessageOutputPacket,MessageInputPacket?)"/></returns>
        internal (MessageOutputPacket, MessageInputPacket?) CreateSSAAUserSMS(MessageInputPacket? mainSMSRequest)
        {
            MessageOutputPacket messageOutputPacket = new MessageOutputPacket();
            try
            {

            }
            catch (Exception)
            {
            }

            return (messageOutputPacket, mainSMSRequest);
        }

        /// <summary>
        /// The CreateCostOfDocumentForPaySMS
        /// </summary>
        /// <param name="mainSMSRequest">The mainSMSRequest<see cref="MessageInputPacket?"/></param>
        /// <returns>The <see cref="Task{(MessageOutputPacket, MessageInputPacket?)}"/></returns>
        internal async Task<(MessageOutputPacket, MessageInputPacket?)> CreateCostOfDocumentForPaySMS(MessageInputPacket? mainSMSRequest)
        {
            MessageOutputPacket messageOutputPacket = new MessageOutputPacket();
            try
            {
                if (mainSMSRequest != null)
                {
                    foreach (DocumentPerson theOnePerson in mainSMSRequest.MainEntity.DocumentPeople)
                    {
                        if (string.IsNullOrWhiteSpace(theOnePerson.MobileNo))
                            continue;

                        if (theOnePerson.DocumentPersonTypeId != null &&
                            (
                            theOnePerson.DocumentPersonTypeId == "42" ||   // مقرله
                            theOnePerson.DocumentPersonTypeId == "43" ||   // متعهدله
                            theOnePerson.DocumentPersonTypeId == "79" ||   // ورثه متعهدله
                            theOnePerson.DocumentPersonTypeId == "45" ||   // رضايت گيرنده
                            theOnePerson.DocumentPersonTypeId == "66" ||   // موصي له
                            theOnePerson.DocumentPersonTypeId == "33"      // وصی
                            ))
                            continue;

                        MessageInputPacket theOneMessageInputPacket = new MessageInputPacket();
                        theOneMessageInputPacket.MessageContext = "هزینه ثبت " +
                                                                  mainSMSRequest.MainEntity.DocumentType.Title +
                                                                  " با شناسه " + mainSMSRequest.MainEntity.NationalNo +
                                                                  " توسط دفترخانه " +/* mainSMSRequest.MainEntity.TheScriptorium.ScriptoriumNo + " " + mainSMSRequest.MainEntity.TheScriptorium.TheGeoLocation.LocationName +*/_userService.UserApplicationContext?.ScriptoriumInformation?.ScriptoriumNo + " " + _userService.UserApplicationContext?.ScriptoriumInformation?.GeoLocationName +
                                                                  " " + mainSMSRequest.MainEntity.SumPrices().ToCommaString() + " ریال می باشد." +
                                                                  "\nتلفن: " + _userService.UserApplicationContext?.ScriptoriumInformation?.Tel /*mainSMSRequest.MainEntity.TheScriptorium.Te*/ +
                                                                  "\nwww.SSAA.ir";

                        theOneMessageInputPacket.RecipientPhoneNo = theOnePerson.MobileNo;
                        theOneMessageInputPacket.RecipientFullName = theOnePerson.FullName();
                        theOneMessageInputPacket.DocNationalNo = mainSMSRequest.MainEntity.NationalNo;
                        theOneMessageInputPacket.DocObjectID = mainSMSRequest.MainEntity.Id.ToString();
                        theOneMessageInputPacket.MainEntity = mainSMSRequest.MainEntity;

                        bool smsIsSent = await smsGeneratorEngine.GenerateCoreSMS(theOneMessageInputPacket, SMSCostPolicy.Non_Free);

                        if (smsIsSent)
                        {
                            DocumentSm theOneRegisterSerivceSMS = new();
                            theOneRegisterSerivceSMS.DocumentId = mainSMSRequest.MainEntity.Id; ;
                            theOneRegisterSerivceSMS.Id = Guid.NewGuid();
                            theOneRegisterSerivceSMS.IsMechanized = "1";
                            theOneRegisterSerivceSMS.IsSent = "1";
                            theOneRegisterSerivceSMS.Ilm = "1";
                            theOneRegisterSerivceSMS.Id = Guid.NewGuid();
                            theOneRegisterSerivceSMS.SmsText = theOneMessageInputPacket.MessageContext;
                            theOneRegisterSerivceSMS.ReceiverName = theOneMessageInputPacket.RecipientFullName;
                            theOneRegisterSerivceSMS.MobileNo = theOneMessageInputPacket.RecipientPhoneNo;
                            theOneRegisterSerivceSMS.CreateDate = dateTimeService.CurrentPersianDate;
                            theOneRegisterSerivceSMS.CreateTime = dateTimeService.CurrentTime.Substring(0, 5);
                            theOneRegisterSerivceSMS.RecordDate = dateTimeService.CurrentDateTime;
                            theOneRegisterSerivceSMS.ScriptoriumId =
                                _userService.UserApplicationContext.ScriptoriumInformation.Code;
                            documentSMSRepository.Add(theOneRegisterSerivceSMS, false);

                        }
                        else
                        {
                            DocumentSm theOneRegisterSerivceSMS = new();
                            theOneRegisterSerivceSMS.DocumentId = mainSMSRequest.MainEntity.Id; ;
                            theOneRegisterSerivceSMS.Id = Guid.NewGuid();
                            theOneRegisterSerivceSMS.IsMechanized = "1";
                            theOneRegisterSerivceSMS.IsSent = "2";
                            theOneRegisterSerivceSMS.Ilm = "1";
                            theOneRegisterSerivceSMS.Id = Guid.NewGuid();
                            theOneRegisterSerivceSMS.SmsText = theOneMessageInputPacket.MessageContext;
                            theOneRegisterSerivceSMS.ReceiverName = theOneMessageInputPacket.RecipientFullName;
                            theOneRegisterSerivceSMS.MobileNo = theOneMessageInputPacket.RecipientPhoneNo;
                            theOneRegisterSerivceSMS.CreateDate = dateTimeService.CurrentPersianDate;
                            theOneRegisterSerivceSMS.CreateTime = dateTimeService.CurrentTime.Substring(0, 5);
                            theOneRegisterSerivceSMS.RecordDate = dateTimeService.CurrentDateTime;
                            theOneRegisterSerivceSMS.ScriptoriumId =
                                _userService.UserApplicationContext.ScriptoriumInformation.Code;
                            documentSMSRepository.Add(theOneRegisterSerivceSMS, false);
                        }
                    }

                    messageOutputPacket.MessageGenerated = true;
                }

            }
            catch (Exception)
            {
                //Implement Exception Code Here......
                messageOutputPacket.MessageGenerated = false;
            }

            return (messageOutputPacket, mainSMSRequest);
        }

        //internal string GenerateSMSEntity(MessageInputPacket messageInputPacket)
        //{
        //    ICMSOrganization cMSOrganization = Rad.CMS.InstanceBuilder.GetEntityByCode<ICMSOrganization>(messageInputPacket.MainEntity.ScriptoriumId, BaseInfoQuery.CMSOrganization.ScriptoriumId);
        //    if (cMSOrganization != null)
        //    {
        //        ITrmSMS SMS = Rad.CMS.InstanceBuilder.NewEntity<ITrmSMS>();
        //        SMS.TheCreatedServer = cMSOrganization.TheCaseServer;
        //        SMS.CreatedServerId = cMSOrganization.TheCaseServer.ObjectId;
        //        SMS.TheCreatedOrg = cMSOrganization;
        //        SMS.CreatedOrgId = cMSOrganization.ObjectId;
        //        SMS.SMSStatus = CMSSMSStatus.NotSended;
        //        SMS.CreateDateTime = Rad.CMS.BaseInfoContext.Instance.CurrentDateTime;
        //        SMS.DocId = messageInputPacket.MainEntity.ObjectId;
        //        SMS.DocNo = messageInputPacket.MainEntity.NationalNo;
        //        SMS.ReceiverName = messageInputPacket.RecipientFullName;
        //        SMS.MobileNumber4SMS = messageInputPacket.RecipientPhoneNo;
        //        SMS.SMSText = messageInputPacket.MessageContext;

        //        ITrmSMSNotSend SMSNotSend = Rad.CMS.InstanceBuilder.NewEntity<ITrmSMSNotSend>();
        //        SMSNotSend.TrmSMSId = SMS.ObjectId;
        //        SMSNotSend.DispatchDateTime = Rad.CMS.BaseInfoContext.Instance.CurrentDateTime;

        //        return SMS.ObjectId;
        //    }

        //    return string.Empty;
        //}

        /// <summary>
        /// The ImplementAzl_EstefaMessageContext
        /// </summary>
        /// <param name="messageInputPacket">The messageInputPacket<see cref="MessageInputPacket?"/></param>
        private void ImplementAzl_EstefaMessageContext(ref MessageInputPacket? messageInputPacket)
        {

            if (messageInputPacket != null && messageInputPacket.RecipientDocPersonTypeCode == "59" ||
                messageInputPacket?.RecipientDocPersonTypeCode == "17")
            {
                string context = "درخواست عزل وکیل برای سند وکالتنامه " + messageInputPacket.MainEntity.RelatedDocumentNo + " توسط موکل در تاریخ " + dateTimeService.CurrentDate + " در سامانه ثبت الکترونیک اسناد به ثبت رسید." + " \nسازمان ثبت اسناد و املاک کشور";
                messageInputPacket.MessageContext = context;
            }

            if (messageInputPacket != null && messageInputPacket.RecipientDocPersonTypeCode == "16")
            {
                string context = "درخواست استعفای وکیل برای سند وکالتنامه " + messageInputPacket.MainEntity.RelatedDocumentNo + " در تاریخ " + dateTimeService.CurrentDate + " در سامانه ثبت الکترونیک اسناد به ثبت رسید." + " \nسازمان ثبت اسناد و املاک کشور";
                messageInputPacket.MessageContext = context;
            }
        }
    }
}
