namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Validators.Core
{
    using MediatR;
    using Notary.SSAA.BO.Configuration;
    using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
    using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.SharedKernel.Result;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Document = Notary.SSAA.BO.Domain.Entities.Document;
    using DocumentType = Notary.SSAA.BO.Domain.Entities.DocumentType;

    /// <summary>
    /// Defines the <see cref="AgentDocumentValidatorCore" />
    /// </summary>
    public class AgentDocumentValidatorCore
    {
        /// <summary>
        /// Defines the willDocValidatorConfiguration
        /// </summary>
        public WillDocValidatorConfiguration willDocValidatorConfiguration;

        /// <summary>
        /// Defines the agentDocValidatorConfiguration
        /// </summary>
        internal AgentDocValidatorConfiguration? agentDocValidatorConfiguration = null;

        /// <summary>
        /// Defines the _theFoundRegisterServiceReqsCollection
        /// </summary>
        public List<Document> _theFoundRegisterServiceReqsCollection = new List<Document>(){ };

        /// <summary>
        /// Defines the _theParentRegisterServiceReqDocumentType
        /// </summary>
        public DocumentType? _theParentRegisterServiceReqDocumentType;

        /// <summary>
        /// Defines the firewall
        /// </summary>
        public Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Firewall.Firewall firewall;

        /// <summary>
        /// Defines the documentRepository
        /// </summary>
        private readonly IDocumentRepository documentRepository;

        /// <summary>
        /// Defines the mediator
        /// </summary>
        private readonly IMediator mediator;

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
        /// Initializes a new instance of the <see cref="AgentDocumentValidatorCore"/> class.
        /// </summary>
        /// <param name="_documentRepository">The _documentRepository<see cref="IDocumentRepository"/></param>
        /// <param name="_userService">The _userService<see cref="IUserService"/></param>
        /// <param name="_dateTimeService">The _dateTimeService<see cref="IDateTimeService"/></param>
        /// <param name="_documentPersonRepository">The _documentPersonRepository<see cref="IDocumentPersonRepository"/></param>
        /// <param name="_documentPersonRelatedRepository">The _documentPersonRelatedRepository<see cref="IDocumentPersonRelatedRepository"/></param>
        /// <param name="_agentDocValidatorConfiguration">The _agentDocValidatorConfiguration<see cref="AgentDocValidatorConfiguration"/></param>
        /// <param name="_willDocValidatorConfiguration">The _willDocValidatorConfiguration<see cref="WillDocValidatorConfiguration"/></param>
        /// <param name="_firewall">The _firewall<see cref="Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Firewall.Firewall"/></param>
        /// <param name="_mediator">The _mediator<see cref="IMediator"/></param>
        public AgentDocumentValidatorCore ( IDocumentRepository _documentRepository, IUserService _userService,
            IDateTimeService _dateTimeService, IDocumentPersonRepository _documentPersonRepository,
            IDocumentPersonRelatedRepository _documentPersonRelatedRepository,
            AgentDocValidatorConfiguration _agentDocValidatorConfiguration,
            WillDocValidatorConfiguration _willDocValidatorConfiguration,
            Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Firewall.Firewall _firewall,
            IMediator _mediator )
        {
            documentRepository = _documentRepository;
            userService = _userService;
            documentPersonRepository = _documentPersonRepository;
            documentPersonRelatedRepository = _documentPersonRelatedRepository;
            dateTimeService = _dateTimeService;
            willDocValidatorConfiguration = _willDocValidatorConfiguration;
            agentDocValidatorConfiguration = _agentDocValidatorConfiguration;
            firewall = _firewall;
            mediator = _mediator;
        }

        //internal AgentDocumentValidatorCore ( DocValidationCommonData validationCommonDataPack )
        //{
        //    _theFoundRegisterServiceReqsCollection = validationCommonDataPack.TheFoundRegisterServiceReqsCollection;
        //    _theParentRegisterServiceReqDocumentType = validationCommonDataPack.TheParentRegisterServiceReqDocumentType;
        //}

        /// <summary>
        /// The ValidateAgentDocument
        /// </summary>
        /// <param name="inputAgentDocumentNationalNo">The inputAgentDocumentNationalNo<see cref="string"/></param>
        /// <param name="inputAgentDocumentScriptoriumId">The inputAgentDocumentScriptoriumId<see cref="string"/></param>
        /// <param name="inputAgentDocumentDate">The inputAgentDocumentDate<see cref="string"/></param>
        /// <param name="inputDocumentTypeID">The inputDocumentTypeID<see cref="string"/></param>
        /// <param name="inputMovakelNationalNo">The inputMovakelNationalNo<see cref="string"/></param>
        /// <param name="inputVakilNationalNo">The inputVakilNationalNo<see cref="string"/></param>
        /// <param name="inputMovakelFullName">The inputMovakelFullName<see cref="string"/></param>
        /// <param name="inputVakilFullName">The inputVakilFullName<see cref="string"/></param>
        /// <param name="inputRegCaseList">The inputRegCaseList<see cref="List{RegCasePacket}"/></param>
        /// <returns>The <see cref="Task{AgentDocValidationResult}"/></returns>
        internal virtual async Task<AgentDocValidationResult> ValidateAgentDocument (
                                                                string inputAgentDocumentNationalNo,
                                                                string inputAgentDocumentScriptoriumId,
                                                                string inputAgentDocumentDate,
                                                                string inputDocumentTypeID,
                                                                string inputMovakelNationalNo,
                                                                string inputVakilNationalNo,
                                                                string inputMovakelFullName,
                                                                string inputVakilFullName,
                                                                List<RegCasePacket> inputRegCaseList
                                                                )
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = source.Token;
            AgentDocValidationResult agentDocValidationResult = new AgentDocValidationResult();
            string pMessageText = string.Empty;
            RequsetType? requestType=null;

            try
            {
                if ( agentDocValidatorConfiguration?.ValidatorEnabled == RestrictionLevel.Disabled )
                {
                    agentDocValidationResult.DocExists = true;
                    agentDocValidationResult.ResultRestrictionLevel = RestrictionLevel.Pass;
                    agentDocValidationResult.RegisterServiceReqID = string.Empty;
                    if ( agentDocValidatorConfiguration.AppendErrorCode )
                    {
                        agentDocValidationResult.MessageText = "#VALIDATOR-CODE:00\n";

                    }
                    else
                    {
                    agentDocValidationResult.MessageText = pMessageText;

                    }


                    return agentDocValidationResult;
                }

                Document? foundRegisterServiceReq = null;
                requestType = await documentRepository.GetRequestType ( cancellationToken, null, inputAgentDocumentNationalNo );

                if ( _theFoundRegisterServiceReqsCollection != null && _theFoundRegisterServiceReqsCollection.Count > 0 )
                {
                    foreach ( Document theOneFoundReq in _theFoundRegisterServiceReqsCollection )
                    {
                        if ( theOneFoundReq.NationalNo == inputAgentDocumentNationalNo )
                        {
                            foundRegisterServiceReq = theOneFoundReq;
                            break;
                        }
                    }
                }

                if ( foundRegisterServiceReq == null )
                {
                    if ( requestType != null )
                    {
                        if ( requestType.HasCases () )
                        {
                            foundRegisterServiceReq = await documentRepository.GetDocumentByNationalNo ( new List<string> { "DocumentPeople", "DocumentCases", "DocumentInfoOther" }, inputAgentDocumentNationalNo, cancellationToken );

                        }
                        if ( requestType.HasVehicles () )
                        {
                            foundRegisterServiceReq = await documentRepository.GetDocumentByNationalNo ( new List<string> { "DocumentPeople", "DocumentVehicles", "DocumentInfoOther" }, inputAgentDocumentNationalNo, cancellationToken );

                        }
                        if ( requestType.HasEstates () )
                        {
                            foundRegisterServiceReq = await documentRepository.GetDocumentByNationalNo ( new List<string> { "DocumentPeople", "DocumentEstates", "DocumentInfoOther" }, inputAgentDocumentNationalNo, cancellationToken );

                        }

                        else
                        {
                            foundRegisterServiceReq = await documentRepository.GetDocumentByNationalNo ( new List<string> { "DocumentPeople" }, inputAgentDocumentNationalNo, cancellationToken );

                        }

                    }

                    ///اگر سندی که پیدا می شود از نوع عزل و یا استعفای وکیل باشد باید بدین صورت عمل کرد.
                    if ( foundRegisterServiceReq != null &&
                        ( foundRegisterServiceReq.DocumentTypeId == "006" || foundRegisterServiceReq.DocumentTypeId == "0022" ) )
                    {
                        (agentDocValidationResult, pMessageText) = await this.ValidateAzlOrEstefaDocument (
                            foundRegisterServiceReq,
                            inputAgentDocumentNationalNo,
                            inputAgentDocumentScriptoriumId,
                            inputAgentDocumentDate,
                            inputDocumentTypeID,
                            inputMovakelNationalNo,
                            inputVakilNationalNo,
                            inputMovakelFullName,
                            inputVakilFullName,
                            pMessageText
                            );

                        return agentDocValidationResult;
                    }
                }

                if ( foundRegisterServiceReq == null )
                {
                    pMessageText += "\t - وکالتنامه مورد استناد در بخش اشخاص، با شماره شناسه " + inputAgentDocumentNationalNo + "  در سامانه ثبت الکترونیک اسناد ثبت نشده است." + System.Environment.NewLine;
                    agentDocValidationResult.DocExists = false;
                    if ( agentDocValidatorConfiguration != null )
                        agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V1;
                    if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                    {
                        agentDocValidationResult.MessageText += "#VALIDATOR-CODE:01\n";
                    }
                    else
                    {
                        agentDocValidationResult.MessageText = pMessageText;
                    }

                    if ( agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance )
                        return agentDocValidationResult;

                }

                if ( requestType?.DocumentTypeGroup1Id != "3" )
                {
                    pMessageText += "\t - سند مورد استناد در بخش اشخاص، با شماره شناسه " + inputAgentDocumentNationalNo + " یک وکالتنامه نیست." + System.Environment.NewLine;
                    agentDocValidationResult.DocExists = false;
                    if ( agentDocValidatorConfiguration != null )
                        agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V2;
                    if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                    {
                        agentDocValidationResult.MessageText += "#VALIDATOR-CODE:02\n";

                    }
                    else { 
                    
                    agentDocValidationResult.MessageText = pMessageText;
                    }

                    if ( agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance )
                        return agentDocValidationResult;
                }

                if ( foundRegisterServiceReq?.State != NotaryRegServiceReqState.Finalized.GetString () )
                {
                    pMessageText += "\t - سند مورد استناد در بخش اشخاص، با شماره شناسه " + inputAgentDocumentNationalNo + " تایید نهایی نشده است." + System.Environment.NewLine;
                    agentDocValidationResult.DocExists = true;
                    if ( agentDocValidatorConfiguration != null )
                        agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V3;
                    if ( agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance )
                    {
                        agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq?.Id.ToString ();
                    }
                    if (agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode)
                    {

                        agentDocValidationResult.MessageText += "#VALIDATOR-CODE:03\n";
                    }
                    else
                    {
                        agentDocValidationResult.MessageText = pMessageText;
                    }
                    if ( agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance )
                        return agentDocValidationResult;
                }

                if ( foundRegisterServiceReq?.ScriptoriumId != inputAgentDocumentScriptoriumId )
                {
                    pMessageText += "\t - دفترخانه تنظیم کننده " + "سند مورد استناد در بخش اشخاص، با شماره شناسه " + inputAgentDocumentNationalNo + " صحیح نمی باشد." + System.Environment.NewLine;
                    agentDocValidationResult.DocExists = true;
                    if ( agentDocValidatorConfiguration != null )

                        agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V4;
                    if ( agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance )
                    {
                        agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq?.Id.ToString ();
                    }
                    if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                    {
                        agentDocValidationResult.MessageText += "#VALIDATOR-CODE:04\n";
                    }
                    else
                    {
                    agentDocValidationResult.MessageText = pMessageText;
                    }

                    if ( agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance )
                        return agentDocValidationResult;
                }

                if ( foundRegisterServiceReq?.DocumentDate != inputAgentDocumentDate && foundRegisterServiceReq?.WriteInBookDate != inputAgentDocumentDate )
                {
                    pMessageText += "\t - تاریخ سند مورد استناد در بخش اشخاص، با شماره شناسه " + inputAgentDocumentNationalNo + " صحیح نمی باشد." + System.Environment.NewLine;
                    agentDocValidationResult.DocExists = true;
                    if ( agentDocValidatorConfiguration != null )
                        agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V5;
                    if ( agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance )
                    {
                        agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq?.Id.ToString ();
                    }
                    if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode)
                    {
                        agentDocValidationResult.MessageText += "#VALIDATOR-CODE:05\n";
                    }
                    else
                    {
                        agentDocValidationResult.MessageText = pMessageText;
                    }

                    if ( agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance )
                        return agentDocValidationResult;
                }

                bool vakilIsFound = false;
                bool movakelIsFound = false;
                int vakilsCount = 0;
                int movakelsCount = 0;
                if ( foundRegisterServiceReq != null )
                {
                    foreach ( DocumentPerson docPerson in foundRegisterServiceReq.DocumentPeople )
                    {
                        if ( docPerson.DocumentPersonTypeId != null )
                        {
                            if ( docPerson.DocumentPersonTypeId == "16" )    // وکیل
                            {
                                ++vakilsCount;
                                if ( docPerson.NationalNo == inputVakilNationalNo )
                                    vakilIsFound = true;
                            }
                            if ( docPerson.DocumentPersonTypeId == "17" ||    // موکل
                                docPerson.DocumentPersonTypeId == "59" )      // اولين موکل
                            {
                                ++movakelsCount;
                                if ( docPerson.NationalNo == inputMovakelNationalNo )
                                    movakelIsFound = true;
                            }
                        }
                    }

                }
                if ( vakilsCount > 1 )
                {
                    pMessageText += "\t - در وکالتنامه مورد استناد در بخش اشخاص با شناسه یکتای " + foundRegisterServiceReq?.NationalNo + " ، مشخصات  " + vakilsCount.ToString () + " نفر بعنوان وکیل ثبت شده است." + System.Environment.NewLine;
                }
                if ( movakelsCount > 1 )
                {
                    pMessageText += "\t - در وکالتنامه مورد استناد در بخش اشخاص با شناسه یکتای " + foundRegisterServiceReq?.NationalNo + " ، مشخصات  " + movakelsCount.ToString () + " نفر بعنوان موکل ثبت شده است." + System.Environment.NewLine;
                }
                if ( !vakilIsFound || !movakelIsFound )
                {
                    if ( !vakilIsFound )
                    {
                        pMessageText += "\t - در وکالتنامه مورد استناد در بخش اشخاص با شناسه یکتای " + foundRegisterServiceReq?.NationalNo + " ، مشخصات  " + inputVakilFullName + " بعنوان وکیل ثبت نشده است." + System.Environment.NewLine;
                    }
                    if ( !movakelIsFound )
                    {
                        pMessageText += "\t - در وکالتنامه مورد استناد در بخش اشخاص با شناسه یکتای " + foundRegisterServiceReq?.NationalNo + " ، مشخصات  " + inputMovakelFullName + " بعنوان موکل ثبت نشده است." + System.Environment.NewLine;
                    }
                    agentDocValidationResult.DocExists = true;
                    if ( agentDocValidatorConfiguration != null )

                        agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V6;
                    if ( agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance )
                    {
                        agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq?.Id.ToString ();
                    }
                    if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                    {
                        agentDocValidationResult.MessageText += "#VALIDATOR-CODE:06\n";
                    }
                    else
                    {
                        agentDocValidationResult.MessageText = pMessageText;
                    }

                    if ( agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance )
                        return agentDocValidationResult;
                }

                if ( foundRegisterServiceReq?.DocumentInfoOther?.HasTime == YesNo.Yes.GetString () &&
    !string.IsNullOrEmpty ( foundRegisterServiceReq.DocumentInfoOther.AdvocacyEndDate ) &&
    string.Compare ( foundRegisterServiceReq.DocumentInfoOther.AdvocacyEndDate, dateTimeService.CurrentPersianDate ) < 0 )
                {
                    pMessageText += "\t - مهلت اعتبار وکالتنامه مورد استناد در بخش اشخاص، به پایان رسیده است." + System.Environment.NewLine;
                    agentDocValidationResult.DocExists = true;
                    if ( agentDocValidatorConfiguration != null )
                        agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V7;
                    if ( agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance )
                    {
                        agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq.Id.ToString ();
                    }

                    if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                    {
                        agentDocValidationResult.MessageText += "#VALIDATOR-CODE:07\n";
                    }
                    else
                    {
                        agentDocValidationResult.MessageText = pMessageText;
                    }

                    if ( agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance )
                        return agentDocValidationResult;
                }

                if ( !string.IsNullOrWhiteSpace ( inputDocumentTypeID ) )
                {
                    DocumentType? documentType = _theParentRegisterServiceReqDocumentType;

                    if ( documentType == null )
                        documentType = await documentRepository.GetDocumentTypeById ( inputDocumentTypeID, cancellationToken );

                    if ( documentType.WealthType == WealthType.Immovable.GetString () && requestType?.WealthType == WealthType.Linkages.GetString () )
                    {
                        pMessageText += "\t - وکالتنامه مورد استناد در بخش اشخاص، مربوط به اموال منقول است." + System.Environment.NewLine;
                        agentDocValidationResult.DocExists = true;
                        if ( agentDocValidatorConfiguration != null )

                            agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V8;
                        if ( agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance )
                        {
                            agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq?.Id.ToString ();
                        }
                        if (agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode)
                        {
                            agentDocValidationResult.MessageText += "#VALIDATOR-CODE:08\n";
                        }
                        else { 
                        agentDocValidationResult.MessageText = pMessageText;
                        }

                        if ( agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance )
                            return agentDocValidationResult;
                    }

                    if ( documentType.WealthType == WealthType.Linkages.GetString () && requestType?.WealthType == WealthType.Immovable.GetString () )
                    {
                        pMessageText += "\t - وکالتنامه مورد استناد در بخش اشخاص، مربوط به اموال غیرمنقول است." + System.Environment.NewLine;
                        agentDocValidationResult.DocExists = true;
                        if ( agentDocValidatorConfiguration != null )
                            agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V9;
                        if ( agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance )
                        {
                            agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq?.Id.ToString ();
                        }
                        if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                        {
                            agentDocValidationResult.MessageText += "#VALIDATOR-CODE:09\n";

                        }
                        else
                        {
                        agentDocValidationResult.MessageText = pMessageText;
                        }

                        if ( agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance )
                            return agentDocValidationResult;
                    }

                    if ( documentType.DocumentTypeGroup2Id == "31"    // اسناد وكالت فروش اموال
    &&
    ( foundRegisterServiceReq?.DocumentTypeId == "322" ||     // سند وكالت کاري اموال غيرمنقول
    foundRegisterServiceReq?.DocumentTypeId == "323" ) )       // سند وكالت کاري وسايل نقليه
                    {
                        pMessageText += "\t - وکالتنامه مورد استناد در بخش اشخاص، وکالت کاری بوده و مربوط به فروش اموال نمی باشد." + System.Environment.NewLine;
                        agentDocValidationResult.DocExists = true;
                        if ( agentDocValidatorConfiguration != null )

                            agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V10;
                        if ( agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance )
                        {
                            agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq.Id.ToString ();
                        }
                        if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                        {
                            agentDocValidationResult.MessageText += "#VALIDATOR-CODE:10\n";
                        }
                        else
                        {
                            agentDocValidationResult.MessageText = pMessageText;
                        }

                        if ( agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance )
                            return agentDocValidationResult;
                    }

                    if ( documentType.WealthType == WealthType.Immovable.GetString () && requestType?.WealthType == WealthType.Immovable.GetString () )
                    {
                        if ( inputRegCaseList != null && inputRegCaseList.Count > 0 )
                            foreach ( RegCasePacket regCase in inputRegCaseList )
                            {
                                bool regCaseFound = false;
                                string basicPlaque = regCase.BasicPlaqueNo;
                                string secondaryPlaqueNo = regCase.SecondaryPlaqueNo;
                                if ( requestType.HasEstates () )
                                {
                                    if ( foundRegisterServiceReq != null )
                                    {

                                        foreach ( DocumentEstate regCaseAgent in foundRegisterServiceReq.DocumentEstates )
                                        {
                                            if ( regCase.BasicPlaqueNo == regCaseAgent.BasicPlaque && regCase.SecondaryPlaqueNo == regCaseAgent.SecondaryPlaque )
                                                regCaseFound = true;
                                        }
                                    }

                                    if ( !regCaseFound )
                                    {
                                        string plaqueText = "";
                                        if ( string.IsNullOrEmpty ( secondaryPlaqueNo ) )
                                            plaqueText = "پلاک اصلی " + basicPlaque;
                                        else
                                            plaqueText = "پلاک فرعی " + secondaryPlaqueNo + " از اصلی " + basicPlaque;

                                        pMessageText += "\t - در وکالتنامه مورد استناد در بخش اشخاص، مشخصات ملک با " + plaqueText + " ثبت نشده است." + System.Environment.NewLine;

                                        agentDocValidationResult.DocExists = true;
                                        if ( agentDocValidatorConfiguration != null )
                                            agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V11;
                                        if ( agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance )
                                        {
                                            agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq?.Id.ToString ();
                                        }

                                        if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                                        {
                                            agentDocValidationResult.MessageText += "#VALIDATOR-CODE:11\n";
                                        }
                                        else
                                        {
                                            agentDocValidationResult.MessageText = pMessageText;
                                        }

                                        if ( agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance )
                                            return agentDocValidationResult;
                                    }

                                }

                            }
                    }

                    if ( documentType.WealthType == WealthType.Linkages.GetString () && requestType?.WealthType == WealthType.Linkages.GetString () )
                    {
                        if ( inputRegCaseList != null && inputRegCaseList.Count > 0 )
                            if ( requestType.HasVehicles () )
                            {
                                foreach ( RegCasePacket regCase in inputRegCaseList )
                                {
                                    bool regCaseFound = false;
                                    string vehicleChassisNo = regCase.vehicleChassisNo;
                                    string vehicleEngineNo = regCase.VehicleEngineNo;
                                    if ( foundRegisterServiceReq != null )
                                    {
                                        foreach ( var regCaseAgent in foundRegisterServiceReq.DocumentVehicles )
                                        {
                                            if ( regCase.vehicleChassisNo == regCaseAgent.ChassisNo && regCase.VehicleEngineNo == regCaseAgent.EngineNo )
                                                regCaseFound = true;
                                        }
                                    }

                                    if ( !regCaseFound )
                                    {
                                        pMessageText += "\t - شماره شاسی یا شماره موتور وسیله نقلیه وارد شده در این سند با مشخصات مندرج در وکالتنامه مورد استناد تطابق ندارد." + System.Environment.NewLine;

                                        agentDocValidationResult.DocExists = true;
                                        if ( agentDocValidatorConfiguration != null )

                                            agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V12;
                                        if ( agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance )
                                        {
                                            agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq?.Id.ToString ();
                                        }

                                        if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                                        {
                                            agentDocValidationResult.MessageText += "#VALIDATOR-CODE:12\n";
                                        }
                                        else
                                        {
                                            agentDocValidationResult.MessageText = pMessageText;
                                        }


                                        if ( agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance )
                                            return agentDocValidationResult;
                                    }

                                }

                            }

                    }
                }
                ///
                IList<Document>? foundAzlOrEstefaList = null;

                if (foundRegisterServiceReq != null)
                {
                    foundAzlOrEstefaList = await documentRepository.GetDocuments(
                        new List<string> { "DocumentType", "DocumentPeople" },
                        cancellationToken,
                        null,
                        NotaryRegServiceReqState.Finalized.GetString(),
                        null,
                        null,
                        new[] { "006", "0022" },
                        foundRegisterServiceReq.Id.ToString()
                    );
                }

                if (foundAzlOrEstefaList?.Count > 0)
                {
                    foreach (var theFoundAzlOrEstefa in foundAzlOrEstefaList)
                    {
                        vakilIsFound = false;
                        movakelIsFound = false;

                        foreach (var docPerson in theFoundAzlOrEstefa.DocumentPeople)
                        {
                            if (docPerson.DocumentPersonTypeId == null)
                                continue;

                            if (docPerson.NationalNo == inputVakilNationalNo && docPerson.DocumentPersonTypeId == "16")
                                vakilIsFound = true;

                            if (docPerson.NationalNo == inputMovakelNationalNo && docPerson.DocumentPersonTypeId == "17")
                                movakelIsFound = true;
                        }

                        // 🔸 در هر شاخه سعی می‌کنیم کد تکراری کمتر باشد
                        var scriptorium = await getScriptorium(theFoundAzlOrEstefa.ScriptoriumId);
                        var scriptoriumName = scriptorium?.Name ?? string.Empty;
                        var docTypeId = theFoundAzlOrEstefa.DocumentTypeId;
                        var docNationalNo = theFoundAzlOrEstefa.NationalNo;
                        var docDate = theFoundAzlOrEstefa.DocumentDate;

                        // وکیل هست، موکل نیست → عزل یا استعفا
                        if (vakilIsFound && !movakelIsFound)
                        {
                            if (docTypeId == "006")
                            {
                                pMessageText +=
                                    $"\t - مطابق با سند شناسه {docNationalNo} در تاریخ {docDate} توسط {scriptoriumName} {inputVakilFullName} عزل شده است.{Environment.NewLine}";

                                if (agentDocValidatorConfiguration?.AppendErrorCode == true)
                                    agentDocValidationResult.MessageText += "#VALIDATOR-CODE:13\n";

                                agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration?.V13 ?? RestrictionLevel.Pass;
                            }
                            else
                            {
                                pMessageText +=
                                    $"\t - مطابق با سند شناسه {docNationalNo} در تاریخ {docDate} توسط {scriptoriumName} {inputVakilFullName} استعفا نموده است.{Environment.NewLine}";

                                if (agentDocValidatorConfiguration?.AppendErrorCode == true)
                                    agentDocValidationResult.MessageText += "#VALIDATOR-CODE:14\n";

                                agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration?.V14 ?? RestrictionLevel.Pass;
                            }
                        }

                        // فقط موکل هست → عزل یا استعفا ثبت شده
                        else if (!vakilIsFound && movakelIsFound)
                        {
                            if (docTypeId == "006")
                            {
                                pMessageText +=
                                    $"\t - وکالتنامه مورد استناد، دارای عزل وکیل ثبت شده با شناسه {docNationalNo} در تاریخ {docDate} توسط {scriptoriumName} می‌باشد.{Environment.NewLine}";

                                if (agentDocValidatorConfiguration?.AppendErrorCode == true)
                                    agentDocValidationResult.MessageText += "#VALIDATOR-CODE:15\n";

                                agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration?.V15 ?? RestrictionLevel.Pass;
                            }
                            else
                            {
                                pMessageText +=
                                    $"\t - وکالتنامه مورد استناد، دارای استعفای وکیل ثبت شده با شناسه {docNationalNo} در تاریخ {docDate} توسط {scriptoriumName} می‌باشد.{Environment.NewLine}";

                                if (agentDocValidatorConfiguration?.AppendErrorCode == true)
                                    agentDocValidationResult.MessageText += "#VALIDATOR-CODE:16\n";

                                agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration?.V16 ?? RestrictionLevel.Pass;
                            }
                        }

                        // هر دو وجود دارند
                        else if (vakilIsFound && movakelIsFound)
                        {
                            if (docTypeId == "006")
                            {
                                pMessageText +=
                                    $"\t - مطابق با سند شناسه {docNationalNo} در تاریخ {docDate} توسط {scriptoriumName} {inputVakilFullName} عزل شده است.{Environment.NewLine}";

                                if (agentDocValidatorConfiguration?.AppendErrorCode == true)
                                    agentDocValidationResult.MessageText += "#VALIDATOR-CODE:17\n";

                                agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration?.V17 ?? RestrictionLevel.Pass;
                            }
                            else
                            {
                                pMessageText +=
                                    $"\t - مطابق با سند شناسه {docNationalNo} در تاریخ {docDate} توسط {scriptoriumName} {inputVakilFullName} استعفا نموده است.{Environment.NewLine}";

                                if (agentDocValidatorConfiguration?.AppendErrorCode == true)
                                    agentDocValidationResult.MessageText += "#VALIDATOR-CODE:18\n";

                                agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration?.V18 ?? RestrictionLevel.Pass;
                            }
                        }

                        agentDocValidationResult.DocExists = true;

                        if (agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance)
                            agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq?.Id.ToString();

                        agentDocValidationResult.MessageText = pMessageText;

                        if (agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance)
                            return agentDocValidationResult;
                    }
                }

                // 🔸 در صورت نبود هیچ‌کدام
                agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration?.V19 ?? RestrictionLevel.Pass;

                // --------------------------------------
                //  ویرایش سندها (Editing Documents)
                // --------------------------------------

                IList<Document>? theFoundEditingDocumentList = null;

                if (foundRegisterServiceReq != null)
                {
                    theFoundEditingDocumentList = await documentRepository.GetDocuments(
                        new List<string> { "DocumentType" },
                        cancellationToken,
                        null,
                        NotaryRegServiceReqState.Finalized.GetString(),
                        null,
                        new[] { foundRegisterServiceReq.NationalNo },
                        new[] { "913", "914", "915" }
                    );
                }

                if (theFoundEditingDocumentList?.Count > 0)
                {
                    foreach (var theFoundEditingDocument in theFoundEditingDocumentList)
                    {
                        var scriptorium = await getScriptorium(theFoundEditingDocument.ScriptoriumId);
                        var scriptoriumName = scriptorium?.Name ?? string.Empty;

                        pMessageText +=
                            $"توجه:{Environment.NewLine}" +
                            $" سند وکالتنامه مورد استناد دارای یک {theFoundEditingDocument.DocumentType.Title}" +
                            $" به شماره {theFoundEditingDocument.NationalNo}" +
                            $" ،تنظیم شده در {scriptoriumName}" +
                            $" در تاریخ {theFoundEditingDocument.DocumentDate} می‌باشد.{Environment.NewLine}";
                    }

                    agentDocValidationResult.DocExists = true;

                    if (agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance)
                        agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq?.Id.ToString();

                    agentDocValidationResult.MessageText = pMessageText;

                    if (agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance)
                        return agentDocValidationResult;
                }
                if ( agentDocValidatorConfiguration != null )
                    agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V19;

                if ( foundRegisterServiceReq != null )
                {
                    theFoundEditingDocumentList = await documentRepository.GetDocuments ( new List<string> { "DocumentType", "DocumentInfoText" }, cancellationToken, null,
                       NotaryRegServiceReqState.Finalized.GetString (), null, new string [ ] { foundRegisterServiceReq.NationalNo }, new string [ ] { "0050", "0023", "0024", "0035" } );

                }

                if ( theFoundEditingDocumentList != null && theFoundEditingDocumentList.Count > 0 )
                {
                    foreach ( Document theFoundEditingDocument in theFoundEditingDocumentList )
                    {
                        if ( theFoundEditingDocument != null )
                        {
                            var scriptorium=await getScriptorium(theFoundEditingDocument.ScriptoriumId);

                            pMessageText +=
                                "توجه:" +
                                System.Environment.NewLine +
                                " سند وکالتنامه مورد استناد دارای یک " +
                                theFoundEditingDocument.DocumentType.Title +
                                " به شماره " + theFoundEditingDocument.NationalNo +
                                  " ،تنظیم شده در " + ( scriptorium?.Name ?? " " ) +/// NewVersion ToDoList

                                //  " ،تنظیم شده در " + theFoundEditingDocument.TheScriptorium.Name +/// NewVersion ToDoList
                                " در تاریخ " + theFoundEditingDocument.DocumentDate +
                                " می باشد.";

                            pMessageText +=
                                System.Environment.NewLine + " متن ملاحظه : " +
                                theFoundEditingDocument?.DocumentInfoText?.LegalText;
                        }
                    }
                    agentDocValidationResult.DocExists = true;
                    if ( agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance )
                        agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq?.Id.ToString ();

                    agentDocValidationResult.MessageText = pMessageText;

                    if ( agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance )
                        return agentDocValidationResult;
                }

                agentDocValidationResult.DocExists = true;
                agentDocValidationResult.ResultRestrictionLevel = RestrictionLevel.Pass;
           
                    agentDocValidationResult.RegisterServiceReqID = foundRegisterServiceReq?.Id.ToString ();
                    agentDocValidationResult.FoundObjectTypeCode = requestType?.Id;
                    agentDocValidationResult.SMSRecipientPacketCollection = this.CollectSMSRecipients ( foundRegisterServiceReq );

                if ( foundRegisterServiceReq != null )
                {
                    if ( !string.IsNullOrWhiteSpace ( foundRegisterServiceReq?.DocumentInfoText?.DocumentDescription ) )
                        pMessageText += System.Environment.NewLine + "توضیحات امضای سند:" + System.Environment.NewLine + foundRegisterServiceReq?.DocumentInfoText?.DocumentDescription;
                }

                agentDocValidationResult.MessageText = pMessageText;

                bool isAuthorize;
                AgentDocValidationResult result;
                (isAuthorize, result) = await firewall.AuthorizeRelatedDocumentValidationResponse ( inputAgentDocumentNationalNo );
                if ( !isAuthorize )
                {
                    agentDocValidationResult.RegisterServiceReqID = null;
                    agentDocValidationResult.ResultRestrictionLevel = RestrictionLevel.Avoidance;
                    agentDocValidationResult.RegisterServiceReqID = null;

                }
                agentDocValidationResult.MessageText += result.MessageText;
                return agentDocValidationResult;
            }
            catch ( System.Exception ex )
            {
                //20
                if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                    agentDocValidationResult.MessageText = "#VALIDATOR-CODE:ExceptionHandler-20\n";
                pMessageText += ex.ToCompleteString ();
                agentDocValidationResult.DocExists = false;
                if ( agentDocValidatorConfiguration != null )
                    agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V20;
                agentDocValidationResult.MessageText = pMessageText;

                return agentDocValidationResult;
            }
        }

        /// <summary>
        /// The CollectSMSRecipients
        /// </summary>
        /// <param name="theFoundRegisterServiceReq">The theFoundRegisterServiceReq<see cref="Document?"/></param>
        /// <returns>The <see cref="List{SMSRecipientPacket}?"/></returns>
        private List<SMSRecipientPacket>? CollectSMSRecipients ( Document? theFoundRegisterServiceReq )
        {
            if ( theFoundRegisterServiceReq != null && theFoundRegisterServiceReq.DocumentPeople != null && theFoundRegisterServiceReq.DocumentPeople.Count > 0 )
            {
                List<SMSRecipientPacket> smsRecipientPacketCollection = new List<SMSRecipientPacket>();
                foreach ( DocumentPerson theOnePerson in theFoundRegisterServiceReq.DocumentPeople )
                {
                    if ( theOnePerson.IsOriginal == YesNo.Yes.GetString () )
                    {
                        if ( theOnePerson.DocumentPersonTypeId != null )
                        {
                            if (
                                   theOnePerson.DocumentPersonTypeId == "17"    //موکل
                                || theOnePerson.DocumentPersonTypeId == "16"  //وکیل
                                || theOnePerson.DocumentPersonTypeId == "59" ) //اولین موکل
                            {
                                if ( string.IsNullOrWhiteSpace ( theOnePerson.MobileNo ) || theOnePerson.MobileNo.Length < 10 )
                                    continue;
                                else
                                {
                                    SMSRecipientPacket theOneSMSRecipient = new SMSRecipientPacket();

                                    theOneSMSRecipient.RecipientFullName = theOnePerson.FullName ();
                                    theOneSMSRecipient.RecipientMobileNo = theOnePerson.MobileNo;
                                    theOneSMSRecipient.RecipientPersonTypeCode = theOnePerson.DocumentPersonTypeId;

                                    smsRecipientPacketCollection.Add ( theOneSMSRecipient );
                                }
                            }
                        }
                    }
                }

                return smsRecipientPacketCollection;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// The ValidateAzlOrEstefaDocument
        /// </summary>
        /// <param name="foundAzlOrEstefa">The foundAzlOrEstefa<see cref="Document"/></param>
        /// <param name="inputAgentDocumentNationalNo">The inputAgentDocumentNationalNo<see cref="string"/></param>
        /// <param name="inputAgentDocumentScriptoriumId">The inputAgentDocumentScriptoriumId<see cref="string"/></param>
        /// <param name="inputAgentDocumentDate">The inputAgentDocumentDate<see cref="string"/></param>
        /// <param name="inputDocumentTypeID">The inputDocumentTypeID<see cref="string"/></param>
        /// <param name="inputMovakelNationalNo">The inputMovakelNationalNo<see cref="string"/></param>
        /// <param name="inputVakilNationalNo">The inputVakilNationalNo<see cref="string"/></param>
        /// <param name="inputMovakelFullName">The inputMovakelFullName<see cref="string"/></param>
        /// <param name="inputVakilFullName">The inputVakilFullName<see cref="string"/></param>
        /// <param name="pMessageText">The pMessageText<see cref="string"/></param>
        /// <returns>The <see cref="Task{(AgentDocValidationResult,string)}"/></returns>
        private async Task<(AgentDocValidationResult, string)> ValidateAzlOrEstefaDocument (
                                                                Document foundAzlOrEstefa,
                                                                string inputAgentDocumentNationalNo,
                                                                string inputAgentDocumentScriptoriumId,
                                                                string inputAgentDocumentDate,
                                                                string inputDocumentTypeID,
                                                                string inputMovakelNationalNo,
                                                                string inputVakilNationalNo,
                                                                string inputMovakelFullName,
                                                                string inputVakilFullName,
                                                                string pMessageText )
        {
            AgentDocValidationResult agentDocValidationResult = new AgentDocValidationResult();

            bool vakilIsFound = false;
            bool movakelIsFound = false;
            foreach ( DocumentPerson docPerson in foundAzlOrEstefa.DocumentPeople )
            {
                if ( docPerson.DocumentPersonTypeId != null )
                {
                    if ( docPerson.NationalNo == inputVakilNationalNo &&
                        docPerson.DocumentPersonTypeId == "16" )    // وکیل
                        vakilIsFound = true;

                    if ( docPerson.NationalNo == inputMovakelNationalNo &&
                        docPerson.DocumentPersonTypeId == "17" )    // موکل
                        movakelIsFound = true;
                }
            }

            if ( vakilIsFound & !movakelIsFound )
            {
                if ( foundAzlOrEstefa.DocumentTypeId == "006" )
                {
                    var scriptorium=await getScriptorium(foundAzlOrEstefa.ScriptoriumId);
                    pMessageText += "\t - مطابق با سند شناسه " + foundAzlOrEstefa.NationalNo + " در تاریخ " + foundAzlOrEstefa.DocumentDate + " توسط " + ( scriptorium?.Name ?? " " ) +/* NewVersion ToDoList*//*foundAzlOrEstefa.TheScriptorium.Name + " " +*/ inputVakilFullName + " عزل شده است." + System.Environment.NewLine;

                    if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                        agentDocValidationResult.MessageText += "#VALIDATOR-CODE:13\n";
                    if ( agentDocValidatorConfiguration != null )

                        agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V13;
                }

                else
                {
                    var scriptorium=await getScriptorium(foundAzlOrEstefa.ScriptoriumId);

                    pMessageText += "\t - مطابق با سند شناسه " + foundAzlOrEstefa.NationalNo + " در تاریخ " + foundAzlOrEstefa.DocumentDate + " توسط " + ( scriptorium?.Name ?? " " ) +/* NewVersion ToDoList*/ /*foundAzlOrEstefa.TheScriptorium.Name + " "*/  inputVakilFullName + " استعفا نموده است." + System.Environment.NewLine;

                    if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                        agentDocValidationResult.MessageText += "#VALIDATOR-CODE:14\n";
                    if ( agentDocValidatorConfiguration != null )

                        agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V14;
                }
            }

            else if ( !vakilIsFound & movakelIsFound )
            {

                if ( foundAzlOrEstefa.DocumentTypeId == "006" )
                {
                    var scriptorium=await getScriptorium(foundAzlOrEstefa.ScriptoriumId);

                    pMessageText += "\t - وکالتنامه مورد استناد، دارای عزل وکیل ثبت شده با شناسه " + foundAzlOrEstefa.NationalNo + " در تاریخ " + foundAzlOrEstefa.DocumentDate + " توسط " + ( scriptorium?.Name ?? " " ) /* NewVersion ToDoList*/ /*foundAzlOrEstefa.TheScriptorium.Name + " "*/  + " می باشد." + System.Environment.NewLine;

                    if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                        agentDocValidationResult.MessageText += "#VALIDATOR-CODE:15\n";
                    if ( agentDocValidatorConfiguration != null )

                        agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V15;
                }

                else
                {
                    var scriptorium=await getScriptorium(foundAzlOrEstefa.ScriptoriumId);

                    pMessageText += "\t - وکالتنامه مورد استناد، دارای استعفای وکیل ثبت شده با شناسه " + foundAzlOrEstefa.NationalNo + " در تاریخ " + foundAzlOrEstefa.DocumentDate + " توسط " + ( scriptorium?.Name ?? " " )  /* NewVersion ToDoList*/ /*foundAzlOrEstefa.TheScriptorium.Name + " "*/ + " می باشد." + System.Environment.NewLine;

                    if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                        agentDocValidationResult.MessageText += "#VALIDATOR-CODE:16\n";
                    if ( agentDocValidatorConfiguration != null )

                        agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V16;
                }
            }

            else if ( vakilIsFound & movakelIsFound )
            {
                if ( foundAzlOrEstefa.DocumentTypeId == "006" )
                {
                    var scriptorium=await getScriptorium(foundAzlOrEstefa.ScriptoriumId);

                    pMessageText += "\t - مطابق با سند شناسه " + foundAzlOrEstefa.NationalNo + " در تاریخ " + foundAzlOrEstefa.DocumentDate + " توسط " + ( scriptorium?.Name ?? " " ) /* NewVersion ToDoList*/ /*foundAzlOrEstefa.TheScriptorium.Name + " "*/  + inputVakilFullName + " عزل شده است." + System.Environment.NewLine;

                    if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                        agentDocValidationResult.MessageText += "#VALIDATOR-CODE:17\n";
                    if ( agentDocValidatorConfiguration != null )
                        agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V17;
                }

                else
                {
                    var scriptorium=await getScriptorium(foundAzlOrEstefa.ScriptoriumId);
                    pMessageText += "\t - مطابق با سند شناسه " + foundAzlOrEstefa.NationalNo + " در تاریخ " + foundAzlOrEstefa.DocumentDate + " توسط " + ( scriptorium?.Name ?? " " )  /* NewVersion ToDoList*/ /*foundAzlOrEstefa.TheScriptorium.Name + " "*/ + inputVakilFullName + " استعفا نموده است." + System.Environment.NewLine;

                    if ( agentDocValidatorConfiguration != null && agentDocValidatorConfiguration.AppendErrorCode )
                        agentDocValidationResult.MessageText += "#VALIDATOR-CODE:18\n";
                    if ( agentDocValidatorConfiguration != null )

                        agentDocValidationResult.ResultRestrictionLevel = agentDocValidatorConfiguration.V18;
                }
            }

            agentDocValidationResult.DocExists = true;

            if ( agentDocValidationResult.ResultRestrictionLevel != RestrictionLevel.Avoidance )
            {
                agentDocValidationResult.RegisterServiceReqID = foundAzlOrEstefa.Id.ToString ();
                ;
            }

            agentDocValidationResult.MessageText = pMessageText;

            return (agentDocValidationResult, pMessageText);
        }

        /// <summary>
        /// The getScriptorium
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{ScriptoriumItem?}"/></returns>
        internal async Task<ScriptoriumItem?> getScriptorium ( string id )
        {

            ScriptoriumInput scriptoriumInput = new ScriptoriumInput(new string[]{id } );
            ApiResult<ScriptoriumViewModel> scriptoriumResponse =await mediator.Send(scriptoriumInput);

            if ( scriptoriumResponse.IsSuccess )
            {
                var scriptorium= scriptoriumResponse.Data.ScriptoriumList[0];
                return scriptorium;

            }
            else
            {
                return null;
            }
        }
    }

}
