using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// درصد مالیات بر ارزش افزوده
/// </summary>
[Table("VALUE_ADDED_TAX")]
[Index("CostTypeId", Name = "IDX_VALUE_ADDED_TAX###COST_TYPE_ID")]
[Index("EndDate", Name = "IDX_VALUE_ADDED_TAX###END_DATE")]
[Index("StartDate", Name = "IDX_VALUE_ADDED_TAX###START_DATE")]
public partial class ValueAddedTax
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// از تاریخ
    /// </summary>
    [Required]
    [Column("START_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string StartDate { get; set; }

    /// <summary>
    /// تا تاریخ
    /// </summary>
    [Required]
    [Column("END_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string EndDate { get; set; }

    /// <summary>
    /// شناسه نوع هزینه خدمات ثبتی
    /// </summary>
    [Required]
    [Column("COST_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string CostTypeId { get; set; }

    /// <summary>
    /// درصد عوارض
    /// </summary>
    [Column("CHARGE_PERCENT", TypeName = "NUMBER(5,2)")]
    public decimal ChargePercent { get; set; }

    /// <summary>
    /// درصد مالیات بر ارزش افزوده
    /// </summary>
    [Column("TAX_PERCENT", TypeName = "NUMBER(5,2)")]
    public decimal TaxPercent { get; set; }

    /// <summary>
    /// كل درصد مالیات بر ارزش افزوده
    /// </summary>
    [Column("TOTAL_TAX_PERCENT", TypeName = "NUMBER(5,2)")]
    public decimal TotalTaxPercent { get; set; }

    [ForeignKey("CostTypeId")]
    [InverseProperty("ValueAddedTaxes")]
    public virtual CostType CostType { get; set; }
}
