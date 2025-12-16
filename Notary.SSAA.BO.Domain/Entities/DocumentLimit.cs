using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سابقه محدودیت ها در اسناد
/// </summary>
[Table("DOCUMENT_LIMITS")]
[Index("ChangeDate", Name = "IDX_DOCUMENT_LIMITS###CHANGE_DATE")]
[Index("ChangeTime", Name = "IDX_DOCUMENT_LIMITS###CHANGE_TIME")]
[Index("DocumentId", Name = "IDX_DOCUMENT_LIMITS###DOCUMENT_ID")]
[Index("FromDate", Name = "IDX_DOCUMENT_LIMITS###FROM_DATE")]
[Index("LetterDate", Name = "IDX_DOCUMENT_LIMITS###LETTER_DATE")]
[Index("LetterNo", Name = "IDX_DOCUMENT_LIMITS###LETTER_NO")]
[Index("Modifier", Name = "IDX_DOCUMENT_LIMITS###MODIFIER")]
[Index("ToDate", Name = "IDX_DOCUMENT_LIMITS###TO_DATE")]
public partial class DocumentLimit
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
    /// شماره نامه تاییدیه امور اسناد
    /// </summary>
    [Column("LETTER_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string LetterNo { get; set; }

    /// <summary>
    /// تاریخ نامه تاییدیه امور اسناد
    /// </summary>
    [Column("LETTER_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LetterDate { get; set; }

    /// <summary>
    /// تصویر نامه تاییدیه امور اسناد
    /// </summary>
    [Column("LETTER_IMAGE", TypeName = "BLOB")]
    public byte[] LetterImage { get; set; }

    /// <summary>
    /// تاریخ شروع محدودیت
    /// </summary>
    [Required]
    [Column("FROM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FromDate { get; set; }

    /// <summary>
    /// تاریخ پایان محدودیت
    /// </summary>
    [Column("TO_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ToDate { get; set; }

    /// <summary>
    /// توضیحات در مورد محدودیت اِعمال شده
    /// </summary>
    [Required]
    [Column("DESCRIPTION", TypeName = "CLOB")]
    public string Description { get; set; }

    /// <summary>
    /// تاریخ ثبت
    /// </summary>
    [Column("CHANGE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ChangeDate { get; set; }

    /// <summary>
    /// زمان ثبت
    /// </summary>
    [Column("CHANGE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ChangeTime { get; set; }

    /// <summary>
    /// نام و نام خانوادگی ثبت كننده
    /// </summary>
    [Column("MODIFIER")]
    [StringLength(100)]
    [Unicode(false)]
    public string Modifier { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentLimits")]
    public virtual Document Document { get; set; }
}
