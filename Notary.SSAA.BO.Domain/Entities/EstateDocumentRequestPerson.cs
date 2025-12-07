using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اشخاص مرتبط
/// </summary>
[Table("ESTATE_DOCUMENT_REQUEST_PERSON")]
[Index("AgentTypeId", Name = "IX_SSR_EDCREQ_AGNTYPID")]
[Index("DocumentPersonId", Name = "IX_SSR_EDCREQ_PERID")]
[Index("DocumentRequestId", Name = "IX_SSR_EDCREQ_REQID")]
public partial class EstateDocumentRequestPerson
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
    /// ردیف نوع نمایندگی
    /// </summary>
    [Column("AGENT_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string AgentTypeId { get; set; }

    /// <summary>
    /// تاریخ تولد/ثبت
    /// </summary>
    [Column("BIRTH_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string BirthDate { get; set; }

    /// <summary>
    /// ردیف نوع شركت
    /// </summary>
    [Column("COMPANY_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string CompanyTypeId { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION", TypeName = "CLOB")]
    public string Description { get; set; }

    /// <summary>
    /// ردیف درخواست
    /// </summary>
    [Column("DOCUMENT_REQUEST_ID")]
    public Guid DocumentRequestId { get; set; }

    /// <summary>
    /// ایمیل
    /// </summary>
    [Column("EMAIL")]
    [StringLength(500)]
    [Unicode(false)]
    public string Email { get; set; }

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
    /// شماره فكس
    /// </summary>
    [Column("FAX_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string FaxNo { get; set; }

    /// <summary>
    /// كارت ملی هوشمند دارد
    /// </summary>
    [Column("HAS_SMART_CARD")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasSmartCard { get; set; }

    /// <summary>
    /// محل صدور/ثبت
    /// </summary>
    [Column("IDENTITY_ISSUE_LOCATION")]
    [StringLength(500)]
    [Unicode(false)]
    public string IdentityIssueLocation { get; set; }

    /// <summary>
    /// شماره شناسنامه/ثبت
    /// </summary>
    [Column("IDENTITY_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string IdentityNo { get; set; }

    /// <summary>
    /// زنده است؟
    /// </summary>
    [Column("IS_ALIVE")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsAlive { get; set; }

    /// <summary>
    /// اخذ اثر انگشت انجام شده
    /// </summary>
    [Column("IS_FINGERPRINT_GOTTEN")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFingerprintGotten { get; set; }

    /// <summary>
    /// ایرانی است؟
    /// </summary>
    [Column("IS_IRANIAN")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsIranian { get; set; }

    /// <summary>
    /// مالك است؟
    /// </summary>
    [Column("IS_ORIGINAL")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsOriginal { get; set; }

    /// <summary>
    /// نماینده یا وكیل یا ... است؟
    /// </summary>
    [Column("IS_RELATED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRelated { get; set; }

    /// <summary>
    /// با ثبت احوال چك شده؟
    /// </summary>
    [Column("IS_SABTAHVAL_CHECKED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSabtahvalChecked { get; set; }

    /// <summary>
    /// تطابق اطلعات با ثبت احوال
    /// </summary>
    [Column("IS_SABTAHVAL_CORRECT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSabtahvalCorrect { get; set; }

    /// <summary>
    /// تاریخ آخرین روزنامه رسمی/گواهی ثبت شركت ها
    /// </summary>
    [Column("LAST_LEGAL_PAPER_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LastLegalPaperDate { get; set; }

    /// <summary>
    /// شماره آخرین روزنامه رسمی/گواهی ثبت شركت ها
    /// </summary>
    [Column("LAST_LEGAL_PAPER_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string LastLegalPaperNo { get; set; }

    /// <summary>
    /// ردیف ماهیت
    /// </summary>
    [Column("LEGALPERSON_NATURE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string LegalpersonNatureId { get; set; }

    /// <summary>
    /// ردیف نوع
    /// </summary>
    [Column("LEGALPERSON_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string LegalpersonTypeId { get; set; }

    /// <summary>
    /// شماره تلفن همراه
    /// </summary>
    [Column("MOBILE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string MobileNo { get; set; }

    /// <summary>
    /// وضعیت صحت سنجی مویایل
    /// </summary>
    [Column("MOBILE_NO_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string MobileNoState { get; set; }

    /// <summary>
    /// وضعیت moc
    /// </summary>
    [Column("MOC_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string MocState { get; set; }

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
    [Column("NATIONAL_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    /// <summary>
    /// ردیف تابعیت
    /// </summary>
    [Column("NATIONALITY_ID")]
    [Precision(6)]
    public int? NationalityId { get; set; }

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
    [StringLength(10)]
    [Unicode(false)]
    public string PostalCode { get; set; }

    /// <summary>
    /// آیا شخص حقوقی ساختار تشكیلات دارد در سامانه ثنا
    /// </summary>
    [Column("SANA_HAS_ORGANIZATION_CHART")]
    [StringLength(1)]
    [Unicode(false)]
    public string SanaHasOrganizationChart { get; set; }

    /// <summary>
    /// تاریخ استعلام از ثنا
    /// </summary>
    [Column("SANA_INQUIRY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SanaInquiryDate { get; set; }

    /// <summary>
    /// زمان استعلام از ثنا
    /// </summary>
    [Column("SANA_INQUIRY_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SanaInquiryTime { get; set; }

    /// <summary>
    /// شماره تلفن همراه در سامانه ثنا
    /// </summary>
    [Column("SANA_MOBILE_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string SanaMobileNo { get; set; }

    /// <summary>
    /// كد شخص حقوقی در سامانه ثنا
    /// </summary>
    [Column("SANA_ORGANIZATION_CODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string SanaOrganizationCode { get; set; }

    /// <summary>
    /// نام شخص حقوقی در سامانه ثنا
    /// </summary>
    [Column("SANA_ORGANIZATION_NAME")]
    [StringLength(2000)]
    [Unicode(false)]
    public string SanaOrganizationName { get; set; }

    /// <summary>
    /// ایا در سامانه ثنا حساب كاربری دارد؟
    /// </summary>
    [Column("SANA_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SanaState { get; set; }

    /// <summary>
    /// ردیف عامل پارتیشن
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شماره سریال شناسنامه-قسمت دوم
    /// </summary>
    [Column("SERI")]
    [StringLength(5)]
    [Unicode(false)]
    public string Seri { get; set; }

    /// <summary>
    /// شماره سریال شناسنامه-قسمت اول
    /// </summary>
    [Column("SERI_ALPHA")]
    [StringLength(2)]
    [Unicode(false)]
    public string SeriAlpha { get; set; }

    /// <summary>
    /// شماره سریال شناسنامه-قسمت سوم
    /// </summary>
    [Column("SERIAL")]
    [StringLength(8)]
    [Unicode(false)]
    public string Serial { get; set; }

    /// <summary>
    /// جنسیت
    /// </summary>
    [Column("SEX_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SexType { get; set; }

    /// <summary>
    /// شماره تلفن ثابت
    /// </summary>
    [Column("TEL")]
    [StringLength(50)]
    [Unicode(false)]
    public string Tel { get; set; }

    /// <summary>
    /// رمز دو عاملی نیاز است؟
    /// </summary>
    [Column("TFA_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string TfaRequired { get; set; }

    /// <summary>
    /// وضعیت رمز دو عاملی
    /// </summary>
    [Column("TFA_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string TfaState { get; set; }

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
    /// شناسه سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// محل صدور خارجي
    /// </summary>
    [Column("FORIEGN_ISSUE_PLACE")]
    [StringLength(2000)]
    [Unicode(false)]
    public string ForiegnIssuePlace { get; set; }

    /// <summary>
    /// شناسه شهر
    /// </summary>
    [Column("CITY_ID")]
    [Precision(6)]
    public int? CityId { get; set; }

    /// <summary>
    /// شناسه محل صدور
    /// </summary>
    [Column("ISSUE_PLACE_ID")]
    [Precision(6)]
    public int? IssuePlaceId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("DOCUMENT_PERSON_ID")]
    public Guid? DocumentPersonId { get; set; }

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

    [ForeignKey("AgentTypeId")]
    [InverseProperty("EstateDocumentRequestPeople")]
    public virtual AgentType AgentType { get; set; }

    [ForeignKey("DocumentPersonId")]
    [InverseProperty("EstateDocumentRequestPeople")]
    public virtual DocumentPerson DocumentPerson { get; set; }

    [ForeignKey("DocumentRequestId")]
    [InverseProperty("EstateDocumentRequestPeople")]
    public virtual EstateDocumentRequest DocumentRequest { get; set; }

    [InverseProperty("AgentPerson")]
    public virtual ICollection<EstateDocReqPersonRelate> EstateDocReqPersonRelateAgentPeople { get; set; } = new List<EstateDocReqPersonRelate>();

    [InverseProperty("MainPerson")]
    public virtual ICollection<EstateDocReqPersonRelate> EstateDocReqPersonRelateMainPeople { get; set; } = new List<EstateDocReqPersonRelate>();
}
