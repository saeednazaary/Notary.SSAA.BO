using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// كلیشه اسناد
/// </summary>
[Table("DOCUMENT_TEMPLATE")]
[Index("Code", Name = "IDX_DOCUMENT_TEMPLATE###CODE")]
[Index("CreateDate", Name = "IDX_DOCUMENT_TEMPLATE###CREATE_DATE")]
[Index("DocumentTypeId", Name = "IDX_DOCUMENT_TEMPLATE###DOCUMENT_TYPE_ID")]
[Index("ModifyDateTime", Name = "IDX_DOCUMENT_TEMPLATE###MODIFY_DATE_TIME")]
[Index("RecordDate", Name = "IDX_DOCUMENT_TEMPLATE###RECORD_DATE")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_TEMPLATE###SCRIPTORIUM_ID")]
[Index("State", Name = "IDX_DOCUMENT_TEMPLATE###STATE")]
[Index("Title", Name = "IDX_DOCUMENT_TEMPLATE###TITLE")]
public partial class DocumentTemplate
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
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شناسه نوع سند
    /// </summary>
    [Required]
    [Column("DOCUMENT_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string DocumentTypeId { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(100)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// متن
    /// </summary>
    [Column("TEXT", TypeName = "CLOB")]
    public string Text { get; set; }

    /// <summary>
    /// تاریخ ایجاد
    /// </summary>
    [Required]
    [Column("CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CreateDate { get; set; }

    /// <summary>
    /// تاریخ و زمان آخرین ویرایش
    /// </summary>
    [Required]
    [Column("MODIFY_DATE_TIME")]
    [StringLength(20)]
    [Unicode(false)]
    public string ModifyDateTime { get; set; }

    /// <summary>
    /// مشخصات آخرین ویرایش كننده
    /// </summary>
    [Required]
    [Column("MODIFIER")]
    [StringLength(200)]
    [Unicode(false)]
    public string Modifier { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// تاریخ ایجاد كلیشه به میلادی
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

    /// <summary>
    /// زمان ایجاد
    /// </summary>
    [Column("CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string CreateTime { get; set; }

    [ForeignKey("DocumentTypeId")]
    [InverseProperty("DocumentTemplates")]
    public virtual DocumentType DocumentType { get; set; }
}
