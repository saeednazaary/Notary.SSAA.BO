using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع سایر اموال منقول
/// </summary>
[Table("DOCUMENT_ASSET_TYPE")]
[Index("LegacyId", Name = "IDX_DOCUMENT_ASSET_TYPE###LEGACY_ID")]
[Index("ParentDocumentAssetTypeId", Name = "IDX_DOCUMENT_ASSET_TYPE###PARENT_DOCUMENT_ASSET_TYPE_ID")]
[Index("State", Name = "IDX_DOCUMENT_ASSET_TYPE###STATE")]
[Index("Code", Name = "UDX_DOCUMENT_ASSET_TYPE###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_DOCUMENT_ASSET_TYPE###TITLE", IsUnique = true)]
public partial class DocumentAssetType
{
    /// <summary>
    ///  ردیف
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(3)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(200)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// شناسه ركورد بالادست
    /// </summary>
    [Column("PARENT_DOCUMENT_ASSET_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string ParentDocumentAssetTypeId { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("DocumentAssetType")]
    public virtual ICollection<DocumentInfoOther> DocumentInfoOthers { get; set; } = new List<DocumentInfoOther>();

    [InverseProperty("ParentDocumentAssetType")]
    public virtual ICollection<DocumentAssetType> InverseParentDocumentAssetType { get; set; } = new List<DocumentAssetType>();

    [ForeignKey("ParentDocumentAssetTypeId")]
    [InverseProperty("InverseParentDocumentAssetType")]
    public virtual DocumentAssetType ParentDocumentAssetType { get; set; }

    [InverseProperty("DocumentAssetType")]
    public virtual ICollection<SsrDocumentAsset> SsrDocumentAssets { get; set; } = new List<SsrDocumentAsset>();
}
