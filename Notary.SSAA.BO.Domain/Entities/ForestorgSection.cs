using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// بخش های سازمان جنگل ها
/// </summary>
[Table("FORESTORG_SECTION")]
[Index("ForestorgCityId", Name = "IX_SSR_FOGSCTN_CTYID")]
public partial class ForestorgSection
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
    [Required]
    [Column("CODE")]
    [StringLength(5)]
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
    /// ردیف شهرستان
    /// </summary>
    [Required]
    [Column("FORESTORG_CITY_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ForestorgCityId { get; set; }

    /// <summary>
    /// شناسه سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("ForestorgCityId")]
    [InverseProperty("ForestorgSections")]
    public virtual ForestorgCity ForestorgCity { get; set; }

    [InverseProperty("ForestorgSection")]
    public virtual ICollection<ForestorgInquiry> ForestorgInquiries { get; set; } = new List<ForestorgInquiry>();

    [InverseProperty("ForestorgSection")]
    public virtual ICollection<ForestorgInquiryResponse> ForestorgInquiryResponses { get; set; } = new List<ForestorgInquiryResponse>();
}
