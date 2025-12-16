using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// گيرنده استعلام ماده 6
/// </summary>
[Table("SSR_ARTICLE6_INQ_RECEIVER")]
[Index("SsrArticle6InqId", Name = "IX_SSR_ART6_RCVR_INQID")]
[Index("SsrArticle6OrganId", Name = "IX_SSR_ART6_RCVR_ORGNID")]
public partial class SsrArticle6InqReceiver
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("SSR_ARTICLE6_INQ_ID")]
    public Guid SsrArticle6InqId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("SSR_ARTICLE6_ORGAN_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string SsrArticle6OrganId { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    [ForeignKey("SsrArticle6InqId")]
    [InverseProperty("SsrArticle6InqReceivers")]
    public virtual SsrArticle6Inq SsrArticle6Inq { get; set; }

    [ForeignKey("SsrArticle6OrganId")]
    [InverseProperty("SsrArticle6InqReceivers")]
    public virtual SsrArticle6Organ SsrArticle6Organ { get; set; }
}
