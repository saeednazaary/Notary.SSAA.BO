using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سهم مالكین از قطعات تفكیك شده در تقسیم نامه
/// </summary>
[Table("DOCUMENT_ESTATE_SEPARATION_PIECES_QUOTA")]
[Index("DocumentPersonId", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES_QUOTA###DOCUMENT_PERSON_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES_QUOTA###ILM")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES_QUOTA###SCRIPTORIUM_ID")]
[Index("DocumentEstateSeparationPiecesId", "DocumentPersonId", Name = "IDX_SSR_DC_SEP_PSQ_PID_PRSID")]
[Index("LegacyId", Name = "UDX_DOCUMENT_ESTATE_SEPARATION_PIECES_QUOTA###LEGACY_ID", IsUnique = true)]
public partial class DocumentEstateSeparationPiecesQuotum
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه قطعات تقسیم شده در اسناد تقسیم نامه ملكی
    /// </summary>
    [Column("DOCUMENT_ESTATE_SEPARATION_PIECES_ID")]
    public Guid DocumentEstateSeparationPiecesId { get; set; }

    /// <summary>
    /// شناسه اشخاص اسناد
    /// </summary>
    [Column("DOCUMENT_PERSON_ID")]
    public Guid DocumentPersonId { get; set; }

    /// <summary>
    /// جزء سهم
    /// </summary>
    [Column("DETAIL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? DetailQuota { get; set; }

    /// <summary>
    /// كل سهم
    /// </summary>
    [Column("TOTAL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? TotalQuota { get; set; }

    /// <summary>
    /// متن سهم
    /// </summary>
    [Column("QUOTA_TEXT")]
    [StringLength(2000)]
    [Unicode(false)]
    public string QuotaText { get; set; }

    /// <summary>
    /// متن شرط مندرج در خلاصه معامله شخص
    /// </summary>
    [Column("DEAL_SUMMARY_PERSON_CONDITIONS")]
    [StringLength(2000)]
    [Unicode(false)]
    public string DealSummaryPersonConditions { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده سند
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// Information Lifecycle Management
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ESTATE_SEPARATION_PIECES_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentEstateSeparationPiecesId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_PERSON_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentPersonId { get; set; }

    [ForeignKey("DocumentEstateSeparationPiecesId")]
    [InverseProperty("DocumentEstateSeparationPiecesQuota")]
    public virtual DocumentEstateSeparationPiece DocumentEstateSeparationPieces { get; set; }

    [ForeignKey("DocumentPersonId")]
    [InverseProperty("DocumentEstateSeparationPiecesQuota")]
    public virtual DocumentPerson DocumentPerson { get; set; }
}
