using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// لاگ سوابق كانورت اطلاعات قدیمی
/// </summary>
[Table("CONVERT_LEGACY_DATA_LOG")]
[Index("DataSubjectName", Name = "IDX_CONVERT_LEGACY_DATA_LOG###DATA_SUBJECT_NAME")]
[Index("EndDate", Name = "IDX_CONVERT_LEGACY_DATA_LOG###END_DATE")]
[Index("ForceStop", Name = "IDX_CONVERT_LEGACY_DATA_LOG###FORCE_STOP")]
[Index("StartDate", Name = "IDX_CONVERT_LEGACY_DATA_LOG###START_DATE")]
public partial class ConvertLegacyDataLog
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// عنوان موضوع اطلاعاتی
    /// </summary>
    [Required]
    [Column("DATA_SUBJECT_NAME")]
    [StringLength(200)]
    [Unicode(false)]
    public string DataSubjectName { get; set; }

    /// <summary>
    /// تاریخ و زمان شروع
    /// </summary>
    [Column("START_DATE", TypeName = "DATE")]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// تاریخ و زمان خاتمه
    /// </summary>
    [Column("END_DATE", TypeName = "DATE")]
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// تعداد ركورد كانورت شده
    /// </summary>
    [Column("CONVERTED_COUNT", TypeName = "NUMBER(22)")]
    public decimal? ConvertedCount { get; set; }

    /// <summary>
    /// آیا اجرای برنامه سریعاً متوقف شود؟
    /// </summary>
    [Column("FORCE_STOP", TypeName = "NUMBER(22)")]
    public decimal? ForceStop { get; set; }

    /// <summary>
    /// شرح خطاها
    /// </summary>
    [Column("ERROR_LOG")]
    [StringLength(2000)]
    [Unicode(false)]
    public string ErrorLog { get; set; }

    [InverseProperty("ConvertLegacyDataLog")]
    public virtual ICollection<ConvertLegacyDataLogDetail> ConvertLegacyDataLogDetails { get; set; } = new List<ConvertLegacyDataLogDetail>();
}
