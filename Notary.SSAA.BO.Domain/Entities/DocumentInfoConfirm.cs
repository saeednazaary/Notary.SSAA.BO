using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// تأییدات اسناد رسمی
/// </summary>
[Table("DOCUMENT_INFO_CONFIRM")]
[Index("ConfirmDate", Name = "IDX_DOCUMENT_INFO_CONFIRM###CONFIRM_DATE")]
[Index("ConfirmTime", Name = "IDX_DOCUMENT_INFO_CONFIRM###CONFIRM_TIME")]
[Index("CreateDate", Name = "IDX_DOCUMENT_INFO_CONFIRM###CREATE_DATE")]
[Index("CreateTime", Name = "IDX_DOCUMENT_INFO_CONFIRM###CREATE_TIME")]
[Index("DaftaryarAccessId", Name = "IDX_DOCUMENT_INFO_CONFIRM###DAFTARYAR_ACCESS_ID")]
[Index("DaftaryarConfirmDate", Name = "IDX_DOCUMENT_INFO_CONFIRM###DAFTARYAR_CONFIRM_DATE")]
[Index("DaftaryarConfirmTime", Name = "IDX_DOCUMENT_INFO_CONFIRM###DAFTARYAR_CONFIRM_TIME")]
[Index("DaftaryarSignCertificateDn", Name = "IDX_DOCUMENT_INFO_CONFIRM###DAFTARYAR_SIGN_CERTIFICATE_DN")]
[Index("Ilm", Name = "IDX_DOCUMENT_INFO_CONFIRM###ILM")]
[Index("RecordDate", Name = "IDX_DOCUMENT_INFO_CONFIRM###RECORD_DATE")]
[Index("SardaftarAccessId", Name = "IDX_DOCUMENT_INFO_CONFIRM###SARDAFTAR_ACCESS_ID")]
[Index("SardaftarConfirmDate", Name = "IDX_DOCUMENT_INFO_CONFIRM###SARDAFTAR_CONFIRM_DATE")]
[Index("SardaftarConfirmTime", Name = "IDX_DOCUMENT_INFO_CONFIRM###SARDAFTAR_CONFIRM_TIME")]
[Index("SardaftarSignCertificateDn", Name = "IDX_DOCUMENT_INFO_CONFIRM###SARDAFTAR_SIGN_CERTIFICATE_DN")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_INFO_CONFIRM###SCRIPTORIUM_ID")]
[Index("DocumentId", Name = "UX_SSR_DC_CNFRM_DID", IsUnique = true)]
public partial class DocumentInfoConfirm
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
    /// گواهی امضای الكترونیك مورد استفاده برای امضای سند - توسط سردفتر
    /// </summary>
    [Column("SARDAFTAR_SIGN_CERTIFICATE_DN")]
    [StringLength(1000)]
    [Unicode(false)]
    public string SardaftarSignCertificateDn { get; set; }

    /// <summary>
    /// شناسه دسترسی سردفتر
    /// </summary>
    [Column("SARDAFTAR_ACCESS_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string SardaftarAccessId { get; set; }

    /// <summary>
    /// نام و نام خانوادگی سردفتر
    /// </summary>
    [Column("SARDAFTAR_NAME_FAMILY")]
    [StringLength(100)]
    [Unicode(false)]
    public string SardaftarNameFamily { get; set; }

    /// <summary>
    /// تاریخ تأیید سردفتر
    /// </summary>
    [Column("SARDAFTAR_CONFIRM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SardaftarConfirmDate { get; set; }

    /// <summary>
    /// زمان تأیید سردفتر
    /// </summary>
    [Column("SARDAFTAR_CONFIRM_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SardaftarConfirmTime { get; set; }

    /// <summary>
    /// گواهی امضای الكترونیك مورد استفاده برای امضای سند - توسط دفتریار
    /// </summary>
    [Column("DAFTARYAR_SIGN_CERTIFICATE_DN")]
    [StringLength(1000)]
    [Unicode(false)]
    public string DaftaryarSignCertificateDn { get; set; }

    /// <summary>
    /// شناسه دسترسی دفتریار
    /// </summary>
    [Column("DAFTARYAR_ACCESS_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string DaftaryarAccessId { get; set; }

    /// <summary>
    /// نام و نام خانوادگی دفتریار
    /// </summary>
    [Column("DAFTARYAR_NAME_FAMILY")]
    [StringLength(100)]
    [Unicode(false)]
    public string DaftaryarNameFamily { get; set; }

    /// <summary>
    /// تاریخ تأیید دفتریار
    /// </summary>
    [Column("DAFTARYAR_CONFIRM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string DaftaryarConfirmDate { get; set; }

    /// <summary>
    /// زمان تأیید دفتریار
    /// </summary>
    [Column("DAFTARYAR_CONFIRM_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string DaftaryarConfirmTime { get; set; }

    /// <summary>
    /// شناسه سابقه اثر انگشت سردفتر در زمان تایید نهایی سند
    /// </summary>
    [Column("LOG_EFPIN_CONFIRM_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LogEfpinConfirmId { get; set; }

    /// <summary>
    /// نام و نام خانوادگی ایجادكننده یا اصلاح كننده
    /// </summary>
    [Required]
    [Column("CREATOR_NAME_FAMILY")]
    [StringLength(100)]
    [Unicode(false)]
    public string CreatorNameFamily { get; set; }

    /// <summary>
    /// تاریخ ایجاد یا اصلاح
    /// </summary>
    [Required]
    [Column("CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CreateDate { get; set; }

    /// <summary>
    /// زمان ایجاد یا اصلاح
    /// </summary>
    [Required]
    [Column("CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string CreateTime { get; set; }

    /// <summary>
    /// نام و نام خانوادگی تأیید نهایی كننده سند
    /// </summary>
    [Column("CONFIRMER_NAME_FAMILY")]
    [StringLength(100)]
    [Unicode(false)]
    public string ConfirmerNameFamily { get; set; }

    /// <summary>
    /// تاریخ تأیید نهایی سند
    /// </summary>
    [Column("CONFIRM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ConfirmDate { get; set; }

    /// <summary>
    /// زمان تأیید نهایی سند
    /// </summary>
    [Column("CONFIRM_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ConfirmTime { get; set; }

    /// <summary>
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده
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
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentId { get; set; }

    /// <summary>
    /// امضای الکترونیک سردفتر
    /// </summary>
    [Column("SARDAFTAR_DOCUMENT_DIGITAL_SIGN", TypeName = "CLOB")]
    public string SardaftarDocumentDigitalSign { get; set; }

    /// <summary>
    /// امضای الکترونیک دفتریار
    /// </summary>
    [Column("DAFTARYAR_DOCUMENT_DIGITAL_SIGN", TypeName = "CLOB")]
    public string DaftaryarDocumentDigitalSign { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentInfoConfirm")]
    public virtual Document Document { get; set; }
}
