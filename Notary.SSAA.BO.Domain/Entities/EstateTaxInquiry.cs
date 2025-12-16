using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// استعلام مالیاتی
/// </summary>
[Table("ESTATE_TAX_INQUIRY")]
[Index("FieldUsingTypeId", Name = "IX_SSR_ESTATE_FLDUTYPID")]
[Index("LocationAssignRigthOwnershipTypeId", Name = "IX_SSR_ESTATE_LOCAROWNTYPID")]
[Index("LocationAssignRigthUsingTypeId", Name = "IX_SSR_ESTATE_LOCARUSTYID")]
[Index("FkEstateTaxProvinceId", Name = "IX_SSR_ESTATE_TXROVID")]
[Index("WorkflowStatesId", Name = "IX_SSR_ESTATE_WSTATID")]
[Index("BuildingUsingTypeId", Name = "IX_SSR_ETAXINQ_BLDUSTYPID")]
[Index("EstateTaxInquiryBuildingConstructionStepId", Name = "IX_SSR_ETXINQ_BLDCSTPID")]
[Index("EstateTaxInquiryBuildingStatusId", Name = "IX_SSR_ETXINQ_BLDSTAID")]
[Index("EstateTaxInquiryBuildingTypeId", Name = "IX_SSR_ETXINQ_BLDTYPID")]
[Index("EstateTaxCityId", Name = "IX_SSR_ETXINQ_CITYID")]
[Index("EstateTaxCountyId", Name = "IX_SSR_ETXINQ_CUNTYID")]
[Index("EstateTaxInquiryDocumentRequestTypeId", Name = "IX_SSR_ETXINQ_DREQTYPID")]
[Index("EstateTaxInquiryFieldTypeId", Name = "IX_SSR_ETXINQ_FTYPID")]
[Index("EstateInquiryId", Name = "IX_SSR_ETXINQ_INQUIRY_ID")]
[Index("EstateSectionId", Name = "IX_SSR_ETXINQ_SECID")]
[Index("EstateSubsectionId", Name = "IX_SSR_ETXINQ_SUBSECID")]
[Index("EstateTaxInquiryTransferTypeId", Name = "IX_SSR_ETXINQ_TRNSFRTYPID")]
[Index("EstateTaxUnitId", Name = "IX_SSR_ETXINQ_UID")]
public partial class EstateTaxInquiry
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// مساحت مفید اعیانی
    /// </summary>
    [Column("APARTMENT_AREA", TypeName = "NUMBER(20,3)")]
    public decimal? ApartmentArea { get; set; }

    /// <summary>
    /// مساحت عرصه
    /// </summary>
    [Column("ARSEH_AREA", TypeName = "NUMBER(20,3)")]
    public decimal? ArsehArea { get; set; }

    /// <summary>
    /// خیابان
    /// </summary>
    [Column("AVENUE", TypeName = "CLOB")]
    public string Avenue { get; set; }

    /// <summary>
    /// قدمت ساختمان
    /// </summary>
    [Column("BUILDING_OLD", TypeName = "NUMBER(24,4)")]
    public decimal? BuildingOld { get; set; }

    /// <summary>
    /// نوع ساختمان
    /// </summary>
    [Column("BUILDING_TYPE", TypeName = "CLOB")]
    public string BuildingType { get; set; }

    /// <summary>
    /// ردیف نوع كاربری اعیان
    /// </summary>
    [Column("BUILDING_USING_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string BuildingUsingTypeId { get; set; }

    /// <summary>
    /// فایل گواهی
    /// </summary>
    [Column("CERTIFICATE_FILE", TypeName = "BLOB")]
    public byte[] CertificateFile { get; set; }

    /// <summary>
    /// شماره گواهی
    /// </summary>
    [Column("CERTIFICATE_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string CertificateNo { get; set; }

    /// <summary>
    /// تاریخ نقل و انتقال
    /// </summary>
    [Column("CESSION_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CessionDate { get; set; }

    /// <summary>
    /// مبلغ معامله
    /// </summary>
    [Column("CESSION_PRICE", TypeName = "NUMBER(30)")]
    public decimal? CessionPrice { get; set; }

    /// <summary>
    /// تاریخ ثبت
    /// </summary>
    [Column("CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CreateDate { get; set; }

    /// <summary>
    /// زمان ثبت
    /// </summary>
    [Column("CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string CreateTime { get; set; }

    /// <summary>
    /// آدرس ملك
    /// </summary>
    [Column("ESTATE_ADDRESS", TypeName = "CLOB")]
    public string EstateAddress { get; set; }

    /// <summary>
    /// ردیف استعلام ملك
    /// </summary>
    [Column("ESTATE_INQUIRY_ID")]
    public Guid? EstateInquiryId { get; set; }

    /// <summary>
    /// كد پستی ملك
    /// </summary>
    [Column("ESTATE_POST_CODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string EstatePostCode { get; set; }

    /// <summary>
    /// ردیف بخش ثبتی
    /// </summary>
    [Column("ESTATE_SECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSectionId { get; set; }

    /// <summary>
    /// شماره قطعه ملك
    /// </summary>
    [Column("ESTATE_SECTOR")]
    [StringLength(20)]
    [Unicode(false)]
    public string EstateSector { get; set; }

    /// <summary>
    /// ردیف شهر
    /// </summary>
    [Column("ESTATE_TAX_CITY_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string EstateTaxCityId { get; set; }

    /// <summary>
    /// ردیف شهرستان
    /// </summary>
    [Column("ESTATE_TAX_COUNTY_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string EstateTaxCountyId { get; set; }

    /// <summary>
    /// ردیف مرحله ساخت ساختمان اعیانی
    /// </summary>
    [Column("ESTATE_TAX_INQUIRY_BUILDING_CONSTRUCTION_STEP_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string EstateTaxInquiryBuildingConstructionStepId { get; set; }

    /// <summary>
    /// ردیف وضعیت ساختمان اعیانی
    /// </summary>
    [Column("ESTATE_TAX_INQUIRY_BUILDING_STATUS_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string EstateTaxInquiryBuildingStatusId { get; set; }

    /// <summary>
    /// ردیف نوع ساختمان اعیانی
    /// </summary>
    [Column("ESTATE_TAX_INQUIRY_BUILDING_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string EstateTaxInquiryBuildingTypeId { get; set; }

    /// <summary>
    /// ردیف نوع درخواست سند
    /// </summary>
    [Column("ESTATE_TAX_INQUIRY_DOCUMENT_REQUEST_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string EstateTaxInquiryDocumentRequestTypeId { get; set; }

    /// <summary>
    /// ردیف نوع عرصه
    /// </summary>
    [Column("ESTATE_TAX_INQUIRY_FIELD_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string EstateTaxInquiryFieldTypeId { get; set; }

    /// <summary>
    /// ردیف موضوع نقل و انتقال
    /// </summary>
    [Column("ESTATE_TAX_INQUIRY_TRANSFER_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string EstateTaxInquiryTransferTypeId { get; set; }

    /// <summary>
    /// ردیف اداره مالیاتی
    /// </summary>
    [Column("ESTATE_TAX_UNIT_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string EstateTaxUnitId { get; set; }

    /// <summary>
    /// پلاك اصلی ملك
    /// </summary>
    [Column("ESTATEBASIC")]
    [StringLength(200)]
    [Unicode(false)]
    public string Estatebasic { get; set; }

    /// <summary>
    /// پلاك فرعی ملك
    /// </summary>
    [Column("ESTATESECONDARY")]
    [StringLength(200)]
    [Unicode(false)]
    public string Estatesecondary { get; set; }

    /// <summary>
    /// ردیف نوع كاربری عرصه
    /// </summary>
    [Column("FIELD_USING_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string FieldUsingTypeId { get; set; }

    /// <summary>
    /// ردیف استان
    /// </summary>
    [Column("FK_ESTATE_TAX_PROVINCE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string FkEstateTaxProvinceId { get; set; }

    /// <summary>
    /// شماره طبقه
    /// </summary>
    [Column("FLOOR_NO", TypeName = "NUMBER(14,4)")]
    public decimal? FloorNo { get; set; }

    /// <summary>
    /// آیا ملك دارای معابر اختصاصی می باشد
    /// </summary>
    [Column("HAS_SPECIAL_TRANCE")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasSpecialTrance { get; set; }

    /// <summary>
    /// آیا ملك دارای ارزش معاملاتی تعیین شده می باشد
    /// </summary>
    [Column("HAS_SPECIFIED_TRADING_VALUE")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasSpecifiedTradingValue { get; set; }

    /// <summary>
    /// فعال بودن
    /// </summary>
    [Column("IS_ACTIVE")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsActive { get; set; }

    /// <summary>
    /// آیا پایان كار یا پروانه ساختمان و یا صورت مجلس تفكیكی به نام فرد انتقال دهنده (متقاضی) است؟
    /// </summary>
    [Column("IS_FIRST_CESSION")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFirstCession { get; set; }

    /// <summary>
    /// آیا نقل و انتقال فعلی ، اولین نقل و انتقال ملك است؟
    /// </summary>
    [Column("IS_FIRST_DEAL")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFirstDeal { get; set; }

    /// <summary>
    /// در طبقه همكف واع است
    /// </summary>
    [Column("IS_GROUND_LEVEL")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsGroundLevel { get; set; }

    /// <summary>
    /// آیا گواهی صادر شده؟
    /// </summary>
    [Column("IS_LICENCE_READY")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsLicenceReady { get; set; }

    /// <summary>
    /// ملك فاقد سند تفكیكی و دارای بیش از یك نوع كاربری
    /// </summary>
    [Column("IS_MISSING_SEPARATE_DOCUMENT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsMissingSeparateDocument { get; set; }

    /// <summary>
    /// بافت فرسوده
    /// </summary>
    [Column("IS_WORN_TEXTURE")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsWornTexture { get; set; }

    /// <summary>
    /// تاریخ آخرین دریافت وضعیت
    /// </summary>
    [Column("LAST_RECEIVE_STATUS_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LastReceiveStatusDate { get; set; }

    /// <summary>
    /// زمان آخرین دریافت وضعیت
    /// </summary>
    [Column("LAST_RECEIVE_STATUS_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string LastReceiveStatusTime { get; set; }

    /// <summary>
    /// تاریخ آخرین ارسال
    /// </summary>
    [Column("LAST_SEND_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LastSendDate { get; set; }

    /// <summary>
    /// زمان آخرین ارسال
    /// </summary>
    [Column("LAST_SEND_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string LastSendTime { get; set; }

    /// <summary>
    /// تاریخ جواز
    /// </summary>
    [Column("LICENSE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LicenseDate { get; set; }

    /// <summary>
    /// ارزش روز معامله حق واگذاری محل
    /// </summary>
    [Column("LOCATION_ASSIGN_RIGHT_DEAL_CURRENT_VALUE", TypeName = "NUMBER(30)")]
    public decimal? LocationAssignRightDealCurrentValue { get; set; }

    /// <summary>
    /// ردیف نوع مالكیت حق واگذاری محل
    /// </summary>
    [Column("LOCATION_ASSIGN_RIGTH_OWNERSHIP_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string LocationAssignRigthOwnershipTypeId { get; set; }

    /// <summary>
    /// ردیف نوع كاربری حق واگذاری محل
    /// </summary>
    [Column("LOCATION_ASSIGN_RIGTH_USING_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string LocationAssignRigthUsingTypeId { get; set; }

    /// <summary>
    /// شماره سیستمی اول
    /// </summary>
    [Column("NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string No { get; set; }

    /// <summary>
    /// شماره سیستمی دوم
    /// </summary>
    [Column("NO2")]
    [StringLength(20)]
    [Unicode(false)]
    public string No2 { get; set; }

    /// <summary>
    /// شماره پلاك
    /// </summary>
    [Column("PLATE_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string PlateNo { get; set; }

    /// <summary>
    /// معاملات قبلی براساس قانون تسهیل انجام شده
    /// </summary>
    [Column("PREV_TRANSACTIONS_ACCORDING_TO_FACILITATE_RULE")]
    [StringLength(1)]
    [Unicode(false)]
    public string PrevTransactionsAccordingToFacilitateRule { get; set; }

    /// <summary>
    /// شماره بلوك قبض عوارض نوسازی
    /// </summary>
    [Column("RENOVATION_RELATED_BLOCK_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string RenovationRelatedBlockNo { get; set; }

    /// <summary>
    /// شماره ملك قبض عوارض نوع سازی
    /// </summary>
    [Column("RENOVATION_RELATED_ESTATE_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string RenovationRelatedEstateNo { get; set; }

    /// <summary>
    /// شماره ردیف قبض عوارض نوسازی
    /// </summary>
    [Column("RENOVATION_RELATED_ROW_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string RenovationRelatedRowNo { get; set; }

    /// <summary>
    /// ردیف دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شماره صورت مجلس تفكیكی
    /// </summary>
    [Column("SEPARATION_PROCESS_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string SeparationProcessNo { get; set; }

    /// <summary>
    /// سهم مالك از مالكیت
    /// </summary>
    [Column("SHARE_OF_OWNERSHIP", TypeName = "NUMBER(16,4)")]
    public decimal? ShareOfOwnership { get; set; }

    /// <summary>
    /// متن وضعیت استعلام
    /// </summary>
    [Column("STATUS_DESCRIPTION", TypeName = "CLOB")]
    public string StatusDescription { get; set; }

    /// <summary>
    /// بدهی مالیاتی
    /// </summary>
    [Column("TAX_AMOUNT")]
    [StringLength(200)]
    [Unicode(false)]
    public string TaxAmount { get; set; }

    /// <summary>
    /// شناصه قبض
    /// </summary>
    [Column("TAX_BILL_IDENTITY")]
    [StringLength(30)]
    [Unicode(false)]
    public string TaxBillIdentity { get; set; }

    /// <summary>
    /// شناسه پرداخت
    /// </summary>
    [Column("TAX_PAYMENT_IDENTITY")]
    [StringLength(30)]
    [Unicode(false)]
    public string TaxPaymentIdentity { get; set; }

    /// <summary>
    /// فيلد عددي کنترلي مخصوص کنترل concurrency
    /// </summary>
    [Column("TIMESTAMP", TypeName = "NUMBER(20)")]
    public decimal Timestamp { get; set; }

    /// <summary>
    /// مساحت كل اعیانی ها
    /// </summary>
    [Column("TOTAL_AREA", TypeName = "NUMBER(20,3)")]
    public decimal? TotalArea { get; set; }

    /// <summary>
    /// كل سهم مالكیت
    /// </summary>
    [Column("TOTAL_OWNERSHIP_SHARE", TypeName = "NUMBER(16,4)")]
    public decimal? TotalOwnershipShare { get; set; }

    /// <summary>
    /// كد رهگیری
    /// </summary>
    [Column("TRACKING_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string TrackingCode { get; set; }

    /// <summary>
    /// عرض از معبر عرصه
    /// </summary>
    [Column("TRANCE_WIDTH", TypeName = "NUMBER(33,3)")]
    public decimal? TranceWidth { get; set; }

    /// <summary>
    /// میزان انتقال برحسب سهم
    /// </summary>
    [Column("TRANSITION_SHARE", TypeName = "NUMBER(16,4)")]
    public decimal? TransitionShare { get; set; }

    /// <summary>
    /// شماره بلوك بر اساس دفترچه ارزش معاملاتی
    /// </summary>
    [Column("VALUEBOOKLET_BLOCK_NO", TypeName = "NUMBER(20)")]
    public decimal? ValuebookletBlockNo { get; set; }

    /// <summary>
    /// شماره ردیف براساس دفترچه ارزش معاملاتی
    /// </summary>
    [Column("VALUEBOOKLET_ROW_NO", TypeName = "NUMBER(20)")]
    public decimal? ValuebookletRowNo { get; set; }

    /// <summary>
    /// تاریخ گواهی پایان كار
    /// </summary>
    [Column("WORK_COMPLETION_CERTIFICATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string WorkCompletionCertificateDate { get; set; }

    /// <summary>
    /// ردیف وضعیت
    /// </summary>
    [Required]
    [Column("WORKFLOW_STATES_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string WorkflowStatesId { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه در سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شبا
    /// </summary>
    [Column("SHEBA_NO")]
    [StringLength(30)]
    [Unicode(false)]
    public string ShebaNo { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("ESTATE_UNIT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string EstateUnitId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("ESTATE_SUBSECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSubsectionId { get; set; }

    /// <summary>
    /// باقيمانده اصلي
    /// </summary>
    [Column("BASIC_REMAINING")]
    [StringLength(1)]
    [Unicode(false)]
    public string BasicRemaining { get; set; }

    /// <summary>
    /// باقيمانده فرعي
    /// </summary>
    [Column("SECONDARY_REMAINING")]
    [StringLength(1)]
    [Unicode(false)]
    public string SecondaryRemaining { get; set; }

    /// <summary>
    /// آيا هزينه ها پرداخت شده است؟
    /// </summary>
    [Column("IS_COST_PAID")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsCostPaid { get; set; }

    /// <summary>
    /// مبلغ قابل پرداخت
    /// </summary>
    [Column("SUM_PRICES")]
    [Precision(8)]
    public int? SumPrices { get; set; }

    /// <summary>
    /// شماره فاکتور
    /// </summary>
    [Column("RECEIPT_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string ReceiptNo { get; set; }

    /// <summary>
    /// تاريخ پرداخت
    /// </summary>
    [Column("PAY_COST_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string PayCostDate { get; set; }

    /// <summary>
    /// زمان پرداخت
    /// </summary>
    [Column("PAY_COST_TIME")]
    [StringLength(50)]
    [Unicode(false)]
    public string PayCostTime { get; set; }

    /// <summary>
    /// شيوه پرداخت
    /// </summary>
    [Column("PAYMENT_TYPE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string PaymentType { get; set; }

    /// <summary>
    /// شناسه قبض
    /// </summary>
    [Column("BILL_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string BillNo { get; set; }

    /// <summary>
    /// روش پرداخت
    /// </summary>
    [Column("HOW_TO_PAY")]
    [StringLength(1000)]
    [Unicode(false)]
    public string HowToPay { get; set; }

    /// <summary>
    /// فایل اچ تی ام ال گواهی
    /// </summary>
    [Column("CERTIFICATE_HTML", TypeName = "CLOB")]
    public string CertificateHtml { get; set; }

    /// <summary>
    /// قبض مالیاتی
    /// </summary>
    [Column("TAX_BILL_HTML", TypeName = "CLOB")]
    public string TaxBillHtml { get; set; }

    /// <summary>
    /// ارزش معاملاتي محاسبه شده عرصه
    /// </summary>
    [Column("LAND_VALUE")]
    [StringLength(400)]
    [Unicode(false)]
    public string LandValue { get; set; }

    /// <summary>
    /// ارزش معاملاتي محاسبه شده اعيان
    /// </summary>
    [Column("BUILDING_VALUE")]
    [StringLength(400)]
    [Unicode(false)]
    public string BuildingValue { get; set; }

    /// <summary>
    /// مبلغ ماليات حق واگذاري محل
    /// </summary>
    [Column("TAX_GOOD_WILL_VALUE")]
    [StringLength(400)]
    [Unicode(false)]
    public string TaxGoodWillValue { get; set; }

    /// <summary>
    /// مبلغ حق واگذاري محل ابرازي مودي
    /// </summary>
    [Column("GOOD_WILL_VALUE")]
    [StringLength(400)]
    [Unicode(false)]
    public string GoodWillValue { get; set; }

    /// <summary>
    /// شماره شبا 2
    /// </summary>
    [Column("SHEBA_NO2")]
    [StringLength(30)]
    [Unicode(false)]
    public string ShebaNo2 { get; set; }

    /// <summary>
    /// شماره شبا 3
    /// </summary>
    [Column("SHEBA_NO3")]
    [StringLength(30)]
    [Unicode(false)]
    public string ShebaNo3 { get; set; }

    /// <summary>
    /// مبلغ بدهي مالياتي 2
    /// </summary>
    [Column("TAX_AMOUNT2")]
    [StringLength(200)]
    [Unicode(false)]
    public string TaxAmount2 { get; set; }

    /// <summary>
    /// مبلغ بدهي مالياتي 3
    /// </summary>
    [Column("TAX_AMOUNT3")]
    [StringLength(200)]
    [Unicode(false)]
    public string TaxAmount3 { get; set; }

    /// <summary>
    /// شناسه قبض 2
    /// </summary>
    [Column("TAX_BILL_IDENTITY2")]
    [StringLength(30)]
    [Unicode(false)]
    public string TaxBillIdentity2 { get; set; }

    /// <summary>
    /// شناسه قبض 3
    /// </summary>
    [Column("TAX_BILL_IDENTITY3")]
    [StringLength(30)]
    [Unicode(false)]
    public string TaxBillIdentity3 { get; set; }

    /// <summary>
    /// شناسه پرداخت 2
    /// </summary>
    [Column("TAX_PAYMENT_IDENTITY2")]
    [StringLength(30)]
    [Unicode(false)]
    public string TaxPaymentIdentity2 { get; set; }

    /// <summary>
    /// شناسه پرداخت 3
    /// </summary>
    [Column("TAX_PAYMENT_IDENTITY3")]
    [StringLength(30)]
    [Unicode(false)]
    public string TaxPaymentIdentity3 { get; set; }

    /// <summary>
    /// سال قانون تسهيل
    /// </summary>
    [Column("FACILITY_LAW_YEAR")]
    [Precision(12)]
    public long? FacilityLawYear { get; set; }

    /// <summary>
    /// شماره قانون تسهیل
    /// </summary>
    [Column("FACILITY_LAW_NUMBER")]
    [StringLength(400)]
    [Unicode(false)]
    public string FacilityLawNumber { get; set; }

    [ForeignKey("BuildingUsingTypeId")]
    [InverseProperty("EstateTaxInquiryBuildingUsingTypes")]
    public virtual EstateTaxInquiryUsingType BuildingUsingType { get; set; }

    [ForeignKey("EstateInquiryId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateInquiry EstateInquiry { get; set; }

    [ForeignKey("EstateSectionId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateSection EstateSection { get; set; }

    [ForeignKey("EstateSubsectionId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateSubsection EstateSubsection { get; set; }

    [ForeignKey("EstateTaxCityId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateTaxCity EstateTaxCity { get; set; }

    [ForeignKey("EstateTaxCountyId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateTaxCounty EstateTaxCounty { get; set; }

    [InverseProperty("EstateTaxInquiry")]
    public virtual ICollection<EstateTaxInquiryAttach> EstateTaxInquiryAttaches { get; set; } = new List<EstateTaxInquiryAttach>();

    [ForeignKey("EstateTaxInquiryBuildingConstructionStepId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateTaxInquiryBuildingConstructionStep EstateTaxInquiryBuildingConstructionStep { get; set; }

    [ForeignKey("EstateTaxInquiryBuildingStatusId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateTaxInquiryBuildingStatus EstateTaxInquiryBuildingStatus { get; set; }

    [ForeignKey("EstateTaxInquiryBuildingTypeId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateTaxInquiryBuildingType EstateTaxInquiryBuildingType { get; set; }

    [ForeignKey("EstateTaxInquiryDocumentRequestTypeId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateTaxInquiryDocumentRequestType EstateTaxInquiryDocumentRequestType { get; set; }

    [ForeignKey("EstateTaxInquiryFieldTypeId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateTaxInquiryFieldType EstateTaxInquiryFieldType { get; set; }

    [InverseProperty("EstateTaxInquiry")]
    public virtual ICollection<EstateTaxInquiryFile> EstateTaxInquiryFiles { get; set; } = new List<EstateTaxInquiryFile>();

    [InverseProperty("EstateTaxInquiry")]
    public virtual ICollection<EstateTaxInquiryPerson> EstateTaxInquiryPeople { get; set; } = new List<EstateTaxInquiryPerson>();

    [InverseProperty("EstateTaxInquiry")]
    public virtual ICollection<EstateTaxInquirySendedSm> EstateTaxInquirySendedSms { get; set; } = new List<EstateTaxInquirySendedSm>();

    [ForeignKey("EstateTaxInquiryTransferTypeId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateTaxInquiryTransferType EstateTaxInquiryTransferType { get; set; }

    [ForeignKey("EstateTaxUnitId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateTaxUnit EstateTaxUnit { get; set; }

    [ForeignKey("FieldUsingTypeId")]
    [InverseProperty("EstateTaxInquiryFieldUsingTypes")]
    public virtual EstateTaxInquiryUsingType FieldUsingType { get; set; }

    [ForeignKey("FkEstateTaxProvinceId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateTaxProvince FkEstateTaxProvince { get; set; }

    [ForeignKey("LocationAssignRigthOwnershipTypeId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateTaxInquiryLocationAssignRigthOwnershipType LocationAssignRigthOwnershipType { get; set; }

    [ForeignKey("LocationAssignRigthUsingTypeId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual EstateTaxInquiryLocationAssignRigthUsingType LocationAssignRigthUsingType { get; set; }

    [ForeignKey("WorkflowStatesId")]
    [InverseProperty("EstateTaxInquiries")]
    public virtual WorkflowState WorkflowStates { get; set; }
}
