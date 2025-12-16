using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جدول استان مرتبط با استعلام ماده 6 قانون الزام مطابق با تقسیمات سیاسی وزارت كشور
/// </summary>
[Table("SSR_ARTICLE6_PROVINCE")]
[Index("Nid", Name = "IX_SSR_ARTICLE6_PROVINCE_NID")]
public partial class SsrArticle6Province
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// کد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(2000)]
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
    /// شناسه ملی استان
    /// </summary>
    [Required]
    [Column("NID")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nid { get; set; }

    [InverseProperty("Province")]
    public virtual ICollection<SsrArticle6Inq> SsrArticle6Inqs { get; set; } = new List<SsrArticle6Inq>();
}
