using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// زمان هایی كه انجام انواع فعالیت های حساس و مهم در روند ثبت الكترونیك اسناد غیرمجاز است
/// </summary>
[Table("IMPORTANT_ACTION_TYPE_ILLEGAL_TIMES")]
[Index("DayOfWeek", Name = "IDX_IMPORTANT_ACTION_TYPE_ILLEGAL_TIMES###DAY_OF_WEEK")]
[Index("FromDate", Name = "IDX_IMPORTANT_ACTION_TYPE_ILLEGAL_TIMES###FROM_DATE")]
[Index("FromTime", Name = "IDX_IMPORTANT_ACTION_TYPE_ILLEGAL_TIMES###FROM_TIME")]
[Index("ImportantActionTypeId", Name = "IDX_IMPORTANT_ACTION_TYPE_ILLEGAL_TIMES###IMPORTANT_ACTION_TYPE_ID")]
[Index("OrganizationId", Name = "IDX_IMPORTANT_ACTION_TYPE_ILLEGAL_TIMES###ORGANIZATION_ID")]
[Index("ToDate", Name = "IDX_IMPORTANT_ACTION_TYPE_ILLEGAL_TIMES###TO_DATE")]
[Index("ToTime", Name = "IDX_IMPORTANT_ACTION_TYPE_ILLEGAL_TIMES###TO_TIME")]
[Index("LegacyId", Name = "UDX_IMPORTANT_ACTION_TYPE_ILLEGAL_TIMES###LEGACY_ID", IsUnique = true)]
public partial class ImportantActionTypeIllegalTime
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
    /// از تاریخ
    /// </summary>
    [Required]
    [Column("FROM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FromDate { get; set; }

    /// <summary>
    /// تا تاریخ
    /// </summary>
    [Required]
    [Column("TO_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ToDate { get; set; }

    /// <summary>
    /// شناسه نوع فعالیت حساس و مهم در روند ثبت الكترونیك اسناد
    /// </summary>
    [Required]
    [Column("IMPORTANT_ACTION_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string ImportantActionTypeId { get; set; }

    /// <summary>
    /// شناسه واحدثبتی یا دفترخانه
    /// </summary>
    [Column("ORGANIZATION_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string OrganizationId { get; set; }

    /// <summary>
    /// روز هفته
    /// </summary>
    [Required]
    [Column("DAY_OF_WEEK")]
    [StringLength(1)]
    [Unicode(false)]
    public string DayOfWeek { get; set; }

    /// <summary>
    /// زمان شروع بازه غیرمجاز
    /// </summary>
    [Required]
    [Column("FROM_TIME")]
    [StringLength(5)]
    [Unicode(false)]
    public string FromTime { get; set; }

    /// <summary>
    /// زمان پایان بازه غیرمجاز
    /// </summary>
    [Required]
    [Column("TO_TIME")]
    [StringLength(5)]
    [Unicode(false)]
    public string ToTime { get; set; }

    /// <summary>
    /// شناسه ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("ImportantActionTypeId")]
    [InverseProperty("ImportantActionTypeIllegalTimes")]
    public virtual ImportantActionType ImportantActionType { get; set; }
}
