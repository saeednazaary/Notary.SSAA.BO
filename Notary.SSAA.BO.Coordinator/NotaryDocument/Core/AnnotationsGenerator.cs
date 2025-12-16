namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core
{
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Document = Notary.SSAA.BO.Domain.Entities.Document;
    using Enumerations = Notary.SSAA.BO.SharedKernel.Enumerations;

    /// <summary>
    /// Defines the <see cref="AnnotationsGenerator" />
    /// </summary>
    public class AnnotationsGenerator
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
        /// Initializes a new instance of the <see cref="AnnotationsGenerator"/> class.
        /// </summary>
        /// <param name="_documentRepository">The _documentRepository<see cref="IDocumentRepository"/></param>
        /// <param name="_userService">The _userService<see cref="IUserService"/></param>
        /// <param name="_documentPersonRepository">The _documentPersonRepository<see cref="IDocumentPersonRepository"/></param>
        /// <param name="_documentPersonRelatedRepository">The _documentPersonRelatedRepository<see cref="IDocumentPersonRelatedRepository"/></param>
        public AnnotationsGenerator ( IDocumentRepository _documentRepository, IUserService _userService, IDocumentPersonRepository _documentPersonRepository, IDocumentPersonRelatedRepository _documentPersonRelatedRepository )
        {
            documentRepository = _documentRepository;
            userService = _userService;
            documentPersonRepository = _documentPersonRepository;
            documentPersonRelatedRepository = _documentPersonRelatedRepository;
        }

        /// <summary>
        /// The GenerateSingleAnnotation
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string?"/></param>
        /// <param name="nationalNo">The nationalNo<see cref="string?"/></param>
        /// <param name="currentCMSOrganizationID">The currentCMSOrganizationID<see cref="string?"/></param>
        /// <returns>The <see cref="Task{AnnotationPack?}"/></returns>
        public async Task<AnnotationPack?> GenerateSingleAnnotation ( string? documentId = null, string? nationalNo = null, string? currentCMSOrganizationID = null )
        {
            Notary.SSAA.BO.Domain.Entities.Document? document=null;
            List<string> detailList=new List<string>();
            RequsetType? requestType =null;
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            if ( documentId != null )
            {
                requestType = await documentRepository.GetRequestType ( token, documentId );

            }
            else
                 if ( nationalNo != null )
            {
                requestType = await documentRepository.GetRequestType ( token, null, nationalNo );

            }
            else
            {
                return null;
            }
            AnnotationPack annotationPack=new AnnotationPack();
            StringBuilder summary = new StringBuilder();
            string scriptoriumTitle =userService.UserApplicationContext.BranchAccess.BranchName; //document.RelatedDocumentScriptorium.Substring(0, document.RelatedDocumentScriptorium.IndexOf("استان") - 1).Replace("دفترخانه ", "دفتر ").Replace("شماره ", "").Replace("شهر ", "");

            if ( requestType != null )
            {
                document = await GetDocumentBaseDocumentType ( requestType.Id, requestType.DocumentId );
                if ( document != null )
                {
                    switch ( requestType.Id )
                    {
                        case "004": //فک رهن
                            string annotationTextSingular = "مورد ثبتی {0} با شناسه {1} در تاریخ {2} فک رهن شده است.";
                            string annotationTextPlural = "موارد ثبتی {0} با شناسه {1} در تاریخ {2} فک رهن شده است.";
                            if ( document.DocumentCases != null && document.DocumentCases.Count > 1 )
                            {
                                annotationTextPlural = string.Format ( annotationTextPlural, document.Cases (), document.NationalNo, document.WriteInBookDate );
                                summary?.AppendLine ( annotationTextPlural );
                            }
                            else
                            {
                                annotationTextSingular = string.Format ( annotationTextSingular, document.Cases (), document.NationalNo, document.WriteInBookDate );
                                summary?.AppendLine ( annotationTextSingular );
                            }

                            break;
                        case "005": //فسخ سند

                            summary.Append ( "سند در تاریخ " + document.DocumentDate + " با شناسه " + document.NationalNo + " فسخ شده است. " );

                            break;
                        case "006": //عزل وكيل

                            int vakilCount = 0;
                            string? vakilNames = null;
                            foreach ( DocumentPerson documentPerson in document.DocumentPeople )
                            {
                                if ( documentPerson.DocumentPersonTypeId != null &&
                                    documentPerson.DocumentPersonTypeId == "16" )
                                {
                                    vakilCount++;

                                    if ( !string.IsNullOrWhiteSpace ( vakilNames ) )
                                        vakilNames += "، ";

                                    vakilNames += documentPerson.FullName ();
                                }
                            }

                            if ( vakilCount > 1 )
                                summary.Append ( "وکلای سند، " );
                            else
                                summary.Append ( "وکیل سند، " );

                            summary.Append ( vakilNames + "، " );

                            summary.Append ( " در تاریخ " );
                            summary.Append ( document.WriteInBookDate + " " );
                            summary.Append ( "با شناسه " );
                            summary.Append ( document.NationalNo + " " );

                            if ( vakilCount > 1 )
                                summary.Append ( "عزل شدند." );
                            else
                                summary.Append ( "عزل شد." );

                            break;
                        case "0022": //استعفای وکیل

                            vakilCount = 0;
                            vakilNames = null;
                            foreach ( DocumentPerson documentPerson in document.DocumentPeople )
                            {
                                if (
                                    documentPerson.DocumentPersonTypeId == "16" )
                                {
                                    vakilCount++;

                                    if ( !string.IsNullOrWhiteSpace ( vakilNames ) )
                                        vakilNames += "، ";

                                    vakilNames += documentPerson.FullName ();
                                }
                            }

                            if ( vakilCount > 1 )
                                summary.Append ( "وکلای سند، " );
                            else
                                summary.Append ( vakilNames + "، " );

                            summary.Append ( vakilNames + "، " );

                            summary.Append ( " در تاریخ " );
                            summary.Append ( document.WriteInBookDate + " " );
                            summary.Append ( "با شناسه " );
                            summary.Append ( document.NationalNo + " " );

                            if ( vakilCount > 1 )
                                summary?.Append ( "استعفا نمودند." );
                            else
                                summary?.Append ( "استعفا نمود." );

                            break;
                        case "008": //اخطاریه

                            string? persons = null;
                            foreach ( DocumentPerson documentPerson in document.DocumentPeople )
                            {
                                if ( documentPerson.DocumentPersonTypeId == "57" ) //مخاطب
                                {
                                    if ( !string.IsNullOrWhiteSpace ( persons ) )
                                        persons += " و ";

                                    if ( documentPerson.SexType == Enumerations.SexType.Female.GetString () )
                                    {
                                        persons += "خانم " + documentPerson.FullName ();

                                    }
                                    else
                                    if ( documentPerson.SexType == Enumerations.SexType.Male.GetString () )
                                    {
                                        persons += "آقای " + documentPerson.FullName ();

                                    }
                                    else
                                    if ( documentPerson.SexType == Enumerations.SexType.None.GetString () )

                                    {

                                        persons += documentPerson.FullName ();

                                    }

                                }
                            }
                            summary.Append ( "برای " );
                            if ( !string.IsNullOrWhiteSpace ( persons ) )
                            {
                                summary.Append ( persons );
                            }
                            else
                            {
                                summary.Append ( "این سند" );
                            }
                            summary.Append ( " اخطاریه صادر شد." );

                            break;
                        case "007": //اجراییه

                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentDate + " " );
                            summary.Append ( "با شناسه " );
                            summary.Append ( document.NationalNo + " " );
                            summary.Append ( " اجراییه صادر شد." );

                            break;
                        case "0012": //گواهي علت عدم انجام معامله //عدم حضور 

                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentDate + " " );
                            summary.Append ( "با شناسه " );
                            summary.Append ( document.NationalNo + " " );
                            summary.Append ( " گواهی عدم حضور صادر شد." );

                            break;
                        case "009": //رونوشت

                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentDate + " " );
                            summary.Append ( "با شناسه " );
                            summary.Append ( document.NationalNo + " " );
                            summary.Append ( " رونوشت صادر شد." );

                            break;
                        case "002": //قبض سپرده

                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentDate + " " );
                            summary.Append ( "با شناسه " );
                            summary.Append ( document.NationalNo + " " );
                            summary.Append ( " قبض سپرده صادر شد." );

                            break;
                        case "0023": // بی اعتباری

                            summary.Append ( "بر اساس حکم دادگاه به شماره " );
                            summary.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentDate + " " );
                            summary.Append ( "مراتب بی اعتباری سند ثبت شد." );

                            break;

                        case "0035": // بی اعتباری بخشی از سند

                            summary.Append ( "بر اساس حکم دادگاه به شماره " );
                            summary.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentDate + " " );
                            summary.Append ( "مراتب بی اعتباری بخشی از سند ثبت شد." );

                            break;
                        case "0024": //رفع بی اعتباری

                            summary.Append ( "بر اساس حکم دادگاه به شماره " );
                            summary.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentDate + " " );
                            summary.Append ( "مراتب رفع بی اعتباری سند ثبت شد." );

                            break;

                        case "0025": //تخلیه عین مستاجره

                            summary.Append ( "بر اساس دادنامه شماره " );
                            summary.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                            summary.Append ( "مورخ " );
                            summary.Append ( document.DocumentInfoJudgement.IssueDate + " " );
                            summary.Append ( "مرجع " );
                            summary.Append ( document.DocumentInfoJudgement.IssuerName + " " );
                            summary.Append ( "عین مستاجره " );
                            if ( document.DocumentEstates != null && document.DocumentEstates.Count > 0 )
                            {
                                summary.Append ( "در تاریخ " );
                                summary.Append ( document.DocumentEstates.ElementAt ( 0 ).EvacuatedDate + " " );
                            }
                            summary?.Append ( "تخلیه شد." );

                            break;
                        case "0026": // ختم پرونده اجرایی

                            summary.Append ( "بر اساس نامه شماره " );
                            summary.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                            summary.Append ( "تاریخ " );
                            summary.Append ( document.DocumentInfoJudgement.IssueDate + " " );
                            summary.Append ( "اداره اجرایی " );
                            summary.Append ( document.DocumentInfoJudgement.IssuerName + "، " );
                            summary.Append ( "ختم پرونده اجرایی اعلان شد." );

                            break;
                        case "0027": //تغییر نشانی

                            if ( document.DocumentPeople != null && document.DocumentPeople.Count > 0 )
                            {
                                var person= document.DocumentPeople.ElementAt ( 0 );
                                summary.Append ( "با مراجعه " );

                                if ( ( document.DocumentPeople.ElementAt ( 0 ) ).SexType == SexType.Male.GetString () )
                                    summary.Append ( "آقای " );
                                else if ( ( document.DocumentPeople.ElementAt ( 0 ) ).SexType == SexType.Female.GetString () )
                                    summary.Append ( "خانم " );

                                summary.Append ( ( document.DocumentPeople.ElementAt ( 0 ) ).FullName () + " " );

                                summary.Append ( "در تاریخ " );
                                summary.Append ( document.DocumentDate + "، " );
                                summary.Append ( "نشانی ایشان به " );
                                summary.Append ( document.DocumentPeople.ElementAt ( 0 ).Address + " " );

                                if ( !string.IsNullOrWhiteSpace ( document.DocumentPeople.ElementAt ( 0 ).PostalCode ) )
                                {
                                    summary.Append ( "و کد پستی " );
                                    summary.Append ( person.PostalCode + " " );
                                }

                                if ( !string.IsNullOrWhiteSpace ( person.Tel ) )
                                {
                                    summary.Append ( "با شماره تلفن ثابت " );
                                    summary.Append ( person.Tel + " " );
                                }

                                if ( !string.IsNullOrWhiteSpace ( person.MobileNo ) )
                                {
                                    summary.Append ( "با شماره تلفن همراه " );
                                    summary.Append ( person.MobileNo + " " );
                                }

                                summary.Append ( "تغییر یافت." );
                            }
                            else
                            {
                                summary.Append ( "تغییر نشانی انجام شده است." );
                            }

                            break;
                        case "913": //سند اقرارنامه اصلاحي

                            summary.AppendLine ( "در " + scriptoriumTitle );

                            summary.AppendLine ();
                            summary.Append ( "سند اقرارنامه اصلاحی با " );

                            int caseCount = 0;
                            string? cases = null;
                            if ( document.DocumentCases != null )
                            {
                                foreach ( var item in document.DocumentCases )
                                {
                                    caseCount++;

                                    if ( !string.IsNullOrWhiteSpace ( cases ) )
                                        cases += " و ";

                                    cases += item.Title;
                                }

                            }

                            if ( caseCount > 1 )
                                summary.Append ( "موضوعات " );
                            else
                                summary.Append ( "موضوع " );

                            summary.Append ( cases + " " );
                            summary.Append ( "توسط " );
                            summary.Append ( scriptoriumTitle + " " );
                            summary.Append ( "با شناسه " );
                            summary.Append ( document.NationalNo + " " );
                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentDate + " " );
                            summary.Append ( "صادر شد." );

                            break;

                        case "914": //سند اقرارنامه عزل وکيل

                            summary.AppendLine ( "در " + scriptoriumTitle );

                            summary.AppendLine ();
                            summary.Append ( "مطابق سند تنظیم شده در " );
                            summary.Append ( scriptoriumTitle + " " );
                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentDate + " " );
                            summary.Append ( "با شناسه " );
                            summary.Append ( document.NationalNo + " " );

                            vakilCount = 0;
                            vakilNames = null;
                            if ( document.DocumentPeople != null && document.DocumentPeople.Count > 0 )
                            {

                                foreach ( var item in document.DocumentPeople )
                                {
                                    if (
                                      item.DocumentPersonTypeId == "42" ) //مقرله
                                    {
                                        vakilCount++;

                                        if ( !string.IsNullOrWhiteSpace ( vakilNames ) )
                                            vakilNames += "، ";

                                        vakilNames += item?.FullName ();
                                    }
                                }

                            }

                            if ( vakilCount > 1 )
                                summary.Append ( "وکلای سند، " );
                            else
                                summary.Append ( "وکیل سند، " );

                            summary.Append ( vakilNames );

                            if ( vakilCount > 1 )
                                summary.Append ( " عزل شدند." );
                            else
                                summary.Append ( " عزل شد." );

                            break;

                        case "915": // سند اقرارنامه استعفاي وکيل

                            summary.AppendLine ( "در " + scriptoriumTitle );

                            summary.AppendLine ();
                            summary.Append ( "مطابق سند تنظیم شده در " );
                            summary.Append ( scriptoriumTitle + " " );
                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentDate + " " );
                            summary.Append ( "با شناسه " );
                            summary.Append ( document.NationalNo + " " );

                            vakilCount = 0;
                            vakilNames = null;
                            if ( document.DocumentPeople != null && document.DocumentPeople.Count > 0 )
                            {

                                foreach ( var item in document.DocumentPeople )
                                {
                                    if (
                                      item.DocumentPersonTypeId == "41" ) //مقرله
                                    {
                                        vakilCount++;

                                        if ( !string.IsNullOrWhiteSpace ( vakilNames ) )
                                            vakilNames += "، ";

                                        vakilNames += item?.FullName ();
                                    }
                                }

                            }

                            if ( vakilCount > 1 )
                                summary.Append ( "وکلای سند، " );
                            else
                                summary.Append ( "وکیل سند، " );

                            summary.Append ( vakilNames );

                            if ( vakilCount > 1 )
                                summary.Append ( " استعفا دادند." );
                            else
                                summary.Append ( " استعفا داد." );

                            break;

                        case "711": //سند اقاله اموال غيرمنقول
                        case "721": //سند اقاله وسايل نقليه
                        case "731": //ساير اسناد اقاله
                            summary.AppendLine ( "در " + scriptoriumTitle );

                            summary.AppendLine ();
                            summary.Append ( document.DocumentType.Title + " " );
                            summary.Append ( "توسط " );
                            summary.Append ( scriptoriumTitle + " " );
                            summary.Append ( "با شناسه " );
                            summary.Append ( document.NationalNo + " " );
                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentDate + " " );
                            summary.Append ( "صادر شد." );

                            break;

                        case "0028": //آراء اعلامي محاکم قضايي - به غيراز آراي بي اعتباري و بطلان سند
                                     //حکم دادگاه به شماره ..........  با موضوع  ..................... در تاریخ ................... ثبت شد.

                            summary.Append ( "حکم دادگاه به شماره " );
                            summary.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                            summary.Append ( "با موضوع " );
                            summary.Append ( document.DocumentType.Title + " " );
                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentInfoJudgement.IssueDate + " " );
                            summary.Append ( "ثبت شد." );
                            break;

                        case "0030"://اعلام انجام تعهد
                                    //متعهدله طی نامه شماره ............... اعلام نموده که متعهد به تعهدات خود عمل نموده است لذا این تعهدنامه انجام شده تلقی می گردد.
                            summary.Append ( "متعهدله طی نامه شماره " );
                            summary.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                            summary.Append ( "اعلام نموده که متعهد به تعهدات خود عمل نموده است لذا این تعهدنامه انجام شده تلقی می گردد." );
                            break;

                        case "0031": //درج مفاد بازداشت مازاد مال مورد وثيقه 
                                     // مفاد بازداشت مازاد مال مورد وثیقه  سند، مطابق نامه شماره ......................... اداره ....................... در تاریخ .................... درج شد.

                            summary.Append ( "مفاد بازداشت مازاد مال مورد وثیقه  سند، مطابق نامه شماره " );
                            summary.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                            summary.Append ( "اداره " );
                            summary.Append ( document.DocumentInfoJudgement.IssuerName + " " );
                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentInfoJudgement.IssueDate + " " );
                            summary.Append ( "درج شد." );
                            break;
                        case "0032": //قيد وثيقه بودن مال در قبال طلب بستانکار بابت معامله مؤخر
                                     //مراتب وثیقه بودن مال در قبال طلب بستانکار،بابت معامله موخر و وجوه پرداختی وی بابت طلب بستانکارمقدم به میزان .............. در تاریخ ............ ثبت شد.

                            summary.Append ( "مراتب وثیقه بودن مال در قبال طلب بستانکار،بابت معامله موخر و وجوه پرداختی وی بابت طلب بستانکارمقدم به میزان " );
                            summary.Append ( document.Price + " " );
                            summary.Append ( "در تاریخ " );
                            summary.Append ( document.DocumentInfoJudgement.IssueDate + " " );
                            summary.Append ( "ثبت شد." );
                            break;
                        case "0033"://قبوض اقساطي
                                    // تعداد ....... برگ قبوض اقساطي در تاريخ ......... به مبلغ .......... صادر شد. تـــــوضــــــيحـــــــــات 

                            summary.Append ( "تعداد " );
                            summary.Append ( document.DocumentInfoOther.RegisterCount + " " );
                            summary.Append ( "برگ قبوض اقساطي در تاريخ " );
                            summary.Append ( document.DocumentDate + " " );
                            summary.Append ( "به مبلغ " );
                            summary.Append ( document.Price + " " );
                            summary.Append ( "صادر شد. " );
                            summary.Append ( document.DocumentInfoText.Description );
                            break;
                        case "0050": //سایر ملاحظات سند

                            summary.Append ( document.DocumentInfoText.LegalText );
                            break;

                        default:
                            break;

                    }
                    if ( currentCMSOrganizationID == null && userService.UserApplicationContext.BranchAccess.BranchId != null )
                        currentCMSOrganizationID = userService.UserApplicationContext.BranchAccess.BranchId;

                    if ( currentCMSOrganizationID == document.ScriptoriumId )
                    {
                        annotationPack.RelatedDocObjectID = document.Id.ToString ();
                        annotationPack.IsLocalDocument = true;
                    }

                    annotationPack.RelatedDocContext = summary?.ToString ();
                    annotationPack.RelatedDocBriefDescription =
                       System.Environment.NewLine +
                       document.DocumentType.Title +
                       System.Environment.NewLine +
                       "در تاریخ " + document.DocumentDate +
                       System.Environment.NewLine +
                       summary?.ToString ();

                    annotationPack.RelatedDocNationalNo = document.NationalNo;
                    annotationPack.RelatedDocDate = document.DocumentDate;
                }

            }

            return annotationPack;
        }

        /// <summary>
        /// The GenerateAnnotations
        /// </summary>
        /// <param name="state">The state<see cref="string"/></param>
        /// <param name="scriptoriumID">The scriptoriumID<see cref="string"/></param>
        /// <param name="no">The no<see cref="List{string}"/></param>
        /// <param name="currentCMSOrganizationID">The currentCMSOrganizationID<see cref="string?"/></param>
        /// <returns>The <see cref="Task{List{AnnotationPack}}"/></returns>
        public async Task<List<AnnotationPack>> GenerateAnnotations ( string state, string scriptoriumID, List<string> no, string? currentCMSOrganizationID = null )
        {
            Notary.SSAA.BO.Domain.Entities.Document ? document=null;
            List<string> detailList=new List<string>();
            List<RequsetType>? requestTypes=null;
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            requestTypes = await documentRepository.GetRequestTypes ( state, scriptoriumID, no, token );
            List<AnnotationPack> annotationPacks=new List<AnnotationPack>();
            StringBuilder? summary=null;
            string scriptoriumTitle =userService.UserApplicationContext.BranchAccess.BranchName; //document.RelatedDocumentScriptorium.Substring(0, document.RelatedDocumentScriptorium.IndexOf("استان") - 1).Replace("دفترخانه ", "دفتر ").Replace("شماره ", "").Replace("شهر ", "");

            if ( requestTypes != null )
            {
                foreach ( var item in requestTypes )
                {
                    document = await GetDocumentBaseDocumentType ( item.Id, item.DocumentId );
                    if ( document != null )
                    {
                        AnnotationPack annotationPack=new AnnotationPack();

                        switch ( item.Id )
                        {
                            case "004": //فک رهن
                                summary = new StringBuilder ();
                                string annotationTextSingular = "مورد ثبتی {0} با شناسه {1} در تاریخ {2} فک رهن شده است.";
                                string annotationTextPlural = "موارد ثبتی {0} با شناسه {1} در تاریخ {2} فک رهن شده است.";
                                if ( document.DocumentCases != null && document.DocumentCases.Count > 1 )
                                {
                                    annotationTextPlural = string.Format ( annotationTextPlural, document.Cases (), document.NationalNo, document.WriteInBookDate );
                                    summary?.AppendLine ( annotationTextPlural );
                                }
                                else
                                {
                                    annotationTextSingular = string.Format ( annotationTextSingular, document.Cases (), document.NationalNo, document.WriteInBookDate );
                                    summary?.AppendLine ( annotationTextSingular );
                                }

                                break;
                            case "005": //فسخ سند

                                summary?.Append ( "سند در تاریخ " + document.DocumentDate + " با شناسه " + document.NationalNo + " فسخ شده است. " );

                                break;
                            case "006": //عزل وكيل

                                int vakilCount = 0;
                                string ? vakilNames = null;
                                foreach ( DocumentPerson documentPerson in document.DocumentPeople )
                                {
                                    if ( documentPerson.DocumentPersonTypeId != null &&
                                        documentPerson.DocumentPersonTypeId == "16" )
                                    {
                                        vakilCount++;

                                        if ( !string.IsNullOrWhiteSpace ( vakilNames ) )
                                            vakilNames += "، ";

                                        vakilNames += documentPerson.FullName ();
                                    }
                                }

                                if ( vakilCount > 1 )
                                    summary?.Append ( "وکلای سند، " );
                                else
                                    summary?.Append ( "وکیل سند، " );

                                summary?.Append ( vakilNames + "، " );

                                summary?.Append ( " در تاریخ " );
                                summary?.Append ( document.WriteInBookDate + " " );
                                summary?.Append ( "با شناسه " );
                                summary?.Append ( document.NationalNo + " " );

                                if ( vakilCount > 1 )
                                    summary?.Append ( "عزل شدند." );
                                else
                                    summary?.Append ( "عزل شد." );

                                break;
                            case "0022": //استعفای وکیل

                                vakilCount = 0;
                                vakilNames = null;
                                foreach ( DocumentPerson documentPerson in document.DocumentPeople )
                                {
                                    if (
                                        documentPerson.DocumentPersonTypeId == "16" )
                                    {
                                        vakilCount++;

                                        if ( !string.IsNullOrWhiteSpace ( vakilNames ) )
                                            vakilNames += "، ";

                                        vakilNames += documentPerson.FullName ();
                                    }
                                }

                                if ( vakilCount > 1 )
                                    summary?.Append ( "وکلای سند، " );
                                else
                                    summary?.Append ( vakilNames + "، " );

                                summary?.Append ( vakilNames + "، " );

                                summary?.Append ( " در تاریخ " );
                                summary?.Append ( document.WriteInBookDate + " " );
                                summary?.Append ( "با شناسه " );
                                summary?.Append ( document.NationalNo + " " );

                                if ( vakilCount > 1 )
                                    summary?.Append ( "استعفا نمودند." );
                                else
                                    summary?.Append ( "استعفا نمود." );

                                break;
                            case "008": //اخطاریه

                                string? persons = null;
                                foreach ( DocumentPerson documentPerson in document.DocumentPeople )
                                {
                                    if ( documentPerson.DocumentPersonTypeId == "57" ) //مخاطب
                                    {
                                        if ( !string.IsNullOrWhiteSpace ( persons ) )
                                            persons += " و ";

                                        if ( documentPerson.SexType == SexType.Female.GetString () )
                                        {
                                            persons += "خانم " + documentPerson.FullName ();

                                        }
                                        else
                                        if ( documentPerson.SexType == SexType.Male.GetString () )
                                        {
                                            persons += "آقای " + documentPerson.FullName ();

                                        }
                                        else
                                        if ( documentPerson.SexType == SexType.None.GetString () )

                                        {

                                            persons += documentPerson.FullName ();

                                        }

                                    }
                                }
                                summary?.Append ( "برای " );
                                if ( !string.IsNullOrWhiteSpace ( persons ) )
                                {
                                    summary?.Append ( persons );
                                }
                                else
                                {
                                    summary?.Append ( "این سند" );
                                }
                                summary?.Append ( " اخطاریه صادر شد." );

                                break;
                            case "007": //اجراییه

                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentDate + " " );
                                summary?.Append ( "با شناسه " );
                                summary?.Append ( document.NationalNo + " " );
                                summary?.Append ( " اجراییه صادر شد." );

                                break;
                            case "0012": //گواهي علت عدم انجام معامله //عدم حضور 

                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentDate + " " );
                                summary?.Append ( "با شناسه " );
                                summary?.Append ( document.NationalNo + " " );
                                summary?.Append ( " گواهی عدم حضور صادر شد." );

                                break;
                            case "009": //رونوشت

                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentDate + " " );
                                summary?.Append ( "با شناسه " );
                                summary?.Append ( document.NationalNo + " " );
                                summary?.Append ( " رونوشت صادر شد." );

                                break;
                            case "002": //قبض سپرده

                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentDate + " " );
                                summary?.Append ( "با شناسه " );
                                summary?.Append ( document.NationalNo + " " );
                                summary?.Append ( " قبض سپرده صادر شد." );

                                break;
                            case "0023": // بی اعتباری

                                summary?.Append ( "بر اساس حکم دادگاه به شماره " );
                                summary?.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentDate + " " );
                                summary?.Append ( "مراتب بی اعتباری سند ثبت شد." );

                                break;

                            case "0035": // بی اعتباری بخشی از سند

                                summary?.Append ( "بر اساس حکم دادگاه به شماره " );
                                summary?.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentDate + " " );
                                summary?.Append ( "مراتب بی اعتباری بخشی از سند ثبت شد." );

                                break;
                            case "0024": //رفع بی اعتباری

                                summary?.Append ( "بر اساس حکم دادگاه به شماره " );
                                summary?.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentDate + " " );
                                summary?.Append ( "مراتب رفع بی اعتباری سند ثبت شد." );

                                break;

                            case "0025": //تخلیه عین مستاجره

                                summary?.Append ( "بر اساس دادنامه شماره " );
                                summary?.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                                summary?.Append ( "مورخ " );
                                summary?.Append ( document.DocumentInfoJudgement.IssueDate + " " );
                                summary?.Append ( "مرجع " );
                                summary?.Append ( document.DocumentInfoJudgement.IssuerName + " " );
                                summary?.Append ( "عین مستاجره " );
                                if ( document.DocumentEstates != null && document.DocumentEstates.Count > 0 )
                                {
                                    summary?.Append ( "در تاریخ " );
                                    summary?.Append ( document.DocumentEstates.ElementAt ( 0 ).EvacuatedDate + " " );
                                }
                                summary?.Append ( "تخلیه شد." );

                                break;
                            case "0026": // ختم پرونده اجرایی

                                summary?.Append ( "بر اساس نامه شماره " );
                                summary?.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                                summary?.Append ( "تاریخ " );
                                summary?.Append ( document.DocumentInfoJudgement.IssueDate + " " );
                                summary?.Append ( "اداره اجرایی " );
                                summary?.Append ( document.DocumentInfoJudgement.IssuerName + "، " );
                                summary?.Append ( "ختم پرونده اجرایی اعلان شد." );

                                break;
                            case "0027": //تغییر نشانی

                                if ( document.DocumentPeople != null && document.DocumentPeople.Count > 0 )
                                {
                                    var person= document.DocumentPeople.ElementAt ( 0 );
                                    summary?.Append ( "با مراجعه " );

                                    if ( ( document.DocumentPeople.ElementAt ( 0 ) ).SexType == SexType.Male.GetString () )
                                        summary?.Append ( "آقای " );
                                    else if ( ( document.DocumentPeople.ElementAt ( 0 ) ).SexType == SexType.Female.GetString () )
                                        summary?.Append ( "خانم " );

                                    summary?.Append ( ( document.DocumentPeople.ElementAt ( 0 ) ).FullName () + " " );

                                    summary?.Append ( "در تاریخ " );
                                    summary?.Append ( document.DocumentDate + "، " );
                                    summary?.Append ( "نشانی ایشان به " );
                                    summary?.Append ( document.DocumentPeople.ElementAt ( 0 ).Address + " " );

                                    if ( !string.IsNullOrWhiteSpace ( document.DocumentPeople.ElementAt ( 0 ).PostalCode ) )
                                    {
                                        summary?.Append ( "و کد پستی " );
                                        summary?.Append ( person.PostalCode + " " );
                                    }

                                    if ( !string.IsNullOrWhiteSpace ( person.Tel ) )
                                    {
                                        summary?.Append ( "با شماره تلفن ثابت " );
                                        summary?.Append ( person.Tel + " " );
                                    }

                                    if ( !string.IsNullOrWhiteSpace ( person.MobileNo ) )
                                    {
                                        summary?.Append ( "با شماره تلفن همراه " );
                                        summary?.Append ( person.MobileNo + " " );
                                    }

                                    summary?.Append ( "تغییر یافت." );
                                }
                                else
                                {
                                    summary?.Append ( "تغییر نشانی انجام شده است." );
                                }

                                break;
                            case "913": //سند اقرارنامه اصلاحي

                                summary?.AppendLine ( "در " + scriptoriumTitle );

                                summary?.AppendLine ();
                                summary?.Append ( "سند اقرارنامه اصلاحی با " );

                                int caseCount = 0;
                                string? cases = null;
                                if ( document.DocumentCases != null )
                                {
                                    foreach ( var caseItem in document.DocumentCases )
                                    {
                                        caseCount++;

                                        if ( !string.IsNullOrWhiteSpace ( cases ) )
                                            cases += " و ";

                                        cases += caseItem.Title;
                                    }

                                }

                                if ( caseCount > 1 )
                                    summary?.Append ( "موضوعات " );
                                else
                                    summary?.Append ( "موضوع " );

                                summary?.Append ( cases + " " );
                                summary?.Append ( "توسط " );
                                summary?.Append ( scriptoriumTitle + " " );
                                summary?.Append ( "با شناسه " );
                                summary?.Append ( document.NationalNo + " " );
                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentDate + " " );
                                summary?.Append ( "صادر شد." );

                                break;

                            case "914": //سند اقرارنامه عزل وکيل

                                summary?.AppendLine ( "در " + scriptoriumTitle );

                                summary?.AppendLine ();
                                summary?.Append ( "مطابق سند تنظیم شده در " );
                                summary?.Append ( scriptoriumTitle + " " );
                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentDate + " " );
                                summary?.Append ( "با شناسه " );
                                summary?.Append ( document.NationalNo + " " );

                                vakilCount = 0;
                                vakilNames = null;
                                if ( document.DocumentPeople != null && document.DocumentPeople.Count > 0 )
                                {

                                    foreach ( var person in document.DocumentPeople )
                                    {
                                        if (
                                          person.DocumentPersonTypeId == "42" ) //مقرله
                                        {
                                            vakilCount++;

                                            if ( !string.IsNullOrWhiteSpace ( vakilNames ) )
                                                vakilNames += "، ";

                                            vakilNames += person.FullName ();
                                        }
                                    }

                                }

                                if ( vakilCount > 1 )
                                    summary?.Append ( "وکلای سند، " );
                                else
                                    summary?.Append ( "وکیل سند، " );

                                summary?.Append ( vakilNames );

                                if ( vakilCount > 1 )
                                    summary?.Append ( " عزل شدند." );
                                else
                                    summary?.Append ( " عزل شد." );

                                break;

                            case "915": // سند اقرارنامه استعفاي وکيل

                                summary?.AppendLine ( "در " + scriptoriumTitle );

                                summary?.AppendLine ();
                                summary?.Append ( "مطابق سند تنظیم شده در " );
                                summary?.Append ( scriptoriumTitle + " " );
                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentDate + " " );
                                summary?.Append ( "با شناسه " );
                                summary?.Append ( document.NationalNo + " " );

                                vakilCount = 0;
                                vakilNames = null;
                                if ( document.DocumentPeople != null && document.DocumentPeople.Count > 0 )
                                {

                                    foreach ( var person in document.DocumentPeople )
                                    {
                                        if (
                                          person.DocumentPersonTypeId == "41" ) //مقرله
                                        {
                                            vakilCount++;

                                            if ( !string.IsNullOrWhiteSpace ( vakilNames ) )
                                                vakilNames += "، ";

                                            vakilNames += person.FullName ();
                                        }
                                    }

                                }

                                if ( vakilCount > 1 )
                                    summary?.Append ( "وکلای سند، " );
                                else
                                    summary?.Append ( "وکیل سند، " );

                                summary?.Append ( vakilNames );

                                if ( vakilCount > 1 )
                                    summary?.Append ( " استعفا دادند." );
                                else
                                    summary?.Append ( " استعفا داد." );

                                break;

                            case "711": //سند اقاله اموال غيرمنقول
                            case "721": //سند اقاله وسايل نقليه
                            case "731": //ساير اسناد اقاله
                                summary?.AppendLine ( "در " + scriptoriumTitle );

                                summary?.AppendLine ();
                                summary?.Append ( document.DocumentType.Title + " " );
                                summary?.Append ( "توسط " );
                                summary?.Append ( scriptoriumTitle + " " );
                                summary?.Append ( "با شناسه " );
                                summary?.Append ( document.NationalNo + " " );
                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentDate + " " );
                                summary?.Append ( "صادر شد." );

                                break;

                            case "0028": //آراء اعلامي محاکم قضايي - به غيراز آراي بي اعتباري و بطلان سند
                                         //حکم دادگاه به شماره ..........  با موضوع  ..................... در تاریخ ................... ثبت شد.

                                summary?.Append ( "حکم دادگاه به شماره " );
                                summary?.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                                summary?.Append ( "با موضوع " );
                                summary?.Append ( document.DocumentType.Title + " " );
                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentInfoJudgement.IssueDate + " " );
                                summary?.Append ( "ثبت شد." );
                                break;

                            case "0030"://اعلام انجام تعهد
                                        //متعهدله طی نامه شماره ............... اعلام نموده که متعهد به تعهدات خود عمل نموده است لذا این تعهدنامه انجام شده تلقی می گردد.
                                summary?.Append ( "متعهدله طی نامه شماره " );
                                summary?.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                                summary?.Append ( "اعلام نموده که متعهد به تعهدات خود عمل نموده است لذا این تعهدنامه انجام شده تلقی می گردد." );
                                break;

                            case "0031": //درج مفاد بازداشت مازاد مال مورد وثيقه 
                                         // مفاد بازداشت مازاد مال مورد وثیقه  سند، مطابق نامه شماره ......................... اداره ....................... در تاریخ .................... درج شد.

                                summary?.Append ( "مفاد بازداشت مازاد مال مورد وثیقه  سند، مطابق نامه شماره " );
                                summary?.Append ( document.DocumentInfoJudgement.IssueNo + " " );
                                summary?.Append ( "اداره " );
                                summary?.Append ( document.DocumentInfoJudgement.IssuerName + " " );
                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentInfoJudgement.IssueDate + " " );
                                summary?.Append ( "درج شد." );
                                break;
                            case "0032": //قيد وثيقه بودن مال در قبال طلب بستانکار بابت معامله مؤخر
                                         //مراتب وثیقه بودن مال در قبال طلب بستانکار،بابت معامله موخر و وجوه پرداختی وی بابت طلب بستانکارمقدم به میزان .............. در تاریخ ............ ثبت شد.

                                summary?.Append ( "مراتب وثیقه بودن مال در قبال طلب بستانکار،بابت معامله موخر و وجوه پرداختی وی بابت طلب بستانکارمقدم به میزان " );
                                summary?.Append ( document.Price + " " );
                                summary?.Append ( "در تاریخ " );
                                summary?.Append ( document.DocumentInfoJudgement.IssueDate + " " );
                                summary?.Append ( "ثبت شد." );
                                break;
                            case "0033"://قبوض اقساطي
                                        // تعداد ....... برگ قبوض اقساطي در تاريخ ......... به مبلغ .......... صادر شد. تـــــوضــــــيحـــــــــات 

                                summary?.Append ( "تعداد " );
                                summary?.Append ( document.DocumentInfoOther.RegisterCount + " " );
                                summary?.Append ( "برگ قبوض اقساطي در تاريخ " );
                                summary?.Append ( document.DocumentDate + " " );
                                summary?.Append ( "به مبلغ " );
                                summary?.Append ( document.Price + " " );
                                summary?.Append ( "صادر شد. " );
                                summary?.Append ( document.DocumentInfoText.Description );
                                break;
                            case "0050": //سایر ملاحظات سند

                                summary?.Append ( document.DocumentInfoText.LegalText );
                                break;

                            default:
                                break;

                        }
                        if ( currentCMSOrganizationID == null && userService.UserApplicationContext.BranchAccess.BranchId != null )
                            currentCMSOrganizationID = userService.UserApplicationContext.BranchAccess.BranchId;

                        if ( currentCMSOrganizationID == document.ScriptoriumId )
                        {
                            annotationPack.RelatedDocObjectID = document.Id.ToString ();
                            annotationPack.IsLocalDocument = true;
                        }

                        annotationPack.RelatedDocContext = summary?.ToString ();
                        annotationPack.RelatedDocBriefDescription =
                           System.Environment.NewLine +
                           document.DocumentType.Title +
                           System.Environment.NewLine +
                           "در تاریخ " + document.DocumentDate +
                           System.Environment.NewLine +
                           summary?.ToString ();

                        annotationPack.RelatedDocNationalNo = document.NationalNo;
                        annotationPack.RelatedDocDate = document.DocumentDate;

                        annotationPacks.Add ( annotationPack );
                    }

                }

            }

            return annotationPacks;
        }

        /// <summary>
        /// The GenerateSingleAgentAnnotation
        /// </summary>
        /// <param name="documentPersonRelatedAgentDocuments">The documentPersonRelatedAgentDocuments<see cref="List{DocumentPersonRelated}"/></param>
        /// <returns>The <see cref="AnnotationPack"/></returns>
        public static AnnotationPack GenerateSingleAgentAnnotation ( List<DocumentPersonRelated> documentPersonRelatedAgentDocuments )
        {
            AnnotationPack? annotationPack = null;
            annotationPack = new AnnotationPack ();
            annotationPack.IsLocalDocument = false;

            StringBuilder sb = new StringBuilder();
            if ( documentPersonRelatedAgentDocuments != null )
            {

                foreach ( var item in documentPersonRelatedAgentDocuments )
                {
                    switch ( item.AgentTypeId )
                    {
                        case "10": //معتمد 

                            string? annotationText = null;
                            if ( item.ReliablePersonReasonId == Enumerations.NotaryNeedToReliableReason.BiSavad.GetString () )
                            {
                                annotationText = "چون {0} سواد نداشت، {1} معتمداً سند را برایش خواند، رضایت داشت.";
                                annotationText = string.Format ( annotationText, item.MainPerson.FullName (), item.AgentPerson.FullName () );
                                sb.AppendLine ( annotationText );
                            }
                            else if ( item.ReliablePersonReasonId == Enumerations.NotaryNeedToReliableReason.KarKourGongBisavad.GetString () )
                            {
                                annotationText = "چون {0} از اشخاص مشمول ماده 64 قانون ثبت اسناد و املاک بود، {1} که توانایی تفهیم مطالب را به وی داشت، معتمداً ثبت و سند را به وی تفهیم نمود.";
                                annotationText = string.Format ( annotationText, item.MainPerson.FullName (), item.AgentPerson.FullName () );
                                sb.AppendLine ( annotationText );
                            }
                            else if ( item.ReliablePersonReasonId == Enumerations.NotaryNeedToReliableReason.Bimar.GetString () )
                            {
                                annotationText = "چون {0} بیمار بود، سند با حضور {1} به عنوان معتمد تنظیم شد.";
                                annotationText = string.Format ( annotationText, item.MainPerson.FullName (), item.AgentPerson.FullName () );
                                sb.AppendLine ( annotationText );
                            }
                            else if ( item.ReliablePersonReasonId == Enumerations.NotaryNeedToReliableReason.BiDastVaPa.GetString () )
                            {
                                annotationText = "چون {0} دست و پا نداشت، سند با حضور {1} به عنوان معتمد در محل دفترخانه تنظیم شد.";
                                annotationText = string.Format ( annotationText, item.MainPerson.FullName (), item.AgentPerson.FullName () );
                                sb.AppendLine ( annotationText );
                            }
                            else if ( item.ReliablePersonReasonId == Enumerations.NotaryNeedToReliableReason.WithoutFingerprint.GetString () )
                            {
                                annotationText = "چون امکان اخذ اثرانگشت {0} وجود نداشت، سند با حضور {1} به عنوان معتمد در محل دفترخانه تنظیم شد.";
                                annotationText = string.Format ( annotationText, item.MainPerson.FullName (), item.AgentPerson.FullName () );
                                sb.AppendLine ( annotationText );
                            }

                            break;

                        case "11": //معرف 

                            sb = new StringBuilder ();
                            annotationText = null;
                            annotationText = "{0} اعلام نمود {1} را می شناسد و هویت وی را به سردفتر معرفی و گواهی می نماید.";
                            annotationText = string.Format ( annotationText, item.MainPerson.FullName (), item.AgentPerson.FullName () );
                            sb.AppendLine ( annotationText );

                            break;

                        case "12":  //مترجم

                            sb = new StringBuilder ();
                            annotationText = null;
                            annotationText = "چون {0} فارسی نمی دانست، {1} به عنوان مترجم رسمی، سند را برای وی کاملاً ترجمه نمود، رضایت داشت.";
                            annotationText = string.Format ( annotationText, item.MainPerson.FullName (), item.AgentPerson.FullName () );
                            sb.AppendLine ( annotationText );

                            break;

                        case "14": //دادستان

                            if ( item.ReliablePersonReasonId == Enumerations.NotaryNeedToReliableReason.Zendanyan.GetString () ||
                                item.ReliablePersonReasonId == Enumerations.NotaryNeedToReliableReason.Bimar.GetString () )
                            {
                                annotationText = "سند با حضور {0} بعنوان نماینده دادستان تنظیم و ثبت شد.";
                                annotationText = string.Format ( annotationText, item.AgentPerson.FullName () );
                                sb.AppendLine ( annotationText );
                            }
                            else //(theOneDocAgent.NeedToReliableReason == Rad.CMS.Enumerations.NotaryNeedToReliableReason.SardaftarDaftaryar)
                            {
                                annotationText = "سند با حضور {0} بعنوان دادستان/ نماینده دادستان / رئیس دادگاه بخش / نماینده رئیس دادگاه بخش تنظیم و ثبت شد.";
                                annotationText = string.Format ( annotationText, item.AgentPerson.FullName () );
                                sb.AppendLine ( annotationText );
                            }

                            break;

                        case "15": //شاهد 

                            sb = new StringBuilder ();
                            sb.Append ( "اینجانب " );
                            sb.Append ( item.AgentPerson.FullName () + " " );
                            sb.Append ( "اعلام می نمایم، " );
                            sb.Append ( item.MainPerson.FullName () + " " );
                            sb.Append ( "را می شناسم و هویت وی را به سردفتر شهادت دادم." );

                            break;

                        default:
                            break;
                    }

                }
            }

            return annotationPack;
        }

        /// <summary>
        /// The GenerateAnnotationsForOnePerson
        /// </summary>
        /// <param name="theOneDocPerson">The theOneDocPerson<see cref="DocumentPerson"/></param>
        /// <returns>The <see cref="Task{List{AnnotationPack}?}"/></returns>
        public async Task<List<AnnotationPack>?> GenerateAnnotationsForOnePerson ( DocumentPerson theOneDocPerson )
        {
            List<DocumentPerson> thePersonsToGenerateAnnotationFor = new List<DocumentPerson>() { theOneDocPerson };
            List<AnnotationPack> annotationsCollection = new List<AnnotationPack>(){ };
            if ( theOneDocPerson.IsRelated != YesNo.Yes.GetString () || ( theOneDocPerson.DocumentPersonRelatedAgentPeople != null && theOneDocPerson.DocumentPersonRelatedAgentPeople.Count > 0 ) )
            {
                //Note : 
                //در صورتی که شخص ورودی، از نوع وکیل و نماینده و ... نباشد، ممکن است در پرونده شخصی با همین کد ملی وجود داشته باشد که این شخص نماینده و وکیل و معتمد و ... باشد.
                // بتابراین سعی شده است شخصی با این خصوصیات را در پرونده پیدا کنیم که در صورت لزوم برایش توضیحات لازم به درج در اثرانگشت را تولید نماییم.
                // این اتفاق در صورتی رخ می دهد که در پرونده شخصی با کد ملی یکسان دارای سمت های متفاوت باشد.

                if ( string.IsNullOrWhiteSpace ( theOneDocPerson.NationalNo ) )
                    return null;
                var thePersonsCollection=await documentRepository.GetAnnotationsForOnePerson ( theOneDocPerson.NationalNo, theOneDocPerson.Id.ToString(), theOneDocPerson.DocumentId.ToString() );
                if ( thePersonsCollection == null || thePersonsCollection.Count == 0 )
                    return null;
                else
                {
                    foreach ( var item in thePersonsCollection )
                    {
                        thePersonsToGenerateAnnotationFor.Add ( item );

                    }
                }
            }

            List<string> agentTypesForAnnotationGeneration = new List<string>() { "10", "11", "12", "14", "15" };
            foreach ( var theOneAgentIncludedPerson in thePersonsToGenerateAnnotationFor )
            {
                foreach ( var theOneAgentTypeCode in agentTypesForAnnotationGeneration )
                {
                    List< Notary.SSAA.BO.Domain.Entities.DocumentPersonRelated> currentCodeAgents = new List<DocumentPersonRelated>();
                    foreach ( DocumentPersonRelated theOneDocAgent in theOneAgentIncludedPerson.DocumentPersonRelatedAgentPeople )
                    {
                        if ( theOneDocAgent.AgentTypeId != theOneAgentTypeCode )
                            continue;

                        currentCodeAgents.Add ( theOneDocAgent );
                    }

                    if ( currentCodeAgents.Count == 0 )
                        continue;


                    var theSingleAnnotationPack = GenerateSingleAgentAnnotation(currentCodeAgents);
                    if (!annotationsCollection.Contains(theSingleAnnotationPack))
                    {
                        annotationsCollection.Add(theSingleAnnotationPack);
                    }

                }
            }

            return annotationsCollection;
        }

        /// <summary>
        /// The GenerateAnnotationsForOnePerson
        /// </summary>
        /// <param name="theOnePersonObjectID">The theOnePersonObjectID<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{AnnotationPack}?}"/></returns>
        public async Task<List<AnnotationPack>?> GenerateAnnotationsForOnePerson ( string theOnePersonObjectID )
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            var theOneDocPerson =await documentPersonRepository.GetDocumentPersonById(Guid.Parse(theOnePersonObjectID),new List<string>{ "DocumentPersonRelatedAgentPeople" } ,token);
            return await GenerateAnnotationsForOnePerson ( theOneDocPerson );
        }

        /// <summary>
        /// The GenerateReliablePersonsAnnotations
        /// </summary>
        /// <param name="nationalno">The nationalno<see cref="string"/></param>
        /// <returns>The <see cref="Task{AnnotationPack?}"/></returns>
        public async Task<AnnotationPack?> GenerateReliablePersonsAnnotations ( string nationalno )
        {

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            var theDocAgentsCollection =await documentPersonRelatedRepository.GetDocumentPersonRelated ( new List<string> {"AgentPerson", "MainPerson"}, token, null, null, null, nationalno,new List<string> { "11" } );//// معرف
            if ( theDocAgentsCollection == null || theDocAgentsCollection.Count == 0 )
                return null;
            StringBuilder sb = new StringBuilder();
            string? annotationText = null;
            foreach ( var theOneReliablePerson in theDocAgentsCollection )
            {
                annotationText = "{0} اعلام نمود {1} را می شناسد و هویت وی را به سردفتر معرفی و گواهی می نماید.";
                annotationText = string.Format ( annotationText, theOneReliablePerson.AgentPerson.FullName (), theOneReliablePerson.MainPerson.FullName () );
                sb.AppendLine ( annotationText );
            }
            AnnotationPack annotationPack = new AnnotationPack();
            annotationPack.RelatedDocContext = sb.ToString ();
            annotationPack.RelatedDocBriefDescription = sb.ToString ();

            return annotationPack;
        }

        /// <summary>
        /// The GetDocumentBaseDocumentType
        /// </summary>
        /// <param name="requestTypeId">The requestTypeId<see cref="string"/></param>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentBaseDocumentType ( string requestTypeId, string documentId )
        {
            List<string> detailList=new List<string>();

            switch ( requestTypeId )
            {
                case "004": //فک رهن
                    detailList.Add ( "DocumentCases" );

                    break;
                case "005": //فسخ سند

                    break;
                case "006": //عزل وكيل
                    detailList.Add ( "DocumentPeople" );
                    break;
                case "0022": //استعفای وکیل

                    detailList.Add ( "DocumentPeople" );

                    break;
                case "008": //اخطاریه

                    detailList.Add ( "DocumentPeople" );

                    break;
                case "007": //اجراییه

                    break;
                case "0012": //گواهي علت عدم انجام معامله //عدم حضور 

                    break;
                case "009": //رونوشت

                    break;
                case "002": //قبض سپرده

                    break;
                case "0023": // بی اعتباری

                    detailList.Add ( "DocumentInfoJudgment" );

                    break;

                case "0035": // بی اعتباری بخشی از سند

                    detailList.Add ( "DocumentInfoJudgment" );

                    break;
                case "0024": //رفع بی اعتباری

                    detailList.Add ( "DocumentInfoJudgment" );

                    break;

                case "0025": //تخلیه عین مستاجره

                    detailList.AddRange ( new List<string> { "DocumentInfoJudgment", "DocumentEstates" } );
                    break;
                case "0026": // ختم پرونده اجرایی

                    detailList.Add ( "DocumentInfoJudgment" );

                    break;
                case "0027": //تغییر نشانی

                    detailList.Add ( "DocumentPeople" );

                    break;
                case "913": //سند اقرارنامه اصلاحي

                    detailList.Add ( "DocumentCases" );

                    break;

                case "914": //سند اقرارنامه عزل وکيل

                    detailList.Add ( "DocumentPeople" );

                    break;

                case "915": // سند اقرارنامه استعفاي وکيل

                    detailList.Add ( "DocumentPeople" );

                    break;

                case "711": //سند اقاله اموال غيرمنقول
                case "721": //سند اقاله وسايل نقليه
                case "731": //ساير اسناد اقاله

                    break;

                case "0028": //آراء اعلامي محاکم قضايي - به غيراز آراي بي اعتباري و بطلان سند
                             //حکم دادگاه به شماره ..........  با موضوع  ..................... در تاریخ ................... ثبت شد.

                    detailList.Add ( "DocumentInfoJudgment" );

                    break;

                case "0030"://اعلام انجام تعهد
                            //متعهدله طی نامه شماره ............... اعلام نموده که متعهد به تعهدات خود عمل نموده است لذا این تعهدنامه انجام شده تلقی می گردد.
                    detailList.Add ( "DocumentInfoJudgment" );

                    break;

                case "0031": //درج مفاد بازداشت مازاد مال مورد وثيقه 
                             // مفاد بازداشت مازاد مال مورد وثیقه  سند، مطابق نامه شماره ......................... اداره ....................... در تاریخ .................... درج شد.

                    detailList.Add ( "DocumentInfoJudgment" );

                    break;
                case "0032": //قيد وثيقه بودن مال در قبال طلب بستانکار بابت معامله مؤخر
                             //مراتب وثیقه بودن مال در قبال طلب بستانکار،بابت معامله موخر و وجوه پرداختی وی بابت طلب بستانکارمقدم به میزان .............. در تاریخ ............ ثبت شد.

                    detailList.Add ( "DocumentInfoJudgment" );

                    break;
                case "0033"://قبوض اقساطي
                            // تعداد ....... برگ قبوض اقساطي در تاريخ ......... به مبلغ .......... صادر شد. تـــــوضــــــيحـــــــــات 

                    detailList.Add ( "DocumentInfoOther" );

                    break;
                case "0050": //سایر ملاحظات سند
                    detailList.Add ( "DocumentInfoText" );

                    break;

                default:
                    break;

            }
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            return await documentRepository.GetDocumentById ( Guid.Parse ( documentId ), detailList, token );
        }
    }

}
