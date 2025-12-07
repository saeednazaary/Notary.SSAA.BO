using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// استان امور مالیاتی
/// </summary>
[Table("ESTATE_TAX_PROVINCE")]
public partial class EstateTaxProvince
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
    /// كد
    /// </summary>
    [Column("CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string Code { get; set; }

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

    [InverseProperty("EstateTaxProvince")]
    public virtual ICollection<EstateTaxCounty> EstateTaxCounties { get; set; } = new List<EstateTaxCounty>();

    [InverseProperty("FkEstateTaxProvince")]
    public virtual ICollection<EstateTaxInquiry> EstateTaxInquiries { get; set; } = new List<EstateTaxInquiry>();
}
