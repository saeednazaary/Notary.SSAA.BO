using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// شهرستان امور مالیاتی
/// </summary>
[Table("ESTATE_TAX_COUNTY")]
[Index("EstateTaxProvinceId", Name = "IX_SSR_ETAXCNTY_PROVID")]
public partial class EstateTaxCounty
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(4)]
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
    /// ردیف استان
    /// </summary>
    [Required]
    [Column("ESTATE_TAX_PROVINCE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string EstateTaxProvinceId { get; set; }

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

    [InverseProperty("EstateTaxCounty")]
    public virtual ICollection<EstateTaxCity> EstateTaxCities { get; set; } = new List<EstateTaxCity>();

    [InverseProperty("EstateTaxCounty")]
    public virtual ICollection<EstateTaxInquiry> EstateTaxInquiries { get; set; } = new List<EstateTaxInquiry>();

    [ForeignKey("EstateTaxProvinceId")]
    [InverseProperty("EstateTaxCounties")]
    public virtual EstateTaxProvince EstateTaxProvince { get; set; }
}
