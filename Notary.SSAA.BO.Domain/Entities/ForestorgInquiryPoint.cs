using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// مختصات ملك
/// </summary>
[Table("FORESTORG_INQUIRY_POINT")]
[Index("ForestorgInquiryId", Name = "IX_SSR_FOGINQPNT_INQID")]
public partial class ForestorgInquiryPoint
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// ایكس
    /// </summary>
    [Column(TypeName = "NUMBER(10,4)")]
    public decimal? X { get; set; }

    /// <summary>
    /// ایگرگ
    /// </summary>
    [Column(TypeName = "NUMBER(11,4)")]
    public decimal? Y { get; set; }

    /// <summary>
    /// زون
    /// </summary>
    [Column("ZONE")]
    [Precision(5)]
    public short? Zone { get; set; }

    /// <summary>
    /// ردیف عامل پارتیشن
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// ردیف استعلام
    /// </summary>
    [Column("FORESTORG_INQUIRY_ID")]
    public Guid ForestorgInquiryId { get; set; }

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
    [InverseProperty("ForestorgInquiryPoints")]
    public virtual ForestorgInquiry ForestorgInquiry { get; set; }
}
