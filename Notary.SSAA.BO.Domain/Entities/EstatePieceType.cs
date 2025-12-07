using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع قطعه ملكی
/// </summary>
[Table("ESTATE_PIECE_TYPE")]
[Index("EstatePieceMainTypeId", Name = "IX_SSR_EPIETYP_MANTYPID")]
public partial class EstatePieceType
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
    [Required]
    [Column("CODE")]
    [StringLength(4)]
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

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("ESTATE_PIECE_MAIN_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string EstatePieceMainTypeId { get; set; }

    [InverseProperty("EstatePieceType")]
    public virtual ICollection<DocumentEstateSeparationPiece> DocumentEstateSeparationPieces { get; set; } = new List<DocumentEstateSeparationPiece>();

    [ForeignKey("EstatePieceMainTypeId")]
    [InverseProperty("EstatePieceTypes")]
    public virtual EstatePieceMainType EstatePieceMainType { get; set; }

    [InverseProperty("EstatePieceType")]
    public virtual ICollection<EstateTaxInquiryAttach> EstateTaxInquiryAttaches { get; set; } = new List<EstateTaxInquiryAttach>();
}
