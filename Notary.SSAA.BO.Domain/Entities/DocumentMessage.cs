using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// پیام های ارسالی در مورد اسناد
/// </summary>
[Table("DOCUMENT_MESSAGES")]
[Index("CreateDate", Name = "IDX_DOCUMENT_MESSAGES###CREATE_DATE")]
[Index("CreateTime", Name = "IDX_DOCUMENT_MESSAGES###CREATE_TIME")]
[Index("DocumentId", Name = "IDX_DOCUMENT_MESSAGES###DOCUMENT_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_MESSAGES###ILM")]
[Index("IsMechanized", Name = "IDX_DOCUMENT_MESSAGES###IS_MECHANIZED")]
[Index("IsSeen", Name = "IDX_DOCUMENT_MESSAGES###IS_SEEN")]
[Index("IsSent", Name = "IDX_DOCUMENT_MESSAGES###IS_SENT")]
[Index("MobileNo", Name = "IDX_DOCUMENT_MESSAGES###MOBILE_NO")]
[Index("RecordDate", Name = "IDX_DOCUMENT_MESSAGES###RECORD_DATE")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_MESSAGES###SCRIPTORIUM_ID")]
[Index("SeenDate", Name = "IDX_DOCUMENT_MESSAGES###SEEN_DATE")]
[Index("SeenTime", Name = "IDX_DOCUMENT_MESSAGES###SEEN_TIME")]
[Index("LegacyId", Name = "UDX_DOCUMENT_MESSAGES###LEGACY_ID", IsUnique = true)]
public partial class DocumentMessage
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
    /// متن پیام
    /// </summary>
    [Required]
    [Column("TEXT")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Text { get; set; }

    /// <summary>
    /// شماره تلفن همراه
    /// </summary>
    [Column("MOBILE_NO")]
    [StringLength(15)]
    [Unicode(false)]
    public string MobileNo { get; set; }

    /// <summary>
    /// گیرنده
    /// </summary>
    [Required]
    [Column("RECEIVER")]
    [StringLength(400)]
    [Unicode(false)]
    public string Receiver { get; set; }

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
    [Column("CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string CreateTime { get; set; }

    /// <summary>
    /// آیا پیام به صورت مكانیزه تولید شده است؟
    /// </summary>
    [Required]
    [Column("IS_MECHANIZED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsMechanized { get; set; }

    /// <summary>
    /// آیا پیام ارسال شده است؟
    /// </summary>
    [Required]
    [Column("IS_SENT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSent { get; set; }

    /// <summary>
    /// آیا پیام توسط گیرنده رؤیت شده است؟
    /// </summary>
    [Column("IS_SEEN")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSeen { get; set; }

    /// <summary>
    /// تاریخ رؤیت
    /// </summary>
    [Column("SEEN_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SeenDate { get; set; }

    /// <summary>
    /// زمان رؤیت
    /// </summary>
    [Column("SEEN_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SeenTime { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده سند
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// تاریخ ركورد به میلادی
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

    /// <summary>
    /// وضعیت ركورد از لحاظ آرشیو
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
    [InverseProperty("DocumentMessages")]
    public virtual Document Document { get; set; }
}
