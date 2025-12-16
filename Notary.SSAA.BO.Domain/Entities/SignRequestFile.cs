using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نسخه های پشتیبان و نهایی اسناد رسمی
/// </summary>
[Table("SIGN_REQUEST_FILE")]
[Index("Ilm", Name = "IDX_SIGN_REQUEST_FILE###ILM")]
[Index("LastFileCreateDate", Name = "IDX_SIGN_REQUEST_FILE###LAST_FILE_CREATE_DATE")]
[Index("LastFileCreateTime", Name = "IDX_SIGN_REQUEST_FILE###LAST_FILE_CREATE_TIME")]
[Index("RecordDate", Name = "IDX_SIGN_REQUEST_FILE###RECORD_DATE")]
[Index("ScanFileCreateDate", Name = "IDX_SIGN_REQUEST_FILE###SCAN_FILE_CREATE_DATE")]
[Index("ScanFileCreateTime", Name = "IDX_SIGN_REQUEST_FILE###SCAN_FILE_CREATE_TIME")]
[Index("ScriptoriumId", Name = "IDX_SIGN_REQUEST_FILE###SCRIPTORIUM_ID")]
[Index("EdmId", Name = "IX_SSR_SIGN_REQUEST_FILE_EDMID")]
[Index("LastLegacyId", Name = "UDX_SIGN_REQUEST_FILE###LAST_LEGACY_ID", IsUnique = true)]
[Index("ScanLegacyId", Name = "UDX_SIGN_REQUEST_FILE###SCAN_LEGACY_ID", IsUnique = true)]
[Index("SignRequestId", Name = "UDX_SIGN_REQUEST_FILE###SIGN_REQUEST_ID", IsUnique = true)]
public partial class SignRequestFile
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
    /// محتوای فایل نسخه نهایی گواهی امضاء
    /// </summary>
    [Column("LAST_FILE", TypeName = "BLOB")]
    public byte[] LastFile { get; set; }

    /// <summary>
    /// تاریخ ایجاد فایل نسخه نهایی گواهی امضاء
    /// </summary>
    [Column("LAST_FILE_CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LastFileCreateDate { get; set; }

    /// <summary>
    /// زمان ایجاد فایل نسخه نهایی گواهی امضاء
    /// </summary>
    [Column("LAST_FILE_CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string LastFileCreateTime { get; set; }

    /// <summary>
    /// كلید اصلی ركورد تصویر معادل در سامانه قدیمی
    /// </summary>
    [Column("SCAN_LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string ScanLegacyId { get; set; }

    /// <summary>
    /// كلید اصلی ركورد چاپ گواهی امضاء معادل در سامانه قدیمی
    /// </summary>
    [Column("LAST_LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LastLegacyId { get; set; }

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
    /// شناسه سند در مخزن اسناد الکترونيک
    /// </summary>
    [Column("EDM_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string EdmId { get; set; }

    /// <summary>
    /// ورژن سند در مخزن اسناد الکترونيک
    /// </summary>
    [Column("EDM_VERSION")]
    [StringLength(50)]
    [Unicode(false)]
    public string EdmVersion { get; set; }

    [ForeignKey("SignRequestId")]
    [InverseProperty("SignRequestFile")]
    public virtual SignRequest SignRequest { get; set; }
}
