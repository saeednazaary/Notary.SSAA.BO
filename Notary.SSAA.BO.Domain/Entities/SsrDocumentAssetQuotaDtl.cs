using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جزئیات سهم بندی فروشنده و خریدار از سایر اموال منقول مورد معامله در سند
/// </summary>
[Table("SSR_DOCUMENT_ASSET_QUOTA_DTLS")]
[Index("SsrDocumentAssetId", Name = "IX_SSR_DOCASTQTADTLS_ASSETID")]
[Index("DocumentPersonBuyerId", Name = "IX_SSR_DOCASTQTADTLS_BUYERID")]
[Index("Ilm", Name = "IX_SSR_DOCASTQTADTLS_ILM")]
[Index("ScriptoriumId", Name = "IX_SSR_DOCASTQTADTLS_SCRPTRMID")]
[Index("DocumentPersonSellerId", Name = "IX_SSR_DOCASTQTADTLS_SELLERID")]
[Index("LegacyId", Name = "UX_SSR_DOCASTQTADTLS_LEGACYID", IsUnique = true)]
public partial class SsrDocumentAssetQuotaDtl
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه سایر اموال منقول ثبت شده در سند
    /// </summary>
    [Column("SSR_DOCUMENT_ASSET_ID")]
    public Guid SsrDocumentAssetId { get; set; }

    /// <summary>
    /// شناسه شخص فروشنده در سند
    /// </summary>
    [Column("DOCUMENT_PERSON_SELLER_ID")]
    public Guid? DocumentPersonSellerId { get; set; }

    /// <summary>
    /// شناسه شخص خریدار در سند
    /// </summary>
    [Column("DOCUMENT_PERSON_BUYER_ID")]
    public Guid? DocumentPersonBuyerId { get; set; }

    /// <summary>
    /// جزء سهم مورد مالكیت
    /// </summary>
    [Column("OWNERSHIP_DETAIL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? OwnershipDetailQuota { get; set; }

    /// <summary>
    /// كل سهم مورد مالكیت
    /// </summary>
    [Column("OWNERSHIP_TOTAL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? OwnershipTotalQuota { get; set; }

    /// <summary>
    /// جزء سهم مورد معامله
    /// </summary>
    [Column("SELL_DETAIL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? SellDetailQuota { get; set; }

    /// <summary>
    /// كل سهم مورد معامله
    /// </summary>
    [Column("SELL_TOTAL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? SellTotalQuota { get; set; }

    /// <summary>
    /// متن سهم مورد معامله
    /// </summary>
    [Column("QUOTA_TEXT")]
    [StringLength(2000)]
    [Unicode(false)]
    public string QuotaText { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده
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

    [ForeignKey("DocumentPersonBuyerId")]
    [InverseProperty("SsrDocumentAssetQuotaDtlDocumentPersonBuyers")]
    public virtual DocumentPerson DocumentPersonBuyer { get; set; }

    [ForeignKey("DocumentPersonSellerId")]
    [InverseProperty("SsrDocumentAssetQuotaDtlDocumentPersonSellers")]
    public virtual DocumentPerson DocumentPersonSeller { get; set; }

    [ForeignKey("SsrDocumentAssetId")]
    [InverseProperty("SsrDocumentAssetQuotaDtls")]
    public virtual SsrDocumentAsset SsrDocumentAsset { get; set; }
}
