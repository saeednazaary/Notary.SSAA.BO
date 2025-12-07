using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// شخص استعلام(مالك)
/// </summary>
[Table("ESTATE_INQUIRY_PERSON")]
[Index("BirthPlaceId", Name = "IDX_ESTATE_INQUIRY_PERSON###BIRTH_PLACE_ID")]
[Index("CityId", Name = "IDX_ESTATE_INQUIRY_PERSON###CITY_ID")]
[Index("EstateInquiryId", Name = "IDX_ESTATE_INQUIRY_PERSON###ESTATE_INQUIRY_ID")]
[Index("IssuePlaceId", Name = "IDX_ESTATE_INQUIRY_PERSON###ISSUE_PLACE_ID")]
[Index("LegacyId", Name = "IDX_ESTATE_INQUIRY_PERSON###LEGACY_ID", IsUnique = true)]
[Index("NationalityId", Name = "IDX_ESTATE_INQUIRY_PERSON###NATIONALITY_ID")]
[Index("ScriptoriumId", Name = "IDX_ESTATE_INQUIRY_PERSON###SCRIPTORIUM_ID")]
public partial class EstateInquiryPerson
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    [Required]
    [Column("NAME")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Name { get; set; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    [Column("FAMILY")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Family { get; set; }

    /// <summary>
    /// نام پدر
    /// </summary>
    [Column("FATHER_NAME")]
    [StringLength(2000)]
    [Unicode(false)]
    public string FatherName { get; set; }

    /// <summary>
    /// شماره شناسنامه/ثبت
    /// </summary>
    [Column("IDENTITY_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string IdentityNo { get; set; }

    /// <summary>
    /// تاریخ تولد/ثبت
    /// </summary>
    [Column("BIRTH_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string BirthDate { get; set; }

    /// <summary>
    /// كد/شناسه ملی
    /// </summary>
    [Column("NATIONALITY_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string NationalityCode { get; set; }

    /// <summary>
    /// سری شناسنامه
    /// </summary>
    [Column("SERI")]
    [StringLength(50)]
    [Unicode(false)]
    public string Seri { get; set; }

    /// <summary>
    /// سریال شناسنامه
    /// </summary>
    [Column("SERIAL_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string SerialNo { get; set; }

    /// <summary>
    /// جزسهم
    /// </summary>
    [Column("SHARE_PART", TypeName = "NUMBER(20,4)")]
    public decimal? SharePart { get; set; }

    /// <summary>
    /// متن سهم
    /// </summary>
    [Column("SHARE_TEXT", TypeName = "CLOB")]
    public string ShareText { get; set; }

    /// <summary>
    /// كل سهم
    /// </summary>
    [Column("SHARE_TOTAL", TypeName = "NUMBER(20,4)")]
    public decimal? ShareTotal { get; set; }

    /// <summary>
    /// محل صدور
    /// </summary>
    [Column("ISSUE_PLACE_TEXT")]
    [StringLength(2000)]
    [Unicode(false)]
    public string IssuePlaceText { get; set; }

    /// <summary>
    /// محل تولد خارجی
    /// </summary>
    [Column("FORIEGN_BIRTH_PLACE")]
    [StringLength(2000)]
    [Unicode(false)]
    public string ForiegnBirthPlace { get; set; }

    /// <summary>
    /// محل صدور خارجی
    /// </summary>
    [Column("FORIEGN_ISSUE_PLACE")]
    [StringLength(2000)]
    [Unicode(false)]
    public string ForiegnIssuePlace { get; set; }

    /// <summary>
    /// جنسیت
    /// </summary>
    [Column("SEX_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SexType { get; set; }

    /// <summary>
    /// نوع شخص
    /// </summary>
    [Column("PERSON_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string PersonType { get; set; }

    /// <summary>
    /// كد پستی
    /// </summary>
    [Column("POSTAL_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string PostalCode { get; set; }

    /// <summary>
    /// آدرس
    /// </summary>
    [Column("ADDRESS", TypeName = "CLOB")]
    public string Address { get; set; }

    /// <summary>
    /// كنترل شده با سرویس مبدا
    /// </summary>
    [Column("VERIFIED_BY_SOURCESERVICE")]
    [Precision(12)]
    public long? VerifiedBySourceservice { get; set; }

    /// <summary>
    /// انتقال اجرایی
    /// </summary>
    [Column("EXECUTIVE_TRANSFER")]
    [StringLength(1)]
    [Unicode(false)]
    public string ExecutiveTransfer { get; set; }

    /// <summary>
    /// ردیف محل صدور
    /// </summary>
    [Column("ISSUE_PLACE_ID")]
    [Precision(6)]
    public int? IssuePlaceId { get; set; }

    /// <summary>
    /// ردیف تابعیت
    /// </summary>
    [Column("NATIONALITY_ID")]
    [Precision(6)]
    public int? NationalityId { get; set; }

    /// <summary>
    /// ردیف عامل پارتیشن
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// ردیف محل تولد
    /// </summary>
    [Column("BIRTH_PLACE_ID")]
    [Precision(6)]
    public int? BirthPlaceId { get; set; }

    /// <summary>
    /// ردیف محل سكونت
    /// </summary>
    [Column("CITY_ID")]
    [Precision(6)]
    public int? CityId { get; set; }

    /// <summary>
    /// ردیف استعلام
    /// </summary>
    [Column("ESTATE_INQUIRY_ID")]
    public Guid EstateInquiryId { get; set; }

    /// <summary>
    /// فيلد عددي کنترلي مخصوص کنترل concurrency
    /// </summary>
    [Column("TIMESTAMP", TypeName = "NUMBER(20)")]
    public decimal Timestamp { get; set; }

    /// <summary>
    /// كارت ملی هوشمند دارد؟
    /// </summary>
    [Column("HAS_SMART_CARD")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasSmartCard { get; set; }

    /// <summary>
    /// وضعیت كارت ملی
    /// </summary>
    [Column("MOC_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string MocState { get; set; }

    /// <summary>
    /// وضعیت ثنا
    /// </summary>
    [Column("SANA_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SanaState { get; set; }

    /// <summary>
    /// رمز دو عاملی نیاز است؟
    /// </summary>
    [Column("TFA_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string TfaRequired { get; set; }

    /// <summary>
    /// وضعیت رمز دوعاملی؟
    /// </summary>
    [Column("TFA_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string TfaState { get; set; }

    /// <summary>
    /// آیا این شخص ایرانی است؟
    /// </summary>
    [Column("IS_IRANIAN")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsIranian { get; set; }

    /// <summary>
    /// آیا استعلام ثبت احوال برای این شخص انجام شده است؟
    /// </summary>
    [Column("IS_SABTAHVAL_CHECKED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSabtahvalChecked { get; set; }

    /// <summary>
    /// آیا اطلاعات شخص با ثبت احوال تطابق دارد؟
    /// </summary>
    [Column("IS_SABTAHVAL_CORRECT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSabtahvalCorrect { get; set; }

    /// <summary>
    /// ایا استعلام از سامانه اشخاص حقوقی برای این شخص انجام شده است؟
    /// </summary>
    [Column("IS_ILENC_CHECKED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsIlencChecked { get; set; }

    /// <summary>
    /// آیا اطلاعات شخص با سامانه اشخاص حقوقی تطابق دارد؟
    /// </summary>
    [Column("IS_ILENC_CORRECT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsIlencCorrect { get; set; }

    /// <summary>
    /// آیا استعلام از سامانه اتباع خارجی انجام شده است؟
    /// </summary>
    [Column("IS_FOREIGNERSSYS_CHECKED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsForeignerssysChecked { get; set; }

    /// <summary>
    /// آیا اطلاعات شخص با ساماه اتباع خارجی تطابق دارد؟
    /// </summary>
    [Column("IS_FOREIGNERSSYS_CORRECT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsForeignerssysCorrect { get; set; }

    /// <summary>
    /// سری شناسنامه - بخش حروفی
    /// </summary>
    [Column("SERI_ALPHA")]
    [StringLength(2)]
    [Unicode(false)]
    public string SeriAlpha { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شماره تلفن همراه
    /// </summary>
    [Column("MOBILE_NO")]
    [StringLength(11)]
    [Unicode(false)]
    public string MobileNo { get; set; }

    /// <summary>
    /// وضعيت مالکيت خط موبايل با شاهکار
    /// </summary>
    [Column("MOBILE_NO_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string MobileNoState { get; set; }

    /// <summary>
    /// پست الکترونيک
    /// </summary>
    [Column("EMAIL")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; }

    /// <summary>
    /// تلفن
    /// </summary>
    [Column("TEL")]
    [StringLength(50)]
    [Unicode(false)]
    public string Tel { get; set; }

    /// <summary>
    /// فکس
    /// </summary>
    [Column("FAX")]
    [StringLength(50)]
    [Unicode(false)]
    public string Fax { get; set; }

    /// <summary>
    /// آيا زنده است؟
    /// </summary>
    [Column("IS_ALIVE")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsAlive { get; set; }

    /// <summary>
    /// اصيل است؟
    /// </summary>
    [Required]
    [Column("IS_ORIGINAL")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsOriginal { get; set; }

    /// <summary>
    /// وابسته است؟
    /// </summary>
    [Required]
    [Column("IS_RELATED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRelated { get; set; }

    /// <summary>
    /// آيا در ثنا چارت سازماني دارد؟
    /// </summary>
    [Column("SANA_HAS_ORGANIZATION_CHART")]
    [Precision(3)]
    public byte? SanaHasOrganizationChart { get; set; }

    /// <summary>
    /// تاريخ استعلام ثنا
    /// </summary>
    [Column("SANA_INQUIRY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SanaInquiryDate { get; set; }

    /// <summary>
    /// زمان استعلام ثنا
    /// </summary>
    [Column("SANA_INQUIRY_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SanaInquiryTime { get; set; }

    /// <summary>
    /// شماره موبايل ثنا
    /// </summary>
    [Column("SANA_MOBILE_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string SanaMobileNo { get; set; }

    /// <summary>
    /// کد سازمان ثنا
    /// </summary>
    [Column("SANA_ORGANIZATION_CODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string SanaOrganizationCode { get; set; }

    /// <summary>
    /// نام سازمان ثنا
    /// </summary>
    [Column("SANA_ORGANIZATION_NAME")]
    [StringLength(2000)]
    [Unicode(false)]
    public string SanaOrganizationName { get; set; }

    /// <summary>
    /// ملاحظات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Description { get; set; }

    /// <summary>
    /// تاريخ آخرين روزنامه رسمي
    /// </summary>
    [Column("LAST_LEGAL_PAPER_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LastLegalPaperDate { get; set; }

    /// <summary>
    /// شماره آخرين روزنامه رسمي
    /// </summary>
    [Column("LAST_LEGAL_PAPER_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string LastLegalPaperNo { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("LEGALPERSON_NATURE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string LegalpersonNatureId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("LEGALPERSON_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string LegalpersonTypeId { get; set; }

    /// <summary>
    /// شناسه نوع شرکت
    /// </summary>
    [Column("COMPANY_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string CompanyTypeId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OTHER_LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OtherLegacyId { get; set; }

    [ForeignKey("EstateInquiryId")]
    [InverseProperty("EstateInquiryPeople")]
    public virtual EstateInquiry EstateInquiry { get; set; }
}
