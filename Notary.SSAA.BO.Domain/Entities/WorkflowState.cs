using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع وضعیت گردش موضوعات اطلاعاتی
/// </summary>
[Table("WORKFLOW_STATES")]
[Index("ColumnName", Name = "IDX_WORKFLOW_STATES###COLUMN_NAME")]
[Index("State", Name = "IDX_WORKFLOW_STATES###STATE")]
[Index("Title", Name = "IDX_WORKFLOW_STATES###TITLE")]
[Index("TableName", "ColumnName", "State", Name = "UDX_WORKFLOW_STATES###TABLE_NAME#COLUMN_NAME#STATE", IsUnique = true)]
[Index("TableName", "ColumnName", "Title", Name = "UDX_WORKFLOW_STATES###TABLE_NAME#COLUMN_NAME#TITLE", IsUnique = true)]
public partial class WorkflowState
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// نام انگلیسی جدول
    /// </summary>
    [Required]
    [Column("TABLE_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string TableName { get; set; }

    /// <summary>
    /// نام انگلیسی ستون وضعیت
    /// </summary>
    [Required]
    [Column("COLUMN_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string ColumnName { get; set; }

    /// <summary>
    /// مقدار وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(4)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// عنوان وضعیت
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(100)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION", TypeName = "CLOB")]
    public string Description { get; set; }

    [InverseProperty("WorkflowStates")]
    public virtual ICollection<DealSummary> DealSummaries { get; set; } = new List<DealSummary>();

    [InverseProperty("WorkflowStates")]
    public virtual ICollection<DealSummarySendreceiveLog> DealSummarySendreceiveLogs { get; set; } = new List<DealSummarySendreceiveLog>();

    [InverseProperty("WorkflowStates")]
    public virtual ICollection<EstateDocumentRequest> EstateDocumentRequests { get; set; } = new List<EstateDocumentRequest>();

    [InverseProperty("WorkflowStates")]
    public virtual ICollection<EstateInquiry> EstateInquiries { get; set; } = new List<EstateInquiry>();

    [InverseProperty("WorkflowStates")]
    public virtual ICollection<EstateInquirySendedSm> EstateInquirySendedSms { get; set; } = new List<EstateInquirySendedSm>();

    [InverseProperty("WorkflowStates")]
    public virtual ICollection<EstateInquirySendreceiveLog> EstateInquirySendreceiveLogs { get; set; } = new List<EstateInquirySendreceiveLog>();

    [InverseProperty("WorkflowStates")]
    public virtual ICollection<EstateTaxInquiry> EstateTaxInquiries { get; set; } = new List<EstateTaxInquiry>();

    [InverseProperty("WorkflowStates")]
    public virtual ICollection<EstateTaxInquirySendedSm> EstateTaxInquirySendedSms { get; set; } = new List<EstateTaxInquirySendedSm>();

    [InverseProperty("StateNavigation")]
    public virtual ICollection<ExecutiveRequest> ExecutiveRequests { get; set; } = new List<ExecutiveRequest>();

    [InverseProperty("StateNavigation")]
    public virtual ICollection<ExecutiveSupport> ExecutiveSupports { get; set; } = new List<ExecutiveSupport>();

    [InverseProperty("WorkflowStates")]
    public virtual ICollection<ForestorgInquiry> ForestorgInquiries { get; set; } = new List<ForestorgInquiry>();

    [InverseProperty("WorkflowStates")]
    public virtual ICollection<SsrArticle6Inq> SsrArticle6Inqs { get; set; } = new List<SsrArticle6Inq>();
}
