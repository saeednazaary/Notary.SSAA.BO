using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جدول علل مخالفت ارگان های استعلام شونده استعلام ماده 6 قانون الزام
/// </summary>
[Table("SSR_ARTICLE6_OPPOSIT_REASON")]
[Index("SsrArticle6SubOrganId", Name = "IX_SSR_ART6_OPP_REAS_ORG_ID")]
public partial class SsrArticle6OppositReason
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
    /// كد
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
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// شناسه سازمان مرتبط
    /// </summary>
    [Required]
    [Column("SSR_ARTICLE6_SUB_ORGAN_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string SsrArticle6SubOrganId { get; set; }

    [InverseProperty("Opposition")]
    public virtual ICollection<SsrArticle6InqResponse> SsrArticle6InqResponses { get; set; } = new List<SsrArticle6InqResponse>();

    [ForeignKey("SsrArticle6SubOrganId")]
    [InverseProperty("SsrArticle6OppositReasons")]
    public virtual SsrArticle6SubOrgan SsrArticle6SubOrgan { get; set; }
}
