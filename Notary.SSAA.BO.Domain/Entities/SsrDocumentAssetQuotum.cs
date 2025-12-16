using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سهم اصحاب سند از سایر اموال منقول مورد معامله در سند
/// </summary>
[Table("SSR_DOCUMENT_ASSET_QUOTA")]
[Index("SsrDocumentAssetId", "DocumentPersonId", Name = "IX_SSR_DOCASTQUOTA_DAID_PRSID")]
[Index("Ilm", Name = "IX_SSR_DOCASTQUOTA_ILM")]
[Index("DocumentPersonId", Name = "IX_SSR_DOCASTQUOTA_PRSID")]
[Index("ScriptoriumId", Name = "IX_SSR_DOCASTQUOTA_SCRPTORMID")]
[Index("LegacyId", Name = "UX_SSR_DOCASTQUOTA_LEGACYID", IsUnique = true)]
public partial class SsrDocumentAssetQuotum
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه سایر اموال منقول ثبت شده در اسناد
    /// </summary>
    [Column("SSR_DOCUMENT_ASSET_ID")]
    public Guid SsrDocumentAssetId { get; set; }

    /// <summary>
    /// شناسه اشخاص اسناد
    /// </summary>
    [Column("DOCUMENT_PERSON_ID")]
    public Guid DocumentPersonId { get; set; }

    /// <summary>
    /// جزء سهم مورد معامله
    /// </summary>
    [Column("DETAIL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? DetailQuota { get; set; }

    /// <summary>
    /// كل سهم مورد معامله
    /// </summary>
    [Column("TOTAL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? TotalQuota { get; set; }

    /// <summary>
    /// متن سهم
    /// </summary>
    [Column("QUOTA_TEXT")]
    [StringLength(200)]
    [Unicode(false)]
    public string QuotaText { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده سند
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// Information Lifecycle Management
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("DocumentPersonId")]
    [InverseProperty("SsrDocumentAssetQuota")]
    public virtual DocumentPerson DocumentPerson { get; set; }

    [ForeignKey("SsrDocumentAssetId")]
    [InverseProperty("SsrDocumentAssetQuota")]
    public virtual SsrDocumentAsset SsrDocumentAsset { get; set; }
}
