
namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public sealed class DocumentGrid
    {
        public DocumentGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentGridItem> GridItems { get; set; }
        public List<DocumentGridItem> SelectedItems { get; set; }
    }
    public class DocumentGridItem
    {
        public string Id { get; set; }
        public string RequestNo { get; set; }
        public string StateId { get; set; }
        public string DocumentTypeId { get; set; }
        public string RelatedDocumentTypeId { get; set; }
        public string DocumentTypeGroupOneId { get; set; }
        public string DocumentTypeGroupTwoId { get; set; }
        public string ClassifyNo { get; set; }
        public string RequestDate { get; set; }
        public string DocumentPersons { get; set; }
        public List<string> DocumentPersonList { get; set; }
        public string DocumentTypeTitle { get; set; }
        public string DocumentCases { get; set; }
        public List<string> DocumentCaseList { get; set; }
        public bool? IsSelected { get; set; }
    }
    public class DocumentExtraParams
    {
        public DocumentExtraParams()
        {
            advancedSearch = new();
        }
        public string StateId { get; set; }
        public string DocumentTypeId { get; set; }
        public string DocumentTypeGroupOneId { get; set; }
        public string DocumentTypeGroupTwoId { get; set; }
        public AdvancedSearch advancedSearch { get; set; }

    }
    public class AdvancedSearch
    {
        public AdvancedSearch()
        {
            DocumentTypeId = new List<string>();
            DocumentScriptorumId = new List<string>();

        }
        //نوع
        public IList<string> DocumentTypeId { get; set; }
        //دفترخانه
        public IList<string> DocumentScriptorumId { get; set; }
        //وضعیت
        public string DocumentState { get; set; }
        //شماره پرونده
        public string RequestNo { get; set; }
        //تاریخ سند از 
        public string FromDocumentDate { get; set; }
        //تاریخ سند تا 
        public string ToDocumentDate { get; set; }
        //تاریخ پرونده از
        public string FromRecordDate { get; set; }
        //تاریخ پرونده از
        public string ToRecordDate { get; set; }
        //شماره جلد
        public string DocumentBookVolumeNo { get; set; }
        //شماره صفحه
        public string DocumentBookPapersNo { get; set; }
        //شماره ترتیب
        public string DocumentClassifyNo { get; set; }
        //شناسه یکتا
        public string DocumentNationalNo { get; set; }
        //تاریخ ورود سند به دفتر
        public string DocumentWriteInBookDate { get; set; }
        //تاریه امضا سند توسط اصحاب سند
        public string DocumentSignDate { get; set; }
        //آیا سند براساس حکم دادگاه است ؟
        public string DocumentIsBasedJudgment { get; set; }
        public DocumentPersonAdvancedSearch documentPersonAdvancedSearch { get; set; }
        public DocumentCaseAdvancedSearch documentCaseAdvancedSearch { get; set; }
        public DocumentEstateAdvancedSearch documentEstateAdvancedSearch { get; set; }
        public DocumentVehicleAdvancedSearch documentVehicleAdvancedSearch { get; set; }
        public DocumentInquiryAdvancedSearch documentInquiryAdvancedSearch { get; set; }
        public DocumentPaymentAdvancedSearch documentPaymentAdvancedSearch { get; set; }

    }
    public class DocumentPersonAdvancedSearch
    {
        public DocumentPersonAdvancedSearch()
        {
            DocumentPersonType = new List<string>();
        }
        //اصیل است؟
        public bool? IsPersonOriginal { get; set; }
        public bool? IsPersonIranian { get; set; }
        public string PersonSexType { get; set; }
        //نوع شخص (حقیقی/حقوقی)
        public string PersonType { get; set; }
        //کدملی
        public string PersonNationalNo { get; set; }
        public string PersonName { get; set; }

        public string PersonFamily { get; set; }
        public string PersonBirthDate { get; set; }
        public string PersonFatherName { get; set; }
        //شماره شناسنامه
        public string PersonIdentityNo { get; set; }
        //سری
        public string PersonSeri { get; set; }
        //الفیا
        public string PersonAlphabetSeri { get; set; }
        //سریال
        public string PersonSerial { get; set; }
        public string PersonMobileNo { get; set; }
        public string PersonEmail { get; set; }
        public string PersonPostalCode { get; set; }
        //تلفن ثابت
        public string PersonTel { get; set; }
        //شماره آخرین روزنامه رسمی
        public string LastLegalPaperNo { get; set; }
        //تاریخ آخرین روزنامه رسمی
        public string LastLegalPaperDate { get; set; }

        public string PersonAddress { get; set; }
        //نوع سمت
        public IList<string> DocumentPersonType { get; set; }

        //شماره فکس (TODO)
    }
    //مورد معامله(TODO)
    public class DocumentCaseAdvancedSearch
    {
        public string OwnershipDetailQuota { get; set; }
        public string OwnershipTotalQuota { get; set; }
        public string SellDetailQuota { get; set; }
        public string SellTotalQuota { get; set; }
        public string GrandeeExceptionDetailQuota { get; set; }
        public string GrandeeExceptionTotalQuota { get; set; }
        public string QuotaText { get; set; }
    }
    public class DocumentEstateAdvancedSearch
    {
        public DocumentEstateAdvancedSearch()
        {
            DocumentEstateTypeId = new List<string>();
            UnitId = new List<string>();
            EstateSubSectionId = new List<string>();
            GeoLocationId = new List<string>();


        }
        //سمت
        public string EstateDirection { get; set; }
        //نوع
        public IList<string> DocumentEstateTypeId { get; set; }
        //شماره بلاک
        public string EstateBlock { get; set; }
        //حوزه ثبتی
        public IList<string> UnitId { get; set; }
        //public string EstateBlock { get; set; }
        //حدود
        public string Limits { get; set; }
        //پلاک فرعی باقی مانده دارد؟
        public bool? SecondaryPlaqueHasRemain { get; set; }
        //طبقه

        public string Floor { get; set; }

        //پلاک ثبتی فرعی
        public string SecondaryPlaque { get; set; }

        //نوع محل استقرار
        public string LocationType { get; set; }

        //پلاک اصلی باقی مانده دارد
        public bool? BasicPlaqueHasRemain { get; set; }

        //نوع ملک
        public string ImmovaleType { get; set; }
        //پلاک ثبتی اصلی
        public string BasicPlaque { get; set; }

        //مساحت
        public string Area { get; set; }

        //ناحیه ثبتی لوکاپ
        public IList<string> EstateSubSectionId { get; set; }

        //توضیحات مساحت
        public string AreaDescription { get; set; }

        //پلاک متنی
        public string PlaqueText { get; set; }

        //کدپستی
        public string PostalCode { get; set; }

        //مفروز و مجزی از فرعی
        public string DivFromSecondaryPlaque { get; set; }

        //محل جغرافیایی لوکاپ
        public IList<string> GeoLocationId { get; set; }

        //شماره قطعه
        public string Piece { get; set; }

        //عرصه یا اعیان
        public string FieldOrGrandee { get; set; }

        //مفروز و مجزی از اصلی
        public string DivFromBasicPlaque { get; set; }

        //نشانی
        public string Address { get; set; }

        //مشاعات و مشترکات
        public string Commons { get; set; }

        //حقوق ارتفاعی
        public string Rights { get; set; }

        //سلسله انتقالات-ایادی
        public string OldSaleDescription { get; set; }


    }
    public class DocumentVehicleAdvancedSearch
    {
        public DocumentVehicleAdvancedSearch()
        {
            DocumentVehicleTypeId = new List<string>();
            DocumentVehicleSystemId = new List<string>();
            DocumentVehicleTipId = new List<string>();
        }
        //نوع خودرو / داخلی یا خارجی
        public string MadeInIran { get; set; }
        //نوع 
        public IList<string> DocumentVehicleTypeId { get; set; }
        //سیستم
        public IList<string> DocumentVehicleSystemId { get; set; }
        //تیپ
        public IList<string> DocumentVehicleTipId { get; set; }
        //مدل
        public string Model { get; set; }

        //شماره شاسی
        public string ChassisNo { get; set; }

        //مرجع صادرکننده سند قبلی
        public string OldDocumentIssuer { get; set; }

        //شماره شناسه خودرو(vin (TODO)

        //رنگ
        public string Color { get; set; }

        //شماره شناسه خودرو برگ سبز(TODO)

        //شرکت بیمه کننده خودرو
        public string InssuranceCo { get; set; }

        //کارت سوخت
        public string FuelCardNo { get; set; }

        //شماره فیش پرداخت
        public string DutyFicheNo { get; set; }

        //تعداد سیلندر
        public string CylinderCount { get; set; }

        //شمارهبیمه شخص ثالت
        public string InssuranceNo { get; set; }

        //سماره سند قبلی
        public string OldDocumentNo { get; set; }

        //شماره انتظامی فروشنده
        public string PlaqueNo1Seller { get; set; }
        public string PlaqueNo2Seller { get; set; }
        public string PlaqueSeriSeller { get; set; }
        public string PlaqueNoAlphaSeller { get; set; }

        //شماره انتظامی خریدار
        public string PlaqueNo1Buyer { get; set; }
        public string PlaqueNo2Buyer { get; set; }
        public string PlaqueSeriBuyer { get; set; }
        public string PlaqueNoAlphaBuyer { get; set; }
        //حجم موتور
        public string EngineCapacity { get; set; }

        //شماره موتور
        public string EngineNo { get; set; }

        //تاریخ سند قبلی
        public string OldDocumentDate { get; set; }


    }
    //مستندات مالکیت(TODO)
    public class DocumentInquiryAdvancedSearch
    {
        public DocumentInquiryAdvancedSearch()
        {
            DocumentInquiryOrganizationId = new List<string>();
        }
        //سازمان استعلام شونده لوکاپ
        public IList<string> DocumentInquiryOrganizationId { get; set; }

        //شماره درخواست
        public string RequestNo { get; set; }

        //تاریخ درخواست از
        public string RequestFromDate { get; set; }

        //تاریخ درخواست استعلام تا
        public string RequestToDate { get; set; }

        //شماره پاسخ استعلام از 
        public string ReplyFromNo { get; set; }

        //شماره پاسخ استعلام تا
        public string ReplyToNo { get; set; }

    }
    public class DocumentPaymentAdvancedSearch
    {
        public DocumentPaymentAdvancedSearch()
        {
            CostTypeId = new List<string>();
        }
        //مبلغ سند از(TODO)
        //مبلغ سند تا(TODO)
        //ماخذ حق الثبت از(TODO)
        //ماخذ حق االثبت تا(TODO)
        //نحوه پرداخت
        public string HowToPay { get; set; }

        //نوع هزینه خدمات ثبتی لوکاپ
        public IList<string> CostTypeId { get; set; }

        //شماره تراکنش
        public string FactorNo { get; set; }
        public string Price { get; set; }

        // مبلغ سند
        public string FromPrice { get; set; }
        public string ToPrice { get; set; }


        //مبلع حق الثبت
        public string FromSabtPrice { get; set; }
        public string ToSabtPrice { get; set; }

        //تاریخ تراکنش فیش
        public string FactorDate { get; set; }

    }
}
