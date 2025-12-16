using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع كاربری حق واگذاری محل
/// </summary>
[Table("ESTATE_TAX_INQUIRY_LOCATION_ASSIGN_RIGTH_USING_TYPE")]
public partial class EstateTaxInquiryLocationAssignRigthUsingType
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
    [Required]
    [Column("CODE")]
    [StringLength(3)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
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

    [InverseProperty("LocationAssignRigthUsingType")]
    public virtual ICollection<EstateTaxInquiry> EstateTaxInquiries { get; set; } = new List<EstateTaxInquiry>();
}
