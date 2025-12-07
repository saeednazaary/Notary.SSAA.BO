using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سایر اموال منقول ثبت شده در اسناد
/// </summary>
[Table("SSR_DOCUMENT_ASSET")]
[Index("DocumentAssetTypeId", Name = "IX_SSR_DOC_ASSET_DOCASTYPID")]
[Index("DocumentId", "RowNo", Name = "IX_SSR_DOC_ASSET_DOCID_ROWNO")]
[Index("Ilm", Name = "IX_SSR_DOC_ASSET_ILM")]
[Index("OwnershipType", Name = "IX_SSR_DOC_ASSET_OWNERSHIPTYPE")]
[Index("RowNo", Name = "IX_SSR_DOC_ASSET_ROW_NO")]
[Index("ScriptoriumId", Name = "IX_SSR_DOC_ASSET_SCRIPTORIUMID")]
[Index("LegacyId", Name = "UX_SSR_DOC_ASSET_LEGACY_ID", IsUnique = true)]
public partial class SsrDocumentAsset
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه سند
    /// </summary>
    [Column("DOCUMENT_ID")]
    public Guid DocumentId { get; set; }

    /// <summary>
    /// ردیف
    /// </summary>
    [Column("ROW_NO")]
    [Precision(5)]
    public short RowNo { get; set; }

    /// <summary>
    /// شناسه نوع سایر اموال منقول مندرج در اسناد رسمی
    /// </summary>
    [Required]
    [Column("DOCUMENT_ASSET_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string DocumentAssetTypeId { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Column("TITLE")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// آيا مورد معامله شامل بدنه شناور مي شود؟
    /// </summary>
    [Column("SHIP_IS_BODY_INCLUDED")]
    [StringLength(1)]
    [Unicode(false)]
    public string ShipIsBodyIncluded { get; set; }

    /// <summary>
    /// آيا مورد معامله شامل موتور شناور مي شود؟
    /// </summary>
    [Column("SHIP_IS_ENGINE_INCLUDED")]
    [StringLength(1)]
    [Unicode(false)]
    public string ShipIsEngineIncluded { get; set; }

    /// <summary>
    /// شماره ثبت شناور
    /// </summary>
    [Column("SHIP_REGISTER_NO")]
    [StringLength(1000)]
    [Unicode(false)]
    public string ShipRegisterNo { get; set; }

    /// <summary>
    /// شماره سريال موتور شناور
    /// </summary>
    [Column("SHIP_ENGINE_SERIAL_NO")]
    [StringLength(1000)]
    [Unicode(false)]
    public string ShipEngineSerialNo { get; set; }

    /// <summary>
    /// نوع موتور شناور
    /// </summary>
    [Column("SHIP_ENGINE_TYPE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string ShipEngineType { get; set; }

    /// <summary>
    /// قدرت موتور شناور
    /// </summary>
    [Column("SHIP_ENGINE_POWER")]
    [StringLength(1000)]
    [Unicode(false)]
    public string ShipEnginePower { get; set; }

    /// <summary>
    /// نام شناور
    /// </summary>
    [Column("SHIP_NAME")]
    [StringLength(1000)]
    [Unicode(false)]
    public string ShipName { get; set; }

    /// <summary>
    /// نوع شناور
    /// </summary>
    [Column("SHIP_TYPE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string ShipType { get; set; }

    /// <summary>
    /// بندر شناور
    /// </summary>
    [Column("SHIP_PORT")]
    [StringLength(1000)]
    [Unicode(false)]
    public string ShipPort { get; set; }

    /// <summary>
    /// ابعاد شناور
    /// </summary>
    [Column("SHIP_DIMENSIONS")]
    [StringLength(1000)]
    [Unicode(false)]
    public string ShipDimensions { get; set; }

    /// <summary>
    /// ظرفيت شناور
    /// </summary>
    [Column("SHIP_CAPACITY")]
    [StringLength(1000)]
    [Unicode(false)]
    public string ShipCapacity { get; set; }

    /// <summary>
    /// مشخصات موتورهاي شناور در پاسخ استعلام
    /// </summary>
    [Column("SHIP_ENGINES_INFO")]
    [StringLength(2000)]
    [Unicode(false)]
    public string ShipEnginesInfo { get; set; }

    /// <summary>
    /// مشخصات مالکين شناور در پاسخ استعلام
    /// </summary>
    [Column("SHIP_OWNERS_INFO")]
    [StringLength(2000)]
    [Unicode(false)]
    public string ShipOwnersInfo { get; set; }

    /// <summary>
    /// شماره سند حج
    /// </summary>
    [Column("HAJ_DOCUMENT_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string HajDocumentNo { get; set; }

    /// <summary>
    /// شماره مجوز حج و زيارت
    /// </summary>
    [Column("HAJ_PERMISSION_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string HajPermissionNo { get; set; }

    /// <summary>
    /// کد شعبه حج و زيارت
    /// </summary>
    [Column("HAJ_BRANCH_CODE")]
    [StringLength(100)]
    [Unicode(false)]
    public string HajBranchCode { get; set; }

    /// <summary>
    /// اطلاعات دريافتي از سازمان حج و زيارت در مورد فيش حج
    /// </summary>
    [Column("HAJ_INQUIRY_INFO")]
    [StringLength(2000)]
    [Unicode(false)]
    public string HajInquiryInfo { get; set; }

    /// <summary>
    /// مبلغ سند
    /// </summary>
    [Column("PRICE")]
    [Precision(15)]
    public long? Price { get; set; }

    /// <summary>
    /// نوع مالكیت
    /// </summary>
    [Column("OWNERSHIP_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string OwnershipType { get; set; }

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
    /// متن سهم
    /// </summary>
    [Column("QUOTA_TEXT")]
    [StringLength(2000)]
    [Unicode(false)]
    public string QuotaText { get; set; }

    /// <summary>
    /// ملاحظات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Description { get; set; }

    /// <summary>
    /// Information Lifecycle Management
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده سند
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("SsrDocumentAssets")]
    public virtual Document Document { get; set; }

    [ForeignKey("DocumentAssetTypeId")]
    [InverseProperty("SsrDocumentAssets")]
    public virtual DocumentAssetType DocumentAssetType { get; set; }

    [InverseProperty("SsrDocumentAsset")]
    public virtual ICollection<SsrDocumentAssetQuotum> SsrDocumentAssetQuota { get; set; } = new List<SsrDocumentAssetQuotum>();

    [InverseProperty("SsrDocumentAsset")]
    public virtual ICollection<SsrDocumentAssetQuotaDtl> SsrDocumentAssetQuotaDtls { get; set; } = new List<SsrDocumentAssetQuotaDtl>();
}
