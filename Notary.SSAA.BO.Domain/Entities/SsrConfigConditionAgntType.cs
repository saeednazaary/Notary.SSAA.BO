using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع وابستگي اشخاص مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي
/// </summary>
[Table("SSR_CONFIG_CONDITION_AGNT_TYPE")]
[Index("AgentTypeId", Name = "IX_SSR_CONFIG_CNDTN_AGNTYP_AID")]
[Index("SsrConfigId", "AgentTypeId", Name = "UX_SSR_CONFIG_CONDITION_AGNTYP", IsUnique = true)]
public partial class SsrConfigConditionAgntType
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
    /// شناسه نوع وابستگي اشخاص
    /// </summary>
    [Required]
    [Column("AGENT_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string AgentTypeId { get; set; }

    [ForeignKey("AgentTypeId")]
    [InverseProperty("SsrConfigConditionAgntTypes")]
    public virtual AgentType AgentType { get; set; }

    [ForeignKey("SsrConfigId")]
    [InverseProperty("SsrConfigConditionAgntTypes")]
    public virtual SsrConfig SsrConfig { get; set; }
}
