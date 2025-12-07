using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace Notary.SSAA.BO.SharedKernel.Enumerations
{


    [Description("EnmAcademicDegree")]
    public enum AcademicDegree
    {
        [Description("فوق ديپلم")]
        AssociateDegree = 3,
        [Description("ليسانس")]
        BachelorsDegree = 4,
        [Description("کمتر از ديپلم")]
        BeforeDiploma = 1,
        [Description("ديپلم")]
        Diploma = 2,
        [Description("دکتري")]
        Doctorate = 6,
        [Description("فوق ليسانس")]
        MastersDegree = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmActionType")]
    public enum ActionType
    {
        [Description("آگهي")]
        Advertise = 508,
        [Description("کميسيون 1")]
        Comission1 = 505,
        [Description("کميسيون 2")]
        Comission2 = 515,
        [Description("اظهارنامه")]
        Declaration = 501,
        [Description("کارشناسي")]
        Expertism = 502,
        [Description("نامه دريافتي از روزنامه رسمي")]
        InputFromNewspaper = 503,
        [Description("نامه وارده مالکيت معنوي")]
        InputLetter = 506,
        [Description("ابلاغيه")]
        Notice = 509,
        [Description("نامه صادره مالکيت معنوي")]
        OutputLetter = 507,
        [Description("اظهارنامه حاصل از انتقال جزيي")]
        PartialTransferDeclaration = 500,
        [Description("اصلاح اشتباه اداري")]
        UserError = 518,
        [Description("")]
        None = 0
    }
    [Description("EnmAddressType")]
    public enum AddressType
    {
        [Description("اعلامي متعهد له")]
        Creditor = 2,
        [Description("اعلامي متعهد")]
        Debtor = 3,
        [Description("متن سند")]
        DocAddress = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmAdjacentLevel")]
    public enum AdjacentLevel
    {
        [Description("معبر")]
        Crossing = 3,
        [Description("قطعه")]
        Piece = 2,
        [Description("پلاک")]
        Plaque = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmAdvertisingState")]
    public enum AdvertisingState
    {
        [Description("تاييد شده")]
        Confirmed = 2,
        [Description("ابطال شده")]
        Disproofed = 7,
        [Description("پيش نويس شده")]
        PreSend = 6,
        [Description("ثبت شده جهت تاييد")]
        Registered = 1,
        [Description("برگشت داده شده")]
        Return = 4,
        [Description("ارسال شده")]
        Sended = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmAdvocatePenaltySubject")]
    public enum AdvocatePenaltySubject
    {
        [Description("وکلاي پايه يک دادگستري")]
        Advocate = 1,
        [Description("کارآموزان وکالت")]
        AdvocateStudent = 2,
        [Description("مشاوران حقوقي")]
        Consultant = 3,
        [Description("کارشناسان رسمي دادگستري")]
        Expert = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmAdvocatePenaltyType")]
    public enum AdvocatePenaltyType
    {
        [Description("تنزل درجه")]
        DownGrade = 3,
        [Description("انفـصـال دائم")]
        ForEverDismissal = 2,
        [Description("ساير موارد")]
        Others = 4,
        [Description("انفـصـال موقت")]
        TemporaryDismissal = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmAgentedPerson")]
    public enum AgentedPerson
    {
        [Description("متعهد له")]
        AgentCreditor = 2,
        [Description("متعهد")]
        AgentDebtor = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmAndOr")]
    public enum AndOr
    {
        [Description("و")]
        AndOperand = 2,
        [Description("يا")]
        OrOperand = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmArchiveReportType")]
    public enum ArchiveReportType
    {
        [Description("گزارش ليست پرونده هاي خارج از بايگاني")]
        DocumentOutOfArchive = 3,
        [Description("گزارش آماري")]
        Statistical = 1,
        [Description("گزارش آماري به تفکيک بايگان")]
        StatisticalByArchivist = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmArchiveStatus")]
    public enum ArchiveStatus
    {
        [Description("مفقود شده")]
        Lost = 3,
        [Description("خارج از بايگاني")]
        OutOfArchive = 2,
        [Description("داخل بايگاني")]
        InArchive = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmArrestEffectType")]
    public enum ArrestEffectType
    {
        [Description("تمامي عمليات شرکت متوقف شود")]
        Lock = 2,
        [Description("عمليات شخص مرتبط متوقف شود")]
        LockPerson = 3,
        [Description("صرفا تذکر داده شود")]
        WarningState = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmArrestType")]
    public enum ArrestType
    {
        [Description("بازداشت سرمايه")]
        Capital = 1,
        [Description("بازداشت سرمايه و سهم الشرکه")]
        CapitalAndStock = 3,
        [Description("بازداشت سرمايه / سهام")]
        CapitalAndStockNew = 7,
        [Description("ابطال ساير موارد شرکت")]
        CCompanyOtherItem = 6,
        [Description("ابطال نام شرکت")]
        InvalidCCompanyName = 5,
        [Description("ابطال شناسه شرکت")]
        InvalidCCompanyNationalCode = 4,
        [Description("بازداشت سهام")]
        Stock = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmAsIsToBeFlag")]
    public enum AsIsToBeFlag
    {
        [Description("فعلي")]
        AsIs = 1,
        [Description("آتي")]
        ToBe = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmAssetOrRight")]
    public enum AssetOrRight
    {
        [Description("اموال غيرمنقول")]
        ImmovableAsset = 1,
        [Description("اموال منقول")]
        LinkageAsset = 2,
        [Description("حق و امتياز")]
        Right = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmAsyncType")]
    public enum AsyncType
    {
        [Description("ثبت")]
        ToCommit = 2,
        [Description("حذف")]
        ToDelete = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmAttachmentProcessState")]
    public enum AttachmentProcessState
    {
        [Description("پرونده مربوطه درج شده است")]
        CaseInserted = 5,
        [Description("محتويات ذخيره نشده است")]
        ContentNotSaved = 14,
        [Description("محتويات ذخيره شده است")]
        ContentSaved = 13,
        [Description("چک مجدد شده و اصلاح نشده")]
        DoubleCheckedWithOutRepair = 7,
        [Description("چک مجدد شده و اصلاح شده")]
        DoubleCheckedWithRepair = 6,
        [Description("فايل ندارد")]
        FileDoesNotExists = 15,
        [Description("پيوست پيدا نشد")]
        FileNotFound = 3,
        [Description(" حجم پيوست از اندازه استادارد بزرگتر است")]
        FileSizeInvalid = 2,
        [Description("رکورد پيوست نا معتبر است")]
        Invalid = 10,
        [Description("پيوست سنواتي مي باشد")]
        NoneMechanized = 12,
        [Description("پيوست مربوط به ثبت شرکتها نيست")]
        NotACompanyAttachment = 9,
        [Description("نحوه ذخيره سازي با آدرس پيوست مطابقت ندارد")]
        PAFNSATInvalid = 11,
        [Description("مسير ذخيره سازي فايل نامعتبر است")]
        PathAndFileNameIsInvalid = 4,
        [Description("پيوست به درستي پردازش شده")]
        ProcessedSuccessfully = 1,
        [Description("مدرک مربوطه يافت نشد")]
        RelatedObjectNotFound = 8,
        [Description("")]
        None = 0
    }
    [Description("EnmAttachmentSaveType")]
    public enum AttachmentSaveType
    {
        [Description("ذخيره بر روي سامانه مديريت محتواي آلفرسکو")]
        Alfresco = 2,
        [Description("ذخيره بر روي هارد")]
        HDD = 1,
        [Description("ذخيره به صورت باينري در اراکل")]
        OraBinary = 3,
        [Description("ذخيره به صورت فايل در اراکل")]
        OraPFile = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmAttachmentType")]
    public enum AttachmentType
    {
        [Description("فايل اتوکد")]
        Autocad = 2,
        [Description("تصوير")]
        Image = 1,
        [Description("فايل آفيس")]
        Office = 4,
        [Description("ساير")]
        Other = 9,
        [Description("فيلم")]
        Video = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmBankType")]
    public enum BankType
    {
        [Description("موسسه مالي و اعتباري")]
        FinanceAndCreditInstitute = 3,
        [Description("قرض الحسنه")]
        GharzAlHasaneh = 4,
        [Description("دولتي")]
        Governmental = 2,
        [Description("خصوصي")]
        Private = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmBatchInsType")]
    public enum BatchInsType
    {
        [Description("اشخاص ثبت تاسيس در ثبت شرکت ها")]
        CRegReqPerson = 1,
        [Description("اشخاص ثبت تغيير در ثبت شرکت ها")]
        CTerMinutePerson = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmBearing")]
    public enum Bearing
    {
        [Description("ديوار باربر")]
        BarrierWall = 3,
        [Description("بتني")]
        Concrete = 2,
        [Description("ترکيبي")]
        Hybrid = 4,
        [Description("سازه سبک")]
        LiteStructure = 5,
        [Description("فلزي")]
        Metal = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmBettermentReportsName")]
    public enum BettermentReportsName
    {
        [Description("املاک - عمليات ثبتي")]
        AmaliateSabtiReport = 2,
        [Description("املاک-انواع اسناد مالکيت صادره بر اساس قوانين موضوعه")]
        AnvaeAsnadeMalekiatReport = 13,
        [Description("املاک-اسناد مالکيت صادره اعم از کاداستري و دفترچه اي")]
        AsnadeMalekiatReport = 14,
        [Description("املاک- بازداشت/رفع بازداشت")]
        BazdashtRafeBazdashtReport = 15,
        [Description("اسناد")]
        FactTable162_DestinationReport = 1,
        [Description("بازرسي و پاسخگوييي به شکايات")]
        FactTable167_DestinationReport = 3,
        [Description("املاک دولتي")]
        FactTable168_DestinationReport = 4,
        [Description("آرا")]
        FactTable170_DestinationReport = 5,
        [Description("پرونده هاي املاک")]
        FactTable171_DestinationReport = 6,
        [Description("پرونده ها - ساير")]
        FactTable173_DestinationReport = 7,
        [Description("دفاتر")]
        FactTable174_DestinationReport = 8,
        [Description("درآمدها")]
        FactTable175_DestinationReport = 9,
        [Description("ساير درآمدها")]
        FactTable176_DestinationReport = 10,
        [Description("اجرا ")]
        FactTable196_DestinationReport = 11,
        [Description("مالکيت صنعتي")]
        FactTable203_DestinationReport = 12,
        [Description("املاک-پاسخ به استعلامات و اعلام وضعيت")]
        PasokhBeEstelamat = 16,
        [Description("")]
        None = 0
    }
    [Description("EnmBlackListPersonRecognitionType")]
    public enum BlackListPersonRecognitionType
    {
        [Description("توسط پست الکترونيکي")]
        ByEmail = 3,
        [Description("توسط شماره تلفن همراه")]
        ByMobileNo = 2,
        [Description("توسط کد ملي")]
        ByNationalityCode = 1,
        [Description("توسط کد ملي و شماره تلفن همراه")]
        ByNationalityCodeAndMobileNo = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmBookPostState")]
    public enum BookPostState
    {
        [Description("مرسوله تحويل شعبه مقصد شده")]
        Delivered = 3,
        [Description("بارکد مرسوله صادر شده")]
        IssueBarcode = 1,
        [Description("مرسوله تحويل پست جهت ارسال به شعبه مقصد شده")]
        ReadyToSend = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCAcivityTime")]
    public enum CAcivityTime
    {
        [Description("محدود")]
        Limited = 1,
        [Description("نامحدود")]
        Unlimited = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCadastrResult")]
    public enum CadastrResult
    {
        [Description("نقشه مورد تاييد قرار گرفته شده")]
        ConfirmMap = 3,
        [Description("نقشه کاداستري دارد")]
        HaveCadastrMap = 7,
        [Description("نقشه مورد تاييد قرار نگرفته است")]
        NotConfirmMap = 4,
        [Description("فاقد نقشه کاداستري است")]
        NotHaveCadastrMap = 8,
        [Description("ارسال نشده")]
        NotSend = 1,
        [Description("ارسال شده")]
        Sended = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCAdvertismentType")]
    public enum CAdvertismentType
    {
        [Description("تغيير")]
        Changes = 2,
        [Description("تأسيس")]
        Establishment = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCAdvocacy")]
    public enum CAdvocacy
    {
        [Description("وکالتنامه مدني")]
        Civil = 1,
        [Description("وکالتنامه قضايي")]
        Judge = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCallCenterPriority")]
    public enum CallCenterPriority
    {
        [Description("فوري")]
        High = 3,
        [Description("کم")]
        Low = 5,
        [Description("آني")]
        RealTime = 1,
        [Description("عادي")]
        Usual = 4,
        [Description("خيلي فوري")]
        VeryHigh = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCallCenterQuestionState")]
    public enum CallCenterQuestionState
    {
        [Description("پـاسـخ داده شده")]
        Answered = 2,
        [Description("اعلام شده")]
        Created = 1,
        [Description("رسيدگي شده توسط مديريت پشتيباني")]
        DoneWithManager = 5,
        [Description("رسيدگي شده توسط کارشناس نرم افزار")]
        DoneWithSoftwareExpert = 6,
        [Description("رسيدگي شده توسط مديريت نرم افزار")]
        DoneWithSoftwareManager = 8,
        [Description("ارجاع داده شده به مديريت پشتيباني")]
        Sent2Manager = 3,
        [Description("ارجاع داده شده به کارشناس نرم افزار")]
        Sent2SoftwareExpert = 4,
        [Description("ارجاع داده شده به مديريت نرم افزار")]
        Sent2SoftwareManager = 7,
        [Description("")]
        None = 0
    }
    [Description("EnmCApplicantPost")]
    public enum CApplicantPost
    {
        [Description("وکيل")]
        Lawyer = 2,
        [Description("اصيل")]
        Original = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCArrestState")]
    public enum CArrestState
    {
        [Description("بازداشت شده")]
        Arrested = 1,
        [Description("رفع بازداشت شده")]
        RemovalArrested = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCartableActivationMethod")]
    public enum CartableActivationMethod
    {
        [Description("رخدادي")]
        Event = 2,
        [Description("عادي")]
        Normal = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCartableCategory")]
    public enum CartableCategory
    {
        [Description("اطلاعي")]
        Notify = 2,
        [Description("اقدامي")]
        Perform = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCaseMechanizeState")]
    public enum CaseMechanizeState
    {
        [Description("مکانيزه")]
        Mechanize = 1,
        [Description("غير مکانيزه")]
        NonMechanize = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCaseOwner")]
    public enum CaseOwner
    {
        [Description("متعلق به پرونده ديگر")]
        OwnerOtherCase = 2,
        [Description("متعلق به همين پرونده")]
        OwnerThisCase = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCasePersonOtherDataType")]
    public enum CasePersonOtherDataType
    {
        [Description("داراي مقادير پيش فرض")]
        HaveLookup = 1,
        [Description("بدون مقادير پيش فرض")]
        HaveNotLookup = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCaseStatisticState")]
    public enum CaseStatisticState
    {
        [Description("جاري")]
        Active = 1,
        [Description("مختومه")]
        Dismantled = 9,
        [Description("توقف عمليات اجرايي از طرف واحد قضايي")]
        EnmStopByJudge = 7,
        [Description("توقف از طرف بستانکار")]
        StopByCreditor = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmCaseWealthChangeState")]
    public enum CaseWealthChangeState
    {
        [Description("تاييد شده")]
        Confirmed = 1,
        [Description("تاييد نشده")]
        NotConfirmed = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCaseWealthState")]
    public enum CaseWealthState
    {
        [Description("مال بابت طلب به نفع بستانکار ضبط شده")]
        Concede = 5,
        [Description("وجود مال متعلق به بدهکار تاييد شده")]
        Confirm = 2,
        [Description("مال به خريدار تحويل داده شد")]
        Deliver2Buyer = 12,
        [Description("مال به بدهکار تحويل داده شد")]
        Deliver2Debtor = 13,
        [Description("مال به موجر تحويل داده شد")]
        Deliver2Lessor = 17,
        [Description("مال به بستانکار تحويل داده شد")]
        DeliverCreditor = 14,
        [Description("مال بازداشت شده")]
        Detention = 3,
        [Description("مال به فروش رفته به نفع بستانکار")]
        GoingToSell = 4,
        [Description("معرفي شده ولي تاييد نشده")]
        IntroducedNotConfirm = 1,
        [Description("رفع بازداشت با دستور تمام قضايي")]
        ReleaseByCourtOrders = 16,
        [Description("رفع بازداشت به تقاضاي بستانکار")]
        ReleaseByCreditorDemands = 15,
        [Description("رفع بازداشت مال به دليل تسويه حساب بدهکار")]
        ReleasePony = 6,
        [Description("توقف عمليات در مورد مال به حکم دادگاه")]
        StopByJudge = 7,
        [Description("")]
        None = 0
    }
    [Description("EnmCBookStatus")]
    public enum CBookStatus
    {
        [Description("------")]
        Converted = 100,
        [Description("زوج")]
        Even = 12,
        [Description("عادي")]
        Normal = 10,
        [Description("فرد")]
        Odd = 11,
        [Description("")]
        None = 0
    }
    [Description("EnmCBranchChangeType")]
    public enum CBranchChangeType
    {
        [Description("فعال ")]
        Active = 2,
        [Description("انحلال")]
        Breakup = 4,
        [Description("ايجاد")]
        Create = 1,
        [Description("ابطال ")]
        Invalid = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCCmrcialAssertRequestType")]
    public enum CCmrcialAssertRequestType
    {
        [Description("ابطال")]
        Cancel = 4,
        [Description("تغيير")]
        Change = 3,
        [Description("ثبت نام")]
        Registration = 1,
        [Description("تمديد")]
        Respite = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCCommercialAssertionAddressType")]
    public enum CCommercialAssertionAddressType
    {
        [Description("ورود اطلاعات شده توسط متقاضي")]
        DataEnteredByApplicant = 1,
        [Description("واکشي شده از آخرين اعلامي دفتر ثبت تجارتي")]
        FetchFromCommercialRegisterNumber = 4,
        [Description("واکشي شده از اطلاعات شخصيت حقوقي سامانه ثبت شرکتها")]
        FetchFromCompany = 2,
        [Description("واکشي شده از پلمب دفتر تجارتي")]
        FetchFromPolomb = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCCommercialAssertionNoticeType")]
    public enum CCommercialAssertionNoticeType
    {
        [Description("ابلاغيه ثبت نام در دفاتر تجارتي")]
        CommercialIssue = 3,
        [Description("ابلاغيه تاييد اظهارنامه ثبت نام در دفاتر تجارتي")]
        Confirm = 1,
        [Description("ابلاغيه رد اظهارنامه ثبت نام در دفاتر تجارتي")]
        Refuse = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCCommericalType")]
    public enum CCommericalType
    {
        [Description("تجارت داخلي و تجارت خارجي")]
        BothCommerical = 3,
        [Description("تجارت داخلي")]
        InnerCommerical = 1,
        [Description("تجارت خارجي")]
        OutsideCommerical = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCCommissionType")]
    public enum CCommissionType
    {
        [Description("هيئت مديره")]
        BoardOfDirectors = 2,
        [Description("هيئت رئيسه")]
        Praesidium = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCCompanyDataCorrectedFieldType")]
    public enum CCompanyDataCorrectedFieldType
    {
        [Description("تاريخ پايان فعاليت")]
        ActivityEndDate = 15,
        [Description("مدت فعاليت")]
        ActivityTimeState = 14,
        [Description("نشاني")]
        Address = 6,
        [Description("نشاني محل جغرافيايي")]
        AddressLocation = 12,
        [Description("وضعيت بازداشتي")]
        ArrestStatus = 13,
        [Description("تاريخ انحلال")]
        BreakupDate = 10,
        [Description("وضعيت خلاصه برداري")]
        CEArchCompanyState = 17,
        [Description("واحد خلاصه برداري")]
        CEArchiveUnitId = 16,
        [Description("نوع شخصيت حقوقي")]
        CompanyType = 3,
        [Description("تاريخ تأسيس")]
        EstablishmentDate = 9,
        [Description("نام شخصيت حقوقي")]
        name = 4,
        [Description("وضعيت")]
        ObjectState = 1,
        [Description("کد پستي")]
        PostCode = 5,
        [Description("تاريخ ثبت")]
        RegisterDate = 8,
        [Description("شماره ثبت")]
        RegisterNumber = 7,
        [Description("تاريخ ختم تصفيه")]
        SettleDate = 11,
        [Description("واحد ثبتي")]
        Unit = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCCompanyLable")]
    public enum CCompanyLable
    {
        [Description("نام تکراري")]
        DuplicateName = 2,
        [Description("شناسه تکراري")]
        DuplicateNationalCode = 3,
        [Description("شماره ثبت تکراري")]
        DuplicateRegisternumber = 4,
        [Description("عادي")]
        Normal = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCCompanyOwnershipType")]
    public enum CCompanyOwnershipType
    {
        [Description("دولتي")]
        Governmental = 2,
        [Description("خصوصي")]
        Private = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCCompanyPerformanceReportSubject")]
    public enum CCompanyPerformanceReportSubject
    {
        [Description("مورد نياز براي اداره بودجه")]
        NeededForBudgetOffice = 1,
        [Description("مورد نياز براي مديران استانها")]
        NeededForManagers = 2,
        [Description("به تفکيک نوع مورد نياز براي مديران")]
        TypeSeparationForManager = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCCompanyReportType")]
    public enum CCompanyReportType
    {
        [Description("به تفکيک موضوع فعاليت")]
        Activity = 1,
        [Description("به تفکيک موضوع فعاليت و واحدثبتي")]
        ActivityAndUnit = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCCompanyReviewStatus")]
    public enum CCompanyReviewStatus
    {
        [Description("بازبيني نشده")]
        NotReviewd = 2,
        [Description("بازبيني شده")]
        Reviewed = 1,
        [Description("بازبيني مجدد شده")]
        SecondReviewed = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCCompanySecurityStatus")]
    public enum CCompanySecurityStatus
    {
        [Description("عادي")]
        Normal = 1,
        [Description("محرمانه")]
        secretly = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCCompanyType")]
    public enum CCompanyType
    {
        [Description("نمايندگي")]
        Agency = 5,
        [Description("شعبه")]
        Branch = 4,
        [Description("شرکت")]
        Company = 1,
        [Description("تعاوني")]
        Cooperative = 3,
        [Description("موسسه")]
        Institute = 2,
        [Description("ساير")]
        Other = 6,
        [Description("")]
        None = 0
    }
    [Description("EnmCeilingType")]
    public enum CeilingType
    {
        [Description("طاق ضربي")]
        Arched = 2,
        [Description("تيرچه بلوک")]
        BlockJoist = 1,
        [Description("کامپوزيت")]
        Composite = 3,
        [Description("دال بتني")]
        Concrete = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmCertificateType")]
    public enum CertificateType
    {
        [Description("گواهي قطعيت دادنامه")]
        JudgmentFinalCertificate = 2,
        [Description("گواهي ماده 212 قانون آيين دادرسي مدني")]
        Law212Certificate = 3,
        [Description("گواهي اقامه دعوا در دادگاه صالحه")]
        ProceedingsCertificate = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCFinanceCalendarType")]
    public enum CFinanceCalendarType
    {
        [Description("تقويم ميلادي")]
        Gregorian = 2,
        [Description("تقويم شمسي")]
        Jalali = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmChangedFiledDataType")]
    public enum ChangedFiledDataType
    {
        [Description("ExcelFIle")]
        ExcelFile = 5,
        [Description("تصوير")]
        ImageField = 3,
        [Description("فهرست مقادير")]
        Lookup = 8,
        [Description("MHTFile")]
        MHTFile = 6,
        [Description("عددي")]
        Numerical = 1,
        [Description("شيء سيستم")]
        Object = 9,
        [Description("PDFFile")]
        PDFFile = 7,
        [Description("متني ساده")]
        SimpleText = 2,
        [Description("WordText")]
        TextLikeWord = 11,
        [Description("WordFile")]
        WordFile = 4,
        [Description("فايل هاي ديگر")]
        OtherFile = 10,
        [Description("")]
        None = 0
    }
    [Description("EnmCHCompanyChangeType")]
    public enum CHCompanyChangeType
    {
        [Description("ايجاد شده است")]
        Created = 1,
        [Description("حذف شده است")]
        Deleted = 3,
        [Description("تغيير کرده است")]
        Edited = 2,
        [Description("تغيير نکرده است")]
        NotChanged = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmCheckNatinalityStatus")]
    public enum CheckNatinalityStatus
    {
        [Description("اطلاعات با ثبت احوال چک شد و معتبر بود")]
        Confirm = 1,
        [Description("اطلاعات با ثبت احوال راستي آزمايي نشده است")]
        NotChecked = 2,
        [Description("اطلاعات مورد تاييد ثبت احوال نبود")]
        Refuse = 3,
        [Description("منتظر بررسي مدير سيستم")]
        Wait4Check = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmCIPostRequestType")]
    public enum CIPostRequestType
    {
        [Description("تغييرات")]
        Changes = 2,
        [Description("تأسيس")]
        Establishment = 1,
        [Description("تأسيس و تغيير")]
        EstablishmentAndChanges = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCircularCopyGettersType")]
    public enum CircularCopyGettersType
    {
        [Description(" دفاتر ازدواج و طلاق سراسر کشور")]
        AllMarriageScriptoriums = 11,
        [Description("ادارات کل ثبت اسناد و املاک سراسر کشور")]
        AllOstanUnits = 1,
        [Description("دفاتر اسناد رسمي سراسر کشور")]
        AllRasmiScriptoriums = 2,
        [Description("معاونت محترم قضائي دادستاني کل کشور")]
        Dadsetani = 7,
        [Description("مديرکل محترم دفتر نظارت و هماهنگي اجراي اسناد رسمي")]
        Ejra = 12,
        [Description("رياست محترم کانون سردفتران و دفترياران")]
        Kanon = 3,
        [Description("مديرکل محترم اداره کل مالکيت صنعتي")]
        MalekiatSanati = 6,
        [Description("ساير")]
        Others = 10,
        [Description("مديرکل محترم اداره کل ثبت شرکت ها و موسسات غيرتجاري")]
        SabteSherkatha = 4,
        [Description("يک اداره کل ثبت اسناد و املاک")]
        SingleOstanUnit = 8,
        [Description("يک دفترخانه خاص")]
        SingleScriptorium = 9,
        [Description("يک اداره ثبت اسناد و املاک")]
        SingleUnit = 14,
        [Description("ادارات ثبت اسناد و املاک سراسر کشور")]
        TotalUnitsInCountry = 13,
        [Description("ذيحساب و مديرکل محترم امور مالي")]
        Zihesab = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmCircularDocumentType")]
    public enum CircularDocumentType
    {
        [Description("سفيد")]
        White = 2,
        [Description("تحرير شده")]
        Written = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCircularItemType")]
    public enum CircularItemType
    {
        [Description("فاقد سابقه ثبت")]
        DontHaveSSAAReg = 2,
        [Description("مفقودي")]
        Lost = 4,
        [Description("فاقد اصالت")]
        NotOriginal = 1,
        [Description("جعلي")]
        Spurious = 5,
        [Description("مسروقه")]
        Stolen = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCircularPaperType")]
    public enum CircularPaperType
    {
        [Description("تمام برگ")]
        FullPaper = 2,
        [Description("نيم برگ")]
        MiniPaper = 1,
        [Description("برگ رونوشت")]
        PaperCopy = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCircularPrefixOrPostfix")]
    public enum CircularPrefixOrPostfix
    {
        [Description("پسوند")]
        Postfix = 2,
        [Description("پيشوند")]
        Prefix = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCircularState")]
    public enum CircularState
    {
        [Description("بـاطـل شده")]
        Canceled = 2,
        [Description("تـايـيـد شده")]
        Confirmed = 3,
        [Description("تبديل شده")]
        Converted = 5,
        [Description("تـنـظـيـم شده")]
        Created = 1,
        [Description("ارسال شده به کارتابل تاييد")]
        ReadyToConfirm = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmClaimDeclarationType")]
    public enum ClaimDeclarationType
    {
        [Description("بين المللي")]
        International = 1,
        [Description("ملي")]
        National = 2,
        [Description("منطقه اي")]
        Regional = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCLLPStep")]
    public enum CLLPStep
    {
        [Description("مرحله اول")]
        StepOne = 1,
        [Description("مرحله دوم")]
        StepTwo = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPActivityKind")]
    public enum CLPActivityKind
    {
        [Description("توسعه اي")]
        Developmental = 2,
        [Description("بنيادي")]
        Fundamental = 1,
        [Description("کاربردي")]
        Practical = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmClpApplicationTypeKind")]
    public enum ClpApplicationTypeKind
    {
        [Description("ساير نوع اشخاص حقوقي")]
        OtherApplication = 2,
        [Description("سازمان ريشه")]
        RootApplication = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPConveneMeeting")]
    public enum CLPConveneMeeting
    {
        [Description("حضوري")]
        Attendance_in_Person = 1,
        [Description("مجازي")]
        Online = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPCourse")]
    public enum CLPCourse
    {
        [Description("ابتدايي")]
        Elementary = 1,
        [Description("متوسطه اول")]
        FirstHighSchool = 2,
        [Description("پيش دبستاني")]
        Preschool = 4,
        [Description("متوسطه دوم")]
        SecondaryHighSchool = 3,
        [Description("فني و حرفه اي")]
        TechnicalAndProfessional = 5,
        [Description("کاردانش")]
        WorkAndKnowledge = 6,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPEndowmentCertificateType")]
    public enum CLPEndowmentCertificateType
    {
        [Description("وقف نامه")]
        EndowmentLetter = 1,
        [Description("نظريه تحقيق")]
        ResearchTheory = 2,
        [Description("نامه زمين شهري")]
        UrbanLandLetter = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPEnduranceDocType")]
    public enum CLPEnduranceDocType
    {
        [Description("وصيت حبس نامه")]
        BequestLetter = 24,
        [Description("گواهي واگذاري زمين شرعي")]
        CertificateTransferReligiousLand = 23,
        [Description("فرمان")]
        Command = 19,
        [Description("اقرارنامه")]
        Confession = 7,
        [Description("راي دادگاه")]
        CourtVerdict = 14,
        [Description("مبايعه وقف نامه")]
        EndowmentSale = 28,
        [Description("شجره نامه")]
        FamilyTree = 3,
        [Description("هبه نامه")]
        Hebe = 11,
        [Description("حبس نامه")]
        LetterArrest = 12,
        [Description("مصالحه نامه")]
        LetterCompromise = 22,
        [Description("استشهادنامه محلي")]
        LocalTestimony = 6,
        [Description("نذرنامه")]
        Nazr = 16,
        [Description("اخبارنامه")]
        Newsletter = 4,
        [Description("وقف نامه رسمي")]
        OfficialEndowmentLetter = 1,
        [Description("وقف نامه عادي")]
        OrdinaryEndowmentLetter = 2,
        [Description("سند مالکيت")]
        OwnershipDocument = 15,
        [Description("عريضه و حکم شرعي")]
        PetitionShariRuling = 18,
        [Description("سوابق عمل به وقف")]
        RecordsPracticeEndowment = 17,
        [Description("تجديد وقفنامه")]
        RenewalEndowment = 8,
        [Description("نظريه تحقيق")]
        ResearchTheory = 25,
        [Description("حکم شرعي")]
        ShariaRuling = 13,
        [Description("اساس نامه")]
        Statute = 5,
        [Description("تقسيم نامه")]
        TaksimLetter = 9,
        [Description("مبايعه نامه")]
        Testament = 20,
        [Description("وصيت اقرارنامه")]
        TestamentaryWill = 27,
        [Description("توليت نامچه")]
        TolitNamcheh = 10,
        [Description("وصيت نامه")]
        Vasiat = 26,
        [Description("وصيت نذرنامه")]
        WillTestament = 21,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPGender")]
    public enum CLPGender
    {
        [Description("پسرانه")]
        Boyish = 1,
        [Description("مختلط")]
        Coeducational = 3,
        [Description("دخترانه")]
        Girly = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmClpIApplicationTopType")]
    public enum ClpIApplicationTopType
    {
        [Description("ثبت شرکتها و موسسات غير تجاري")]
        CompanyReg = 6,
        [Description("قوه مجريه")]
        Executive = 1,
        [Description("قوه قضائيه")]
        Judeiary = 3,
        [Description("قوه مقننه")]
        LegisLatuer = 2,
        [Description("موسسه يا نهادهاي عمومي غيردولتي")]
        PublicNonGovern = 5,
        [Description("دفتر مقام معظم رهبري")]
        SupremeLeader = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPKindOfLegalManage")]
    public enum CLPKindOfLegalManage
    {
        [Description("متمرکز")]
        Concentrated = 1,
        [Description("هيات موسس")]
        FounderBoard = 5,
        [Description("هيات مديره")]
        ManagerBoard = 3,
        [Description("هيات امنا")]
        OmanaBoard = 2,
        [Description("شورا عالي")]
        PerfectCouncil = 4,
        [Description("مجمع عمومي")]
        PublicAssembly = 6,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPMainFieldActivity")]
    public enum CLPMainFieldActivity
    {
        [Description("کشاورزي")]
        Agriculture = 3,
        [Description("علوم کاربردي")]
        AppliedSciences = 2,
        [Description("هنرومعماري")]
        ArtandArchitecture = 5,
        [Description("فني مهندسي")]
        Engineering = 4,
        [Description("علوم انساني")]
        Humanities = 1,
        [Description("بين رشته اي")]
        Interdisciplinary = 6,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPManageType")]
    public enum CLPManageType
    {
        [Description("هيئت امنايي")]
        BoardTrustees = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPMeeting")]
    public enum CLPMeeting
    {
        [Description("هيأت")]
        Board = 2,
        [Description("کارگروه کارشناسي")]
        ExpertGroupWork = 1,
        [Description("برون سازماني")]
        External_Organization = 4,
        [Description("درون سازماني")]
        Internal_Organization = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPMeetingPersonState")]
    public enum CLPMeetingPersonState
    {
        [Description("غايب")]
        Absent = 2,
        [Description("مدعو")]
        Invited = 3,
        [Description("حاضر")]
        Present = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPMeetingType")]
    public enum CLPMeetingType
    {
        [Description(" فوق العاده")]
        Amazing = 1,
        [Description("عادي")]
        Usual = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPReviewRequestRegisterUser")]
    public enum CLPReviewRequestRegisterUser
    {
        [Description("از سامانه شناسه ملي (فرم)")]
        FromILENCPortal = 3,
        [Description("از ثبت من")]
        FromMySSAA = 2,
        [Description("از سرويس")]
        FromWebService = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPUnitKind")]
    public enum CLPUnitKind
    {
        [Description("دولتي")]
        Governmental = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCLPUserState")]
    public enum CLPUserState
    {
        [Description("تاييد شده و فعال")]
        Active = 2,
        [Description("غير فعال")]
        InActive = 3,
        [Description("تعريف شده و تاييد نشده")]
        NotConfirm = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCmEmailState")]
    public enum CmEmailState
    {
        [Description("ارسال ايميل ناموفق")]
        EmailFailed = 3,
        [Description("در صف")]
        EmailInQueue = 1,
        [Description("ارسال ايميل موفق")]
        EmailSent = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCMemberType")]
    public enum CMemberType
    {
        [Description("رکن اداره کننده")]
        DirectorMember = 2,
        [Description("رکن اداره کننده/تصميم گيرنده")]
        FounderDirectorMember = 3,
        [Description("رکن تصميم گيرنده")]
        FounderMember = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCmFeedbackState")]
    public enum CmFeedbackState
    {
        [Description("منقضي شده")]
        Expired = 4,
        [Description("داراي پاسخ")]
        Replied = 3,
        [Description("منتظر پاسخ")]
        WaitForReply = 2,
        [Description("منتظر ارسال")]
        WaitToSend = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCmHeadNumberType")]
    public enum CmHeadNumberType
    {
        [Description("رايگان")]
        Free = 1,
        [Description("ارزش افزوده")]
        Vas = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCMinutesDefectState")]
    public enum CMinutesDefectState
    {
        [Description("اصلاح شده")]
        Corrected = 3,
        [Description("ابلاغ شده")]
        Notified = 2,
        [Description("ابلاغ نشده")]
        UnNotified = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCMinutesPersonDataState")]
    public enum CMinutesPersonDataState
    {
        [Description("موجود است")]
        Exsits = 1,
        [Description("موجود نيست")]
        NonExsits = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCMinutesPersonType")]
    public enum CMinutesPersonType
    {
        [Description("نماينده سهام دار")]
        AgentShareHolder = 7,
        [Description("وکيل سهام دار")]
        LawyerShareHolder = 8,
        [Description("رئيس جلسه")]
        MinutesBoss = 1,
        [Description("ناظر رئيس جلسه")]
        MinutesBossSupervisor = 4,
        [Description("منشي جلسه")]
        MinutesSecretary = 3,
        [Description("ناظر جلسه")]
        MinutesSupervisor = 2,
        [Description("ساير")]
        Other = 99,
        [Description("دارنده سهم الشرکه")]
        PartnershipShare = 9,
        [Description("سهام دار")]
        ShareHolder = 6,
        [Description("")]
        None = 0
    }
    [Description("EnmCminutesReportFieldType")]
    public enum CminutesReportFieldType
    {
        [Description("زمان ارسال براي صدور آگهي")]
        AdvertisingIssuedDateTime = 12,
        [Description("زمان اعمال تغييرات")]
        AdvertismentCommitChangesDateTime = 21,
        [Description("زمان تأييد آگهي توسط مدير")]
        AdvertismentConfirmByManagerDateTime = 17,
        [Description("زمان تأييد آگهي توسط ذي سمت")]
        AdvertismentConfirmByOfficialDateTime = 18,
        [Description("زمان ارسال آگهي به روزنامه رسمي")]
        AdvertismentSendToLegalNewspaperDateTime = 20,
        [Description("زمان درج آگهي در دفتر")]
        AdvertismentSendToRegisterBookDateTime = 19,
        [Description("زمان خاتمه داده آمايي")]
        DataPreparationDateTime = 6,
        [Description("زمان تأييد نواقص")]
        DefectConfirmDateTime = 13,
        [Description("زمان ابلاغ نواقص")]
        DefectNotifiedDateTime = 15,
        [Description("زمان ارسال مدارک")]
        DocSendDateTime = 3,
        [Description("زمان بررسي نام هاي درخواستي")]
        NameDeermineDateTime = 8,
        [Description("زمان پذيرش نهايي")]
        PermanentRegisterDateTime = 2,
        [Description("زمان ارجاع به داداه آما")]
        ReferToDataPreparDateTime = 5,
        [Description("زمان ارجاع به کارشناس")]
        ReferToExpertDateTime = 9,
        [Description("زمان ثبت نواقص")]
        RegisterDefectDateTime = 10,
        [Description("زمان ثبت در ثبت شرکت ها")]
        RegisterInCompanyOfficeDateTime = 4,
        [Description("زمان ثبت رد")]
        RegisterRejectDateTime = 11,
        [Description("زمان تأييد رد")]
        RejectconfirmDateTime = 14,
        [Description("زمان ابلاغ دلايل رد")]
        RejectnotifiedDateTime = 16,
        [Description("زمان ارسال به واحد تعيين نام")]
        SendToNameDetermineDateTime = 7,
        [Description("زمان پذيرش موقت ")]
        TemporaryRegisterDateTime = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCMinuteState")]
    public enum CMinuteState
    {
        [Description("تمامي سهامدران/شرکاء")]
        AllPersons = 2,
        [Description("اکثريت سهامدران/شرکاء")]
        Majority = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCmMessageState")]
    public enum CmMessageState
    {
        [Description("در صف ارسال")]
        InQueue = 1,
        [Description("ارسال ناموفق پس از چند تلاش")]
        SendFailed = 2,
        [Description("ارسال موفق - تحويل شده")]
        SentDelivered = 4,
        [Description("ارسال موفق - تحويل نامشخص")]
        SentDeliveryUnknown = 3,
        [Description("ارسال موفق - تحويل نشده")]
        SentNotDelivered = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmCMSEMailStatus")]
    public enum CMSEMailStatus
    {
        [Description("عدم توانايي براي رساندن به مقصد")]
        CanNotReceive = 5,
        [Description("ارسال نشده")]
        NotSended = 1,
        [Description("به مقصد رسيده")]
        Receive = 3,
        [Description("ارسال شده و منتظر اولين پاسخ")]
        Sended = 6, // remove warning 		
        [Description("منتظر مانده")]
        Wait = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmCmSendType")]
    public enum CmSendType
    {
        [Description("ايميل")]
        Email = 2,
        [Description("پيام")]
        Message = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCMSNoObjectOrNoticeSheet")]
    public enum CMSNoObjectOrNoticeSheet
    {
        [Description("مدركي بجز ابلاغنامه")]
        CMSNoObject = 1,
        [Description("ابلاغنامه")]
        NoticeSheet = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCMSSMSStatus")]
    public enum CMSSMSStatus
    {
        [Description("عدم توانايي براي رساندن به مقصد")]
        CanNotReceive = 5,
        [Description("ارسال نشده")]
        NotSended = 1,
        [Description("به مقصد رسيده")]
        Receive = 3,
        [Description("ارسال شده و منتظر اولين پاسخ")]
        Sended = 2,
        [Description("منتظر مانده")]
        Wait = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmCNameDetermineReportFieldType")]
    public enum CNameDetermineReportFieldType
    {
        [Description("زمان تأييد نام در خواستي")]
        ConfirmRequestNameDateTime = 2,
        [Description("زمان ارجاع به کارشناس  ")]
        ReferToExpertDateTime = 1,
        [Description("زمان رد نام هاي درخواستي")]
        RejectRequestNamesDateTime = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCNameNotice")]
    public enum CNameNotice
    {
        [Description("تاييد درخواست و تعيين نام")]
        Confirm = 1,
        [Description("رد درخواست")]
        Refuse = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCNameProcessingState")]
    public enum CNameProcessingState
    {
        [Description("درحال پردازش")]
        BeingProcessed = 1,
        [Description("پردازش شده")]
        Processed = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCNationalityType")]
    public enum CNationalityType
    {
        [Description("غيرايراني")]
        Foreign = 2,
        [Description("ايراني")]
        Iranian = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCommandType")]
    public enum CommandType
    {
        [Description("حذف")]
        Delete = 3,
        [Description("درج")]
        Insert = 1,
        [Description("حذف در ساير نرم افزارها")]
        OtherSoftwareDelete = 6,
        [Description("درج در ساير نرم افزارها")]
        OtherSoftwareInsert = 4,
        [Description("به روزرساني در ساير نرم افزارها")]
        OtherSoftwareUpdate = 5,
        [Description("به روز رساني")]
        Update = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCompanyArrestStatus")]
    public enum CompanyArrestStatus
    {
        [Description("بازداشتي دارد")]
        CompanyArrested = 2,
        [Description("سهام شخص يا اشخاصي از شرکت بازداشت است")]
        CompanyPersonStocksArrested = 3,
        [Description("بازداشتي ندارد")]
        NoArrested = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCompanyAttachmentType")]
    public enum CompanyAttachmentType
    {
        [Description("تصوير آگهي")]
        CAdvertisingImage = 3,
        [Description("تصوير نامه وارده")]
        CIncomingLetterImage = 4,
        [Description("تصوير صورتجلسه")]
        CMinuteImage = 1,
        [Description("تصوير نام")]
        CNameImage = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCompanyDocumentType")]
    public enum CompanyDocumentType
    {
        [Description("آگهي")]
        CAdvertisment = 3,
        [Description("بازداشتي")]
        CArrestBook = 4,
        [Description("رفع بازداشتي")]
        CArrestRemoval = 5,
        [Description("نامه وارده")]
        CIncomingLetter = 1,
        [Description("صورتجلسه")]
        CMinutes = 6,
        [Description("نامه صادره")]
        COutpoingLetter = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCompanyPersonArrestStatus")]
    public enum CompanyPersonArrestStatus
    {
        [Description("بازداشتي ندارد")]
        NoArrested = 1,
        [Description("سهمي از شخص بازداشت است")]
        PersonStockArrested = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCompanyState")]
    public enum CompanyState
    {
        [Description("فعال")]
        Active = 1,
        [Description("منحل شده")]
        BreakedUp = 2,
        [Description("ختم تصفيه")]
        Setteled = 3,
        [Description("منتقل شده به مرجع ديگر")]
        Transfered = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmCompanyTypeInRegisterationCost")]
    public enum CompanyTypeInRegisterationCost
    {
        [Description("انواع شرکت هاي تجاري و شعب خارجي")]
        CompanyAndForeignBranch = 1,
        [Description("شرکت هاي تعاوني")]
        Cooperative = 3,
        [Description("موسسات غير تجاري")]
        Institution = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCompetentAuthority")]
    public enum CompetentAuthority
    {
        [Description("قضايي مكانيزه")]
        Justic = 1,
        [Description("غيرقضايي ")]
        NonJustic = 2,
        [Description("قضايي غير مكانيزه")]
        NonMechanizeJustic = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmComplaintResponseType")]
    public enum ComplaintResponseType
    {
        [Description("پست الکترونيک")]
        Email = 1,
        [Description("پست")]
        Post = 3,
        [Description("پيامک")]
        SMS = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmConfirmerNoticeState")]
    public enum ConfirmerNoticeState
    {
        [Description("صحت عمليات ابلاغ تاييد شده است")]
        Confirm = 1,
        [Description("هنوز نظري داده نشده است")]
        NoOpinion = 3,
        [Description("صحت عمليات ابلاغ تاييد نشده است")]
        Refuse = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmConvictionState")]
    public enum ConvictionState
    {
        [Description("در حال انجام")]
        Active = 1,
        [Description("خاتمه يافته")]
        Passive = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCooling")]
    public enum Cooling
    {
        [Description("چيلر فن کوئل")]
        Chiller = 3,
        [Description("کولر گازي")]
        GasCooler = 2,
        [Description("ندار د")]
        HaveNot = 6,
        [Description("ساير")]
        Others = 4,
        [Description("وي آر اف")]
        VRF = 5,
        [Description("کولر آبي")]
        WaterCooler = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCourtType")]
    public enum CourtType
    {
        [Description("دادگاه تجديد نظر")]
        AmendmentCourt = 8,
        [Description("دادگاه عمومي بخش")]
        BakhshCourt = 13,
        [Description("بازپرسي")]
        Bazpors = 3,
        [Description("دادگاه كيفري")]
        CriminalCourt = 6,
        [Description("واحد اظهار نظر")]
        CriminalRemark = 2,
        [Description("دادستاني")]
        Dadsetan = 4,
        [Description("دادياري تحقيق")]
        Dadyar = 7,
        [Description("دادياري سرپرستي")]
        DadyariSarparsti = 12,
        [Description("شوراي حل اختلاف")]
        DiscordCouncilResolve = 10,
        [Description("اجراي احکام شورا")]
        ExecuteDiscordCouncilResolve = 15,
        [Description("واحد اجراي احكام مدني")]
        ExecutorCivil = 11,
        [Description("واحد اجراي احكام كيفري")]
        ExecutorCriminal = 1,
        [Description("کشيک دادگاه")]
        KeskikDadgah = 17,
        [Description("کشيک دادسرا")]
        KeskikDadsara = 16,
        [Description("دادگاه حقوقي")]
        LegalCourt = 5,
        [Description("دادگاه كيفري استان")]
        OstanCriminal = 14,
        [Description("شعبه ديوانعالي كشور")]
        SupremeCourt = 9,
        [Description("")]
        None = 0
    }
    [Description("EnmCOutsideCommercialType")]
    public enum COutsideCommercialType
    {
        [Description("صادرات")]
        Export = 1,
        [Description("واردات")]
        Import = 2,
        [Description("حق العمل کاري")]
        ServiceBase = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCOwnershipType")]
    public enum COwnershipType
    {
        [Description("تعاوني")]
        Cooperative = 2,
        [Description("تحت پوشش")]
        Covered = 4,
        [Description("دولتي")]
        Governmental = 3,
        [Description("نهاد")]
        Institution = 5,
        [Description("مختلط غير سهامي")]
        LimitedPartnership = 7,
        [Description("ملي شده")]
        Nationalized = 6,
        [Description("ساير")]
        Others = 8,
        [Description("خصوصي")]
        Private = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCPersonRelationDocType")]
    public enum CPersonRelationDocType
    {
        [Description("قيم نامه")]
        LettersAdministration = 2,
        [Description("وکالتنامه")]
        PowerAttorney = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCPolompBookType")]
    public enum CPolompBookType
    {
        [Description("دفاتر معمولي")]
        NormalBook = 1,
        [Description("دفاتر خاص")]
        SpecialBook = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCPolompFinancialYear")]
    public enum CPolompFinancialYear
    {
        [Description("جاري")]
        CurrentYear = 1,
        [Description("آينده")]
        NextYear = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCPolompNoticeType")]
    public enum CPolompNoticeType
    {
        [Description("ابلاغيه صدور دفاتر تجارتي پلمب شده")]
        PolompBook = 3,
        [Description("ابلاغيه تاييد اظهارنامه پلمب دفاتر تجارتي")]
        PolompConfirm = 1,
        [Description("ابلاغيه رد اظهارنامه پلمب دفاتر تجارتي")]
        PolompRefuse = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCRegisterBookCompType")]
    public enum CRegisterBookCompType
    {
        [Description("شرکت")]
        Company = 1,
        [Description("شعبه و نمايندگي شرکت خارجي")]
        Foreign = 3,
        [Description("موسسه")]
        Institution = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCRegisterBookType")]
    public enum CRegisterBookType
    {
        [Description("متمم")]
        Complement = 4,
        [Description("زوج")]
        Even = 2,
        [Description("عادي جديد")]
        NewNormal = 8,
        [Description("عادي")]
        Normal = 1,
        [Description("فرد")]
        Odd = 3,
        [Description("فرد قديمي")]
        OIdOdd = 7,
        [Description("زوج قديمي")]
        OldEven = 6,
        [Description("مکرر")]
        Repetitious = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmCRegisterNumberAllocateType")]
    public enum CRegisterNumberAllocateType
    {
        [Description("صدور شماره ثبت جديد در محدوده واحد جاري")]
        IssuingNewRegisterNumber = 2,
        [Description("استفاده از شماره ثبت قبلي موجود در واحد جاري")]
        UseExistRegisterNumber = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCRegisternumberRangeType")]
    public enum CRegisternumberRangeType
    {
        [Description(" شرکت ")]
        Company = 1,
        [Description("نمايندگي شرکت خارجي")]
        ForeignBranch = 3,
        [Description("موسسه/صندوق")]
        Institution = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCRelatedDocOnLetter")]
    public enum CRelatedDocOnLetter
    {
        [Description("شرکت/موسسه")]
        Company = 1,
        [Description("درخواست ثبت تاسيس اينترنتي")]
        InternetRequest = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCReportBaseOn")]
    public enum CReportBaseOn
    {
        [Description("داراي نفص ")]
        Deficient = 4,
        [Description(" وارده")]
        Incoming = 1,
        [Description(" ثبت شده")]
        Registered = 2,
        [Description(" رد شده")]
        Rejectted = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCReportSubject")]
    public enum CReportSubject
    {
        [Description("تعداد شرکت هاي داراي شعبه")]
        CCompanyHavingBranch = 7,
        [Description("تعدا تصميمات صورتجلسه ها")]
        CDecisionCount = 3,
        [Description("تعداد صورتجلسات")]
        CMinutCount = 1,
        [Description("تعدا شعبه هاي ايجاد شده")]
        CreatedbranchesCount = 6,
        [Description("تعدا درخواست هاي ثبت تأسيس")]
        CTerRegRequestCount = 2,
        [Description("تعداد انتقال هاي وارد شده")]
        IncommingTransferCount = 4,
        [Description("تعداد انتقال هاي خارج شده")]
        OutgoingTransfer = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmCReportType")]
    public enum CReportType
    {
        [Description("گزارش صورتجلسه به تفکيک وضعيت")]
        CMinutesByState = 4,
        [Description("گزارش صورتجلسه اينترنتي به تفکيک وضعيت")]
        CterMinutesByState = 5,
        [Description("گزارش ثبت تأسيس به تفکيک وضعيت")]
        CTerRegRequestByState = 6,
        [Description("مدارک بر اساس کاربر")]
        DocByUser = 1,
        [Description("صورتجلسه بر اساس کاربر و وضعيت")]
        DocByUserAndState = 2,
        [Description("ثبت تأسيس بر اساس کاربر و وضعيت")]
        RegRequestByUserAndState = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCReportUnitOrExpertStatistics")]
    public enum CReportUnitOrExpertStatistics
    {
        [Description("به ترتيب نام شرکت")]
        CompanyName = 3,
        [Description("به ترتيب زمان ثبت مدرک")]
        DocCreateDateTime = 6,
        [Description("به ترتيب شناسه ملي")]
        NationalCode = 1,
        [Description("به ترتيب شماره ثبت")]
        RegisterNumber = 2,
        [Description("به ترتيب کد سطح واحد سازماني")]
        UnitLevelCode = 5,
        [Description("به ترتيب عنوان واحد سازماني")]
        UnitName = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmCReqAdverState")]
    public enum CReqAdverState
    {
        [Description("تاييد شده")]
        Confirm = 1,
        [Description("رد شده")]
        Refuse = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCRequestType")]
    public enum CRequestType
    {
        [Description("درخواست ثبت تغييرات")]
        ChangesMinute = 2,
        [Description("درخواست ثبت تأسيس")]
        RegRequest = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCRestrictionType")]
    public enum CRestrictionType
    {
        [Description("هشدار")]
        Alarm = 1,
        [Description("توقف عمليات")]
        Stop = 2,
        [Description("توقف عمليات به همراه ابلاغ")]
        StopWithNotice = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCSearchType")]
    public enum CSearchType
    {
        [Description("و.")]
        And = 3,
        [Description("مساوي")]
        Equal = 1,
        [Description("يا")]
        Or = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCSignBookLawyerState")]
    public enum CSignBookLawyerState
    {
        [Description("با حق توکيل")]
        WithEmpower = 2,
        [Description("با حق توکيل ولو کرارا")]
        WithEmpowerRepetitive = 3,
        [Description("بدون حق توکيل")]
        WithoutEmpower = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCSignBookPostType")]
    public enum CSignBookPostType
    {
        [Description("نماينده شرکت دولتي")]
        Agent = 6,
        [Description("وکيل رسمي")]
        Lawyer = 4,
        [Description("احدي از موسسين")]
        OneFounder = 7,
        [Description("احدي از مديران")]
        OneManager = 1,
        [Description("احدي از اعضاء")]
        OneMember = 5,
        [Description("احدي از شرکاء")]
        OneShare = 2,
        [Description("احدي از سهامداران")]
        OneStocks = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCSignStatus")]
    public enum CSignStatus
    {
        [Description("اوراق عادي و اداري")]
        AdministrativePapers = 6,
        [Description("هم اوراق و اسناد بهادار و تعهدآور هم اوراق عادي و اداري")]
        BothofThem = 7,
        [Description("حق امضاء ندارد")]
        NoneOfThem = 4,
        [Description("اوراق بهادار و تعهد آور")]
        Securities = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmCSuspendReason")]
    public enum CSuspendReason
    {
        [Description("انصراف متقاضي")]
        ApplicantCancel = 1,
        [Description("عدم رفع نقص")]
        DefectNotCleared = 3,
        [Description("عدم مراجعه در وقت مقرر")]
        TimeOut = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCTerChangeType")]
    public enum CTerChangeType
    {
        [Description("جديد")]
        Added = 1,
        [Description("حذف شده")]
        Deleted = 3,
        [Description("ويرايش شده")]
        Edited = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmCTerMinuteBy")]
    public enum CTerMinuteBy
    {
        [Description("به تفکيک ورودي/خروجي/در حال پردازش بر اساس تاريخ فعاليت")]
        PartGen = 1,
        [Description("به تفکيک وضعيت بر اساس تاريخ فعاليت")]
        State = 2,
        [Description("به تفکيک وضعيت بر اساس تاريخ ورود")]
        StateInputDate = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmCTerMinutesReportFieldType")]
    public enum CTerMinutesReportFieldType
    {
        [Description("زمان پذيرش نهايي توسط متقاضي")]
        PermanentRegisterDateTime = 2,
        [Description("زمان ارسال مدارک پستي")]
        PostDocSendDateTime = 3,
        [Description("زمان ثبت در مرجع ثبت شرکت ها ")]
        RegisterInOfficeDateTime = 5,
        [Description("زمان رد توسط دبير خانه")]
        RejectDatetime = 4,
        [Description("زمان پذيرش موقت توسط متقاضي")]
        TemporaryRegisterDateTime = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmCterRegRequestReportFieldType")]
    public enum CterRegRequestReportFieldType
    {
        [Description("زمان آعمال تغييرات")]
        AdvertismentCommitChangesDateTime = 20,
        [Description("زمان تأييد آگهي توسط مدير")]
        AdvertismentConfirmByManagerDateTime = 16,
        [Description("زمان تأييد آگهي توسط ذي سمت")]
        AdvertismentConfirmByOfficialDateTime = 17,
        [Description("زمان ارسال براي صدور آگهي")]
        AdvertismentIssuedDateTime = 15,
        [Description("زمان ارسال آگهي به روزنامه رسمي")]
        AdvertismentSendToLegalNewspaperDateTime = 19,
        [Description("زمان درج آگهي در دفتر")]
        AdvertismentSendToRegisterBookDateTime = 18,
        [Description("زمان رفع بازداشت مالياتي")]
        CTaxDebtorRemoveDateTime = 8,
        [Description("زمان تأييد نواقص توسط مدير")]
        DefectConfirmDateTime = 13,
        [Description("زمان ثبت نواقص ")]
        DefectsRegisterDateTime = 11,
        [Description("زمان تأييد نام درخواستي")]
        NameDeterminedDateTime = 5,
        [Description("زمان رد نام هاي درخواستي")]
        NameRejectDateTime = 6,
        [Description("زمان پذيرش نهايي")]
        PermanentRegisterDateTime = 2,
        [Description("زمان ارسال مدارک پستي")]
        PostDocSendDateTime = 9,
        [Description("زمان ارجاع به کارشناس    ")]
        ReferToExpertDateTime = 10,
        [Description("زمان تأييد دلايل رد توسط مدير")]
        RejecConfirmtDatetime = 14,
        [Description("زمان ثبت دلايل رد ")]
        RejectRegisterDateTime = 12,
        [Description("زمان رد درخواست توسط دبير خانه")]
        RejectRequestDateTime = 3,
        [Description("زمان ارسال نام هاي جدي")]
        SendNewNameDateTime = 7,
        [Description("زمان ارسال به واحد تعيين نام")]
        SendToNameDetermine = 4,
        [Description("زمان پذيرش موقت")]
        TemporaryRegisterDateTime = 1,
        [Description("زمان تاييد نهايي درخواست سهامي عام مرحله دوم")]
        PermanentRegisterDateTimeLLPStep2 = 21,
        [Description("")]
        None = 0
    }
    [Description("EnmCVerdictIssuer")]
    public enum CVerdictIssuer
    {
        [Description("شوراي عالي ثبت")]
        Council = 3,
        [Description("رييس ثبت محل")]
        LocalBoss = 1,
        [Description("هيات نظارت استان")]
        ProvinceSupervisoryBoard = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmDataNotSendState")]
    public enum DataNotSendState
    {
        [Description("به خطا برخورد کرده")]
        Error = 2,
        [Description("ارسال نشده")]
        NotSend = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmDatapreparationStatus")]
    public enum DatapreparationStatus
    {
        [Description("داده آمايي شده")]
        Dataprepared = 1,
        [Description("نيمه داده آمايي شده")]
        HalfDataPrepared = 3,
        [Description("داده آمايي نشده")]
        UnDataprepared = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmDayNight")]
    public enum DayNight
    {
        [Description("روز")]
        Day = 1,
        [Description("شب")]
        Night = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmDeclarationClaimType")]
    public enum DeclarationClaimType
    {
        [Description("تکميلي")]
        Completed = 3,
        [Description("تقسيمي")]
        Division = 2,
        [Description("حق تقدم")]
        FirstOneClaim = 1,
        [Description("ادغامي")]
        Merge = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmDeclarationPersonRoletype")]
    public enum DeclarationPersonRoletype
    {
        [Description("نماينده قانوني")]
        Agent = 3,
        [Description("مالک يا متقاضي")]
        Applicant = 1,
        [Description("تاييد کننده گواهينامه")]
        ConfirmerLicPerson = 6,
        [Description("طراح  ")]
        Designer = 2,
        [Description("مخترع")]
        Inventor = 5,
        [Description("تحويل گيرنده ابلاغ در ايران")]
        IranNotified = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmDeclarationSoftwarePatentType")]
    public enum DeclarationSoftwarePatentType
    {
        [Description("نرم افزار کاربردي")]
        ApplicationSoftware = 1,
        [Description("نرم افزار خدماتي")]
        ServiceSoftware = 2,
        [Description("نرم افزار سيستمي")]
        SystemSoftware = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmDeclarationState")]
    public enum DeclarationState
    {
        [Description("ثبت نهايي شده")]
        AcceptToDeclaration = 3,
        [Description("تبديل شده به پرونده")]
        ConvertToCase = 4,
        [Description("پيش نويس")]
        CreatedViaInternet = 1,
        [Description("بررسي اوليه شده")]
        FormalChecked = 2,
        [Description("ارسال براي بررسي اوليه")]
        ReadyToFormalCheck = 6,
        [Description("عدم امکان پذيرش و رد")]
        Rejected = 5,
        [Description("در حال بررسي توسط ساير مالکين")]
        VerifyingByOtherOwners = 7,
        [Description("")]
        None = 0
    }
    [Description("EnmDeclarationType")]
    public enum DeclarationType
    {
        [Description("علامت تجاري")]
        BusinessSign = 2,
        [Description("نشان جغرافيايي")]
        GeographicalSign = 4,
        [Description("طرح صنعتي")]
        IndustrialPlan = 3,
        [Description("اختراع")]
        Invention = 1,
        [Description("مادريد")]
        Madrid = 5,
        [Description("بين المللي اختراعات")]
        PCT = 6,
        [Description("")]
        None = 0
    }
    [Description("EnmDependentLPType")]
    public enum DependentLPType
    {
        [Description("دفتر نمايندگي")]
        Agency = 3,
        [Description("شعبه  ")]
        Branch = 1,
        [Description("حوزه")]
        Domain = 4,
        [Description("ساختار تشکيلاتي تابعه")]
        FollowerStructure = 5,
        [Description("مستقل")]
        Independent = 8,
        [Description("واحد مستقل")]
        IndependentUnit = 6,
        [Description("شعبه داخلي")]
        InternalBranch = 2,
        [Description("واحد عملياتي")]
        OperationalUnit = 7,
        [Description("تحت توليت")]
        Toliat = 9,
        [Description("")]
        None = 0
    }
    [Description("EnmDeploymentState")]
    public enum DeploymentState
    {
        [Description("درون سازماني")]
        InsideOrganization = 1,
        [Description("برون سازماني")]
        OutsideOrganization = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmDepositOrWithdrawal")]
    public enum DepositOrWithdrawal
    {
        [Description("واريز به حساب")]
        Deposit = 1,
        [Description("برداشت از حساب")]
        Withdrawal = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmDetailValueBugetStateProcess")]
    public enum DetailValueBugetStateProcess
    {
        [Description(" ستاد/بوجه")]
        StaffBudget = 2,
        [Description("ستاد/معاونت/بودجه")]
        StaffDeputyBudget = 4,
        [Description("استان/بودجه")]
        StateBuget = 5,
        [Description(" استان/ستاد /بودجه")]
        StateStaffBudget = 1,
        [Description(" استان/ستاد/معاونت/بودجه")]
        StateStaffDeputyBudget = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmDirection")]
    public enum Direction
    {
        [Description("شرق")]
        East = 3,
        [Description("شمال")]
        North = 1,
        [Description("جنوب")]
        South = 2,
        [Description("غرب")]
        West = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmDivorceBaenType")]
    public enum DivorceBaenType
    {
        [Description("غيرمدخوله")]
        GheyreMadkholeh = 1,
        [Description("خلع")]
        Kholea = 2,
        [Description("مبارات")]
        Mobarat = 3,
        [Description("ساير")]
        Sayer = 5,
        [Description("يائسه")]
        Yaese = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmDivorceRequester")]
    public enum DivorceRequester
    {
        [Description("توافقي")]
        ByAgree = 3,
        [Description("به درخواست زوج")]
        ByHusband = 1,
        [Description("به درخواست زوجه")]
        ByWife = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmDivorceType")]
    public enum DivorceType
    {
        [Description("بائن")]
        Baen = 2,
        [Description("رجعي")]
        Rojee = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmDocPostType")]
    public enum DocPostType
    {
        [Description("مدارک اصلاحي پذيرش")]
        CorrectiveDoc = 2,
        [Description("مدارک اوليه پذيرش")]
        PrimaryDoc = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmDocReqState")]
    public enum DocReqState
    {
        [Description("ابطـال شده")]
        Canceled = 6,
        [Description("ثبت اوليه")]
        PrimaryRegistry = 1,
        [Description("برگشت داده شده از طرف اداره املاک")]
        Rejected = 9,
        [Description("ارسال شـده")]
        Sent = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmDocumentType")]
    public enum DocumentType
    {
        [Description("مكانيزه")]
        Mechanize = 1,
        [Description("پويشي")]
        Scan = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmDrawing")]
    public enum Drawing
    {
        [Description("انجام شود")]
        Do = 1,
        [Description("انجام نــشود")]
        DoNot = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEArchiveDocumentDeliveringParty")]
    public enum EArchiveDocumentDeliveringParty
    {
        [Description("تحويل به شرکت")]
        RealEstateRegistrationOffice = 1,
        [Description("بازگشت از شرکت")]
        ThirdPartyCompany = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEArchiveReceiptState")]
    public enum EArchiveReceiptState
    {
        [Description("دريافت نشده")]
        NotReceived = 2,
        [Description("دريافت شده")]
        Received = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmEArrestType")]
    public enum EArrestType
    {
        [Description("بازداشت سرقفلي")]
        OwnershipLock = 3,
        [Description("بازداشت دائم")]
        Permanent = 1,
        [Description("بازداشت موقت")]
        Temporary = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEarthquakeResistant")]
    public enum EarthquakeResistant
    {
        [Description("ديوار باربر شناژ قائم و افقي")]
        BarrierWall = 5,
        [Description("قاب خمشي")]
        BendingFrame = 2,
        [Description("باد بند")]
        DikeWind = 3,
        [Description("ندارد")]
        HasNot = 1,
        [Description("ترکيبي")]
        Hybrid = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmECessionEstateType")]
    public enum ECessionEstateType
    {
        [Description("انتقال منضم")]
        TransferOfAttached = 2,
        [Description("انتقال پلاک")]
        TransferOfPlaque = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmECMServerLocation")]
    public enum ECMServerLocation
    {
        [Description("مرکز")]
        Central = 3,
        [Description("اداره کل-استان")]
        HighOffice = 2,
        [Description("محلي-حوزه ثبتي")]
        Local = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmECoordinateSystem")]
    public enum ECoordinateSystem
    {
        [Description("Local")]
        Local = 2,
        [Description("UTM")]
        UTM = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmEDataChangeType")]
    public enum EDataChangeType
    {
        [Description("افزودن")]
        Add = 1,
        [Description("ويرايش")]
        Edit = 2,
        [Description("حذف کردن")]
        Remove = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmEDefect")]
    public enum EDefect
    {
        [Description("تعيين شده")]
        Determined = 1,
        [Description("رفع شده")]
        Fixed = 2,
        [Description("رفع نشده")]
        NotFixed = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmEEstateEArchiveDiffType")]
    public enum EEstateEArchiveDiffType
    {
        [Description("پرونده به بايگاني بازگردانده نشده است")]
        DocumentsNotReturned = 1,
        [Description("پلاک تکراري در جامع املاک موجود است")]
        DuplicateInEEstateFound = 3,
        [Description("پلاک آرشيو استاندارد نيست")]
        EArchiveRecordNotStandard = 4,
        [Description("پلاک آرشيو با پلاک جامع املاک مغاير است")]
        EEstateEArchiveDiffFound = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEEstateEArchiveMatchType")]
    public enum EEstateEArchiveMatchType
    {
        [Description("انطباق ناقص")]
        PartialMatch = 0,
        [Description("انطباق کامل")]
        TotalMatch = 1,
        [Description("انتخاب شده توسط کاربر")]
        UserChosen = 2,
        [Description("")]
        None = -1
    }
    [Description("EnmEMaadRequestSender")]
    public enum EMaadRequestSender
    {
        [Description("دستگاههاي اجرايي")]
        GoverenmentExecutiveUnit = 2,
        [Description("استعلام")]
        Inquery = 1,
        [Description("دعوتنامه")]
        Invitation = 4,
        [Description("مستندات قانوني")]
        LegalDocuments = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmEMapKeepingPlace")]
    public enum EMapKeepingPlace
    {
        [Description("داخل پرونده ثبتي")]
        InsideTheCase = 1,
        [Description("خارج پرونده ثبتي")]
        OutOfFile = 2,
        [Description("ناموجود")]
        Unavailable = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmEMapProducer")]
    public enum EMapProducer
    {
        [Description("متقاضي")]
        Applicant = 2,
        [Description("ثبت اسناد")]
        RegistrationOrganization = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmEMapType")]
    public enum EMapType
    {
        [Description("رقومي")]
        NotPaper = 2,
        [Description("کاغذي")]
        Paper = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmEmpPostType")]
    public enum EmpPostType
    {
        [Description("ثابت")]
        Permanent = 1,
        [Description("موقت")]
        Temporary = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEntitlementToFile")]
    public enum EntitlementToFile
    {
        [Description("دولت تابعه")]
        ContractingOrganization = 2,
        [Description("کشور تابعه")]
        ContractingState = 1,
        [Description("کشور مقيم")]
        Domiciled = 3,
        [Description("کشور مالک علامت")]
        EffectiveIndustrial = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmEPaperSize")]
    public enum EPaperSize
    {
        [Description("A")]
        A = 1,
        [Description("A0")]
        A0 = 2,
        [Description("A1")]
        A1 = 3,
        [Description("A2")]
        A2 = 4,
        [Description("A3")]
        A3 = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmEParameterType")]
    public enum EParameterType
    {
        [Description("ورودي")]
        Input = 1,
        [Description("خروجي")]
        Output = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEPaymentState")]
    public enum EPaymentState
    {
        [Description("از پرداخت منصرف شده است")]
        Canceled = 3,
        [Description("پرداخت انجام شده غير قابل استرداد")]
        Paid = 2,
        [Description("وضعيت پرداخت در حال بررسي است")]
        Processing = 5,
        [Description("آماده استرداد")]
        Ready2Refund = 7,
        [Description("آماده پرداخت است")]
        ReadyToPayment = 1,
        [Description("بي اثر شده")]
        Refundable = 8,
        [Description("استرداد شده")]
        Refunded = 9,
        [Description("")]
        None = 0
    }
    [Description("EnmEPieceNatureType")]
    public enum EPieceNatureType
    {
        [Description("مشاعي")]
        Participant = 1,
        [Description("اختصاصي")]
        Privacy = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEProcessStepState")]
    public enum EProcessStepState
    {
        [Description("پاياني")]
        Final = 2,
        [Description("آغازين")]
        Initialize = 0,
        [Description("مياني")]
        middle = 1,
        [Description("تک گام")]
        OnlyStep = 3,
        [Description("")]
        None = -1 //remove warning
    }
    [Description("EnmERequestManType")]
    public enum ERequestManType
    {
        [Description("نماينده")]
        Agent = 4,
        [Description("قيم")]
        Ghayyem = 2,
        [Description("وکيل")]
        Lawyer = 3,
        [Description("وصي")]
        Vasi = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmErrorRelatedGroup")]
    public enum ErrorRelatedGroup
    {
        [Description("تحليل گر")]
        Analyst = 2,
        [Description("طراح")]
        Designer = 3,
        [Description("برنامه نويس")]
        Programmer = 4,
        [Description("آموزش")]
        Tutorial = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmErrorState")]
    public enum ErrorState
    {
        [Description("انجام شد   ")]
        IsDone = 1,
        [Description("خطا وارد نيست")]
        NotAcceptedError = 4,
        [Description("انجام نشد  ")]
        NotDone = 2,
        [Description("در حال انجام  ")]
        UnderDevelope = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmEstate")]
    public enum Estate
    {
        [Description("ملکـــ")]
        Estaet = 1,
        [Description("قطعــه")]
        Piece = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateCessionTypeKind")]
    public enum EstateCessionTypeKind
    {
        [Description("تجميع سهام")]
        AggregateShares = 5,
        [Description("املاک جاري")]
        EstateCurrent = 3,
        [Description("محدوديت")]
        EstateRestriction = 1,
        [Description("مالکيت")]
        EstateTransaction = 2,
        [Description("خروج مالکيت")]
        ExitFromDocument = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateDocType")]
    public enum EstateDocType
    {
        [Description("کاداستري")]
        Cadaster = 1,
        [Description("دفترچه‏اي")]
        Note = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateDocumentType")]
    public enum EstateDocumentType
    {
        [Description("سند")]
        Document = 2,
        [Description("پيش سند")]
        PreDocument = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateEstalam")]
    public enum EstateEstalam
    {
        [Description("ثبت استعلام")]
        RegistrationEstalem = 1,
        [Description("ثبت پاسخ استعلام")]
        ResponceEstalam = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateFunctionDocument")]
    public enum EstateFunctionDocument
    {
        [Description("عمليات صدور سند مالکيت")]
        EstateFunctionDocument = 1,
        [Description("عمليات ابطال سند مالکيت")]
        FunctionDelete = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateInputOutput")]
    public enum EstateInputOutput
    {
        [Description("وارده")]
        Input = 1,
        [Description("صادره")]
        Output = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateInquiryType")]
    public enum EstateInquiryType
    {
        [Description("منضم")]
        Attach = 2,
        [Description("ملکي")]
        Estate = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateJoint")]
    public enum EstateJoint
    {
        [Description("منضـم")]
        EstateAttached = 1,
        [Description("مشـاع")]
        EstateJoint = 2,
        [Description("عرصـه")]
        EstateParcel = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateNatureChangeDetail")]
    public enum EstateNatureChangeDetail
    {
        [Description("تفکيک آپارتماني")]
        SeparationToApartment = 2,
        [Description("تفکيک قطعاتي")]
        Seperation = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateNatureChangeDetailOther")]
    public enum EstateNatureChangeDetailOther
    {
        [Description("عمليات تجميع")]
        Aggregate = 2,
        [Description("عمليات تخريب")]
        Destruction = 3,
        [Description("عمليات افراز")]
        Patition = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateOperationKind")]
    public enum EstateOperationKind
    {
        [Description("تجميع")]
        Aggregate = 4,
        [Description("صدور سند")]
        Issue = 1,
        [Description("افراز")]
        Partition = 3,
        [Description("انتقال")]
        Transfer = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateOwnershipType")]
    public enum EstateOwnershipType
    {
        [Description("عرصه و اعيان")]
        Feild_Grandee = 3,
        [Description("عرصه")]
        Field = 1,
        [Description("اعيان")]
        Grandee = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateOwnerType")]
    public enum EstateOwnerType
    {
        [Description("موسسه غيردولتي")]
        Institue = 2,
        [Description("شخص")]
        Person = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateOwnetMultiplicity")]
    public enum EstateOwnetMultiplicity
    {
        [Description("بدون مالکيت")]
        EmptyOwner = 3,
        [Description("چند مالکيتي")]
        MultipleOwner = 2,
        [Description("تک مالکيتي")]
        SingleOwner = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmEstatePersonType")]
    public enum EstatePersonType
    {
        [Description("وکيل")]
        Attorney = 2,
        [Description("ولي")]
        Deputy = 3,
        [Description("مالک")]
        OwnerEstate = 1,
        [Description("قيم")]
        Protector = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateSaleInstanceType")]
    public enum EstateSaleInstanceType
    {
        [Description("معامل")]
        Factor = 2,
        [Description("متعامل")]
        Interacting = 1,
        [Description("به نفع")]
        Mortgage = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmEstateType")]
    public enum EstateType
    {
        [Description("مسکوني")]
        Bulding = 3,
        [Description("باغ")]
        Garden = 2,
        [Description("مزروعي")]
        Land = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmEventAction")]
    public enum EventAction
    {
        [Description("توقف فرآيند ")]
        Close = 4,
        [Description("انجام  ")]
        Done = 2,
        [Description("هيچكدام      ")]
        NoEvent = 5,
        [Description("تعليق فرآيند ")]
        Pause = 3,
        [Description("ادامه فرآيند   ")]
        Resume = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmEventRegisterationEffect")]
    public enum EventRegisterationEffect
    {
        [Description("عدم توقف و منتظر رخداد")]
        ProcessWait = 2,
        [Description("توقف و منتظر رخداد")]
        StopWait = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmEventState")]
    public enum EventState
    {
        [Description("واقع شده")]
        Done = 1,
        [Description("برگشت داده شده")]
        StepBacked = 3,
        [Description(" منتظر رخداد")]
        Wait = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmEvidenceOwnerShip")]
    public enum EvidenceOwnerShip
    {
        [Description("خريداري رسمي")]
        Formal = 2,
        [Description("خريداري عادي")]
        Normal = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmExecutiveState")]
    public enum ExecutiveState
    {
        [Description("تاييد تکميل اجرائيه جهت تشکيل پرونده")]
        Confirm4CreateCase = 6,
        [Description("تاييد و ارسال شده به واحد ثبتي توسط دفترخانه")]
        ConfirmAndSend = 2,
        [Description("تشکيل پرونده از روي اجرائيه انجام شده")]
        CreateCase = 5,
        [Description("مختومه شده")]
        EndState = 9,
        [Description("تنظيم شده")]
        PreConfirm = 1,
        [Description("برگشت داده شده به دفترخانه به دليل نقص")]
        Return = 4,
        [Description("برگشت اجرائيه به کارشناس جهت رفع اشکال ها")]
        Return2Expert = 8,
        [Description("تاييد وصول اجرائيه ارسالي دفترخانه به واحد ثبتي توسط واحد ثبتي")]
        View = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmExpEmployeeType")]
    public enum ExpEmployeeType
    {
        [Description("کارشناس مرکز مشاورين قوه قضاييه")]
        AdvisorJudiciary = 3,
        [Description("مامور اجرا")]
        ExecutiveAgent = 4,
        [Description("خبره محلي-غيررسمي")]
        LocalExpert = 2,
        [Description("رسمي ")]
        Official = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmExpertismDescMakerType")]
    public enum ExpertismDescMakerType
    {
        [Description("کارشناس پرونده")]
        ExpertMan = 1,
        [Description("مدير")]
        Manager = 3,
        [Description("کارشناس مسئول")]
        MasterExpertMan = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmExpertismOutConflictDbType")]
    public enum ExpertismOutConflictDbType
    {
        [Description("EPO")]
        EPO = 2,
        [Description("ساير")]
        Others = 4,
        [Description("PatentScope")]
        PatentScope = 1,
        [Description("USPTO")]
        USPTO = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmExpertState")]
    public enum ExpertState
    {
        [Description("مشغول به كار")]
        Busy = 3,
        [Description("اتمام كار")]
        FinishJob = 4,
        [Description("مرخصي ")]
        Leave = 1,
        [Description("تعليق ")]
        Suspend = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmExplorerUpDownFlag")]
    public enum ExplorerUpDownFlag
    {
        [Description("متغير پايين دست")]
        Down = 2,
        [Description("متغير بالادست")]
        Up = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmExpNodeType")]
    public enum ExpNodeType
    {
        [Description("پوشه كاوشگر")]
        ExpFolder = 2,
        [Description("شيء كاوشگر")]
        ExpObject = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmFichePaymentForType")]
    public enum FichePaymentForType
    {
        [Description("هزينه کارشناسي")]
        ExpertCost = 1,
        [Description("هزينه دادرسي")]
        JudgmentCost = 2,
        [Description("خسارت احتمالي")]
        LikelyDamage = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmFichePersonType")]
    public enum FichePersonType
    {
        [Description("شخص دخيل در پرونده")]
        CasePerson = 1,
        [Description("شخص دخيل در دادخواست  ")]
        PetitionPerson = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmFieldDataType")]
    public enum FieldDataType
    {
        [Description("متني")]
        Char = 1,
        [Description("عدد صحيح")]
        Integer = 2,
        [Description("عدد اعشاري")]
        Number = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmFileExtension")]
    public enum FileExtension
    {
        [Description("Aspx")]
        Aspx = 19,
        [Description("بي ام پي")]
        BMP = 12,
        [Description("CSV")]
        CSV = 18,
        [Description("ورد")]
        DOC = 5,
        [Description("ورد دوهزاروهفت به بالا")]
        DOCX = 6,
        [Description("dwg")]
        Dwg = 20,
        [Description("گيف")]
        GIF = 10,
        [Description("جي پي اي چي")]
        JPEG = 11,
        [Description("جي پي  جي")]
        JPG = 3,
        [Description("ام اچ تي")]
        MHT = 13,
        [Description("partial")]
        Partial = 17,
        [Description("پي دي اف")]
        PDF = 1,
        [Description("پي ان جي")]
        PNG = 4,
        [Description("رِر")]
        RAR = 15,
        [Description("تيف")]
        TIF = 9,
        [Description("تيفف")]
        TIFF = 2,
        [Description("TXT")]
        Txt = 21,
        [Description("url")]
        URL = 16,
        [Description("اکسل")]
        XLS = 7,
        [Description("اکسل دوهزاروهفت به بالا")]
        XLSX = 8,
        [Description("زيپ")]
        ZIP = 14,
        [Description("")]
        None = 0
    }
    [Description("EnmFinger")]
    public enum Finger
    {
        [Description("انگشت شست دست راست")]
        Finger1 = 1,
        [Description("انگشت كوچك دست چپ")]
        Finger10 = 10,
        [Description("انگشت اشاره دست راست")]
        Finger2 = 2,
        [Description("انگشت وسط دست راست")]
        Finger3 = 3,
        [Description("انگشت انگشتري دست راست")]
        Finger4 = 4,
        [Description("انگشت كوچك دست راست")]
        Finger5 = 5,
        [Description("انگشت شست دست چپ")]
        Finger6 = 6,
        [Description("انگشت اشاره دست چپ")]
        Finger7 = 7,
        [Description("انگشت وسط دست چپ")]
        Finger8 = 8,
        [Description("انگشت انگشتري دست چپ")]
        Finger9 = 9,
        [Description("")]
        None = 0
    }
    [Description("EnmFingerType")]
    public enum FingerType
    {
        [Description("انگشت کنار شست پاي چپ")]
        LeftFoot12 = 12,
        [Description("انگشت مياني پاي چپ")]
        LeftFoot14 = 14,
        [Description("انگشت شست پاي چپ")]
        LeftFoot16 = 16,
        [Description("انگشت کنار انگشت کوچک پاي چپ")]
        LeftFoot18 = 18,
        [Description("انگشت کوچک پاي چپ")]
        LeftFoot20 = 20,
        [Description("کوچک دست چپ")]
        LeftLittle = 10,
        [Description("مياني دست چپ")]
        LeftMiddle = 8,
        [Description("اشاره دست چپ")]
        LeftPoint = 7,
        [Description("انگشتر دست چپ")]
        LeftRing = 9,
        [Description("شست دست چپ")]
        LeftThumb = 6,
        [Description("انگشت کنار شست پاي راست")]
        RightFoot11 = 11,
        [Description("انگشت مياني پاي راست")]
        RightFoot13 = 13,
        [Description("انگشت شست پاي راست")]
        RightFoot15 = 15,
        [Description("انگشت کنار انگشت کوچک پاي راست")]
        RightFoot17 = 17,
        [Description("انگشت کوچک پاي راست")]
        RightFoot19 = 19,
        [Description("کوچک دست راست")]
        RightLittle = 5,
        [Description("مياني دست راست")]
        RightMiddle = 3,
        [Description("اشاره دست راست")]
        RightPoint = 2,
        [Description("انگشتر دست راست")]
        RightRing = 4,
        [Description("شست دست راست")]
        RightThumb = 1,
        [Description("اثر انگشت معتبر ندارد")]
        NoFingerPrint = 99,
        [Description("")]
        None = 0
    }
    [Description("EnmFixingState")]
    public enum FixingState
    {
        [Description("تطبيق داده شده")]
        Adapted = 5,
        [Description("در حال تثبيت")]
        BeingFixing = 3,
        [Description("تثبيت شده")]
        Fixing = 2,
        [Description("نياز به تثبيت مجدد")]
        NeedtobeFixingAgain = 4,
        [Description("تثبيت نشده")]
        NotFixing = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmGArgumentDataType")]
    public enum GArgumentDataType
    {
        [Description("منطقي")]
        Boolean = 2,
        [Description("عددي")]
        Number = 3,
        [Description("متني")]
        String = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmGArgumentType")]
    public enum GArgumentType
    {
        [Description("ورودي")]
        Input = 1,
        [Description("خروجي")]
        Output = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmGeneralOrganizationType")]
    public enum GeneralOrganizationType
    {
        [Description("جهاد کشاورزي")]
        Arazi = 6,
        [Description("شعب دادگاه")]
        JudiciaryUnit = 2,
        [Description("شهرداري")]
        Municipality = 5,
        [Description("شعب ناجا")]
        NAJAUnit = 3,
        [Description("ساير سازمانها")]
        OtherOrganization = 4,
        [Description("واحدهاي سازمان ثبت")]
        SSAAOrganization = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmGetIdentityType")]
    public enum GetIdentityType
    {
        [Description("درخواست مرجع نظارتي")]
        AgentReq = 3,
        [Description("درخواست مديران عالي سازمان")]
        HighMngReq = 1,
        [Description("درخواست مرجع قضايي")]
        JudReq = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmGoodsServiceType")]
    public enum GoodsServiceType
    {
        [Description("همه کالاها يا خدمات")]
        AllGoodsServices = 1,
        [Description("ليستي از همه کالاها يا خدمات")]
        ListOfPartialGoodsServices = 3,
        [Description("برخي از کالاها يا خدمات")]
        PartialGoodsSevices = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmHaveAttachment")]
    public enum HaveAttachment
    {
        [Description("پيوست مي باشد")]
        HaveAttachment = 1,
        [Description("پيوست نمي باشد")]
        HaveNotAttachment = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmHaveNoHave")]
    public enum HaveNoHave
    {
        [Description("دارد")]
        Have = 2,
        [Description("ندارد")]
        NotHave = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmHeating")]
    public enum Heating
    {
        [Description("بويلر فن کوئل")]
        Boiler = 3,
        [Description("بخاري گازي")]
        GasHeater = 2,
        [Description("ندا رد")]
        HaveNot = 6,
        [Description("موتورخانه و رادياتور")]
        Radiator = 1,
        [Description("وي آر آف")]
        VRF = 5,
        [Description("ساير")]
        Others = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmHistoryType")]
    public enum HistoryType
    {
        [Description("غير كامپيوتري")]
        Manual = 1,
        [Description("كامپيوتري")]
        Systematic = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmHolidayKind")]
    public enum HolidayKind
    {
        [Description("تعطيل رسمي")]
        Holiday = 2,
        [Description("تعطيل غيررسمي")]
        UnFormalHoliday = 3,
        [Description("روز کاري")]
        Work = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmHowNotified")]
    public enum HowNotified
    {
        [Description("بوسيله ارسال به صندوق پستي")]
        ByPostBox = 6,
        [Description("بوسيله سامانه ابلاغ الکترونيک")]
        ByElectronicNoticeSystem = 14,
        [Description("توسط کارمند واحد")]
        ByOfficer = 5,
        [Description("توسط اداره پست")]
        ByPost = 1,
        [Description("بوسيله رايانامه")]
        EMail = 4,
        [Description("توسط فرماندهي قضايي")]
        JudgeHead = 11,
        [Description("توسط واحد ابلاغ دادگستري")]
        JudgeNoticeOffice = 8,
        [Description("توسط پاسگاه انتظامي")]
        MeletaryCamp = 10,
        [Description("توسط واحد ابلاغ نيروي انتظامي")]
        NAJAOffice = 9,
        [Description("به وسيله آگهي در روزنامه")]
        NewspaperNotice = 3,
        [Description("توسط مامور ابلاغ")]
        OfficerNotified = 15,
        [Description("بوسيله نيابت به حوزه ثبتي ديگر")]
        OutsideArea = 7,
        [Description("بوسيله پست سفارشي")]
        RegisteredMail = 2,
        [Description("توسط دفتر نظارت و هماهنگي اجراي اسناد رسمي")]
        SSAANezarat = 12,
        [Description("")]
        None = 0
    }
    [Description("EnmHowToPay")]
    public enum HowToPay
    {
        [Description("نقد")]
        Cash = 4,
        [Description("پرداخت در باجه بانک")]
        Counter = 2,
        [Description("سامانه قبلي پرداخت وجوه")]
        DisconnectedPOS = 3,
        [Description("پرداخت الکترونيک")]
        Electronic = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmHowToPayXCaseCost")]
    public enum HowToPayXCaseCost
    {
        [Description("به اعتراف بستانکار")]
        DebtorAdmission = 2,
        [Description("واريز به حساب")]
        DepositAccount = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmHstNoticeSendState")]
    public enum HstNoticeSendState
    {
        [Description("رويت نتيجه ابلاغ شده/صحت عمليات ابلاغ تاييد شده است")]
        Confirm4Notice = 6,
        [Description("تاييد شده جهت ارسال")]
        Confirm4Send = 3,
        [Description("پيش نويس شده")]
        Draft = 1,
        [Description("رويت نتيجه ابلاغ شده ولي صحت عمليات ابلاغ تاييد نشده است")]
        Refuse4Notice = 7,
        [Description("ثبت شده جهت تاييد")]
        Registered = 2,
        [Description("برگشت داده شده به ثبت کننده جهت رفع اشکال")]
        Return4Modify = 4,
        [Description("ارسال شده")]
        Sended = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmHstUserSecurity")]
    public enum HstUserSecurity
    {
        [Description("كارمند شعبه")]
        BranchEmployee = 1,
        [Description("كارمند اظهار نظر")]
        CriminalRemarkEmployee = 5,
        [Description("سابقه ياب")]
        FindHistory = 6,
        [Description("رئيس شعبه ")]
        Judge = 4,
        [Description("مدير سيستم")]
        Manager = 3,
        [Description("ثبات مجتمع")]
        Punchist = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmIAgentType")]
    public enum IAgentType
    {
        [Description("کارگزار حقوقي")]
        LegalAgent = 2,
        [Description("دفتر اسناد رسمي")]
        Notary = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmICaseOldHistoryChangeType")]
    public enum ICaseOldHistoryChangeType
    {
        [Description("آگهي اصلاحي")]
        AdvertisignModification = 6,
        [Description("ابطال دادگاه")]
        CancelByJudge = 1,
        [Description("درخواست المثني")]
        CopyRequest = 5,
        [Description("اعراض")]
        Eraz = 2,
        [Description("تغيير نام")]
        Rename = 7,     // remove warning	
        [Description("انتقال ")]
        Transfer = 4,
        [Description("اجازه بهره برداري")]
        UsePermissionByOther = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmICommissionDecissionType")]
    public enum ICommissionDecissionType
    {
        [Description("پذيرش اعتراض و کان لم يکن کردن پرونده مورد اعتراض")]
        AcceptProtestAndLikeNotExistProtested = 8,
        [Description("تغيير در نوع تصميم اداره")]
        ChangeDecisionOnExpertism = 1,
        [Description("پذيرش اعتراض به تقاضاي ثبت")]
        MainCaseAcceptProtestAfterProtest = 4,
        [Description("اعتراض به تقاضاي ثبت وارد نيست. پذيرش بلامانع است")]
        MainCaseReadyToAcceptAfterAnotherProtest = 3,
        [Description("پذيرش اعتراض به تقاضاي ثبت و اقدام طبق نظر کميسيون")]
        OtherCaseAcceptProtestWithCommissionAction = 7,
        [Description("پذيرش اعتراض به تقاضاي ثبت و عدم نياز به اقدام")]
        OtherCaseAcceptProtestWithNoAction = 6,
        [Description("رد اعتراض به تقاضاي ثبت")]
        OtherCaseRejectProtest = 5,
        [Description("رد اعتراض به تصميم اداره")]
        RejectProtestOnExpertism = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmICommissionFinalizedState")]
    public enum ICommissionFinalizedState
    {
        [Description("تاييد يا نقض شده در دادگاه")]
        ChangeOrConfirmByCourt = 3,
        [Description("قطعي شده")]
        Finalized = 2,
        [Description("قطعي نشده")]
        NonFinalized = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmICommissionType")]
    public enum ICommissionType
    {
        [Description("رسيدگي به اعتراض به تقاضاي ثبت")]
        Commission4ProtestOnDeclaration = 2,
        [Description("رسيدگي به اعتراض به رد")]
        Commission4ProtestOnReject = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmIDecisionSource")]
    public enum IDecisionSource
    {
        [Description("کميسيون")]
        Commission = 2,
        [Description("دادگاه ")]
        Court = 3,
        [Description("اداره")]
        ExpertMan = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmIDeclarationAgentType")]
    public enum IDeclarationAgentType
    {
        [Description("نماينده با وکالت محضري")]
        AgentWithBureauAttorney = 4,
        [Description("تحصيلدار")]
        Collector = 5,
        [Description("صاحب امضاء")]
        HasSignPriority = 3,
        [Description("وکيل رسمي دادگستري")]
        Lawyer = 1,
        [Description("دفتر حقوقي")]
        LegalLawyer = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmIDeclarationNewOrModifyType")]
    public enum IDeclarationNewOrModifyType
    {
        [Description("اظهارنامه افزايش کالا")]
        AddProduct = 2,
        [Description("اظهارنامه اضافه کالاي همزمان با اعتراض")]
        AddProductByProtest = 5,
        [Description("تغيير علامت")]
        ChangeImage = 3,
        [Description("اظهارنامه تقسيمي اختراع")]
        DivisionalDeclaration = 7,
        [Description("ادغام اظهارنامه")]
        MergeDeclaration = 8,
        [Description("اظهارنامه جديد")]
        NewDeclaration = 1,
        [Description("اظهارنامه همزمان با اعتراض")]
        NewDeclarationByProtest = 4,
        [Description("تعيين متعاقب")]
        SubsequentDesignation = 9,
        [Description("اظهارنامه تکميلي اختراع")]
        SupplementaryDeclaration = 6,
        [Description("تبديل پرونده بين المللي به ملي")]
        Transformation = 10,
        [Description("")]
        None = 0
    }
    [Description("EnmIExpertismDecisionType")]
    public enum IExpertismDecisionType
    {
        [Description("پذيرش درخواست")]
        AcceptRequest = 10,
        [Description("ابطال طبق نظر دادگاه")]
        CancelByCourt = 12,
        [Description("تاييد صحت مدارک")]
        CaseDocumentsIsComplete = 18,
        [Description("تکراري")]
        Duplicate = 32,
        [Description("پايان مهلت رفع نقص")]
        ExpireDefectRespite = 2,
        [Description("آگهي نوبت اول")]
        FirstAdvertisign = 5,
        [Description("آگهي نوبت اول طبق نظر کميسيون")]
        FirstAdvertisignByCommission = 16,
        [Description("آگهي نوبت اول طبق نظر دادگاه")]
        FirstAdvertisignByCourt = 17,
        [Description("نقص دارد")]
        HasDefect = 1,
        [Description("نياز به استعلام")]
        NeedToEnquiry = 6,
        [Description("نياز به حضور جهت مطالعه سوابق")]
        NeedToPresence = 11,
        [Description("تصميمي اتخاذ نشده")]
        NotDecidedYet = 30,
        [Description("اختراع ناپذيري")]
        NotPatentable = 31,
        [Description("پذيرش بلامانع است")]
        ReadyToAccept = 4,
        [Description("پذيرش بلامانع طبق نظر کميسيون")]
        ReadyToAcceptByCommission = 14,
        [Description("پذيرش بلامانع طبق نظر دادگاه")]
        ReadyToAcceptByCourt = 13,
        [Description("ثبت بلامانع")]
        ReadyToRegister = 7,
        [Description("پايان مهلت پرداخت حق الثبت")]
        RegisterCostRespite = 8,
        [Description("رد مي شود")]
        Reject = 3,
        [Description("رد طبق نظر کميسيون")]
        RejectByCommission = 15,
        [Description("رد به موجب راي دادگاه")]
        RejectByCourt = 20,
        [Description("رد مرجع")]
        RejectByReference = 37,
        [Description("نقص ندارد. منتظر فرآيند اعتراضي است.")]
        WaitForProtestFlow = 9,
        [Description("نياز به استعلام طبق نظر کميسيون")]
        NeedToCommisionDecisionInquiry = 19,
        [Description("")]
        None = 0
    }
    [Description("EnmIExpertismLikeType")]
    public enum IExpertismLikeType
    {
        [Description("متفاوت")]
        Different = 3,
        [Description("عين")]
        Equal = 2,
        [Description("مشابه")]
        Like = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmIExpertismState")]
    public enum IExpertismState
    {
        [Description("اتمام کارشناسي")]
        ConfirmedByExpertMan = 2,
        [Description("تاييد توسط مدير")]
        ConfirmedByManager = 3,
        [Description("پيش نويس")]
        Draft = 1,
        [Description("رد توسط مدير")]
        RejectedByManager = 4,
        [Description("ارسال مجدد به مديريت")]
        ResendToManager = 6,
        [Description("برگشت به کارشناس")]
        ReturnToExpertMan = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmIExpertismType")]
    public enum IExpertismType
    {
        [Description("کارشناسي تغييرات")]
        ChangeExpertism = 2,
        [Description("کارشناسي جديد")]
        NewExpertism = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmIIndustrialCaseReferringReason")]
    public enum IIndustrialCaseReferringReason
    {
        [Description("تغيير ارجاع توسط مدير")]
        RefferdByManager = 2,
        [Description("ارجاع بدليل درخواست تغييرات")]
        RefferedByInputLetter = 3,
        [Description("ارجاع اظهارنامه")]
        RefferedOnDeclaration = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmIIndustrialSignPartType")]
    public enum IIndustrialSignPartType
    {
        [Description("قطعي")]
        Final = 1,
        [Description("اختياري")]
        Optional = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmIMistakeCorrectionType")]
    public enum IMistakeCorrectionType
    {
        [Description("ابطال و صدور مجدد آگهي")]
        DisproffAdvertisingAndReIssueAdvertising = 8,
        [Description("ابطال آگهي")]
        CancelAdvertising = 1,
        [Description("ابطال کارشناسي")]
        CancelExpertism = 2,
        [Description("ابطال ابلاغيه")]
        CancelNotice = 3,
        [Description("تغيير وضعيت پرونده")]
        ChangeCaseState = 5,
        [Description("تغيير نوع اظهارنامه و شماره مرتبط")]
        ChangeNewOrModifyType = 7,
        [Description("تغيير شماره ثبت مالکيت")]
        ChangeRegisterNo = 6,
        [Description("اصلاح شماره و تاريخ اظهارنامه")]
        ModifyDeclarationNo = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmImmovableOwnershipType")]
    public enum ImmovableOwnershipType
    {
        [Description("عـرصـه")]
        Field = 1,
        [Description("عرصه و اعيان")]
        FieldAndGrandee = 3,
        [Description("اعيان")]
        Grandee = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmIncomingLetterState")]
    public enum IncomingLetterState
    {
        [Description("ارجاع/ارسال شده به کاربر")]
        ReferByMngSecret = 4,
        [Description("ثبت شده")]
        Registered = 1,
        [Description("برگشت داده شده به ثبت کننده")]
        Return = 3,
        [Description("برگشت داده شده به مدير")]
        ReturnToMng = 5,
        [Description("رويت شده")]
        View = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmIncommingLetterSenderType")]
    public enum IncommingLetterSenderType
    {
        [Description("شخص پرونده")]
        CasePerson = 1,
        [Description("واحد ثبتي")]
        SabtUnit = 2,
        [Description("از فهرست فرستنده ها")]
        SendersInList = 3,
        [Description("تايپ نام فرستنده")]
        SendersNotInList = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmIndividualPersonType")]
    public enum IndividualPersonType
    {
        [Description("وكيل")]
        Lawyer = 3,
        [Description("حقوقي           ")]
        Legal = 2,
        [Description("حقيقي بجز وكيل")]
        NaturalPerson = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmIndustrialAgentType")]
    public enum IndustrialAgentType
    {
        [Description("نماينده با وکالت محضري")]
        AgentWithBureauAttorney = 3,
        [Description("تحصيلدار")]
        Collector = 5,
        [Description("وکيل رسمي دادگستري")]
        JusticeLegalAgent = 1,
        [Description("دفتر حقوقي")]
        LegalBureau = 2,
        [Description("دارنده حق امضاء")]
        SignPermissionOwner = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmIndustrialCaseChangeState")]
    public enum IndustrialCaseChangeState
    {
        [Description("لغو تغييرات")]
        CancelChanges = 3,
        [Description("تاييد نهايي تغييرات")]
        ConfirmChanges = 2,
        [Description("پيش نويس")]
        Draft = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmIndustrialCaseRejectType")]
    public enum IndustrialCaseRejectType
    {
        [Description("ابطال")]
        Cancel = 2,
        [Description("کان لم يکن")]
        ItIsNot = 3,
        [Description("رد")]
        Reject = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmIndustrialCaseState")]
    public enum IndustrialCaseState
    {
        [Description("به پرونده اصلي ملحق شد")]
        AddedToMainCase = 18,
        [Description("توقيف شده")]
        Arrested = 22,
        [Description("انصراف داده شده")]
        CancelByApplicant = 15,
        [Description("ابطال شده ")]
        Canceled = 3,
        [Description("مدارک پرونده تاييد نشده")]
        CaseDocumentsNotConfirmed = 23,
        [Description("در مرحله استعلام")]
        ConceptualChecking = 6,
        [Description("در حال رفع نقص")]
        DefectProcess = 2,
        [Description("آگهي نوبت اول")]
        FirstAdvertisign = 17,
        [Description("طرح در کميسيون")]
        InCommission = 12,
        [Description("کان لم يکن")]
        LikeNotExists = 19,
        [Description("ادغام شده")]
        Merged = 25,
        [Description("طبقه بندي نشده")]
        NotCategorized = 21,
        [Description("در حال بررسي اوليه")]
        OnFirstAction = 1,
        [Description("اعتراضي")]
        OnProtest = 14,
        [Description("عرضه شده در فرابورس")]
        PosedInIFB = 20,
        [Description("پذيرش بلامانع")]
        ReadyToAccept = 5,
        [Description("ثبت قطعي شده")]
        Registered = 10,
        [Description("رد شده")]
        Rejected = 4,
        [Description("رد مشروط")]
        RejectNotification = 16,
        [Description("ارسال شده به وايپو")]
        SentToWipo = 24,
        [Description("در انتظار تاييد حساب کاربري")]
        WaitForConfirmingUser = 7,
        [Description("")]
        None = 0
    }
    [Description("EnmIndustrialCertificateType")]
    public enum IndustrialCertificateType
    {
        [Description("اضافه کالا")]
        AddProduct = 3,
        [Description("تغييرات")]
        DeclarationChange = 4,
        [Description("گواهينامه اصلي")]
        MainCertificate = 1,
        [Description("تمديد گواهينامه")]
        ProlongationCertificate = 2,
        [Description("تغيير علامت")]
        SignModification = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmIndustrialExpertismResult")]
    public enum IndustrialExpertismResult
    {
        [Description("نقص شکلي دارد")]
        HaveDefect = 1,
        [Description("بررسي ماهوي شد و رد شد")]
        Rejected = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmIndustrialFlowType")]
    public enum IndustrialFlowType
    {
        [Description("اعمال تغيير در اطلاعات پرونده بر اساس موضوع نامه")]
        ApplyCaseInfoChangeBasedOnLetterSubject = 3,
        [Description("اعمال پرداخت در پرونده")]
        ApplyCasePayment = 4,
        [Description("تغيير کارشناس پرونده اصلي")]
        ChangeExpertManForMainCase = 7,
        [Description("تغيير کارشناس پرونده اعتراضي")]
        ChangeExpertManForProtestedCase = 10,
        [Description("اقدام نهايي نامه وارده")]
        FinalActionOnInputLetter = 5,
        [Description("تعيين کارشناس پرونده اصلي")]
        SetExpertManForMainCase = 6,
        [Description("تعيين کارشناس پرونده اعتراضي")]
        SetExpertManForProtestedCase = 9,
        [Description("تغيير وضعيت پرونده اعتراض شده به وضعيت اعتراضي")]
        SetProtestedCaseToOnProtestState = 11,
        [Description("شروع فرايند اظهارنامه")]
        StartDeclarationFlow = 1,
        [Description("شروع فرايند نامه وارده")]
        StartIncomingLetterFlow = 2,
        [Description("به روز رساني اطلاعات آگهي")]
        UpdateAdvertisingInfo = 8,
        [Description("به روز رساني اقساط پرونده اصلي")]
        UpdateCaseInstallment = 12,
        [Description("")]
        None = 0
    }
    [Description("EnmIndustrialGoodOrServiceType")]
    public enum IndustrialGoodOrServiceType
    {
        [Description("کالا")]
        Good = 1,
        [Description("خدمت بدون کالا")]
        GoodLessService = 2,
        [Description("خدمت با  کالا")]
        GoodNeededService = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmIndustrialSMSOrEmailType")]
    public enum IndustrialSMSOrEmailType
    {
        [Description("آگهي")]
        Advertising = 3,
        [Description("پيامک کارتابل مردمي")]
        CartableUser = 7,
        [Description("کميسيون")]
        Commision = 6,
        [Description("رمز شخصي")]
        Declaration = 1,
        [Description("نامه وارده مالکيت")]
        InputLetter = 5,
        [Description("ابلاغيه")]
        Notice = 2,
        [Description("نامه صادره مالکيت")]
        OutputLetter = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmInquiryType")]
    public enum InquiryType
    {
        [Description("استعلام از سامانه ثبت شرکت ها")]
        CompanyInquiry = 1,
        [Description("استعلام از سامانه املاک")]
        EstateInquiry = 3,
        [Description("استعلام از سامانه مالکيت معنوي")]
        IndustrialInquiry = 4,
        [Description("استعلام از سامانه ثبت الکترونيک")]
        NotaryOfficeInquiery = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmInspectionRequestPriority")]
    public enum InspectionRequestPriority
    {
        [Description("فوري")]
        Immediate = 2,
        [Description("عادي")]
        Normal = 1,
        [Description("خيلي فوري")]
        VeryUrgent = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmInspectorOrAssistant")]
    public enum InspectorOrAssistant
    {
        [Description("همراه")]
        Assistant = 2,
        [Description("بازرس")]
        Inspector = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmInstitutState")]
    public enum InstitutState
    {
        [Description("غير انتفاعي")]
        NoneProfit = 2,
        [Description("انتفاعي")]
        Profit = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmInterval")]
    public enum Interval
    {
        [Description("به پي")]
        T1 = 1,
        [Description("ديوار به پي")]
        T10 = 10,
        [Description("ديوار به ديوار")]
        T11 = 11,
        [Description("ديوار مشترک")]
        T12 = 12,
        [Description("ديواريست مشترک")]
        T13 = 13,
        [Description("غيره")]
        T14 = 14,
        [Description("فنس")]
        T15 = 15,
        [Description("مرزيست اشتراکي")]
        T16 = 16,
        [Description("مرزيست")]
        T17 = 17,
        [Description("مرز بلندي است")]
        T18 = 18,
        [Description("به مرز بلند")]
        T19 = 19,
        [Description("به ديوار")]
        T2 = 2,
        [Description("به مرز")]
        T20 = 20,
        [Description("جا پي است")]
        T21 = 21,
        [Description("به جا پي")]
        T22 = 22,
        [Description("جاي درب وديوار است")]
        T23 = 23,
        [Description("پخي است")]
        T24 = 24,
        [Description("فنسي است")]
        T25 = 25,
        [Description("فنسي است اشتراکي")]
        T26 = 26,
        [Description("به فنس")]
        T27 = 27,
        [Description("ديوار چينه ايست")]
        T28 = 28,
        [Description("چينه ايست اشتراکي")]
        T29 = 29,
        [Description("پي است")]
        T3 = 3,
        [Description("به ديوار چينه اي")]
        T30 = 30,
        [Description("ته پي است")]
        T31 = 31,
        [Description("ته ديواريست")]
        T32 = 32,
        [Description("ته ديواريست اشتراکي")]
        T33 = 33,
        [Description("به ته ديوار")]
        T34 = 34,
        [Description("ته ديوار به ته ديواريست")]
        T35 = 35,
        [Description("خط فرضي است")]
        T36 = 36,
        [Description("بندي است")]
        T37 = 37,
        [Description("ديوار به ديوار است")]
        T38 = 38,
        [Description("جاي پي اشتراکي است")]
        T39 = 39,
        [Description("درب وديواريست")]
        T40 = 40,
        [Description("ديواريست به صورت پخ")]
        T41 = 41,
        [Description("درب است")]
        T42 = 42,
        [Description("درب و ديوار است")]
        T43 = 43,
        [Description("درب و ديوار تمام شيشه اي")]
        T44 = 44,
        [Description("درب و ديوار پنجره ايست")]
        T45 = 45,
        [Description("ديوار و پنجره است")]
        T46 = 46,
        [Description("ديوار ودريچه")]
        T47 = 47,
        [Description("ديواريست مشترک")]
        T48 = 48,
        [Description("ديوار تمام شيشه اي")]
        T49 = 49,
        [Description("پي در پي")]
        T5 = 4,
        [Description("دواريست به صورت مورب")]
        T50 = 50,
        [Description("نيم ديوار")]
        T52 = 52,
        [Description("چير است")]
        T53 = 53,
        [Description("پي مشترک")]
        T6 = 5,
        [Description("در امتداد نهر")]
        T7 = 7,
        [Description("درب و ديوار")]
        T8 = 8,
        [Description("ديواريست")]
        T9 = 9,
        [Description("نرده ايست")]
        ف51 = 51,
        [Description("")]
        None = 0
    }
    [Description("EnmIOTreaty")]
    public enum IOTreaty
    {
        [Description("معاهده پاريس")]
        Paris = 2,
        [Description("PCT")]
        PCT = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmIsBaseRole")]
    public enum IsBaseRole
    {
        [Description("نقش پايه اي")]
        Base = 1,
        [Description("نقش پستي")]
        Post = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmIsDone")]
    public enum IsDone
    {
        [Description("انجام شد")]
        Done = 1,
        [Description("انجام نشد")]
        NotDone = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmIsInCourt")]
    public enum IsInCourt
    {
        [Description("مطرح در محاکم")]
        InCourt = 2,
        [Description("فاقد پرونده در محاکم")]
        NotInCourt = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmIsLost")]
    public enum IsLost
    {
        [Description("بدل")]
        Fake = 2,
        [Description("مفقود")]
        Lost = 3,
        [Description("اصل")]
        Orginal = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmIsReplay")]
    public enum IsReplay
    {
        [Description("دارد ")]
        Have = 1,
        [Description("ندارد ")]
        NotHave = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmIssueDocTime")]
    public enum IssueDocTime
    {
        [Description("بعد از دريافت وجه از شخص")]
        AfterGetMoney = 2,
        [Description("قبل از دريافت وجه از شخص")]
        BeforeGetMoney = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmIsSuspend")]
    public enum IsSuspend
    {
        [Description("در حال فعاليت")]
        Active = 1,
        [Description("راکد")]
        Suspend = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmIsTransfer")]
    public enum IsTransfer
    {
        [Description("تأسيس شده در مرجع جاري")]
        NotTransfer = 1,
        [Description("منتقل شده از مرجع ديگر")]
        Transfer = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmITimedActionState")]
    public enum ITimedActionState
    {
        [Description("انجام داده شده")]
        Done = 1,
        [Description("انجام داده نشده")]
        InProgress = 2,
        [Description("تجديد شده")]
        Repeated = 3,
        [Description("ابطال شده است")]
        Revoke = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmJBulding")]
    public enum JBulding
    {
        [Description("ملکي")]
        Possessive = 1,
        [Description("غير ملکي _استيجاري ")]
        UnlikePossessive = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmJReportType")]
    public enum JReportType
    {
        [Description("ماتريسي")]
        MatrixType = 2,
        [Description("جدولي")]
        TableType = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmJudgementType")]
    public enum JudgementType
    {
        [Description("محکوميت")]
        Condemnation = 1,
        [Description("برائت")]
        Innocence = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmJudgeNotaryState")]
    public enum JudgeNotaryState
    {
        [Description("غير قابل رويت در اداره کل سردفتران و دفترياران")]
        NotVisible = 1,
        [Description("قابل رويت ولي اقدام نشده در اداره کل سردفتران و دفترياران")]
        VisibleNotProcessed = 2,
        [Description("قابل رويت و شروع فرآينده ابلاغ شده در اداره کل سردفتران و دفترياران")]
        VisibleProcessed = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmJudiciaryUnitType")]
    public enum JudiciaryUnitType
    {
        [Description("شعبه")]
        Court = 3,
        [Description("دادگاه عمومي و انقلاب")]
        Dadgah = 8,
        [Description("دادسراي عمومي و انقلاب")]
        Dadsara = 7,
        [Description("اداره كل دادگستري استان")]
        HeadOffice = 20,
        [Description("مجتمع قضايي")]
        JudicialComplex = 5,
        [Description("حوزه قضايي")]
        JuratoryArea = 9,
        [Description("سازمان قضايي")]
        OrganizationJudge = 30,
        [Description("شوراي حل اختلاف")]
        Shora = 6,
        [Description("")]
        None = 0
    }
    [Description("EnmJUnitType")]
    public enum JUnitType
    {
        [Description("بودجه")]
        CostBudget = 500,
        [Description("برنامه")]
        Plan = 499,
        [Description("تملک")]
        TakingPossession = 501,
        [Description("")]
        None = 0
    }
    [Description("EnmKeepPricePeriod")]
    public enum KeepPricePeriod
    {
        [Description("روزانه")]
        Daily = 2,
        [Description("ماهانه")]
        Monthly = 4,
        [Description("بطور يکجا")]
        OverAll = 1,
        [Description("هفتگي")]
        Weekly = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmKindsOfNoticePeople")]
    public enum KindsOfNoticePeople
    {
        [Description("ورثه")]
        Heirs = 3,
        [Description("حقوقي")]
        Legal = 2,
        [Description("حقيقي")]
        Natural = 1,
        [Description("حقيقي/حقوقي")]
        NaturalOrLegal = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmLargeFieldTextSaveType")]
    public enum LargeFieldTextSaveType
    {
        [Description("Html")]
        HtmlType = 1,
        [Description("RTF")]
        RTFType = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmLastExecuteStatus")]
    public enum LastExecuteStatus
    {
        [Description("اجرا شده")]
        Executed = 3,
        [Description("در حين اجرا به خطا برخورده كرده")]
        Failed = 2,
        [Description("اجرا نشده")]
        NotExecuted = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmLawyerDegree")]
    public enum LawyerDegree
    {
        [Description("مشاور درجه 1")]
        FirstClassConcultant = 2,
        [Description("وكيل پايه 1 دادگستري")]
        FirstClassLawyer = 1,
        [Description("كارآموز وكالت")]
        LearningClassConcultant = 4,
        [Description("مشاور درجه 2")]
        SecondClassConcultant = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmLawyerState")]
    public enum LawyerState
    {
        [Description("فوت شده")]
        Dead = 5,
        [Description("ابطال پروانه")]
        DisproofLicense = 2,
        [Description("توديع پروانه")]
        GiveBackLicense = 6,
        [Description("بازنشسته شده")]
        Retired = 4,
        [Description("معلق شده")]
        Suspension = 3,
        [Description("مشغول به كار")]
        Work = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmLawyerTypeLicense")]
    public enum LawyerTypeLicense
    {
        [Description("مركز مشاورين قوه قضاييه")]
        AdvisorJudicature = 2,
        [Description("كانون وكلاء دادگستري")]
        LawyerBand = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmLegalPersonType")]
    public enum LegalPersonType
    {
        [Description("حكومتي")]
        Government = 2,
        [Description("غيردولتي(خصوصي/تعاوني(")]
        NonState = 4,
        [Description("عمومي غير دولتي")]
        Public = 3,
        [Description("دولتي")]
        State = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmLetterAttachmentType")]
    public enum LetterAttachmentType
    {
        [Description("مدرک مکانيزه")]
        Mechanize = 1,
        [Description("سند يا پيوست غيرمکانيزه")]
        NonMechanize = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmLetterBetweenOrgState")]
    public enum LetterBetweenOrgState
    {
        [Description("ارسال نشده")]
        NotSend = 1,
        [Description("دريافت شده")]
        Receive = 3,
        [Description("ارجاع داده شده")]
        Refer = 5,
        [Description("برگشت داده شده")]
        Return = 6,
        [Description("ارسال شده به سازمان/واحد مقصد")]
        Sended = 2,
        [Description("ارسال شده به مرکز")]
        SendedToCentral = 8,
        [Description("رويت شده")]
        View = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmLetterSubjectType")]
    public enum LetterSubjectType
    {
        [Description("ورودي")]
        Input = 1,
        [Description("ورودي/خروجي")]
        InputOutput = 3,
        [Description("خروجي")]
        Output = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmLetterType")]
    public enum LetterType
    {
        [Description("رونوشت نامه")]
        Copy = 2,
        [Description("اصل نامه   ")]
        Letter = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmLOBFieldType")]
    public enum LOBFieldType
    {
        [Description("BLOB_RTF")]
        BLOB_RTFType = 5,
        [Description("BLOB_SimpleText")]
        BLOB_SimpleText = 4,
        [Description("CLOB_RTF")]
        CLOB_RTFType = 2,
        [Description("CLOB_SimpleText")]
        CLOB_SimpleText = 1,
        [Description("Image")]
        ImageType = 3,
        [Description("ساير")]
        OtherBLOBType = 6,
        [Description("")]
        None = 0
    }
    [Description("EnmLocationType")]
    public enum LocationType
    {
        [Description("شهر")]
        City = 5,
        [Description("كشور")]
        Country = 1,
        [Description("دهستان")]
        Part = 6,
        [Description("شهرستان")]
        Province = 3,
        [Description("بخش")]
        Section = 4,
        [Description("استان_ايالت")]
        State = 2,
        [Description("ده")]
        Village = 7,
        [Description("")]
        None = 0
    }
    [Description("EnmLongRunningTaskState")]
    public enum LongRunningTaskState
    {
        [Description("با موفقيت اجرا شده")]
        DoRun = 4,
        [Description("به خطا برخورد کرده")]
        FailRunning = 3,
        [Description("اجرا نشده")]
        NotRun = 1,
        [Description("در حال اجرا")]
        NowRunning = 2,
        [Description("معلق شده")]
        Suspention = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmLpmsDocumentType")]
    public enum LpmsDocumentType
    {
        [Description("مستند ابطال شده")]
        Canceled = 3,
        [Description("مستند تأييد صحت شده")]
        Confirmed = 2,
        [Description("مستند ثبت شده")]
        Registered = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmLpmsExpertNoiceStatus")]
    public enum LpmsExpertNoiceStatus
    {
        [Description("ابلاغيه ابطال شده")]
        Canceled = 4,
        [Description("ابلاغيه تأييد شده")]
        Confirm = 6,
        [Description("ابلاغيه اصلاح شده")]
        Correction = 3,
        [Description("ابلاغيه اصلاح نشده")]
        NonCorrection = 2,
        [Description("ابلاغيه ثبت شده")]
        Registered = 1,
        [Description("ابلاغيه رد شده")]
        Reject = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmLpmsFieldTypeList")]
    public enum LpmsFieldTypeList
    {
        [Description("تاريخ جاري")]
        CurrentDate = 5,
        [Description("ساعت جاري")]
        CurrentTime = 4,
        [Description("عنوان متقاضي")]
        RequestApplicantDescPE = 1,
        [Description("شماره درخواست")]
        RequestNumber = 2,
        [Description("شماره پيگيري")]
        TrackingNumber = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmLpmsNationalCodeStatus")]
    public enum LpmsNationalCodeStatus
    {
        [Description("فراخواني شناسه ملي")]
        IlencRegister = 2,
        [Description("ثبت سيستمي")]
        SystemRegister = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmLpmsNCodeInquiryConnectType")]
    public enum LpmsNCodeInquiryConnectType
    {
        [Description("از طريق وب سرويس")]
        OfWebService = 2,
        [Description("از طريق صفحه سايت اينترنتي")]
        OfWebSite = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmLpmsNCodeInquiryType")]
    public enum LpmsNCodeInquiryType
    {
        [Description("استعلام شناسه ملي بر اساس سوابق نام براي سازمان ثبت کننده")]
        InquiryByArchiveNameForSpecificUnit = 14,
        [Description("استعلام سنجي شناسه ملي بر اساس نام براي سازمان ثبت کننده")]
        InquiryByNameForSpecificUnit = 13,
        [Description("فقط استعلام شناسه ملي بر اساس شناسه ملي براي سازمان ثبت کننده")]
        InquiryByNationalCodeForSpecificUnit = 12,
        [Description("فقط استعلام چند فيلد خاص شناسه ملي بر اساس سوابق نام")]
        InquirySpecialByArchiveName = 11,
        [Description("استعلام شناسه ملي تغيير يافته در يک بازه تاريخ خاص")]
        InquirySpecialByChangeDate = 17,
        [Description("استعلام ليست شناسه ملي هاي ثبت شده بر اساس تاريخ")]
        InquirySpecialByDate = 15,
        [Description("فقط استعلام چند فيلد خاص شناسه ملي بر اساس نام")]
        InquirySpecialByName = 10,
        [Description("فقط استعلام چند فيلد خاص شناسه ملي بر اساس شناسه ملي")]
        InquirySpecialByNationalCode = 9,
        [Description("استعلام شناسه ملي بر اساس تاريخ ختم تصفيه")]
        InquirySpecialBySettleDate = 16,
        [Description("فقط استعلام براي تطابق نام با شناسه ملي")]
        ValidateCompareNationalCodeAndName = 8,
        [Description("فقط استعلام صحت و اعتبارسنجي شناسه ملي بر اساس سوابق نام")]
        ValidateOnlyByArchiveName = 7,
        [Description("فقط استعلام صحت و اعتبارسنجي شناسه ملي بر اساس نام")]
        ValidateOnlyByName = 6,
        [Description("فقط استعلام صحت و اعتبارسنجي شناسه ملي بر اساس شناسه ملي")]
        ValidateOnlyByNationalCode = 5,
        [Description("استعلام بر اساس نام فعال")]
        InquiryByActiveFullName = 2,
        [Description("استعلام بر اساس سوابق نام")]
        InquiryByArchiveFullName = 3,
        [Description("استعلام بر اساس شناسه ملي")]
        InquiryByNationalCode = 1,
        [Description("استعلام صحت شناسه ملي")]
        InquiryValidateNationalCode = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmLpmsPolompBookType")]
    public enum LpmsPolompBookType
    {
        [Description("دفتر دارائي")]
        InventoryBook = 3,
        [Description("دفتر روزنامه")]
        JournalBook = 2,
        [Description("دفتر کل")]
        LedgerBook = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmLpmsRequestStatus")]
    public enum LpmsRequestStatus
    {
        [Description("درخواست فعال")]
        Active = 1,
        [Description("انسداد درخواست")]
        Block = 12,
        [Description("ابطال درخواست")]
        Cancel = 13,
        [Description("تأييد درخواست")]
        Confirm = 2,
        [Description("انصراف از درخواست")]
        Dissuasion = 11,
        [Description("درخواست بدون اعتبار")]
        NoCredit = 14,
        [Description("")]
        None = 0
    }
    [Description("EnmLpmsSurveyPresentType")]
    public enum LpmsSurveyPresentType
    {
        [Description("اعلام خطاي سيستم")]
        SurveyException = 3,
        [Description("درخواست راهنمايي فرآيند سيستم")]
        SurveyHelp = 2,
        [Description("ارائه پيشنهاد توسط کاربر")]
        SurveyProposal = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMadridInOut")]
    public enum MadridInOut
    {
        [Description("داخل به خارج")]
        InOut = 1,
        [Description("خارج به داخل")]
        OutIn = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMadridPreferredLanguage")]
    public enum MadridPreferredLanguage
    {
        [Description("انگليسي")]
        English = 1,
        [Description("فرانسه")]
        French = 2,
        [Description("اسپانيايي")]
        Spanish = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmMainMinorObject")]
    public enum MainMinorObject
    {
        [Description("شيء اصلي")]
        Main = 2,
        [Description("شيء فرعي")]
        Minor = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMainMinorTemplate")]
    public enum MainMinorTemplate
    {
        [Description("کليشه اصلي")]
        Main = 1,
        [Description("کليشه فرعي")]
        Miror = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMannerType")]
    public enum MannerType
    {
        [Description("واقف")]
        Dedicator = 2,
        [Description("اداره محل")]
        Office = 3,
        [Description("متولي")]
        Trustee = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMarriageBazlModatType")]
    public enum MarriageBazlModatType
    {
        [Description("بذل مدت توسط زوج انجام شده است")]
        ByHusband = 1,
        [Description("بذل مدت توسط زوجه به وکالت از طرف زوج انجام شده است")]
        ByWife = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMarriageEventType")]
    public enum MarriageEventType
    {
        [Description("بذل مدت")]
        BazlModat = 6,
        [Description("فسخ نکاح")]
        CancelMarriage = 5,
        [Description("طلاق")]
        Divorce = 2,
        [Description("ازدواج ")]
        Marriage = 1,
        [Description("صورتجلسه طلاق رجعي")]
        Minute = 8,
        [Description("رجوع به ما بذل")]
        Return = 3,
        [Description("رجوع به زوجيت")]
        ReturnToMarriage = 4,
        [Description("رونوشت طلاق")]
        Roonevesht = 7,
        [Description("رونوشت ازدواج")]
        RooneveshtMarriage = 9,
        [Description("")]
        None = 0
    }
    [Description("EnmMarriageState")]
    public enum MarriageState
    {
        [Description("معيل")]
        HasWifeChild = 3,
        [Description("متاهل")]
        Married = 2,
        [Description("مجرد")]
        Single = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMarriageType")]
    public enum MarriageType
    {
        [Description("دائم")]
        Continus = 1,
        [Description("منقطع")]
        Discrete = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMeasurementUnitType")]
    public enum MeasurementUnitType
    {
        [Description("مساحت")]
        Area = 2,
        [Description("طول")]
        Length = 1,
        [Description("پول")]
        Money = 3,
        [Description("ساير")]
        Other = 30,
        [Description("زمان")]
        Time = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmMechanizedFieldType")]
    public enum MechanizedFieldType
    {
        [Description("آبجکت جيسوني مدرک مکانيزه مربوطه که مدرک توسط کاربر مشخص مي شود")]
        JSON4Doc = 5,
        [Description("قلم اطلاعاتي که از مدرک مکانيزه مربوطه بصورت اتوماتيک مقداردهي مي شود بجز فايل چاپي مدرک")]
        MechanizedField = 4,
        [Description("فايل مدرک غيرمکانيزه")]
        NonMechanizedDocFile = 1,
        [Description("قلم اطلاعاتي غيرمکانيزه که بصورت دستي وارد مي شود بجز تصاوير و فايل ها")]
        NonMechanizedField = 3,
        [Description("فايل چاپي يک مدرک مکانيزه ذيل پرونده(يا مدرک اصلي مشابه آن) که مدرک توسط کاربر مشخص مي شود")]
        PrintFile4Doc = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMechanizedLetterCaseLocation")]
    public enum MechanizedLetterCaseLocation
    {
        [Description("مقصد")]
        Destination = 2,
        [Description("مبداء")]
        Source = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMechanizedLetterMainRcvOrTranscriptRcv")]
    public enum MechanizedLetterMainRcvOrTranscriptRcv
    {
        [Description("گيرنده اصل نامه")]
        MainReceiver = 1,
        [Description("رونوشت گيرنده")]
        TranscriptReceiver = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMechanizedLetterOrgTypeReceiver")]
    public enum MechanizedLetterOrgTypeReceiver
    {
        [Description("هم درون سازماني هم برون سازماني")]
        InnerAndOuterOrg = 3,
        [Description("مخصوص درون سازماني")]
        InnerOrg = 1,
        [Description("مخصوص برون سازماني")]
        OuterOrg = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMechanizedLetterReceiverType")]
    public enum MechanizedLetterReceiverType
    {
        [Description("درون سازماني")]
        InnerOrg = 1,
        [Description("برون سازماني")]
        OutterOrg = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMechanizedLetterRelLetterGetType")]
    public enum MechanizedLetterRelLetterGetType
    {
        [Description("مکانيزه")]
        Mechanize = 2,
        [Description("دستي")]
        NonMechanize = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMechanizedLetterRelType")]
    public enum MechanizedLetterRelType
    {
        [Description("بازگشت")]
        Comeback = 1,
        [Description("پيرو")]
        Follower = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMechanizedLetterSearchItem")]
    public enum MechanizedLetterSearchItem
    {
        [Description("تمامي نامه ها")]
        AllLeters = 1,
        [Description("نامه هاي واحد مربوطه")]
        CurrentOrganizationLetters = 2,
        [Description("نامه هاي کاربر جاري")]
        CurrentUserLetters = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmMechanizeLetterActionLevel")]
    public enum MechanizeLetterActionLevel
    {
        [Description("تأييد کننده")]
        Confimer = 3,
        [Description("تهيه کننده")]
        Creator = 1,
        [Description("ارجاع کننده")]
        Refferer = 4,
        [Description("ارسال کننده")]
        Sender = 2,
        [Description("رويت کننده")]
        Viewer = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmMechanizeLetterFieldSourceType")]
    public enum MechanizeLetterFieldSourceType
    {
        [Description("پرونده(يا مدرک اصلي يا مشابه آن)")]
        InCase = 2,
        [Description("مدرک در ارتباط به پرونده")]
        InDoc = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMechanizeRefererType")]
    public enum MechanizeRefererType
    {
        [Description("کارمند")]
        Employee = 2,
        [Description("سمت")]
        PostRole = 3,
        [Description("دفترخانه")]
        Scriptorium = 4,
        [Description("واحد سازماني")]
        Unit = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMechanizLetTypeSenderReceiver")]
    public enum MechanizLetTypeSenderReceiver
    {
        [Description("مخصوص گيرنده دفترخانه ازدواج و طلاق")]
        MarrageReceiver = 6,
        [Description("مخصوص فرستنده دفترخانه ازدواج و طلاق")]
        MarrageSender = 5,
        [Description("مخصوص گيرنده دفترخانه اسناد رسمي")]
        NotaryReceiver = 4,
        [Description("مخصوص فرستنده دفترخانه اسناد رسمي")]
        NotarySender = 3,
        [Description("مخصوص براي گيرنده از نوع واحد ثبتي")]
        ReceiverType = 2,
        [Description("موضوع براي فرستنده از نوع واحد ثبتي")]
        SenderType = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMenuItemState")]
    public enum MenuItemState
    {
        [Description("غيرفعال")]
        Disable = 2,
        [Description("فعال")]
        Enable = 1,
        [Description("پنهان")]
        Hide = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmMessageForceType")]
    public enum MessageForceType
    {
        [Description("عادي")]
        Normal = 3,
        [Description("فوري")]
        Urgent = 2,
        [Description("خيلي فوري")]
        VeryUrgent = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMessageRecieverType")]
    public enum MessageRecieverType
    {
        [Description("كاربر")]
        Employee = 1,
        [Description("واحد ثبتي")]
        Unit = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMessageSecurityType")]
    public enum MessageSecurityType
    {
        [Description("رييس واحد يا ناظر")]
        Boss = 2,
        [Description("مدير سيستم")]
        SystemManager = 3,
        [Description("ساير افراد")]
        Normal = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMessageState")]
    public enum MessageState
    {
        [Description("ارسال نشده")]
        NotSend = 1,
        [Description("ارسال شده")]
        Sended = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMilitaryState")]
    public enum MilitaryState
    {
        [Description("معافيت کفالت")]
        BailmentExemption = 3,
        [Description("خريد خدمت")]
        Buying = 5,
        [Description("معافيت تحصيلي")]
        EducationalExepmtion = 2,
        [Description("پايان خدمت")]
        Finished = 1,
        [Description("معافيت پزشکي")]
        MedicalExepmtion = 4,
        [Description("معافيت رهبري")]
        Rahbar = 6,
        [Description("")]
        None = 0
    }
    [Description("EnmMLetterSendTYpe")]
    public enum MLetterSendTYpe
    {
        [Description("ارجاع")]
        Refer = 1,
        [Description("بازگشت")]
        Return = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMNotaryAddressType")]
    public enum MNotaryAddressType
    {
        [Description("شهري")]
        City = 1,
        [Description("روستايي")]
        Village = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMNotaryDadehAmaeeState")]
    public enum MNotaryDadehAmaeeState
    {
        [Description("اطلاعات واقعه داده آمايي شده توسط دفترخانه باطل شده است")]
        Canceled = 3,
        [Description("اطلاعات واقعه داده آمايي شده توسط اداره ثبت تأييد شده است")]
        Confirmed = 9,
        [Description("اطلاعات واقعه داده آمايي شده در دفترخانه ثبت شده است")]
        Created = 1,
        [Description("اطلاعات واقعه داده آمايي شده توسط اداره ثبت رد شده است")]
        Rejected = 8,
        [Description("اطلاعات واقعه داده آمايي شده به اداره ثبت براي تأييد ارسال شده است")]
        Sent = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMNotaryDocumentGetter")]
    public enum MNotaryDocumentGetter
    {
        [Description("طلاق دهنده")]
        Divorcer = 3,
        [Description("قائم مقام قانوني طلاق دهنده")]
        DivorcerAgent = 5,
        [Description("زوج ")]
        Husband = 2,
        [Description("زوجه ")]
        Wife = 1,
        [Description("قائم مقام قانوني زوجه")]
        WifeAgent = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmMNotaryEventRegisterState")]
    public enum MNotaryEventRegisterState
    {
        [Description("باطل شده")]
        Canceled = 3,
        [Description("بي اثر شده")]
        CanceledAfterGetCode = 8,
        [Description("هزينه ها پرداخت شده است")]
        CostPaid = 5,
        [Description("ثبت نهايي شده")]
        FixedRegistered = 2,
        [Description("اصلاح پس از ثبت نهايي")]
        Modified = 4,
        [Description("سند نهايي چاپ شده است")]
        Printed = 7,
        [Description("شناسه يکتا گرفته است")]
        SetNationalDocumentNo = 6,
        [Description("ثبت اوليه شده")]
        TemporalRegistered = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMNotaryImmovableType")]
    public enum MNotaryImmovableType
    {
        [Description("انبـاري")]
        Anbari = 15,
        [Description("آپارتمان")]
        Apartment = 11,
        [Description("بـاغ")]
        Bagh = 31,
        [Description("باغچه")]
        Baghcheh = 32,
        [Description("كارگاه")]
        Kargah = 16,
        [Description("كارخانه")]
        Karkhaneh = 17,
        [Description("خانه")]
        Khaneh = 12,
        [Description("مغازه")]
        Maghazeh = 14,
        [Description("ساختمان")]
        Sakhteman = 13,
        [Description("ساير املاك")]
        SayerAmlaak = 41,
        [Description("زمين با بناي احداثي")]
        ZaminBaBana = 28,
        [Description("زمين باير")]
        ZaminBayer = 24,
        [Description("زمين محصور")]
        ZaminMahsour = 22,
        [Description("زمين مرتع")]
        ZaminMartae = 27,
        [Description("زمين موات")]
        ZaminMavaat = 26,
        [Description("زمين مزروعي")]
        ZaminMazroei = 21,
        [Description("زمين ملي")]
        ZaminMelli = 25,
        [Description("زمين مشجر")]
        ZaminMoshajar = 23,
        [Description("ساير انواع زمين")]
        ZaminSayer = 29,
        [Description("")]
        None = 0
    }
    [Description("EnmMNotaryIntroductionLetterState")]
    public enum MNotaryIntroductionLetterState
    {
        [Description("بـاطل شده")]
        Canceled = 4,
        [Description("تنظيم شده")]
        Created = 1,
        [Description("چاپ شده")]
        Printed = 2,
        [Description("پـاسخ داده شده")]
        Responded = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmMNotaryModifyReqState")]
    public enum MNotaryModifyReqState
    {
        [Description("بـاطــل شــده")]
        Canceled = 3,
        [Description("تأييد شده توسط اداره کل استان")]
        ConfirmedByOstan = 6,
        [Description("تأييد شده توسط دفترخانه")]
        ConfirmedByScriptorium = 8,
        [Description("تأييد شده توسط حوزه ثبتي")]
        ConfirmedByUnit = 4,
        [Description("تنظيم شده")]
        Created = 1,
        [Description("رد شده توسط اداره کل استان")]
        RejectedByOstan = 7,
        [Description("رد شده توسط حوزه ثبتي")]
        RejectedByUnit = 5,
        [Description("ارسال شده به حوزه ثبتي")]
        Sent2Unit = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMNotaryNamePrefix")]
    public enum MNotaryNamePrefix
    {
        [Description("بانو")]
        Mrs = 2,
        [Description("دوشيزه")]
        Ms = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMNotaryNoticeState")]
    public enum MNotaryNoticeState
    {
        [Description("ابـطـال شـده")]
        Canceled = 8,
        [Description("تأييد شده")]
        Confirmed = 9,
        [Description("تنظيـم شده")]
        Created = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmMNotarySend2SabtAhvalState")]
    public enum MNotarySend2SabtAhvalState
    {
        [Description("ارسال  نشده")]
        NotSent = 1,
        [Description("ارسال شده و دريافت بازخورد از ثبت احوال")]
        SendAndGetFeedback = 3,
        [Description("ارسال شده و منتظر دريافت بازخورد از ثبت احوال")]
        SendAndWait = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMNotarySendToNocrState")]
    public enum MNotarySendToNocrState
    {
        [Description("ارسال ناموفق")]
        FailedSend = 3,
        [Description("جديد")]
        New = 6,
        [Description("ارسال موفق بدون وقايع قبلي")]
        PartialSuccess = 4,
        [Description("آماده ارسال")]
        ReadyToSend = 1,
        [Description("ارسال موفق")]
        SuccessfulSend = 2,
        [Description("غير قابل ارسال")]
        UnSendable = 9,
        [Description("")]
        None = 0
    }
    [Description("EnmMobileNoState")]
    public enum MobileNoState
    {
        [Description("شماره دريافتي از ثنا")]
        GetFromSana = 1,
        [Description("شماره بنام شخص ديگري است")]
        IsNotOwner = 3,
        [Description("شماره بنام خود شخص است")]
        IsOwner = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmMocState")]
    public enum MocState
    {
        [Description("اثر انگشت مطابقت دارد")]
        FingerprintMatched = 2,
        [Description("اثر انگشت مطابقت ندارد")]
        FingerprintNotMatched = 3,
        [Description("انجام نـشـده")]
        NotDone = 1,
        [Description("پين مطابقت دارد")]
        PinMatched = 4,
        [Description("پين مطابقت ندارد")]
        PinNotMatched = 5,
        [Description("با وجود عدم تطابق اثر انگشت، ادامه فرآيند انجام شود")]
        VetoFingerprintNotMatched = 6,
        [Description("با وجود عدم تطابق پين، ادامه فرآيند انجام شود")]
        VetoPinNotMatched = 7,
        [Description("")]
        None = 0
    }
    [Description("EnmMonth")]
    public enum Month
    {
        [Description("آبان")]
        Aban = 8,
        [Description("آذر")]
        Azar = 9,
        [Description("بهمن")]
        Bahman = 11,
        [Description("دي")]
        Day = 10,
        [Description("اسفند")]
        Esfand = 12,
        [Description("فروردين")]
        Farvardin = 1,
        [Description("خرداد")]
        Khordad = 3,
        [Description("مهر")]
        Mehr = 7,
        [Description("مرداد")]
        Mordad = 5,
        [Description("ارديبهشت")]
        Ordibehesht = 2,
        [Description("شهريور")]
        Shahrivar = 6,
        [Description("تير")]
        Tir = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmMultiSessionPolicy")]
    public enum MultiSessionPolicy
    {
        [Description("شناسه کاربري با اين نوع نقش بتواند روي چند کامپيوتر بطور همزمان وصل شود")]
        Optimistic = 1,
        [Description("با اتصال يک شناسه کاربري با اين نوع نقش، ابتدا همه اتصالات قبلي روي ساير کامپيوترها قطع شده، سپس اين اتصال برقرار شود")]
        Pessimistic = 3,
        [Description("با اتصال يک شناسه کاربري با اين نوع نقش، پس از هشدار، همه اتصالات قبلي روي ساير کامپيوترها قطع شده، سپس اين اتصال برقرار شود")]
        SemiPessimistic = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNAJAUnitType")]
    public enum NAJAUnitType
    {
        [Description("شعبه مرکزي")]
        CentralBranch = 6,
        [Description("اداره کل")]
        HighOffice = 2,
        [Description("اداره")]
        Office = 3,
        [Description("سازمان")]
        Org = 1,
        [Description("واحد")]
        Unit = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmNameDetermineReqType")]
    public enum NameDetermineReqType
    {
        [Description("تغيير نام")]
        NameChanged = 2,
        [Description("تعيين نام")]
        NameDetermine = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNameSearchType")]
    public enum NameSearchType
    {
        [Description("ترکيب تمامي کلمات")]
        AndSearch = 2,
        [Description("مطابق عبارت وارد شده")]
        EqualSearch = 1,
        [Description("ترکيب تک تک کلمات")]
        OrSearch = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNameState")]
    public enum NameState
    {
        [Description("تأييد ")]
        Confirm = 1,
        [Description("رد")]
        Reject = 2,
        [Description("رد نام به دليل رد درخواست تغيير/تاسيس")]
        RejectByRejectReq = 4,
        [Description("در انتظار بررسي")]
        WateingForCheking = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNationalityType")]
    public enum NationalityType
    {
        [Description("اتباع بيگانه")]
        Foreign = 2,
        [Description("ايراني")]
        Iranian = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNeedFollowUp")]
    public enum NeedFollowUp
    {
        [Description(" دارد ")]
        Have = 1,
        [Description(" ندارد ")]
        NoHave = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNewspaperCertificateType")]
    public enum NewspaperCertificateType
    {
        [Description("الکترونيک - غير برخط")]
        Offline = 3,
        [Description("الکترونيک - برخط")]
        Online = 2,
        [Description("چاپي")]
        Printed = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNewsPaperGroup")]
    public enum NewsPaperGroup
    {
        [Description("روزنامه رسمي")]
        LegalNewspaper = 2,
        [Description("روزنامه کثيرالانتشار")]
        WidelyPublishedNewspaper = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNewspaperGroupLevel")]
    public enum NewspaperGroupLevel
    {
        [Description("گروه پر هزينه ")]
        HighCostCategory = 2,
        [Description("گروه کم هزينه")]
        LowCostCategory = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNewspaperPeriod")]
    public enum NewspaperPeriod
    {
        [Description("سالانه")]
        Annually = 9,
        [Description("روزنامه")]
        Daily = 1,
        [Description("ماهنامه")]
        Monthly = 5,
        [Description("پايگاه خبري")]
        NewsSite = 11,
        [Description("خبرگزاري")]
        Press = 10,
        [Description("فصلنامه")]
        Quarterly = 7,
        [Description("دو ماهنامه")]
        TowMonthly = 6,
        [Description("دو فصلنامه")]
        TowQuarterly = 8,
        [Description("دو هفته نامه")]
        TowWeekly = 4,
        [Description("دو شماره در هفته")]
        TwoInWeek = 2,
        [Description("هفته نامه")]
        Weekly = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNewspaperPrintArea")]
    public enum NewspaperPrintArea
    {
        [Description("سراسري")]
        InCountry = 1,
        [Description("استاني")]
        InOstan = 2,
        [Description("محلي")]
        Local = 4,
        [Description("منطقه اي")]
        Regional = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNewspaperQuestionType")]
    public enum NewspaperQuestionType
    {
        [Description("تشريحي")]
        Narrative = 2,
        [Description("انتخابي")]
        Selective = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNewspaperType")]
    public enum NewspaperType
    {
        [Description("علي البدل")]
        Exchange = 2,
        [Description("اصلي")]
        Main = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryAbsentType")]
    public enum NotaryAbsentType
    {
        [Description("غيبت")]
        Absent = 3,
        [Description("بازداشت")]
        Arrest = 2,
        [Description("فـوت")]
        Death = 1,
        [Description("ساير")]
        Other = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryActivityType")]
    public enum NotaryActivityType
    {
        [Description("ازدواج و طلاق")]
        Divorce = 2,
        [Description("اسناد رسمي")]
        Documents = 3,
        [Description("ازدواج")]
        Marriage = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryAlphabetLetter")]
    public enum NotaryAlphabetLetter
    {
        [Description("ع")]
        Ain = 21,
        [Description("الف")]
        Alef = 1,
        [Description("ب")]
        Be = 2,
        [Description("چ")]
        Che = 7,
        [Description("د")]
        Dal = 10,
        [Description("ف")]
        Fe = 23,
        [Description("گ")]
        Gaf = 26,
        [Description("ق")]
        Ghaf = 24,
        [Description("غ")]
        Ghain = 22,
        [Description("ح")]
        He = 8,
        [Description("ه")]
        Hee = 31,
        [Description("ژ")]
        Jhe = 14,
        [Description("ج")]
        Jim = 6,
        [Description("ک")]
        Kaf = 25,
        [Description("خ")]
        Khe = 9,
        [Description("ل")]
        Lam = 27,
        [Description("م")]
        Mim = 28,
        [Description("ن")]
        Non = 29,
        [Description("پ")]
        Pe = 3,
        [Description("ر")]
        Re = 12,
        [Description("ص")]
        Sad = 17,
        [Description("ث")]
        Se = 5,
        [Description("ش")]
        Shin = 16,
        [Description("س")]
        Sin = 15,
        [Description("ط")]
        Ta = 19,
        [Description("ت")]
        Te = 4,
        [Description("و ")]
        Vav = 30,
        [Description("ي")]
        Ye = 32,
        [Description("ظ")]
        Za = 20,
        [Description("ض")]
        Zad = 18,
        [Description("ذ")]
        Zal = 11,
        [Description("ز")]
        Ze = 13,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryAppoinementState")]
    public enum NotaryAppoinementState
    {
        [Description("باتصدي اصيل")]
        Asil = 2,
        [Description("بلاتصدي")]
        Free = 1,
        [Description("باتصدي کفيل")]
        Kafil = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryBookType")]
    public enum NotaryBookType
    {
        [Description("مکرر اول")]
        Mokarar1 = 2,
        [Description("مکرر دوم")]
        Mokarar2 = 3,
        [Description("متمم")]
        Motamam = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryCompanyType")]
    public enum NotaryCompanyType
    {
        [Description("موسسه غير تجاري")]
        GheyreTejari = 9,
        [Description("نمايندگي شرکت خارجي")]
        KharejiNamayandegi = 12,
        [Description("شعبه شرکت خارجي")]
        KharejiShoebeh = 11,
        [Description("شركت با مسئوليت محدود")]
        MasoliatMahdod = 1,
        [Description("شركت مختلط غيرسهامي")]
        MokhtaletGheyreSahami = 6,
        [Description("شركت مختلط سهامي")]
        MokhtaletSahami = 5,
        [Description("شركت نسبي")]
        Nesbi = 4,
        [Description("شركت سهامي عام")]
        SahamiAam = 7,
        [Description("شركت سهامي خاص")]
        SahamiKhas = 2,
        [Description("شركت تعاوني")]
        Taavoni = 8,
        [Description("شركت تعاوني سهامي عام")]
        TaavoniSahamiAam = 13,
        [Description("شركت تضامني")]
        Tazamoni = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryConditionType")]
    public enum NotaryConditionType
    {
        [Description("AND")]
        AND = 1,
        [Description("NOT")]
        NOT = 3,
        [Description("OR")]
        OR = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryCostPayType")]
    public enum NotaryCostPayType
    {
        [Description("فيش بانکي")]
        BankFiche = 2,
        [Description("کارت خوان")]
        Pos = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryDisAppointmentReason")]
    public enum NotaryDisAppointmentReason
    {
        [Description("بازنشستگي سردفتر")]
        Bazneshastegi = 4,
        [Description("انفصال دائم سردفتر")]
        Enfesal = 3,
        [Description("انتقال سردفتر")]
        Enteghal = 2,
        [Description("استعفا سردفتر")]
        Estefa = 1,
        [Description("فوت سردفتر")]
        Fout = 6,
        [Description("لغو ابلاغ سردفتر")]
        LaghveEblagh = 7,
        [Description("معافيت از اشتغال سردفتر")]
        MoaafAzEshteghal = 8,
        [Description("سلب صلاحيت سردفتر")]
        SalbeSalaheyat = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryDocClassifyNoCounterResetType")]
    public enum NotaryDocClassifyNoCounterResetType
    {
        [Description("با رسيدن به حداکثر مقدار")]
        PerMaxValue = 3,
        [Description("در شروع هر ماه")]
        PerMonth = 2,
        [Description("در شروع هر سال")]
        PerYear = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryDocClassifyNoSectionType")]
    public enum NotaryDocClassifyNoSectionType
    {
        [Description("حروف الفبا")]
        Alphabet = 3,
        [Description("شمارنده")]
        Counter = 5,
        [Description("ماه")]
        Month = 2,
        [Description("عدد ثابت")]
        Number = 4,
        [Description("سال")]
        Year = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryDocContentType")]
    public enum NotaryDocContentType
    {
        [Description("مختصر")]
        Brief = 1,
        [Description("بي اهميت")]
        DontCare = 3,
        [Description("مفصل")]
        Long = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryDocumentState")]
    public enum NotaryDocumentState
    {
        [Description("سند ثبت قطعي شده است")]
        Approved = 3,
        [Description("سند پس از تنظيم باطل شد")]
        Canceled = 4,
        [Description("سند تنظيم شده است")]
        Created = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryDocVerificationLevel")]
    public enum NotaryDocVerificationLevel
    {
        [Description("کليات سند")]
        AtAGlance = 1,
        [Description("جزئيات سند")]
        Details = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryDocVerificationRequesterType")]
    public enum NotaryDocVerificationRequesterType
    {
        [Description("دفترخانه اسناد رسمي")]
        Notary = 2,
        [Description("مردم عادي")]
        People = 1,
        [Description("واحد ثبتي")]
        SSAAUnit = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryDocVerificationState")]
    public enum NotaryDocVerificationState
    {
        [Description("پاسخ داده شده")]
        Replied = 3,
        [Description("درخواست شده")]
        Requested = 1,
        [Description("در حال بررسي")]
        Verifing = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryEArchiveDocumentState")]
    public enum NotaryEArchiveDocumentState
    {
        [Description("تأييد شده")]
        Confirmed = 2,
        [Description("ايجاد شده")]
        Created = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryEditRecordState")]
    public enum NotaryEditRecordState
    {
        [Description("ويرايش شده")]
        Edited = 1,
        [Description("آماده ارسال به اداره کل امور اسناد")]
        Ready2OmorAsnad = 2,
        [Description("آماده بروز رساني در سامانه هاي ثبت الکترونيک اسناد")]
        Ready2Synchronize = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryEmployeeDeathState")]
    public enum NotaryEmployeeDeathState
    {
        [Description("بـاطـل شـده")]
        Canceled = 2,
        [Description("تاييد شده")]
        Confirmed = 3,
        [Description("ايجاد شده")]
        Created = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryEmployeePosition")]
    public enum NotaryEmployeePosition
    {
        [Description("دفتريار اول")]
        NotaryFirstAssistant = 2,
        [Description("دفتريار دوم")]
        NotarySecondAssistant = 3,
        [Description("سندنويس")]
        SanadNevis = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryEmployeeResignState")]
    public enum NotaryEmployeeResignState
    {
        [Description("بـاطــل شده")]
        Canceled = 2,
        [Description("تـايـيـد شده")]
        Confirmed = 3,
        [Description("ايجاد شده")]
        Created = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryEmpPostOriginality")]
    public enum NotaryEmpPostOriginality
    {
        [Description("کفيل")]
        NotOriginal = 2,
        [Description("اصيل")]
        Original = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryEstateDocType")]
    public enum NotaryEstateDocType
    {
        [Description("کاداستري داراي شماره دفتر الکترونيک")]
        Cadaster = 1,
        [Description("کاداستري فاقد شماره دفتر الکترونيک")]
        CadasterWithoutNo = 3,
        [Description("دفترچه ‏اي")]
        Note = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryExcApplIssuingState")]
    public enum NotaryExcApplIssuingState
    {
        [Description("لـغو شـده")]
        Canceled = 4,
        [Description("پرونده ايجاد شده است")]
        Created = 1,
        [Description("چاپ گرفته شده است")]
        Printed = 2,
        [Description("اعلام نقص شده است")]
        ReModified = 5,
        [Description("تقاضانامه رفع نقص ارسال شده است")]
        ReSent = 6,
        [Description("به اداره اجرا ارسال شده است")]
        Sent = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryExcSrvReqState")]
    public enum NotaryExcSrvReqState
    {
        [Description("لغو شـده")]
        Canceled = 9,
        [Description("تنظيم شده")]
        Created = 1,
        [Description("رد شده توسط اداره اجرا")]
        Rejected = 7,
        [Description("پاسخ دريافت شده")]
        Respond = 2,
        [Description("ارسال شده به اداره اجرا")]
        Sent = 8,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryFingerprintDocType")]
    public enum NotaryFingerprintDocType
    {
        [Description("درخواست سند مالکيت")]
        DocReq = 3,
        [Description("سند رسمي")]
        Document = 1,
        [Description("تقاضانامه صدور اجرائيه")]
        ExecuteReq = 4,
        [Description("گواهي امضاء")]
        SignCertificate = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryFloatingModeType")]
    public enum NotaryFloatingModeType
    {
        [Description("سلب صلاحيت طلاق")]
        DisqualificationFromDivorced = 9,
        [Description("سلب صلاحيت ازدواج")]
        DisqualificationFromMarried = 10,
        [Description("انفصال طلاق")]
        DivorcedEnfesal = 1,
        [Description("کفالت طلاق")]
        DivorcedKefalat = 4,
        [Description("معلق طلاق")]
        FloatingFromDivorced = 5,
        [Description("معلق ازدواج")]
        FloatingFromMarried = 6,
        [Description("انفصال ازدواج")]
        MariiedEnfesal = 2,
        [Description("کفالت ازدواج")]
        MarriedKefalat = 3,
        [Description("مستعفي طلاق")]
        ResignedFromDivorced = 7,
        [Description("مستعفي ازدواج")]
        ResignedFromMarried = 8,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryGeneratedDealSummary")]
    public enum NotaryGeneratedDealSummary
    {
        [Description("خلاصه معامله ارسال نشده است")]
        NotSent = 2,
        [Description("خلاصه معامله ارسال شده است")]
        Sent = 1,
        [Description("محدوده زماني ارسال گذشته است")]
        TimedOut = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryGrandeeExceptionType")]
    public enum NotaryGrandeeExceptionType
    {
        [Description("ربعيه")]
        Robeyeh = 2,
        [Description("ثمنيه")]
        Somnyeh = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryHagheEntefae")]
    public enum NotaryHagheEntefae
    {
        [Description("حبـس")]
        Habs = 4,
        [Description("عمري")]
        Omra = 1,
        [Description("رقبي")]
        Roghba = 3,
        [Description("سکني")]
        Sokna = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryHokmType")]
    public enum NotaryHokmType
    {
        [Description("حکـم دادگاه")]
        Dadgah = 2,
        [Description("اجرائيه ثبت")]
        Ejra = 1,
        [Description("اصلاحات اراضي")]
        EslaahaateArazi = 4,
        [Description("کشت موقت")]
        KeshteMovaghat = 3,
        [Description("اداره امور مالياتي")]
        Maliyati = 6,
        [Description("تأمين اجتماعي")]
        TamineEjtemaei = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryImmovableLocationType")]
    public enum NotaryImmovableLocationType
    {
        [Description("اراضي")]
        Ground = 4,
        [Description("داخل محدوده شهرها")]
        InCity = 1,
        [Description("بافت مسكوني روستاها")]
        InVillage = 3,
        [Description("خارج از محدوده شهرها")]
        OutCity = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryImmovaleType")]
    public enum NotaryImmovaleType
    {
        [Description("دولتي")]
        Dolati = 4,
        [Description("حبس")]
        Habs = 3,
        [Description("ثلث")]
        Sols = 1,
        [Description("ثلث باقي")]
        SolsBaghi = 2,
        [Description("طلق")]
        Telgh = 5,
        [Description("طلق با عرصه دولتي - مسكن مهر")]
        TelghArsehDolati = 6,
        [Description("طلق با عرصه وقف")]
        TelghArsehVaghf = 7,
        [Description("طلق و وقف")]
        TelghVaghf = 8,
        [Description("وقف")]
        Vaghf = 9,
        [Description("وقف آستان قدس رضوي")]
        VaghfAstanRazavi = 10,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryInquiryFromUnitState")]
    public enum NotaryInquiryFromUnitState
    {
        [Description("ابـطـال شده")]
        Canceled = 2,
        [Description("تنظيـم شده است")]
        Created = 1,
        [Description("پاسخ داده شده است")]
        Respond = 4,
        [Description("ارسـال شده است")]
        Sent = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryInquiryState")]
    public enum NotaryInquiryState
    {
        [Description("بايگاني شده")]
        Archived = 5,
        [Description("ويرايش و ارسال شده")]
        EditedAndSent = 6,
        [Description("داراي پيام")]
        HasMessage = 4,
        [Description("هزينه پرداخت شده است")]
        PayCost = 20,
        [Description("ثبت  شده")]
        Registered = 1,
        [Description("پاسخ داده  شده")]
        Replied = 3,
        [Description("ارسال  شده")]
        Sent = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryIsDone")]
    public enum NotaryIsDone
    {
        [Description("انجام شده است")]
        IsDone = 1,
        [Description("انجام نـشده است")]
        IsNotDone = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryLeaveReqDetailsState")]
    public enum NotaryLeaveReqDetailsState
    {
        [Description("ابلاغ کفـالت صادر شده است")]
        BailEblaghIssued = 4,
        [Description("ايجاد شده است")]
        Created = 1,
        [Description("توسط اصيل تحويل داده شده است")]
        DeliveredByAsil = 5,
        [Description("توسط کفيل تحويل گرفته شده است")]
        GetByKafil = 6,
        [Description("عودت به اصيل توسط بازرس تاييد شده است")]
        ReturnByBazras = 8,
        [Description("عودت به اصيل توسط کفيل اعلام شده است")]
        ReturnByKafil = 7,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryLeaveReqState")]
    public enum NotaryLeaveReqState
    {
        [Description("ابلاغ کفالت صادر شده است")]
        BailEblaghIssued = 4,
        [Description("لغو شده")]
        Cancel = 9,
        [Description("تنظيم شده است")]
        Created = 1,
        [Description("دفترخانه توسط اصيل تحويل داده شده است")]
        DeliveredByAsil = 5,
        [Description("دفترخانه توسط کفيل تحويل گرفته شده است")]
        GetByKafil = 6,
        [Description("ابلاغ مرخصي صادر شده است")]
        LeaveEblaghIssued = 3,
        [Description("رد شـده")]
        Rejected = 10,
        [Description("عودت دفترخانه به اصيل توسط بازرس تاييد شده است")]
        ReturnByBazras = 8,
        [Description("عودت دفترخانه به اصيل توسط کفيل اعلام شده است")]
        ReturnByKafil = 7,
        [Description("به اداره ثبت ارسال شده است")]
        Sent = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryLeaveType")]
    public enum NotaryLeaveType
    {
        [Description("درخواست لغو مرخصي")]
        CancelRequest = 7,
        [Description("مرخصي سالانه")]
        EntitledDaysOfLeavesInYear = 8,
        [Description("استحقاقي")]
        Estehghaghi = 1,
        [Description("استعلاجي")]
        Estelaji = 2,
        [Description("تمديد مرخصي")]
        Extension = 5,
        [Description("خروج از کشور")]
        KhoroujAzKeshvar = 3,
        [Description("مانده مرخصي")]
        RemainingLeaveDays = 9,
        [Description("استعلاجي - بدون حضور سردفتر")]
        Remedial = 6,
        [Description("تحصيلي")]
        Tahsili = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryLegalPersonBaseType")]
    public enum NotaryLegalPersonBaseType
    {
        [Description("خصوصي")]
        Co = 2,
        [Description("دولتي")]
        Gov = 1,
        [Description("ساير")]
        Others = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryMinutesState")]
    public enum NotaryMinutesState
    {
        [Description("باطـل شـده")]
        Canceled = 4,
        [Description("تاييد شده")]
        Confirmed = 3,
        [Description("تنظيم شده")]
        Created = 1,
        [Description("ارسال شده به کارتابل تاييد")]
        ReadyToConfirm = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryNeedToReliableReason")]
    public enum NotaryNeedToReliableReason
    {
        [Description("اشخاص معلول بي دست و پا")]
        BiDastVaPa = 4,
        [Description("اشخاص بيمار")]
        Bimar = 3,
        [Description("اشخاص بي سواد")]
        BiSavad = 1,
        [Description("اشخاص کر، کور، گنگ بي سواد")]
        KarKourGongBisavad = 2,
        [Description("سردفتران و دفترياران و اقارب و خدمه آنها")]
        SardaftarDaftaryar = 6,
        [Description("اشخاص بدون اثر انگشت")]
        WithoutFingerprint = 7,
        [Description("زندانيان")]
        Zendanyan = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryNotificationAttachmentType")]
    public enum NotaryNotificationAttachmentType
    {
        [Description("حکم دادگاه")]
        Judgment = 1,
        [Description("صورتجلسه تحويل و تحول")]
        MOM = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryNotificationKind")]
    public enum NotaryNotificationKind
    {
        [Description("انتصاب")]
        Appointment = 1,
        [Description("تغيير وضعيت دفتريار")]
        AssistanceChangeState = 13,
        [Description("کفالت")]
        Bail = 2,
        [Description("بـرائت")]
        Beraat = 15,
        [Description("جريمه")]
        Jarimeh = 16,
        [Description("مرخصي")]
        Leave = 7,
        [Description("مجوز خاص")]
        License = 5,
        [Description("سياري")]
        Mobile = 6,
        [Description("سلب صلاحيت عملي")]
        Noscientific = 9,
        [Description("اشتغال مجدد")]
        Occupation = 8,
        [Description("انفصال دائم امور سردفتري")]
        PermanentDismissal = 12,
        [Description("استعفا")]
        Resignation = 4,
        [Description("بازنشستگي")]
        Retirement = 3,
        [Description("تعليق")]
        Suspension = 10,
        [Description("توبيخ")]
        Tobikh = 14,
        [Description("انفصال موقت امور سردفتري")]
        Tseparation = 11,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryNotificationState")]
    public enum NotaryNotificationState
    {
        [Description("باطل شـده")]
        Canceled = 4,
        [Description("تاييد شده")]
        Confirmed = 3,
        [Description("تنظيم شده")]
        Created = 1,
        [Description("ارسال شده به کارتابل تاييد")]
        ReadyToConfirm = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryOccupationState")]
    public enum NotaryOccupationState
    {
        [Description("غيبت")]
        AbsentPerson = 53,
        [Description("بازداشت  ")]
        ArrestedPerson = 52,
        [Description("فوت")]
        Dead = 16,
        [Description("انفصال موقت")]
        Discharge = 2,
        [Description("انفصال دائم")]
        DischargeContinual = 12,
        [Description("انفصال موقت طلاق")]
        DischargeContinualFromDivorce = 17,
        [Description("انفصال موقت از طلاق و انفصال موقت از ازدواج")]
        DischargeContinualFromDivorceAndDischargeContinualFromMarriage = 23,
        [Description("انفصال موقت از طلاق و انفصال دائم از ازدواج")]
        DischargeContinualFromDivorceAndDischargeFromMarriage = 24,
        [Description("انفصال موقت طلاق و سلب صلاحيت ازدواج")]
        DischargeContinualFromDivorceAndDisqualificationFromMarriage = 50,
        [Description("انفصال موقت طلاق  و معلق ازدواج")]
        DischargeContinualFromDivorceAndFloatingFromMarriage = 36,
        [Description("انفصال موقت طلاق  و مستعفي ازدواج")]
        DischargeContinualFromDivorceAndResignedFromMarriage = 44,
        [Description("انفصال موقت ازدواج")]
        DischargeContinualFromMarriage = 18,
        [Description("انفصال دائم طلاق")]
        DischargeFromDivorce = 19,
        [Description("انفصال دائم از طلاق و انفصال موقت از ازدواج")]
        DischargeFromDivorceAndDischargeContinualFromMarriage = 22,
        [Description("انفصال دائم از طلاق و انفصال دائم از ازدواج")]
        DischargeFromDivorceAndDischargeFromMarriage = 21,
        [Description("انفصال دائم طلاق و سلب صلاحيت ازدواج")]
        DischargeFromDivorceAndDisqualificationFromMarriage = 51,
        [Description("انفصال دائم طلاق و معلق ازدواج   ")]
        DischargeFromDivorceAndFloatingFromMarriage = 37,
        [Description("انفصال دائم طلاق و مستعفي ازدواج")]
        DischargeFromDivorceAndResignedFromMarriage = 45,
        [Description("انفصال دائم ازدواج")]
        DischargeFromMarriage = 20,
        [Description("سلب صلاحيت")]
        Disqualification = 11,
        [Description("سلب صلاحيت از طلاق")]
        DisqualificationFromDivorce = 29,
        [Description("سلب صلاحيت طلاق و انفصال موقت ازدواج")]
        DisqualificationFromDivorceAndDischargeContinualFromMarriage = 47,
        [Description("سلب صلاحيت طلاق و انفصال دائم ازدواج")]
        DisqualificationFromDivorceAndDischargeFromMarriage = 48,
        [Description("سلب صلاحيت طلاق و سلب صلاحيت ازدواج")]
        DisqualificationFromDivorceAndDisqualificationFromMarriage = 46,
        [Description("سلب صلاحيت طلاق و معلق ازدواج  ")]
        DisqualificationFromDivorceAndFloatingFromMarriage = 39,
        [Description("سلب صلاحيت طلاق و مستعفي ازدواج")]
        DisqualificationFromDivorceAndResignedFromMarriage = 49,
        [Description("سلب صلاحيت از ازدواج")]
        DisqualificationFromMarriage = 30,
        [Description("مرخصي تحصيلي")]
        Education = 6,
        [Description("معذوريت از حضور در محل دفترخانه")]
        Excuse = 8,
        [Description("معافيت از اشتغال")]
        ExemptionWorking = 13,
        [Description("معلق")]
        Floating = 3,
        [Description("معلق از طلاق")]
        FloatingFromDivorce = 25,
        [Description("معلق طلاق و انفصال موقت ازدواج")]
        FloatingFromDivorceAndDischargeContinualFromMarriage = 32,
        [Description("معلق طلاق و انفصال دائم ازدواج")]
        FloatingFromDivorceAndDischargeFromMarriage = 33,
        [Description("معلق طلاق و سلب صلاحيت ازدواج")]
        FloatingFromDivorceAndDisqualificationFromMarriage = 35,
        [Description("معلق طلاق و معلق ازدواج")]
        FloatingFromDivorceAndFloatingFromMarried = 31,
        [Description("معلق طلاق و مستعفي ازدواج")]
        FloatingFromDivorceAndResignedFromMarriage = 34,
        [Description("معلق از ازدواج")]
        FloatingFromMarriage = 26,
        [Description("مرخصي استحقاقي خروج از کشور")]
        LeaveAbroad = 7,
        [Description("مرخصي استعلاجي")]
        LeaveSick = 5,
        [Description("مرخصي استحقاقي")]
        LeavrJustly = 4,
        [Description("لغو ابلاغ ماده 15")]
        LiftNotification15 = 15,
        [Description("لغو ابلاغ ماده 74")]
        LiftNotification74 = 14,
        [Description("مستعفي")]
        Resigned = 10,
        [Description("مستعفي از طلاق")]
        ResignedFromDivorce = 27,
        [Description("مستعفي طلاق و انفصال موقت ازدواج")]
        ResignedFromDivorceAndDischargeContinualFromMarriage = 41,
        [Description("مستعفي طلاق و انفصال دائم ازدواج")]
        ResignedFromDivorceAndDischargeFromMarriage = 42,
        [Description("مستعفي طلاق و سلب صلاحيت ازدواج")]
        ResignedFromDivorceAndDisqualificationFromMarriage = 43,
        [Description("مستعفي طلاق و معلق ازدواج   ")]
        ResignedFromDivorceAndFloatingFromMarriage = 38,
        [Description("مستعفي طلاق و مستعفي ازدواج")]
        ResignedFromDivorceAndResignedFromMarriage = 40,
        [Description("مستعفي از ازدواج")]
        ResignedFromMarriage = 28,
        [Description("بازنشسته")]
        Retirement = 9,
        [Description("شاغل")]
        Working = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryOperatorType")]
    public enum NotaryOperatorType
    {
        [Description("مساوي باشد با")]
        EqualTo = 1,
        [Description("بزرگتر يا مساوي باشد از")]
        GreaterThanOrEqualTo = 6,
        [Description("بزرگتر باشد از")]
        GreaterThanTo = 5,
        [Description("آيا اولين رکورد است؟")]
        IsFirstRecord = 10,
        [Description("آيا آخرين رکورد است؟")]
        IsLastRecord = 11,
        [Description("کوچکتر يا مساوي باشد با")]
        LessThanOrEqualTo = 4,
        [Description("کوچکتر باشد از")]
        LessThanTo = 3,
        [Description("شبيه باشد به")]
        LikeTo = 7,
        [Description("مخالف باشد با")]
        NotEqualTo = 2,
        [Description("از انتها شبيه باشد به")]
        PostLikeTo = 9,
        [Description("از ابتدا شبيه باشد به")]
        PreLikeTo = 8,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryOtherPaymentsSubject")]
    public enum NotaryOtherPaymentsSubject
    {
        [Description("بقاياي ثبتي")]
        Baghaya = 4,
        [Description("استعلام ثبت شرکت ها")]
        Company = 6,
        [Description("کپي برابر اصل اسناد مالکيت")]
        EstateDoc = 1,
        [Description("کپي برابر اصل اسناد دفترخانه")]
        NotaryDoc = 2,
        [Description("کپي برابر اصل ساير مدارک")]
        OtherDoc = 3,
        [Description("استعلام املاک جاري")]
        UnregisteredEstate = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryOwnershipDocumentType")]
    public enum NotaryOwnershipDocumentType
    {
        [Description("بنچاق دفترخانه")]
        NotaryDocument = 2,
        [Description("سند مالکيت")]
        SabtDocument = 1,
        [Description("گزارش وضعيت ثبتي")]
        SabtStateReport = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryOwnershipType")]
    public enum NotaryOwnershipType
    {
        [Description("مفروز")]
        Mafroz = 1,
        [Description("مشاع")]
        Moshae = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryPageOrientation")]
    public enum NotaryPageOrientation
    {
        [Description("Landscape")]
        Landscape = 2,
        [Description("Portrait")]
        Portrait = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryPaperInfoMdfyReqState")]
    public enum NotaryPaperInfoMdfyReqState
    {
        [Description("درخواست مورد رسيدگي قرار گرفت")]
        ResidegiShod = 3,
        [Description("درخواست ثبت شد")]
        SabtShod = 1,
        [Description("ارسال شد")]
        Sent = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryPaperInfoMdfyState")]
    public enum NotaryPaperInfoMdfyState
    {
        [Description("پذيرفته شد")]
        Confirmed = 1,
        [Description("رد شد")]
        Rejected = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryPaperReqState")]
    public enum NotaryPaperReqState
    {
        [Description("درخواست پذيرفته شد")]
        Accepted = 1,
        [Description("برگه ها چاپ شد")]
        Printed = 4,
        [Description("درخواست پذيرفته نشد")]
        Rejected = 3,
        [Description("با بخشي از درخواست موافقت شد")]
        SemiAccepted = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryPaperState")]
    public enum NotaryPaperState
    {
        [Description("استفاده شده براي چاپ سند")]
        DocPrinted = 2,
        [Description("مفقودي")]
        Lost = 4,
        [Description("ابطال به علت اشکال در چاپ")]
        PrintDamaged = 6,
        [Description("چاپ شده - آماده بهره برداري")]
        Printed = 7,
        [Description("مسروقـه")]
        Stolen = 5,
        [Description("امحاء شده")]
        Torn = 3,
        [Description("توليد شده - آماده چاپ")]
        White = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryPaperStateBrief")]
    public enum NotaryPaperStateBrief
    {
        [Description("ابطـال شـده")]
        Invalid = 1,
        [Description("استفاده شده براي چاپ سـند")]
        Used = 2,
        [Description("خام")]
        White = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryPaperStateModify")]
    public enum NotaryPaperStateModify
    {
        [Description("اصلاح اطلاعات سند")]
        DocEdited = 9,
        [Description("استفاده شده براي چاپ سند")]
        DocPrinted = 2,
        [Description("مفقودي")]
        Lost = 4,
        [Description("ابطـال به علت اشکال در چاپ")]
        PrintDamaged = 6,
        [Description("برگ استفاده نشده - خام")]
        Printed = 7,
        [Description("مـسـروقه")]
        Stolen = 5,
        [Description("امحـاء شده")]
        Torn = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryPaperType")]
    public enum NotaryPaperType
    {
        [Description("سند رسمي")]
        LawDocument = 1,
        [Description("خلاصه معامله املاک در جريان ثبت")]
        SaleBrief = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryProhibitedPersonType")]
    public enum NotaryProhibitedPersonType
    {
        [Description("محجور")]
        Insolvent = 2,
        [Description("ممنوع المعامله")]
        Prohibited = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryQuotaType")]
    public enum NotaryQuotaType
    {
        [Description("دانگ")]
        Dang = 1,
        [Description("درصد")]
        Percent = 2,
        [Description("سهم")]
        Quota = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryRegCaseAttachmentType")]
    public enum NotaryRegCaseAttachmentType
    {
        [Description("انشعاب آب اشتراکي")]
        AbCommon = 6,
        [Description("انشعاب آب اختصاصي")]
        AbPrivate = 5,
        [Description("انباري")]
        Anbari = 2,
        [Description("انشعاب برق اشتراکي")]
        BarghCommon = 4,
        [Description("انشعاب برق اختصاصي")]
        BarghPrivate = 3,
        [Description("انشعاب گاز اشتراکي")]
        GazCommon = 8,
        [Description("انشعاب گاز اختصاصي")]
        GazPrivate = 7,
        [Description("ساير")]
        Others = 10,
        [Description("پارکينگ")]
        Parking = 1,
        [Description("خط تلفن")]
        Tel = 9,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryRegServiceReqState")]
    public enum NotaryRegServiceReqState
    {
        [Description("بعد از اخذ شناسه يکتا، پرونده بسته شده است")]
        CanceledAfterGetCode = 8,
        [Description("قبل از اخذ شناسه يکتا، پرونده بسته شده است")]
        CanceledBeforeGetCode = 9,
        [Description("هزينه ها محاسبه شده است")]
        CostCalculated = 3,
        [Description("هزينه ها پرداخت شده است")]
        CostPaid = 4,
        [Description("پرونده ايجاد شده است")]
        Created = 1,
        [Description("سند توسط سردفتر تاييد نهايي شده است")]
        Finalized = 6,
        [Description("چاپ نسخه پشتيبان سند گرفته شده است")]
        FinalPrinted = 7,
        [Description("سند شناسه يکتا گرفته است")]
        SetNationalDocumentNo = 5,
        [Description("منتظر اخذ پاسخ استعلام ها")]
        WaitForInquiry = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryRequestType")]
    public enum NotaryRequestType
    {
        [Description("حکم دادگاه")]
        Judgment = 5,
        [Description("نامه")]
        Letter = 4,
        [Description("گزارش")]
        Report = 3,
        [Description("درخواست")]
        Request = 2,
        [Description("پيشنهاد")]
        Suggestion = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotarySignReqState")]
    public enum NotarySignReqState
    {
        [Description("پرونده بسته شده است")]
        Canceled = 3,
        [Description("گواهي امضاء تأييد شده است")]
        Confirmed = 2,
        [Description("پرونده ايجاد شده است")]
        Created = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotarySomnyehRobeyehActionType")]
    public enum NotarySomnyehRobeyehActionType
    {
        [Description("به انضمام")]
        Add = 2,
        [Description("به استثناء")]
        Sub = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryTemplateType")]
    public enum NotaryTemplateType
    {
        [Description("اصلي")]
        Main = 1,
        [Description("فرعي")]
        Subsidiary = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryTextPositionType")]
    public enum NotaryTextPositionType
    {
        [Description("منضمات")]
        Attachments = 4,
        [Description("مشخصات مورد معامله")]
        Case = 3,
        [Description("شرايط")]
        Conditions = 12,
        [Description("هزينه ها")]
        Cost = 11,
        [Description("مشخصات سند")]
        Document = 13,
        [Description("وضعيت تخليه")]
        Evacuation = 5,
        [Description("استعلامات")]
        Inquiry = 9,
        [Description("مستندات مالکيت")]
        OwnershipDocuments = 6,
        [Description("اشخاص")]
        Person = 2,
        [Description("سهم متعاملين")]
        PersonQuota = 7,
        [Description("بهاي سند")]
        Price = 10,
        [Description("سهم مورد معامله")]
        Quota = 8,
        [Description("مشخصات پرونده")]
        RegReq = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNotaryVaghfTypes")]
    public enum NotaryVaghfTypes
    {
        [Description("عام")]
        Am = 2,
        [Description("خاص")]
        Khass = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmNoticeFinalResult")]
    public enum NoticeFinalResult
    {
        [Description("ابلاغ بوسيله سامانه ابلاغ الکترونيکي")]
        ByElectronicSystem = 4,
        [Description("ابلاغ حضوري")]
        DeliverdInPerson = 3,
        [Description("عودت از مرجع ابلاغ")]
        DoNotice = 1,
        [Description("مجهول است")]
        DonotNotice = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNoticeResultType")]
    public enum NoticeResultType
    {
        [Description("قانوني")]
        LawNotice = 2,
        [Description("انجام نشده است")]
        NotDone = 9,
        [Description("واقعي")]
        RealNotice = 1,
        [Description("استنکاف")]
        Refusal = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNotificationFieldBasicType")]
    public enum NotificationFieldBasicType
    {
        [Description("تاريخي")]
        DateType = 3,
        [Description("عددي")]
        NumberType = 1,
        [Description("رشته اي")]
        StringType = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmNotificationTypePersmission")]
    public enum NotificationTypePersmission
    {
        [Description("استان")]
        Province = 1,
        [Description("ستاد")]
        headquarters = 2,
        [Description("واحد ثبتي")]
        Unit = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmNScriptoriumEmployeeListSort")]
    public enum NScriptoriumEmployeeListSort
    {
        [Description("تاريخ تولد")]
        BirthDate = 3,
        [Description("نام خانوادگي")]
        LastName = 2,
        [Description("شماره ملي")]
        NationalityCode = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmObjectSendState")]
    public enum ObjectSendState
    {
        [Description("ارسال نشده")]
        NotSend = 1,
        [Description("ارسال شده")]
        Sended = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmOperandType")]
    public enum OperandType
    {
        [Description(" ثابت")]
        ConstantValue = 2,
        [Description("قلم اطلاعاتي و متغيرهاي محيطي")]
        DataItem = 1,
        [Description("پارامتر")]
        Parameter = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmOperatorType")]
    public enum OperatorType
    {
        [Description("مساوي")]
        Equal = 4,
        [Description("بزرگتر")]
        Greater = 1,
        [Description("بزرگتر يا مساوي")]
        GreaterOrEqual = 2,
        [Description("پس شباهت")]
        LastWordLike = 8,
        [Description("کوچکتر")]
        LessThan = 5,
        [Description("کوچکتر يا مساوي")]
        LessThanOrEqual = 6,
        [Description("حاوي")]
        Like = 9,
        [Description("مخالف")]
        NotEqual = 3,
        [Description("پيش شباهت")]
        PreWordLike = 7,
        [Description("")]
        None = 0
    }
    [Description("EnmOprationSelectedState")]
    public enum OprationSelectedState
    {
        [Description("پيش نويس")]
        Preview = 1,
        [Description("تاييد شده")]
        Publishable = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmOrg4ExchangeData")]
    public enum Org4ExchangeData
    {
        [Description("دادگستري")]
        Judiciary = 1,
        [Description("روزنامه رسمي")]
        LagalNewspaper = 3,
        [Description("ناجا")]
        NAJA = 2,
        [Description("سازمان ثبت احوال کشور")]
        PersonEventRegisterationOrg = 4,
        [Description("سازمان امور مالياتي")]
        TaxOrg = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmOrgSenderType")]
    public enum OrgSenderType
    {
        [Description("هر دو")]
        BothOfThem = 3,
        [Description("سازمان مربوطه")]
        OtherOrg = 2,
        [Description("سازمان ثبت")]
        Sabt = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmOutgoingLetterRecieverType")]
    public enum OutgoingLetterRecieverType
    {
        [Description("شخص پرونده ")]
        CasePerson = 1,
        [Description("از فهرست گيرندگان")]
        RecieversInList = 3,
        [Description("تايپ گيرنده")]
        RecieversNotInList = 4,
        [Description("واحد ثبتي")]
        SabtUnit = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmOutgoingLetterState")]
    public enum OutgoingLetterState
    {
        [Description("تاييد شده")]
        Confirmed = 2,
        [Description("پيش نويس شده")]
        PreSend = 6,
        [Description("ثبت شده جهت تاييد")]
        Registered = 1,
        [Description("برگشت داده شده")]
        Return = 4,
        [Description("ارسال شده")]
        Sended = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmOverallBranchType")]
    public enum OverallBranchType
    {
        [Description("دادگاه")]
        Court = 1,
        [Description("دادسرا")]
        Prosecuter = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmOwnedType")]
    public enum OwnedType
    {
        [Description("واگذاري")]
        Assignment = 4,
        [Description("خريداري شده")]
        Bought = 2,
        [Description("احداث شده")]
        Constructed = 1,
        [Description("استيجاري")]
        Leasing = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmOwnershipState")]
    public enum OwnershipState
    {
        [Description("اطلاعات شخص معتبر نيست")]
        InputDataIsNotValid = 4,
        [Description("مالکيت ندارد")]
        IsNotOwner = 2,
        [Description("مالکيت دارد")]
        IsOwner = 1,
        [Description("نياز به بررسي دارد")]
        Need2BeChecked = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmOwnershipType")]
    public enum OwnershipType
    {
        [Description("بايد مقدار دهي شود")]
        Test = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmOwnerWealthType")]
    public enum OwnerWealthType
    {
        [Description("متعهد")]
        Debtor = 1,
        [Description("شخص ثالث")]
        ThirdPerson = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPACAbsoluteStatus")]
    public enum PACAbsoluteStatus
    {
        [Description("قطعي شده")]
        IsAbsolute = 1,
        [Description("قطعي نشده")]
        IsnotAbsolute = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPACAppealCourtJudgState")]
    public enum PACAppealCourtJudgState
    {
        [Description("تغيير يافته در دادگاه تجديد نظر")]
        Changed = 1,
        [Description("تاييد شده در دادگاه تجديد نظر")]
        Confirm = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPACCaseStatus")]
    public enum PACCaseStatus
    {
        [Description("ارسال به دادگاه")]
        SendToCourt = 2,
        [Description("ارسال به کميسيون کار آموزي")]
        SendToInternshipsCommission = 4,
        [Description("ارسال به دادگاه انتظامي قضاوت")]
        SendToJudgeDisciplinaryCourt = 3,
        [Description("ارسال به دادسرا")]
        SendToProsecturs = 1,
        [Description("ارسال به اجراي احکام")]
        SendToSentencesExecution = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmPACExecutedState")]
    public enum PACExecutedState
    {
        [Description("اجرا شده ")]
        Executed = 1,
        [Description("اجرا نشده ")]
        NonExecuted = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPACHowToObtainComplain")]
    public enum PACHowToObtainComplain
    {
        [Description("اداره کل ثبت اسناد و املاک استان")]
        AdministrationOfRegistration = 6,
        [Description("حوزه ثبتي")]
        DomainRegistration = 7,
        [Description("سازمان بازرسي کل کشور")]
        GeneralInspectorOffice = 2,
        [Description("دفتر بازرسي")]
        InspectionOffice = 3,
        [Description("شاکي خصوصي")]
        PrivateComplained = 1,
        [Description("دادستان")]
        Prosecutors = 4,
        [Description("امور اسناد")]
        StateDocuments = 8,
        [Description("دادستان امور مالياتي")]
        TaxProsecutor = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmPACInspectorActivities")]
    public enum PACInspectorActivities
    {
        [Description("بازرس جاري")]
        CurrentInspector = 1,
        [Description("بازرس قبلي")]
        PreviousInspector = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPACJudgeSubjectType")]
    public enum PACJudgeSubjectType
    {
        [Description("برائت در استفاده از کارت بانکي شخص")]
        BankCardPersonal = 11,
        [Description("محکوميت-تبصره ماده 2")]
        Condemnation = 2,
        [Description("تصميم دادگاه")]
        CourtDecision = 15,
        [Description("سلب صلاحيت")]
        Disqualification = 4,
        [Description("موقوفي تعقيب وفق ماده 46 قانون دفاتر اسناد رسمي")]
        EndowmentsChase46 = 12,
        [Description("موقوفي تعقيب حاصل عمومات قانوني")]
        EndowmentsChaseGeneral = 13,
        [Description("جريمه نقدي فراز 1,2")]
        Fine1_2 = 9,
        [Description("جريمه نقدي فراز 4")]
        Fine4 = 8,
        [Description("جريمه نقدي فراز 8")]
        Fine8 = 10,
        [Description("کيفرخواست ازدواج و طلاق")]
        IndictmentMarriageDivorce = 5,
        [Description("کيفرخواست اسناد رسمي")]
        IndictmentNotary = 6,
        [Description("برائت-تبصره ماده 2")]
        Innocence = 1,
        [Description("جريمه نقدي ساير فراز ها")]
        OtherFine = 14,
        [Description("منع تعقيب")]
        ProhibitProsecution = 3,
        [Description("توبيخ کتبي")]
        WrittenReprimand = 7,
        [Description("")]
        None = 0
    }
    [Description("EnmPACJudgeType")]
    public enum PACJudgeType
    {
        [Description("راي")]
        Verdict = 2,
        [Description("قرار نهايي دادسرا")]
        Writ = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPACJudgState")]
    public enum PACJudgState
    {
        [Description("قرار/کيفرخواست اصلاحي")]
        CorrectiveIndict = 5,
        [Description("راي اصلاحي")]
        CorrectiveJudge = 3,
        [Description("قرار/کيفرخواست اصلي بدون اصلاحي")]
        MainIndict = 4,
        [Description("قرار/کيفرخواست اصلي داراي اصلاحي")]
        MainIndictHasCorrective = 6,
        [Description("راي اصلي بدون اصلاحي")]
        MainJudge = 1,
        [Description("راي اصلي داراي اصلاحي")]
        MainJudgeHasCorrective = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPACMinInspectState")]
    public enum PACMinInspectState
    {
        [Description("صورتجلسه جاري")]
        Actice = 1,
        [Description("صورتجلسه بسته")]
        ClosedState = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPACMinorityOrMajority")]
    public enum PACMinorityOrMajority
    {
        [Description("راي اکثريت")]
        IsMajority = 2,
        [Description("راي اقليت")]
        IsMinority = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPACNotifyVoteToProsecutor")]
    public enum PACNotifyVoteToProsecutor
    {
        [Description("ابلاغ نشده به دادستان")]
        NotSentToProsecutor = 2,
        [Description("ابلاغ شده به دادستان")]
        SentToProsecutor = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPACPostType")]
    public enum PACPostType
    {
        [Description("شاکي")]
        Plaintiff = 1,
        [Description("مشتکي عنه/متخلف")]
        Violator = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPACRegisterTimeStatus")]
    public enum PACRegisterTimeStatus
    {
        [Description("وقت اقدام شده")]
        IsAct = 1,
        [Description("وقت اقدام نشده")]
        IsNotAct = 2,
        [Description("وقت تجديد شده")]
        Repeated = 3,
        [Description("وقت ابطال شده")]
        Revocation = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmPACRegisterTimeType")]
    public enum PACRegisterTimeType
    {
        [Description("رسيدگي")]
        Disposition = 1,
        [Description("نظارت / احتياطي")]
        Regulation = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPACResolutionOfDate")]
    public enum PACResolutionOfDate
    {
        [Description("تاريخ ايجاد راي/قرار نهايي")]
        CreateDateTime = 2,
        [Description("زمان راي/ قرار نهايي")]
        JudgDate = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPACScrHstReportOrder")]
    public enum PACScrHstReportOrder
    {
        [Description("اداره کل")]
        HighOffice = 1,
        [Description("تعداد سابقه وارد شده")]
        HstCount = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPACScriptorPostType")]
    public enum PACScriptorPostType
    {
        [Description("کفيل دفتريار ")]
        ActingAssistantOffice = 5,
        [Description("کفيل سردفتر ")]
        ActingNotary = 4,
        [Description("دفتريار اول ")]
        FirstOfficeAssistant = 2,
        [Description("بازرس ")]
        Inspector = 6,
        [Description("سردفتر ")]
        Notary = 1,
        [Description("دفتريار دوم ")]
        SecendOfficeAssistant = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmPACStatisticStatus")]
    public enum PACStatisticStatus
    {
        [Description("آمار نشده")]
        NonStatistic = 2,
        [Description("آمار شده")]
        Statisticed = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPACTallyStatus")]
    public enum PACTallyStatus
    {
        [Description("وضعيت مختومه")]
        Belated = 2,
        [Description("وضعيت جاري")]
        OnGoing = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPACTransferType")]
    public enum PACTransferType
    {
        [Description("پستي")]
        Postal = 2,
        [Description("حضوري")]
        Verbal = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPACUnitFinalJudgementType")]
    public enum PACUnitFinalJudgementType
    {
        [Description("دادگاه انتظامي")]
        DisciplineCourt = 1,
        [Description("دادگاه عالي انتظامي")]
        SupremumCourt = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmParkingType")]
    public enum ParkingType
    {
        [Description("مسقف داخل ساختمان")]
        InsideCeiling = 1,
        [Description("ند ارد")]
        Non = 4,
        [Description("مسقف خارج ساختمان")]
        OutsideCeiling = 2,
        [Description("بدون سقف")]
        WithoutCeiling = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmPaymentMediaType")]
    public enum PaymentMediaType
    {
        [Description("فيش بانکي")]
        ViaFiche = 3,
        [Description("درون سازماني")]
        ViaInternalOrganization = 4,
        [Description("اينترنتي")]
        ViaInternet = 1,
        [Description("پايانه فروش")]
        ViaPos = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPaymentRecieptType")]
    public enum PaymentRecieptType
    {
        [Description("پرداخت به شخص")]
        Payment = 2,
        [Description("دريافت از شخص")]
        Reciept = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPaymentRefundState")]
    public enum PaymentRefundState
    {
        [Description("تقاضاي استرداد پذيرفته شده است")]
        Accepted = 4,
        [Description("تقاضاي استرداد رد شده است")]
        Rejected = 3,
        [Description("تقاضاي استرداد بخشي از مبلغ داده شده است")]
        SemiPriceRefundRequest = 2,
        [Description("تقاضاي استرداد کل مبلغ داده شده است")]
        TotalPriceRefundRequest = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPerformerType")]
    public enum PerformerType
    {
        [Description("ساير")]
        Other = 2,
        [Description("فرم")]
        SystemForm = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPeriodicInspectionType")]
    public enum PeriodicInspectionType
    {
        [Description("اجمالي")]
        Brief = 2,
        [Description("کلي")]
        Total = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPermissionType")]
    public enum PermissionType
    {
        [Description("قابل رويت")]
        Visible = 1,
        [Description("قابل رويت و قابل ويرايش")]
        VisibleEditable = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPersonGeneralType")]
    public enum PersonGeneralType
    {
        [Description("حقيقي-قاضي")]
        Judge = 3,
        [Description("حقيقي-وکيل دادگستري")]
        LawyerPerson = 4,
        [Description("حقوقي-شخص سمت دار")]
        LegalPerson = 2,
        [Description("حقوقي-شرکت يا سازمان")]
        LegalUnit = 1,
        [Description("حقيقي-ساير")]
        OtherRealPerson = 6,
        [Description("حقيقي-نظامي")]
        SoldierPerson = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmPersonTitleType")]
    public enum PersonTitleType
    {
        [Description("شرکت ها")]
        Company = 5,
        [Description("اتحاديه")]
        Guild = 7,
        [Description("موسسه ها")]
        Institute = 4,
        [Description("آقا")]
        Mr = 2,
        [Description("خانم")]
        Mrs = 1,
        [Description("شهرداري")]
        Municipality = 9,
        [Description("اداره ها")]
        Office = 3,
        [Description("سازمان ها")]
        Organization = 6,
        [Description("دانشگاه")]
        University = 8,
        [Description("دهياري")]
        Village = 10,
        [Description("")]
        None = 0
    }
    [Description("EnmPersonType")]
    public enum PersonType
    {
        [Description("حقوقي")]
        Legal = 2,
        [Description("حقيقي")]
        NaturalPerson = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPiping")]
    public enum Piping
    {
        [Description("اير واشر")]
        AirWasher = 3,
        [Description("موتورخانه وشوفاژ")]
        aPowerhouse = 4,
        [Description("فاضلاب")]
        Wastewater = 2,
        [Description("آب")]
        Water = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPkiRaCertificateState")]
    public enum PkiRaCertificateState
    {
        [Description("گواهي صادره شده و در توکن هم ريخته شده است")]
        Imported = 2,
        [Description("گواهي صادره شده ولي هنوز در توکن ريخته نشده است")]
        NotImported = 1,
        [Description("باطـل شده")]
        Revoked = 3,
        [Description("گواهي تعليق شده است")]
        Suspended = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmPkiRaReasonType")]
    public enum PkiRaReasonType
    {
        [Description("گواهي خراب شده است")]
        BadCertificate = 4,
        [Description("سخت افزار توکن خراب است يا بايد عوض شود")]
        BadToken = 3,
        [Description("از تاريخ اعتبار گواهي گذشته است")]
        EndDate = 2,
        [Description("توکن مفقود شده است")]
        LostToken = 5,
        [Description("تاريخ انقضاي گواهي نزديک است")]
        NearEndDate = 1,
        [Description("توکن به سرقت رفته است")]
        StolenToken = 6,
        [Description("")]
        None = 0
    }
    [Description("EnmPkiRaReqState")]
    public enum PkiRaReqState
    {
        [Description("گواهي تحويل داده شده است")]
        Delivered = 4,
        [Description("گواهي صادر شده است")]
        Issued = 2,
        [Description("ثـبـت شده")]
        Registered = 1,
        [Description("درخواست باطل شده است")]
        RequestIsRevoked = 5,
        [Description("درخواست امضاء شده است")]
        Signed = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmPkiRaReqType")]
    public enum PkiRaReqType
    {
        [Description("درخواست صدور گواهي جديد")]
        New = 1,
        [Description("درخواست تجديد گواهي")]
        ReNew = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPkiRaRequesterType")]
    public enum PkiRaRequesterType
    {
        [Description("دفـتـرخـانـه")]
        Scriptorium = 2,
        [Description("واحد ثبتي")]
        Unit = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPkiRaRequiredDocs")]
    public enum PkiRaRequiredDocs
    {
        [Description("تصوير ابلاغيه سردفتر")]
        Eblagh = 3,
        [Description("نامه از واحد ثبتي")]
        Letter = 4,
        [Description("تصوير کارت شناسايي ملي")]
        MeliCard = 1,
        [Description("ساير")]
        Others = 5,
        [Description("تصوير صفحه اول شناسنامه")]
        Shenasnameh = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPoliceType")]
    public enum PoliceType
    {
        [Description("مراكز نظامي")]
        MilitaryService = 6,
        [Description("اداره ابلاغ ")]
        NotifiedOffice = 1,
        [Description("ساير  ")]
        Other = 10,
        [Description("ساير مراكز انتظامي")]
        OtherPolice = 5,
        [Description("كلانتري")]
        Police = 2,
        [Description("زندان")]
        Prison = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmPolompActionStatus")]
    public enum PolompActionStatus
    {
        [Description("باطل شدن پلمب امنيتي")]
        InvalidPolomp = 3,
        [Description("تخصيص پلمب امنيتي براي دفتر")]
        PolompAsign = 1,
        [Description("عمليات الصاق پلمب امنيتي با موفقيت انجام شد")]
        Successfull = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmPolompBookType")]
    public enum PolompBookType
    {
        [Description("کل")]
        Kol = 1,
        [Description("روزنامه")]
        Rozname = 2,
        [Description("دفتر خاص")]
        SpecialBook = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmPolompPageNumberType")]
    public enum PolompPageNumberType
    {
        [Description("صد برگ")]
        Pages100 = 2,
        [Description("دويست برگ")]
        Pages200 = 3,
        [Description("سيصد برگ")]
        Pages300 = 4,
        [Description("چهارصد")]
        Pages400 = 5,
        [Description("پنجاه برگ")]
        Pages50 = 1,
        [Description("پانصد برگ")]
        Pages500 = 6,
        [Description("")]
        None = 0
    }
    [Description("EnmPolompPersonType")]
    public enum PolompPersonType
    {
        [Description("شعبه شخصيت حقوقي ثبت شده در اداره ثبت شرکت ها")]
        BranchRegisterLegalPerson = 4,
        [Description("شخصيت حقوقي ثبت نشده در اداره ثبت شرکت ها و داراي شناسه ملي")]
        NonRegisterLegalPerson = 3,
        [Description("حقيقي")]
        RealPerson = 1,
        [Description("شخصيت حقوقي ثبت شده در اداره ثبت شرکت ها")]
        RegisterLegalPerson = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPolompRejectReason")]
    public enum PolompRejectReason
    {
        [Description("مخدوش بودن پلمب")]
        Damaged = 1,
        [Description("نقص تعداد ارقام بارکد")]
        DigitDefect = 2,
        [Description("مفقودي")]
        LostPolomp = 5,
        [Description("خرابي المان پلمب")]
        PolompOutofOrder = 4,
        [Description("اشتباه متصدي در الصاق پلمب به دفتر")]
        RegisterError = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmPolompRejectType")]
    public enum PolompRejectType
    {
        [Description("معيوبي دفتر بعد از الصاق پلمب امنيتي و بعد از پايان عمليات پلمب")]
        DamageBookAfterPolombedAfterEndState = 7,
        [Description("معيوبي دفتر بعد از الصاق پلمب امنيتي و قبل از پايان عمليات پلمب")]
        DamageBookAfterPolombedBeforeEndState = 5,
        [Description("معيوبي دفتر قبل از الصاق پلمب امنيتي به آن")]
        DamageBookBeforePolombed = 3,
        [Description("معيوبي پلمب امنيتي بعد از پايان عمليات پلمب")]
        DamagePolombAfterEndState = 9,
        [Description("معيوبي پلمب امنيتي قبل از پايان عمليات پلمب")]
        DamagePolombBeforeEndState = 1,
        [Description("مفقودي دفتر بعد از الصاق پلمب امنيتي و بعد از پايان عمليات پلمب")]
        LostBookAfterBookPolombedAfterEndState = 8,
        [Description("مفقودي دفتر بعد از الصاق پلمب امنيتي و قبل از پايان عمليات پلمب")]
        LostBookAfterBookPolombedBeforeEndState = 6,
        [Description("مفقودي دفتر قبل از الصاق پلمب امنيتي به آن")]
        LostBookBeforeBookPolombed = 4,
        [Description("مفقودي پلمب امنيتي قبل از الصاق به دفتر")]
        LostPolombBeforeBookPolombed = 2,
        [Description("ابطال دفتر بعد از پايان عمليات پلمب به دليل عدم شناسايي متقاضي در آدرس و اتمام مهلت 60 روزه")]
        RejectCauseUnknownAddress = 10,
        [Description("")]
        None = 0
    }
    [Description("EnmPolompRequestAddressState")]
    public enum PolompRequestAddressState
    {
        [Description("نشاني وارد شده در اظهارنامه با نشاني شخص در پايگاه داده اشخاص حقوقي مطابقت دارد")]
        AddressIsOK = 1,
        [Description("مطابقت داده نشده است")]
        NotChecked = 3,
        [Description("عدم تطابق نشاني وارد شده در اظهارنامه با نشاني شخص در پايگاه داده اشخاص حقوقي")]
        Paradox = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmPolompRequestPaidStatus")]
    public enum PolompRequestPaidStatus
    {
        [Description("تقسيم وجه شده")]
        Distributed = 152,
        [Description("درحال تقسيم وجه")]
        Distributing = 151,
        [Description("ارسال پارامترهاي اشتباه")]
        INVALID_PARAMETERS = 30,
        [Description("تقسيم  وجوه انجام نشد")]
        NoDistributed = 150,
        [Description("تراکنش لغو شده")]
        Payment_Cancel = 55,
        [Description("کد رمز شده اشتباه")]
        PublicStaticFinalIntINVALID_HASH_CODE = 40,
        [Description("شماره سفارش تکراري")]
        PublicStaticFinalIntREPETITIVE_ORDER_ID = 50,
        [Description("ورود به صفحه پرداخت اينترنتي")]
        SendToEPay = 11,
        [Description("سرويس پرداخت غير فعال بوده")]
        SERVICE_INACTIVE = 52,
        [Description("تراکنش با موفقيت انجام شد")]
        SUCCESSFUL_TRANSACTION = 10,
        [Description("تراکنش ناموفق")]
        TransactionFaild = 80,
        [Description("شماره سفارش اشتباه")]
        WRONG_CLIENT_ORDER_ID_CODE = 51,
        [Description("")]
        None = 0
    }
    [Description("EnmPolompRestoreMoneyReason")]
    public enum PolompRestoreMoneyReason
    {
        [Description("بيش از يک مرتبه براي يک متقاضي در يک سال مالي درخواست پلمب شده است")]
        MorethanOneRequest = 2,
        [Description("بيش از يک مرتبه براي يک اظهارنامه پلمب پول گرفته شده است")]
        MorethanOneTime4OneRequest = 1,
        [Description("تعداد دفاتر درخواستي بيش از تعداد مورد نياز ثبت شده است")]
        TooBookRequest = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmPostType")]
    public enum PostType
    {
        [Description("مدير كلان سيستم")]
        HighManager = 300,
        [Description("اطلاع رسان")]
        Information = 290,
        [Description("سابقه ياب")]
        inveterate = 260,
        [Description("گزارش ساز")]
        ReportBuilder = 460,
        [Description("مدير سيستم ")]
        SystemManager = 137,
        [Description("")]
        None = 0
    }
    [Description("EnmPrepareMap")]
    public enum PrepareMap
    {
        [Description("ترسيم ملک در بانک بدون نقشه برداري ، صرفا براساس ابعاد سندي و ضمن تعيين موقعيت تقريبي ،انجام شده است ")]
        DocumentAndBoundary = 4,
        [Description("ترسيم ملک در بانک بدون نقشه برداري ، صرفا براساس ابعاد سندي و ضمن تعيين موقعيت دقيق شميمي ، انجام شده است ")]
        DocumentAndSHamimBoudary = 5,
        [Description("نقشه برداري دقيق ولي جانمايي بدون شميم ، انجام داده ام ")]
        Mapping = 1,
        [Description("نقشه برداري دقيق و جانمايي با شميم ، انجام داده ام ")]
        MappingAndShamim = 2,
        [Description("استفاده از نقشه قبلي موجود در بانک کاداستر، انجام شده است")]
        OldMapping = 3,
        [Description("استفاده از نقشه ارائه شده توسط دستگاه متولي /مالک ، انجام شده است")]
        OwnerDevices = 7,
        [Description("ترسيم ملک در بانک بدون نقشه برداري ، صرفا براساس بخشنامه 24042 و ضمن تعيين موقعيت تقريبي ، انجام شده است ")]
        Role24042 = 6,
        [Description("")]
        None = 0
    }
    [Description("EnmPrivilegedStockFeature")]
    public enum PrivilegedStockFeature
    {
        [Description("سود سهام")]
        StockProfit = 2,
        [Description("حق رأي")]
        Votes = 1,
        [Description("حق راي و سود سهام")]
        VotesAndProfit = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmProceedingCostType")]
    public enum ProceedingCostType
    {
        [Description("نيم عشر")]
        FivePercent = 1,
        [Description("ربع عشر")]
        HalfFivePercent = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmProcessServerType")]
    public enum ProcessServerType
    {
        [Description("كارمند")]
        Employee = 1,
        [Description("غير كارمند")]
        UnEmployee = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmProductGroupType")]
    public enum ProductGroupType
    {
        [Description("دسته")]
        Category = 3,
        [Description("طبقه")]
        Group = 1,
        [Description("کالا/خدمات")]
        Product = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmProductOrService")]
    public enum ProductOrService
    {
        [Description("کالا")]
        Product = 1,
        [Description("خدمت")]
        Service = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmProhibitedCaseType")]
    public enum ProhibitedCaseType
    {
        [Description("ممنوعيت از معامله")]
        Mamnoee = 1,
        [Description("ممنوعيت و مصادره")]
        MamnoeeAndMosadereh = 3,
        [Description("مصادره")]
        Mosadereh = 2,
        [Description("ساير موارد")]
        Sayer = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmPTCircularType")]
    public enum PTCircularType
    {
        [Description("بخشنامه رفع محجوريت")]
        EliminationInsolvent = 4,
        [Description("بخشنامه رفع ممنوع المعامله")]
        EliminationProhibited = 2,
        [Description("بخشنامه محجوريت")]
        Insolvent = 3,
        [Description("بخشنامه ممنوع المعامله")]
        Prohibited = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmPublicWordState")]
    public enum PublicWordState
    {
        [Description("ابـطال شده")]
        Cancel = 3,
        [Description("ثبت نهايي")]
        Final = 2,
        [Description("ثبت موقت")]
        Temporary = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmReceiverLetterUnitType")]
    public enum ReceiverLetterUnitType
    {
        [Description("دفترخانه اسناد رسمي")]
        Scriptorium = 2,
        [Description("واحد ثبتي")]
        SSAA = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmReceiverServerType")]
    public enum ReceiverServerType
    {
        [Description("خادم سازمان ثبت")]
        CMSServer = 1,
        [Description("قوه قضاييه")]
        Judiciary = 2,
        [Description("روزنامه رسمي")]
        LagalNewspaper = 3,
        [Description("سازمان امور مالياتي")]
        TaxOrg = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmRefrenceType")]
    public enum RefrenceType
    {
        [Description("مرجع قضايي مكانيزه")]
        JudicialMechanize = 1,
        [Description("مرجع قضايي غير مكانيزه")]
        JudicialNonMechanize = 3,
        [Description("مرجع غير قضايي")]
        NonJudicial = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmRefundReqState")]
    public enum RefundReqState
    {
        [Description("بـاطـل  شـده")]
        Canceled = 9,
        [Description("تنظيم شده")]
        Created = 1,
        [Description("مسترد شده")]
        Refunded = 3,
        [Description("رد  شده")]
        Rejected = 8,
        [Description("ارسـال شده")]
        Sent = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmRegOrgState")]
    public enum RegOrgState
    {
        [Description("خطا در برقراري ارتباط با ثبت احوال")]
        ConnectionError = 1,
        [Description("نام خانوادگي با ثبت احوال مغايرت دارد")]
        FamilyInCorrect = 5,
        [Description("نام با ثبت احوال مغايرت دارد")]
        NameInCorrect = 4,
        [Description("با ثبت احوال چک نشده")]
        NotChecked = 6,
        [Description("شخص مورد نظر فوت شده")]
        PersonIsDead = 3,
        [Description("شخص مورد نظر يافت نشد")]
        PersonNotFound = 2,
        [Description("تمامي اقلام صحيح است")]
        Successful = 7,
        [Description("")]
        None = 0
    }
    [Description("EnmRelatedOtherOrg")]
    public enum RelatedOtherOrg
    {
        [Description("مرجع ثبت شرکت/موسسه منطقه آزاد")]
        FreezoneCCompany = 7,
        [Description("ايجاد شده از طريق شناسه ملي براي سازمان ايجاد کننده شناسه ملي")]
        ILENCCreated = 20,
        [Description("قوه قضاييه")]
        JudgeOrg = 8,
        [Description("روزنامه رسمي")]
        LagalNewspaper = 3,
        [Description("ناجا")]
        NAJA = 2,
        [Description("سازمان ثبت احوال کشور")]
        PersonEventRegisterationOrg = 4,
        [Description("شرکت پست")]
        PostCo = 6,
        [Description("سازمان تعزيرات حکومتي")]
        PUOOrg = 9,
        [Description("سازمان بيمه تامين اجتماعي")]
        SocialSecurityInsuranceOrg = 14,
        [Description("سازمان امور مالياتي")]
        TaxOrg = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmReligion")]
    public enum Religion
    {
        [Description("مسيحي")]
        Christian = 5,
        [Description("كليمي")]
        Jew = 3,
        [Description("مسلمان-شيعه")]
        Shia = 1,
        [Description("مسلمان-سني")]
        Sunni = 2,
        [Description("زرتشتي")]
        Zoroastrian = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmRemovalBannedDocumentation")]
    public enum RemovalBannedDocumentation
    {
        [Description("بدهکار مبلغ بدهي را به طلبکار و نيز مبلغ نيم عشر اجرائي را طي فيش مربوطه به صندوق دولت پرداخت نموده است")]
        PaidCost = 1,
        [Description("بستانکار به شرح نامه وارده مربوطه درخواست رفع منع خروج از کشور نامبرده را نموده است")]
        RemovalBanned = 2,
        [Description("وفق ماده 201 آئين نامه اجراي اسناد رسمي، بدهکار با معرفي ملک درخواست رفع ممنوع الخروجي براي يکبار بمدت شش ماه را دارد")]
        RemovalBannedForSixMonth = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmReplyState")]
    public enum ReplyState
    {
        [Description("جواب داده نشد")]
        NonReplied = 2,
        [Description("جواب داده شده")]
        Replied = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmReqSub")]
    public enum ReqSub
    {
        [Description("ماده 106")]
        Article106 = 3,
        [Description("تعويض سند مالکيت دفترچه اي به کاداستري")]
        ChangeBooktoCadas = 1,
        [Description("تعويض سند مالکيت کاداستري")]
        ChangetoCadas = 2,
        [Description("تقسيم نامه")]
        EnumDividedletter = 4,
        [Description("ورثه اي")]
        Inheritance = 5,
        [Description("انتقال قطعي")]
        Item1 = 7,
        [Description("درخواست صدور سند مالکيت اصلاحي بموجب تغيير مشخصات سجلي مالک")]
        Item10 = 16,
        [Description("درخواست صدور سند مالکيت اصلاحي و تجميعي")]
        Item11 = 17,
        [Description("درخواست تعويض سند")]
        Item2 = 8,
        [Description("درخواست ماده 106 و تقسيم نامه")]
        Item3 = 9,
        [Description("درخواست صدور سند مالکيت سهم الارث وراث با ارائه سند مالکيت متوفي")]
        Item4 = 10,
        [Description("درخواست صدور سند مالکيت سهم الارث وراث بدون ارائه سند مالکيت متوفي")]
        Item5 = 11,
        [Description("درخواست صدور المثني سند مالکيت به دليل فقدان سند مالکيت")]
        Item6 = 12,
        [Description("درخواست صدور المثني بعلت ريختن جوهر يا سوختگي يا پارگي يا جهات ديگري که قسمتي از سند از بين رفته باشد يا قابل استفاده نباشد")]
        Item7 = 13,
        [Description("درخواست صدور المثني سند مالکيت به دليل تقاضاي ورثه")]
        Item8 = 14,
        [Description("درخواست صدور المثني سند مالکيت در موارديکه سند مالکيت در يد ثالث است")]
        Item9 = 15,
        [Description("سايـر")]
        Others = 6,
        [Description("")]
        None = 0
    }
    [Description("EnmRequestState")]
    public enum RequestState
    {
        [Description("در حال بررسي")]
        BeingProcessed = 2,
        [Description("به دليل بروز مشکل متوقف شده")]
        ErrOccured = 4,
        [Description("پاسخ گرفته")]
        Finished = 3,
        [Description("درخواست ثبت شده")]
        Registered = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmRequestType")]
    public enum RequestType
    {
        [Description("ورشکسته")]
        BankRuptcy = 9,
        [Description("درخواست انحلال شناسه ملي")]
        BreakUpRequest = 5,
        [Description("درخواست اصلاح شناسه ملي")]
        EditRequest = 2,
        [Description("درخواست لغو اعتبار شناسه ملي")]
        InvalidationRequest = 3,
        [Description("درخواست صدور شناسه ملي")]
        IssuingRequest = 1,
        [Description("درخواست ادغام شناسه ملي")]
        MergRequest = 6,
        [Description("درخواست بازبيني شناسه ملي")]
        Review = 12,
        [Description("درخواست ختم تصفيه")]
        SettleRequest = 7,
        [Description("درخواست تعليق شناسه ملي")]
        SuspentionRequest = 11,
        [Description("بدهکار مالياتي")]
        TaxRestriction = 8,
        [Description("درخواست اعتباردهي شناسه ملي")]
        ValidationRequest = 4,
        [Description("رفع بدهکار مالياتي")]
        TaxRestrictionRemove = 10,
        [Description("")]
        None = 0
    }
    [Description("EnmResultType")]
    public enum ResultType
    {
        [Description("انجام شد  ")]
        Done = 1,
        [Description("انجام نشد ")]
        NotDone = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmRojouBeBazlReqType")]
    public enum RojouBeBazlReqType
    {
        [Description("با درخواست وکيل")]
        AttorneyRequest = 3,
        [Description("بر مبناي اقرارنامه رسمي")]
        ByAffidavit = 4,
        [Description("بر اساس حکم دادگاه")]
        ByCourt = 1,
        [Description("با درخواست زوجه")]
        WifeRequest = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmRpgAggregateType")]
    public enum RpgAggregateType
    {
        [Description("معدل")]
        Average = 3,
        [Description("تعداد")]
        Count = 1,
        [Description("تعداد غيرمکرر")]
        CountDistinct = 8,
        [Description("بيشينه")]
        Maximum = 7,
        [Description("كمينه")]
        Minimum = 6,
        [Description("جمع")]
        Sum = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmRpgAttributeType")]
    public enum RpgAttributeType
    {
        [Description("باينري بزرگ")]
        Blob = 4,
        [Description("تاريخ")]
        DateType = 5,
        [Description("عددي")]
        Numerical = 2,
        [Description("شمارشي")]
        RpgEnumerations = 3,
        [Description("ساعت")]
        TimeType = 6,
        [Description("كاركتري")]
        Varchar2 = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmRpgOrderType")]
    public enum RpgOrderType
    {
        [Description("صعودي")]
        Ascending = 2,
        [Description("نزولي")]
        Descending = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmRunState")]
    public enum RunState
    {
        [Description("اعمال شده")]
        Applied = 3,
        [Description("به خطا برخورد كرده")]
        Fail = 2,
        [Description("اعمال نشده")]
        NotApplied = 1,
        [Description("نياز به بررسي دارد")]
        Recheck = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmSaveAttachmentType")]
    public enum SaveAttachmentType
    {
        [Description("در پايگاه داده")]
        SaveInDataBase = 1,
        [Description("ذخيره در نرم افزار مديريت سند")]
        SaveInDocumentManager = 3,
        [Description("ذخيره بصورت فايل عادي")]
        SaveInFile = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmScriptoriumEmployeePosition")]
    public enum ScriptoriumEmployeePosition
    {
        [Description("سردفتر")]
        Notary = 1,
        [Description("دفتريار اول")]
        NotaryFirstAssistant = 2,
        [Description("دفتريار دوم")]
        NotarySecondAssistant = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmScriptoriumEmployeePositionType")]
    public enum ScriptoriumEmployeePositionType
    {
        [Description("کفيل")]
        Bailment = 2,
        [Description("اصيل")]
        Originality = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmScriptoriumPostType")]
    public enum ScriptoriumPostType
    {
        [Description("کفيل دفتريار")]
        ActingAssistantOffice = 4,
        [Description("کفيل سردفتر")]
        ActingNotary = 5,
        [Description("دفتريار اول")]
        FirstOfficeAssistant = 2,
        [Description("سر دفتر")]
        Notary = 1,
        [Description("دفتريار دوم")]
        SecendOfficeAssistant = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmSearchType")]
    public enum SearchType
    {
        [Description("جستجوي دقيق")]
        Exact = 1,
        [Description("جستجوي مشابه")]
        like = 3,
        [Description("جستجوي هم آوا")]
        SameAva = 2,
        [Description("جستجو در معاني مشابه")]
        SearchText = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmSeason")]
    public enum Season
    {
        [Description("پاييز")]
        Fall = 3,
        [Description("بهار")]
        Spring = 1,
        [Description("تابستان")]
        Summer = 2,
        [Description("زمستان")]
        Winter = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmSecurityType")]
    public enum SecurityType
    {
        [Description("فعاليت حوزه قضايي")]
        ActivityArea = 10,
        [Description("فعاليت شعبه")]
        ActivityCourt = 6,
        [Description("فعاليت مجتمع")]
        ActivityDepartman = 7,
        [Description("فعاليت نقش")]
        ActivityRole = 9,
        [Description("فعاليت كاربر")]
        ActivityUser = 8,
        [Description("فرآيند حوزه قضايي")]
        WorkFlowArea = 5,
        [Description("فرآيند شعبه")]
        WorkFlowCourt = 1,
        [Description("فرآيند مجتمع")]
        WorkFlowDepartman = 2,
        [Description("فرآيند نقش")]
        WorkFlowRole = 4,
        [Description("فرآيند كاربر")]
        WorkFlowUser = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmSendDataState")]
    public enum SendDataState
    {
        [Description("ارسال نشده       ")]
        NotSended = 1,
        [Description("ارسال شده       ")]
        Sended = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmSenderType")]
    public enum SenderType
    {
        [Description("منو")]
        Menu = 1,
        [Description(" گردش كار ")]
        Workflow = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmSendSecurityType")]
    public enum SendSecurityType
    {
        [Description("جهت ملاحظه")]
        ReadOnly = 1,
        [Description("جهت رسيدگي")]
        ReadWrite = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmSequenceNum")]
    public enum SequenceNum
    {
        [Description("يكبار")]
        Ones = 1,
        [Description("سه بار")]
        ThreeTime = 3,
        [Description("دو بار")]
        Twice = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmSequenceType")]
    public enum SequenceType
    {
        [Description("اول")]
        First = 1,
        [Description("دوم")]
        Second = 2,
        [Description("سوم")]
        Third = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmServerType")]
    public enum ServerType
    {
        [Description("دستگاه خادم پشتيبان خادم مركزي")]
        BackupCenteralServer = 15,
        [Description("دستگاه خادم پايگاه داده و برنامه کاربردي مرکزي")]
        CentralApplicationAndDataBaseServer = 8,
        [Description("دستگاه خادم برنامه کاربردي مرکزي")]
        CentralApplicationServer = 2,
        [Description("دستگاه خادم پايگاه داده مرکزي")]
        CentralDataBaseServer = 5,
        [Description("دستگاه خادم پايگاه داده و برنامه کاربردي اينترنت")]
        InternetApplicationAndDataBaseServer = 9,
        [Description("دستگاه خادم برنامه کاربردي اينترنت")]
        InternetApplicationServer = 3,
        [Description("دستگاه خادم پايگاه داده اينترنت")]
        InternetDataBaseServer = 6,
        [Description("دستگاه خادم پايگاه داده و برنامه کاربردي محلي")]
        LocalApplicationAndDataBaseServer = 7,
        [Description("دستگاه خادم برنامه کاربردي محلي")]
        LocalApplicationServer = 1,
        [Description("دستگاه خادم پايگاه داده محلي")]
        LocalDataBaseServer = 4,
        [Description("دستگاه خادم سرويس ها")]
        ServiceServer = 12,
        [Description("")]
        None = 0
    }
    [Description("EnmServiceMessageType")]
    public enum ServiceMessageType
    {
        [Description("خطا")]
        Error = 4,
        [Description("اطلاعات")]
        Information = 2,
        [Description("موفقيت آميز")]
        Success = 1,
        [Description("هشداري")]
        Warning = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmSetRange")]
    public enum SetRange
    {
        [Description("محدوده شهر")]
        Cityboundary = 1,
        [Description("غيره")]
        Other = 4,
        [Description("حريم شهر")]
        Privacycity = 2,
        [Description("بافت مسکوني و روستايي")]
        Urban = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmSexType")]
    public enum SexType
    {
        [Description("زن")]
        Female = 1,
        [Description("مرد")]
        Male = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmShareState")]
    public enum ShareState
    {
        [Description("سهم نمي برد")]
        HasNoShare = 2,
        [Description("سهم مي برد")]
        HasShare = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmSlashType")]
    public enum SlashType
    {
        [Description("برش واحد ثبتي")]
        CitySlash = 2,
        [Description("برش استاني")]
        ProvinceSlash = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmSMSEmailSendSuccess")]
    public enum SMSEmailSendSuccess
    {
        [Description("تکراري. قبلا در صف قرار گرفته بود")]
        DublicateInQueue = 2,
        [Description("ناموفق")]
        NotSuccessfull = 3,
        [Description("با موفقيت در صف ارسال قرار گرفت")]
        SucessQueued = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmSMSPersonType")]
    public enum SMSPersonType
    {
        [Description("شخص پرونده                           ")]
        CasePerson = 1,
        [Description("كارشناس                       ")]
        ExpertMan = 2,
        [Description("ساير اشخاص")]
        Individual = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmSMSReceiveState")]
    public enum SMSReceiveState
    {
        [Description("جديد")]
        New = 2,
        [Description("دريافت شده")]
        Received = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmSMSSendState")]
    public enum SMSSendState
    {
        [Description("تحويل شده")]
        Delivered = 5,
        [Description("ارسال ناموفق پيامک")]
        FailedSend = 3,
        [Description("ارسال ناموفق پس از کليه تلاش ها")]
        FailedSendAfterAllAttempts = 4,
        [Description("جديد")]
        NewSMS = 1,
        [Description("ارسال موفق پيامک")]
        SuccessfulSend = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmSortType")]
    public enum SortType
    {
        [Description("صعودي")]
        Asc = 1,
        [Description("نزولي")]
        Desc = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmSpecialProfessional")]
    public enum SpecialProfessional
    {
        [Description("تخصصي")]
        Professional = 2,
        [Description("اختصاصي")]
        Special = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmState")]
    public enum State
    {
        [Description("غير فعال")]
        Invalid = 2,
        [Description("فعال")]
        Valid = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmStateOfSuspend")]
    public enum StateOfSuspend
    {
        [Description("معلق است")]
        IsSuspended = 1,
        [Description("معلق نيست")]
        NotSuspended = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmStateType")]
    public enum StateType
    {
        [Description("پس از وضعيت نهايي")]
        AfterEndState = 4,
        [Description("وضعيت نهايي منفي")]
        FinalNegativeState = 3,
        [Description("وضعيت نهايي مثبت")]
        FinalPositiveState = 2,
        [Description("وضعيت هاي ابتدايي")]
        Initial = 6,
        [Description("وضعيت مياني")]
        MiddleState = 1,
        [Description("چاپ مدرک")]
        PrintDoc = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmSubsystems")]
    public enum Subsystems
    {
        [Description("اداره امور حقوقي پيش فرض")]
        DefaultOLA = 16,
        [Description("بهبود سيستمها و پردازش اطلاعات")]
        Betterment = 40,
        [Description("بخشنامه هاي ثبتي")]
        Circular = 50,
        [Description("ارتباطات")]
        Communications = 32,
        [Description("ثبت شرکت ها")]
        Company = 4,
        [Description("سامانه ثبت ريز اقلام بودجه")]
        DetailValueBudget = 12,
        [Description("ملک")]
        Estate = 2,
        [Description("اجرا")]
        Executive = 1,
        [Description("عمومي")]
        General = 30,
        [Description("شناسه ملي اشخاص حقوق")]
        ILENC = 9,
        [Description("مالکيت")]
        IndustrialOwnership = 3,
        [Description("ازدواج و طلاق   ")]
        Marriage = 13,
        [Description("ماده147")]
        N147 = 5,
        [Description("ارسال آگهي به روزنامه هاي کثيرالانتشار")]
        Newspaper = 15,
        [Description("ابلاغ سردفتران")]
        Notary = 6,
        [Description("اداره امور حقوقي دو")]
        OfficeLegalAffairs = 10,
        [Description("ثبت الکترونيکي اسناد")]
        OnlineReg = 7,
        [Description("پلمب دفاتر تجاري")]
        Polomb = 14,
        [Description("دادسرا و دادگاه انتظامي سردفتران و دفترياران")]
        ProsectursAndCourt = 8,
        [Description("مديريت پرونده شوراي عالي ثبت و هيات نظارت استان")]
        RegistrationCouncil = 11,
        [Description("")]
        None = 0
    }
    [Description("EnmSynchBannedState")]
    public enum SynchBannedState
    {
        [Description("به خطا خورده")]
        FailedBanned = 4,
        [Description("در حال پردازش")]
        ProcessingBanned = 2,
        [Description("با موفقيت انجام شده")]
        SuccessBanned = 3,
        [Description("در انتظار پردازش")]
        WatingBanned = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmSyncProcessed")]
    public enum SyncProcessed
    {
        [Description("پردازش نشده")]
        NonProcessed = 1,
        [Description("پردازش شده")]
        Processed = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmSystemExistanceState")]
    public enum SystemExistanceState
    {
        [Description("موجود")]
        Exist = 2,
        [Description("غيرموجود")]
        NotExist = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmSystemMessageTypeType")]
    public enum SystemMessageTypeType
    {
        [Description("خطاي حذف در FK")]
        FKConstraintDelete = 3,
        [Description("خطاي درج در Fk")]
        FKConstraintInsert = 4,
        [Description("ساير")]
        Other = 5,
        [Description("خطاي Pk")]
        PkConstraint = 1,
        [Description("خطاي Uk")]
        UniqueConstraint = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmSystemObjectType")]
    public enum SystemObjectType
    {
        [Description("پايه اي")]
        Base = 1,
        [Description("غير پايه اي")]
        NonBase = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmTaxDebtorState")]
    public enum TaxDebtorState
    {
        [Description("بازداشت شده")]
        Arrested = 1,
        [Description("رفع بازداشت شده")]
        FixingArrested = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmTerArrestStatus")]
    public enum TerArrestStatus
    {
        [Description("رفع بازداشت مالياتي شده")]
        FixArrested = 4,
        [Description("بازداشتي از نوع توقف عمليات تغيير/تاسيس دارد")]
        Lock = 2,
        [Description("بازداشتي ندارد")]
        NotArrested = 1,
        [Description("بازداشتي از نوع تذکر در حين عمليات تغيير/تاسيس دارد")]
        WarningState = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmTfaState")]
    public enum TfaState
    {
        [Description("با تأييد سردفتر، احراز هويت دو عاملي انجام نشد")]
        TfaBypassed = 4,
        [Description("احراز هويت دو مرحله اي با شکست مواجـه شد")]
        TfaFail = 3,
        [Description("احراز هويت دو عاملي هنوز انجام نـشده است")]
        TfaNotDone = 1,
        [Description("احراز هويت دو عاملي با موفقيت انجام شده است")]
        TfaSuccessed = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmTimeState")]
    public enum TimeState
    {
        [Description("ابطال شده")]
        Cancel = 3,
        [Description("اقدام شده")]
        Done = 2,
        [Description("اقدام نشده")]
        NotDone = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmTranscriptSendType")]
    public enum TranscriptSendType
    {
        [Description("بوسيله ابلاغ")]
        ByNotice = 2,
        [Description("بوسيله نامه صادره")]
        ByOutgoingLetter = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmTransferObjectState")]
    public enum TransferObjectState
    {
        [Description("اطلاعات ارسال نشده")]
        Notsended = 1,
        [Description("اطلاعات ارسال شده")]
        Sended = 2,
        [Description("ارشال شده و پذيرفته شده")]
        SendedAndAccepted = 3,
        [Description("ارسال شده و پذيرفته نشده")]
        SendedAndRejected = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmTranslateType")]
    public enum TranslateType
    {
        [Description("کلي")]
        All = 1,
        [Description("عربي به فارسي")]
        Ar2Fa = 4,
        [Description("انگليسي به فارسي")]
        En2Fa = 2,
        [Description("فارسي به عربي")]
        Fa2Ar = 5,
        [Description("فارسي به انگليسي")]
        Fa2En = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmTrueFalse")]
    public enum TrueFalse
    {
        [Description("غلط")]
        False = 2,
        [Description("درست")]
        True = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmTypeUser")]
    public enum TypeUser
    {
        [Description("کشاورزي زراعي")]
        AgriculturalFarming = 4,
        [Description("متقاضي منابع طبيعي وقفي")]
        ApplicantforNaturalResources = 6,
        [Description("ساختمان  ")]
        Building = 2,
        [Description("زمين")]
        Earth = 1,
        [Description("کشاورزي باغي")]
        GardenFarming = 3,
        [Description("منابع طبيعي")]
        NaturalResources = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmTypeValue")]
    public enum TypeValue
    {
        [Description("داده مشخص")]
        ConstantData = 4,
        [Description("تاريخ")]
        Date = 3,
        [Description("عددي")]
        Numeric = 1,
        [Description("واحد سازماني")]
        OrgUnit = 5,
        [Description("رشته ايي")]
        String = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmUnitExchangeAttachmentType")]
    public enum UnitExchangeAttachmentType
    {
        [Description("قابل تغيير")]
        Editable = 1,
        [Description("فقط مطالعه")]
        ReadOnly = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmUnitExchangeLetter")]
    public enum UnitExchangeLetter
    {
        [Description("پاسخ نامه داده شده")]
        Answered = 6,
        [Description("ثبت شده / ارسال نشده")]
        NotSend = 1,
        [Description("مشاهده شده")]
        Receive = 3,
        [Description("ارجاع داده شده")]
        Refer = 4,
        [Description("برگشت داده شده")]
        Return = 5,
        [Description("ارسال شده  / مشاهده نشده")]
        Send = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmUnitParameter")]
    public enum UnitParameter
    {
        [Description("بازه زماني تغيير رمز عبور به روز")]
        ChangePasswordPeriod = 14,
        [Description("مکانيزه شدن شعبه")]
        CourtCMSState = 8,
        [Description("شروع ساعت دفتر اوقات")]
        DailyBeginTime = 3,
        [Description("پايان ساعت دفتر اوقات")]
        DailyEndTime = 4,
        [Description("تعداد كارمند")]
        EmployeeCount = 11,
        [Description("تعداد قاضي")]
        JudgeCount = 12,
        [Description("حداکثر زمان ثبت در دفاتر مکانيزه")]
        MaxDelaySaveData = 9,
        [Description("تاريخ شروع به روز شدن اطلاعات و جمع آوري دفاتر")]
        RemoveBooks = 10,
        [Description("استاندارد حداقل تعداد وقت رسيدگي در روز")]
        SessionNumberPerDay = 5,
        [Description("استاندارد حداقل تعداد وقت احتياطي در روز")]
        TemporaryNumberPerDay = 6,
        [Description("حداقل زمان وقت دهي شعبه")]
        UnitTimePeriod = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmUnitType")]
    public enum UnitType
    {
        [Description("سازمان مستقل ثبتي")]
        OrganizationRegistration = 30,
        [Description("اداره كل ثبت اسناد و املاک استان")]
        ProvinceRegistration = 20,
        [Description("واحد ثبتي")]
        RegistrationUnit = 9,
        [Description("")]
        None = 0
    }
    [Description("EnmUrgencyLetter")]
    public enum UrgencyLetter
    {
        [Description("عادي")]
        Normal = 1,
        [Description("فوري")]
        Urgent = 2,
        [Description("خيلي فوري")]
        VeryUrgent = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmUsersPost")]
    public enum UsersPost
    {
        [Description("دادورز  ")]
        Bailiff = 10,
        [Description("منشي  ")]
        CourtClerk = 4,
        [Description("بايگان     ")]
        FileKeeper = 8,
        [Description("متصدي شعبه ")]
        Judge = 7,
        [Description("مدير دفتر   ")]
        OfficeAdministrator = 1,
        [Description("متصدي امور دفتري  ")]
        OfficeIncumbent = 3,
        [Description("دادرس  ")]
        Proceeder = 9,
        [Description("دادستان  ")]
        Prosecutor = 5,
        [Description("معاون ارجاع  ")]
        RefferingAssistant = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmUserType")]
    public enum UserType
    {
        [Description("كارمند ")]
        Emp = 1,
        [Description("غير كارمند ")]
        NonEmp = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmVekalteZemnDadnameh")]
    public enum VekalteZemnDadnameh
    {
        [Description("خير")]
        No = 3,
        [Description("بله-زوج وکيل زوجه است")]
        YesZoj = 1,
        [Description("بله-زوجه وکيل زوج است")]
        YesZojeh = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmVerificationState")]
    public enum VerificationState
    {
        [Description("تاييد")]
        Approved = 4,
        [Description("عدم تاييد")]
        Denied = 3,
        [Description("بررسي نشده")]
        HasNotVerified = 1,
        [Description("در حال بررسي")]
        InProgress = 2,
        [Description("مراجع قديمي")]
        OldReference = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmWastewater")]
    public enum Wastewater
    {
        [Description("چاه جذبي")]
        AbsorptionWell = 1,
        [Description("اگو شهري")]
        EgoUrban = 2,
        [Description("سپتيک")]
        Septic = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmWDocDeliveryMinutesState")]
    public enum WDocDeliveryMinutesState
    {
        [Description("توسط تحويل دهنده باطل شده است")]
        Canceled = 3,
        [Description("توسط تحويل گيرنده رد شده است")]
        CanceledByReciever = 5,
        [Description("توسط تحويل گيرنده تاييد شده است")]
        ConfirmedByReciever = 4,
        [Description("توسط تحويل دهنده تاييد شده است")]
        ConfirmedBySender = 2,
        [Description("تنظيم شده است")]
        Created = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmWDocDocumentsState")]
    public enum WDocDocumentsState
    {
        [Description("تاييد شده بعنوان باطل شده")]
        Invalid = 6,
        [Description("اظهار شده بعنوان باطل شده")]
        InvalidAnnounced = 3,
        [Description("تاييد شده بعنوان مفقودي")]
        Lost = 8,
        [Description("اظهار شده بعنوان مفقودي")]
        LostAnnounced = 5,
        [Description("حذف شده از موجودي اوليه")]
        Mistaken = 10,
        [Description("درخواست شده براي حذف از موجودي اوليه")]
        MistakenAnnounced = 9,
        [Description("تاييد شده بعنوان مسروقه")]
        Stolen = 7,
        [Description("اظهار شده بعنوان مسروقه")]
        StolenAnnounced = 4,
        [Description("خام و موجود براي تحرير")]
        Unused = 1,
        [Description("تحرير شده")]
        Used = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmWDocModifyReqDetailsState")]
    public enum WDocModifyReqDetailsState
    {
        [Description("درخواست اصلاح پذيرفته شد")]
        Confirmed = 1,
        [Description("درخواست اصلاح رد شد")]
        Rejected = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmWDocModifyReqState")]
    public enum WDocModifyReqState
    {
        [Description("درخواست اصلاح مورد رسيدگي قرار گرفت")]
        ResidegiShod = 3,
        [Description("درخواست اصلاح ثبت شد")]
        SabtShod = 1,
        [Description("درخواست اصلاح ارسال شد")]
        Sent = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmWDocRequestState")]
    public enum WDocRequestState
    {
        [Description("باطل شده است")]
        Canceled = 3,
        [Description("تنظيم شده است")]
        Created = 1,
        [Description("رسيدگي شده است")]
        Processed = 4,
        [Description("رد شــده")]
        Rejected = 5,
        [Description("ارسال شده است")]
        Sent = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmWealthType")]
    public enum WealthType
    {
        [Description("منقول")]
        Linkages = 1,
        [Description("غيرمنقول")]
        Immovable = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmWebServiceCallType")]
    public enum WebServiceCallType
    {
        [Description("فراخوانده شدن")]
        BeCall = 1,
        [Description("فراخواندن")]
        Call = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmWebServiceOwnerOrg")]
    public enum WebServiceOwnerOrg
    {
        [Description("ساير سازمان ها")]
        OtherOrg = 2,
        [Description("سازمان ثبت")]
        SSAA = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmWeekDay")]
    public enum WeekDay
    {
        [Description("جمعه")]
        Friday = 7,
        [Description("دوشنبه")]
        Monday = 3,
        [Description("شنبه")]
        Saturday = 1,
        [Description("يکشنبه")]
        Sunday = 2,
        [Description("پنج شنبه")]
        Thursday = 6,
        [Description("سه شنبه")]
        Tuesday = 4,
        [Description("چهارشنبه")]
        Wednesday = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmWipoPaymentType")]
    public enum WipoPaymentType
    {
        [Description("واريز به حساب بانکي")]
        WipoBankAccount = 2,
        [Description("ارسال پستي")]
        WipoPostalAccount = 3,
        [Description("پرداخت مستقيم")]
        WipoReceipt = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmWorkFlowActionTime")]
    public enum WorkFlowActionTime
    {
        [Description("موقع حذف")]
        OnDelete = 5,
        [Description("موقع ايجاد")]
        OnEntry = 1,
        [Description("موقع اتمام")]
        OnExit = 2,
        [Description("موقع برگشت")]
        OnStepBack = 4,
        [Description("موقع اصلاح")]
        OnUpdate = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmWorkFlowActionType")]
    public enum WorkFlowActionType
    {
        [Description("اجراي دستورالعمل")]
        ActionScript = 4,
        [Description("فراخواني متد")]
        MethodCall = 2,
        [Description("تغيير وضعيت متغير")]
        SetState = 3,
        [Description("رخداد روي تغيير متغير")]
        VariableChangeEvent = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmWorkFlowActivityInstanceState")]
    public enum WorkFlowActivityInstanceState
    {
        [Description("خاتمه يافته  ")]
        Done = 1,
        [Description("متوقف")]
        Pause = 3,
        [Description("در حال اجرا ")]
        Processing = 2,
        [Description("برگشت به عقب داده شده")]
        StepBacked = 6,
        [Description("منتظر")]
        Wait = 4,
        [Description("منتظر رخداد")]
        WaitOnEvent = 5,
        [Description("")]
        None = 0
    }
    [Description("EnmWorkFlowActivityType")]
    public enum WorkFlowActivityType
    {
        [Description("حاشيه اي")]
        AnnotationType = 4,
        [Description("پايان")]
        EndType = 2,
        [Description("آغاز توازي")]
        ForkType = 1,
        [Description("پايان توازي")]
        JoinType = 3,
        [Description("انتقالي")]
        RouterType = 8,
        [Description("ساده")]
        SimpleActivityType = 5,
        [Description("شروع")]
        StartType = 6,
        [Description("فراخوان گردش کار")]
        SubWorkflowType = 7,
        [Description("")]
        None = 0
    }
    [Description("EnmWorkFlowCartable")]
    public enum WorkFlowCartable
    {
        [Description("به تاخير انداخته شده")]
        Delay = 3,
        [Description("انجام شده")]
        Done = 1,
        [Description("انجام نشده")]
        Processing = 2,
        [Description("برگشت پذير")]
        StepBack = 5,
        [Description("در انتظار رخداد")]
        Wait = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmWorkFlowDataType")]
    public enum WorkFlowDataType
    {
        [Description("عددي")]
        IntegerType = 2,
        [Description("رشته اي")]
        StringType = 1,
        [Description("شيء سيستم")]
        SystemObject = 3,
        [Description("ليستي از شيء سيستم")]
        SystemObjectCollection = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmWorkFlowDisplayType")]
    public enum WorkFlowDisplayType
    {
        [Description("در سطح زير گردشها")]
        AllSubWorkFlow = 3,
        [Description("در سطح فرم")]
        FormWorkFlow = 1,
        [Description("در سطح گردش")]
        OneWorkFlow = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmWorkFlowElementType")]
    public enum WorkFlowElementType
    {
        [Description("فعاليت")]
        Activity = 1,
        [Description("گردش كار")]
        WorkFlow = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmWorkFlowInstanceState")]
    public enum WorkFlowInstanceState
    {
        [Description("خاتمه يافته")]
        Done = 2,
        [Description("متوقف شده")]
        Paused = 4,
        [Description("در حال اجرا")]
        Processing = 1,
        [Description("در حال انتظار")]
        Wait = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmWorkFlowTargetType")]
    public enum WorkFlowTargetType
    {
        [Description("متغير فعاليت")]
        ActivityContextVariable = 2,
        [Description("متغير خارجي")]
        ExternalVariable = 3,
        [Description("متغير گردش كار")]
        WorkFlowContextVariable = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmWorkFlowVariableEventTime")]
    public enum WorkFlowVariableEventTime
    {
        [Description("ايجاد")]
        OnCreate = 2,
        [Description("موقع اصلاح")]
        OnUpdate = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmWorkFlowVariableSource")]
    public enum WorkFlowVariableSource
    {
        [Description("محاسباتي")]
        Computed = 2,
        [Description("ورودي")]
        InputParameter = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmWorkFlowWorkItemType")]
    public enum WorkFlowWorkItemType
    {
        [Description("مستقل از فرآيند")]
        NonWorkFlow = 2,
        [Description("فرآيندي")]
        WorkFlow = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmXApplicationIssuingState")]
    public enum XApplicationIssuingState
    {
        [Description("از روي تقاضانامه اجرائيه ايجاد شده است")]
        CreateExecutive = 7,
        [Description("پيش نويس")]
        Draft = 1,
        [Description("تاييد شده در دفترخانه")]
        ScriptoriumConfirm = 5,
        [Description("دريافت شده در دفترخانه")]
        ScriptoriumReceived = 3,
        [Description("ارسال شده")]
        Sended = 2,
        [Description("تاييد شده در واحد ثبتي")]
        SSAAConfirm = 6,
        [Description("دريافت شده در واحد ثبتي")]
        SSAAReceived = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmXAuctionShift")]
    public enum XAuctionShift
    {
        [Description("نوبت اول")]
        StepOne = 1,
        [Description("نوبت دوم")]
        StepTwo = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXAuctionType")]
    public enum XAuctionType
    {
        [Description("بدون تشريفات")]
        WithoutProtocol = 2,
        [Description("با تشريفات")]
        WithProtocol = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmXAuctionWealthState")]
    public enum XAuctionWealthState
    {
        [Description("هنوز به مزايده گذاشته نشده")]
        NotAuction = 1,
        [Description("به مزايده گذاشته شده و به فروش نرفته")]
        NotSell = 3,
        [Description("به مزايده گذاشته شده و فروخته شده")]
        Sell = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXBankCostState")]
    public enum XBankCostState
    {
        [Description("تاييد بانک دارد")]
        BankConfirmation = 1,
        [Description("تاييد بانک ندارد")]
        NoBankConfirmation = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXBannedType")]
    public enum XBannedType
    {
        [Description("بابت بدهي بدهکار به بستانکار")]
        ForDebtOwedToCreditor = 1,
        [Description("بابت نيم عشر دولتي")]
        StateTitheHalf = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXBannedXRemovalBannedPrintType")]
    public enum XBannedXRemovalBannedPrintType
    {
        [Description("تمامي موارد")]
        PrintAll = 4,
        [Description("ممنوع الخروجي")]
        PrintXBanned = 1,
        [Description("اجازه يکبار خروج")]
        PrintXBannedOnceLetOut = 3,
        [Description("رفع ممنوع الخروجي")]
        PrintXRemovalBanned = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXCaseCostPersonsType")]
    public enum XCaseCostPersonsType
    {
        [Description("غير شخص پرونده اجرا")]
        NonXCasePerson = 2,
        [Description("شخص پرونده اجرا")]
        XCasePerson = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmXCaseCostType")]
    public enum XCaseCostType
    {
        [Description("پرداختي بدهکار بابت تعهد")]
        DebtPaymentCommitment = 3,
        [Description("هزينه اجرا")]
        ExecutiveCost = 2,
        [Description("حقوق دولتي")]
        StateLaw = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmXCaseCreditorTaskState")]
    public enum XCaseCreditorTaskState
    {
        [Description("انجام شده")]
        Done = 10,
        [Description("انجام نشده")]
        NotDone = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmXCasePayState")]
    public enum XCasePayState
    {
        [Description("تنظيم شده")]
        Issue = 1,
        [Description("پرداخت شده")]
        Pay = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXCasePlace")]
    public enum XCasePlace
    {
        [Description("داخل انبار")]
        InsideTheWarehouse = 1,
        [Description("خارج انبار")]
        OutsideTheWarehouse = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXCaseType")]
    public enum XCaseType
    {
        [Description("اصلي")]
        Main = 1,
        [Description("نيابتي")]
        Vicarious = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXCostState")]
    public enum XCostState
    {
        [Description("چک تحويل گرفته شده")]
        DeliverCheck = 2,
        [Description("پول به کارشناس تحويل شده")]
        DeliverExpert = 4,
        [Description("سپرده برگشت داده شده")]
        Deposit = 5,
        [Description("چک نقد شده")]
        GetMoneyCheck = 3,
        [Description("وجه به حساب واريز شده")]
        MoneyDepositAccount = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmXExecutiveFinalState")]
    public enum XExecutiveFinalState
    {
        [Description("از اجرائيه پرونده تشکيل شده است")]
        CaseCreatedFromXExecutive = 1,
        [Description("اجرائيه به پرونده الحاق شده است")]
        XExecutiveJoinedToCase = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXExecutiveSenderType")]
    public enum XExecutiveSenderType
    {
        [Description("ارسال از سمت سيستم اجراء")]
        SenderIsExecutiveSystem = 1,
        [Description("ارسال از سمت دفاتر اسناد رسمي")]
        SenderIsScriptorium = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXFieldType")]
    public enum XFieldType
    {
        [Description("تاريخ")]
        DateType = 3,
        [Description("شرح")]
        DescType = 1,
        [Description("مقدار")]
        NumberType = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXInstallmentsState")]
    public enum XInstallmentsState
    {
        [Description("پرداخت نشده")]
        NotPay = 1,
        [Description("پرداخت شده")]
        Payed = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXPersonState")]
    public enum XPersonState
    {
        [Description("فعال")]
        Active = 1,
        [Description("غيرفعال")]
        DeActive = 3,
        [Description("غير فعال بعد از اعمال تغييرات")]
        DeActiveAfterDoneChanges = 4,
        [Description("توقف عمليات براي شخص به دليل حکم دادگاه")]
        StopByJudge = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXPersonSubstituteReason")]
    public enum XPersonSubstituteReason
    {
        [Description("وارث")]
        Inherit = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmXPersonType")]
    public enum XPersonType
    {
        [Description("نماينده")]
        Agent = 3,
        [Description("متعهد له")]
        Creditor = 2,
        [Description("متعهد")]
        Debtor = 1,
        [Description("ساير سمت ها")]
        Other = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmXPonyType")]
    public enum XPonyType
    {
        [Description("تخليه")]
        Discharge = 2,
        [Description("انجام عمل معين")]
        DoSomething = 3,
        [Description("پرداخت مبلغ تعهد")]
        PayMoney = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmXRemindReportType")]
    public enum XRemindReportType
    {
        [Description("به تفکيک نوع شخص حقوقي متعهد له مرتبط با اجرا")]
        EjraLegalPerson = 3,
        [Description("به طور کلي")]
        GeneralReport = 1,
        [Description("به تفکيک نوع شخص حقوقي متعهد له")]
        LegalPerson = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXRemovalBannedType")]
    public enum XRemovalBannedType
    {
        [Description("رفع ممنوع الخروجي")]
        LetOut = 2,
        [Description("اجازه يک بار خروج")]
        OnceLetOut = 1,
        [Description("")]
        None = 0
    }
    [Description("EnmXReportActionUsers")]
    public enum XReportActionUsers
    {
        [Description("نوع فعاليت")]
        ActionType = 1,
        [Description("واحد")]
        Unit = 3,
        [Description("کاربر")]
        User = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXReportAdvertisment")]
    public enum XReportAdvertisment
    {
        [Description("به تفکيک ارسال کننده")]
        Sender = 3,
        [Description("به تفکيک موضوع انتشار آگهي")]
        Subject = 2,
        [Description("به تفکيک موضوع انتشار آگهي و ارسال کننده")]
        SubjectAndSender = 1,
        [Description("به تفکيک واحد ثبتي")]
        Unit = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmXReportExecutive")]
    public enum XReportExecutive
    {
        [Description("به ترتيب رديف نوع اجرائيه")]
        ExecutiveTypeId = 4,
        [Description("به ترتيب عنوان نوع اجرائيه")]
        ExecutiveTypeTitle = 5,
        [Description("به ترتيب رديف واحد سازماني")]
        UnitId = 1,
        [Description("به ترتيب کد سطح واحد سازماني")]
        UnitLevelCode = 2,
        [Description("به ترتيب عنوان واحد سازماني")]
        UnitName = 3,
        [Description("")]
        None = 0
    }
    [Description("EnmXReportIncomingLetter")]
    public enum XReportIncomingLetter
    {
        [Description("به تفکيک ثبت کننده")]
        Registrar = 3,
        [Description("به تفکيک موضوع نامه")]
        Subject = 2,
        [Description("به تفکيک موضوع نامه و ثبت کننده")]
        SubjectAndRegistrar = 1,
        [Description("به تفکيک واحد ثبتي")]
        Unit = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmXReportNotice")]
    public enum XReportNotice
    {
        [Description("به تفکيک ثبت کننده")]
        Registrar = 3,
        [Description("به تفکيک موضوع ابلاغيه")]
        Subject = 2,
        [Description("به تفکيک موضوع ابلاغيه و ثبت کننده")]
        SubjectAndRegistrar = 1,
        [Description("به تفکيک واحد ثبتي")]
        Unit = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmXReportOutgoingLetter")]
    public enum XReportOutgoingLetter
    {
        [Description("به تفکيک ارسال کننده")]
        Sender = 3,
        [Description("به تفکيک موضوع نامه")]
        Subject = 2,
        [Description("به تفکيک موضوع نامه و ارسال کننده")]
        SubjectAndSender = 1,
        [Description("به تفکيک واحد ثبتي")]
        Unit = 4,
        [Description("")]
        None = 0
    }
    [Description("EnmXReportUnitOrExecutiveType")]
    public enum XReportUnitOrExecutiveType
    {
        [Description("به تفکيک واحد سازماني")]
        Unit = 1,
        [Description("به تفکيک واحد سازماني و نوع اجرائيه")]
        UnitAndExecutiveType = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmXReportUnitOrExpertStatistics")]
    public enum XReportUnitOrExpertStatistics
    {
        [Description("به ترتيب نام خانوادگي کارمند")]
        EmployeeFamilyName = 6,
        [Description("به ترتيب رديف کارمند")]
        EmployeeId = 4,
        [Description("به ترتيب نام کارمند")]
        EmployeeName = 5,
        [Description("به ترتيب نوع اجرائيه")]
        ExecutiveType = 7,
        [Description("به ترتيب رديف سازماني")]
        UnitId = 1,
        [Description("به ترتيب کد سطح واحد سازماني")]
        UnitLevelCode = 3,
        [Description("به ترتيب عنوان واحد سازماني")]
        UnitName = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmYearHalf")]
    public enum YearHalf
    {
        [Description("نيمسال اول")]
        FirstYearHalf = 1,
        [Description("نيمسال دوم")]
        SecondYearHalf = 2,
        [Description("")]
        None = 0
    }
    [Description("EnmYesNo")]
    public enum YesNo
    {
        [Description("خير")]
        No = 2,
        [Description("بلي")]
        Yes = 1,
        [Description("")]
        None = 0
    }
    public enum DocumentState
    {
        [Description("قبل از اخذ شناسه يکتا، پرونده بسته شده است")]
        BeforeCreateIdCanceled = 9,
        [Description("بعد از اخذ شناسه يکتا، پرونده بسته شده است")]
        AfterCreateIdCanceled = 8,
        [Description("چاپ نسخه پشتيبان سند گرفته شده است")]
        BackupReport = 7,
        [Description("سند توسط سردفتر تاييد نهايي شده است")]
        Confirmed = 6,
        [Description("سند شناسه يکتا گرفته است")]
        CreateDocumentId = 5,
        [Description("هزينه ها پرداخت شده است")]
        CalculatePay = 4,
        [Description("هزينه ها محاسبه شده است")]
        CalculateCost = 3,
        [Description("منتظر اخذ پاسخ استعلام ها")]
        WaitForResponse = 2,
        [Description("پرونده ايجاد شده است")]
        Created = 1,
        [Description("")]
        None = 0
    }
    [Description("DocumentEstateAttachmentType")]
    public enum DocumentEstateAttachmentType
    {
        [Description("انشعاب آب اشتراکي")]
        AbCommon = 6,
        [Description("انشعاب آب اختصاصي")]
        AbPrivate = 5,
        [Description("انباري")]
        Anbari = 2,
        [Description("انشعاب برق اشتراکي")]
        BarghCommon = 4,
        [Description("انشعاب برق اختصاصي")]
        BarghPrivate = 3,
        [Description("انشعاب گاز اشتراکي")]
        GazCommon = 8,
        [Description("انشعاب گاز اختصاصي")]
        GazPrivate = 7,
        [Description("ساير")]
        Others = 10,
        [Description("پارکينگ")]
        Parking = 1,
        [Description("خط تلفن")]
        Tel = 9,
        [Description("")]
        None = 0
    }
    [Description("EnmFingerprintState")]
    public enum FingerprintAquisitionPermission
    {
        [Description("اثرانشگت نباید اخذ گردد")]
        Forbidden = 0,
        [Description("اخذ اثرانشگت اجباری است ")]
        Mandatory = 1,
        [Description("اخذ اثرانگشت اختیاری است")]
        Optional = 2
    }
    [Description("EnmDocumentDatailName")]
    public enum DocumentDatailName
    {
        [Description("اشخاص")]
        DocumentPeople = 1,
        [Description("اشخاص وابسته")]
        DocumentRelatedPeople = 2,
        [Description("مورد معامله")]
        DocumentCase = 3,
        [Description("سایر اطلاعات")]
        DocumentInfoOther = 4,
        [Description("شرایط و متون حقوقی")]
        DocumentInfoText = 5,
        [Description("محاسبه هزینه")]
        DocumentCost = 6,
        [Description("اسناد وابسته")]
        RelationDocument = 7,
        [Description("استعلامات")]
        DocumentInquirie = 8,
        [Description("پرداخت هزینه")]
        DocumentPayment = 10,
        [Description("پیامک")]
        DocumentSms = 11,
        [Description("")]
        None = 0
    }
    [Description("EnmCaseType")]
    public enum CaseType
    {
        [Description("خودرو")]
        DocumentVehicle = 1,
        [Description("ملک")]
        DocumentEstate = 2,
        [Description("مورد معامله")]
        DocumentCase = 3,
        [Description("")]
        None = 0
    }

    [Description("EnmCostCalculateConfirmed")]
    public enum CostCalculateConfirmed
    {
        [Description("هزینه‌ها محاسبه شده است")]
        Calculate = 1,
        [Description("هزینه‌ها محاسبه نشده است")]
        DocumentEstate = 2,
        [Description("")]
        None = 0
    }

    [Description("EnmCostPaymentConfirmed")]
    public enum CostPaymentConfirmed
    {
        [Description("پرداخت شده")]
        Paid = 1,
        [Description("آماده پرداخت")]
        ReadyToPay = 2,
        [Description("نامشخص")]
        Uncertain = 3,
        [Description("")]
        None = 0
    }

    [Description("EnmPaymentInquiryResult")]
    public enum PaymentInquiryResult
    {
        [Description("آماده پرداخت")]
        ReadyToPay = 1,
        [Description("پرداخت شده")]
        Paid = 2,
        [Description("انصراف از پرداخت")]
        PaymentCanceled = 3,
        [Description("پرداخت ثبت نشده")]
        None = 0
    }




    public enum RestrictionLevel
    {
        [EnumMember]
        None = -1,
        [EnumMember]
        Disabled = 0,
        [EnumMember]
        Enabled = 1,
        [EnumMember]
        Warning = 2,
        [EnumMember]
        Avoidance = 4,
        [EnumMember]
        Pass = 8
    }
    public enum RelatedPersonType
    {
        [EnumMember]
        None = 0,
        [EnumMember]
        Vakil = 1,
        [EnumMember]
        Movakel = 2
    }
    [Flags]
    public enum DSUActionLevel
    {
        [EnumMember]
        None = 0,
        [EnumMember]
        SelectInquiry = 1,
        [EnumMember]
        MakeReadOnly = 2,
        [EnumMember]
        GenerateDSU = 4,
        [EnumMember]
        FullFeature = 8
    }
    public enum UserAction
    {
        None = 0,
        SaveData = 1,
        FinalConfirm = 2
    }
    public enum SMSUsageContext
    {
        AgentDocument = 1,
        RelatedDocument = 2,
        FinalVerification = 3,
        UserDefined = 4,
        SSAADefined = 5,
        AzlOrEstefa = 6,
        CostOfDocumentForPay = 7
    }
    public enum SMSCostPolicy
    {
        Free,
        Non_Free
    }


    public enum IdentifierType
    {
        DocumentTypeID = 0,
        DSUTransferTypeID = 1
    }

    public enum ProcessActionType
    {
        None = 0,
        DSUSimulation = 1,
        DSUSending = 2
    }

    public enum DSUGeneratingStatus
    {
        [Description("عملیات ایجاد خلاصه معامله با شکست مواجه شد")]
        Failed = -1,
        [Description("خلاصه معامله برای این پرونده لازم نیست")]
        NotNeeded = 0,
        [Description("خلاصه معامله های پرونده با موفقیت ایجاد گردید")]
        Succeeded = 1
    }

    public enum RequirmentsValidationStatus
    {
        Succeeded = 1,
        Failed = -1,
        CompatibleCheckError = 2,
        SequenceCheckError = 4,
        InquiriesNotPermitted = 8
    }

    public enum HealthyCheckStatus
    {
        OK = 1,
        SharePartsConflict = 2,
        ShareContextConflict = 4,
        NoDSUCreated = 8,
        NoDSUFound = 16,
        Exception = -1,
        NoPersonFound = -2
    }

    public enum DigitalBookGeneratingPermissionStatus
    {
        NotNeeded = 0,
        Needed = 1,
        CriticalState = 2
    }

    public enum FingerDeviceType
    {
        Suprima = 0,
        Hongda = 1,
        Fotronic = 2
    }
    public static class EnumExtensions
    {
        public static string GetDescription(this System.Enum enumValue)
        {
            if (enumValue == null) return string.Empty;
            Type enumType = enumValue.GetType();
            string fieldName = Enum.GetName(enumType, enumValue);

            FieldInfo enumField = enumType.GetField(fieldName, BindingFlags.Static | BindingFlags.Public);
            object[] descAttributes = enumField.GetCustomAttributes(typeof(DescriptionAttribute), false);

            string displayText = fieldName;
            if (descAttributes.Length > 0)
            {
                displayText = ((DescriptionAttribute)descAttributes[0]).Description;
            }
            return displayText;
        }

        public static int ToInt32(this System.Enum enumValue)
        {
            if (enumValue == null) return 0;
            return System.Convert.ToInt32(enumValue);
        }

        public static string GetString(this System.Enum enumValue)
        {
            if (enumValue == null) return "0";
            return System.Convert.ToInt32(enumValue).ToString();
        }
        [Description("EnmLegalpersonNature")]
        public enum LegalpersonNature
        {
            [Description("")]
            None = 0,
            [Description("دولتي")]
            Government = 1,
            [Description("خصوصي")]
            Private = 2,
            [Description("ساير")]
            Other = 3
        }

    }
}
