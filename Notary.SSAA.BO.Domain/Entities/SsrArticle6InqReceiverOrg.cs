using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// دستگاه های دریافت كننده استعلام
/// </summary>
[Table("SSR_ARTICLE6_INQ_RECEIVER_ORG")]
[Index("SsrArticle6SubOrganId", Name = "IX_SSR_ART6_INQ_REC_ORG_ID")]
[Index("ScriptoriumId", Name = "IX_SSR_ART6_INQ_REC_ORG_SCR_ID")]
[Index("SsrArticle6InqId", Name = "IX_SSR_ART6_REC_ORG_INQ_ID")]
public partial class SsrArticle6InqReceiverOrg
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه دستگاه مریوطه
    /// </summary>
    [Required]
    [Column("SSR_ARTICLE6_SUB_ORGAN_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string SsrArticle6SubOrganId { get; set; }

    /// <summary>
    /// شناسه استعلام
    /// </summary>
    [Column("SSR_ARTICLE6_INQ_ID")]
    public Guid SsrArticle6InqId { get; set; }

    /// <summary>
    /// كد رهگیری دستگاه مربوطه
    /// </summary>
    [Column("TRACKING_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string TrackingCode { get; set; }

    /// <summary>
    /// تاریخ ارسال استعلام به دستگاه مربوطه
    /// </summary>
    [Column("SEND_DATE")]
    [StringLength(50)]
    [Unicode(false)]
    public string SendDate { get; set; }

    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    [ForeignKey("SsrArticle6InqId")]
    [InverseProperty("SsrArticle6InqReceiverOrgs")]
    public virtual SsrArticle6Inq SsrArticle6Inq { get; set; }

    [ForeignKey("SsrArticle6SubOrganId")]
    [InverseProperty("SsrArticle6InqReceiverOrgs")]
    public virtual SsrArticle6SubOrgan SsrArticle6SubOrgan { get; set; }
}
