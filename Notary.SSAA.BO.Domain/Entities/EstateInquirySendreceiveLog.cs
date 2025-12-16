using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سوابق ارسال و دریافت پاسخ
/// </summary>
[Table("ESTATE_INQUIRY_SENDRECEIVE_LOG")]
[Index("EstateInquiryActionTypeId", Name = "IDX_ESTATE_INQUIRY_SENDRECEIVE_LOG###ESTATE_INQUIRY_ACTION_TYPE_ID")]
[Index("EstateInquiryId", Name = "IDX_ESTATE_INQUIRY_SENDRECEIVE_LOG###ESTATE_INQUIRY_ID")]
[Index("LegacyId", Name = "IDX_ESTATE_INQUIRY_SENDRECEIVE_LOG###LEGACY_ID", IsUnique = true)]
[Index("ScriptoriumId", Name = "IDX_ESTATE_INQUIRY_SENDRECEIVE_LOG###SCRIPTORIUM_ID")]
[Index("WorkflowStatesId", Name = "IDX_ESTATE_INQUIRY_SENDRECEIVE_LOG###WORKFLOW_STATES_ID")]
public partial class EstateInquirySendreceiveLog
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// تاریخ اقدام
    /// </summary>
    [Required]
    [Column("ACTION_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ActionDate { get; set; }

    /// <summary>
    /// شماره اقدام
    /// </summary>
    [Column("ACTION_NUMBER")]
    [StringLength(50)]
    [Unicode(false)]
    public string ActionNumber { get; set; }

    /// <summary>
    /// متن اقدام
    /// </summary>
    [Column("ACTION_TEXT", TypeName = "CLOB")]
    public string ActionText { get; set; }

    /// <summary>
    /// زمان اقدام
    /// </summary>
    [Required]
    [Column("ACTION_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ActionTime { get; set; }

    /// <summary>
    /// ردیف نوع اقدام
    /// </summary>
    [Required]
    [Column("ESTATE_INQUIRY_ACTION_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string EstateInquiryActionTypeId { get; set; }

    /// <summary>
    /// ردیف استعلام ملك
    /// </summary>
    [Column("ESTATE_INQUIRY_ID")]
    public Guid EstateInquiryId { get; set; }

    /// <summary>
    /// ردیف عامل پارتیشن
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

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
    /// شناسه گردش کار
    /// </summary>
    [Required]
    [Column("WORKFLOW_STATES_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string WorkflowStatesId { get; set; }

    [ForeignKey("EstateInquiryId")]
    [InverseProperty("EstateInquirySendreceiveLogs")]
    public virtual EstateInquiry EstateInquiry { get; set; }

    [ForeignKey("EstateInquiryActionTypeId")]
    [InverseProperty("EstateInquirySendreceiveLogs")]
    public virtual EstateInquiryActionType EstateInquiryActionType { get; set; }

    [ForeignKey("WorkflowStatesId")]
    [InverseProperty("EstateInquirySendreceiveLogs")]
    public virtual WorkflowState WorkflowStates { get; set; }
}
