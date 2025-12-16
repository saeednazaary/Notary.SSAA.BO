using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// لاگ ثنا
/// </summary>
[Table("INQUIRY_SANA_LOG")]
public partial class InquirySanaLog
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
    /// تاریخ استعلام ثنا
    /// </summary>
    [Column("SANA_INQUIRY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SanaInquiryDate { get; set; }

    /// <summary>
    /// زمان استعلام ثنا
    /// </summary>
    [Column("SANA_INQUIRY_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string SanaInquiryTime { get; set; }

    /// <summary>
    /// وضعيت ثنا
    /// </summary>
    [Column("SANA_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SanaState { get; set; }

    /// <summary>
    /// آيا در ثنا چارت سازماني دارد؟
    /// </summary>
    [Column("SANA_HAS_ORGANIZATION_CHART")]
    [StringLength(1)]
    [Unicode(false)]
    public string SanaHasOrganizationChart { get; set; }

    /// <summary>
    /// کد سازمان ثنا
    /// </summary>
    [Column("SANA_ORGANIZATION_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string SanaOrganizationCode { get; set; }

    /// <summary>
    /// نام سازمان ثنا
    /// </summary>
    [Column("SANA_ORGANIZATION_NAME")]
    [StringLength(1000)]
    [Unicode(false)]
    public string SanaOrganizationName { get; set; }

    /// <summary>
    /// شماره تلفن همراه در سامانه ثنا
    /// </summary>
    [Column("SANA_MOBILE_NO")]
    [StringLength(11)]
    [Unicode(false)]
    public string SanaMobileNo { get; set; }

    /// <summary>
    /// تاریخ ارسال پیام به كارتابل ثنا
    /// </summary>
    [Column("SANA_NOTIFICATION_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SanaNotificationDate { get; set; }

    /// <summary>
    /// زمان ارسال پیام به كارتابل ثنا
    /// </summary>
    [Column("SANA_NOTIFICATION_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string SanaNotificationTime { get; set; }

    /// <summary>
    /// كد رهگیری ارسال پیام به كارتابل ثنا
    /// </summary>
    [Column("SANA_NOTIFICATION_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string SanaNotificationCode { get; set; }

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
