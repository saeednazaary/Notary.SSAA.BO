using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع فعالیت های حساس و مهم در روند ثبت الكترونیك اسناد
/// </summary>
[Table("IMPORTANT_ACTION_TYPE")]
[Index("LegacyId", Name = "IDX_IMPORTANT_ACTION_TYPE###LEGACY_ID")]
[Index("State", Name = "IDX_IMPORTANT_ACTION_TYPE###STATE")]
[Index("Code", Name = "UDX_IMPORTANT_ACTION_TYPE###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_IMPORTANT_ACTION_TYPE###TITLE", IsUnique = true)]
public partial class ImportantActionType
{
    /// <summary>
    ///  ردیف
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(2)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(200)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// آیا انجام این فعالیت در روزهای تعطیل غیرمجاز است؟
    /// </summary>
    [Required]
    [Column("IS_ILLEGAL_IN_HOLIDAYS")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsIllegalInHolidays { get; set; }

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

    [InverseProperty("ImportantActionType")]
    public virtual ICollection<ImportantActionTypeIllegalTime> ImportantActionTypeIllegalTimes { get; set; } = new List<ImportantActionTypeIllegalTime>();
}
