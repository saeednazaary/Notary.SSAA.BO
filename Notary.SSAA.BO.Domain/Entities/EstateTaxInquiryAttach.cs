using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// منضمات ملك مورد استعلام
/// </summary>
[Table("ESTATE_TAX_INQUIRY_ATTACH")]
[Index("EstateTaxInquiryId", Name = "IX_SSR_ESTATE_ATCH_INQID")]
[Index("EstatePieceTypeId", Name = "IX_SSR_ESTATE_ATCH_PTYPID")]
[Index("EstateSideTypeId", Name = "IX_SSR_ESTATE_ATCH_STYPID")]
public partial class EstateTaxInquiryAttach
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// مساحت
    /// </summary>
    [Column("AREA", TypeName = "NUMBER(20,3)")]
    public decimal Area { get; set; }

    /// <summary>
    /// بلوك
    /// </summary>
    [Required]
    [Column("BLOCK")]
    [StringLength(50)]
    [Unicode(false)]
    public string Block { get; set; }

    /// <summary>
    /// ردیف نوع قطعه
    /// </summary>
    [Required]
    [Column("ESTATE_PIECE_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string EstatePieceTypeId { get; set; }

    /// <summary>
    /// ردیف سمت
    /// </summary>
    [Required]
    [Column("ESTATE_SIDE_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string EstateSideTypeId { get; set; }

    /// <summary>
    /// ردیف استعلام مرتبط
    /// </summary>
    [Column("ESTATE_TAX_INQUIRY_ID")]
    public Guid EstateTaxInquiryId { get; set; }

    /// <summary>
    /// طبقه
    /// </summary>
    [Required]
    [Column("FLOOR")]
    [StringLength(50)]
    [Unicode(false)]
    public string Floor { get; set; }

    /// <summary>
    /// شماره قطعه
    /// </summary>
    [Required]
    [Column("PIECE")]
    [StringLength(50)]
    [Unicode(false)]
    public string Piece { get; set; }

    /// <summary>
    /// ردیف دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// فيلد عددي کنترلي مخصوص کنترل concurrency
    /// </summary>
    [Column("TIMESTAMP", TypeName = "NUMBER(20)")]
    public decimal Timestamp { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه در سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// وضعيت
    /// </summary>
    [Column("CHANGE_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string ChangeState { get; set; }

    [ForeignKey("EstatePieceTypeId")]
    [InverseProperty("EstateTaxInquiryAttaches")]
    public virtual EstatePieceType EstatePieceType { get; set; }

    [ForeignKey("EstateSideTypeId")]
    [InverseProperty("EstateTaxInquiryAttaches")]
    public virtual EstateSideType EstateSideType { get; set; }

    [ForeignKey("EstateTaxInquiryId")]
    [InverseProperty("EstateTaxInquiryAttaches")]
    public virtual EstateTaxInquiry EstateTaxInquiry { get; set; }
}
