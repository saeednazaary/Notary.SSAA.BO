using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع موضوعات تنظيمات ثبت الکترونيک اسناد رسمي
/// </summary>
[Table("SSR_CONFIG_SUBJECT")]
[Index("ConfigType", Name = "IX_SSR_CONFIG_SUBJ_CT")]
[Index("SsrConfigMainSubjectId", Name = "IX_SSR_CONFIG_SUBJ_MAINSUBJID")]
[Index("State", Name = "IX_SSR_CONFIG_SUBJ_STATE")]
[Index("Code", Name = "UX_SSR_CONFIG_SUBJ_CODE", IsUnique = true)]
[Index("Title", Name = "UX_SSR_CONFIG_SUBJ_TITLE", IsUnique = true)]
public partial class SsrConfigSubject
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه موضوع اصلي تنظيمات ثبت الکترونيک اسناد رسمي
    /// </summary>
    [Column("SSR_CONFIG_MAIN_SUBJECT_ID")]
    public Guid SsrConfigMainSubjectId { get; set; }

    /// <summary>
    /// نوع:        1_مقدار ثابت        2_شرط و نوع برخورد با موضوع
    /// </summary>
    [Required]
    [Column("CONFIG_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string ConfigType { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(200)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// نوع برخورد با تناقض احتمالي تنظيمات:       1: خوشبينانه عمل شود            2:بدبينانه عمل شود            3:آخرين کانفيگ اعمال شود
    /// </summary>
    [Required]
    [Column("CONFILICT_RESOLVE_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string ConfilictResolveType { get; set; }

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

    [ForeignKey("SsrConfigMainSubjectId")]
    [InverseProperty("SsrConfigSubjects")]
    public virtual SsrConfigMainSubject SsrConfigMainSubject { get; set; }

    [InverseProperty("SsrConfigSubject")]
    public virtual ICollection<SsrConfig> SsrConfigs { get; set; } = new List<SsrConfig>();
}
