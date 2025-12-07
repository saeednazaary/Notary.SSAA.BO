using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع استعلام از واحدهای ثبتی (مانند استعلام از اداره مالكیت معنوی)
/// </summary>
[Table("INQUIRY_FROM_UNIT_TYPE")]
[Index("State", Name = "IDX_INQUIRY_FROM_UNIT_TYPE###STATE")]
[Index("Code", Name = "UDX_INQUIRY_FROM_UNIT_TYPE###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_INQUIRY_FROM_UNIT_TYPE###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_INQUIRY_FROM_UNIT_TYPE###TITLE", IsUnique = true)]
public partial class InquiryFromUnitType
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
    [StringLength(100)]
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
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("InquiryFromUnitType")]
    public virtual ICollection<InquiryFromUnit> InquiryFromUnits { get; set; } = new List<InquiryFromUnit>();
}
