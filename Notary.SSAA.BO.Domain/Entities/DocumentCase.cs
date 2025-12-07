using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// موارد ثبت شده در اسناد
/// </summary>
[Table("DOCUMENT_CASE")]
[Index("Ilm", Name = "IDX_DOCUMENT_CASE###ILM")]
[Index("RowNo", Name = "IDX_DOCUMENT_CASE###ROW_NO")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_CASE###SCRIPTORIUM_ID")]
[Index("Title", Name = "IDX_DOCUMENT_CASE###TITLE")]
[Index("DocumentId", "RowNo", Name = "IDX_SSR_DC_CASE_DID_ROW_NO")]
[Index("LegacyId", Name = "UDX_DOCUMENT_CASE###LEGACY_ID", IsUnique = true)]
public partial class DocumentCase
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
    /// ردیف
    /// </summary>
    [Column("ROW_NO")]
    [Precision(5)]
    public short RowNo { get; set; }

    /// <summary>
    /// عنوان مورد ثبت
    /// </summary>
    [Column("TITLE")]
    [StringLength(100)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Description { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده
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
    [Column("OLD_DOCUMENT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentId { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentCases")]
    public virtual Document Document { get; set; }
}
