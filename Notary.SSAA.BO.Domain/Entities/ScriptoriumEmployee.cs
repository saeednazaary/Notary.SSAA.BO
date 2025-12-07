using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// كاركنان دفترخانه ها
/// </summary>
[Table("SCRIPTORIUM_EMPLOYEE")]
[Index("BirthDate", Name = "IDX_SCRIPTORIUM_EMPLOYEE###BIRTH_DATE")]
[Index("Family", Name = "IDX_SCRIPTORIUM_EMPLOYEE###FAMILY")]
[Index("IsAlive", Name = "IDX_SCRIPTORIUM_EMPLOYEE###IS_ALIVE")]
[Index("IsSabtahvalChecked", Name = "IDX_SCRIPTORIUM_EMPLOYEE###IS_SABTAHVAL_CHECKED")]
[Index("IsSabtahvalCorrect", Name = "IDX_SCRIPTORIUM_EMPLOYEE###IS_SABTAHVAL_CORRECT")]
[Index("MobileNo", Name = "IDX_SCRIPTORIUM_EMPLOYEE###MOBILE_NO")]
[Index("MobileNoState", Name = "IDX_SCRIPTORIUM_EMPLOYEE###MOBILE_NO_STATE")]
[Index("Name", Name = "IDX_SCRIPTORIUM_EMPLOYEE###NAME")]
[Index("NationalNo", Name = "IDX_SCRIPTORIUM_EMPLOYEE###NATIONAL_NO")]
[Index("SanaState", Name = "IDX_SCRIPTORIUM_EMPLOYEE###SANA_STATE")]
[Index("SexType", Name = "IDX_SCRIPTORIUM_EMPLOYEE###SEX_TYPE")]
[Index("LegacyId", Name = "UDX_SCRIPTORIUM_EMPLOYEE###LEGACY_ID", IsUnique = true)]
public partial class ScriptoriumEmployee
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شماره ملی
    /// </summary>
    [Required]
    [Column("NATIONAL_NO")]
    [StringLength(10)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    [Required]
    [Column("NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    [Required]
    [Column("FAMILY")]
    [StringLength(50)]
    [Unicode(false)]
    public string Family { get; set; }

    /// <summary>
    /// نام مستعار
    /// </summary>
    [Column("ALIAS_NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string AliasName { get; set; }

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
    /// وضعیت مالكیت شماره خط تلفن همراه
    /// </summary>
    [Column("MOBILE_NO_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string MobileNoState { get; set; }

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

    [InverseProperty("ScriptoriumEmployee")]
    public virtual ICollection<ScriptoriumEmployeeAccess> ScriptoriumEmployeeAccesses { get; set; } = new List<ScriptoriumEmployeeAccess>();
}
