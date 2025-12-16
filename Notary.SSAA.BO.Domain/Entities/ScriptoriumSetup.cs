using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اطلاعات اولیه ثبت اسناد در هر دفترخانه
/// </summary>
[Table("SCRIPTORIUM_SETUP")]
[Index("ScriptoriumId", Name = "UDX_SCRIPTORIUM_SETUP###SCRIPTORIUM_ID", IsUnique = true)]
public partial class ScriptoriumSetup
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// آخرین شماره ترتیب سند در آغاز كار با سامانه
    /// </summary>
    [Column("LAST_DOCUMENT_CLASSIFY_NO")]
    [Precision(10)]
    public int? LastDocumentClassifyNo { get; set; }

    /// <summary>
    /// آخرین شماره استعلام ملك در آغاز كار با سامانه
    /// </summary>
    [Column("LAST_INQUIRY_NO")]
    [Precision(10)]
    public int? LastInquiryNo { get; set; }

    /// <summary>
    /// آخرین شماره ترتیب گواهی امضاء در آغاز كار با سامانه
    /// </summary>
    [Column("LAST_SIGN_NO")]
    [Precision(10)]
    public int? LastSignNo { get; set; }

    /// <summary>
    /// نوع پورت پوز
    /// </summary>
    [Column("POS_PORT_TYPE")]
    [StringLength(10)]
    [Unicode(false)]
    public string PosPortType { get; set; }

    /// <summary>
    /// شماره پوز
    /// </summary>
    [Column("POS_BASE_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string PosBaseNo { get; set; }

    /// <summary>
    /// پیش فرض اندازه فونت در چاپ اسناد
    /// </summary>
    [Column("DOCUMENT_TEXT_FONT_SIZE", TypeName = "NUMBER(5,2)")]
    public decimal? DocumentTextFontSize { get; set; }
}
