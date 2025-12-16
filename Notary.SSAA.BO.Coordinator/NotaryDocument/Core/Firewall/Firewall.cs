namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Firewall
{
    using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="Firewall" />
    /// </summary>
    public class Firewall
    {
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
        /// Defines the dateTimeService
        /// </summary>
        private readonly IDateTimeService dateTimeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Firewall"/> class.
        /// </summary>
        /// <param name="_documentRepository">The _documentRepository<see cref="IDocumentRepository"/></param>
        /// <param name="_userService">The _userService<see cref="IUserService"/></param>
        /// <param name="_dateTimeService">The _dateTimeService<see cref="IDateTimeService"/></param>
        /// <param name="_documentPersonRepository">The _documentPersonRepository<see cref="IDocumentPersonRepository"/></param>
        /// <param name="_documentPersonRelatedRepository">The _documentPersonRelatedRepository<see cref="IDocumentPersonRelatedRepository"/></param>
        public Firewall ( IDocumentRepository _documentRepository, IUserService _userService, IDateTimeService _dateTimeService, IDocumentPersonRepository _documentPersonRepository, IDocumentPersonRelatedRepository _documentPersonRelatedRepository )
        {
            documentRepository = _documentRepository;
            userService = _userService;
            documentPersonRepository = _documentPersonRepository;
            documentPersonRelatedRepository = _documentPersonRelatedRepository;
            dateTimeService = _dateTimeService;
        }

        /// <summary>
        /// The AuthorizeRelatedDocumentValidationResponse
        /// </summary>
        /// <param name="agentDocumentNationalNo">The agentDocumentNationalNo<see cref="string"/></param>
        /// <returns>The <see cref="Task{(bool,AgentDocValidationResult)}"/></returns>
        public async Task<(bool, AgentDocValidationResult)> AuthorizeRelatedDocumentValidationResponse ( string agentDocumentNationalNo )
        {
            AgentDocValidationResult agentDocValidationResult=new AgentDocValidationResult();
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            var limit= await documentRepository.GetDocumentLimitation ( agentDocumentNationalNo, token );

            //در صورتی که اسناد به صورت سیستمی در سازمان توقیف شده باشند، باید این امکان وجود داشته باشد که برخی خدمات تبعی ثبت گردد. 
            //مانند بی اعتباری، چون بی اعتباری در واقع حکم دادگاه مبنی بر ابطال سند صادر شده می باشد پس نباید توقیف سند، مانع ثبت بی اعتباری گردد.

            if ( limit != null )
            {
                agentDocValidationResult.MessageText = "\n*******تذکر مهم*******\n" + limit.Description + "\n";
                agentDocValidationResult.RegisterServiceReqID = null;
                agentDocValidationResult.ResultRestrictionLevel = RestrictionLevel.Avoidance;

                return (false, agentDocValidationResult);
            }
            else
            {
                return (true, agentDocValidationResult);
            }
        }

        /// <summary>
        /// The AuthorizeRelatedDocumentValidationResponse
        /// </summary>
        /// <param name="agentDocumentNationalNo">The agentDocumentNationalNo<see cref="string"/></param>
        /// <param name="currentDocumentTypeID">The currentDocumentTypeID<see cref="string"/></param>
        /// <returns>The <see cref="Task{RelatedDocValidationResult?}"/></returns>
        public async Task<RelatedDocValidationResult?> AuthorizeRelatedDocumentValidationResponse ( string agentDocumentNationalNo, string currentDocumentTypeID )
        {
            RelatedDocValidationResult relatedDocValidationResult=new RelatedDocValidationResult();
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            var limit= await documentRepository.GetDocumentLimitation ( agentDocumentNationalNo, token );

            //در صورتی که اسناد به صورت سیستمی در سازمان توقیف شده باشند، باید این امکان وجود داشته باشد که برخی خدمات تبعی ثبت گردد. 
            //مانند بی اعتباری، چون بی اعتباری در واقع حکم دادگاه مبنی بر ابطال سند صادر شده می باشد پس نباید توقیف سند، مانع ثبت بی اعتباری گردد.
            List<string> exceptedDocumentTypes = new List<string>()
            {
                "AE1549A3AC7F4EAE84D1F6FF67783980",  // اعلام بی اعتباری سند 
                "A0AC1FE6D428497BB82BDD16B98C7441"   // اعلام بي اعتباري بخشي از سند
            };

            if ( limit != null && !exceptedDocumentTypes.Contains ( currentDocumentTypeID ) )
            {
                relatedDocValidationResult.ValidationMessage += "\n*******تذکر مهم*******\n" + limit.Description + "\n";
                relatedDocValidationResult.RelatedRegisterServiceReqObjectID = null;
                relatedDocValidationResult.IsValid = false;

                return relatedDocValidationResult;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// The AuthorizeDocument
        /// </summary>
        /// <param name="documentNationalNo">The documentNationalNo<see cref="string"/></param>
        /// <returns>The <see cref="Task{RelatedDocValidationResult}"/></returns>
        public async Task<RelatedDocValidationResult> AuthorizeDocument ( string documentNationalNo )
        {
            RelatedDocValidationResult relatedDocValidationResult=new RelatedDocValidationResult();

            try
            {
                CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken token = source.Token;
                var limit= await documentRepository.GetDocumentLimitation ( documentNationalNo, token );
                if ( limit != null )
                {
                    relatedDocValidationResult.ValidationMessage = "\n*******تذکر مهم*******\n" + limit.Description + "\n";
                    relatedDocValidationResult.IsValid = false;
                }
                else
                {
                    relatedDocValidationResult.IsValid = true;
                }
            }
            catch
            {
                relatedDocValidationResult.IsValid = false;
                relatedDocValidationResult.ValidationMessage += "خطا در بررسی مجوزهای سند مورد استعلام! لطفاً مجدداً تلاش نمایید.";
            }
            return ( relatedDocValidationResult );
        }
    }

}
