using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سازمان استعلام شونده ماده 6
/// </summary>
[Table("SSR_ARTICLE6_SUB_ORGAN")]
[Index("SsrArticle6OrganId", Name = "IX_SSR_ART6_SUB_ORG_PAR_ID")]
public partial class SsrArticle6SubOrgan
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
    /// شناسه وزارت خانه
    /// </summary>
    [Required]
    [Column("SSR_ARTICLE6_ORGAN_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string SsrArticle6OrganId { get; set; }

    /// <summary>
    /// كد
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
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    [InverseProperty("SsrArticle6SubOrgan")]
    public virtual ICollection<SsrArticle6InqReceiverOrg> SsrArticle6InqReceiverOrgs { get; set; } = new List<SsrArticle6InqReceiverOrg>();

    [InverseProperty("SenderOrg")]
    public virtual ICollection<SsrArticle6InqResponse> SsrArticle6InqResponses { get; set; } = new List<SsrArticle6InqResponse>();

    [InverseProperty("SsrArticle6SubOrgan")]
    public virtual ICollection<SsrArticle6OppositReason> SsrArticle6OppositReasons { get; set; } = new List<SsrArticle6OppositReason>();

    [ForeignKey("SsrArticle6OrganId")]
    [InverseProperty("SsrArticle6SubOrgans")]
    public virtual SsrArticle6Organ SsrArticle6Organ { get; set; }
}
