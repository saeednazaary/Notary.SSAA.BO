using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اطلاعات پایه دفتر الكترونیك اسناد رسمی
/// </summary>
[Table("DOCUMENT_ELECTRONIC_BOOK_BASEINFO")]
[Index("ScriptoriumId", Name = "UDX_DOCUMENT_ELECTRONIC_BOOK_BASEINFO###SCRIPTORIUM_ID", IsUnique = true)]
public partial class DocumentElectronicBookBaseinfo
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
    /// شماره ترتیب آخرین سند ثبت شده در دفاتر كاغذی
    /// </summary>
    [Column("LAST_CLASSIFY_NO")]
    [Precision(6)]
    public int LastClassifyNo { get; set; }

    /// <summary>
    /// شماره جلد دفتر آخرین سند ثبت شده در دفاتر كاغذی
    /// </summary>
    [Required]
    [Column("LAST_REGISTER_VOLUME_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string LastRegisterVolumeNo { get; set; }

    /// <summary>
    /// شماره صفحه دفتر آخرین سند ثبت شده در دفاتر كاغذی
    /// </summary>
    [Column("LAST_REGISTER_PAPER_NO")]
    [Precision(6)]
    public int LastRegisterPaperNo { get; set; }

    /// <summary>
    /// تعداد كل دفاتر كاغذی
    /// </summary>
    [Column("NUMBER_OF_BOOKS")]
    [Precision(6)]
    public int NumberOfBooks { get; set; }

    /// <summary>
    /// تعداد دفاتر رهنی
    /// </summary>
    [Column("NUMBER_OF_BOOKS_RAHNI")]
    [Precision(6)]
    public int? NumberOfBooksRahni { get; set; }

    /// <summary>
    /// تعداد دفاتر اتومبیل
    /// </summary>
    [Column("NUMBER_OF_BOOKS_VEHICLE")]
    [Precision(6)]
    public int? NumberOfBooksVehicle { get; set; }

    /// <summary>
    /// تعداد دفاتر اوقاف
    /// </summary>
    [Column("NUMBER_OF_BOOKS_OGHAF")]
    [Precision(6)]
    public int? NumberOfBooksOghaf { get; set; }

    /// <summary>
    /// تعداد دفاتر جاری (سردفتر)
    /// </summary>
    [Column("NUMBER_OF_BOOKS_JARI")]
    [Precision(6)]
    public int? NumberOfBooksJari { get; set; }

    /// <summary>
    /// تعداد دفاتر اصلاحات ارضی
    /// </summary>
    [Column("NUMBER_OF_BOOKS_ARZI")]
    [Precision(6)]
    public int? NumberOfBooksArzi { get; set; }

    /// <summary>
    /// تعداد دفاتر سایر
    /// </summary>
    [Column("NUMBER_OF_BOOKS_OTHERS")]
    [Precision(6)]
    public int? NumberOfBooksOthers { get; set; }

    /// <summary>
    /// تعداد دفاتر نماینده
    /// </summary>
    [Column("NUMBER_OF_BOOKS_AGENT")]
    [Precision(6)]
    public int? NumberOfBooksAgent { get; set; }

    /// <summary>
    /// تاریخ امضای سردفتر
    /// </summary>
    [Required]
    [Column("EXORDIUM_CONFIRM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ExordiumConfirmDate { get; set; }

    /// <summary>
    /// زمان امضای سردفتر
    /// </summary>
    [Required]
    [Column("EXORDIUM_CONFIRM_TIME")]
    [StringLength(5)]
    [Unicode(false)]
    public string ExordiumConfirmTime { get; set; }

    /// <summary>
    /// امضای الكترونیك سردفتر
    /// </summary>
    [Required]
    [Column("EXORDIUM_DIGITAL_SIGN", TypeName = "CLOB")]
    public string ExordiumDigitalSign { get; set; }
}
