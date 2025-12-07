using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// وزارت خانه استعلام شونده ماده 6
/// </summary>
[Table("SSR_ARTICLE6_ORGAN")]
public partial class SsrArticle6Organ
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
    [StringLength(15)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(1000)]
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

    [InverseProperty("SsrArticle6Organ")]
    public virtual ICollection<SsrArticle6InqReceiver> SsrArticle6InqReceivers { get; set; } = new List<SsrArticle6InqReceiver>();

    [InverseProperty("SsrArticle6Organ")]
    public virtual ICollection<SsrArticle6SubOrgan> SsrArticle6SubOrgans { get; set; } = new List<SsrArticle6SubOrgan>();
}
