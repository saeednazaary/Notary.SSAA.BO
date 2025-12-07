using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سمافور برای تایید نهایی سند
/// </summary>
[Table("DOCUMENT_SEMAPHORE")]
[Index("DocumentId", Name = "IDX_DOCUMENT_SEMAPHORE###DOCUMENT_ID")]
[Index("LastChangeDate", Name = "IDX_DOCUMENT_SEMAPHORE###LAST_CHANGE_DATE")]
[Index("LastChangeTime", Name = "IDX_DOCUMENT_SEMAPHORE###LAST_CHANGE_TIME")]
[Index("RecordDate", Name = "IDX_DOCUMENT_SEMAPHORE###RECORD_DATE")]
[Index("State", Name = "IDX_DOCUMENT_SEMAPHORE###STATE")]
[Index("ScriptoriumId", "OperationType", Name = "UX_SSR_DOC_SEMAPHORE_SID_OPT", IsUnique = true)]
public partial class DocumentSemaphore
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
    /// شناسه سند مربوطه
    /// </summary>
    [Column("DOCUMENT_ID")]
    public Guid? DocumentId { get; set; }

    /// <summary>
    /// اقلام اطلاعاتی سند مربوطه قبل از شروع عملیات
    /// </summary>
    [Required]
    [Column("ORIGINAL_DOCUMENT_DATA", TypeName = "CLOB")]
    public string OriginalDocumentData { get; set; }

    /// <summary>
    /// اقلام اطلاعاتی سند مربوطه بعد از شروع عملیات
    /// </summary>
    [Required]
    [Column("NEW_DOCUMENT_DATA", TypeName = "CLOB")]
    public string NewDocumentData { get; set; }

    /// <summary>
    /// اقلام اطلاعاتی ركوردهایی كه در دفتر الكترونیك سندء ساخته خواهند شد
    /// </summary>
    [Required]
    [Column("DOCUMENT_ELECTRONIC_BOOK_DATA", TypeName = "CLOB")]
    public string DocumentElectronicBookData { get; set; }

    /// <summary>
    /// آخرین شماره ترتیب سندء در دفترخانه مربوطه قبل از شروع عملیات
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

    /// <summary>
    /// نوع عملیات
    /// </summary>
    [Required]
    [Column("OPERATION_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string OperationType { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentSemaphores")]
    public virtual Document Document { get; set; }
}
