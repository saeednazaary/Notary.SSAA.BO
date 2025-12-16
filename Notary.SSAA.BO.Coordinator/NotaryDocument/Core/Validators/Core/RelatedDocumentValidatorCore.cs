namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Validators.Core
{
    using Notary.SSAA.BO.Configuration;
    using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="RelatedDocumentValidatorCore" />
    /// </summary>
    public class RelatedDocumentValidatorCore
    {
        /// <summary>
        /// Defines the relatedDocValidatorConfiguration
        /// </summary>
        internal RelatedDocValidatorConfiguration relatedDocValidatorConfiguration;

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
        /// Defines the firewall
        /// </summary>
        public Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Firewall.Firewall firewall;

        /// <summary>
        /// Defines the userService
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Defines the dateTimeService
        /// </summary>
        private readonly IDateTimeService dateTimeService;

        /// <summary>
        /// Defines the documentTypeRepository
        /// </summary>
        private IDocumentTypeRepository documentTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedDocumentValidatorCore"/> class.
        /// </summary>
        /// <param name="_documentRepository">The _documentRepository<see cref="IDocumentRepository"/></param>
        /// <param name="_documentTypeRepository">The _documentTypeRepository<see cref="IDocumentTypeRepository"/></param>
        /// <param name="_userService">The _userService<see cref="IUserService"/></param>
        /// <param name="_dateTimeService">The _dateTimeService<see cref="IDateTimeService"/></param>
        /// <param name="_relatedDocValidatorConfiguration">The _relatedDocValidatorConfiguration<see cref="RelatedDocValidatorConfiguration"/></param>
        /// <param name="_documentPersonRepository">The _documentPersonRepository<see cref="IDocumentPersonRepository"/></param>
        /// <param name="_documentPersonRelatedRepository">The _documentPersonRelatedRepository<see cref="IDocumentPersonRelatedRepository"/></param>
        /// <param name="_firewall">The _firewall<see cref="Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Firewall.Firewall"/></param>
        public RelatedDocumentValidatorCore ( IDocumentRepository _documentRepository, IDocumentTypeRepository _documentTypeRepository,
            IUserService _userService, IDateTimeService _dateTimeService, RelatedDocValidatorConfiguration _relatedDocValidatorConfiguration,
            IDocumentPersonRepository _documentPersonRepository, IDocumentPersonRelatedRepository _documentPersonRelatedRepository, Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Firewall.Firewall _firewall )
        {
            documentRepository = _documentRepository;
            userService = _userService;
            documentPersonRepository = _documentPersonRepository;
            documentPersonRelatedRepository = _documentPersonRelatedRepository;
            dateTimeService = _dateTimeService;
            documentTypeRepository = _documentTypeRepository;
            relatedDocValidatorConfiguration = _relatedDocValidatorConfiguration;
            firewall = _firewall;
        }

        /// <summary>
        /// The ValidateRelatedDocument
        /// </summary>
        /// <param name="theRelatedDocValidationRequest">The theRelatedDocValidationRequest<see cref="Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document.RelatedDocValidationRequest"/></param>
        /// <returns>The <see cref="Task{RelatedDocValidationResult}"/></returns>
        internal async Task<RelatedDocValidationResult> ValidateRelatedDocument ( Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document.RelatedDocValidationRequest theRelatedDocValidationRequest )
        {
            RelatedDocValidationResult relatedDocValidationResult = new RelatedDocValidationResult();
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = source.Token;
            string pMessageText = string.Empty;
            var foundRegisterServiceReq=await documentRepository.GetDocumentByNationalNo(new List<string>{ "DocumentType","DocumentTypeGroup2","DocumentInfoOther"},theRelatedDocValidationRequest.RelatedDocumentNationalNo,cancellationToken);
            var documentType=await documentTypeRepository.GetDocumentType(theRelatedDocValidationRequest.DocumentTypeID,cancellationToken);

            if ( foundRegisterServiceReq == null )
            {
                pMessageText += "\t - اطلاعات سند وابسته مندرج در بخش اسناد وابسته، در سامانه ثبت الکترونیک اسناد ثبت نشده است." + System.Environment.NewLine;

                if ( relatedDocValidatorConfiguration.V1 == RestrictionLevel.Avoidance )
                {
                    relatedDocValidationResult.IsValid = false;
                    relatedDocValidationResult.ValidationMessage = pMessageText;
                    return relatedDocValidationResult;
                }
            }
            else
            {
                if ( foundRegisterServiceReq.DocumentType.IsSupportive == YesNo.Yes.GetString () )
                {
                    pMessageText += "\t - لطفاً در بخش سند وابسته، شناسه یکتای مربوط به اسناد اصلی را استفاده نمایید. درج شناسه یکتای خدمات تبعی به عنوان سند وابسته مجاز نمی باشد. " + System.Environment.NewLine;

                    relatedDocValidationResult.IsValid = false;
                    relatedDocValidationResult.ValidationMessage = pMessageText;
                    return relatedDocValidationResult;
                }

                if ( foundRegisterServiceReq.State != NotaryRegServiceReqState.Finalized.GetString () )    // آیا سند تایید نهایی شده است؟
                {
                    pMessageText += "\t - سند وابسته مندرج در بخش اسناد وابسته، تایید نهایی نشده است." + System.Environment.NewLine;

                    if ( relatedDocValidatorConfiguration.V2 == RestrictionLevel.Avoidance )
                    {
                        relatedDocValidationResult.IsValid = false;
                        relatedDocValidationResult.ValidationMessage = pMessageText;
                        return relatedDocValidationResult;
                    }
                }

                if ( foundRegisterServiceReq.ScriptoriumId != theRelatedDocValidationRequest.RelatedDocumentScriptoriumId )    // آیا سند مربوط به دفترخانه تعیین شده است؟
                {
                    pMessageText += "\t - دفترخانه تنظیم کننده سند وابسته مندرج در بخش اسناد وابسته، صحیح نمی باشد." + System.Environment.NewLine;

                    if ( relatedDocValidatorConfiguration.V3 == RestrictionLevel.Avoidance )
                    {
                        relatedDocValidationResult.IsValid = false;
                        relatedDocValidationResult.ValidationMessage = pMessageText;
                        return relatedDocValidationResult;
                    }
                }

                if ( foundRegisterServiceReq.DocumentDate != theRelatedDocValidationRequest.RelatedDocumentDate && foundRegisterServiceReq.WriteInBookDate != theRelatedDocValidationRequest.RelatedDocumentDate )    // آیا تاریخ صدور سند درست است؟
                {
                    pMessageText += "\t - تاریخ سند وابسته مندرج در بخش اسناد وابسته، صحیح نمی باشد." + System.Environment.NewLine;

                    if ( relatedDocValidatorConfiguration.V4 == RestrictionLevel.Avoidance )
                    {
                        relatedDocValidationResult.IsValid = false;
                        relatedDocValidationResult.ValidationMessage = pMessageText;
                        return relatedDocValidationResult;
                    }
                }

                if ( documentType.DocumentTypeGroup2Id == "33" &&      // تفویض وکالت
    foundRegisterServiceReq.DocumentType.DocumentTypeGroup2.DocumentTypeGroup1Id == "3" )    // وکالتنامه
                {
                    if ( foundRegisterServiceReq.DocumentInfoOther.HasTime == YesNo.Yes.GetString () &&
                        !string.IsNullOrEmpty ( foundRegisterServiceReq.DocumentInfoOther.AdvocacyEndDate ) &&
                        string.Compare ( foundRegisterServiceReq.DocumentInfoOther.AdvocacyEndDate, dateTimeService.CurrentPersianDate ) < 0 )
                    {
                        pMessageText += "\t - مهلت اعتبار وکالتنامه مندرج در بخش سایر اطلاعات، به پایان رسیده است." + System.Environment.NewLine;

                        if ( relatedDocValidatorConfiguration.V5 == RestrictionLevel.Avoidance )
                        {
                            relatedDocValidationResult.IsValid = false;
                            relatedDocValidationResult.ValidationMessage = pMessageText;
                            return relatedDocValidationResult;
                        }
                    }
                }

                if ( documentType.DocumentTypeGroup2Id == "33" &&      // تفویض وکالت
    foundRegisterServiceReq.DocumentInfoOther.HasAdvocacyToOthersPermit == YesNo.No.GetString () )
                {
                    pMessageText += "\t - در وکالتنامه مندرج در بخش اسناد وابسته، حق توکیل به غیر وجود ندارد." + System.Environment.NewLine;

                    if ( relatedDocValidatorConfiguration.V6 == RestrictionLevel.Avoidance )
                    {
                        relatedDocValidationResult.IsValid = false;
                        relatedDocValidationResult.ValidationMessage = pMessageText;
                        return relatedDocValidationResult;
                    }
                }

            }
            relatedDocValidationResult.RelatedRegisterServiceReqObjectID = foundRegisterServiceReq?.Id.ToString ();

            relatedDocValidationResult.IsValid = true;
            relatedDocValidationResult.ValidationMessage = pMessageText;

            var result= await firewall.AuthorizeRelatedDocumentValidationResponse ( theRelatedDocValidationRequest.RelatedDocumentNationalNo, theRelatedDocValidationRequest.DocumentTypeID );
            if ( result != null )
            {
                relatedDocValidationResult.ValidationMessage += result.ValidationMessage;
            }

            return relatedDocValidationResult;
        }
    }

}
