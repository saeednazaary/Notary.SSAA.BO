using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// پیامك های ارسالی در مورد اسناد
/// </summary>
[Table("DOCUMENT_SMS")]
[Index("CreateDate", Name = "IDX_DOCUMENT_SMS###CREATE_DATE")]
[Index("CreateTime", Name = "IDX_DOCUMENT_SMS###CREATE_TIME")]
[Index("DocumentId", Name = "IDX_DOCUMENT_SMS###DOCUMENT_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_SMS###ILM")]
[Index("IsMechanized", Name = "IDX_DOCUMENT_SMS###IS_MECHANIZED")]
[Index("IsSent", Name = "IDX_DOCUMENT_SMS###IS_SENT")]
[Index("MobileNo", Name = "IDX_DOCUMENT_SMS###MOBILE_NO")]
[Index("RecordDate", Name = "IDX_DOCUMENT_SMS###RECORD_DATE")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_SMS###SCRIPTORIUM_ID")]
[Index("Trmsmsid", Name = "IDX_DOCUMENT_SMS###TRMSMSID")]
[Index("LegacyId", Name = "UDX_DOCUMENT_SMS###LEGACY_ID", IsUnique = true)]
public partial class DocumentSm
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه پرونده خدمات ثبتی
    /// </summary>
    [Column("DOCUMENT_ID")]
    public Guid DocumentId { get; set; }

    /// <summary>
    /// شماره همراه جهت ارسال پیام كوتاه
    /// </summary>
    [Required]
    [Column("MOBILE_NO")]
    [StringLength(15)]
    [Unicode(false)]
    public string MobileNo { get; set; }

    /// <summary>
    /// نام گیرنده
    /// </summary>
    [Required]
    [Column("RECEIVER_NAME")]
    [StringLength(400)]
    [Unicode(false)]
    public string ReceiverName { get; set; }

    /// <summary>
    /// شناسه ركورد پیامك در جدول پیامك فریمورك قدیم
    /// </summary>
    [Column("TRMSMSID")]
    [StringLength(50)]
    [Unicode(false)]
    public string Trmsmsid { get; set; }

    /// <summary>
    /// متن پیامك
    /// </summary>
    [Required]
    [Column("SMS_TEXT")]
    [StringLength(1000)]
    [Unicode(false)]
    public string SmsText { get; set; }

    /// <summary>
    /// آیا پیامك به صورت مكانیزه تولید شده است؟
    /// </summary>
    [Required]
    [Column("IS_MECHANIZED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsMechanized { get; set; }

    /// <summary>
    /// آیا پیامك ارسال شده است؟
    /// </summary>
    [Required]
    [Column("IS_SENT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSent { get; set; }

    /// <summary>
    /// تاریخ ثبت
    /// </summary>
    [Required]
    [Column("CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CreateDate { get; set; }

    /// <summary>
    /// زمان ثبت
    /// </summary>
    [Required]
    [Column("CREATE_TIME")]
    [StringLength(5)]
    [Unicode(false)]
    public string CreateTime { get; set; }

    /// <summary>
    /// تاریخ ركورد به میلادی
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

    /// <summary>
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// وضعیت ركورد از لحاظ آرشیو
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه ركورد در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentId { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentSms")]
    public virtual Document Document { get; set; }

    [InverseProperty("DocumentSms")]
    public virtual ICollection<DocumentPersonRelated> DocumentPersonRelateds { get; set; } = new List<DocumentPersonRelated>();
}
