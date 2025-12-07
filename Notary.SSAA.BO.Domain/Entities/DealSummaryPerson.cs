using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اشخاص خلاصه معامله
/// </summary>
[Table("DEAL_SUMMARY_PERSON")]
[Index("DealSummaryId", Name = "IDX_DEAL_SUMMARY_PERSON_DEAL_SUMMARY_ID")]
[Index("LegacyId", Name = "IDX_DEAL_SUMMARY_PERSON_LEGACYID")]
[Index("RelationTypeId", Name = "IX_SSR_DLSMRY_PRSRELTYPID")]
public partial class DealSummaryPerson
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
    /// شماره/شناسه ملی
    /// </summary>
    [Column("NATIONALITY_CODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string NationalityCode { get; set; }

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
    /// متن شرط
    /// </summary>
    [Column("CONDITION_TEXT", TypeName = "CLOB")]
    public string ConditionText { get; set; }

    /// <summary>
    /// ثمنیه-ربعیه
    /// </summary>
    [Column("OCTANT_QUARTER")]
    [StringLength(2000)]
    [Unicode(false)]
    public string OctantQuarter { get; set; }

    /// <summary>
    /// جز ثمنیه-ربعیه
    /// </summary>
    [Column("OCTANT_QUARTER_PART")]
    [StringLength(50)]
    [Unicode(false)]
    public string OctantQuarterPart { get; set; }

    /// <summary>
    /// كل ثمنیه-ربعیه
    /// </summary>
    [Column("OCTANT_QUARTER_TOTAL")]
    [StringLength(50)]
    [Unicode(false)]
    public string OctantQuarterTotal { get; set; }

    /// <summary>
    /// نوع شخص
    /// </summary>
    [Column("PERSON_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string PersonType { get; set; }

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
    /// جنسیت
    /// </summary>
    [Column("SEX_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SexType { get; set; }

    /// <summary>
    /// جز سهم
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
    /// كد پستی
    /// </summary>
    [Column("POSTAL_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string PostalCode { get; set; }

    /// <summary>
    /// آدرس
    /// </summary>
    [Column("ADDRESS", TypeName = "CLOB")]
    public string Address { get; set; }

    /// <summary>
    /// ردیف محل صدور
    /// </summary>
    [Column("ISSUE_PLACE_ID")]
    [Precision(6)]
    public int? IssuePlaceId { get; set; }

    /// <summary>
    /// ردیف خلاصه معامله
    /// </summary>
    [Column("DEAL_SUMMARY_ID")]
    public Guid DealSummaryId { get; set; }

    /// <summary>
    /// ردیف محل تولد
    /// </summary>
    [Column("BIRTH_PLACE_ID")]
    [Precision(6)]
    public int? BirthPlaceId { get; set; }

    /// <summary>
    /// ردیف شهر
    /// </summary>
    [Column("CITY_ID")]
    [Precision(6)]
    public int? CityId { get; set; }

    /// <summary>
    /// ردیف تابعیت
    /// </summary>
    [Column("NATIONALITY_ID")]
    [Precision(6)]
    public int? NationalityId { get; set; }

    /// <summary>
    /// ردیف نوع ارتباط در معامله
    /// </summary>
    [Column("RELATION_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string RelationTypeId { get; set; }

    /// <summary>
    /// ردیف عامل پارتیشن
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

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
    /// بخش حرفي شماره شناسنامه
    /// </summary>
    [Column("SERI_ALPHA")]
    [StringLength(2)]
    [Unicode(false)]
    public string SeriAlpha { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("RELATED_PERSON_ID")]
    public Guid? RelatedPersonId { get; set; }

    /// <summary>
    /// آيا همان شخص استعلام است؟
    /// </summary>
    [Column("IS_INQUIRY_PERSON")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsInquiryPerson { get; set; }

    [ForeignKey("DealSummaryId")]
    [InverseProperty("DealSummaryPeople")]
    public virtual DealSummary DealSummary { get; set; }

    [ForeignKey("RelationTypeId")]
    [InverseProperty("DealSummaryPeople")]
    public virtual DealsummaryPersonRelateType RelationType { get; set; }
}
