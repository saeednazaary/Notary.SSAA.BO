namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core
{
    using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;

    /// <summary>
    /// Defines the <see cref="DataCollector" />
    /// </summary>
    public class DataCollector
    {
        /// <summary>
        /// Defines the annotationsController
        /// </summary>
        private readonly  AnnotationsController annotationsController;

        /// <summary>
        /// Defines the documentRepository
        /// </summary>
        private readonly IDocumentRepository documentRepository;

        /// <summary>
        /// Defines the documentPersonRepository
        /// </summary>
        private readonly IDocumentPersonRepository documentPersonRepository;

        /// <summary>
        /// Defines the documentPersonRelatedRepository
        /// </summary>
        private readonly IDocumentPersonRelatedRepository documentPersonRelatedRepository;

        /// <summary>
        /// Defines the userService
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCollector"/> class.
        /// </summary>
        /// <param name="_documentRepository">The _documentRepository<see cref="IDocumentRepository"/></param>
        /// <param name="_userService">The _userService<see cref="IUserService"/></param>
        /// <param name="_documentPersonRepository">The _documentPersonRepository<see cref="IDocumentPersonRepository"/></param>
        /// <param name="_documentPersonRelatedRepository">The _documentPersonRelatedRepository<see cref="IDocumentPersonRelatedRepository"/></param>
        /// <param name="_annotationsController">The _annotationsController<see cref="AnnotationsController"/></param>
        public DataCollector ( IDocumentRepository _documentRepository, IUserService _userService, IDocumentPersonRepository _documentPersonRepository, IDocumentPersonRelatedRepository _documentPersonRelatedRepository, AnnotationsController _annotationsController )
        {
            documentRepository = _documentRepository;
            userService = _userService;
            documentPersonRepository = _documentPersonRepository;
            documentPersonRelatedRepository = _documentPersonRelatedRepository;
            annotationsController = _annotationsController;
        }

        /// <summary>
        /// The CollectData
        /// </summary>
        /// <param name="dataCollectionRequestPacket">The dataCollectionRequestPacket<see cref="DataCollectionRequestPacket"/></param>
        /// <returns>The <see cref="Task{DataCollectionResponsePacket}"/></returns>
        public async Task<DataCollectionResponsePacket> CollectData ( DataCollectionRequestPacket dataCollectionRequestPacket )
        {
            DataCollectionResponsePacket dataCollectionResponsePacket = new DataCollectionResponsePacket();
            //if (!string.IsNullOrWhiteSpace(connectionSwitcher.CurrentConnectionString))
            {
                if ( !string.IsNullOrWhiteSpace ( dataCollectionRequestPacket.DocumentNationalNo ) )
                {
                    if ( dataCollectionRequestPacket.DocumentNationalNo.Length < 18 )
                    {
                        dataCollectionResponsePacket.Succeeded = false;
                        dataCollectionResponsePacket.ServiceResponse = "شماره شناسه وارد شده برای سند وکالت، صحیح نمی باشد.";
                        return dataCollectionResponsePacket;
                    }
                }

                string scriptoriumCode = !string.IsNullOrWhiteSpace(dataCollectionRequestPacket.DocumentNationalNo) ? dataCollectionRequestPacket.DocumentNationalNo.Substring(7, 5) : string.Empty;

                string errorMessage = string.Empty;
                DocumentResponseMessage? documentResponseMessage=    await     this.GetLegalText ( dataCollectionRequestPacket.DocumentNationalNo, dataCollectionRequestPacket.DocumentSecretCode, dataCollectionRequestPacket.IncludeRegCaseDescription );
                if ( documentResponseMessage != null )
                    dataCollectionResponsePacket.Context = ( string ) documentResponseMessage.MainEntity;
                if ( string.IsNullOrEmpty ( dataCollectionResponsePacket.Context ) )
                {
                    dataCollectionResponsePacket.ServiceResponse = documentResponseMessage?.Message;
                    dataCollectionResponsePacket.Succeeded = false;
                }
                else
                {
                    dataCollectionResponsePacket.Succeeded = true;
                }

            }

            return dataCollectionResponsePacket;
        }

        /// <summary>
        /// The CollectSMSRecipients
        /// </summary>
        /// <param name="documnetNationalNo">The documnetNationalNo<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{SMSRecipientPacket}?}"/></returns>
        public async Task<List<SMSRecipientPacket>?> CollectSMSRecipients ( string documnetNationalNo )
        {
            List<SMSRecipientPacket>? smsPacketsCollection = new List<SMSRecipientPacket>{ };

            smsPacketsCollection = await CollectSMSRecipientPackets ( documnetNationalNo );

            return smsPacketsCollection;
        }

        /// <summary>
        /// The CollectDocumentAnnotations
        /// </summary>
        /// <param name="classifyNo">The classifyNo<see cref="string"/></param>
        /// <param name="nationalno">The nationalno<see cref="string"/></param>
        /// <param name="scriptoriumID">The scriptoriumID<see cref="string"/></param>
        /// <returns>The <see cref="Task {List{string}?}"/></returns>
        public async Task<List<string>?> CollectDocumentAnnotations ( string classifyNo, string nationalno, string scriptoriumID )
        {
            List<AnnotationPack>? annotaionsList =await annotationsController.CollectAnnotations(classifyNo, scriptoriumID, nationalno);
            if ( annotaionsList == null || annotaionsList.Count == 0 )
                return null;

            List<string> annotationsContexes = new List<string>(){ };
            foreach ( var item in annotaionsList )
            {

                annotationsContexes.Add ( item.RelatedDocContext );
            }

            return annotationsContexes;
        }

        /// <summary>
        /// The CollectSMSRecipientPackets
        /// </summary>
        /// <param name="documnetNationalNo">The documnetNationalNo<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{SMSRecipientPacket}?}"/></returns>
        private async Task<List<SMSRecipientPacket>?> CollectSMSRecipientPackets ( string documnetNationalNo )
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            var document=  await documentRepository.GetDocumentByNationalNo (new List<string>(){ "DocumentPeople" }, documnetNationalNo, token );

            if ( document == null )
                return null;

            List<SMSRecipientPacket> smsRecipientsCollection = new List<SMSRecipientPacket>(){ };
            foreach ( var theOnePerson in document.DocumentPeople )
            {
                if ( string.IsNullOrWhiteSpace ( theOnePerson.MobileNo ) || theOnePerson.MobileNo.Length < 10 )
                    continue;


                if ( this.EnsureNotRepeatedSMSPacket ( smsRecipientsCollection, theOnePerson.MobileNo ) )
                {
                    SMSRecipientPacket theOneSMSPacket = new SMSRecipientPacket();
                    theOneSMSPacket.RecipientMobileNo = theOnePerson.MobileNo;
                    theOneSMSPacket.RecipientFullName = theOnePerson.FullName ();
                    if ( theOnePerson.DocumentPersonTypeId != null )
                    {
                        theOneSMSPacket.RecipientPersonTypeCode = theOnePerson.DocumentPersonTypeId;
                    }

                    smsRecipientsCollection.Add ( theOneSMSPacket );
                }
            }

            return smsRecipientsCollection;
        }

        /// <summary>
        /// The EnsureNotRepeatedSMSPacket
        /// </summary>
        /// <param name="theSMSPacketsCollection">The theSMSPacketsCollection<see cref="List{SMSRecipientPacket}"/></param>
        /// <param name="mobileNo">The mobileNo<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool EnsureNotRepeatedSMSPacket ( List<SMSRecipientPacket> theSMSPacketsCollection, string mobileNo )
        {
            foreach ( SMSRecipientPacket theOnePacket in theSMSPacketsCollection )
            {
                if ( theOnePacket.RecipientMobileNo == mobileNo )
                    return false;
            }

            return true;
        }

        /// <summary>
        /// The GetLegalText
        /// </summary>
        /// <param name="documnetNationalNo">The documnetNationalNo<see cref="string"/></param>
        /// <param name="documnetSecretCode">The documnetSecretCode<see cref="string"/></param>
        /// <param name="includeRegCases">The includeRegCases<see cref="bool"/></param>
        /// <returns>The <see cref="Task{DocumentResponseMessage?}"/></returns>
        private async Task<DocumentResponseMessage?> GetLegalText ( string documnetNationalNo, string documnetSecretCode, bool includeRegCases )
        {
            DocumentResponseMessage documentResponseMessage = new DocumentResponseMessage();
            string legalText = string.Empty;

            try
            {
                CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken token = source.Token;
                var document=  await documentRepository.GetDocumentByNationalNo (new List<string>(){ "DocumentInfoText","DocumentCases" }, documnetNationalNo, token );
                if ( document == null )
                {
                    documentResponseMessage.Message = "سند با شناسه یکتای " + documnetNationalNo + " یافت نشد!";
                    return documentResponseMessage;
                }

                if ( document.DocumentSecretCode != documnetSecretCode )
                {
                    documentResponseMessage.Message = "رمز تصدیق وارد شده اشتباه می باشد!";
                    return documentResponseMessage;
                }

                if ( document.State != NotaryRegServiceReqState.Finalized.GetString () )
                {
                    documentResponseMessage.Message = "سند مورد درخواست، تایید نهایی نشده است!";
                    return documentResponseMessage;
                }

                if ( string.IsNullOrEmpty ( document?.DocumentInfoText?.LegalText ) )
                {
                    legalText = "متن حقوقی سند درخواست شده، مشخص نشده است.";
                    documentResponseMessage.Message = legalText;
                    return documentResponseMessage;
                }

                string? regCases = null;
                if ( includeRegCases && document.DocumentCases != null )
                {
                    foreach ( var theOneCase in document.DocumentCases )
                    {
                        if ( string.IsNullOrWhiteSpace ( regCases ) )
                            regCases = theOneCase.Title + " : " + Environment.NewLine;

                        regCases += " - " + theOneCase.RegCaseText () + Environment.NewLine;
                    }

                }

                legalText =
                   ( includeRegCases && !string.IsNullOrWhiteSpace ( regCases ) ? regCases + Environment.NewLine : string.Empty ) +
                   Environment.NewLine +
                   document?.DocumentInfoText?.LegalText;
                documentResponseMessage.MainEntity = legalText;

                return documentResponseMessage;
            }
            catch ( Exception )
            {
                documentResponseMessage.Message = "خطا در اخذ متن حقوقی سند مورد درخواست!";
                return null;
            }
        }
    }
}
