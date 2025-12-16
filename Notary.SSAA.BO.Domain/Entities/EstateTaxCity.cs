using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// شهر امور مالیاتی
/// </summary>
[Table("ESTATE_TAX_CITY")]
[Index("EstateTaxCountyId", Name = "IX_SSR_ETAXCITY_CUNTYID")]
public partial class EstateTaxCity
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Column("CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// ردیف شهرستان
    /// </summary>
    [Required]
    [Column("ESTATE_TAX_COUNTY_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string EstateTaxCountyId { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Column("TITLE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// شناسه در سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("EstateTaxCountyId")]
    [InverseProperty("EstateTaxCities")]
    public virtual EstateTaxCounty EstateTaxCounty { get; set; }

    [InverseProperty("EstateTaxCity")]
    public virtual ICollection<EstateTaxInquiry> EstateTaxInquiries { get; set; } = new List<EstateTaxInquiry>();
}
