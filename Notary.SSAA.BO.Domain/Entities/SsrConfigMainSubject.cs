using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع موضوعات اصلي تنظيمات ثبت الکترونيک اسناد رسمي
/// </summary>
[Table("SSR_CONFIG_MAIN_SUBJECT")]
[Index("State", Name = "IX_SSR_CONFIG_MAIN_SUBJ_STATE")]
[Index("Code", Name = "UX_SSR_CONFIG_MAIN_SUBJ_CODE", IsUnique = true)]
[Index("Title", Name = "UX_SSR_CONFIG_MAIN_SUBJ_TITLE", IsUnique = true)]
public partial class SsrConfigMainSubject
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(200)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// وضعيت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// کد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(100)]
    [Unicode(false)]
    public string Code { get; set; }

    [InverseProperty("SsrConfigMainSubject")]
    public virtual ICollection<SsrConfigSubject> SsrConfigSubjects { get; set; } = new List<SsrConfigSubject>();

    [InverseProperty("SsrConfigMainSubject")]
    public virtual ICollection<SsrConfig> SsrConfigs { get; set; } = new List<SsrConfig>();
}
