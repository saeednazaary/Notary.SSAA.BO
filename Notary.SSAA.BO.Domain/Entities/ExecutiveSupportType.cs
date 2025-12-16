using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع خدمات تبعی اجرائیه
/// </summary>
[Table("EXECUTIVE_SUPPORT_TYPE")]
[Index("State", Name = "IDX_EXECUTIVE_SUPPORT_TYPE###STATE")]
[Index("Code", Name = "UDX_EXECUTIVE_SUPPORT_TYPE###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_EXECUTIVE_SUPPORT_TYPE###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_EXECUTIVE_SUPPORT_TYPE###TITLE", IsUnique = true)]
public partial class ExecutiveSupportType
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(1000)]
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

    [InverseProperty("ExecutiveSupportType")]
    public virtual ICollection<ExecutiveSupport> ExecutiveSupports { get; set; } = new List<ExecutiveSupport>();
}
