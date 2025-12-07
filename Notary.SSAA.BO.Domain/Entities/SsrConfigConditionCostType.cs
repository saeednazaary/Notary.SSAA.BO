using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع هزينه هاي مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي
/// </summary>
[Table("SSR_CONFIG_CONDITION_COST_TYPE")]
[Index("CostTypeId", Name = "IX_SSR_CONFIG_CONDTN_CSTP_CTD")]
[Index("SsrConfigId", "CostTypeId", Name = "UX_SSR_CONFIG_CONDITION_CSTYP", IsUnique = true)]
public partial class SsrConfigConditionCostType
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
    /// شناسه نوع هزينه
    /// </summary>
    [Required]
    [Column("COST_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string CostTypeId { get; set; }

    [ForeignKey("CostTypeId")]
    [InverseProperty("SsrConfigConditionCostTypes")]
    public virtual CostType CostType { get; set; }

    [ForeignKey("SsrConfigId")]
    [InverseProperty("SsrConfigConditionCostTypes")]
    public virtual SsrConfig SsrConfig { get; set; }
}
