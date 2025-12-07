using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// شناسه
/// </summary>
[Table("INQUIRY_TFA_LOG")]
public partial class InquiryTfaLog
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه فرم
    /// </summary>
    [Required]
    [Column("FORM_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string FormId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("OBJECT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string ObjectId { get; set; }

    /// <summary>
    /// وضعيت مالکيت خط موبايل با شاهکار
    /// </summary>
    [Column("MOBILE_NO_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string MobileNoState { get; set; }

    /// <summary>
    /// وضعيت کد دو عاملي
    /// </summary>
    [Column("TFA_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string TfaState { get; set; }

    /// <summary>
    /// تاریخ ارسال پیامك عامل دوم
    /// </summary>
    [Column("TFA_SEND_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string TfaSendDate { get; set; }

    /// <summary>
    /// زمان ارسال پیامك عامل دوم
    /// </summary>
    [Column("TFA_SEND_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string TfaSendTime { get; set; }

    /// <summary>
    /// تاریخ اعتبارسنجی عامل دوم اعلام شده توسط شخص
    /// </summary>
    [Column("TFA_VALIDATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string TfaValidateDate { get; set; }

    /// <summary>
    /// زمان اعتبارسنجی عامل دوم اعلام شده توسط شخص
    /// </summary>
    [Column("TFA_VALIDATE_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string TfaValidateTime { get; set; }

    /// <summary>
    /// مقدار عامل دوم
    /// </summary>
    [Column("TFA_VALUE")]
    [StringLength(2000)]
    [Unicode(false)]
    public string TfaValue { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }
}
