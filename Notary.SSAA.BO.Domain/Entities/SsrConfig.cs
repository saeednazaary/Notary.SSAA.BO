using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// تنظيمات ثبت الکترونيک اسناد رسمي
/// </summary>
[Table("SSR_CONFIG")]
[Index("ActionType", Name = "IX_SSR_CONFIG_A")]
[Index("ConfirmDate", Name = "IX_SSR_CONFIG_CD")]
[Index("SsrConfigMainSubjectId", Name = "IX_SSR_CONFIG_CMSID")]
[Index("SsrConfigSubjectId", Name = "IX_SSR_CONFIG_CSID")]
[Index("ConfirmTime", Name = "IX_SSR_CONFIG_CT")]
[Index("ConfigStartDate", Name = "IX_SSR_CONFIG_FD")]
[Index("ConfigStartTime", Name = "IX_SSR_CONFIG_FT")]
[Index("IsConfirmed", Name = "IX_SSR_CONFIG_IC")]
[Index("ConfigEndDate", Name = "IX_SSR_CONFIG_TD")]
[Index("ConfigEndTime", Name = "IX_SSR_CONFIG_TT")]
public partial class SsrConfig
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه نوع موضوع اصلي تنظيمات ثبت الکترونيک اسناد رسمي
    /// </summary>
    [Column("SSR_CONFIG_MAIN_SUBJECT_ID")]
    public Guid SsrConfigMainSubjectId { get; set; }

    /// <summary>
    /// شناسه نوع موضوع تنظيمات ثبت الکترونيک اسناد رسمي
    /// </summary>
    [Column("SSR_CONFIG_SUBJECT_ID")]
    public Guid SsrConfigSubjectId { get; set; }

    /// <summary>
    /// مقدار تنظيم شده
    /// </summary>
    [Column("VALUE", TypeName = "CLOB")]
    public string Value { get; set; }

    /// <summary>
    /// تاريخ شروع تأثيرگذاري تنظيمات
    /// </summary>
    [Required]
    [Column("CONFIG_START_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ConfigStartDate { get; set; }

    /// <summary>
    /// زمان شروع تأثيرگذاري تنظيمات - مثال 11:00
    /// </summary>
    [Required]
    [Column("CONFIG_START_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string ConfigStartTime { get; set; }

    /// <summary>
    /// تاريخ اتمام تأثيرگذاري تنظيمات
    /// </summary>
    [Required]
    [Column("CONFIG_END_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ConfigEndDate { get; set; }

    /// <summary>
    /// زمان اتمام تأثيرگذاري تنظيمات - مثال 16:30
    /// </summary>
    [Required]
    [Column("CONFIG_END_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string ConfigEndTime { get; set; }

    /// <summary>
    /// يک-هميشه و بدون شرط انجام بشود      دو-بطور مشروط انجام بشود       سه-غيرفعال
    /// </summary>
    [Column("CONDITION_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string ConditionType { get; set; }

    /// <summary>
    /// شرط نوع سند :     0-مطرح نيست      1-براي همه      2-براي بعضي
    /// </summary>
    [Column("DOC_TYPE_CONDITION")]
    [StringLength(1)]
    [Unicode(false)]
    public string DocTypeCondition { get; set; }

    /// <summary>
    /// شرط نوع سمت اشخاص :     0-مطرح نيست      1-براي همه      2-براي بعضي
    /// </summary>
    [Column("PERSON_TYPE_CONDITION")]
    [StringLength(1)]
    [Unicode(false)]
    public string PersonTypeCondition { get; set; }

    /// <summary>
    /// شرط نوع وابستگي اشخاص :     0-مطرح نيست      1-براي همه      2-براي بعضي
    /// </summary>
    [Column("AGENT_TYPE_CONDITION")]
    [StringLength(1)]
    [Unicode(false)]
    public string AgentTypeCondition { get; set; }

    /// <summary>
    /// شرط واحد ثبتي :     0-مطرح نيست      1-براي همه      2-براي بعضي
    /// </summary>
    [Column("UNIT_CONDITION")]
    [StringLength(1)]
    [Unicode(false)]
    public string UnitCondition { get; set; }

    /// <summary>
    /// شرط دفترخانه :     0-مطرح نيست      1-براي همه      2-براي بعضي
    /// </summary>
    [Column("SCRIPTORIUM_CONDITION")]
    [StringLength(1)]
    [Unicode(false)]
    public string ScriptoriumCondition { get; set; }

    /// <summary>
    /// شرط محل جغرافيايي :     0-مطرح نيست      1-براي همه      2-براي بعضي
    /// </summary>
    [Column("GEO_CONDITION")]
    [StringLength(1)]
    [Unicode(false)]
    public string GeoCondition { get; set; }

    /// <summary>
    /// شرط نوع هزينه :     0-مطرح نيست      1-براي همه      2-براي بعضي
    /// </summary>
    [Column("COST_TYPE_CONDITION")]
    [StringLength(1)]
    [Unicode(false)]
    public string CostTypeCondition { get; set; }

    /// <summary>
    /// شرط محدوده زماني :     0-مطرح نيست      1-براي همه      2-براي بعضي
    /// </summary>
    [Column("TIME_CONDITION")]
    [StringLength(1)]
    [Unicode(false)]
    public string TimeCondition { get; set; }

    /// <summary>
    /// نوع عملکرد در قبال بروز شرايط و عدم تحقق خواسته:      0: گذر کن       1: فقط هشدار بده و گذر کن        2: از ادامه کار جلوگيري کن
    /// </summary>
    [Column("ACTION_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string ActionType { get; set; }

    /// <summary>
    /// آيا تأييد شده است؟
    /// </summary>
    [Required]
    [Column("IS_CONFIRMED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsConfirmed { get; set; }

    /// <summary>
    /// مشخصات تأييدکننده
    /// </summary>
    [Column("CONFIRMER")]
    [StringLength(200)]
    [Unicode(false)]
    public string Confirmer { get; set; }

    /// <summary>
    /// تاريخ تأييد
    /// </summary>
    [Column("CONFIRM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ConfirmDate { get; set; }

    /// <summary>
    /// زمان تأييد
    /// </summary>
    [Column("CONFIRM_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string ConfirmTime { get; set; }

    [InverseProperty("SsrConfig")]
    public virtual ICollection<SsrConfigConditionAgntType> SsrConfigConditionAgntTypes { get; set; } = new List<SsrConfigConditionAgntType>();

    [InverseProperty("SsrConfig")]
    public virtual ICollection<SsrConfigConditionCostType> SsrConfigConditionCostTypes { get; set; } = new List<SsrConfigConditionCostType>();

    [InverseProperty("SsrConfig")]
    public virtual ICollection<SsrConfigConditionDcprstp> SsrConfigConditionDcprstps { get; set; } = new List<SsrConfigConditionDcprstp>();

    [InverseProperty("SsrConfig")]
    public virtual ICollection<SsrConfigConditionDoctype> SsrConfigConditionDoctypes { get; set; } = new List<SsrConfigConditionDoctype>();

    [InverseProperty("SsrConfig")]
    public virtual ICollection<SsrConfigConditionGeoloc> SsrConfigConditionGeolocs { get; set; } = new List<SsrConfigConditionGeoloc>();

    [InverseProperty("SsrConfig")]
    public virtual ICollection<SsrConfigConditionScrptrm> SsrConfigConditionScrptrms { get; set; } = new List<SsrConfigConditionScrptrm>();

    [InverseProperty("SsrConfig")]
    public virtual ICollection<SsrConfigConditionTime> SsrConfigConditionTimes { get; set; } = new List<SsrConfigConditionTime>();

    [InverseProperty("SsrConfig")]
    public virtual ICollection<SsrConfigConditionUnit> SsrConfigConditionUnits { get; set; } = new List<SsrConfigConditionUnit>();

    [ForeignKey("SsrConfigMainSubjectId")]
    [InverseProperty("SsrConfigs")]
    public virtual SsrConfigMainSubject SsrConfigMainSubject { get; set; }

    [ForeignKey("SsrConfigSubjectId")]
    [InverseProperty("SsrConfigs")]
    public virtual SsrConfigSubject SsrConfigSubject { get; set; }
}
