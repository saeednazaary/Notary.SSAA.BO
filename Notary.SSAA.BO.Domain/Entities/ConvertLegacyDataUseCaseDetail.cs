using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جزئیات موضوع اطلاعاتی برای تبدیل اطلاعات قدیمی
/// </summary>
[Table("CONVERT_LEGACY_DATA_USE_CASE_DETAILS")]
[Index("ConvertLegacyDataUseCaseId", Name = "IDX_CONVERT_LEGACY_DATA_USE_CASE_DETAILS###CONVERT_LEGACY_DATA_USE_CASE_ID")]
[Index("FromDate", Name = "IDX_CONVERT_LEGACY_DATA_USE_CASE_DETAILS###FROM_DATE")]
[Index("State", Name = "IDX_CONVERT_LEGACY_DATA_USE_CASE_DETAILS###STATE")]
[Index("ToDate", Name = "IDX_CONVERT_LEGACY_DATA_USE_CASE_DETAILS###TO_DATE")]
[Index("Code", Name = "UDX_CONVERT_LEGACY_DATA_USE_CASE_DETAILS###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_CONVERT_LEGACY_DATA_USE_CASE_DETAILS###TITLE", IsUnique = true)]
public partial class ConvertLegacyDataUseCaseDetail
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// شناسه موضوع اطلاعاتی برای تبدیل اطلاعات قدیمی
    /// </summary>
    [Required]
    [Column("CONVERT_LEGACY_DATA_USE_CASE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string ConvertLegacyDataUseCaseId { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(4)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(100)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// از تاریخ
    /// </summary>
    [Column("FROM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FromDate { get; set; }

    /// <summary>
    /// تا تاریخ
    /// </summary>
    [Column("TO_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ToDate { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    [InverseProperty("ConvertLegacyDataUseCaseDetails")]
    public virtual ICollection<ConvertLegacyDataRun> ConvertLegacyDataRuns { get; set; } = new List<ConvertLegacyDataRun>();

    [ForeignKey("ConvertLegacyDataUseCaseId")]
    [InverseProperty("ConvertLegacyDataUseCaseDetails")]
    public virtual ConvertLegacyDataUseCase ConvertLegacyDataUseCase { get; set; }
}
