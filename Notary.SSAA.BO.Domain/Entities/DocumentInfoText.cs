using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// متون اسناد رسمی
/// </summary>
[Table("DOCUMENT_INFO_TEXT")]
[Index("Ilm", Name = "IDX_DOCUMENT_INFO_TEXT###ILM")]
[Index("RecordDate", Name = "IDX_DOCUMENT_INFO_TEXT###RECORD_DATE")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_INFO_TEXT###SCRIPTORIUM_ID")]
[Index("DocumentId", Name = "UX_SSR_DC_TXT_DID", IsUnique = true)]
public partial class DocumentInfoText
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
    /// متن سند
    /// </summary>
    [Column("DOCUMENT_TEXT", TypeName = "CLOB")]
    public string DocumentText { get; set; }

    /// <summary>
    /// متن حقوقی سند
    /// </summary>
    [Column("LEGAL_TEXT", TypeName = "CLOB")]
    public string LegalText { get; set; }

    /// <summary>
    /// فونت متن سند
    /// </summary>
    [Column("DOCUMENT_TEXT_FONT_SIZE", TypeName = "NUMBER(5,2)")]
    public decimal? DocumentTextFontSize { get; set; }

    /// <summary>
    /// شیوه چاپ سند
    /// </summary>
    [Column("PRINT_MODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string PrintMode { get; set; }

    /// <summary>
    /// متن شرط
    /// </summary>
    [Column("CONDITIONS_TEXT", TypeName = "CLOB")]
    public string ConditionsText { get; set; }

    /// <summary>
    /// توضیحات در مورد سند پس از تأیید نهایی
    /// </summary>
    [Column("DOCUMENT_MODIFY_DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string DocumentModifyDescription { get; set; }

    /// <summary>
    /// ملاحظات و توضیحات در مورد مشخصات سند
    /// </summary>
    [Column("DOCUMENT_DESCRIPTION", TypeName = "CLOB")]
    public string DocumentDescription { get; set; }

    /// <summary>
    /// ملاحظات و توضیحات در مورد پرونده
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Description { get; set; }

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
    [InverseProperty("DocumentInfoText")]
    public virtual Document Document { get; set; }
}
