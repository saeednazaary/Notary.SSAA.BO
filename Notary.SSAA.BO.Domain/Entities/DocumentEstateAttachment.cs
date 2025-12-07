using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// منضمات املاك ثبت شده در اسناد
/// </summary>
[Table("DOCUMENT_ESTATE_ATTACHMENT")]
[Index("AttachmentType", Name = "IDX_DOCUMENT_ESTATE_ATTACHMENT###ATTACHMENT_TYPE")]
[Index("Ilm", Name = "IDX_DOCUMENT_ESTATE_ATTACHMENT###ILM")]
[Index("RowNo", Name = "IDX_DOCUMENT_ESTATE_ATTACHMENT###ROW_NO")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_ESTATE_ATTACHMENT###SCRIPTORIUM_ID")]
[Index("DocumentEstateId", "RowNo", Name = "IDX_SSR_DC_ESTATE_ATCH_DEID_RN")]
[Index("LegacyId", Name = "UDX_DOCUMENT_ESTATE_ATTACHMENT###LEGACY_ID", IsUnique = true)]
public partial class DocumentEstateAttachment
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه املاك ثبت شده در اسناد
    /// </summary>
    [Column("DOCUMENT_ESTATE_ID")]
    public Guid DocumentEstateId { get; set; }

    /// <summary>
    /// ردیف
    /// </summary>
    [Column("ROW_NO")]
    [Precision(5)]
    public short RowNo { get; set; }

    /// <summary>
    /// نوع منضم
    /// </summary>
    [Required]
    [Column("ATTACHMENT_TYPE")]
    [StringLength(2)]
    [Unicode(false)]
    public string AttachmentType { get; set; }

    /// <summary>
    /// شماره
    /// </summary>
    [Column("NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string No { get; set; }

    /// <summary>
    /// حدود محل
    /// </summary>
    [Column("LOCATION")]
    [StringLength(100)]
    [Unicode(false)]
    public string Location { get; set; }

    /// <summary>
    /// مساحت
    /// </summary>
    [Column("AREA", TypeName = "NUMBER(7,3)")]
    public decimal? Area { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION", TypeName = "CLOB")]
    public string Description { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده سند
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

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
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ESTATE_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentEstateId { get; set; }

    [ForeignKey("DocumentEstateId")]
    [InverseProperty("DocumentEstateAttachments")]
    public virtual DocumentEstate DocumentEstate { get; set; }
}
