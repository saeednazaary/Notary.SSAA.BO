using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// محدوده هاي زماني مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي
/// </summary>
[Table("SSR_CONFIG_CONDITION_TIME")]
[Index("SsrConfigId", Name = "IX_SSR_CONFIG_CNDITN_TIME_CID")]
[Index("DayOfWeek", Name = "IX_SSR_CONFIG_CNDITN_TIME_DOW")]
[Index("FromTime", Name = "IX_SSR_CONFIG_CNDITN_TIME_FTM")]
[Index("ToTime", Name = "IX_SSR_CONFIG_CNDITN_TIME_TTM")]
public partial class SsrConfigConditionTime
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
    /// روز هفته يا تعطيل:     0-تعطيل رسمي     1-شنبه     2-يکشنبه     3-دوشنبه     4-سه شنبه     5-چهارشنبه     6-پنجشنبه     7-جمعه
    /// </summary>
    [Required]
    [Column("DAY_OF_WEEK")]
    [StringLength(1)]
    [Unicode(false)]
    public string DayOfWeek { get; set; }

    /// <summary>
    /// از ساعت - مثال 11:00
    /// </summary>
    [Required]
    [Column("FROM_TIME")]
    [StringLength(5)]
    [Unicode(false)]
    public string FromTime { get; set; }

    /// <summary>
    /// تا ساعت - مثال 16:30
    /// </summary>
    [Required]
    [Column("TO_TIME")]
    [StringLength(5)]
    [Unicode(false)]
    public string ToTime { get; set; }

    [ForeignKey("SsrConfigId")]
    [InverseProperty("SsrConfigConditionTimes")]
    public virtual SsrConfig SsrConfig { get; set; }
}
