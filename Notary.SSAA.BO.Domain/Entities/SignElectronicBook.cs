using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// دفتر الکترونيک گواهي امضا
/// </summary>
[Table("SIGN_ELECTRONIC_BOOK")]
[Index("ClassifyNo", Name = "IDX_SIGN_ELECTRONIC_BOOK###CLASSIFY_NO")]
[Index("ConfirmDate", Name = "IDX_SIGN_ELECTRONIC_BOOK###CONFIRM_DATE")]
[Index("ConfirmTime", Name = "IDX_SIGN_ELECTRONIC_BOOK###CONFIRM_TIME")]
[Index("Ilm", Name = "IDX_SIGN_ELECTRONIC_BOOK###ILM")]
[Index("RecordDate", Name = "IDX_SIGN_ELECTRONIC_BOOK###RECORD_DATE")]
[Index("SignDate", Name = "IDX_SIGN_ELECTRONIC_BOOK###SIGN_DATE")]
[Index("SignRequestId", Name = "IDX_SIGN_ELECTRONIC_BOOK###SIGN_REQUEST_ID")]
[Index("SignRequestNationalNo", Name = "IDX_SIGN_ELECTRONIC_BOOK###SIGN_REQUEST_NATIONAL_NO")]
[Index("ClassifyNoReserved", Name = "IX_SSR_SIGN_EBOOK_CLSFYNO_RES")]
[Index("LegacyId", Name = "UDX_SIGN_ELECTRONIC_BOOK###LEGACY_ID", IsUnique = true)]
[Index("ScriptoriumId", "ClassifyNo", Name = "UDX_SIGN_ELECTRONIC_BOOK###SCRIPTORIUM_ID###CLASSIFY_NO", IsUnique = true)]
[Index("SignRequestPersonId", Name = "UDX_SIGN_ELECTRONIC_BOOK###SIGN_REQUEST_PERSON_ID", IsUnique = true)]
public partial class SignElectronicBook
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شماره ترتیب ثبت در دفتر
    /// </summary>
    [Column("CLASSIFY_NO")]
    [Precision(8)]
    public int? ClassifyNo { get; set; }

    /// <summary>
    /// شناسه گواهی امضاء
    /// </summary>
    [Column("SIGN_REQUEST_ID")]
    public Guid SignRequestId { get; set; }

    /// <summary>
    /// شناسه یكتا گواهی امضاء
    /// </summary>
    [Required]
    [Column("SIGN_REQUEST_NATIONAL_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string SignRequestNationalNo { get; set; }

    /// <summary>
    /// شناسه شخص گواهی امضاء
    /// </summary>
    [Column("SIGN_REQUEST_PERSON_ID")]
    public Guid SignRequestPersonId { get; set; }

    /// <summary>
    /// تاریخ گواهی امضاء
    /// </summary>
    [Required]
    [Column("SIGN_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SignDate { get; set; }

    /// <summary>
    /// هش اثرانگشت شخص در گواهی امضاء
    /// </summary>
    [Column("HASH_OF_FINGERPRINT", TypeName = "CLOB")]
    public string HashOfFingerprint { get; set; }

    /// <summary>
    /// هش فایل چاپ نهایی گواهی امضاء
    /// </summary>
    [Column("HASH_OF_FILE", TypeName = "CLOB")]
    public string HashOfFile { get; set; }

    /// <summary>
    /// امضای الكترونیك
    /// </summary>
    [Required]
    [Column("DIGITAL_SIGN", TypeName = "CLOB")]
    public string DigitalSign { get; set; }

    /// <summary>
    /// تاریخ تأیید
    /// </summary>
    [Required]
    [Column("CONFIRM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ConfirmDate { get; set; }

    /// <summary>
    /// زمان تأیید
    /// </summary>
    [Required]
    [Column("CONFIRM_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ConfirmTime { get; set; }

    /// <summary>
    /// تاریخ و زمان ركورد به میلادی
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
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// اطلاعات شناسه گواهی امضای الكترونیك
    /// </summary>
    [Column("SIGN_CERTIFICATE_DN", TypeName = "CLOB")]
    public string SignCertificateDn { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_SIGN_REQUEST_PERSON_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldSignRequestPersonId { get; set; }

    /// <summary>
    /// شماره ترتيب - رزرو شده
    /// </summary>
    [Column("CLASSIFY_NO_RESERVED")]
    [Precision(8)]
    public int? ClassifyNoReserved { get; set; }

    [ForeignKey("SignRequestId")]
    [InverseProperty("SignElectronicBooks")]
    public virtual SignRequest SignRequest { get; set; }

    [ForeignKey("SignRequestPersonId")]
    [InverseProperty("SignElectronicBook")]
    public virtual SignRequestPerson SignRequestPerson { get; set; }
}
