using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع کاربری ملک برای استعلام ماده 6 قانون الزام
/// </summary>
[Table("SSR_ARTICLE6_ESTATE_USING")]
public partial class SsrArticle6EstateUsing
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(20)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// کد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(20)]
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
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    [InverseProperty("EstateUsing")]
    public virtual ICollection<SsrArticle6Inq> SsrArticle6Inqs { get; set; } = new List<SsrArticle6Inq>();
}
