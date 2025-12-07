using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// فایل های پیوست استعلام مالیاتی
/// </summary>
[Table("ESTATE_TAX_INQUIRY_FILE")]
[Index("EstateTaxInquiryId", Name = "IX_SSR_ESTATE_FILE_INQUID")]
public partial class EstateTaxInquiryFile
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// تاریخ پیوست
    /// </summary>
    [Column("ATTACHMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string AttachmentDate { get; set; }

    /// <summary>
    /// شرح
    /// </summary>
    [Column("ATTACHMENT_DESC", TypeName = "CLOB")]
    public string AttachmentDesc { get; set; }

    /// <summary>
    /// شماره پیوست
    /// </summary>
    [Column("ATTACHMENT_NO")]
    [StringLength(30)]
    [Unicode(false)]
    public string AttachmentNo { get; set; }

    /// <summary>
    /// عنوان پیوست
    /// </summary>
    [Column("ATTACHMENT_TITLE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string AttachmentTitle { get; set; }

    /// <summary>
    /// تاریخ ثبت
    /// </summary>
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
    /// ردیف استعلام مرتبط
    /// </summary>
    [Column("ESTATE_TAX_INQUIRY_ID")]
    public Guid EstateTaxInquiryId { get; set; }

    /// <summary>
    /// محتویات فایل
    /// </summary>
    [Column("FILE_CONTENT", TypeName = "BLOB")]
    public byte[] FileContent { get; set; }

    /// <summary>
    /// پسوند فایل
    /// </summary>
    [Required]
    [Column("FILE_EXTENTION")]
    [StringLength(15)]
    [Unicode(false)]
    public string FileExtention { get; set; }

    /// <summary>
    /// ردیف دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// تاریخ ارسال
    /// </summary>
    [Column("SEND_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SendDate { get; set; }

    /// <summary>
    /// زمان ارسال
    /// </summary>
    [Column("SEND_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SendTime { get; set; }

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
    /// شناسه در سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("ARCHIVE_MEDIA_FILE_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string ArchiveMediaFileId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("ARCHIVE_ATTACHMENT_TYPE_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string ArchiveAttachmentTypeId { get; set; }

    /// <summary>
    /// وضعيت
    /// </summary>
    [Column("CHANGE_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string ChangeState { get; set; }

    [ForeignKey("EstateTaxInquiryId")]
    [InverseProperty("EstateTaxInquiryFiles")]
    public virtual EstateTaxInquiry EstateTaxInquiry { get; set; }
}
