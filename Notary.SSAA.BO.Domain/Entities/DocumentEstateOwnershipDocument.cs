using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// مستندات مالكیت املاك مندرج در سند
/// </summary>
[Table("DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT")]
[Index("DocumentPersonId", Name = "IDX_DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT###DOCUMENT_PERSON_ID")]
[Index("EstateSeridaftarId", Name = "IDX_DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT###ESTATE_SERIDAFTAR_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT###ILM")]
[Index("OwnershipDocumentType", Name = "IDX_DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT###OWNERSHIP_DOCUMENT_TYPE")]
[Index("RowNo", Name = "IDX_DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT###ROW_NO")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT###SCRIPTORIUM_ID")]
[Index("DocumentEstateId", "RowNo", Name = "IDX_SSR_DC_EST_OWNDOC_DEID_RN")]
[Index("LegacyId", Name = "UDX_DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT###LEGACY_ID", IsUnique = true)]
public partial class DocumentEstateOwnershipDocument
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه املاك ثبت شده در سند
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
    /// شناسه اشخاص سند
    /// </summary>
    [Column("DOCUMENT_PERSON_ID")]
    public Guid? DocumentPersonId { get; set; }

    /// <summary>
    /// نوع مستند مالكیت
    /// </summary>
    [Required]
    [Column("OWNERSHIP_DOCUMENT_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string OwnershipDocumentType { get; set; }

    /// <summary>
    /// شناسه استعلام های املاك مربوط به سند
    /// </summary>
    [Column("ESTATE_INQUIRIES_ID", TypeName = "CLOB")]
    public string EstateInquiriesId { get; set; }

    /// <summary>
    /// نوع سند ملك
    /// </summary>
    [Column("ESTATE_DOCUMENT_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string EstateDocumentType { get; set; }

    /// <summary>
    /// شماره ثبت
    /// </summary>
    [Column("ESTATE_SABT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string EstateSabtNo { get; set; }

    /// <summary>
    /// شماره چاپی سند
    /// </summary>
    [Column("ESTATE_DOCUMENT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string EstateDocumentNo { get; set; }

    /// <summary>
    /// شماره دفتر املاك
    /// </summary>
    [Column("ESTATE_BOOK_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string EstateBookNo { get; set; }

    /// <summary>
    /// شماره صفحه دفتر املاك
    /// </summary>
    [Column("ESTATE_BOOK_PAGE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string EstateBookPageNo { get; set; }

    /// <summary>
    /// نوع دفتر املاك
    /// </summary>
    [Column("ESTATE_BOOK_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string EstateBookType { get; set; }

    /// <summary>
    /// شماره صفحه دفتر الكترونیك املاك
    /// </summary>
    [Column("ESTATE_ELECTRONIC_PAGE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string EstateElectronicPageNo { get; set; }

    /// <summary>
    /// شناسه سری دفتر املاك
    /// </summary>
    [Column("ESTATE_SERIDAFTAR_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSeridaftarId { get; set; }

    /// <summary>
    /// آیا سند المثنی است؟
    /// </summary>
    [Column("ESTATE_IS_REPLACEMENT_DOCUMENT")]
    [StringLength(1)]
    [Unicode(false)]
    public string EstateIsReplacementDocument { get; set; }

    /// <summary>
    /// شماره دفترخانه صادركننده سند بیع
    /// </summary>
    [Column("NOTARY_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string NotaryNo { get; set; }

    /// <summary>
    /// محل دفترخانه صادركننده سند بیع
    /// </summary>
    [Column("NOTARY_LOCATION")]
    [StringLength(100)]
    [Unicode(false)]
    public string NotaryLocation { get; set; }

    /// <summary>
    /// شماره سند بیع دفترخانه
    /// </summary>
    [Column("NOTARY_DOCUMENT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string NotaryDocumentNo { get; set; }

    /// <summary>
    /// تاریخ صدور سند بیع دفترخانه
    /// </summary>
    [Column("NOTARY_DOCUMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string NotaryDocumentDate { get; set; }

    /// <summary>
    /// شماره گزارش وضعیت ثبتی
    /// </summary>
    [Column("SABT_STATE_REPORT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string SabtStateReportNo { get; set; }

    /// <summary>
    /// تاریخ گزارش وضعیت ثبتی
    /// </summary>
    [Column("SABT_STATE_REPORT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SabtStateReportDate { get; set; }

    /// <summary>
    /// نام واحد ثبتی صادركننده گزارش وضعیت ثبتی
    /// </summary>
    [Column("SABT_STATE_REPORT_UNIT_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string SabtStateReportUnitName { get; set; }

    /// <summary>
    /// شرح مشخصات مورد ثبتی مطابق سند
    /// </summary>
    [Column("SPECIFICATIONS_TEXT", TypeName = "CLOB")]
    public string SpecificationsText { get; set; }

    /// <summary>
    /// متن رهن
    /// </summary>
    [Column("MORTGAGE_TEXT", TypeName = "CLOB")]
    public string MortgageText { get; set; }

    /// <summary>
    /// موارد لازم به ذكر در خلاصه معامله
    /// </summary>
    [Column("DEAL_SUMMARY_TEXT", TypeName = "CLOB")]
    public string DealSummaryText { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION", TypeName = "CLOB")]
    public string Description { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده
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

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_PERSON_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentPersonId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_ESTATE_INQUIRIES_ID", TypeName = "CLOB")]
    public string OldEstateInquiriesId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_ESTATE_SERIDAFTAR_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldEstateSeridaftarId { get; set; }

    [ForeignKey("DocumentEstateId")]
    [InverseProperty("DocumentEstateOwnershipDocuments")]
    public virtual DocumentEstate DocumentEstate { get; set; }

    [InverseProperty("DocumentEstateOwnershipDocument")]
    public virtual ICollection<DocumentEstateQuotaDetail> DocumentEstateQuotaDetails { get; set; } = new List<DocumentEstateQuotaDetail>();

    [InverseProperty("DocumentEstateOwnershipDocument")]
    public virtual ICollection<DocumentInquiry> DocumentInquiries { get; set; } = new List<DocumentInquiry>();

    [ForeignKey("DocumentPersonId")]
    [InverseProperty("DocumentEstateOwnershipDocuments")]
    public virtual DocumentPerson DocumentPerson { get; set; }

    [ForeignKey("EstateSeridaftarId")]
    [InverseProperty("DocumentEstateOwnershipDocuments")]
    public virtual EstateSeridaftar EstateSeridaftar { get; set; }
}
