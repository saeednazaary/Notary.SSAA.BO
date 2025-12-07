using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// شناسه گواهی امضاء
/// </summary>
[Table("SIGN_REQUEST_PERSON")]
[Index("BirthDate", Name = "IDX_SIGN_REQUEST_PERSON###BIRTH_DATE")]
[Index("Family", Name = "IDX_SIGN_REQUEST_PERSON###FAMILY")]
[Index("FatherName", Name = "IDX_SIGN_REQUEST_PERSON###FATHER_NAME")]
[Index("HasSmartCard", Name = "IDX_SIGN_REQUEST_PERSON###HAS_SMART_CARD")]
[Index("IdentityIssueLocation", Name = "IDX_SIGN_REQUEST_PERSON###IDENTITY_ISSUE_LOCATION")]
[Index("IdentityNo", Name = "IDX_SIGN_REQUEST_PERSON###IDENTITY_NO")]
[Index("Ilm", Name = "IDX_SIGN_REQUEST_PERSON###ILM")]
[Index("IsAlive", Name = "IDX_SIGN_REQUEST_PERSON###IS_ALIVE")]
[Index("IsFingerprintGotten", Name = "IDX_SIGN_REQUEST_PERSON###IS_FINGERPRINT_GOTTEN")]
[Index("IsIranian", Name = "IDX_SIGN_REQUEST_PERSON###IS_IRANIAN")]
[Index("IsOriginal", Name = "IDX_SIGN_REQUEST_PERSON###IS_ORIGINAL")]
[Index("IsRelated", Name = "IDX_SIGN_REQUEST_PERSON###IS_RELATED")]
[Index("IsSabtahvalChecked", Name = "IDX_SIGN_REQUEST_PERSON###IS_SABTAHVAL_CHECKED")]
[Index("IsSabtahvalCorrect", Name = "IDX_SIGN_REQUEST_PERSON###IS_SABTAHVAL_CORRECT")]
[Index("MobileNo", Name = "IDX_SIGN_REQUEST_PERSON###MOBILE_NO")]
[Index("MobileNoState", Name = "IDX_SIGN_REQUEST_PERSON###MOBILE_NO_STATE")]
[Index("MocState", Name = "IDX_SIGN_REQUEST_PERSON###MOC_STATE")]
[Index("Name", Name = "IDX_SIGN_REQUEST_PERSON###NAME")]
[Index("NationalityId", Name = "IDX_SIGN_REQUEST_PERSON###NATIONALITY_ID")]
[Index("NationalNo", Name = "IDX_SIGN_REQUEST_PERSON###NATIONAL_NO")]
[Index("PersonType", Name = "IDX_SIGN_REQUEST_PERSON###PERSON_TYPE")]
[Index("RecordDate", Name = "IDX_SIGN_REQUEST_PERSON###RECORD_DATE")]
[Index("SanaState", Name = "IDX_SIGN_REQUEST_PERSON###SANA_STATE")]
[Index("ScriptoriumId", Name = "IDX_SIGN_REQUEST_PERSON###SCRIPTORIUM_ID")]
[Index("SexType", Name = "IDX_SIGN_REQUEST_PERSON###SEX_TYPE")]
[Index("SignClassifyNo", Name = "IDX_SIGN_REQUEST_PERSON###SIGN_CLASSIFY_NO")]
[Index("TfaRequired", Name = "IDX_SIGN_REQUEST_PERSON###TFA_REQUIRED")]
[Index("TfaState", Name = "IDX_SIGN_REQUEST_PERSON###TFA_STATE")]
[Index("AmlakEskanState", Name = "IX_SSR_SIGNREQPRS_AMLAK_ESKAN")]
[Index("IsPrisoner", Name = "IX_SSR_SIGNREQPRS_IS_PRISONER")]
[Index("SignRequestId", Name = "IX_SSR_SIGNREQPRS_REQID")]
[Index("RowNo", Name = "IX_SSR_SIGNREQPRS_ROWNO")]
[Index("ClassifyNoReserved", Name = "IX_SSR_SIGN_REQPRS_CLSFYNO_RES")]
[Index("OldNationalityId", Name = "SSR_IX_SIGN_RQ_PRS_REL_LD_NAT_ID")]
[Index("OldSignRequestId", Name = "SSR_IX_SIGN_RQ_PRS_REL_LD_RQ_ID")]
[Index("LegacyId", Name = "UDX_SIGN_REQUEST_PERSON###LEGACY_ID", IsUnique = true)]
public partial class SignRequestPerson
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه گواهی امضاء
    /// </summary>
    [Column("SIGN_REQUEST_ID")]
    public Guid SignRequestId { get; set; }

    /// <summary>
    /// ردیف
    /// </summary>
    [Column("ROW_NO")]
    [Precision(5)]
    public short RowNo { get; set; }

    /// <summary>
    /// شماره ترتیب گواهی امضاء
    /// </summary>
    [Column("SIGN_CLASSIFY_NO")]
    [Precision(8)]
    public int? SignClassifyNo { get; set; }

    /// <summary>
    /// ملاحظات مربوط به شماره ترتیب گواهی امضاء
    /// </summary>
    [Column("SIGN_CLASSIFY_NO_DESCRIPTION")]
    [StringLength(1000)]
    [Unicode(false)]
    public string SignClassifyNoDescription { get; set; }

    /// <summary>
    /// آیا این شخص اصیل است؟
    /// </summary>
    [Column("IS_ORIGINAL")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsOriginal { get; set; }

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
    [StringLength(200)]
    [Unicode(false)]
    public string Name { get; set; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    [Column("FAMILY")]
    [StringLength(200)]
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
    [StringLength(100)]
    [Unicode(false)]
    public string FatherName { get; set; }

    /// <summary>
    /// محل صدور شناسنامه
    /// </summary>
    [Column("IDENTITY_ISSUE_LOCATION")]
    [StringLength(100)]
    [Unicode(false)]
    public string IdentityIssueLocation { get; set; }

    /// <summary>
    /// شماره شناسنامه
    /// </summary>
    [Column("IDENTITY_NO")]
    [StringLength(20)]
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
    /// آیا انجام احراز هویت دو مرحله ای برای این شخص لازم است؟
    /// </summary>
    [Column("TFA_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string TfaRequired { get; set; }

    /// <summary>
    /// نتیجه انجام احراز هویت دو مرحله ای
    /// </summary>
    [Column("TFA_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string TfaState { get; set; }

    /// <summary>
    /// آیا تصویر اثر انگشت شخص گرفته شده است؟
    /// </summary>
    [Column("IS_FINGERPRINT_GOTTEN")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFingerprintGotten { get; set; }

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
    [StringLength(150)]
    [Unicode(false)]
    public string Email { get; set; }

    /// <summary>
    /// كد پستی
    /// </summary>
    [Column("POSTAL_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string PostalCode { get; set; }

    /// <summary>
    /// نشانی
    /// </summary>
    [Column("ADDRESS")]
    [Unicode(false)]
    public string Address { get; set; }

    /// <summary>
    /// شماره تلفن ثابت
    /// </summary>
    [Column("TEL")]
    [StringLength(100)]
    [Unicode(false)]
    public string Tel { get; set; }

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
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// تاریخ ركورد به میلادی
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_SIGN_REQUEST_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldSignRequestId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_NATIONALITY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldNationalityId { get; set; }

    /// <summary>
    /// وضعیت ثبت نام در سامانه املاك و اسكان
    /// </summary>
    [Column("AMLAK_ESKAN_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string AmlakEskanState { get; set; }

    /// <summary>
    /// شماره ترتيب - رزرو شده
    /// </summary>
    [Column("CLASSIFY_NO_RESERVED")]
    [Precision(8)]
    public int? ClassifyNoReserved { get; set; }

    /// <summary>
    /// آیا ثبت سند برای این شخص در زندان صورت گرفته است؟
    /// </summary>
    [Column("IS_PRISONER")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsPrisoner { get; set; }

    [InverseProperty("SignRequestPerson")]
    public virtual SignElectronicBook SignElectronicBook { get; set; }

    [ForeignKey("SignRequestId")]
    [InverseProperty("SignRequestPeople")]
    public virtual SignRequest SignRequest { get; set; }

    [InverseProperty("AgentPerson")]
    public virtual ICollection<SignRequestPersonRelated> SignRequestPersonRelatedAgentPeople { get; set; } = new List<SignRequestPersonRelated>();

    [InverseProperty("MainPerson")]
    public virtual ICollection<SignRequestPersonRelated> SignRequestPersonRelatedMainPeople { get; set; } = new List<SignRequestPersonRelated>();
}
