using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// موضوع اطلاعاتی برای تبدیل اطلاعات قدیمی
/// </summary>
[Table("CONVERT_LEGACY_DATA_USE_CASE")]
[Index("SmsReceiversMobileNo", Name = "IDX_CONVERT_LEGACY_DATA_USE_CASE###SMS_RECEIVERS_MOBILE_NO")]
[Index("State", Name = "IDX_CONVERT_LEGACY_DATA_USE_CASE###STATE")]
[Index("Code", Name = "UDX_CONVERT_LEGACY_DATA_USE_CASE###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_CONVERT_LEGACY_DATA_USE_CASE###TITLE", IsUnique = true)]
public partial class ConvertLegacyDataUseCase
{
    /// <summary>
    /// شناسه
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
    [StringLength(100)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// بازه زمانی تازه سازی آمارهای پیشرفت
    /// </summary>
    [Column("REFRESH_INTERVAL", TypeName = "NUMBER(38)")]
    public decimal? RefreshInterval { get; set; }

    /// <summary>
    /// بازه زمانی ارسال پیامك های آمارهای پیشرفت
    /// </summary>
    [Column("SMS_INTERVAL", TypeName = "NUMBER(38)")]
    public decimal? SmsInterval { get; set; }

    /// <summary>
    /// شماره موبایل های دریافت كننده پیامك های آمارهای پیشرفت
    /// </summary>
    [Column("SMS_RECEIVERS_MOBILE_NO")]
    [StringLength(1000)]
    [Unicode(false)]
    public string SmsReceiversMobileNo { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    [InverseProperty("ConvertLegacyDataUseCase")]
    public virtual ICollection<ConvertLegacyDataRun> ConvertLegacyDataRuns { get; set; } = new List<ConvertLegacyDataRun>();

    [InverseProperty("ConvertLegacyDataUseCase")]
    public virtual ICollection<ConvertLegacyDataUseCaseDetail> ConvertLegacyDataUseCaseDetails { get; set; } = new List<ConvertLegacyDataUseCaseDetail>();
}
