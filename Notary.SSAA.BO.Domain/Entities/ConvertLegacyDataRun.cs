using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اطلاعات اجرای برنامه های كانورت اطلاعات قدیمی
/// </summary>
[Table("CONVERT_LEGACY_DATA_RUN")]
[Index("ConvertedCount", Name = "IDX_CONVERT_LEGACY_DATA_RUN###CONVERTED_COUNT")]
[Index("ConvertLegacyDataUseCaseDetailsId", Name = "IDX_CONVERT_LEGACY_DATA_RUN###CONVERT_LEGACY_DATA_USE_CASE_DETAILS_ID")]
[Index("ConvertLegacyDataUseCaseId", Name = "IDX_CONVERT_LEGACY_DATA_RUN###CONVERT_LEGACY_DATA_USE_CASE_ID")]
[Index("ErrorCount", Name = "IDX_CONVERT_LEGACY_DATA_RUN###ERROR_COUNT")]
[Index("FullTimeWorkInHolidays", Name = "IDX_CONVERT_LEGACY_DATA_RUN###FULL_TIME_WORK_IN_HOLIDAYS")]
[Index("IsFinished", Name = "IDX_CONVERT_LEGACY_DATA_RUN###IS_FINISHED")]
[Index("IsForceStoped", Name = "IDX_CONVERT_LEGACY_DATA_RUN###IS_FORCE_STOPED")]
[Index("RemainedCount", Name = "IDX_CONVERT_LEGACY_DATA_RUN###REMAINED_COUNT")]
[Index("RunEndDate", Name = "IDX_CONVERT_LEGACY_DATA_RUN###RUN_END_DATE")]
[Index("RunEndTime", Name = "IDX_CONVERT_LEGACY_DATA_RUN###RUN_END_TIME")]
[Index("RunStartDate", Name = "IDX_CONVERT_LEGACY_DATA_RUN###RUN_START_DATE")]
[Index("RunStartTime", Name = "IDX_CONVERT_LEGACY_DATA_RUN###RUN_START_TIME")]
[Index("ScheduledEndDate", Name = "IDX_CONVERT_LEGACY_DATA_RUN###SCHEDULED_END_DATE")]
[Index("ScheduledEndTime", Name = "IDX_CONVERT_LEGACY_DATA_RUN###SCHEDULED_END_TIME")]
[Index("ScheduledStartDate", Name = "IDX_CONVERT_LEGACY_DATA_RUN###SCHEDULED_START_DATE")]
[Index("ScheduledStartTime", Name = "IDX_CONVERT_LEGACY_DATA_RUN###SCHEDULED_START_TIME")]
[Index("TotalCount", Name = "IDX_CONVERT_LEGACY_DATA_RUN###TOTAL_COUNT")]
public partial class ConvertLegacyDataRun
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه موضوع اطلاعاتی برای تبدیل اطلاعات قدیمی
    /// </summary>
    [Required]
    [Column("CONVERT_LEGACY_DATA_USE_CASE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string ConvertLegacyDataUseCaseId { get; set; }

    /// <summary>
    /// شناسه جزئیات موضوع اطلاعاتی برای تبدیل اطلاعات قدیمی
    /// </summary>
    [Required]
    [Column("CONVERT_LEGACY_DATA_USE_CASE_DETAILS_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string ConvertLegacyDataUseCaseDetailsId { get; set; }

    /// <summary>
    /// تاریخ برنامه ریزی شده برای شروع كار
    /// </summary>
    [Required]
    [Column("SCHEDULED_START_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ScheduledStartDate { get; set; }

    /// <summary>
    /// تاریخ برنامه ریزی شده برای اتمام كار
    /// </summary>
    [Required]
    [Column("SCHEDULED_END_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ScheduledEndDate { get; set; }

    /// <summary>
    /// زمان برنامه ریزی شده برای شروع كار
    /// </summary>
    [Required]
    [Column("SCHEDULED_START_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ScheduledStartTime { get; set; }

    /// <summary>
    /// زمان برنامه ریزی شده برای اتمام كار
    /// </summary>
    [Required]
    [Column("SCHEDULED_END_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ScheduledEndTime { get; set; }

    /// <summary>
    /// آیا در روزهای تعطیل بصورت تمام وقت اجرا شود؟
    /// </summary>
    [Required]
    [Column("FULL_TIME_WORK_IN_HOLIDAYS")]
    [StringLength(1)]
    [Unicode(false)]
    public string FullTimeWorkInHolidays { get; set; }

    /// <summary>
    /// تعداد كل ركوردهای لازم به كانورت در این موضوع
    /// </summary>
    [Column("TOTAL_COUNT", TypeName = "NUMBER(22)")]
    public decimal? TotalCount { get; set; }

    /// <summary>
    /// تعداد ركوردهای كانورت شده
    /// </summary>
    [Column("CONVERTED_COUNT", TypeName = "NUMBER(22)")]
    public decimal? ConvertedCount { get; set; }

    /// <summary>
    /// تعداد ركوردهای باقیمانده
    /// </summary>
    [Column("REMAINED_COUNT", TypeName = "NUMBER(22)")]
    public decimal? RemainedCount { get; set; }

    /// <summary>
    /// تعداد ركوردهای دچار خطا شده
    /// </summary>
    [Column("ERROR_COUNT", TypeName = "NUMBER(22)")]
    public decimal? ErrorCount { get; set; }

    /// <summary>
    /// آیا اجرای برنامه سریعاً متوقف شود؟
    /// </summary>
    [Required]
    [Column("IS_FORCE_STOPED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsForceStoped { get; set; }

    /// <summary>
    /// شرح خطاها
    /// </summary>
    [Column("ERROR_LOG", TypeName = "CLOB")]
    public string ErrorLog { get; set; }

    /// <summary>
    /// تاریخ شروع اجرا
    /// </summary>
    [Required]
    [Column("RUN_START_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string RunStartDate { get; set; }

    /// <summary>
    /// زمان شروع اجرا
    /// </summary>
    [Required]
    [Column("RUN_START_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string RunStartTime { get; set; }

    /// <summary>
    /// تاریخ اتمام اجرا
    /// </summary>
    [Column("RUN_END_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string RunEndDate { get; set; }

    /// <summary>
    /// زمان اتمام اجرا
    /// </summary>
    [Column("RUN_END_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string RunEndTime { get; set; }

    /// <summary>
    /// آیا اجرای برنامه تمام شده است؟
    /// </summary>
    [Required]
    [Column("IS_FINISHED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFinished { get; set; }

    [InverseProperty("ConvertLegacyDataRun")]
    public virtual ICollection<ConvertLegacyDataRunDetail> ConvertLegacyDataRunDetails { get; set; } = new List<ConvertLegacyDataRunDetail>();

    [InverseProperty("ConvertLegacyDataRun")]
    public virtual ICollection<ConvertLegacyDataRunDetailsError> ConvertLegacyDataRunDetailsErrors { get; set; } = new List<ConvertLegacyDataRunDetailsError>();

    [ForeignKey("ConvertLegacyDataUseCaseId")]
    [InverseProperty("ConvertLegacyDataRuns")]
    public virtual ConvertLegacyDataUseCase ConvertLegacyDataUseCase { get; set; }

    [ForeignKey("ConvertLegacyDataUseCaseDetailsId")]
    [InverseProperty("ConvertLegacyDataRuns")]
    public virtual ConvertLegacyDataUseCaseDetail ConvertLegacyDataUseCaseDetails { get; set; }
}
