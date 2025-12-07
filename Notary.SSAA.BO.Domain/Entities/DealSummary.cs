using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// خلاصه معامله
/// </summary>
[Table("DEAL_SUMMARY")]
[Index("LegacyId", Name = "IDX_DEAL_SUMMARY_LEGACYID")]
[Index("ScriptoriumId", Name = "IDX_DEAL_SUMMARY_SCRIPTORIUM_ID")]
[Index("DealSummaryTransferTypeId", Name = "IX_SSR_DLSMRY_DEAL_TYPE_ID")]
[Index("EstateInquiryId", Name = "IX_SSR_DLSMRY_INQID")]
[Index("NewNotaryDocumentId", Name = "IX_SSR_DLSMRY_NEWDOCID")]
[Index("EstateOwnershipTypeId", Name = "IX_SSR_DLSMRY_OWNTYPEID")]
[Index("EstateTransitionTypeId", Name = "IX_SSR_DLSMRY_TRNTYPID")]
[Index("UnrestrictionTypeId", Name = "IX_SSR_DLSMRY_UNTYPID")]
[Index("WorkflowStatesId", Name = "IX_SSR_DLSMRY_WRKFLWSTID")]
public partial class DealSummary
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شماره سیستمی
    /// </summary>
    [Required]
    [Column("NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string No { get; set; }

    /// <summary>
    /// تاریخ انجام معامله
    /// </summary>
    [Required]
    [Column("TRANSACTION_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string TransactionDate { get; set; }

    /// <summary>
    /// تاریخ خلاصه معامله
    /// </summary>
    [Required]
    [Column("DEAL_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string DealDate { get; set; }

    /// <summary>
    /// شماره خلاصه معامله
    /// </summary>
    [Required]
    [Column("DEAL_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string DealNo { get; set; }

    /// <summary>
    /// تاریخ ارسال
    /// </summary>
    [Column("SEND_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SendDate { get; set; }

    /// <summary>
    /// زمان ارسال
    /// </summary>
    [Column("SEND_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SendTime { get; set; }

    /// <summary>
    /// مبلغ
    /// </summary>
    [Column("AMOUNT")]
    [Precision(18)]
    public long? Amount { get; set; }

    /// <summary>
    /// مدت
    /// </summary>
    [Column("DURATION", TypeName = "NUMBER(20)")]
    public decimal? Duration { get; set; }

    /// <summary>
    /// امضای دیجیتال
    /// </summary>
    [Column("DATA_DIGITAL_SIGNATURE", TypeName = "BLOB")]
    public byte[] DataDigitalSignature { get; set; }

    /// <summary>
    /// موضوع امضای دیجیتال
    /// </summary>
    [Column("SUBJECT_DN")]
    [StringLength(2000)]
    [Unicode(false)]
    public string SubjectDn { get; set; }

    /// <summary>
    /// تاریخ رفع محدودیت
    /// </summary>
    [Column("REMOVE_RESTRICTION_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string RemoveRestrictionDate { get; set; }

    /// <summary>
    /// شماره رفع محدودیت
    /// </summary>
    [Column("REMOVE_RESTRICTION_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string RemoveRestrictionNo { get; set; }

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
    /// شماره پاسخ
    /// </summary>
    [Column("RESPONSE_NUMBER")]
    [StringLength(50)]
    [Unicode(false)]
    public string ResponseNumber { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION", TypeName = "CLOB")]
    public string Description { get; set; }

    /// <summary>
    /// متن منضم
    /// </summary>
    [Column("ATTACHED_TEXT", TypeName = "CLOB")]
    public string AttachedText { get; set; }

    /// <summary>
    /// شناسه سند ثبت انی قدیم
    /// </summary>
    [Column("NOTARY_DOCUMENT_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string NotaryDocumentId { get; set; }

    /// <summary>
    /// ردیف استعلام 
    /// </summary>
    [Column("ESTATE_INQUIRY_ID")]
    public Guid EstateInquiryId { get; set; }

    /// <summary>
    /// شناسه سند در سیستم ثبت آنی جدید
    /// </summary>
    [Column("NEW_NOTARY_DOCUMENT_ID")]
    public Guid? NewNotaryDocumentId { get; set; }

    /// <summary>
    /// ردیف واحد مدت
    /// </summary>
    [Column("TIME_UNIT_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string TimeUnitId { get; set; }

    /// <summary>
    /// ردیف واحد مبلغ
    /// </summary>
    [Column("AMOUNT_UNIT_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string AmountUnitId { get; set; }

    /// <summary>
    /// ردیف عامل پارتیشن
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// ردیف نوع رفع محدودیت
    /// </summary>
    [Column("UNRESTRICTION_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string UnrestrictionTypeId { get; set; }

    /// <summary>
    /// ردیف نوع انتقال
    /// </summary>
    [Required]
    [Column("DEAL_SUMMARY_TRANSFER_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string DealSummaryTransferTypeId { get; set; }

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
    /// شناسه در سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شناسه گردش کار
    /// </summary>
    [Required]
    [Column("WORKFLOW_STATES_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string WorkflowStatesId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("ESTATE_TRANSITION_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string EstateTransitionTypeId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("ESTATE_OWNERSHIP_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string EstateOwnershipTypeId { get; set; }

    /// <summary>
    /// گواهی امضای دیجیتال
    /// </summary>
    [Column("CERTIFICATE_BASE64", TypeName = "CLOB")]
    public string CertificateBase64 { get; set; }

    /// <summary>
    /// متن پاسخ
    /// </summary>
    [Column("RESPONSE", TypeName = "CLOB")]
    public string Response { get; set; }

    [InverseProperty("DealSummary")]
    public virtual ICollection<DealSummaryPerson> DealSummaryPeople { get; set; } = new List<DealSummaryPerson>();

    [InverseProperty("DealSummary")]
    public virtual ICollection<DealSummarySendreceiveLog> DealSummarySendreceiveLogs { get; set; } = new List<DealSummarySendreceiveLog>();

    [ForeignKey("DealSummaryTransferTypeId")]
    [InverseProperty("DealSummaries")]
    public virtual DealSummaryTransferType DealSummaryTransferType { get; set; }

    [ForeignKey("EstateInquiryId")]
    [InverseProperty("DealSummaries")]
    public virtual EstateInquiry EstateInquiry { get; set; }

    [ForeignKey("EstateOwnershipTypeId")]
    [InverseProperty("DealSummaries")]
    public virtual EstateOwnershipType EstateOwnershipType { get; set; }

    [ForeignKey("EstateTransitionTypeId")]
    [InverseProperty("DealSummaries")]
    public virtual EstateTransitionType EstateTransitionType { get; set; }

    [ForeignKey("NewNotaryDocumentId")]
    [InverseProperty("DealSummaries")]
    public virtual Document NewNotaryDocument { get; set; }

    [ForeignKey("UnrestrictionTypeId")]
    [InverseProperty("DealSummaries")]
    public virtual DealsummaryUnrestrictionType UnrestrictionType { get; set; }

    [ForeignKey("WorkflowStatesId")]
    [InverseProperty("DealSummaries")]
    public virtual WorkflowState WorkflowStates { get; set; }
}
