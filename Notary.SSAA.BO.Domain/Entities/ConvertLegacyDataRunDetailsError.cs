using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جزئیات ركوردهای خطادار در اجرای برنامه های كانورت اطلاعات قدیمی
/// </summary>
[Table("CONVERT_LEGACY_DATA_RUN_DETAILS_ERRORS")]
[Index("ConvertLegacyDataRunDetailsId", Name = "IDX_CONVERT_LEGACY_DATA_RUN_DETAILS_ERRORS###CONVERT_LEGACY_DATA_RUN_DETAILS_ID")]
[Index("ConvertLegacyDataRunId", Name = "IDX_CONVERT_LEGACY_DATA_RUN_DETAILS_ERRORS###CONVERT_LEGACY_DATA_RUN_ID")]
[Index("TroubledRecordId", Name = "IDX_CONVERT_LEGACY_DATA_RUN_DETAILS_ERRORS###TROUBLED_RECORD_ID")]
public partial class ConvertLegacyDataRunDetailsError
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
    /// شناسه ركورد جزئیات اطلاعات اجرای برنامه های كانورت اطلاعات قدیمی
    /// </summary>
    [Column("CONVERT_LEGACY_DATA_RUN_DETAILS_ID")]
    public Guid ConvertLegacyDataRunDetailsId { get; set; }

    /// <summary>
    /// شناسه ركوردی كه تبدیل اطلاعات آن به مشكل برخورده است
    /// </summary>
    [Required]
    [Column("TROUBLED_RECORD_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string TroubledRecordId { get; set; }

    /// <summary>
    /// شرح خطاها
    /// </summary>
    [Required]
    [Column("ERROR_LOG", TypeName = "CLOB")]
    public string ErrorLog { get; set; }

    /// <summary>
    /// محتوای آبجكت
    /// </summary>
    [Column("OBJECT_CONTENT", TypeName = "CLOB")]
    public string ObjectContent { get; set; }

    [ForeignKey("ConvertLegacyDataRunId")]
    [InverseProperty("ConvertLegacyDataRunDetailsErrors")]
    public virtual ConvertLegacyDataRun ConvertLegacyDataRun { get; set; }
}
