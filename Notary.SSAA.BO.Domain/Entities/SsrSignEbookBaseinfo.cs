using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اطلاعات پایه دفتر الكترونیك گواهی امضاء
/// </summary>
[Table("SSR_SIGN_EBOOK_BASEINFO")]
[Index("ScriptoriumId", Name = "UX_SSR_SIGN_EBOOK_BASEINFO_SID", IsUnique = true)]
public partial class SsrSignEbookBaseinfo
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
    /// شماره ترتیب آخرین گواهی امضاء ثبت شده در دفاتر كاغذی
    /// </summary>
    [Column("LAST_CLASSIFY_NO")]
    [Precision(6)]
    public int LastClassifyNo { get; set; }

    /// <summary>
    /// شماره جلد دفتر آخرین گواهی امضاء ثبت شده در دفاتر كاغذی
    /// </summary>
    [Required]
    [Column("LAST_REGISTER_VOLUME_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string LastRegisterVolumeNo { get; set; }

    /// <summary>
    /// شماره صفحه دفتر آخرین گواهی امضاء ثبت شده در دفاتر كاغذی
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
