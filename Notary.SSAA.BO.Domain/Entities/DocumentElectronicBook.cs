using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// دفتر الكترونیك اسناد رسمی
/// </summary>
[Table("DOCUMENT_ELECTRONIC_BOOK")]
[Index("ClassifyNo", Name = "IDX_DOCUMENT_ELECTRONIC_BOOK###CLASSIFY_NO")]
[Index("DocumentDate", Name = "IDX_DOCUMENT_ELECTRONIC_BOOK###DOCUMENT_DATE")]
[Index("DocumentTypeId", Name = "IDX_DOCUMENT_ELECTRONIC_BOOK###DOCUMENT_TYPE_ID")]
[Index("EnterBookDateTime", Name = "IDX_DOCUMENT_ELECTRONIC_BOOK###ENTER_BOOK_DATE_TIME")]
[Index("ExordiumConfirmDateTime", Name = "IDX_DOCUMENT_ELECTRONIC_BOOK###EXORDIUM_CONFIRM_DATE_TIME")]
[Index("Ilm", Name = "IDX_DOCUMENT_ELECTRONIC_BOOK###ILM")]
[Index("RecordDate", Name = "IDX_DOCUMENT_ELECTRONIC_BOOK###RECORD_DATE")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_ELECTRONIC_BOOK###SCRIPTORIUM_ID")]
[Index("SignDate", Name = "IDX_DOCUMENT_ELECTRONIC_BOOK###SIGN_DATE")]
[Index("ClassifyNoReserved", Name = "IX_SSR_DOCBOOK_CLSFYNO_RES")]
[Index("NationalNo", Name = "IX_SSR_DOC_EBOOK_NATIONAL_NO")]
public partial class DocumentElectronicBook
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
    /// شناسه یكتا سند
    /// </summary>
    [Required]
    [Column("NATIONAL_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    /// <summary>
    /// شماره ترتیب سند
    /// </summary>
    [Column("CLASSIFY_NO")]
    [Precision(6)]
    public int? ClassifyNo { get; set; }

    /// <summary>
    /// تاریخ سند
    /// </summary>
    [Required]
    [Column("DOCUMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string DocumentDate { get; set; }

    /// <summary>
    /// تاریخ امضاء
    /// </summary>
    [Column("SIGN_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SignDate { get; set; }

    /// <summary>
    /// شناسه نوع سند
    /// </summary>
    [Required]
    [Column("DOCUMENT_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string DocumentTypeId { get; set; }

    /// <summary>
    /// hash of fingerprints
    /// </summary>
    [Column("HASH_OF_FINGERPRINTS", TypeName = "CLOB")]
    public string HashOfFingerprints { get; set; }

    /// <summary>
    /// hash of document PDF
    /// </summary>
    [Column("HASH_OF_PDF", TypeName = "CLOB")]
    public string HashOfPdf { get; set; }

    /// <summary>
    /// امضای الكترونیك سردفتر
    /// </summary>
    [Column("EXORDIUM_DIGITAL_SIGN", TypeName = "CLOB")]
    public string ExordiumDigitalSign { get; set; }

    /// <summary>
    /// تاریخ و زمان ورود به دفتر
    /// </summary>
    [Required]
    [Column("ENTER_BOOK_DATE_TIME")]
    [StringLength(16)]
    [Unicode(false)]
    public string EnterBookDateTime { get; set; }

    /// <summary>
    /// تاریخ و زمان امضای سردفتر
    /// </summary>
    [Column("EXORDIUM_CONFIRM_DATE_TIME")]
    [StringLength(16)]
    [Unicode(false)]
    public string ExordiumConfirmDateTime { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Description { get; set; }

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
    /// كلید اصلی ركورد در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شماره ترتیب - رزرو شده
    /// </summary>
    [Column("CLASSIFY_NO_RESERVED")]
    [Precision(6)]
    public int? ClassifyNoReserved { get; set; }

    [ForeignKey("DocumentTypeId")]
    [InverseProperty("DocumentElectronicBooks")]
    public virtual DocumentType DocumentType { get; set; }
}
