using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// فایل مرتبط با استعلام جنگل ها
/// </summary>
[Table("FORESTORG_INQUIRY_FILE")]
[Index("ForestorgInquiryId", Name = "IX_SSR_FOGINQFIL_INQID")]
public partial class ForestorgInquiryFile
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شماره
    /// </summary>
    [Column("ATTACHMENT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string AttachmentNo { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Column("ATTACHMENT_TITLE")]
    [StringLength(50)]
    [Unicode(false)]
    public string AttachmentTitle { get; set; }

    /// <summary>
    /// محتویات فایل
    /// </summary>
    [Column("FILE_CONTENT", TypeName = "BLOB")]
    public byte[] FileContent { get; set; }

    /// <summary>
    /// نوع فایل
    /// </summary>
    [Column("FILE_EXTENTION")]
    [StringLength(50)]
    [Unicode(false)]
    public string FileExtention { get; set; }

    /// <summary>
    /// شرح
    /// </summary>
    [Column("DESCRIPTION", TypeName = "CLOB")]
    public string Description { get; set; }

    /// <summary>
    /// ردیف استعلام
    /// </summary>
    [Column("FORESTORG_INQUIRY_ID")]
    public Guid ForestorgInquiryId { get; set; }

    /// <summary>
    /// ردیف عامل پارتیشن
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
    /// شناسه سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("ForestorgInquiryId")]
    [InverseProperty("ForestorgInquiryFiles")]
    public virtual ForestorgInquiry ForestorgInquiry { get; set; }
}
