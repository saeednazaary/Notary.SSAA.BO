using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// بخش ثبتی
/// </summary>
[Table("ESTATE_SECTION")]
public partial class EstateSection
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

    [InverseProperty("EstateSection")]
    public virtual ICollection<DocumentEstateSeparationPiece> DocumentEstateSeparationPieces { get; set; } = new List<DocumentEstateSeparationPiece>();

    [InverseProperty("EstateSection")]
    public virtual ICollection<EstateDivisionRequestElement> EstateDivisionRequestElements { get; set; } = new List<EstateDivisionRequestElement>();

    [InverseProperty("EstateSection")]
    public virtual ICollection<EstateDocumentRequest> EstateDocumentRequests { get; set; } = new List<EstateDocumentRequest>();

    [InverseProperty("EstateSection")]
    public virtual ICollection<EstateInquiry> EstateInquiries { get; set; } = new List<EstateInquiry>();

    [InverseProperty("EstateSection")]
    public virtual ICollection<EstateSubsection> EstateSubsections { get; set; } = new List<EstateSubsection>();

    [InverseProperty("EstateSection")]
    public virtual ICollection<EstateTaxInquiry> EstateTaxInquiries { get; set; } = new List<EstateTaxInquiry>();

    [InverseProperty("EstateSection")]
    public virtual ICollection<SsrArticle6Inq> SsrArticle6Inqs { get; set; } = new List<SsrArticle6Inq>();
}
