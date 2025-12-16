using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اطلاعات حكم دادگاه یا سایر مراجع قانونی كه بر مبنای آن سند رسمی صادر شده است
/// </summary>
[Table("DOCUMENT_INFO_JUDGEMENT")]
[Index("DocumentJudgementTypeId", Name = "IDX_DOCUMENT_INFO_JUDGEMENT###DOCUMENT_JUDGEMENT_TYPE_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_INFO_JUDGEMENT###ILM")]
[Index("RecordDate", Name = "IDX_DOCUMENT_INFO_JUDGEMENT###RECORD_DATE")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_INFO_JUDGEMENT###SCRIPTORIUM_ID")]
[Index("DocumentId", Name = "UX_SSR_DC_JUDG_DID", IsUnique = true)]
public partial class DocumentInfoJudgement
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه سند
    /// </summary>
    [Column("DOCUMENT_ID")]
    public Guid DocumentId { get; set; }

    /// <summary>
    /// شناسه نوع حكم
    /// </summary>
    [Column("DOCUMENT_JUDGEMENT_TYPE_ID")]
    [StringLength(1)]
    [Unicode(false)]
    public string DocumentJudgementTypeId { get; set; }

    /// <summary>
    /// صادركننده
    /// </summary>
    [Column("ISSUER_NAME")]
    [StringLength(150)]
    [Unicode(false)]
    public string IssuerName { get; set; }

    /// <summary>
    /// شماره حكم
    /// </summary>
    [Column("ISSUE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string IssueNo { get; set; }

    /// <summary>
    /// تاریخ حكم
    /// </summary>
    [Column("ISSUE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string IssueDate { get; set; }

    /// <summary>
    /// شماره كلاسه حكم
    /// </summary>
    [Column("CASE_CLASSIFY_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string CaseClassifyNo { get; set; }

    /// <summary>
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

    /// <summary>
    /// Information Lifecycle Management
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentId { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentInfoJudgement")]
    public virtual Document Document { get; set; }

    [ForeignKey("DocumentJudgementTypeId")]
    [InverseProperty("DocumentInfoJudgements")]
    public virtual DocumentJudgementType DocumentJudgementType { get; set; }
}
