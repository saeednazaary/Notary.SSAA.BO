using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نسخه های پشتیبان و نهایی اسناد رسمی
/// </summary>
[Table("DOCUMENT_FILE")]
[Index("FirstFileCreateDate", Name = "IDX_DOCUMENT_FILE###FIRST_FILE_CREATE_DATE")]
[Index("FirstFileCreateTime", Name = "IDX_DOCUMENT_FILE###FIRST_FILE_CREATE_TIME")]
[Index("Ilm", Name = "IDX_DOCUMENT_FILE###ILM")]
[Index("LastFileCreateDate", Name = "IDX_DOCUMENT_FILE###LAST_FILE_CREATE_DATE")]
[Index("LastFileCreateTime", Name = "IDX_DOCUMENT_FILE###LAST_FILE_CREATE_TIME")]
[Index("RecordDate", Name = "IDX_DOCUMENT_FILE###RECORD_DATE")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_FILE###SCRIPTORIUM_ID")]
[Index("LegacyId", Name = "UDX_DOCUMENT_FILE###LEGACY_ID", IsUnique = true)]
[Index("DocumentId", Name = "UX_SSR_DC_FILE_DID", IsUnique = true)]
public partial class DocumentFile
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
    /// محتوای فایل نسخه پشتیبان سند
    /// </summary>
    [Column("FIRST_FILE", TypeName = "BLOB")]
    public byte[] FirstFile { get; set; }

    /// <summary>
    /// تاریخ ایجاد فایل نسخه پشتیبان سند
    /// </summary>
    [Column("FIRST_FILE_CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FirstFileCreateDate { get; set; }

    /// <summary>
    /// زمان ایجاد فایل نسخه پشتیبان سند
    /// </summary>
    [Column("FIRST_FILE_CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string FirstFileCreateTime { get; set; }

    /// <summary>
    /// محتوای فایل نسخه نهایی سند
    /// </summary>
    [Column("LAST_FILE", TypeName = "BLOB")]
    public byte[] LastFile { get; set; }

    /// <summary>
    /// تاریخ ایجاد فایل نسخه نهایی سند
    /// </summary>
    [Column("LAST_FILE_CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LastFileCreateDate { get; set; }

    /// <summary>
    /// زمان ایجاد فایل نسخه نهایی سند
    /// </summary>
    [Column("LAST_FILE_CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string LastFileCreateTime { get; set; }

    /// <summary>
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// تاریخ ایجاد ركورد به میلادی
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
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentFile")]
    public virtual Document Document { get; set; }
}
