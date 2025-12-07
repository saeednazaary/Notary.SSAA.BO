using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// گروه بندی انواع املاك مندرج در اسناد رسمی
/// </summary>
[Table("DOCUMENT_ESTATE_TYPE_GROUP")]
[Index("State", Name = "IDX_DOCUMENT_ESTATE_TYPE_GROUP###STATE")]
[Index("Code", Name = "UDX_DOCUMENT_ESTATE_TYPE_GROUP###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_DOCUMENT_ESTATE_TYPE_GROUP###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_DOCUMENT_ESTATE_TYPE_GROUP###TITLE", IsUnique = true)]
public partial class DocumentEstateTypeGroup
{
    /// <summary>
    ///  ردیف
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(1)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(1)]
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

    [InverseProperty("DocumentEstateTypeGroup")]
    public virtual ICollection<DocumentEstateType> DocumentEstateTypes { get; set; } = new List<DocumentEstateType>();
}
