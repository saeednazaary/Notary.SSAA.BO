using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سوابق ارسال و دریافت پاسخ خلاصه معامله
/// </summary>
[Table("DEAL_SUMMARY_SENDRECEIVE_LOG")]
[Index("DealSummaryActionTypeId", Name = "IX_SSR_DLSMRY_SNDRCIVLG_ACTPID")]
[Index("DealSummaryId", Name = "IX_SSR_DLSMRY_SNDRCIVLG_SMRYID")]
[Index("WorkflowStatesId", Name = "IX_SSR_DLSMRY_SNDRCIVLG_WSTTID")]
public partial class DealSummarySendreceiveLog
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
    /// زمان اقدام
    /// </summary>
    [Required]
    [Column("ACTION_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ActionTime { get; set; }

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
    /// ردیف نوع خلاصه معامله
    /// </summary>
    [Required]
    [Column("DEAL_SUMMARY_ACTION_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string DealSummaryActionTypeId { get; set; }

    /// <summary>
    /// ردیف خلاصه معامله
    /// </summary>
    [Column("DEAL_SUMMARY_ID")]
    public Guid DealSummaryId { get; set; }

    /// <summary>
    /// فيلد عددي کنترلي مخصوص کنترل concurrency
    /// </summary>
    [Column("TIMESTAMP", TypeName = "NUMBER(20)")]
    public decimal Timestamp { get; set; }

    /// <summary>
    /// ردیف عامل پارتیشن
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه گردش کار
    /// </summary>
    [Required]
    [Column("WORKFLOW_STATES_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string WorkflowStatesId { get; set; }

    [ForeignKey("DealSummaryId")]
    [InverseProperty("DealSummarySendreceiveLogs")]
    public virtual DealSummary DealSummary { get; set; }

    [ForeignKey("DealSummaryActionTypeId")]
    [InverseProperty("DealSummarySendreceiveLogs")]
    public virtual DealSummaryActionType DealSummaryActionType { get; set; }

    [ForeignKey("WorkflowStatesId")]
    [InverseProperty("DealSummarySendreceiveLogs")]
    public virtual WorkflowState WorkflowStates { get; set; }
}
