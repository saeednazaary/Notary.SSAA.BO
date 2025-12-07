using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سری دفتر
/// </summary>
[Table("ESTATE_SERIDAFTAR")]
public partial class EstateSeridaftar
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// كد ثبتی
    /// </summary>
    [Required]
    [Column("SSAA_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SsaaCode { get; set; }

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
    /// ردیف واحد ثبتی
    /// </summary>
    [Required]
    [Column("UNIT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string UnitId { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("EstateSeridaftar")]
    public virtual ICollection<DocumentEstateOwnershipDocument> DocumentEstateOwnershipDocuments { get; set; } = new List<DocumentEstateOwnershipDocument>();

    [InverseProperty("EstateSeridaftar")]
    public virtual ICollection<EstateInquiry> EstateInquiries { get; set; } = new List<EstateInquiry>();
}
