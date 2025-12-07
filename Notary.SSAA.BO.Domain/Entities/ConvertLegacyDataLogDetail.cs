using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جزئیات لاگ سوابق كانورت اطلاعات قدیمی
/// </summary>
[Table("CONVERT_LEGACY_DATA_LOG_DETAILS")]
[Index("ConvertLegacyDataLogId", Name = "IDX_CONVERT_LEGACY_DATA_LOG_DETAILS###CONVERT_LEGACY_DATA_LOG_ID")]
[Index("DataCategory", Name = "IDX_CONVERT_LEGACY_DATA_LOG_DETAILS###DATA_CATEGORY")]
[Index("DoDate", Name = "IDX_CONVERT_LEGACY_DATA_LOG_DETAILS###DO_DATE")]
public partial class ConvertLegacyDataLogDetail
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه لاگ سوابق كانورت اطلاعات قدیمی
    /// </summary>
    [Column("CONVERT_LEGACY_DATA_LOG_ID")]
    public Guid ConvertLegacyDataLogId { get; set; }

    /// <summary>
    /// عنوان موضوع اطلاعاتی
    /// </summary>
    [Required]
    [Column("DATA_CATEGORY")]
    [StringLength(500)]
    [Unicode(false)]
    public string DataCategory { get; set; }

    /// <summary>
    /// تعداد ركورد كانورت شده
    /// </summary>
    [Column("RECORD_COUNT", TypeName = "NUMBER(22)")]
    public decimal? RecordCount { get; set; }

    /// <summary>
    /// تاریخ و زمان انجام
    /// </summary>
    [Column("DO_DATE", TypeName = "DATE")]
    public DateTime? DoDate { get; set; }

    /// <summary>
    /// شرح خطاها
    /// </summary>
    [Column("ERROR_LOG")]
    [StringLength(2000)]
    [Unicode(false)]
    public string ErrorLog { get; set; }

    [ForeignKey("ConvertLegacyDataLogId")]
    [InverseProperty("ConvertLegacyDataLogDetails")]
    public virtual ConvertLegacyDataLog ConvertLegacyDataLog { get; set; }
}
