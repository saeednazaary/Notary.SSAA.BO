using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جنس قطعه تقسیم شده در اسناد تقسیم نامه ملكی
/// </summary>
[Table("DOCUMENT_ESTATE_SEPARATION_PIECE_KIND")]
[Index("State", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECE_KIND###STATE")]
[Index("Code", Name = "UDX_DOCUMENT_ESTATE_SEPARATION_PIECE_KIND###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_DOCUMENT_ESTATE_SEPARATION_PIECE_KIND###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_DOCUMENT_ESTATE_SEPARATION_PIECE_KIND###TITLE", IsUnique = true)]
public partial class DocumentEstateSeparationPieceKind
{
    /// <summary>
    ///  ردیف
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
    [StringLength(100)]
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
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("DocumentEstateSeparationPieceKind")]
    public virtual ICollection<DocumentEstateSeparationPiece> DocumentEstateSeparationPieces { get; set; } = new List<DocumentEstateSeparationPiece>();
}
