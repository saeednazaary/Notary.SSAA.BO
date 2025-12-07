using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اشخاص اسناد
/// </summary>
[Table("DOCUMENT_PERSON")]
[Index("BirthDate", Name = "IDX_DOCUMENT_PERSON###BIRTH_DATE")]
[Index("BirthYear", Name = "IDX_DOCUMENT_PERSON###BIRTH_YEAR")]
[Index("CompanyTypeId", Name = "IDX_DOCUMENT_PERSON###COMPANY_TYPE_ID")]
[Index("DocumentPersonTypeId", Name = "IDX_DOCUMENT_PERSON###DOCUMENT_PERSON_TYPE_ID")]
[Index("Family", Name = "IDX_DOCUMENT_PERSON###FAMILY")]
[Index("GrowthSenderId", Name = "IDX_DOCUMENT_PERSON###GROWTH_GROWTH_SENDER_ID")]
[Index("GrowthIssuerId", Name = "IDX_DOCUMENT_PERSON###GROWTH_ISSUER_ID")]
[Index("HasGrowthJudgment", Name = "IDX_DOCUMENT_PERSON###HAS_GROWTH_JUDGMENT")]
[Index("HasSmartCard", Name = "IDX_DOCUMENT_PERSON###HAS_SMART_CARD")]
[Index("IdentityIssueGeoLocationId", Name = "IDX_DOCUMENT_PERSON###IDENTITY_ISSUE_GEO_LOCATION_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_PERSON###ILM")]
[Index("IsAlive", Name = "IDX_DOCUMENT_PERSON###IS_ALIVE")]
[Index("IsFingerprintGotten", Name = "IDX_DOCUMENT_PERSON###IS_FINGERPRINT_GOTTEN")]
[Index("IsIranian", Name = "IDX_DOCUMENT_PERSON###IS_IRANIAN")]
[Index("IsMartyrApplicant", Name = "IDX_DOCUMENT_PERSON###IS_MARTYR_APPLICANT")]
[Index("IsMartyrIncluded", Name = "IDX_DOCUMENT_PERSON###IS_MARTYR_INCLUDED")]
[Index("IsOriginal", Name = "IDX_DOCUMENT_PERSON###IS_ORIGINAL")]
[Index("IsRelated", Name = "IDX_DOCUMENT_PERSON###IS_RELATED")]
[Index("IsSabtahvalChecked", Name = "IDX_DOCUMENT_PERSON###IS_SABTAHVAL_CHECKED")]
[Index("IsSabtahvalCorrect", Name = "IDX_DOCUMENT_PERSON###IS_SABTAHVAL_CORRECT")]
[Index("IsSignedDocument", Name = "IDX_DOCUMENT_PERSON###IS_SIGNED_DOCUMENT")]
[Index("IsTfaRequired", Name = "IDX_DOCUMENT_PERSON###IS_TFA_REQUIRED")]
[Index("LegalpersonNatureId", Name = "IDX_DOCUMENT_PERSON###LEGALPERSON_NATURE_ID")]
[Index("LegalpersonTypeId", Name = "IDX_DOCUMENT_PERSON###LEGALPERSON_TYPE_ID")]
[Index("MobileNo", Name = "IDX_DOCUMENT_PERSON###MOBILE_NO")]
[Index("MobileNoState", Name = "IDX_DOCUMENT_PERSON###MOBILE_NO_STATE")]
[Index("MocState", Name = "IDX_DOCUMENT_PERSON###MOC_STATE")]
[Index("Name", Name = "IDX_DOCUMENT_PERSON###NAME")]
[Index("NationalityId", Name = "IDX_DOCUMENT_PERSON###NATIONALITY_ID")]
[Index("NationalNo", Name = "IDX_DOCUMENT_PERSON###NATIONAL_NO")]
[Index("PassportNo", Name = "IDX_DOCUMENT_PERSON###PASSPORT_NO")]
[Index("PersonType", Name = "IDX_DOCUMENT_PERSON###PERSON_TYPE")]
[Index("SanaState", Name = "IDX_DOCUMENT_PERSON###SANA_STATE")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_PERSON###SCRIPTORIUM_ID")]
[Index("SexType", Name = "IDX_DOCUMENT_PERSON###SEX_TYPE")]
[Index("SignDate", Name = "IDX_DOCUMENT_PERSON###SIGN_DATE")]
[Index("TfaState", Name = "IDX_DOCUMENT_PERSON###TFA_STATE")]
[Index("WouldSignedDocument", Name = "IDX_DOCUMENT_PERSON###WOULD_SIGNED_DOCUMENT")]
[Index("DocumentId", "RowNo", Name = "IDX_SSR_DC_PRS_DID_RWN")]
[Index("AmlakEskanState", Name = "IX_SSR_DOCPRS_AMLAK_ESKAN")]
[Index("DocumentDraftConfirmDate", Name = "IX_SSR_DOC_PRS_DDCD")]
[Index("DocumentDraftConfirmTime", Name = "IX_SSR_DOC_PRS_DDCT")]
[Index("IsDocumentDraftConfirmed", Name = "IX_SSR_DOC_PRS_IDDC")]
[Index("IsPrisoner", Name = "IX_SSR_DOC_PRS_IS_PRISONER")]
[Index("SanaNotificationCode", Name = "IX_SSR_DOC_PRS_SANA_NOTIF_CD")]
[Index("SanaNotificationDateTime", Name = "IX_SSR_DOC_PRS_SANA_NOTIF_DT")]
[Index("LegacyId", Name = "UDX_DOCUMENT_PERSON###LEGACY_ID", IsUnique = true)]
public partial class DocumentPerson
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
    /// آیا این شخص اصیل است؟
    /// </summary>
    [Required]
    [Column("IS_ORIGINAL")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsOriginal { get; set; }

    /// <summary>
    /// شناسه نوع سمت اشخاص سند
    /// </summary>
    [Column("DOCUMENT_PERSON_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string DocumentPersonTypeId { get; set; }

    /// <summary>
    /// آیا این شخص وكیل یا نماینده اشخاص دیگری است؟
    /// </summary>
    [Column("IS_RELATED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRelated { get; set; }

    /// <summary>
    /// حقیقی است یا حقوقی؟
    /// </summary>
    [Column("PERSON_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string PersonType { get; set; }

    /// <summary>
    /// آیا این شخص ایرانی است؟
    /// </summary>
    [Column("IS_IRANIAN")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsIranian { get; set; }

    /// <summary>
    /// شناسه كشور تابعیت
    /// </summary>
    [Column("NATIONALITY_ID")]
    [Precision(6)]
    public int? NationalityId { get; set; }

    /// <summary>
    /// ملیت
    /// </summary>
    [Column("NATIONALITY")]
    [StringLength(100)]
    [Unicode(false)]
    public string Nationality { get; set; }

    /// <summary>
    /// شناسه ملی
    /// </summary>
    [Column("NATIONAL_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    /// <summary>
    /// شماره گذرنامه
    /// </summary>
    [Column("PASSPORT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string PassportNo { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    [Required]
    [Column("NAME")]
    [StringLength(500)]
    [Unicode(false)]
    public string Name { get; set; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    [Column("FAMILY")]
    [StringLength(500)]
    [Unicode(false)]
    public string Family { get; set; }

    /// <summary>
    /// تاریخ تولد
    /// </summary>
    [Column("BIRTH_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string BirthDate { get; set; }

    /// <summary>
    /// سال تولد
    /// </summary>
    [Column("BIRTH_YEAR")]
    [StringLength(4)]
    [Unicode(false)]
    public string BirthYear { get; set; }

    /// <summary>
    /// جنسیت
    /// </summary>
    [Column("SEX_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SexType { get; set; }

    /// <summary>
    /// نام پدر
    /// </summary>
    [Column("FATHER_NAME")]
    [StringLength(150)]
    [Unicode(false)]
    public string FatherName { get; set; }

    /// <summary>
    /// محل صدور شناسنامه
    /// </summary>
    [Column("IDENTITY_ISSUE_LOCATION")]
    [StringLength(200)]
    [Unicode(false)]
    public string IdentityIssueLocation { get; set; }

    /// <summary>
    /// شناسه محل جغرافیایی صدور شناسنامه
    /// </summary>
    [Column("IDENTITY_ISSUE_GEO_LOCATION_ID")]
    [Precision(6)]
    public int? IdentityIssueGeoLocationId { get; set; }

    /// <summary>
    /// شماره شناسنامه
    /// </summary>
    [Column("IDENTITY_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string IdentityNo { get; set; }

    /// <summary>
    /// سری شناسنامه - بخش حرفی
    /// </summary>
    [Column("SERI_ALPHA")]
    [StringLength(50)]
    [Unicode(false)]
    public string SeriAlpha { get; set; }

    /// <summary>
    /// سری شناسنامه - بخش عددی
    /// </summary>
    [Column("SERI")]
    [StringLength(50)]
    [Unicode(false)]
    public string Seri { get; set; }

    /// <summary>
    /// سریال شناسنامه
    /// </summary>
    [Column("SERIAL")]
    [StringLength(50)]
    [Unicode(false)]
    public string Serial { get; set; }

    /// <summary>
    /// نوع شركت
    /// </summary>
    [Column("COMPANY_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string CompanyTypeId { get; set; }

    /// <summary>
    /// شماره ثبت
    /// </summary>
    [Column("COMPANY_REGISTER_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string CompanyRegisterNo { get; set; }

    /// <summary>
    /// تاریخ ثبت
    /// </summary>
    [Column("COMPANY_REGISTER_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CompanyRegisterDate { get; set; }

    /// <summary>
    /// محل ثبت
    /// </summary>
    [Column("COMPANY_REGISTER_LOCATION")]
    [StringLength(100)]
    [Unicode(false)]
    public string CompanyRegisterLocation { get; set; }

    /// <summary>
    /// شماره آخرین روزنامه رسمی/گواهی ثبت شركت ها
    /// </summary>
    [Column("LAST_LEGAL_PAPER_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string LastLegalPaperNo { get; set; }

    /// <summary>
    /// تاریخ آخرین روزنامه رسمی/گواهی ثبت شركت ها
    /// </summary>
    [Column("LAST_LEGAL_PAPER_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LastLegalPaperDate { get; set; }

    /// <summary>
    /// شناسه ماهیت شخص حقوقی
    /// </summary>
    [Column("LEGALPERSON_NATURE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string LegalpersonNatureId { get; set; }

    /// <summary>
    /// شناسه نوع شخص حقوقی
    /// </summary>
    [Column("LEGALPERSON_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string LegalpersonTypeId { get; set; }

    /// <summary>
    /// شماره مجوز استملاك
    /// </summary>
    [Column("ESTEMLAK_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string EstemlakNo { get; set; }

    /// <summary>
    /// تاریخ مجوز استملاك
    /// </summary>
    [Column("ESTEMLAK_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstemlakDate { get; set; }

    /// <summary>
    /// شناسه استعلام های ملك مرتبط
    /// </summary>
    [Column("ESTATE_INQUIRY_ID", TypeName = "CLOB")]
    public string EstateInquiryId { get; set; }

    /// <summary>
    /// آیا حكم رشد دارد؟
    /// </summary>
    [Column("HAS_GROWTH_JUDGMENT")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasGrowthJudgment { get; set; }

    /// <summary>
    /// شماره حكم رشد
    /// </summary>
    [Column("GROWTH_JUDGMENT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string GrowthJudgmentNo { get; set; }

    /// <summary>
    /// تاریخ حكم رشد
    /// </summary>
    [Column("GROWTH_JUDGMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string GrowthJudgmentDate { get; set; }

    /// <summary>
    /// شماره نامه مربوط به حكم رشد
    /// </summary>
    [Column("GROWTH_LETTER_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string GrowthLetterNo { get; set; }

    /// <summary>
    /// تاریخ نامه مربوط به حكم رشد
    /// </summary>
    [Column("GROWTH_LETTER_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string GrowthLetterDate { get; set; }

    /// <summary>
    /// توضیحات مربوط به حكم رشد
    /// </summary>
    [Column("GROWTH_DESCRIPTION")]
    [StringLength(1000)]
    [Unicode(false)]
    public string GrowthDescription { get; set; }

    /// <summary>
    /// شناسه سازمان عمومی صادركننده
    /// </summary>
    [Column("GROWTH_ISSUER_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string GrowthIssuerId { get; set; }

    /// <summary>
    /// شناسه سازمان عمومی ارسال كننده
    /// </summary>
    [Column("GROWTH_SENDER_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string GrowthSenderId { get; set; }

    /// <summary>
    /// آیا استعلام ثبت احوال برای این شخص انجام شده است؟
    /// </summary>
    [Column("IS_SABTAHVAL_CHECKED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSabtahvalChecked { get; set; }

    /// <summary>
    /// آیا اطلاعات وارد شده با اطلاعات ثبت احوال تطابق دارد؟
    /// </summary>
    [Column("IS_SABTAHVAL_CORRECT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSabtahvalCorrect { get; set; }

    /// <summary>
    /// آیا این شخص زنده است؟
    /// </summary>
    [Column("IS_ALIVE")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsAlive { get; set; }

    /// <summary>
    /// وضعیت حساب كاربری ثنا
    /// </summary>
    [Column("SANA_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SanaState { get; set; }

    /// <summary>
    /// آیا شخص حقوقی، در سامانه ثنا ساختار تشكیلات دارد؟
    /// </summary>
    [Column("SANA_HAS_ORGANIZATION_CHART")]
    [StringLength(1)]
    [Unicode(false)]
    public string SanaHasOrganizationChart { get; set; }

    /// <summary>
    /// كد شخص حقوقی در سامانه ثنا
    /// </summary>
    [Column("SANA_ORGANIZATION_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string SanaOrganizationCode { get; set; }

    /// <summary>
    /// كد شخص حقوقی در سامانه ثنا
    /// </summary>
    [Column("SANA_ORGANIZATION_NAME")]
    [StringLength(1000)]
    [Unicode(false)]
    public string SanaOrganizationName { get; set; }

    /// <summary>
    /// آیا كارت هوشمند ملی دارد؟
    /// </summary>
    [Column("HAS_SMART_CARD")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasSmartCard { get; set; }

    /// <summary>
    /// وضعیت تطابق اثر انگشت با اثر انگشت مندرج در كارت هوشمند ملی
    /// </summary>
    [Column("MOC_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string MocState { get; set; }

    /// <summary>
    /// وضعیت مالكیت شماره خط تلفن همراه
    /// </summary>
    [Column("MOBILE_NO_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string MobileNoState { get; set; }

    /// <summary>
    /// آیا انجام مرحله اعتبارسنجی عامل دوم برای این شخص لازم است؟
    /// </summary>
    [Column("IS_TFA_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsTfaRequired { get; set; }

    /// <summary>
    /// نتیجه انجام احراز هویت دو مرحله ای
    /// </summary>
    [Column("TFA_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string TfaState { get; set; }

    /// <summary>
    /// آیا این شخص، متقاضی استفاده از تخفیف حق الثبت مخصوص ایثارگران هست؟
    /// </summary>
    [Column("IS_MARTYR_APPLICANT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsMartyrApplicant { get; set; }

    /// <summary>
    /// بر اساس استعلام انجام شده از بنیاد شهید، آیا این شخص مشمول تخفیف حق الثبت مخصوص ایثارگران هست؟
    /// </summary>
    [Column("IS_MARTYR_INCLUDED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsMartyrIncluded { get; set; }

    /// <summary>
    /// آیا تصویر اثر انگشت شخص گرفته شده است؟
    /// </summary>
    [Column("IS_FINGERPRINT_GOTTEN")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFingerprintGotten { get; set; }

    /// <summary>
    /// آیا این شخص سند را امضاء خواهد كرد؟
    /// </summary>
    [Column("WOULD_SIGNED_DOCUMENT")]
    [StringLength(1)]
    [Unicode(false)]
    public string WouldSignedDocument { get; set; }

    /// <summary>
    /// آیا این شخص سند را امضاء كرده است؟
    /// </summary>
    [Column("IS_SIGNED_DOCUMENT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSignedDocument { get; set; }

    /// <summary>
    /// تاریخ امضاء
    /// </summary>
    [Column("SIGN_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SignDate { get; set; }

    /// <summary>
    /// شماره تلفن همراه
    /// </summary>
    [Column("MOBILE_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string MobileNo { get; set; }

    /// <summary>
    /// آدرس پست الكترونیكی
    /// </summary>
    [Column("EMAIL")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; }

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
    [StringLength(300)]
    [Unicode(false)]
    public string Address { get; set; }

    /// <summary>
    /// نوع نشانی
    /// </summary>
    [Column("ADDRESS_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string AddressType { get; set; }

    /// <summary>
    /// شماره تلفن ثابت
    /// </summary>
    [Column("TEL")]
    [StringLength(50)]
    [Unicode(false)]
    public string Tel { get; set; }

    /// <summary>
    /// شماره فكی
    /// </summary>
    [Column("FAX_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string FaxNo { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
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
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// تاریخ استعلام ثبت احوال
    /// </summary>
    [Column("SABTAHVAL_INQUIRY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SabtahvalInquiryDate { get; set; }

    /// <summary>
    /// زمان استعلام ثبت احوال
    /// </summary>
    [Column("SABTAHVAL_INQUIRY_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string SabtahvalInquiryTime { get; set; }

    /// <summary>
    /// تاریخ استعلام ثنا
    /// </summary>
    [Column("SANA_INQUIRY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SanaInquiryDate { get; set; }

    /// <summary>
    /// زمان استعلام ثنا
    /// </summary>
    [Column("SANA_INQUIRY_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string SanaInquiryTime { get; set; }

    /// <summary>
    /// تاریخ استعلام ام او سی
    /// </summary>
    [Column("MOC_INQUIRY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string MocInquiryDate { get; set; }

    /// <summary>
    /// زمان استعلام ام او سی
    /// </summary>
    [Column("MOC_INQUIRY_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string MocInquiryTime { get; set; }

    /// <summary>
    /// تاریخ استعلام انجام شده از بنیاد شهید
    /// </summary>
    [Column("MARTYR_INQUIRY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string MartyrInquiryDate { get; set; }

    /// <summary>
    /// زمان استعلام انجام شده از بنیاد شهید
    /// </summary>
    [Column("MARTYR_INQUIRY_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string MartyrInquiryTime { get; set; }

    /// <summary>
    /// كد رهگیری استعلام انجام شده از بنیاد شهید
    /// </summary>
    [Column("MARTYR_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string MartyrCode { get; set; }

    /// <summary>
    /// توضیحات استعلام انجام شده از بنیاد شهید
    /// </summary>
    [Column("MARTYR_DESCRIPTION")]
    [StringLength(1000)]
    [Unicode(false)]
    public string MartyrDescription { get; set; }

    /// <summary>
    /// وضعیت ثبت نام در سامانه املاك و اسكان
    /// </summary>
    [Column("AMLAK_ESKAN_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string AmlakEskanState { get; set; }

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
    [Column("OLD_DOCUMENT_PERSON_TYPE_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentPersonTypeId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_NATIONALITY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldNationalityId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_IDENTITY_ISSUE_GEO_LOCATION_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldIdentityIssueGeoLocationId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_ESTATE_INQUIRY_ID", TypeName = "CLOB")]
    public string OldEstateInquiryId { get; set; }

    /// <summary>
    /// آيا اين شخص پيش نويس سند را تأييد کرده است؟
    /// </summary>
    [Column("IS_DOCUMENT_DRAFT_CONFIRMED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsDocumentDraftConfirmed { get; set; }

    /// <summary>
    /// تاريخ تأييد پيش نويس سند توسط شخص
    /// </summary>
    [Column("DOCUMENT_DRAFT_CONFIRM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string DocumentDraftConfirmDate { get; set; }

    /// <summary>
    /// زمان تأييد پيش نويس سند توسط شخص
    /// </summary>
    [Column("DOCUMENT_DRAFT_CONFIRM_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string DocumentDraftConfirmTime { get; set; }

    /// <summary>
    /// آیا ثبت سند برای این شخص در زندان صورت گرفته است؟
    /// </summary>
    [Column("IS_PRISONER")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsPrisoner { get; set; }

    /// <summary>
    /// کد ارسال سند به کارتابل ثنا شخص
    /// </summary>
    [Column("SANA_NOTIFICATION_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string SanaNotificationCode { get; set; }

    /// <summary>
    /// تاریخ و زمان ارسال سند به کارتابل ثنا شخص
    /// </summary>
    [Column("SANA_NOTIFICATION_DATE_TIME")]
    [StringLength(50)]
    [Unicode(false)]
    public string SanaNotificationDateTime { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentPeople")]
    public virtual Document Document { get; set; }

    [InverseProperty("DocumentPerson")]
    public virtual ICollection<DocumentEstateOwnershipDocument> DocumentEstateOwnershipDocuments { get; set; } = new List<DocumentEstateOwnershipDocument>();

    [InverseProperty("DocumentPerson")]
    public virtual ICollection<DocumentEstateQuotum> DocumentEstateQuota { get; set; } = new List<DocumentEstateQuotum>();

    [InverseProperty("DocumentPersonBuyer")]
    public virtual ICollection<DocumentEstateQuotaDetail> DocumentEstateQuotaDetailDocumentPersonBuyers { get; set; } = new List<DocumentEstateQuotaDetail>();

    [InverseProperty("DocumentPersonSeller")]
    public virtual ICollection<DocumentEstateQuotaDetail> DocumentEstateQuotaDetailDocumentPersonSellers { get; set; } = new List<DocumentEstateQuotaDetail>();

    [InverseProperty("DocumentPerson")]
    public virtual ICollection<DocumentEstateSeparationPiecesQuotum> DocumentEstateSeparationPiecesQuota { get; set; } = new List<DocumentEstateSeparationPiecesQuotum>();

    [InverseProperty("DocumentPerson")]
    public virtual ICollection<DocumentInquiry> DocumentInquiries { get; set; } = new List<DocumentInquiry>();

    [InverseProperty("AgentPerson")]
    public virtual ICollection<DocumentPersonRelated> DocumentPersonRelatedAgentPeople { get; set; } = new List<DocumentPersonRelated>();

    [InverseProperty("MainPerson")]
    public virtual ICollection<DocumentPersonRelated> DocumentPersonRelatedMainPeople { get; set; } = new List<DocumentPersonRelated>();

    [ForeignKey("DocumentPersonTypeId")]
    [InverseProperty("DocumentPeople")]
    public virtual DocumentPersonType DocumentPersonType { get; set; }

    [InverseProperty("DocumentPerson")]
    public virtual ICollection<DocumentVehicleQuotum> DocumentVehicleQuota { get; set; } = new List<DocumentVehicleQuotum>();

    [InverseProperty("DocumentPersonBuyer")]
    public virtual ICollection<DocumentVehicleQuotaDetail> DocumentVehicleQuotaDetailDocumentPersonBuyers { get; set; } = new List<DocumentVehicleQuotaDetail>();

    [InverseProperty("DocumentPersonSeller")]
    public virtual ICollection<DocumentVehicleQuotaDetail> DocumentVehicleQuotaDetailDocumentPersonSellers { get; set; } = new List<DocumentVehicleQuotaDetail>();

    [InverseProperty("DocumentPerson")]
    public virtual ICollection<EstateDocumentRequestPerson> EstateDocumentRequestPeople { get; set; } = new List<EstateDocumentRequestPerson>();

    [ForeignKey("GrowthIssuerId")]
    [InverseProperty("DocumentPersonGrowthIssuers")]
    public virtual GeneralOrganization GrowthIssuer { get; set; }

    [ForeignKey("GrowthSenderId")]
    [InverseProperty("DocumentPersonGrowthSenders")]
    public virtual GeneralOrganization GrowthSender { get; set; }

    [InverseProperty("DocumentPerson")]
    public virtual ICollection<SsrDocumentAssetQuotum> SsrDocumentAssetQuota { get; set; } = new List<SsrDocumentAssetQuotum>();

    [InverseProperty("DocumentPersonBuyer")]
    public virtual ICollection<SsrDocumentAssetQuotaDtl> SsrDocumentAssetQuotaDtlDocumentPersonBuyers { get; set; } = new List<SsrDocumentAssetQuotaDtl>();

    [InverseProperty("DocumentPersonSeller")]
    public virtual ICollection<SsrDocumentAssetQuotaDtl> SsrDocumentAssetQuotaDtlDocumentPersonSellers { get; set; } = new List<SsrDocumentAssetQuotaDtl>();
}
