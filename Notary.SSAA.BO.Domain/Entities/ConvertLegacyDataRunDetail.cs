using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جزئیات اطلاعات اجرای برنامه های كانورت اطلاعات قدیمی
/// </summary>
[Table("CONVERT_LEGACY_DATA_RUN_DETAILS")]
[Index("ConvertedCount", Name = "IDX_CONVERT_LEGACY_DATA_RUN_DETAILS###CONVERTED_COUNT")]
[Index("DataCategory", Name = "IDX_CONVERT_LEGACY_DATA_RUN_DETAILS###DATA_CATEGORY")]
[Index("ErrorCount", Name = "IDX_CONVERT_LEGACY_DATA_RUN_DETAILS###ERROR_COUNT")]
[Index("FinishDate", Name = "IDX_CONVERT_LEGACY_DATA_RUN_DETAILS###FINISH_DATE")]
[Index("FinishTime", Name = "IDX_CONVERT_LEGACY_DATA_RUN_DETAILS###FINISH_TIME")]
[Index("IsFinished", Name = "IDX_CONVERT_LEGACY_DATA_RUN_DETAILS###IS_FINISHED")]
[Index("StartDate", Name = "IDX_CONVERT_LEGACY_DATA_RUN_DETAILS###START_DATE")]
[Index("StartTime", Name = "IDX_CONVERT_LEGACY_DATA_RUN_DETAILS###START_TIME")]
[Index("TotalCount", Name = "IDX_CONVERT_LEGACY_DATA_RUN_DETAILS###TOTAL_COUNT")]
[Index("ConvertLegacyDataRunId", Name = "IX_SSR_CNVRTLGCYDTRUNDTLS_RID")]
public partial class ConvertLegacyDataRunDetail
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه ركورد اطلاعات اجرای برنامه های كانورت اطلاعات قدیمی
    /// </summary>
    [Column("CONVERT_LEGACY_DATA_RUN_ID")]
    public Guid ConvertLegacyDataRunId { get; set; }

    /// <summary>
    /// شاخصه دسته بندی اطلاعات
    /// </summary>
    [Required]
    [Column("DATA_CATEGORY")]
    [StringLength(1000)]
    [Unicode(false)]
    public string DataCategory { get; set; }

    /// <summary>
    /// تعداد كل ركوردهای لازم به كانورت در این شاخصه اطلاعات
    /// </summary>
    [Column("TOTAL_COUNT", TypeName = "NUMBER(22)")]
    public decimal TotalCount { get; set; }

    /// <summary>
    /// تعداد ركوردهای كانورت شده
    /// </summary>
    [Column("CONVERTED_COUNT", TypeName = "NUMBER(22)")]
    public decimal ConvertedCount { get; set; }

    /// <summary>
    /// تعداد ركوردهای دچار خطا شده
    /// </summary>
    [Column("ERROR_COUNT", TypeName = "NUMBER(22)")]
    public decimal ErrorCount { get; set; }

    /// <summary>
    /// تاریخ شروع این مرحله
    /// </summary>
    [Required]
    [Column("START_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string StartDate { get; set; }

    /// <summary>
    /// زمان شروع این مرحله
    /// </summary>
    [Required]
    [Column("START_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string StartTime { get; set; }

    /// <summary>
    /// تاریخ اتمام این مرحله
    /// </summary>
    [Column("FINISH_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FinishDate { get; set; }

    /// <summary>
    /// زمان اتمام این مرحله
    /// </summary>
    [Column("FINISH_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string FinishTime { get; set; }

    /// <summary>
    /// شرح خطاها
    /// </summary>
    [Column("ERROR_LOG", TypeName = "CLOB")]
    public string ErrorLog { get; set; }

    /// <summary>
    /// آیا خاتمه یافته است؟
    /// </summary>
    [Required]
    [Column("IS_FINISHED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFinished { get; set; }

    [ForeignKey("ConvertLegacyDataRunId")]
    [InverseProperty("ConvertLegacyDataRunDetails")]
    public virtual ConvertLegacyDataRun ConvertLegacyDataRun { get; set; }
}
