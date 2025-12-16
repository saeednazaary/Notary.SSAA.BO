using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سمافور برای تایید نهایی گواهی امضا
/// </summary>
[Table("SIGN_REQUEST_SEMAPHORE")]
[Index("LastChangeDate", Name = "IDX_SIGN_REQUEST_SEMAPHORE###LAST_CHANGE_DATE")]
[Index("LastChangeTime", Name = "IDX_SIGN_REQUEST_SEMAPHORE###LAST_CHANGE_TIME")]
[Index("RecordDate", Name = "IDX_SIGN_REQUEST_SEMAPHORE###RECORD_DATE")]
[Index("SignRequestId", Name = "IDX_SIGN_REQUEST_SEMAPHORE###SIGN_REQUEST_ID")]
[Index("State", Name = "IDX_SIGN_REQUEST_SEMAPHORE###STATE")]
[Index("ScriptoriumId", Name = "UDX_SIGN_REQUEST_SEMAPHORE###SCRIPTORIUM_ID", IsUnique = true)]
public partial class SignRequestSemaphore
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه دفترخانه مربوطه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شناسه گواهی امضا مربوطه
    /// </summary>
    [Column("SIGN_REQUEST_ID")]
    public Guid SignRequestId { get; set; }

    /// <summary>
    /// اقلام اطلاعاتی گواهی امضا مربوطه قبل از شروع عملیات
    /// </summary>
    [Required]
    [Column("ORIGINAL_SIGN_REQUEST_DATA", TypeName = "CLOB")]
    public string OriginalSignRequestData { get; set; }

    /// <summary>
    /// اقلام اطلاعاتی گواهی امضا مربوطه بعد از شروع عملیات
    /// </summary>
    [Required]
    [Column("NEW_SIGN_REQUEST_DATA", TypeName = "CLOB")]
    public string NewSignRequestData { get; set; }

    /// <summary>
    /// اقلام اطلاعاتی ركوردهایی كه در دفتر الكترونیك گواهی امضاء ساخته خواهند شد
    /// </summary>
    [Required]
    [Column("SIGN_ELECTRONIC_BOOK_DATA", TypeName = "CLOB")]
    public string SignElectronicBookData { get; set; }

    /// <summary>
    /// آخرین شماره ترتیب گواهی امضاء در دفترخانه مربوطه قبل از شروع عملیات
    /// </summary>
    [Column("LAST_CLASSIFY_NO")]
    [Precision(8)]
    public int LastClassifyNo { get; set; }

    /// <summary>
    /// تاریخ آخرین تغییر
    /// </summary>
    [Required]
    [Column("LAST_CHANGE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LastChangeDate { get; set; }

    /// <summary>
    /// زمان آخرین تغییر
    /// </summary>
    [Column("LAST_CHANGE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string LastChangeTime { get; set; }

    /// <summary>
    /// تاریخ ایجاد ركورد
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

    /// <summary>
    /// وضعیت سمافور
    /// </summary>
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    [ForeignKey("SignRequestId")]
    [InverseProperty("SignRequestSemaphores")]
    public virtual SignRequest SignRequest { get; set; }
}
