using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// پلکان ريز محاسبات فرمول محاسبه انواع هزينه در انواع مختلف خدمات ثبتي در دوره هاي تاريخي مختلف
/// </summary>
[Table("SSR_EPAY_USCS_CST_FRML_DTL_STP")]
[Index("FromStep", Name = "IX_SSR_USCSCSTFRMLDTLSTP_FSTP")]
[Index("ToStep", Name = "IX_SSR_USCSCSTFRMLDTLSTP_TSTP")]
[Index("UsecaseCstFrmlDtlsId", Name = "IX_SSR_USCSCSTFRMLDTLS_DTLSID")]
public partial class SsrEpayUscsCstFrmlDtlStp
{
    /// <summary>
    /// کليد اصلي
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// کليد اصلي ريز محاسبات فرمول محاسبه انواع هزينه در انواع مختلف خدمات ثبتي در دوره هاي تاريخي مختلف
    /// </summary>
    [Column("USECASE_CST_FRML_DTLS_ID")]
    public Guid UsecaseCstFrmlDtlsId { get; set; }

    /// <summary>
    /// مقدار شروع پله
    /// </summary>
    [Column("FROM_STEP")]
    [Precision(15)]
    public long FromStep { get; set; }

    /// <summary>
    /// مقدار پايان پله
    /// </summary>
    [Column("TO_STEP")]
    [Precision(15)]
    public long ToStep { get; set; }

    /// <summary>
    /// ضريب
    /// </summary>
    [Column("FACTOR", TypeName = "NUMBER(15,5)")]
    public decimal? Factor { get; set; }

    /// <summary>
    /// مقدار
    /// </summary>
    [Column("VALUE")]
    [Precision(15)]
    public long? Value { get; set; }

    [ForeignKey("UsecaseCstFrmlDtlsId")]
    [InverseProperty("SsrEpayUscsCstFrmlDtlStps")]
    public virtual SsrEpayUsecaseCstFrmlDtl UsecaseCstFrmlDtls { get; set; }
}
