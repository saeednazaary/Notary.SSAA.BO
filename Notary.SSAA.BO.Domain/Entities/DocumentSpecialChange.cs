using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سابقه تغییرات خاص در اسناد
/// </summary>
[Table("DOCUMENT_SPECIAL_CHANGES")]
[Index("ChangeDate", Name = "IDX_DOCUMENT_SPECIAL_CHANGES###CHANGE_DATE")]
[Index("ChangeTime", Name = "IDX_DOCUMENT_SPECIAL_CHANGES###CHANGE_TIME")]
[Index("DocumentId", Name = "IDX_DOCUMENT_SPECIAL_CHANGES###DOCUMENT_ID")]
[Index("LetterDate", Name = "IDX_DOCUMENT_SPECIAL_CHANGES###LETTER_DATE")]
[Index("LetterNo", Name = "IDX_DOCUMENT_SPECIAL_CHANGES###LETTER_NO")]
[Index("Modifier", Name = "IDX_DOCUMENT_SPECIAL_CHANGES###MODIFIER")]
[Index("NewState", Name = "IDX_DOCUMENT_SPECIAL_CHANGES###NEW_STATE")]
[Index("OldState", Name = "IDX_DOCUMENT_SPECIAL_CHANGES###OLD_STATE")]
public partial class DocumentSpecialChange
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
    /// مقدار فیلد وضعیت قبلی
    /// </summary>
    [Required]
    [Column("OLD_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string OldState { get; set; }

    /// <summary>
    /// مقدار فیلد وضعیت جدید
    /// </summary>
    [Required]
    [Column("NEW_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string NewState { get; set; }

    /// <summary>
    /// شماره نامه تاییدیه امور اسناد
    /// </summary>
    [Required]
    [Column("LETTER_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string LetterNo { get; set; }

    /// <summary>
    /// تاریخ نامه تاییدیه امور اسناد
    /// </summary>
    [Required]
    [Column("LETTER_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LetterDate { get; set; }

    /// <summary>
    /// تصویر نامه تاییدیه امور اسناد
    /// </summary>
    [Required]
    [Column("LETTER_IMAGE", TypeName = "BLOB")]
    public byte[] LetterImage { get; set; }

    /// <summary>
    /// تاریخ انجام اصلاح
    /// </summary>
    [Required]
    [Column("CHANGE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ChangeDate { get; set; }

    /// <summary>
    /// زمان انجام اصلاح
    /// </summary>
    [Column("CHANGE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ChangeTime { get; set; }

    /// <summary>
    /// نام و نام خانوادگی تغییردهنده
    /// </summary>
    [Required]
    [Column("MODIFIER")]
    [StringLength(100)]
    [Unicode(false)]
    public string Modifier { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentSpecialChanges")]
    public virtual Document Document { get; set; }
}
