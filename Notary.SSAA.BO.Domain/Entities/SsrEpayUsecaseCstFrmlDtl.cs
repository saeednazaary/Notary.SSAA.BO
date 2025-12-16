using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// ريز محاسبات فرمول محاسبه انواع هزينه در انواع مختلف خدمات ثبتي در دوره هاي تاريخي مختلف
/// </summary>
[Table("SSR_EPAY_USECASE_CST_FRML_DTLS")]
[Index("BusinessCodes", Name = "IX_SSR_USCSCSTFRMLDTLS_BCODE")]
[Index("CalculateType", Name = "IX_SSR_USCSCSTFRML_DTLS_CTYPE")]
[Index("UsecaseCostFormulaId", Name = "IX_SSR_USCSCSTFRML_DTLS_FID")]
public partial class SsrEpayUsecaseCstFrmlDtl
{
    /// <summary>
    /// کليد اصلي
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// کليد اصلي فرمول محاسبه انواع هزينه در انواع مختلف خدمات ثبتي در دوره هاي تاريخي مختلف
    /// </summary>
    [Column("USECASE_COST_FORMULA_ID")]
    public Guid UsecaseCostFormulaId { get; set; }

    /// <summary>
    /// کدهاي موارد مشمول، مثلاً: 111,112,113 يا % براي همه موارد
    /// </summary>
    [Column("BUSINESS_CODES")]
    [StringLength(2000)]
    public string BusinessCodes { get; set; }

    /// <summary>
    /// شيوه محاسبه
    /// </summary>
    [Required]
    [Column("CALCULATE_TYPE")]
    [StringLength(1)]
    public string CalculateType { get; set; }

    /// <summary>
    /// ضريب ثابت، مثلاً 0.005
    /// </summary>
    [Column("FIXED_FACTOR", TypeName = "NUMBER(10,5)")]
    public decimal? FixedFactor { get; set; }

    /// <summary>
    /// مقدار ثابت، مثلاً  4000000
    /// </summary>
    [Column("FIXED_VALUE")]
    [Precision(15)]
    public long? FixedValue { get; set; }

    /// <summary>
    /// توضيحات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(1000)]
    public string Description { get; set; }

    [Column("OLD_ID")]
    public Guid? OldId { get; set; }

    [InverseProperty("UsecaseCstFrmlDtls")]
    public virtual ICollection<SsrEpayUscsCstFrmlDtlStp> SsrEpayUscsCstFrmlDtlStps { get; set; } = new List<SsrEpayUscsCstFrmlDtlStp>();

    [ForeignKey("UsecaseCostFormulaId")]
    [InverseProperty("SsrEpayUsecaseCstFrmlDtls")]
    public virtual SsrEpayUsecaseCostFormula UsecaseCostFormula { get; set; }
}
