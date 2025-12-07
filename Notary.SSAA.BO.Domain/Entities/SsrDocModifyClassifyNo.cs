using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سابقه اصلاح اطلاعات ثبت سند در دفتر
/// </summary>
[Table("SSR_DOC_MODIFY_CLASSIFY_NO")]
[Index("ClassifyNoNew", Name = "IX_SSR_DC_MDFY_CNO_CNNEW")]
[Index("ClassifyNoOld", Name = "IX_SSR_DC_MDFY_CNO_CNOLD")]
[Index("DocumentId", Name = "IX_SSR_DC_MDFY_CNO_DOCID")]
[Index("ModifyDate", Name = "IX_SSR_DC_MDFY_CNO_MDATE")]
[Index("ModifyTime", Name = "IX_SSR_DC_MDFY_CNO_MTIM")]
[Index("ScriptoriumId", Name = "IX_SSR_DOC_MDFY_CLSFYNO_SCRID")]
public partial class SsrDocModifyClassifyNo
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
    /// شماره ترتیب قبلی
    /// </summary>
    [Column("CLASSIFY_NO_OLD")]
    [Precision(6)]
    public int ClassifyNoOld { get; set; }

    /// <summary>
    /// شماره ترتیب جدید
    /// </summary>
    [Column("CLASSIFY_NO_NEW")]
    [Precision(6)]
    public int? ClassifyNoNew { get; set; }

    /// <summary>
    /// تاریخ قبلی ثبت در دفتر
    /// </summary>
    [Required]
    [Column("WRITE_IN_BOOK_DATE_OLD")]
    [StringLength(10)]
    [Unicode(false)]
    public string WriteInBookDateOld { get; set; }

    /// <summary>
    /// تاریخ جدید ثبت در دفتر
    /// </summary>
    [Column("WRITE_IN_BOOK_DATE_NEW")]
    [StringLength(10)]
    [Unicode(false)]
    public string WriteInBookDateNew { get; set; }

    /// <summary>
    /// شماره جلد قبلی دفتر
    /// </summary>
    [Required]
    [Column("REGISTER_VOLUME_NO_OLD")]
    [StringLength(20)]
    [Unicode(false)]
    public string RegisterVolumeNoOld { get; set; }

    /// <summary>
    /// شماره جلد جدید دفتر
    /// </summary>
    [Column("REGISTER_VOLUME_NO_NEW")]
    [StringLength(20)]
    [Unicode(false)]
    public string RegisterVolumeNoNew { get; set; }

    /// <summary>
    /// شماره صفحات قبلی دفتر
    /// </summary>
    [Required]
    [Column("REGISTER_PAPERS_NO_OLD")]
    [StringLength(20)]
    [Unicode(false)]
    public string RegisterPapersNoOld { get; set; }

    /// <summary>
    /// شماره صفحات جدید دفتر
    /// </summary>
    [Column("REGISTER_PAPERS_NO_NEW")]
    [StringLength(20)]
    [Unicode(false)]
    public string RegisterPapersNoNew { get; set; }

    /// <summary>
    /// مشخصات اصلاح كننده
    /// </summary>
    [Required]
    [Column("MODIFIER")]
    [StringLength(200)]
    [Unicode(false)]
    public string Modifier { get; set; }

    /// <summary>
    /// تاریخ اصلاح
    /// </summary>
    [Required]
    [Column("MODIFY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ModifyDate { get; set; }

    /// <summary>
    /// زمان اصلاح
    /// </summary>
    [Required]
    [Column("MODIFY_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string ModifyTime { get; set; }

    /// <summary>
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("SsrDocModifyClassifyNos")]
    public virtual Document Document { get; set; }
}
