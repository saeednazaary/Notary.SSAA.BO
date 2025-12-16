using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نرخ ماليات بر ارزش افزوده در دوره هاي تاريخي مختلف
/// </summary>
[Table("SSR_EPAY_VAT_RATE")]
[Index("FromDate", Name = "IX_SSR_EPAY_VAT_RATE_FROM_DATE")]
[Index("ToDate", Name = "IX_SSR_EPAY_VAT_RATE_TO_DATE")]
public partial class SsrEpayVatRate
{
    /// <summary>
    /// شناسه يکتا
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// از تاريخ
    /// </summary>
    [Required]
    [Column("FROM_DATE")]
    [StringLength(10)]
    public string FromDate { get; set; }

    /// <summary>
    /// تا تاريخ
    /// </summary>
    [Required]
    [Column("TO_DATE")]
    [StringLength(10)]
    public string ToDate { get; set; }

    /// <summary>
    /// درصد ارزش افزوده
    /// </summary>
    [Column("SSR_EPAY_VAT_RATE", TypeName = "NUMBER(5,2)")]
    public decimal SsrEpayVatRate1 { get; set; }
}
