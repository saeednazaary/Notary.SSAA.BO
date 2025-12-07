using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع حكم
/// </summary>
[Table("DOCUMENT_JUDGEMENT_TYPE")]
[Index("State", Name = "IDX_DOCUMENT_JUDGEMENT_TYPE###STATE")]
[Index("Code", Name = "UDX_DOCUMENT_JUDGEMENT_TYPE###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_DOCUMENT_JUDGEMENT_TYPE###TITLE", IsUnique = true)]
public partial class DocumentJudgementType
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(1)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(1)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(50)]
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

    [InverseProperty("DocumentJudgementType")]
    public virtual ICollection<DocumentInfoJudgement> DocumentInfoJudgements { get; set; } = new List<DocumentInfoJudgement>();
}
