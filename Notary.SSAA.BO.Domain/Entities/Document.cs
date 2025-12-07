using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اسناد رسمی
/// </summary>
[Table("DOCUMENT")]
[Index("BillNo", Name = "IDX_DOCUMENT###BILL_NO")]
[Index("ClassifyNo", Name = "IDX_DOCUMENT###CLASSIFY_NO")]
[Index("CurrencyTypeId", Name = "IDX_DOCUMENT###CURRENCY_TYPE_ID")]
[Index("DocumentDate", Name = "IDX_DOCUMENT###DOCUMENT_DATE")]
[Index("DocumentSecretCode", Name = "IDX_DOCUMENT###DOCUMENT_SECRET_CODE")]
[Index("DocumentTypeId", Name = "IDX_DOCUMENT###DOCUMENT_TYPE_ID")]
[Index("GetDocumentNoDate", Name = "IDX_DOCUMENT###GET_DOCUMENT_NO_DATE")]
[Index("Ilm", Name = "IDX_DOCUMENT###ILM")]
[Index("IsBasedJudgment", Name = "IDX_DOCUMENT###IS_BASED_JUDGMENT")]
[Index("IsCostCalculateConfirmed", Name = "IDX_DOCUMENT###IS_COST_CALCULATE_CONFIRMED")]
[Index("IsCostPaymentConfirmed", Name = "IDX_DOCUMENT###IS_COST_PAYMENT_CONFIRMED")]
[Index("IsFinalVerificationVisited", Name = "IDX_DOCUMENT###IS_FINAL_VERIFICATION_VISITED")]
[Index("IsRahProcessed", Name = "IDX_DOCUMENT###IS_RAH_PROCESSED")]
[Index("IsRegistered", Name = "IDX_DOCUMENT###IS_REGISTERED")]
[Index("IsRelatedDocAbroad", Name = "IDX_DOCUMENT###IS_RELATED_DOC_ABROAD")]
[Index("IsSentToTaxOrganization", Name = "IDX_DOCUMENT###IS_SENT_TO_TAX_ORGANIZATION")]
[Index("RecordDate", Name = "IDX_DOCUMENT###RECORD_DATE")]
[Index("RefundId", Name = "IDX_DOCUMENT###REFUND_ID")]
[Index("RefundState", Name = "IDX_DOCUMENT###REFUND_STATE")]
[Index("RelatedDocumentDate", Name = "IDX_DOCUMENT###RELATED_DOCUMENT_DATE")]
[Index("RelatedDocumentId", Name = "IDX_DOCUMENT###RELATED_DOCUMENT_ID")]
[Index("RelatedDocumentIsInSsar", Name = "IDX_DOCUMENT###RELATED_DOCUMENT_IS_IN_SSAR")]
[Index("RelatedDocumentNo", Name = "IDX_DOCUMENT###RELATED_DOCUMENT_NO")]
[Index("RelatedDocumentSmsId", Name = "IDX_DOCUMENT###RELATED_DOCUMENT_SMS_ID")]
[Index("RelatedDocumentTypeId", Name = "IDX_DOCUMENT###RELATED_DOCUMENT_TYPE_ID")]
[Index("RelatedDocAbroadCountryId", Name = "IDX_DOCUMENT###RELATED_DOC_ABROAD_COUNTRY_ID")]
[Index("RelatedRegCaseId", Name = "IDX_DOCUMENT###RELATED_REG_CASE_ID")]
[Index("RelatedScriptoriumId", Name = "IDX_DOCUMENT###RELATED_SCRIPTORIUM_ID")]
[Index("RequestDate", Name = "IDX_DOCUMENT###REQUEST_DATE")]
[Index("RequestTime", Name = "IDX_DOCUMENT###REQUEST_TIME")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT###SCRIPTORIUM_ID")]
[Index("SignDate", Name = "IDX_DOCUMENT###SIGN_DATE")]
[Index("SignTime", Name = "IDX_DOCUMENT###SIGN_TIME")]
[Index("State", Name = "IDX_DOCUMENT###STATE")]
[Index("WriteInBookDate", Name = "IDX_DOCUMENT###WRITE_IN_BOOK_DATE")]
[Index("ClassifyNoReserved", Name = "IX_SSR_DOCUMENT_CLSFYNO_RES")]
[Index("IsRemoteRequest", Name = "IX_SSR_DOCUMENT_ISREMREQ")]
[Index("RemoteRequestId", Name = "IX_SSR_DOCUMENT_REMREQID")]
[Index("WriteInBookDateReserved", Name = "IX_SSR_DOCUMENT_WRTBKDAT_RES")]
[Index("LegacyId", Name = "UDX_DOCUMENT###LEGACY_ID", IsUnique = true)]
[Index("NationalNo", Name = "UDX_DOCUMENT###NATIONAL_NO", IsUnique = true)]
[Index("RequestNo", Name = "UDX_DOCUMENT###REQUEST_NO", IsUnique = true)]
public partial class Document
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شماره درخواست
    /// </summary>
    [Required]
    [Column("REQUEST_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string RequestNo { get; set; }

    /// <summary>
    /// تاریخ درخواست
    /// </summary>
    [Required]
    [Column("REQUEST_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string RequestDate { get; set; }

    /// <summary>
    /// زمان درخواست
    /// </summary>
    [Required]
    [Column("REQUEST_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string RequestTime { get; set; }

    /// <summary>
    /// تاریخ سند
    /// </summary>
    [Column("DOCUMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string DocumentDate { get; set; }

    /// <summary>
    /// تاریخ اخذ شناسه یكتا
    /// </summary>
    [Column("GET_DOCUMENT_NO_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string GetDocumentNoDate { get; set; }

    /// <summary>
    /// تاریخ امضاء سند
    /// </summary>
    [Column("SIGN_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SignDate { get; set; }

    /// <summary>
    /// زمان امضاء سند
    /// </summary>
    [Column("SIGN_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SignTime { get; set; }

    /// <summary>
    /// تاریخ ورود سند به دفتر
    /// </summary>
    [Column("WRITE_IN_BOOK_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string WriteInBookDate { get; set; }

    /// <summary>
    /// شناسه نوع سند
    /// </summary>
    [Required]
    [Column("DOCUMENT_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string DocumentTypeId { get; set; }

    /// <summary>
    /// عنوان نوع سند سایر
    /// </summary>
    [Column("DOCUMENT_TYPE_TITLE")]
    [StringLength(100)]
    [Unicode(false)]
    public string DocumentTypeTitle { get; set; }

    /// <summary>
    /// رمز تصدیق سند
    /// </summary>
    [Column("DOCUMENT_SECRET_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string DocumentSecretCode { get; set; }

    /// <summary>
    /// شناسه یكتا سند
    /// </summary>
    [Column("NATIONAL_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    /// <summary>
    /// شماره ترتیب سند
    /// </summary>
    [Column("CLASSIFY_NO")]
    [Precision(6)]
    public int? ClassifyNo { get; set; }

    /// <summary>
    /// شماره جلد دفتر دستنویس
    /// </summary>
    [Column("BOOK_VOLUME_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string BookVolumeNo { get; set; }

    /// <summary>
    /// شماره صفحه دفتر دستنویس
    /// </summary>
    [Column("BOOK_PAPERS_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string BookPapersNo { get; set; }

    /// <summary>
    /// آیا ثبت سند براساس حكم دادگاه یا سایر مراجع قانونی است؟
    /// </summary>
    [Column("IS_BASED_JUDGMENT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsBasedJudgment { get; set; }

    /// <summary>
    /// آیا ملك ثبت شده است؟
    /// </summary>
    [Column("IS_REGISTERED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRegistered { get; set; }

    /// <summary>
    /// شناسه نوع ارز
    /// </summary>
    [Column("CURRENCY_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyTypeId { get; set; }

    /// <summary>
    /// آیا سند وابسته در مراجع قانونی خارج از كشور ثبت شده است؟
    /// </summary>
    [Column("IS_RELATED_DOC_ABROAD")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRelatedDocAbroad { get; set; }

    /// <summary>
    /// شناسه كشوری كه سند وابسته در آن تنظیم شده است؟
    /// </summary>
    [Column("RELATED_DOC_ABROAD_COUNTRY_ID")]
    [Precision(6)]
    public int? RelatedDocAbroadCountryId { get; set; }

    /// <summary>
    /// آیا سند وابسته در سیستم ثبت الكترونیك اسناد ثبت شده است؟
    /// </summary>
    [Column("RELATED_DOCUMENT_IS_IN_SSAR")]
    [StringLength(1)]
    [Unicode(false)]
    public string RelatedDocumentIsInSsar { get; set; }

    /// <summary>
    /// شماره سند وابسته
    /// </summary>
    [Column("RELATED_DOCUMENT_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string RelatedDocumentNo { get; set; }

    /// <summary>
    /// تاریخ سند وابسته
    /// </summary>
    [Column("RELATED_DOCUMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string RelatedDocumentDate { get; set; }

    /// <summary>
    /// شناسه نوع سند وابسته
    /// </summary>
    [Column("RELATED_DOCUMENT_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string RelatedDocumentTypeId { get; set; }

    /// <summary>
    /// شماره و محل دفترخانه صادركننده سند وابسته
    /// </summary>
    [Column("RELATED_DOCUMENT_SCRIPTORIUM")]
    [StringLength(200)]
    [Unicode(false)]
    public string RelatedDocumentScriptorium { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده سند وابسته
    /// </summary>
    [Column("RELATED_SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string RelatedScriptoriumId { get; set; }

    /// <summary>
    /// رمز تصدیق سند وابسته
    /// </summary>
    [Column("RELATED_DOCUMENT_SECRET_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string RelatedDocumentSecretCode { get; set; }

    /// <summary>
    /// شناسه سند وابسته
    /// </summary>
    [Column("RELATED_DOCUMENT_ID")]
    public Guid? RelatedDocumentId { get; set; }

    /// <summary>
    /// شناسه مورد ثبتی وابسته
    /// </summary>
    [Column("RELATED_REG_CASE_ID")]
    public Guid? RelatedRegCaseId { get; set; }

    /// <summary>
    /// شناسه ركورد پیامك معادل سند وابسته
    /// </summary>
    [Column("RELATED_DOCUMENT_SMS_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string RelatedDocumentSmsId { get; set; }

    /// <summary>
    /// مبلغ سند
    /// </summary>
    [Column("PRICE", TypeName = "NUMBER(22)")]
    public decimal? Price { get; set; }

    /// <summary>
    /// مأخذ حق الثبت سند
    /// </summary>
    [Column("SABT_PRICE", TypeName = "NUMBER(22)")]
    public decimal? SabtPrice { get; set; }

    /// <summary>
    /// قیمت منطقه ای ملك
    /// </summary>
    [Column("REGIONAL_PRICE", TypeName = "NUMBER(22)")]
    public decimal? RegionalPrice { get; set; }

    /// <summary>
    /// آیا همه هزینه های قانونی سند پرداخت شده است؟
    /// </summary>
    [Column("IS_COST_PAYMENT_CONFIRMED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsCostPaymentConfirmed { get; set; }

    /// <summary>
    /// تاریخ پرداخت هزینه ها
    /// </summary>
    [Column("COST_PAYMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CostPaymentDate { get; set; }

    /// <summary>
    /// زمان پرداخت هزینه ها
    /// </summary>
    [Column("COST_PAYMENT_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string CostPaymentTime { get; set; }

    /// <summary>
    /// نوع پرداخت
    /// </summary>
    [Column("PAYMENT_TYPE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string PaymentType { get; set; }

    /// <summary>
    /// شیوه پرداخت
    /// </summary>
    [Column("HOW_TO_PAY")]
    [StringLength(1000)]
    [Unicode(false)]
    public string HowToPay { get; set; }

    /// <summary>
    /// شناسه قبض پرداخت
    /// </summary>
    [Column("BILL_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string BillNo { get; set; }

    /// <summary>
    /// شماره مرجع تراكنش یا شناسه پرداخت
    /// </summary>
    [Column("RECEIPT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string ReceiptNo { get; set; }

    /// <summary>
    /// شماره تراكنش/فیش
    /// </summary>
    [Column("FACTOR_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string FactorNo { get; set; }

    /// <summary>
    /// تاریخ تراكنش/فیش
    /// </summary>
    [Column("FACTOR_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FactorDate { get; set; }

    /// <summary>
    /// كلید اصلی ركورد استرداد
    /// </summary>
    [Column("REFUND_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string RefundId { get; set; }

    /// <summary>
    /// مبلغ استرداد حق الثبت غیركاداستر
    /// </summary>
    [Column("REFUND_PRICE")]
    [Precision(12)]
    public long? RefundPrice { get; set; }

    /// <summary>
    /// مبلغ استرداد شده حق الثبت كاداستر
    /// </summary>
    [Column("REFUND_PRICE_HAGHOSABT_CADASTRE")]
    [Precision(12)]
    public long? RefundPriceHaghosabtCadastre { get; set; }

    /// <summary>
    /// مبلغ استرداد شده افزایش نیم درصد حق الثبت
    /// </summary>
    [Column("REFUND_PRICE_HAGHOSABT_HALF_PRCNT")]
    [Precision(12)]
    public long? RefundPriceHaghosabtHalfPrcnt { get; set; }

    /// <summary>
    /// ملاحظات استرداد
    /// </summary>
    [Column("REFUND_DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string RefundDescription { get; set; }

    /// <summary>
    /// وضعیت استرداد
    /// </summary>
    [Column("REFUND_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string RefundState { get; set; }

    /// <summary>
    /// آیا همه هزینه های قانونی سند محاسبه شده است؟
    /// </summary>
    [Column("IS_COST_CALCULATE_CONFIRMED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsCostCalculateConfirmed { get; set; }

    /// <summary>
    /// آیا وضعیت احراز هویت اشخاص با ثبت احوال و كنترل بخشنامه ها بررسی و رویت شده است؟
    /// </summary>
    [Column("IS_FINAL_VERIFICATION_VISITED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFinalVerificationVisited { get; set; }

    /// <summary>
    /// آیا سند نقل و انتقال وسیله نقلیه برای وزارت راه آماده سازی شده است؟
    /// </summary>
    [Column("IS_RAH_PROCESSED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRahProcessed { get; set; }

    /// <summary>
    /// آیا خلاصه معامله برای سازمان مالیاتی ارسال شده است؟
    /// </summary>
    [Column("IS_SENT_TO_TAX_ORGANIZATION")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSentToTaxOrganization { get; set; }

    /// <summary>
    /// وضعیت پرونده
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// تاریخ پرونده به میلادی
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

    /// <summary>
    /// Information Lifecycle Management
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// آیا بصورت غیرحضوری درخواست شده است؟
    /// </summary>
    [Column("IS_REMOTE_REQUEST")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRemoteRequest { get; set; }

    /// <summary>
    /// شناسه درخواست غیرحضوری
    /// </summary>
    [Column("REMOTE_REQUEST_ID")]
    public Guid? RemoteRequestId { get; set; }

    /// <summary>
    /// شماره ترتیب - رزرو شده
    /// </summary>
    [Column("CLASSIFY_NO_RESERVED")]
    [Precision(6)]
    public int? ClassifyNoReserved { get; set; }

    /// <summary>
    /// تاریخ ورود به دفتر - رزرو شده
    /// </summary>
    [Column("WRITE_IN_BOOK_DATE_RESERVED")]
    [StringLength(10)]
    [Unicode(false)]
    public string WriteInBookDateReserved { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_RELATED_DOC_ABROAD_COUNTRY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldRelatedDocAbroadCountryId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_RELATED_DOCUMENT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldRelatedDocumentId { get; set; }

    [InverseProperty("NewNotaryDocument")]
    public virtual ICollection<DealSummary> DealSummaries { get; set; } = new List<DealSummary>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentCase> DocumentCases { get; set; } = new List<DocumentCase>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentCostUnchanged> DocumentCostUnchangeds { get; set; } = new List<DocumentCostUnchanged>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentCost> DocumentCosts { get; set; } = new List<DocumentCost>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentEstateDealSummaryGenerated> DocumentEstateDealSummaryGenerateds { get; set; } = new List<DocumentEstateDealSummaryGenerated>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentEstateDealSummarySelected> DocumentEstateDealSummarySelecteds { get; set; } = new List<DocumentEstateDealSummarySelected>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentEstateDealSummarySeparation> DocumentEstateDealSummarySeparations { get; set; } = new List<DocumentEstateDealSummarySeparation>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentEstateSeparationPiece> DocumentEstateSeparationPieces { get; set; } = new List<DocumentEstateSeparationPiece>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentEstate> DocumentEstates { get; set; } = new List<DocumentEstate>();

    [InverseProperty("Document")]
    public virtual DocumentFile DocumentFile { get; set; }

    [InverseProperty("Document")]
    public virtual DocumentInfoConfirm DocumentInfoConfirm { get; set; }

    [InverseProperty("Document")]
    public virtual DocumentInfoJudgement DocumentInfoJudgement { get; set; }

    [InverseProperty("Document")]
    public virtual DocumentInfoOther DocumentInfoOther { get; set; }

    [InverseProperty("Document")]
    public virtual DocumentInfoText DocumentInfoText { get; set; }

    [InverseProperty("Document")]
    public virtual ICollection<DocumentInquiry> DocumentInquiries { get; set; } = new List<DocumentInquiry>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentLimit> DocumentLimits { get; set; } = new List<DocumentLimit>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentMessage> DocumentMessages { get; set; } = new List<DocumentMessage>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentPayment> DocumentPayments { get; set; } = new List<DocumentPayment>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentPerson> DocumentPeople { get; set; } = new List<DocumentPerson>();

    [InverseProperty("AgentDocument")]
    public virtual ICollection<DocumentPersonRelated> DocumentPersonRelatedAgentDocuments { get; set; } = new List<DocumentPersonRelated>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentPersonRelated> DocumentPersonRelatedDocuments { get; set; } = new List<DocumentPersonRelated>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentRelation> DocumentRelationDocuments { get; set; } = new List<DocumentRelation>();

    [InverseProperty("RelatedDocument")]
    public virtual ICollection<DocumentRelation> DocumentRelationRelatedDocuments { get; set; } = new List<DocumentRelation>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentSemaphore> DocumentSemaphores { get; set; } = new List<DocumentSemaphore>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentSm> DocumentSms { get; set; } = new List<DocumentSm>();

    [InverseProperty("Document")]
    public virtual ICollection<DocumentSpecialChange> DocumentSpecialChanges { get; set; } = new List<DocumentSpecialChange>();

    [ForeignKey("DocumentTypeId")]
    [InverseProperty("DocumentDocumentTypes")]
    public virtual DocumentType DocumentType { get; set; }

    [InverseProperty("Document")]
    public virtual ICollection<DocumentVehicle> DocumentVehicles { get; set; } = new List<DocumentVehicle>();

    [InverseProperty("Document")]
    public virtual ICollection<EstateDocumentRequest> EstateDocumentRequests { get; set; } = new List<EstateDocumentRequest>();

    [InverseProperty("RelatedDocument")]
    public virtual ICollection<Document> InverseRelatedDocument { get; set; } = new List<Document>();

    [ForeignKey("RelatedDocumentId")]
    [InverseProperty("InverseRelatedDocument")]
    public virtual Document RelatedDocument { get; set; }

    [ForeignKey("RelatedDocumentTypeId")]
    [InverseProperty("DocumentRelatedDocumentTypes")]
    public virtual DocumentType RelatedDocumentType { get; set; }

    [InverseProperty("Document")]
    public virtual ICollection<SsrDocModifyClassifyNo> SsrDocModifyClassifyNos { get; set; } = new List<SsrDocModifyClassifyNo>();

    [InverseProperty("Document")]
    public virtual ICollection<SsrDocumentAsset> SsrDocumentAssets { get; set; } = new List<SsrDocumentAsset>();
}
