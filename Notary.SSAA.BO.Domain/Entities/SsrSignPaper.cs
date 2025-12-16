using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// صفحات اسکن شده گواهی امضاء
/// </summary>
[Table("SSR_SIGN_PAPER")]
[Index("ScanFileCreateTime", Name = "IX_SSR_SIGNPEPR_CREATE_TIME")]
[Index("RecordDate", Name = "IX_SSR_SIGNPEPR_RECORD_DATE")]
[Index("ScanLegacyId", Name = "IX_SSR_SIGNPEPR_SCAN_LEGACY_ID")]
[Index("ScriptoriumId", Name = "IX_SSR_SIGNPEPR_SCRIPTORIUM_ID")]
[Index("ScanFileCreateDate", Name = "IX_SSR_SSIGNPEPR_CREATE_DATE")]
[Index("Ilm", Name = "IX_SSR_SSIGNPEPR_ILM")]
[Index("RowNo", Name = "IX_SSR_SSIGNPEPR_ROW_NO")]
[Index("SignRequestId", "RowNo", Name = "UX_SSR_SSIGNPEPR_REQID_RWNO", IsUnique = true)]
public partial class SsrSignPaper
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه گواهی امضاء
    /// </summary>
    [Column("SIGN_REQUEST_ID")]
    public Guid SignRequestId { get; set; }

    /// <summary>
    /// ردیف
    /// </summary>
    [Column("ROW_NO")]
    [Precision(3)]
    public byte RowNo { get; set; }

    /// <summary>
    /// محتوای فایل اسكن گواهی امضاء
    /// </summary>
    [Column("SCAN_FILE", TypeName = "BLOB")]
    public byte[] ScanFile { get; set; }

    /// <summary>
    /// تاریخ ایجاد فایل اسكن گواهی امضاء
    /// </summary>
    [Column("SCAN_FILE_CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ScanFileCreateDate { get; set; }

    /// <summary>
    /// زمان ایجاد فایل اسكن گواهی امضاء
    /// </summary>
    [Column("SCAN_FILE_CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ScanFileCreateTime { get; set; }

    /// <summary>
    /// كلید اصلی ركورد تصویر معادل در سامانه قدیمی
    /// </summary>
    [Column("SCAN_LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string ScanLegacyId { get; set; }

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

    [ForeignKey("SignRequestId")]
    [InverseProperty("SsrSignPapers")]
    public virtual SignRequest SignRequest { get; set; }
}
