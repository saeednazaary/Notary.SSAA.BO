using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// دفترخانه هاي مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي
/// </summary>
[Table("SSR_CONFIG_CONDITION_SCRPTRM")]
[Index("ScriptoriumId", Name = "IX_SSR_CONFIG_CNDTN_SCRPTM_SID")]
[Index("SsrConfigId", "ScriptoriumId", Name = "UX_SSR_CONFIG_CONDITION_SCRPTM", IsUnique = true)]
public partial class SsrConfigConditionScrptrm
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
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    [ForeignKey("SsrConfigId")]
    [InverseProperty("SsrConfigConditionScrptrms")]
    public virtual SsrConfig SsrConfig { get; set; }
}
