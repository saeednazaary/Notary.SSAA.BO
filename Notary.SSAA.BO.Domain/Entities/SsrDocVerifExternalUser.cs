using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جدول کاربران خارجی مجاز به فراخوانی سرویس تصدیق اصالت سند
/// </summary>
[Table("SSR_DOC_VERIF_EXTERNAL_USER")]
[Index("SsrApiExternalUserId", Name = "IX_SSR_DOC_VERIF_EXT_USR_ID")]
public partial class SsrDocVerifExternalUser
{
    /// <summary>
    /// شناسه کاربر خارج از سامانه
    /// </summary>
    [Required]
    [Column("SSR_API_EXTERNAL_USER_ID")]
    [StringLength(20)]
    [Unicode(false)]
    public string SsrApiExternalUserId { get; set; }

    /// <summary>
    /// مجاز بودن/نبودن برای فراخوانی سرویس
    /// </summary>
    [Required]
    [Column("IS_ACTIVE")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsActive { get; set; }

    [Required]
    [Column("METHOD_NAME")]
    [StringLength(1000)]
    [Unicode(false)]
    public string MethodName { get; set; }

    /// <summary>
    /// مجاز بودن/نبودن برای دریافت فایل پی دی اف سند مربوطه
    /// </summary>
    [Required]
    [Column("ALLOW_SHOW_PDF")]
    [StringLength(1)]
    [Unicode(false)]
    public string AllowShowPdf { get; set; }

    /// <summary>
    /// مجاز بودن/نبودن برای دریافت فایل پی دی اف دارای امضای الکترونیک سند مربوطه
    /// </summary>
    [Required]
    [Column("ALLOW_SHOW_SIGN_PDF")]
    [StringLength(1)]
    [Unicode(false)]
    public string AllowShowSignPdf { get; set; }

    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(20)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// مجاز بودن/نبودن برای تصدیق اصالت همه انواع سند
    /// </summary>
    [Required]
    [Column("ALLOW_FOR_ALL_DOC_TYPES")]
    [StringLength(1)]
    [Unicode(false)]
    public string AllowForAllDocTypes { get; set; }

    /// <summary>
    /// شناسه نوع سند های مجاز برای تصدیق اصالت
    /// </summary>
    [Column("ALLOWED_DOC_TYPES_ID")]
    [StringLength(1200)]
    [Unicode(false)]
    public string AllowedDocTypesId { get; set; }

    [ForeignKey("SsrApiExternalUserId")]
    [InverseProperty("SsrDocVerifExternalUsers")]
    public virtual SsrApiExternalUser SsrApiExternalUser { get; set; }
}
