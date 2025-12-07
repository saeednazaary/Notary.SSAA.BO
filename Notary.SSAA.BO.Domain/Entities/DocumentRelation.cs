using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اسناد وابسته مربوط به اسناد رسمی
/// </summary>
[Table("DOCUMENT_RELATION")]
[Index("DocumentId", Name = "IDX_DOCUMENT_RELATION###DOCUMENT_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_RELATION###ILM")]
[Index("IsRelatedDocAbroad", Name = "IDX_DOCUMENT_RELATION###IS_RELATED_DOC_ABROAD")]
[Index("RecordDate", Name = "IDX_DOCUMENT_RELATION###RECORD_DATE")]
[Index("RelatedDocumentDate", Name = "IDX_DOCUMENT_RELATION###RELATED_DOCUMENT_DATE")]
[Index("RelatedDocumentId", Name = "IDX_DOCUMENT_RELATION###RELATED_DOCUMENT_ID")]
[Index("RelatedDocumentIsInSsar", Name = "IDX_DOCUMENT_RELATION###RELATED_DOCUMENT_IS_IN_SSAR")]
[Index("RelatedDocumentNo", Name = "IDX_DOCUMENT_RELATION###RELATED_DOCUMENT_NO")]
[Index("RelatedDocumentSmsId", Name = "IDX_DOCUMENT_RELATION###RELATED_DOCUMENT_SMS_ID")]
[Index("RelatedDocumentTypeId", Name = "IDX_DOCUMENT_RELATION###RELATED_DOCUMENT_TYPE_ID")]
[Index("RelatedDocAbroadCountryId", Name = "IDX_DOCUMENT_RELATION###RELATED_DOC_ABROAD_COUNTRY_ID")]
[Index("RelatedRegCaseId", Name = "IDX_DOCUMENT_RELATION###RELATED_REG_CASE_ID")]
[Index("RelatedScriptoriumId", Name = "IDX_DOCUMENT_RELATION###RELATED_SCRIPTORIUM_ID")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_RELATION###SCRIPTORIUM_ID")]
public partial class DocumentRelation
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه سند اصلی
    /// </summary>
    [Column("DOCUMENT_ID")]
    public Guid DocumentId { get; set; }

    /// <summary>
    /// آیا سند وابسته در مراجع قانونی خارج از كشور ثبت شده است؟
    /// </summary>
    [Column("IS_RELATED_DOC_ABROAD")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRelatedDocAbroad { get; set; }

    /// <summary>
    /// شناسه كشوری كه سند وابسته در آن تنظیم شده است؟
    /// </summary>
    [Column("RELATED_DOC_ABROAD_COUNTRY_ID")]
    [Precision(6)]
    public int? RelatedDocAbroadCountryId { get; set; }

    /// <summary>
    /// آیا سند وابسته در سیستم ثبت الكترونیك اسناد ثبت شده است؟
    /// </summary>
    [Required]
    [Column("RELATED_DOCUMENT_IS_IN_SSAR")]
    [StringLength(1)]
    [Unicode(false)]
    public string RelatedDocumentIsInSsar { get; set; }

    /// <summary>
    /// شماره سند وابسته
    /// </summary>
    [Required]
    [Column("RELATED_DOCUMENT_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string RelatedDocumentNo { get; set; }

    /// <summary>
    /// تاریخ سند وابسته
    /// </summary>
    [Required]
    [Column("RELATED_DOCUMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string RelatedDocumentDate { get; set; }

    /// <summary>
    /// شناسه نوع سند وابسته
    /// </summary>
    [Required]
    [Column("RELATED_DOCUMENT_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string RelatedDocumentTypeId { get; set; }

    /// <summary>
    /// شماره و محل دفترخانه صادركننده سند وابسته
    /// </summary>
    [Column("RELATED_DOCUMENT_SCRIPTORIUM")]
    [StringLength(200)]
    [Unicode(false)]
    public string RelatedDocumentScriptorium { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده سند وابسته
    /// </summary>
    [Column("RELATED_SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string RelatedScriptoriumId { get; set; }

    /// <summary>
    /// رمز تصدیق سند وابسته
    /// </summary>
    [Column("RELATED_DOCUMENT_SECRET_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string RelatedDocumentSecretCode { get; set; }

    /// <summary>
    /// شناسه سند وابسته
    /// </summary>
    [Column("RELATED_DOCUMENT_ID")]
    public Guid? RelatedDocumentId { get; set; }

    /// <summary>
    /// شناسه مورد ثبتی وابسته
    /// </summary>
    [Column("RELATED_REG_CASE_ID")]
    public Guid? RelatedRegCaseId { get; set; }

    /// <summary>
    /// شناسه ركورد پیامك معادل سند وابسته
    /// </summary>
    [Column("RELATED_DOCUMENT_SMS_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string RelatedDocumentSmsId { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده سند اصلی
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// تاریخ پرونده به میلادی
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
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_RELATED_DOC_ABROAD_COUNTRY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldRelatedDocAbroadCountryId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_RELATED_SCRIPTORIUM_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldRelatedScriptoriumId { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentRelationDocuments")]
    public virtual Document Document { get; set; }

    [ForeignKey("RelatedDocumentId")]
    [InverseProperty("DocumentRelationRelatedDocuments")]
    public virtual Document RelatedDocument { get; set; }

    [ForeignKey("RelatedDocumentTypeId")]
    [InverseProperty("DocumentRelations")]
    public virtual DocumentType RelatedDocumentType { get; set; }
}
