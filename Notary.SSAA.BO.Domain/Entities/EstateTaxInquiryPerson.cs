using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اشخاص مرتبط با امور مالیاتی
/// </summary>
[Table("ESTATE_TAX_INQUIRY_PERSON")]
[Index("DealsummaryPersonRelateTypeId", Name = "IX_SSR_EPRS_DLSMPRRLTYPID")]
[Index("EstateTaxInquiryId", Name = "IX_SSR_EPRS_ETXINQID")]
public partial class EstateTaxInquiryPerson
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// آدرس
    /// </summary>
    [Column("ADDRESS", TypeName = "CLOB")]
    public string Address { get; set; }

    /// <summary>
    /// تاریخ تولد/ثبت
    /// </summary>
    [Column("BIRTH_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string BirthDate { get; set; }

    /// <summary>
    /// ردیف نوع ارتباط
    /// </summary>
    [Required]
    [Column("DEALSUMMARY_PERSON_RELATE_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string DealsummaryPersonRelateTypeId { get; set; }

    /// <summary>
    /// ردیف استعلام مرتبط
    /// </summary>
    [Column("ESTATE_TAX_INQUIRY_ID")]
    public Guid EstateTaxInquiryId { get; set; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    [Column("FAMILY")]
    [StringLength(500)]
    [Unicode(false)]
    public string Family { get; set; }

    /// <summary>
    /// نام پدر
    /// </summary>
    [Column("FATHER_NAME")]
    [StringLength(500)]
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
    /// محل صدور
    /// </summary>
    [Column("ISSUE_PLACE")]
    [StringLength(2000)]
    [Unicode(false)]
    public string IssuePlace { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    [Column("NAME")]
    [StringLength(500)]
    [Unicode(false)]
    public string Name { get; set; }

    /// <summary>
    /// شماره/شناسه ملی
    /// </summary>
    [Column("NATIONALITY_CODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string NationalityCode { get; set; }

    /// <summary>
    /// ردیف تابعیت
    /// </summary>
    [Column("NATIONALITY_ID")]
    [Precision(6)]
    public int NationalityId { get; set; }

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
    [StringLength(15)]
    [Unicode(false)]
    public string PostalCode { get; set; }

    /// <summary>
    /// ردیف دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// سهم از كل سهم خریداری شده
    /// </summary>
    [Column("SHARE_PART", TypeName = "NUMBER(16,4)")]
    public decimal? SharePart { get; set; }

    /// <summary>
    /// كل سهم خریداری شده
    /// </summary>
    [Column("SHARE_TOTAL", TypeName = "NUMBER(16,4)")]
    public decimal? ShareTotal { get; set; }

    /// <summary>
    /// آيا شخص خارجي چک شده است؟
    /// </summary>
    [Column("IS_FOREIGNERSSYS_CHECKED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsForeignerssysChecked { get; set; }

    /// <summary>
    /// آيا شخص خارجي صحيح است؟
    /// </summary>
    [Column("IS_FOREIGNERSSYS_CORRECT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsForeignerssysCorrect { get; set; }

    /// <summary>
    /// آيا با پايگاه اشخاص حقوقي چک شده است؟
    /// </summary>
    [Column("IS_ILENC_CHECKED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsIlencChecked { get; set; }

    /// <summary>
    /// آيا با پايگاه اشخاص حقوقي تطابق دارد؟
    /// </summary>
    [Column("IS_ILENC_CORRECT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsIlencCorrect { get; set; }

    /// <summary>
    /// آيا ايراني است؟
    /// </summary>
    [Column("IS_IRANIAN")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsIranian { get; set; }

    /// <summary>
    /// آيا با ثبت احوال چک شده است؟
    /// </summary>
    [Column("IS_SABTAHVAL_CHECKED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSabtahvalChecked { get; set; }

    /// <summary>
    /// آيا اطلاعات شخص با ثبت احوال تطابق دارد؟
    /// </summary>
    [Column("IS_SABTAHVAL_CORRECT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSabtahvalCorrect { get; set; }

    /// <summary>
    /// فيلد عددي کنترلي مخصوص کنترل concurrency
    /// </summary>
    [Column("TIMESTAMP", TypeName = "NUMBER(20)")]
    public decimal Timestamp { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه در سیستم قدیم
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
    /// جنسيت
    /// </summary>
    [Column("SEX_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SexType { get; set; }

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
    /// سري شناسنامه
    /// </summary>
    [Column("SERI")]
    [StringLength(10)]
    [Unicode(false)]
    public string Seri { get; set; }

    /// <summary>
    /// سريال شناسنامه
    /// </summary>
    [Column("SERIAL")]
    [StringLength(10)]
    [Unicode(false)]
    public string Serial { get; set; }

    /// <summary>
    /// بخش حرفي شماره شناسنامه
    /// </summary>
    [Column("SERI_ALPHA")]
    [StringLength(2)]
    [Unicode(false)]
    public string SeriAlpha { get; set; }

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
    /// شناسه نوع شرکت
    /// </summary>
    [Column("COMPANY_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string CompanyTypeId { get; set; }

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
    /// وضعيت کد دو عاملي
    /// </summary>
    [Column("TFA_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string TfaState { get; set; }

    /// <summary>
    /// آيا کد دو عاملي اجباري است؟
    /// </summary>
    [Column("TFA_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string TfaRequired { get; set; }

    /// <summary>
    /// متن سهم
    /// </summary>
    [Column("SHARE_TEXT", TypeName = "CLOB")]
    public string ShareText { get; set; }

    /// <summary>
    /// وضعيت ثنا
    /// </summary>
    [Column("SANA_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SanaState { get; set; }

    /// <summary>
    /// وضعيت ام او سي
    /// </summary>
    [Column("MOC_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string MocState { get; set; }

    /// <summary>
    /// شناسه محل صدور
    /// </summary>
    [Column("ISSUE_PLACE_ID")]
    [Precision(6)]
    public int? IssuePlaceId { get; set; }

    /// <summary>
    /// آيا کارت ملي هوشمند دارد؟
    /// </summary>
    [Column("HAS_SMART_CARD")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasSmartCard { get; set; }

    /// <summary>
    /// محل صدور خارجي
    /// </summary>
    [Column("FORIEGN_ISSUE_PLACE")]
    [StringLength(2000)]
    [Unicode(false)]
    public string ForiegnIssuePlace { get; set; }

    /// <summary>
    /// محل تولد خارجي
    /// </summary>
    [Column("FORIEGN_BIRTH_PLACE")]
    [StringLength(2000)]
    [Unicode(false)]
    public string ForiegnBirthPlace { get; set; }

    /// <summary>
    /// شناسه شهر
    /// </summary>
    [Column("CITY_ID")]
    [Precision(6)]
    public int? CityId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("BIRTH_PLACE_ID")]
    [Precision(6)]
    public int? BirthPlaceId { get; set; }

    /// <summary>
    /// وضعيت
    /// </summary>
    [Column("CHANGE_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string ChangeState { get; set; }

    /// <summary>
    /// نوع سمت
    /// </summary>
    [Column("ROLE_TYPE", TypeName = "NUMBER(1)")]
    public bool? RoleType { get; set; }

    [ForeignKey("DealsummaryPersonRelateTypeId")]
    [InverseProperty("EstateTaxInquiryPeople")]
    public virtual DealsummaryPersonRelateType DealsummaryPersonRelateType { get; set; }

    [ForeignKey("EstateTaxInquiryId")]
    [InverseProperty("EstateTaxInquiryPeople")]
    public virtual EstateTaxInquiry EstateTaxInquiry { get; set; }
}
