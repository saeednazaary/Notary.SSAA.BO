namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices
{
    public sealed class CircularAdvancedSearchViewModel
    {
        public CircularAdvancedSearchViewModel()
        {

            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<CircularAdvancedSearchItem> GridItems { get; set; }
        public List<CircularAdvancedSearchItem> SelectedItems { get; set; }
    }
    public sealed class CircularAdvancedSearchItem
    {
        public string CircularId { get; set; }
        public string CircularArchiveClassifyNo { get; set; }
        public string CircularNationalNo { get; set; }
        public string CircularDate { get; set; }
        public string CircularTypeTitle { get; set; }
        public string CircularIssuerTitle { get; set; }
        public string StateTitle { get; set; }
        public bool IsSelected { get; set; }

    }
    public sealed class CircularAdvancedSearchExtraParams
    {
        public CircularAdvancedSearchExtraParams()
        {
            CircularProvinceId = new List<string>();
            CircularUnitId = new List<string>();
            CircularTypeId = new List<string>();
            FollowingCircularId = new List<string>();
            CircularItemTypeId = new List<string>();
            CircularScriptoriumId = new List<string>();
        }
        [System.ComponentModel.DisplayName("شناسه استان")]
        public IList<string> CircularProvinceId { get; set; }
        [System.ComponentModel.DisplayName("شناسه واحد ثبتی")]
        public IList<string> CircularUnitId { get; set; }
        [System.ComponentModel.DisplayName("شناسه نوع بخشنامه")]
        public IList<string> CircularTypeId { get; set; }
        [System.ComponentModel.DisplayName("شناسه یکتا بخشنامه")]
        public string CircularNationalNo { get; set; }
        [System.ComponentModel.DisplayName("شماره اندیکاتور بخشنامه")]
        public string CircularNo { get; set; }
        [System.ComponentModel.DisplayName("تاریخ بخشنامه")]
        public string CircularIssueDate { get; set; }
        [System.ComponentModel.DisplayName("شناسه وضعیت")]
        public string CircularStateId { get; set; }
        [System.ComponentModel.DisplayName("شناسه بخشنامه پیرو")]
        public IList<string> FollowingCircularId { get; set; }
        [System.ComponentModel.DisplayName("شماره بخشنامه پیرو")]
        public string FollowingCircularNo { get; set; }
        [System.ComponentModel.DisplayName("تاریخ بخشنامه پیرو")]
        public string FollowingCircularDate { get; set; }
        [System.ComponentModel.DisplayName("شماره نامه مرجع قضایی")]
        public string CircularCourtNo { get; set; }
        [System.ComponentModel.DisplayName("تاریخ نامه مرجع قضایی")]
        public string CircularCourtDate { get; set; }
        [System.ComponentModel.DisplayName("نام نامه مرجع قضایی")]
        public string CircularCourtTitle { get; set; }
        [System.ComponentModel.DisplayName("شماره نامه مرجع اعلام کننده")]
        public string CircularBaseClaimerNo { get; set; }
        [System.ComponentModel.DisplayName("تاریخ نامه مرجع اعلام کننده")]
        public string CircularBaseClaimerDate { get; set; }
        [System.ComponentModel.DisplayName(" نام مرجع اعلام کننده")]
        public string CircularBaseClaimerTitle { get; set; }
        [System.ComponentModel.DisplayName("جنسیت")]
        public string CircularPersonSexType { get; set; }
        [System.ComponentModel.DisplayName("نوع شخص")]
        public string CircularPersonType { get; set; }
        [System.ComponentModel.DisplayName("نام شخص")]
        public string CircularPersonName { get; set; }
        [System.ComponentModel.DisplayName("نام خانوادگی شخص")]
        public string CircularPersonFamily { get; set; }
        [System.ComponentModel.DisplayName("شماره ملی شخص")]
        public string CircularPersonNationalNo { get; set; }
        [System.ComponentModel.DisplayName("نام پدر شخص")]
        public string CircularPersonFatherName { get; set; }
        [System.ComponentModel.DisplayName("محل صدور شناسنامه")]
        public string CircularPersonIdentityIssueLocation { get; set; }
        [System.ComponentModel.DisplayName("شماره شناسنامه")]
        public string CircularPersonIdentityNo { get; set; }
        [System.ComponentModel.DisplayName("نوع شخص (حقیقی یا حقوقی)")]
        public string CircularLegalPersonType { get; set; }
        [System.ComponentModel.DisplayName("شماره ثبت")]
        public string CircularCompanyRegisterNo { get; set; }
        [System.ComponentModel.DisplayName("محل ثبت")]
        public string CircularCompanyLocation { get; set; }
        [System.ComponentModel.DisplayName("محل ثبت")]
        public string CircularLastLegalPaperNo { get; set; }
        [System.ComponentModel.DisplayName("تاريخ آخرين روزنامه رسمي/گواهي ثبت شرکت ها")]
        public string CircularLastLegalPaperDate { get; set; }
        [System.ComponentModel.DisplayName("ملیت")]
        public string CircularPersonNationality { get; set; }
        [System.ComponentModel.DisplayName("شامل اقارب نيز مي‌شود؟")]
        public bool? IsFamilyIncluded { get; set; }
        [System.ComponentModel.DisplayName("نوع مورد ممنوعیت")]
        public string CircularProhibitedCaseTypeId { get; set; }
        [System.ComponentModel.DisplayName("تمام اموال منقول و غيرمنقول؟")]
        public bool? AllAsset { get; set; }
        [System.ComponentModel.DisplayName("پلاک ثبتي فرعي")]
        public string CircularSecondaryPlaqueNo { get; set; }
        [System.ComponentModel.DisplayName("مفروز و مجزي از فرعي")]
        public string CircularDivFromSecondaryPlaque { get; set; }
        [System.ComponentModel.DisplayName("پلاک ثبتي اصلي")]
        public string CircularBasicPlaque { get; set; }
        [System.ComponentModel.DisplayName("مفروز و مجزي از اصلي")]
        public string CircularDivFromBasicPlaque { get; set; }
        [System.ComponentModel.DisplayName("بخش ثبتي ملک")]
        public string CircularEstateRegisterLocation { get; set; }
        [System.ComponentModel.DisplayName("شناسه انواع بي اعتباري در بخشنامه هاي ثبتي")]
        public IList<string> CircularItemTypeId { get; set; }
        [System.ComponentModel.DisplayName("شماره سند")]
        public string CircularDocumentNo { get; set; }
        [System.ComponentModel.DisplayName("تاريخ سند")]
        public string CircularDocumentDate { get; set; }
        [System.ComponentModel.DisplayName("شماره سري اوراق")]
        public string CircularPaperSeri { get; set; }
        [System.ComponentModel.DisplayName("شماره سريال اوراق")]
        public string CircularPaperSerial { get; set; }
        [System.ComponentModel.DisplayName("نوع سند")]
        public string CircularDocumentTitle { get; set; }
        [System.ComponentModel.DisplayName("دفترچه اي يا تک برگ")]
        public string CircularEstateDocType { get; set; }
        [System.ComponentModel.DisplayName("تحرير شده يا سفيد")]
        public string CircularDocumentTypeId { get; set; }
        [System.ComponentModel.DisplayName("نوع اوراق")]
        public string CircularPaperType { get; set; }
        [System.ComponentModel.DisplayName("تعداد اوراق")]
        public string CircularPaperCount { get; set; }
        [System.ComponentModel.DisplayName("از شماره سريال")]
        public string CircularPaperFromSerial { get; set; }
        [System.ComponentModel.DisplayName("تا شماره سريال")]
        public string CircularPaperToSerial { get; set; }
        [System.ComponentModel.DisplayName("رديف دفترخانه")]
        public IList<string> CircularScriptoriumId { get; set; }
        [System.ComponentModel.DisplayName("نوع دفترخانه - افزونه")]
        public string CircularScriptoriumType { get; set; }
        [System.ComponentModel.DisplayName("شماره دفترخانه - افزونه")]
        public string CircularScriptoriumNo { get; set; }
        [System.ComponentModel.DisplayName("محل دفترخانه - افزونه")]
        public string CircularScriptoriumLocation { get; set; }
        [System.ComponentModel.DisplayName("نوع خودرو")]
        public string CircularCarType { get; set; }
        [System.ComponentModel.DisplayName("شماره شاسي")]
        public string CircularCarChassisNo { get; set; }
        [System.ComponentModel.DisplayName("شماره موتور")]
        public string CircularCarEngineNo { get; set; }
        [System.ComponentModel.DisplayName("شماره انتظامي جديد")]
        public string CircularCarNewPlaqueNo { get; set; }
        [System.ComponentModel.DisplayName("شماره انتظامي قدیم")]
        public string CircularCarOldPlaqueNo { get; set; }
    }
}
