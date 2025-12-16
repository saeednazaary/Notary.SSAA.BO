using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// کاربردهاي استفاده کننده از خدمات پرداخت
/// </summary>
[Table("SSR_EPAY_USECASE")]
[Index("SsrEpaySystemId", Name = "IX_SSR_EPAY_SYS_EPAYSYSID")]
[Index("State", Name = "IX_SSR_EPAY_USECASE_STATE")]
[Index("Title", Name = "UX_SSR_EPAY_USECASE_TITLE", IsUnique = true)]
[Index("Username", Name = "UX_SSR_EPAY_USECASE_USERNAME", IsUnique = true)]
public partial class SsrEpayUsecase
{
    /// <summary>
    /// کليد اصلي
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(3)]
    public string Id { get; set; }

    /// <summary>
    /// شناسه سامانه
    /// </summary>
    [Column("SSR_EPAY_SYSTEM_ID")]
    [StringLength(2)]
    public string SsrEpaySystemId { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(100)]
    public string Title { get; set; }

    /// <summary>
    /// نام کاربري
    /// </summary>
    [Column("USERNAME")]
    [StringLength(100)]
    public string Username { get; set; }

    /// <summary>
    /// رمز عبور
    /// </summary>
    [Column("PASSWORD")]
    [StringLength(100)]
    public string Password { get; set; }

    /// <summary>
    /// ايشوور توکن
    /// </summary>
    [Column("TOKEN_ISSUER")]
    public string TokenIssuer { get; set; }

    /// <summary>
    /// سريال توکن
    /// </summary>
    [Column("TOKEN_SERIAL")]
    public string TokenSerial { get; set; }

    /// <summary>
    /// وضعيت
    /// </summary>
    [Column("STATE", TypeName = "NUMBER")]
    public decimal State { get; set; }

    [ForeignKey("SsrEpaySystemId")]
    [InverseProperty("SsrEpayUsecases")]
    public virtual SsrEpaySystem SsrEpaySystem { get; set; }

    [InverseProperty("SsrEpayUsecase")]
    public virtual ICollection<SsrEpayUsecaseCostFormula> SsrEpayUsecaseCostFormulas { get; set; } = new List<SsrEpayUsecaseCostFormula>();
}
