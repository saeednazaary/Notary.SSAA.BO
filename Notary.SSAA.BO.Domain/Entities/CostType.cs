using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع هزینه های خدمات ثبتی
/// </summary>
[Table("COST_TYPE")]
[Index("State", Name = "IDX_COST_TYPE###STATE")]
[Index("Code", Name = "UDX_COST_TYPE###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_COST_TYPE###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_COST_TYPE###TITLE", IsUnique = true)]
public partial class CostType
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
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("CostType")]
    public virtual ICollection<DocumentCostUnchanged> DocumentCostUnchangeds { get; set; } = new List<DocumentCostUnchanged>();

    [InverseProperty("CostType")]
    public virtual ICollection<DocumentCost> DocumentCosts { get; set; } = new List<DocumentCost>();

    [InverseProperty("CostType")]
    public virtual ICollection<DocumentPayment> DocumentPayments { get; set; } = new List<DocumentPayment>();

    [InverseProperty("CostType")]
    public virtual ICollection<SsrConfigConditionCostType> SsrConfigConditionCostTypes { get; set; } = new List<SsrConfigConditionCostType>();

    [InverseProperty("CostType")]
    public virtual ICollection<ValueAddedTax> ValueAddedTaxes { get; set; } = new List<ValueAddedTax>();
}
