using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// املاك ثبت شده در اسناد
/// </summary>
[Table("DOCUMENT_ESTATE")]
[Index("AttachmentType", Name = "IDX_DOCUMENT_ESTATE###ATTACHMENT_TYPE")]
[Index("BasicPlaque", Name = "IDX_DOCUMENT_ESTATE###BASIC_PLAQUE")]
[Index("DocumentEstateTypeId", Name = "IDX_DOCUMENT_ESTATE###DOCUMENT_ESTATE_TYPE_ID")]
[Index("EstateSectionId", Name = "IDX_DOCUMENT_ESTATE###ESTATE_SECTION_ID")]
[Index("EstateSubsectionId", Name = "IDX_DOCUMENT_ESTATE###ESTATE_SUBSECTION_ID")]
[Index("FieldOrGrandee", Name = "IDX_DOCUMENT_ESTATE###FIELD_OR_GRANDEE")]
[Index("GeoLocationId", Name = "IDX_DOCUMENT_ESTATE###GEO_LOCATION_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_ESTATE###ILM")]
[Index("ImmovaleType", Name = "IDX_DOCUMENT_ESTATE###IMMOVALE_TYPE")]
[Index("IsAttachment", Name = "IDX_DOCUMENT_ESTATE###IS_ATTACHMENT")]
[Index("IsEvacuated", Name = "IDX_DOCUMENT_ESTATE###IS_EVACUATED")]
[Index("IsFacilitationLaw", Name = "IDX_DOCUMENT_ESTATE###IS_FACILITATION_LAW")]
[Index("IsProportionateQuota", Name = "IDX_DOCUMENT_ESTATE###IS_PROPORTIONATE_QUOTA")]
[Index("IsRegistered", Name = "IDX_DOCUMENT_ESTATE###IS_REGISTERED")]
[Index("IsRemortage", Name = "IDX_DOCUMENT_ESTATE###IS_REMORTAGE")]
[Index("LocationType", Name = "IDX_DOCUMENT_ESTATE###LOCATION_TYPE")]
[Index("OwnershipType", Name = "IDX_DOCUMENT_ESTATE###OWNERSHIP_TYPE")]
[Index("RowNo", Name = "IDX_DOCUMENT_ESTATE###ROW_NO")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_ESTATE###SCRIPTORIUM_ID")]
[Index("SecondaryPlaque", Name = "IDX_DOCUMENT_ESTATE###SECONDARY_PLAQUE")]
[Index("UnitId", Name = "IDX_DOCUMENT_ESTATE###UNIT_ID")]
[Index("DocumentId", "RowNo", Name = "IDX_SSR_DC_ESTATE_DID_RWNO")]
[Index("LegacyId", Name = "UDX_DOCUMENT_ESTATE###LEGACY_ID", IsUnique = true)]
public partial class DocumentEstate
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
    /// ردیف
    /// </summary>
    [Column("ROW_NO")]
    [Precision(5)]
    public short RowNo { get; set; }

    /// <summary>
    /// شناسه انواع املاك مندرج در اسناد رسمی
    /// </summary>
    [Column("DOCUMENT_ESTATE_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string DocumentEstateTypeId { get; set; }

    /// <summary>
    /// آیا ملك ثبت شده است؟
    /// </summary>
    [Column("IS_REGISTERED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRegistered { get; set; }

    /// <summary>
    /// شناسه حوزه ثبتی
    /// </summary>
    [Column("UNIT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string UnitId { get; set; }

    /// <summary>
    /// شناسه بخش ثبتی
    /// </summary>
    [Column("ESTATE_SECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSectionId { get; set; }

    /// <summary>
    /// شناسه ناحیه ثبتی
    /// </summary>
    [Column("ESTATE_SUBSECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSubsectionId { get; set; }

    /// <summary>
    /// پلاك ثبتی اصلی
    /// </summary>
    [Column("BASIC_PLAQUE")]
    [StringLength(100)]
    [Unicode(false)]
    public string BasicPlaque { get; set; }

    /// <summary>
    /// پلاك ثبتی فرعی
    /// </summary>
    [Column("SECONDARY_PLAQUE")]
    [StringLength(100)]
    [Unicode(false)]
    public string SecondaryPlaque { get; set; }

    /// <summary>
    /// پلاك متنی
    /// </summary>
    [Column("PLAQUE_TEXT")]
    [StringLength(500)]
    [Unicode(false)]
    public string PlaqueText { get; set; }

    /// <summary>
    /// پلاك اصلی باقیمانده دارد؟
    /// </summary>
    [Column("BASIC_PLAQUE_HAS_REMAIN")]
    [StringLength(1)]
    [Unicode(false)]
    public string BasicPlaqueHasRemain { get; set; }

    /// <summary>
    /// پلاك فرعی باقیمانده دارد؟
    /// </summary>
    [Column("SECONDARY_PLAQUE_HAS_REMAIN")]
    [StringLength(1)]
    [Unicode(false)]
    public string SecondaryPlaqueHasRemain { get; set; }

    /// <summary>
    /// مفروز و مجزی از اصلی
    /// </summary>
    [Column("DIV_FROM_BASIC_PLAQUE")]
    [StringLength(100)]
    [Unicode(false)]
    public string DivFromBasicPlaque { get; set; }

    /// <summary>
    /// مفروز و مجزی از فرعی
    /// </summary>
    [Column("DIV_FROM_SECONDARY_PLAQUE")]
    [StringLength(100)]
    [Unicode(false)]
    public string DivFromSecondaryPlaque { get; set; }

    /// <summary>
    /// نوع ملك
    /// </summary>
    [Column("IMMOVALE_TYPE")]
    [StringLength(2)]
    [Unicode(false)]
    public string ImmovaleType { get; set; }

    /// <summary>
    /// نوع محل استقرار
    /// </summary>
    [Column("LOCATION_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string LocationType { get; set; }

    /// <summary>
    /// شماره قطعه
    /// </summary>
    [Column("PIECE")]
    [StringLength(50)]
    [Unicode(false)]
    public string Piece { get; set; }

    /// <summary>
    /// شماره بلوك
    /// </summary>
    [Column("BLOCK")]
    [StringLength(10)]
    [Unicode(false)]
    public string Block { get; set; }

    /// <summary>
    /// طبقه
    /// </summary>
    [Column("FLOOR")]
    [StringLength(20)]
    [Unicode(false)]
    public string Floor { get; set; }

    /// <summary>
    /// سمت
    /// </summary>
    [Column("DIRECTION")]
    [StringLength(500)]
    [Unicode(false)]
    public string Direction { get; set; }

    /// <summary>
    /// كد پستی
    /// </summary>
    [Column("POSTAL_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string PostalCode { get; set; }

    /// <summary>
    /// نشانی
    /// </summary>
    [Column("ADDRESS")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Address { get; set; }

    /// <summary>
    /// مساحت
    /// </summary>
    [Column("AREA", TypeName = "NUMBER(20,3)")]
    public decimal? Area { get; set; }

    /// <summary>
    /// توضیحات مساحت
    /// </summary>
    [Column("AREA_DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string AreaDescription { get; set; }

    /// <summary>
    /// مشاعات و مشتركات
    /// </summary>
    [Column("COMMONS", TypeName = "CLOB")]
    public string Commons { get; set; }

    /// <summary>
    /// حقوق ارتفاقی
    /// </summary>
    [Column("RIGHTS", TypeName = "CLOB")]
    public string Rights { get; set; }

    /// <summary>
    /// حدود
    /// </summary>
    [Column("LIMITS", TypeName = "CLOB")]
    public string Limits { get; set; }

    /// <summary>
    /// شناسه محل جغرافیایی ملك
    /// </summary>
    [Column("GEO_LOCATION_ID")]
    [Precision(6)]
    public int? GeoLocationId { get; set; }

    /// <summary>
    /// عنوان مورد ثبتی
    /// </summary>
    [Column("REGISTER_CASE_TITLE")]
    [StringLength(100)]
    [Unicode(false)]
    public string RegisterCaseTitle { get; set; }

    /// <summary>
    /// بنا به اظهار متعاملین ملك تخلیه شده است؟
    /// </summary>
    [Column("IS_EVACUATED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsEvacuated { get; set; }

    /// <summary>
    /// مشخصات قبوض تخلیه
    /// </summary>
    [Column("EVACUATION_PAPERS")]
    [StringLength(2000)]
    [Unicode(false)]
    public string EvacuationPapers { get; set; }

    /// <summary>
    /// تاریخ تخلیه
    /// </summary>
    [Column("EVACUATED_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string EvacuatedDate { get; set; }

    /// <summary>
    /// توضیحات در مورد وضعیت تخلیه
    /// </summary>
    [Column("EVACUATION_DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string EvacuationDescription { get; set; }

    /// <summary>
    /// سلسله انتقالات - ایادی
    /// </summary>
    [Column("OLD_SALE_DESCRIPTION", TypeName = "CLOB")]
    public string OldSaleDescription { get; set; }

    /// <summary>
    /// شناسه ركورد استعلام معادل در جدول سیستم ملك
    /// </summary>
    [Column("ESTATE_INQUIRY_ID", TypeName = "CLOB")]
    public string EstateInquiryId { get; set; }

    /// <summary>
    /// رهن مجدد با حفظ حقوق سند رهنی قبلی است؟
    /// </summary>
    [Column("IS_REMORTAGE")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRemortage { get; set; }

    /// <summary>
    /// آیا سهم بندی مورد معامله بین اصحاب سند بصورت حسب السهم خواهد بود؟
    /// </summary>
    [Column("IS_PROPORTIONATE_QUOTA")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsProportionateQuota { get; set; }

    /// <summary>
    /// آیا برای این ملك از قانون تسهیل استفاده شده است؟
    /// </summary>
    [Column("IS_FACILITATION_LAW")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFacilitationLaw { get; set; }

    /// <summary>
    /// مبلغ سند
    /// </summary>
    [Column("PRICE")]
    [Precision(15)]
    public long? Price { get; set; }

    /// <summary>
    /// ارزش منطقه ای ملك
    /// </summary>
    [Column("REGIONAL_PRICE")]
    [Precision(15)]
    public long? RegionalPrice { get; set; }

    /// <summary>
    /// عرصه یا اعیان
    /// </summary>
    [Column("FIELD_OR_GRANDEE")]
    [StringLength(1)]
    [Unicode(false)]
    public string FieldOrGrandee { get; set; }

    /// <summary>
    /// نوع مالكیت
    /// </summary>
    [Column("OWNERSHIP_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string OwnershipType { get; set; }

    /// <summary>
    /// شماره پایان كار شهرداری
    /// </summary>
    [Column("MUNICIPALITY_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string MunicipalityNo { get; set; }

    /// <summary>
    /// تاریخ پایان كار شهرداری
    /// </summary>
    [Column("MUNICIPALITY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string MunicipalityDate { get; set; }

    /// <summary>
    /// مرجع صدور پایان كار شهرداری
    /// </summary>
    [Column("MUNICIPALITY_ISSUER")]
    [StringLength(1000)]
    [Unicode(false)]
    public string MunicipalityIssuer { get; set; }

    /// <summary>
    /// وجه التزام
    /// </summary>
    [Column("COMMITMENT_PRICE")]
    [Precision(10)]
    public int? CommitmentPrice { get; set; }

    /// <summary>
    /// توضیحات صورتمجلس تفكیكی
    /// </summary>
    [Column("SEPARATION_DESCRIPTION", TypeName = "CLOB")]
    public string SeparationDescription { get; set; }

    /// <summary>
    /// نوع تفكیك
    /// </summary>
    [Column("SEPARATION_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SeparationType { get; set; }

    /// <summary>
    /// شماره صورتمجلس تفكیكی
    /// </summary>
    [Column("SEPARATION_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string SeparationNo { get; set; }

    /// <summary>
    /// تاریخ صورتمجلس تفكیكی
    /// </summary>
    [Column("SEPARATION_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SeparationDate { get; set; }

    /// <summary>
    /// مرجع صدور صورتمجلس تفكیكی
    /// </summary>
    [Column("SEPARATION_ISSUER")]
    [StringLength(1000)]
    [Unicode(false)]
    public string SeparationIssuer { get; set; }

    /// <summary>
    /// متن صورتمجلس تفكیكی
    /// </summary>
    [Column("SEPARATION_TEXT", TypeName = "CLOB")]
    public string SeparationText { get; set; }

    /// <summary>
    /// آیا انتقال منضم است؟
    /// </summary>
    [Column("IS_ATTACHMENT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsAttachment { get; set; }

    /// <summary>
    /// نوع منضم
    /// </summary>
    [Column("ATTACHMENT_TYPE")]
    [StringLength(2)]
    [Unicode(false)]
    public string AttachmentType { get; set; }

    /// <summary>
    /// توضیحات - در انتقال منضم
    /// </summary>
    [Column("ATTACHMENT_DESCRIPTION", TypeName = "CLOB")]
    public string AttachmentDescription { get; set; }

    /// <summary>
    /// مشخصات منضم - در انتقال منضم
    /// </summary>
    [Column("ATTACHMENT_SPECIFICATIONS", TypeName = "CLOB")]
    public string AttachmentSpecifications { get; set; }

    /// <summary>
    /// نوع منضم - سایر
    /// </summary>
    [Column("ATTACHMENT_TYPE_OTHERS")]
    [StringLength(100)]
    [Unicode(false)]
    public string AttachmentTypeOthers { get; set; }

    /// <summary>
    /// پلاك ثبتی اصلی - ملكی كه منضم به آن منتقل می شود
    /// </summary>
    [Column("RECEIVER_BASIC_PLAQUE")]
    [StringLength(100)]
    [Unicode(false)]
    public string ReceiverBasicPlaque { get; set; }

    /// <summary>
    /// پلاك ثبتی فرعی - ملكی كه منضم به آن منتقل می شود
    /// </summary>
    [Column("RECEIVER_SECONDARY_PLAQUE")]
    [StringLength(100)]
    [Unicode(false)]
    public string ReceiverSecondaryPlaque { get; set; }

    /// <summary>
    /// پلاك متنی - ملكی كه منضم به آن منتقل می شود
    /// </summary>
    [Column("RECEIVER_PLAQUE_TEXT")]
    [StringLength(500)]
    [Unicode(false)]
    public string ReceiverPlaqueText { get; set; }

    /// <summary>
    /// پلاك اصلی باقیمانده دارد؟ - ملكی كه منضم به آن منتقل می شود
    /// </summary>
    [Column("RECEIVER_BASIC_PLAQUE_HAS_REMAIN")]
    [StringLength(1)]
    [Unicode(false)]
    public string ReceiverBasicPlaqueHasRemain { get; set; }

    /// <summary>
    /// پلاك فرعی باقیمانده دارد؟ - ملكی كه منضم به آن منتقل می شود
    /// </summary>
    [Column("RECEIVER_SECONDARY_PLAQUE_HAS_REMAIN")]
    [StringLength(1)]
    [Unicode(false)]
    public string ReceiverSecondaryPlaqueHasRemain { get; set; }

    /// <summary>
    /// مفروز و مجزی از اصلی - ملكی كه منضم به آن منتقل می شود
    /// </summary>
    [Column("RECEIVER_DIV_FROM_BASIC_PLAQUE")]
    [StringLength(100)]
    [Unicode(false)]
    public string ReceiverDivFromBasicPlaque { get; set; }

    /// <summary>
    /// مفروز و مجزی از فرعی - ملكی كه منضم به آن منتقل می شود
    /// </summary>
    [Column("RECEIVER_DIV_FROM_SECONDARY_PLAQUE")]
    [StringLength(100)]
    [Unicode(false)]
    public string ReceiverDivFromSecondaryPlaque { get; set; }

    /// <summary>
    /// نوع استثناء - ثمنیه یا ربعیه
    /// </summary>
    [Column("GRANDEE_EXCEPTION_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string GrandeeExceptionType { get; set; }

    /// <summary>
    /// جزء استثناء
    /// </summary>
    [Column("GRANDEE_EXCEPTION_DETAIL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? GrandeeExceptionDetailQuota { get; set; }

    /// <summary>
    /// كل استثناء
    /// </summary>
    [Column("GRANDEE_EXCEPTION_TOTAL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? GrandeeExceptionTotalQuota { get; set; }

    /// <summary>
    /// عرصه یا اعیان در ثمنیه یا ربعیه
    /// </summary>
    [Column("SOMNYEH_ROBEYEH_FIELD_GRANDEE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SomnyehRobeyehFieldGrandee { get; set; }

    /// <summary>
    /// نوع عمل ثمنیه یا ربعیه
    /// </summary>
    [Column("SOMNYEH_ROBEYEH_ACTION_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SomnyehRobeyehActionType { get; set; }

    /// <summary>
    /// جزء سهم مورد مالكیت
    /// </summary>
    [Column("OWNERSHIP_DETAIL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? OwnershipDetailQuota { get; set; }

    /// <summary>
    /// كل سهم مورد مالكیت
    /// </summary>
    [Column("OWNERSHIP_TOTAL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? OwnershipTotalQuota { get; set; }

    /// <summary>
    /// جزء سهم مورد معامله
    /// </summary>
    [Column("SELL_DETAIL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? SellDetailQuota { get; set; }

    /// <summary>
    /// كل سهم مورد معامله
    /// </summary>
    [Column("SELL_TOTAL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? SellTotalQuota { get; set; }

    /// <summary>
    /// متن سهم
    /// </summary>
    [Column("QUOTA_TEXT")]
    [StringLength(2000)]
    [Unicode(false)]
    public string QuotaText { get; set; }

    /// <summary>
    /// ملاحظات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Description { get; set; }

    /// <summary>
    /// Information Lifecycle Management
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده سند
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

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
    [Column("OLD_ESTATE_SECTION_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldEstateSectionId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_ESTATE_SUBSECTION_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldEstateSubsectionId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_GEO_LOCATION_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldGeoLocationId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_ESTATE_INQUIRY_ID", TypeName = "CLOB")]
    public string OldEstateInquiryId { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentEstates")]
    public virtual Document Document { get; set; }

    [InverseProperty("DocumentEstate")]
    public virtual ICollection<DocumentEstateAttachment> DocumentEstateAttachments { get; set; } = new List<DocumentEstateAttachment>();

    [InverseProperty("DocumentEstate")]
    public virtual ICollection<DocumentEstateInquiry> DocumentEstateInquiries { get; set; } = new List<DocumentEstateInquiry>();

    [InverseProperty("DocumentEstate")]
    public virtual ICollection<DocumentEstateOwnershipDocument> DocumentEstateOwnershipDocuments { get; set; } = new List<DocumentEstateOwnershipDocument>();

    [InverseProperty("DocumentEstate")]
    public virtual ICollection<DocumentEstateQuotum> DocumentEstateQuota { get; set; } = new List<DocumentEstateQuotum>();

    [InverseProperty("DocumentEstate")]
    public virtual ICollection<DocumentEstateQuotaDetail> DocumentEstateQuotaDetails { get; set; } = new List<DocumentEstateQuotaDetail>();

    [InverseProperty("DocumentEstate")]
    public virtual ICollection<DocumentEstateSeparationPiece> DocumentEstateSeparationPieces { get; set; } = new List<DocumentEstateSeparationPiece>();

    [ForeignKey("DocumentEstateTypeId")]
    [InverseProperty("DocumentEstates")]
    public virtual DocumentEstateType DocumentEstateType { get; set; }

    [InverseProperty("DocumentEstate")]
    public virtual ICollection<DocumentInquiry> DocumentInquiries { get; set; } = new List<DocumentInquiry>();
}
