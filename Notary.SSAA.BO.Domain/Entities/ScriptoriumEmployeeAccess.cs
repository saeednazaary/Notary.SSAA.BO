using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سوابق دسترسی های كاركنان دفترخانه ها
/// </summary>
[Table("SCRIPTORIUM_EMPLOYEE_ACCESS")]
[Index("FromDate", Name = "IDX_SCRIPTORIUM_EMPLOYEE_ACCESS###FROM_DATE")]
[Index("FromTime", Name = "IDX_SCRIPTORIUM_EMPLOYEE_ACCESS###FROM_TIME")]
[Index("ScriptoriumEmployeeId", Name = "IDX_SCRIPTORIUM_EMPLOYEE_ACCESS###SCRIPTORIUM_EMPLOYEE_ID")]
[Index("ScriptoriumId", Name = "IDX_SCRIPTORIUM_EMPLOYEE_ACCESS###SCRIPTORIUM_ID")]
[Index("ToDate", Name = "IDX_SCRIPTORIUM_EMPLOYEE_ACCESS###TO_DATE")]
[Index("ToTime", Name = "IDX_SCRIPTORIUM_EMPLOYEE_ACCESS###TO_TIME")]
public partial class ScriptoriumEmployeeAccess
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه كاركنان دفترخانه ها
    /// </summary>
    [Column("SCRIPTORIUM_EMPLOYEE_ID")]
    public Guid ScriptoriumEmployeeId { get; set; }

    /// <summary>
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// تاریخ شروع دسترسی
    /// </summary>
    [Required]
    [Column("FROM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FromDate { get; set; }

    /// <summary>
    /// زمان شروع دسترسی
    /// </summary>
    [Column("FROM_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string FromTime { get; set; }

    /// <summary>
    /// تاریخ خاتمه دسترسی
    /// </summary>
    [Column("TO_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ToDate { get; set; }

    /// <summary>
    /// زمان خاتمه دسترسی
    /// </summary>
    [Column("TO_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ToTime { get; set; }

    [ForeignKey("ScriptoriumEmployeeId")]
    [InverseProperty("ScriptoriumEmployeeAccesses")]
    public virtual ScriptoriumEmployee ScriptoriumEmployee { get; set; }
}
