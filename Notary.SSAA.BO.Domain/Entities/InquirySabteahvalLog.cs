using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// لاگ ثبت احوال
/// </summary>
[Table("INQUIRY_SABTEAHVAL_LOG")]
public partial class InquirySabteahvalLog
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
    /// آيا اطلاعات شخص با ثبت احوال تطابق دارد؟
    /// </summary>
    [Column("IS_SABTAHVAL_CORRECT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSabtahvalCorrect { get; set; }

    /// <summary>
    /// آيا زنده است؟
    /// </summary>
    [Column("IS_ALIVE")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsAlive { get; set; }

    /// <summary>
    /// تاریخ استعلام ثبت احوال
    /// </summary>
    [Column("SABTEAHVAL_INQUIRY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SabteahvalInquiryDate { get; set; }

    /// <summary>
    /// زمان استعلام ثبت احوال
    /// </summary>
    [Column("SABTEAHVAL_INQUIRY_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string SabteahvalInquiryTime { get; set; }

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
