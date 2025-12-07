using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// واحدهاي ثبتي مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي
/// </summary>
[Table("SSR_CONFIG_CONDITION_UNIT")]
[Index("UnitLevelCode", Name = "IX_SSR_CONFIG_CNDITN_UNIT_UID")]
[Index("SsrConfigId", "UnitLevelCode", Name = "UX_SSR_CONFIG_CONDITION_UNIT", IsUnique = true)]
public partial class SsrConfigConditionUnit
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه رکورد تنظيمات ثبت الکترونيک اسناد رسمي
    /// </summary>
    [Column("SSR_CONFIG_ID")]
    public Guid SsrConfigId { get; set; }

    /// <summary>
    /// کد سلسله مراتبي در ساختار درختي واحدهاي ثبتي
    /// </summary>
    [Required]
    [Column("UNIT_LEVEL_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string UnitLevelCode { get; set; }

    [ForeignKey("SsrConfigId")]
    [InverseProperty("SsrConfigConditionUnits")]
    public virtual SsrConfig SsrConfig { get; set; }
}
