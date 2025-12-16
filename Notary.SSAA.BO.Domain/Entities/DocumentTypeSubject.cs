using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع موضوعات اسناد
/// </summary>
[Table("DOCUMENT_TYPE_SUBJECT")]
[Index("Code", Name = "IDX_DOCUMENT_TYPE_SUBJECT###CODE")]
[Index("State", Name = "IDX_DOCUMENT_TYPE_SUBJECT###STATE")]
[Index("Title", Name = "IDX_DOCUMENT_TYPE_SUBJECT###TITLE")]
[Index("DocumentTypeId", "Code", Name = "UDX_DOCUMENT_TYPE_SUBJECT###DOCUMENT_TYPE_ID#CODE", IsUnique = true)]
[Index("DocumentTypeId", "Title", Name = "UDX_DOCUMENT_TYPE_SUBJECT###DOCUMENT_TYPE_ID#TITLE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_DOCUMENT_TYPE_SUBJECT###LEGACY_ID", IsUnique = true)]
public partial class DocumentTypeSubject
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
    /// شناسه نوع سند
    /// </summary>
    [Required]
    [Column("DOCUMENT_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string DocumentTypeId { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(4)]
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
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("DocumentTypeSubject")]
    public virtual ICollection<DocumentInfoOther> DocumentInfoOthers { get; set; } = new List<DocumentInfoOther>();

    [ForeignKey("DocumentTypeId")]
    [InverseProperty("DocumentTypeSubjects")]
    public virtual DocumentType DocumentType { get; set; }
}
