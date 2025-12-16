using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع اموال در اجرا
/// </summary>
[Table("EXECUTIVE_WEALTH_TYPE")]
[Index("State", Name = "IDX_EXECUTIVE_WEALTH_TYPE###STATE")]
[Index("WealthType", Name = "IDX_EXECUTIVE_WEALTH_TYPE###WEALTH_TYPE")]
[Index("Code", Name = "UDX_EXECUTIVE_WEALTH_TYPE###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_EXECUTIVE_WEALTH_TYPE###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_EXECUTIVE_WEALTH_TYPE###TITLE", IsUnique = true)]
public partial class ExecutiveWealthType
{
    /// <summary>
    /// شناسه
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
    [StringLength(100)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// منقول/غیرمنقول
    /// </summary>
    [Column("WEALTH_TYPE")]
    [Precision(3)]
    public byte WealthType { get; set; }

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

    [InverseProperty("ExecutiveWealthType")]
    public virtual ICollection<ExecutiveSupportAsset> ExecutiveSupportAssets { get; set; } = new List<ExecutiveSupportAsset>();

    [InverseProperty("ExecutiveWealthType")]
    public virtual ICollection<ExecutiveWealthFieldType> ExecutiveWealthFieldTypes { get; set; } = new List<ExecutiveWealthFieldType>();
}
