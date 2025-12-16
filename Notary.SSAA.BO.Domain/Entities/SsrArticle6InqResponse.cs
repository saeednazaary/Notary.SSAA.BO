using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// پاسخ استعلام ماده 6 قانون الزام
/// </summary>
[Table("SSR_ARTICLE6_INQ_RESPONSE")]
[Index("SsrArticle6InqId", Name = "IX_SSR_ART6_INQ_RES_INQID")]
[Index("OppositionId", Name = "IX_SSR_ART6_INQ_RES_OPTID")]
[Index("ScriptoriumId", Name = "IX_SSR_ART6_INQ_RES_SCRID")]
[Index("SenderOrgId", Name = "IX_SSR_ART6_INQ_RES_SORGID")]
public partial class SsrArticle6InqResponse
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// تاریخ پاسخ
    /// </summary>
    [Required]
    [Column("RESPONSE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ResponseDate { get; set; }

    /// <summary>
    /// زمان پاسخ
    /// </summary>
    [Required]
    [Column("RESPONSE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ResponseTime { get; set; }

    /// <summary>
    /// شماره پاسخ
    /// </summary>
    [Column("RESPONSE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string ResponseNo { get; set; }

    /// <summary>
    /// نوع پاسخ
    /// </summary>
    [Column("RESPONSE_TYPE", TypeName = "NUMBER(1)")]
    public bool ResponseType { get; set; }

    /// <summary>
    /// شرح
    /// </summary>
    [Column("DESCRIPTION", TypeName = "CLOB")]
    public string Description { get; set; }

    /// <summary>
    /// شناسه ركورد در جدول متناظر
    /// </summary>
    [Column("SSR_ARTICLE6_INQ_ID")]
    public Guid SsrArticle6InqId { get; set; }

    /// <summary>
    /// شناسه ركورد در جدول متناظر
    /// </summary>
    [Column("OPPOSITION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string OppositionId { get; set; }

    /// <summary>
    /// مپ
    /// </summary>
    [Column("ESTATE_MAP", TypeName = "CLOB")]
    public string EstateMap { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه ركورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// دستگاه گیرنده استعلام
    /// </summary>
    [Required]
    [Column("SENDER_ORG_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string SenderOrgId { get; set; }

    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    [ForeignKey("OppositionId")]
    [InverseProperty("SsrArticle6InqResponses")]
    public virtual SsrArticle6OppositReason Opposition { get; set; }

    [ForeignKey("SenderOrgId")]
    [InverseProperty("SsrArticle6InqResponses")]
    public virtual SsrArticle6SubOrgan SenderOrg { get; set; }

    [ForeignKey("SsrArticle6InqId")]
    [InverseProperty("SsrArticle6InqResponses")]
    public virtual SsrArticle6Inq SsrArticle6Inq { get; set; }
}
