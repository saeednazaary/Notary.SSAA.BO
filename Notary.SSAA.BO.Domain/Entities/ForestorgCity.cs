using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// شهرستان سازمان جنگل ها
/// </summary>
[Table("FORESTORG_CITY")]
[Index("ForestorgProvinceId", Name = "IX_SSR_FOGCTY_PROVID")]
public partial class ForestorgCity
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
    /// شماره حساب
    /// </summary>
    [Column("ACCOUNT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string AccountNo { get; set; }

    /// <summary>
    /// شماره بهداد
    /// </summary>
    [Column("BEHDAD_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string BehdadNo { get; set; }

    /// <summary>
    /// شماره شبا
    /// </summary>
    [Column("SHEBA_NO")]
    [StringLength(30)]
    [Unicode(false)]
    public string ShebaNo { get; set; }

    /// <summary>
    /// ردیف استان
    /// </summary>
    [Required]
    [Column("FORESTORG_PROVINCE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string ForestorgProvinceId { get; set; }

    /// <summary>
    /// شناسه سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("ForestorgCity")]
    public virtual ICollection<ForestorgInquiry> ForestorgInquiries { get; set; } = new List<ForestorgInquiry>();

    [InverseProperty("ForestorgCity")]
    public virtual ICollection<ForestorgInquiryResponse> ForestorgInquiryResponses { get; set; } = new List<ForestorgInquiryResponse>();

    [ForeignKey("ForestorgProvinceId")]
    [InverseProperty("ForestorgCities")]
    public virtual ForestorgProvince ForestorgProvince { get; set; }

    [InverseProperty("ForestorgCity")]
    public virtual ICollection<ForestorgSection> ForestorgSections { get; set; } = new List<ForestorgSection>();
}
