using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// فرمول محاسبه انواع هزينه در انواع مختلف خدمات ثبتي در دوره هاي تاريخي مختلف
/// </summary>
[Table("SSR_EPAY_USECASE_COST_FORMULA")]
[Index("FromDate", Name = "IX_SSR_USCSCSTFRML_FDATE")]
[Index("ToDate", Name = "IX_SSR_USCSCSTFRML_TDATE")]
[Index("CostTypeId", Name = "IX_SSR_USCSCST_FRML_CTYPEID")]
[Index("SsrEpayUsecaseId", Name = "IX_SSR_USCSCST_FRML_USCSID")]
public partial class SsrEpayUsecaseCostFormula
{
    /// <summary>
    /// کليد اصلي
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// کليد اصلي نوع خدمت
    /// </summary>
    [Required]
    [Column("SSR_EPAY_USECASE_ID")]
    [StringLength(3)]
    public string SsrEpayUsecaseId { get; set; }

    /// <summary>
    /// کليد اصلي نوع هزينه
    /// </summary>
    [Required]
    [Column("COST_TYPE_ID")]
    [StringLength(2)]
    public string CostTypeId { get; set; }

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

    [ForeignKey("CostTypeId")]
    [InverseProperty("SsrEpayUsecaseCostFormulas")]
    public virtual CostType CostType { get; set; }

    [ForeignKey("SsrEpayUsecaseId")]
    [InverseProperty("SsrEpayUsecaseCostFormulas")]
    public virtual SsrEpayUsecase SsrEpayUsecase { get; set; }

    [InverseProperty("UsecaseCostFormula")]
    public virtual ICollection<SsrEpayUsecaseCstFrmlDtl> SsrEpayUsecaseCstFrmlDtls { get; set; } = new List<SsrEpayUsecaseCstFrmlDtl>();
}
