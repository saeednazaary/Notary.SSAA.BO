using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// پیامك های ارسال شده برای استعلام مالیاتی
/// </summary>
[Table("ESTATE_INQUIRY_SENDED_SMS")]
[Index("EstateInquiryId", Name = "IX_SSR_EINQSNDSMS_INQID")]
[Index("WorkflowStatesId", Name = "IX_SSR_EINQSNDSMS_WSTATID")]
public partial class EstateInquirySendedSm
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("ESTATE_INQUIRY_ID")]
    public Guid EstateInquiryId { get; set; }

    /// <summary>
    /// تاريخ ارسال
    /// </summary>
    [Required]
    [Column("SEND_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SendDate { get; set; }

    /// <summary>
    /// زمان ارسال
    /// </summary>
    [Required]
    [Column("SEND_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string SendTime { get; set; }

    /// <summary>
    /// شماره تلفن همراه
    /// </summary>
    [Required]
    [Column("MOBILE_NO")]
    [StringLength(15)]
    [Unicode(false)]
    public string MobileNo { get; set; }

    /// <summary>
    /// پيام
    /// </summary>
    [Required]
    [Column("MESSAGE", TypeName = "CLOB")]
    public string Message { get; set; }

    /// <summary>
    /// شماره رهگيري پيامک
    /// </summary>
    [Required]
    [Column("SMS_TRACKING_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string SmsTrackingNo { get; set; }

    /// <summary>
    /// شناسه گردش کار
    /// </summary>
    [Required]
    [Column("WORKFLOW_STATES_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string WorkflowStatesId { get; set; }

    [ForeignKey("EstateInquiryId")]
    [InverseProperty("EstateInquirySendedSms")]
    public virtual EstateInquiry EstateInquiry { get; set; }

    [ForeignKey("WorkflowStatesId")]
    [InverseProperty("EstateInquirySendedSms")]
    public virtual WorkflowState WorkflowStates { get; set; }
}
