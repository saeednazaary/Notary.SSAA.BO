using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اشخاص استعلام از واحدهای ثبتی (مانند استعلام از اداره مالكیت معنوی)
/// </summary>
[Table("INQUIRY_FROM_UNIT_PERSON")]
[Index("BirthDate", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###BIRTH_DATE")]
[Index("CompanyType", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###COMPANY_TYPE")]
[Index("Family", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###FAMILY")]
[Index("IsAlive", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###IS_ALIVE")]
[Index("IsIranian", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###IS_IRANIAN")]
[Index("IsOriginal", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###IS_ORIGINAL")]
[Index("IsRelated", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###IS_RELATED")]
[Index("IsSabtahvalChecked", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###IS_SABTAHVAL_CHECKED")]
[Index("IsSabtahvalCorrect", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###IS_SABTAHVAL_CORRECT")]
[Index("LegalpersonNatureId", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###LEGALPERSON_NATURE_ID")]
[Index("LegalpersonTypeId", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###LEGALPERSON_TYPE_ID")]
[Index("LegalPersonType", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###LEGAL_PERSON_TYPE")]
[Index("MobileNo", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###MOBILE_NO")]
[Index("Name", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###NAME")]
[Index("NationalityId", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###NATIONALITY_ID")]
[Index("NationalNo", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###NATIONAL_NO")]
[Index("PersonType", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###PERSON_TYPE")]
[Index("SexType", Name = "IDX_INQUIRY_FROM_UNIT_PERSON###SEX_TYPE")]
[Index("InquiryFromUnitId", "RowNo", Name = "UDX_INQUIRY_FROM_UNIT_PERSON###INQUIRY_FROM_UNIT_ID#ROW_NO", IsUnique = true)]
[Index("LegacyId", Name = "UDX_INQUIRY_FROM_UNIT_PERSON###LEGACY_ID", IsUnique = true)]
public partial class InquiryFromUnitPerson
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه استعلام از واحدهای ثبتی (مانند استعلام از اداره مالكیت معنوی)
    /// </summary>
    [Column("INQUIRY_FROM_UNIT_ID")]
    public Guid InquiryFromUnitId { get; set; }

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
    /// آیا این شخص وكیل یا نماینده اشخاص دیگری است؟
    /// </summary>
    [Required]
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
    [Required]
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
    /// شناسه ملی
    /// </summary>
    [Column("NATIONAL_NO")]
    [StringLength(11)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    [Required]
    [Column("NAME")]
    [StringLength(150)]
    [Unicode(false)]
    public string Name { get; set; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    [Column("FAMILY")]
    [StringLength(50)]
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
    [StringLength(50)]
    [Unicode(false)]
    public string FatherName { get; set; }

    /// <summary>
    /// محل صدور شناسنامه
    /// </summary>
    [Column("IDENTITY_ISSUE_LOCATION")]
    [StringLength(50)]
    [Unicode(false)]
    public string IdentityIssueLocation { get; set; }

    /// <summary>
    /// شماره شناسنامه
    /// </summary>
    [Column("IDENTITY_NO")]
    [StringLength(10)]
    [Unicode(false)]
    public string IdentityNo { get; set; }

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
    /// نوع شركت
    /// </summary>
    [Column("COMPANY_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string CompanyType { get; set; }

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
    /// نوع شخص حقوقی
    /// </summary>
    [Column("LEGAL_PERSON_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string LegalPersonType { get; set; }

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
    /// شماره تلفن همراه
    /// </summary>
    [Column("MOBILE_NO")]
    [StringLength(11)]
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
    [StringLength(200)]
    [Unicode(false)]
    public string Address { get; set; }

    /// <summary>
    /// شماره تلفن ثابت
    /// </summary>
    [Column("TEL")]
    [StringLength(50)]
    [Unicode(false)]
    public string Tel { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Description { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("InquiryFromUnitId")]
    [InverseProperty("InquiryFromUnitPeople")]
    public virtual InquiryFromUnit InquiryFromUnit { get; set; }
}
