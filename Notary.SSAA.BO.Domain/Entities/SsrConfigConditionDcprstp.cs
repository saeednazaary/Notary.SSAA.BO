using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع سِمت هاي اشخاص در اسناد مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي
/// </summary>
[Table("SSR_CONFIG_CONDITION_DCPRSTP")]
[Index("DocumentPersonTypeId", Name = "IX_SSR_CONFIG_CONDTN_DCPT_DPID")]
[Index("SsrConfigId", "DocumentPersonTypeId", Name = "UX_SSR_CONFIG_CONDITION_DCPRSTP", IsUnique = true)]
public partial class SsrConfigConditionDcprstp
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
    /// شناسه نوع سِمت در اسناد
    /// </summary>
    [Required]
    [Column("DOCUMENT_PERSON_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string DocumentPersonTypeId { get; set; }

    [ForeignKey("DocumentPersonTypeId")]
    [InverseProperty("SsrConfigConditionDcprstps")]
    public virtual DocumentPersonType DocumentPersonType { get; set; }

    [ForeignKey("SsrConfigId")]
    [InverseProperty("SsrConfigConditionDcprstps")]
    public virtual SsrConfig SsrConfig { get; set; }
}
