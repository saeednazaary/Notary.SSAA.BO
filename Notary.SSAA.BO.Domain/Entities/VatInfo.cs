using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اطلاعات پرونده مالياتي سردفتران و دفترياران
/// </summary>
[Table("VAT_INFO")]
[Index("LegacyId", Name = "IDX_VAT_INFO###LEGACY_ID", IsUnique = true)]
[Index("NationalNo", Name = "IDX_VAT_INFO###NATIONAL_NO")]
[Index("TaxCode", Name = "IX_SSR_VAT_INFO", IsUnique = true)]
public partial class VatInfo
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شماره ملي
    /// </summary>
    [Required]
    [Column("NATIONAL_NO")]
    [StringLength(10)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    [Required]
    [Column("NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; }

    /// <summary>
    /// نام خانوادگي
    /// </summary>
    [Required]
    [Column("FAMILY")]
    [StringLength(50)]
    [Unicode(false)]
    public string Family { get; set; }

    /// <summary>
    /// استان ماليات
    /// </summary>
    [Required]
    [Column("TAX_OSTAN")]
    [StringLength(100)]
    [Unicode(false)]
    public string TaxOstan { get; set; }

    /// <summary>
    /// کد اداره مالياتي
    /// </summary>
    [Required]
    [Column("TAX_UNIT_CODE")]
    [StringLength(100)]
    [Unicode(false)]
    public string TaxUnitCode { get; set; }

    /// <summary>
    /// نام اداره مالياتي
    /// </summary>
    [Required]
    [Column("TAX_UNIT_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string TaxUnitName { get; set; }

    /// <summary>
    /// شبا واريز ماليات
    /// </summary>
    [Required]
    [Column("TAX_SHEBA_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string TaxShebaNo { get; set; }

    /// <summary>
    /// شماره حساب واريز ماليات
    /// </summary>
    [Required]
    [Column("TAX_ACCOUNT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string TaxAccountNo { get; set; }

    /// <summary>
    /// درصد ماليات بر ارزش افزوده
    /// </summary>
    [Column("TAX_PERCENT", TypeName = "NUMBER(5,2)")]
    public decimal TaxPercent { get; set; }

    /// <summary>
    /// شبا واريز عوارض
    /// </summary>
    [Column("TOLL_SHEBA_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string TollShebaNo { get; set; }

    /// <summary>
    /// شماره حساب واريز عوارض
    /// </summary>
    [Column("TOLL_ACCOUNT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string TollAccountNo { get; set; }

    /// <summary>
    /// استان عوارض
    /// </summary>
    [Column("TOLL_OSTAN")]
    [StringLength(100)]
    [Unicode(false)]
    public string TollOstan { get; set; }

    /// <summary>
    /// درصد عوارض
    /// </summary>
    [Column("TOLL_PERCENT", TypeName = "NUMBER(5,2)")]
    public decimal? TollPercent { get; set; }

    /// <summary>
    /// کليد اصلي رکورد در سامانه قبلي
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// كد مالیاتی
    /// </summary>
    [Column("TAX_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string TaxCode { get; set; }
}
