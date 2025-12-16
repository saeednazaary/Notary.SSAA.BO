using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سامانه هاي استفاده کننده از خدمات پرداخت
/// </summary>
[Table("SSR_EPAY_SYSTEM")]
[Index("State", Name = "IX_SSR_EPAY_SYS_STATE")]
[Index("EnglishTitle", Name = "UX_SSR_EPAYSYS_ENGLISH_TITLE", IsUnique = true)]
[Index("Title", Name = "UX_SSR_EPAYSYS_TITLE", IsUnique = true)]
public partial class SsrEpaySystem
{
    /// <summary>
    /// کليد اصلي
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(2)]
    public string Id { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(100)]
    public string Title { get; set; }

    /// <summary>
    /// عنوان انگليسي
    /// </summary>
    [Required]
    [Column("ENGLISH_TITLE")]
    [StringLength(100)]
    public string EnglishTitle { get; set; }

    /// <summary>
    /// وضعيت
    /// </summary>
    [Column("STATE", TypeName = "NUMBER")]
    public decimal State { get; set; }

    [InverseProperty("SsrEpaySystem")]
    public virtual ICollection<SsrEpayUsecase> SsrEpayUsecases { get; set; } = new List<SsrEpayUsecase>();
}
