using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع اسناد و خدمات ثبتی
/// </summary>
[Table("DOCUMENT_TYPE")]
[Index("AssetIsRequired", Name = "IDX_DOCUMENT_TYPE###ASSET_IS_REQUIRED")]
[Index("AssetTypeIsRequired", Name = "IDX_DOCUMENT_TYPE###ASSET_TYPE_IS_REQUIRED")]
[Index("DocumentTextWritingAllowed", Name = "IDX_DOCUMENT_TYPE###DOCUMENT_TEXT_WRITING_ALLOWED")]
[Index("DocumentTypeGroup1Id", Name = "IDX_DOCUMENT_TYPE###DOCUMENT_TYPE_GROUP1_ID")]
[Index("DocumentTypeGroup2Id", Name = "IDX_DOCUMENT_TYPE###DOCUMENT_TYPE_GROUP2_ID")]
[Index("EstateInquiryIsRequired", Name = "IDX_DOCUMENT_TYPE###ESTATE_INQUIRY_IS_REQUIRED")]
[Index("HasAsset", Name = "IDX_DOCUMENT_TYPE###HAS_ASSET")]
[Index("HasAssetType", Name = "IDX_DOCUMENT_TYPE###HAS_ASSET_TYPE")]
[Index("HasCount", Name = "IDX_DOCUMENT_TYPE###HAS_COUNT")]
[Index("HasEstateAttachments", Name = "IDX_DOCUMENT_TYPE###HAS_ESTATE_ATTACHMENTS")]
[Index("HasEstateInquiry", Name = "IDX_DOCUMENT_TYPE###HAS_ESTATE_INQUIRY")]
[Index("HasNonregisteredEstate", Name = "IDX_DOCUMENT_TYPE###HAS_NONREGISTERED_ESTATE")]
[Index("HasRelatedDocument", Name = "IDX_DOCUMENT_TYPE###HAS_RELATED_DOCUMENT")]
[Index("HasSubject", Name = "IDX_DOCUMENT_TYPE###HAS_SUBJECT")]
[Index("IsSupportive", Name = "IDX_DOCUMENT_TYPE###IS_SUPPORTIVE")]
[Index("RowNo", Name = "IDX_DOCUMENT_TYPE###ROW_NO")]
[Index("State", Name = "IDX_DOCUMENT_TYPE###STATE")]
[Index("SubjectIsRequired", Name = "IDX_DOCUMENT_TYPE###SUBJECT_IS_REQUIRED")]
[Index("WealthType", Name = "IDX_DOCUMENT_TYPE###WEALTH_TYPE")]
[Index("Code", Name = "UDX_DOCUMENT_TYPE###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_DOCUMENT_TYPE###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_DOCUMENT_TYPE###TITLE", IsUnique = true)]
public partial class DocumentType
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// شناسه سطح اول گروه‌بندی اسناد و خدمات ثبتی
    /// </summary>
    [Required]
    [Column("DOCUMENT_TYPE_GROUP1_ID")]
    [StringLength(1)]
    [Unicode(false)]
    public string DocumentTypeGroup1Id { get; set; }

    /// <summary>
    /// شناسه سطح دوم  گروه‌بندی اسناد و خدمات ثبتی
    /// </summary>
    [Required]
    [Column("DOCUMENT_TYPE_GROUP2_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string DocumentTypeGroup2Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(4)]
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
    /// ترتیب
    /// </summary>
    [Required]
    [Column("ROW_NO")]
    [StringLength(3)]
    [Unicode(false)]
    public string RowNo { get; set; }

    /// <summary>
    /// آیا مخصوص خدمات ثبتی است؟
    /// </summary>
    [Required]
    [Column("IS_SUPPORTIVE")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSupportive { get; set; }

    /// <summary>
    /// آیا برای این نوع سند تعیین اموال مورد ثبت تعریف شده است؟
    /// </summary>
    [Required]
    [Column("HAS_ASSET")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasAsset { get; set; }

    /// <summary>
    /// آیا برای این نوع سند تعیین اموال مورد ثبت اجباری است؟
    /// </summary>
    [Required]
    [Column("ASSET_IS_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string AssetIsRequired { get; set; }

    /// <summary>
    /// نوع اموال مندرج در سند
    /// </summary>
    [Column("WEALTH_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string WealthType { get; set; }

    /// <summary>
    /// عنوان كلی مورد ثبتی
    /// </summary>
    [Column("CASE_TITLE")]
    [StringLength(100)]
    [Unicode(false)]
    public string CaseTitle { get; set; }

    /// <summary>
    /// آیا برای این نوع سند نوشتن كل متن سند توسط دفترخانه مجاز است؟
    /// </summary>
    [Required]
    [Column("DOCUMENT_TEXT_WRITING_ALLOWED")]
    [StringLength(1)]
    [Unicode(false)]
    public string DocumentTextWritingAllowed { get; set; }

    /// <summary>
    /// آیا برای این نوع سند، سند وابسته باید تعیین شود؟
    /// </summary>
    [Required]
    [Column("HAS_RELATED_DOCUMENT")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasRelatedDocument { get; set; }

    /// <summary>
    /// آیا تعداد مطرح است؟
    /// </summary>
    [Required]
    [Column("HAS_COUNT")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasCount { get; set; }

    /// <summary>
    /// آیا موضوع دارد؟
    /// </summary>
    [Required]
    [Column("HAS_SUBJECT")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasSubject { get; set; }

    /// <summary>
    /// آیا موضوع اجباری است؟
    /// </summary>
    [Required]
    [Column("SUBJECT_IS_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string SubjectIsRequired { get; set; }

    /// <summary>
    /// آیا استعلام ملك دارد؟
    /// </summary>
    [Required]
    [Column("HAS_ESTATE_INQUIRY")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasEstateInquiry { get; set; }

    /// <summary>
    /// آیا استعلام ملك اجباری است؟
    /// </summary>
    [Required]
    [Column("ESTATE_INQUIRY_IS_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string EstateInquiryIsRequired { get; set; }

    /// <summary>
    /// آیا شامل املاك جاری هم می شود؟
    /// </summary>
    [Required]
    [Column("HAS_NONREGISTERED_ESTATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasNonregisteredEstate { get; set; }

    /// <summary>
    /// آیا گزینه انتقال منضم دارد؟
    /// </summary>
    [Required]
    [Column("HAS_ESTATE_ATTACHMENTS")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasEstateAttachments { get; set; }

    /// <summary>
    /// آیا نوع اموال منقول دارد؟
    /// </summary>
    [Required]
    [Column("HAS_ASSET_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasAssetType { get; set; }

    /// <summary>
    /// آیا نوع اموال منقول اجباری است؟
    /// </summary>
    [Required]
    [Column("ASSET_TYPE_IS_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string AssetTypeIsRequired { get; set; }

    /// <summary>
    /// عنوان كلی اشخاص سند
    /// </summary>
    [Required]
    [Column("GENERAL_PERSON_POST_TITLE")]
    [StringLength(100)]
    [Unicode(false)]
    public string GeneralPersonPostTitle { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("DocumentType")]
    public virtual ICollection<Document> DocumentDocumentTypes { get; set; } = new List<Document>();

    [InverseProperty("DocumentType")]
    public virtual ICollection<DocumentElectronicBook> DocumentElectronicBooks { get; set; } = new List<DocumentElectronicBook>();

    [InverseProperty("DocumentType")]
    public virtual ICollection<DocumentPersonType> DocumentPersonTypes { get; set; } = new List<DocumentPersonType>();

    [InverseProperty("RelatedDocumentType")]
    public virtual ICollection<Document> DocumentRelatedDocumentTypes { get; set; } = new List<Document>();

    [InverseProperty("RelatedDocumentType")]
    public virtual ICollection<DocumentRelation> DocumentRelations { get; set; } = new List<DocumentRelation>();

    [InverseProperty("DocumentType")]
    public virtual ICollection<DocumentTemplate> DocumentTemplates { get; set; } = new List<DocumentTemplate>();

    [ForeignKey("DocumentTypeGroup1Id")]
    [InverseProperty("DocumentTypes")]
    public virtual DocumentTypeGroup1 DocumentTypeGroup1 { get; set; }

    [ForeignKey("DocumentTypeGroup2Id")]
    [InverseProperty("DocumentTypes")]
    public virtual DocumentTypeGroup2 DocumentTypeGroup2 { get; set; }

    [InverseProperty("DocumentType")]
    public virtual ICollection<DocumentTypeSubject> DocumentTypeSubjects { get; set; } = new List<DocumentTypeSubject>();

    [InverseProperty("TransferDocumentType")]
    public virtual ICollection<EstateDocumentRequest> EstateDocumentRequests { get; set; } = new List<EstateDocumentRequest>();

    [InverseProperty("DocumentType")]
    public virtual ICollection<SsrConfigConditionDoctype> SsrConfigConditionDoctypes { get; set; } = new List<SsrConfigConditionDoctype>();
}
