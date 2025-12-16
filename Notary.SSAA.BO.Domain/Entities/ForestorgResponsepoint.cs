using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// مختصات نقاط پاسخ استعلام جنگل ها
/// </summary>
[Table("FORESTORG_RESPONSEPOINT")]
[Index("ForestorgInquiryResponseId", Name = "IX_SSR_FOGRSPNSPNT_INQRESID")]
public partial class ForestorgResponsepoint
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// Lat
    /// </summary>
    [Column("LAT")]
    [StringLength(200)]
    [Unicode(false)]
    public string Lat { get; set; }

    /// <summary>
    /// Lng
    /// </summary>
    [Column("LNG")]
    [StringLength(200)]
    [Unicode(false)]
    public string Lng { get; set; }

    /// <summary>
    /// كد دسته بندی
    /// </summary>
    [Column("GROUP_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string GroupCode { get; set; }

    /// <summary>
    /// ردیف پاسخ استعلام
    /// </summary>
    [Column("FORESTORG_INQUIRY_RESPONSE_ID")]
    public Guid ForestorgInquiryResponseId { get; set; }

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

    [ForeignKey("ForestorgInquiryResponseId")]
    [InverseProperty("ForestorgResponsepoints")]
    public virtual ForestorgInquiryResponse ForestorgInquiryResponse { get; set; }
}
