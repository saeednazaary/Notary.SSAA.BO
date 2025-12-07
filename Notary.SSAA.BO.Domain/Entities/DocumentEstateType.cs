using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع املاك مندرج در اسناد رسمی
/// </summary>
[Table("DOCUMENT_ESTATE_TYPE")]
[Index("CountUnitTitle", Name = "IDX_DOCUMENT_ESTATE_TYPE###COUNT_UNIT_TITLE")]
[Index("DocumentEstateTypeGroupId", Name = "IDX_DOCUMENT_ESTATE_TYPE###DOCUMENT_ESTATE_TYPE_GROUP_ID")]
[Index("LegacyId", Name = "IDX_DOCUMENT_ESTATE_TYPE###LEGACY_ID")]
[Index("State", Name = "IDX_DOCUMENT_ESTATE_TYPE###STATE")]
[Index("Code", Name = "UDX_DOCUMENT_ESTATE_TYPE###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_DOCUMENT_ESTATE_TYPE###TITLE", IsUnique = true)]
public partial class DocumentEstateType
{
    /// <summary>
    ///  ردیف
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(2)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(50)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// شناسه گروه بندی انواع املاك مندرج در اسناد رسمی
    /// </summary>
    [Required]
    [Column("DOCUMENT_ESTATE_TYPE_GROUP_ID")]
    [StringLength(1)]
    [Unicode(false)]
    public string DocumentEstateTypeGroupId { get; set; }

    /// <summary>
    /// واحد شمارش
    /// </summary>
    [Required]
    [Column("COUNT_UNIT_TITLE")]
    [StringLength(20)]
    [Unicode(false)]
    public string CountUnitTitle { get; set; }

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

    [ForeignKey("DocumentEstateTypeGroupId")]
    [InverseProperty("DocumentEstateTypes")]
    public virtual DocumentEstateTypeGroup DocumentEstateTypeGroup { get; set; }

    [InverseProperty("DocumentEstateType")]
    public virtual ICollection<DocumentEstate> DocumentEstates { get; set; } = new List<DocumentEstate>();
}
