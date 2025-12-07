using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اقلام اطلاعاتی اموال معرفی شده در سایر تقاضاهای مربوط به اجرائیه برون سپاری شده به دفترخانه
/// </summary>
[Table("EXECUTIVE_SUPPORT_ASSET_FIELD")]
[Index("ExecutiveSupportAssetId", Name = "IDX_EXECUTIVE_SUPPORT_ASSET_FIELD###EXECUTIVE_SUPPORT_ASSET_ID")]
[Index("ExecutiveWealthFieldTypeId", Name = "IDX_EXECUTIVE_SUPPORT_ASSET_FIELD###EXECUTIVE_WEALTH_FIELD_TYPE_ID")]
[Index("Ilm", Name = "IDX_EXECUTIVE_SUPPORT_ASSET_FIELD###ILM")]
[Index("LegacyId", Name = "UDX_EXECUTIVE_SUPPORT_ASSET_FIELD###LEGACY_ID", IsUnique = true)]
public partial class ExecutiveSupportAssetField
{
    /// <summary>
    ///  ردیف
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه اموال معرفی شده در سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه
    /// </summary>
    [Column("EXECUTIVE_SUPPORT_ASSET_ID")]
    public Guid ExecutiveSupportAssetId { get; set; }

    /// <summary>
    /// شناسه انواع اقلام اطلاعاتی مربوط به یك مال
    /// </summary>
    [Required]
    [Column("EXECUTIVE_WEALTH_FIELD_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string ExecutiveWealthFieldTypeId { get; set; }

    /// <summary>
    /// تاریخ
    /// </summary>
    [Column("FIELD_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FieldDate { get; set; }

    /// <summary>
    /// شرح
    /// </summary>
    [Column("FIELD_DESCRIPTION", TypeName = "CLOB")]
    public string FieldDescription { get; set; }

    /// <summary>
    /// مقدار
    /// </summary>
    [Column("FIELD_VALUE")]
    [Precision(18)]
    public long? FieldValue { get; set; }

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
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("ExecutiveSupportAssetId")]
    [InverseProperty("ExecutiveSupportAssetFields")]
    public virtual ExecutiveSupportAsset ExecutiveSupportAsset { get; set; }
}
