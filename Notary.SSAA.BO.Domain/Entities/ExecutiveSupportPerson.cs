using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اشخاص سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه
/// </summary>
[Table("EXECUTIVE_SUPPORT_PERSON")]
[Index("CompanyType", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###COMPANY_TYPE")]
[Index("ExecutiveAddressTypeId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###EXECUTIVE_ADDRESS_TYPE_ID")]
[Index("ExecutivePersonPostTypeId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###EXECUTIVE_PERSON_POST_TYPE_ID")]
[Index("HasSana", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###HAS_SANA")]
[Index("Ilm", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###ILM")]
[Index("IsAlive", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###IS_ALIVE")]
[Index("IsFingerprintGotten", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###IS_FINGERPRINT_GOTTEN")]
[Index("IsIranian", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###IS_IRANIAN")]
[Index("IsOriginal", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###IS_ORIGINAL")]
[Index("IsRelated", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###IS_RELATED")]
[Index("IsSabtahvalChecked", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###IS_SABTAHVAL_CHECKED")]
[Index("IsSabtahvalCorrect", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###IS_SABTAHVAL_CORRECT")]
[Index("LegalpersonNatureId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###LEGALPERSON_NATURE_ID")]
[Index("LegalpersonTypeId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###LEGALPERSON_TYPE_ID")]
[Index("MobileNo", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###MOBILE_NO")]
[Index("NationalityId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###NATIONALITY_ID")]
[Index("NationalNo", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###NATIONAL_NO")]
[Index("PersonType", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###PERSON_TYPE")]
[Index("RowNo", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###ROW_NO")]
[Index("ScriptoriumId", Name = "IDX_EXECUTIVE_SUPPORT_PERSON###SCRIPTORIUM_ID")]
[Index("ExecutiveSupportId", "RowNo", Name = "UDX_EXECUTIVE_SUPPORT_PERSON###EXECUTIVE_SUPPORT_ID#ROW_NO", IsUnique = true)]
[Index("LegacyId", Name = "UDX_EXECUTIVE_SUPPORT_PERSON###LEGACY_ID", IsUnique = true)]
public partial class ExecutiveSupportPerson
{
    /// <summary>
    ///  ردیف
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه
    /// </summary>
    [Column("EXECUTIVE_SUPPORT_ID")]
    public Guid ExecutiveSupportId { get; set; }

    /// <summary>
    /// ردیف
    /// </summary>
    [Column("ROW_NO")]
    [Precision(3)]
    public byte RowNo { get; set; }

    /// <summary>
    /// شناسه سمت اشخاص در اجرائیات ثبت
    /// </summary>
    [Column("EXECUTIVE_PERSON_POST_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string ExecutivePersonPostTypeId { get; set; }

    /// <summary>
    /// حقیقی است یا حقوقی؟
    /// </summary>
    [Required]
    [Column("PERSON_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string PersonType { get; set; }

    /// <summary>
    /// شناسه ملی
    /// </summary>
    [Column("NATIONAL_NO")]
    [StringLength(11)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    /// <summary>
    /// تاریخ تولد
    /// </summary>
    [Column("BIRTH_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string BirthDate { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    [Required]
    [Column("NAME")]
    [StringLength(250)]
    [Unicode(false)]
    public string Name { get; set; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    [Column("FAMILY")]
    [StringLength(150)]
    [Unicode(false)]
    public string Family { get; set; }

    /// <summary>
    /// نام پدر
    /// </summary>
    [Column("FATHER_NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string FatherName { get; set; }

    /// <summary>
    /// جنسیت
    /// </summary>
    [Column("SEX_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SexType { get; set; }

    /// <summary>
    /// محل تولد
    /// </summary>
    [Column("BIRTH_LOCATION")]
    [StringLength(50)]
    [Unicode(false)]
    public string BirthLocation { get; set; }

    /// <summary>
    /// شماره شناسنامه
    /// </summary>
    [Column("IDENTITY_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string IdentityNo { get; set; }

    /// <summary>
    /// محل صدور شناسنامه
    /// </summary>
    [Column("IDENTITY_ISSUE_LOCATION")]
    [StringLength(50)]
    [Unicode(false)]
    public string IdentityIssueLocation { get; set; }

    /// <summary>
    /// سری شناسنامه - بخش حرفی
    /// </summary>
    [Column("SERI_ALPHA")]
    [StringLength(2)]
    [Unicode(false)]
    public string SeriAlpha { get; set; }

    /// <summary>
    /// سری شناسنامه - بخش عددی
    /// </summary>
    [Column("SERI")]
    [StringLength(10)]
    [Unicode(false)]
    public string Seri { get; set; }

    /// <summary>
    /// سریال شناسنامه
    /// </summary>
    [Column("SERIAL")]
    [StringLength(10)]
    [Unicode(false)]
    public string Serial { get; set; }

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
    /// شماره گذرنامه
    /// </summary>
    [Column("PASSPORT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string PassportNo { get; set; }

    /// <summary>
    /// شناسه نوع شركت
    /// </summary>
    [Column("COMPANY_TYPE")]
    [StringLength(2)]
    [Unicode(false)]
    public string CompanyType { get; set; }

    /// <summary>
    /// محل ثبت
    /// </summary>
    [Column("COMPANY_REGISTER_LOCATION")]
    [StringLength(100)]
    [Unicode(false)]
    public string CompanyRegisterLocation { get; set; }

    /// <summary>
    /// شماره ثبت
    /// </summary>
    [Column("COMPANY_REGISTER_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string CompanyRegisterNo { get; set; }

    /// <summary>
    /// نوع شخص حقوقی
    /// </summary>
    [Column("LEGAL_PERSON_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string LegalPersonType { get; set; }

    /// <summary>
    /// تاریخ ثبت شخص حقوقی
    /// </summary>
    [Column("COMPANY_REGISTER_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CompanyRegisterDate { get; set; }

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
    /// آیا این شخص اصیل است؟
    /// </summary>
    [Required]
    [Column("IS_ORIGINAL")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsOriginal { get; set; }

    /// <summary>
    /// آیا این شخص وكیل است؟
    /// </summary>
    [Required]
    [Column("IS_RELATED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRelated { get; set; }

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
    /// تاریخ مطابقت با ثبت احوال
    /// </summary>
    [Column("SABTAHVAL_INQUIRY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SabtahvalInquiryDate { get; set; }

    /// <summary>
    /// زمان مطابقت با ثبت احوال
    /// </summary>
    [Column("SABTAHVAL_INQUIRY_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SabtahvalInquiryTime { get; set; }

    /// <summary>
    /// آیا تصویر اثر انگشت شخص گرفته شده است؟
    /// </summary>
    [Column("IS_FINGERPRINT_GOTTEN")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFingerprintGotten { get; set; }

    /// <summary>
    /// آیا در سامانه ثنا حساب كاربری دارد؟
    /// </summary>
    [Column("HAS_SANA")]
    [Precision(3)]
    public byte? HasSana { get; set; }

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
    [StringLength(8)]
    [Unicode(false)]
    public string SanaInquiryTime { get; set; }

    /// <summary>
    /// نام شخص حقوقی در سامانه ثنا
    /// </summary>
    [Column("SANA_ORGANIZATION_NAME")]
    [StringLength(2000)]
    [Unicode(false)]
    public string SanaOrganizationName { get; set; }

    /// <summary>
    /// كد شخص حقوقی در سامانه ثنا
    /// </summary>
    [Column("SANA_ORGANIZATION_CODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string SanaOrganizationCode { get; set; }

    /// <summary>
    /// شماره تلفن همراه در سامانه ثنا
    /// </summary>
    [Column("SANA_MOBILE_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string SanaMobileNo { get; set; }

    /// <summary>
    /// آیا شخص حقوقی، ساختار تشكیلات دارد؟ در سامانه ثنا
    /// </summary>
    [Column("SANA_HAS_ORGANIZATION_CHART")]
    [Precision(3)]
    public byte? SanaHasOrganizationChart { get; set; }

    /// <summary>
    /// شماره تلفن همراه
    /// </summary>
    [Column("MOBILE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string MobileNo { get; set; }

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
    [StringLength(200)]
    [Unicode(false)]
    public string Address { get; set; }

    /// <summary>
    /// نوع نشانی
    /// </summary>
    [Column("EXECUTIVE_ADDRESS_TYPE_ID")]
    [StringLength(1)]
    [Unicode(false)]
    public string ExecutiveAddressTypeId { get; set; }

    /// <summary>
    /// شماره تلفن ثابت
    /// </summary>
    [Column("TEL")]
    [StringLength(50)]
    [Unicode(false)]
    public string Tel { get; set; }

    /// <summary>
    /// شماره فكس
    /// </summary>
    [Column("FAX")]
    [StringLength(50)]
    [Unicode(false)]
    public string Fax { get; set; }

    /// <summary>
    /// آدرس پست الكترونیكی
    /// </summary>
    [Column("EMAIL")]
    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; }

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

    [ForeignKey("ExecutiveAddressTypeId")]
    [InverseProperty("ExecutiveSupportPeople")]
    public virtual ExecutiveAddressType ExecutiveAddressType { get; set; }

    [ForeignKey("ExecutivePersonPostTypeId")]
    [InverseProperty("ExecutiveSupportPeople")]
    public virtual ExecutivePersonPostType ExecutivePersonPostType { get; set; }

    [ForeignKey("ExecutiveSupportId")]
    [InverseProperty("ExecutiveSupportPeople")]
    public virtual ExecutiveSupport ExecutiveSupport { get; set; }

    [InverseProperty("OwnerPerson")]
    public virtual ICollection<ExecutiveSupportAsset> ExecutiveSupportAssets { get; set; } = new List<ExecutiveSupportAsset>();

    [InverseProperty("ChangedAddressPerson")]
    public virtual ICollection<ExecutiveSupport> ExecutiveSupportChangedAddressPeople { get; set; } = new List<ExecutiveSupport>();

    [InverseProperty("AgentPerson")]
    public virtual ICollection<ExecutiveSupportPersonRelated> ExecutiveSupportPersonRelatedAgentPeople { get; set; } = new List<ExecutiveSupportPersonRelated>();

    [InverseProperty("MainPerson")]
    public virtual ICollection<ExecutiveSupportPersonRelated> ExecutiveSupportPersonRelatedMainPeople { get; set; } = new List<ExecutiveSupportPersonRelated>();

    [InverseProperty("Requester")]
    public virtual ICollection<ExecutiveSupport> ExecutiveSupportRequesters { get; set; } = new List<ExecutiveSupport>();
}
