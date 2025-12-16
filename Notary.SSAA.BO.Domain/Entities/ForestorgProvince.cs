using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// استان سازمان جنگل ها
/// </summary>
[Table("FORESTORG_PROVINCE")]
public partial class ForestorgProvince
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
    [StringLength(2000)]
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
    /// شناسه سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("ForestorgProvince")]
    public virtual ICollection<ForestorgCity> ForestorgCities { get; set; } = new List<ForestorgCity>();

    [InverseProperty("ForestorgProvince")]
    public virtual ICollection<ForestorgInquiry> ForestorgInquiries { get; set; } = new List<ForestorgInquiry>();

    [InverseProperty("ForestorgProvince")]
    public virtual ICollection<ForestorgInquiryResponse> ForestorgInquiryResponses { get; set; } = new List<ForestorgInquiryResponse>();
}
