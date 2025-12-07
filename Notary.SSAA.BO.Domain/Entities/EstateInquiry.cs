using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// استعلام ملك
/// </summary>
[Table("ESTATE_INQUIRY")]
[Index("RelatedOwnershipId", Name = "IDX_ESTATE_INQUIRY###RELATED_OWNERSHIP_ID")]
[Index("EstateInquiryId", Name = "IDX_ESTATE_INQUIRY_ESTATE_INQUIRY_ID")]
[Index("InquiryDate", Name = "IDX_ESTATE_INQUIRY_INQUIRY_DATE")]
[Index("LegacyId", Name = "IDX_ESTATE_INQUIRY_LEGACYID")]
[Index("ScriptoriumId", Name = "IDX_ESTATE_INQUIRY_SCRIPTORIUM_ID")]
[Index("GeoLocationId", Name = "IX_ESTATE_INQUIRY_GEOLOCATION_ID")]
[Index("InquiryKey", Name = "IX_ESTATE_INQUIRY_INQUIRY_KEY")]
[Index("ResponseDate", Name = "IX_ESTATE_INQUIRY_RESPONSE_DATE")]
[Index("EstateSectionId", Name = "IX_ESTATE_INQUIRY_SECTION_ID")]
[Index("EstateSeridaftarId", Name = "IX_ESTATE_INQUIRY_SERIDAFTAR_ID")]
[Index("EstateSubsectionId", Name = "IX_ESTATE_INQUIRY_SUBSECTION_ID")]
[Index("EstateInquiryTypeId", Name = "IX_ESTATE_INQUIRY_TYPE_ID")]
[Index("UnitId", Name = "IX_ESTATE_INQUIRY_UNIT_ID")]
[Index("WorkflowStatesId", Name = "IX_ESTATE_INQUIRY_WORKFLOW_STATES_ID")]
public partial class EstateInquiry
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// پلاك اصلی
    /// </summary>
    [Column("BASIC")]
    [StringLength(200)]
    [Unicode(false)]
    public string Basic { get; set; }

    /// <summary>
    /// پلاك اصلی_باقیمانده
    /// </summary>
    [Column("BASIC_REMAINING")]
    [StringLength(1)]
    [Unicode(false)]
    public string BasicRemaining { get; set; }

    /// <summary>
    /// پلاك فرعی
    /// </summary>
    [Column("SECONDARY")]
    [StringLength(200)]
    [Unicode(false)]
    public string Secondary { get; set; }

    /// <summary>
    /// پلاك فرعی_باقیمانده
    /// </summary>
    [Column("SECONDARY_REMAINING")]
    [StringLength(1)]
    [Unicode(false)]
    public string SecondaryRemaining { get; set; }

    /// <summary>
    /// مساحت
    /// </summary>
    [Column("AREA", TypeName = "NUMBER(20,3)")]
    public decimal? Area { get; set; }

    /// <summary>
    /// تاریخ ثبت سیسیتمی
    /// </summary>
    [Column("CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CreateDate { get; set; }

    /// <summary>
    /// زمان ثبت سیستمی استعلام
    /// </summary>
    [Column("CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string CreateTime { get; set; }

    /// <summary>
    /// تاریخ خلاصخه معامله قطعی
    /// </summary>
    [Column("DEAL_SUMMARY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string DealSummaryDate { get; set; }

    /// <summary>
    /// شماره خلاصه معامله قطعی
    /// </summary>
    [Column("DEAL_SUMMARY_NO")]
    [StringLength(400)]
    [Unicode(false)]
    public string DealSummaryNo { get; set; }

    /// <summary>
    /// دفترخانه تنظیم كننده
    /// </summary>
    [Column("DEAL_SUMMARY_SCRIPTORIUM")]
    [StringLength(2000)]
    [Unicode(false)]
    public string DealSummaryScriptorium { get; set; }

    /// <summary>
    /// تاریخ صورت مجلس تفكیك
    /// </summary>
    [Column("SEPARATION_DATE")]
    [StringLength(20)]
    [Unicode(false)]
    public string SeparationDate { get; set; }

    /// <summary>
    /// شماره صورت مجلس تفكیك
    /// </summary>
    [Column("SEPARATION_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string SeparationNo { get; set; }

    /// <summary>
    /// وضعیت خاص
    /// </summary>
    [Column("SPECIFIC_STATUS")]
    [StringLength(2000)]
    [Unicode(false)]
    public string SpecificStatus { get; set; }

    /// <summary>
    /// TrtsReadDate
    /// </summary>
    [Column("TRTS_READ_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string TrtsReadDate { get; set; }

    /// <summary>
    /// TrtsReadTime
    /// </summary>
    [Column("TRTS_READ_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string TrtsReadTime { get; set; }

    /// <summary>
    /// شماره چاپی سند
    /// </summary>
    [Column("DOC_PRINT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string DocPrintNo { get; set; }

    /// <summary>
    /// سند دفنرچه ای است
    /// </summary>
    [Column("DOCUMENT_IS_NOTE")]
    [StringLength(1)]
    [Unicode(false)]
    public string DocumentIsNote { get; set; }

    /// <summary>
    /// سند المثنی است
    /// </summary>
    [Column("DOCUMENT_IS_REPLICA")]
    [StringLength(1)]
    [Unicode(false)]
    public string DocumentIsReplica { get; set; }

    /// <summary>
    /// مجموع مساحت اعیانی ها
    /// </summary>
    [Column("APARTMENTS_TOTALAREA", TypeName = "NUMBER(20,2)")]
    public decimal? ApartmentsTotalarea { get; set; }

    /// <summary>
    /// ضمیمه به خلاصه معامله
    /// </summary>
    [Column("ATTACHED_TO_DEALSUMMARY")]
    [StringLength(1)]
    [Unicode(false)]
    public string AttachedToDealsummary { get; set; }

    /// <summary>
    /// شماره اظهارنامه
    /// </summary>
    [Column("EDECLARATION_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string EdeclarationNo { get; set; }

    /// <summary>
    /// شماره دفتر املاك الكترونیك
    /// </summary>
    [Column("ELECTRONIC_ESTATE_NOTE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string ElectronicEstateNoteNo { get; set; }

    /// <summary>
    /// آدرس ملك
    /// </summary>
    [Column("ESTATE_ADDRESS", TypeName = "CLOB")]
    public string EstateAddress { get; set; }

    /// <summary>
    /// كد پستی ملك
    /// </summary>
    [Column("ESTATE_POSTAL_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstatePostalCode { get; set; }

    /// <summary>
    /// شماره دفتر
    /// </summary>
    [Column("ESTATE_NOTE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string EstateNoteNo { get; set; }

    /// <summary>
    /// تاریخ دریافت اولین ارسال
    /// </summary>
    [Column("FIRST_RECEIVE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FirstReceiveDate { get; set; }

    /// <summary>
    /// زمان دریافت اولین ارسال
    /// </summary>
    [Column("FIRST_RECEIVE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string FirstReceiveTime { get; set; }

    /// <summary>
    /// تاریخ اولین ارسال
    /// </summary>
    [Column("FIRST_SEND_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FirstSendDate { get; set; }

    /// <summary>
    /// زمان اولین ارسال
    /// </summary>
    [Column("FIRST_SEND_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string FirstSendTime { get; set; }

    /// <summary>
    /// تاریخ استعلام
    /// </summary>
    [Column("INQUIRY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string InquiryDate { get; set; }

    /// <summary>
    /// شماره استعلام
    /// </summary>
    [Column("INQUIRY_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string InquiryNo { get; set; }

    /// <summary>
    /// شماره مرجع پرداخت هزینه استعلام
    /// </summary>
    [Column("INQUIRY_PAYMANT_REFNO")]
    [StringLength(50)]
    [Unicode(false)]
    public string InquiryPaymantRefno { get; set; }

    /// <summary>
    /// تاریخ آخرین ارسال
    /// </summary>
    [Column("LAST_SEND_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LastSendDate { get; set; }

    /// <summary>
    /// زمان اخرین ارسال
    /// </summary>
    [Column("LAST_SEND_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string LastSendTime { get; set; }

    /// <summary>
    /// تاریخ دریافت آخرین ویرایش
    /// </summary>
    [Column("LASTEDIT_RECEIVE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LasteditReceiveDate { get; set; }

    /// <summary>
    /// زمان دریافت آخرین ویرایش
    /// </summary>
    [Column("LASTEDIT_RECEIVE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string LasteditReceiveTime { get; set; }

    /// <summary>
    /// تذكر
    /// </summary>
    [Column("MENTION", TypeName = "CLOB")]
    public string Mention { get; set; }

    /// <summary>
    /// تلفن همرا ه برای پیامك
    /// </summary>
    [Column("MOBILE_NO")]
    [StringLength(15)]
    [Unicode(false)]
    public string MobileNo { get; set; }

    /// <summary>
    /// متن رهن
    /// </summary>
    [Column("MORTGAGE_TEXT", TypeName = "CLOB")]
    public string MortgageText { get; set; }

    /// <summary>
    /// شماره سیستمی
    /// </summary>
    [Column("NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string No { get; set; }

    /// <summary>
    /// شماره سیستمی 2
    /// </summary>
    [Column("NO2")]
    [StringLength(20)]
    [Unicode(false)]
    public string No2 { get; set; }

    /// <summary>
    /// شماره مرجع پرداخت تبصره 21 ق.ب
    /// </summary>
    [Column("NOTE21_PAYMENT_REFNO")]
    [StringLength(50)]
    [Unicode(false)]
    public string Note21PaymentRefno { get; set; }

    /// <summary>
    /// شماره صفحه
    /// </summary>
    [Column("PAGE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string PageNo { get; set; }

    /// <summary>
    /// شماره ثبت
    /// </summary>
    [Column("REGISTER_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string RegisterNo { get; set; }

    /// <summary>
    /// كد پاسخ
    /// </summary>
    [Column("RESPONSE_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string ResponseCode { get; set; }

    /// <summary>
    /// تاریخ پاسخ
    /// </summary>
    [Column("RESPONSE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ResponseDate { get; set; }

    /// <summary>
    /// زمان پاسخ
    /// </summary>
    [Column("RESPONSE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ResponseTime { get; set; }

    /// <summary>
    /// امضای دیجیتال پاسخ
    /// </summary>
    [Column("RESPONSE_DIGITALSIGNATURE", TypeName = "BLOB")]
    public byte[] ResponseDigitalsignature { get; set; }

    /// <summary>
    /// شماره پاسخ
    /// </summary>
    [Column("RESPONSE_NUMBER")]
    [StringLength(50)]
    [Unicode(false)]
    public string ResponseNumber { get; set; }

    /// <summary>
    /// تاریخ دریافت پاسخ
    /// </summary>
    [Column("RESPONSE_RECEIVE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ResponseReceiveDate { get; set; }

    /// <summary>
    /// زمان دریافت پاسخ
    /// </summary>
    [Column("RESPONSE_RECEIVE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ResponseReceiveTime { get; set; }

    /// <summary>
    /// نتیجه پاسخ
    /// </summary>
    [Column("RESPONSE_RESULT")]
    [StringLength(50)]
    [Unicode(false)]
    public string ResponseResult { get; set; }

    /// <summary>
    /// موضوع امضای دیجیتال پاسخ
    /// </summary>
    [Column("RESPONSE_SUBJECTDN")]
    [StringLength(2000)]
    [Unicode(false)]
    public string ResponseSubjectdn { get; set; }

    /// <summary>
    /// ردیف نوع استعلام
    /// </summary>
    [Column("ESTATE_INQUIRY_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string EstateInquiryTypeId { get; set; }

    /// <summary>
    /// ردیف بخش
    /// </summary>
    [Column("ESTATE_SECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSectionId { get; set; }

    /// <summary>
    /// ردیف سری دفتر
    /// </summary>
    [Column("ESTATE_SERIDAFTAR_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSeridaftarId { get; set; }

    /// <summary>
    /// ردیف ناحیه
    /// </summary>
    [Column("ESTATE_SUBSECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSubsectionId { get; set; }

    /// <summary>
    /// ردیف استعلام پیرو
    /// </summary>
    [Column("ESTATE_INQUIRY_ID")]
    public Guid? EstateInquiryId { get; set; }

    /// <summary>
    /// ردیف شهر ملك
    /// </summary>
    [Column("GEO_LOCATION_ID")]
    [Precision(6)]
    public int? GeoLocationId { get; set; }

    /// <summary>
    /// ردیف فرستنده
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// ردیف گیرنده
    /// </summary>
    [Required]
    [Column("UNIT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string UnitId { get; set; }

    /// <summary>
    /// فيلد عددي کنترلي مخصوص کنترل concurrency
    /// </summary>
    [Column("TIMESTAMP", TypeName = "NUMBER(20)")]
    public decimal Timestamp { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// آيا استعلام پيروي اصلاح شده است؟
    /// </summary>
    [Column("IS_FOLLOWED_INQUIRY_UPDATED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFollowedInquiryUpdated { get; set; }

    /// <summary>
    /// شناسه گردش کار
    /// </summary>
    [Required]
    [Column("WORKFLOW_STATES_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string WorkflowStatesId { get; set; }

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
    /// پاسخ متصدی پرونده ملك
    /// </summary>
    [Column("CASEAGENT_RESPONSE", TypeName = "CLOB")]
    public string CaseagentResponse { get; set; }

    /// <summary>
    /// متن پاسخ
    /// </summary>
    [Column("RESPONSE", TypeName = "CLOB")]
    public string Response { get; set; }

    /// <summary>
    /// شماره استعلام پيروي
    /// </summary>
    [Column("FOLLOWEINQUIRYNO")]
    [StringLength(16)]
    [Unicode(false)]
    public string Followeinquiryno { get; set; }

    /// <summary>
    /// تاريخ استعلام پيروي
    /// </summary>
    [Column("FOLLOWERINQUIRYDATE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Followerinquirydate { get; set; }

    /// <summary>
    /// متن پيام سيستم
    /// </summary>
    [Column("SYSTEM_MESSAGE", TypeName = "CLOB")]
    public string SystemMessage { get; set; }

    /// <summary>
    /// کليد استعلام
    /// </summary>
    [Column("INQUIRY_KEY")]
    [StringLength(50)]
    [Unicode(false)]
    public string InquiryKey { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("PREV_FOLLOWED_INQUIRY_ID")]
    public Guid? PrevFollowedInquiryId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("RELATED_OWNERSHIP_ID")]
    [StringLength(70)]
    [Unicode(false)]
    public string RelatedOwnershipId { get; set; }

    [InverseProperty("EstateInquiry")]
    public virtual ICollection<DealSummary> DealSummaries { get; set; } = new List<DealSummary>();

    [ForeignKey("EstateInquiryId")]
    [InverseProperty("InverseEstateInquiryNavigation")]
    public virtual EstateInquiry EstateInquiryNavigation { get; set; }

    [InverseProperty("EstateInquiry")]
    public virtual ICollection<EstateInquiryPerson> EstateInquiryPeople { get; set; } = new List<EstateInquiryPerson>();

    [InverseProperty("EstateInquiry")]
    public virtual ICollection<EstateInquirySendedSm> EstateInquirySendedSms { get; set; } = new List<EstateInquirySendedSm>();

    [InverseProperty("EstateInquiry")]
    public virtual ICollection<EstateInquirySendreceiveLog> EstateInquirySendreceiveLogs { get; set; } = new List<EstateInquirySendreceiveLog>();

    [ForeignKey("EstateInquiryTypeId")]
    [InverseProperty("EstateInquiries")]
    public virtual EstateInquiryType EstateInquiryType { get; set; }

    [ForeignKey("EstateSectionId")]
    [InverseProperty("EstateInquiries")]
    public virtual EstateSection EstateSection { get; set; }

    [ForeignKey("EstateSeridaftarId")]
    [InverseProperty("EstateInquiries")]
    public virtual EstateSeridaftar EstateSeridaftar { get; set; }

    [ForeignKey("EstateSubsectionId")]
    [InverseProperty("EstateInquiries")]
    public virtual EstateSubsection EstateSubsection { get; set; }

    [InverseProperty("EstateInquiry")]
    public virtual ICollection<EstateTaxInquiry> EstateTaxInquiries { get; set; } = new List<EstateTaxInquiry>();

    [InverseProperty("EstateInquiry")]
    public virtual ICollection<ForestorgInquiry> ForestorgInquiries { get; set; } = new List<ForestorgInquiry>();

    [InverseProperty("EstateInquiryNavigation")]
    public virtual ICollection<EstateInquiry> InverseEstateInquiryNavigation { get; set; } = new List<EstateInquiry>();

    [ForeignKey("WorkflowStatesId")]
    [InverseProperty("EstateInquiries")]
    public virtual WorkflowState WorkflowStates { get; set; }
}
