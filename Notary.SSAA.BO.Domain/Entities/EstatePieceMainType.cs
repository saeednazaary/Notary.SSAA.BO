using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع اصلی قطعه ملكی
/// </summary>
[Table("ESTATE_PIECE_MAIN_TYPE")]
public partial class EstatePieceMainType
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

    [InverseProperty("EstatePieceMainType")]
    public virtual ICollection<EstatePieceType> EstatePieceTypes { get; set; } = new List<EstatePieceType>();
}
