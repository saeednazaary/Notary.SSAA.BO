using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// استعلام ماده 6 قانون الزام
/// </summary>
[Table("SSR_ARTICLE6_INQ")]
[Index("CountyId", Name = "IX_SSR_ART6_INQ_CID")]
[Index("EstateTypeId", Name = "IX_SSR_ART6_INQ_ETID")]
[Index("EstateUsingId", Name = "IX_SSR_ART6_INQ_EUID")]
[Index("ProvinceId", Name = "IX_SSR_ART6_INQ_PID")]
[Index("ScriptoriumId", Name = "IX_SSR_ART6_INQ_SCRID")]
[Index("EstateSectionId", Name = "IX_SSR_ART6_INQ_SID")]
[Index("EstateSubsectionId", Name = "IX_SSR_ART6_INQ_SSID")]
[Index("EstateUnitId", Name = "IX_SSR_ART6_INQ_UID")]
[Index("WorkflowStatesId", Name = "IX_SSR_ART6_INQ_WSID")]
public partial class SsrArticle6Inq
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شماره یكتا
    /// </summary>
    [Required]
    [Column("NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string No { get; set; }

    /// <summary>
    /// تاریخ ثبت
    /// </summary>
    [Required]
    [Column("CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CreateDate { get; set; }

    /// <summary>
    /// زمان ثبت
    /// </summary>
    [Required]
    [Column("CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string CreateTime { get; set; }

    /// <summary>
    /// نوع درخواست
    /// </summary>
    [Required]
    [Column("TYPE", TypeName = "NUMBER(1)")]
    public bool? Type { get; set; }

    /// <summary>
    /// شناسه استان
    /// </summary>
    [Required]
    [Column("PROVINCE_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string ProvinceId { get; set; }

    /// <summary>
    /// شناسه شهرستان
    /// </summary>
    [Required]
    [Column("COUNTY_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string CountyId { get; set; }

    /// <summary>
    /// نقشه ملك
    /// </summary>
    [Required]
    [Column("ESTATE_MAP", TypeName = "CLOB")]
    public string EstateMap { get; set; }

    /// <summary>
    /// شناسه واحد ثبتی مرتبط با ملك
    /// </summary>
    [Required]
    [Column("ESTATE_UNIT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string EstateUnitId { get; set; }

    /// <summary>
    /// شناسه بخش ثبتی مرتبط با ملك
    /// </summary>
    [Required]
    [Column("ESTATE_SECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSectionId { get; set; }

    /// <summary>
    /// شناسه ناحیه ثبتی مرتبط با ملك
    /// </summary>
    [Required]
    [Column("ESTATE_SUBSECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSubsectionId { get; set; }

    /// <summary>
    /// پلاك اصلی ملك
    /// </summary>
    [Required]
    [Column("ESTATE_MAIN_PLAQUE")]
    [StringLength(200)]
    [Unicode(false)]
    public string EstateMainPlaque { get; set; }

    /// <summary>
    /// پلاك فرعی ملك
    /// </summary>
    [Required]
    [Column("ESTATE_SECONDARY_PLAQUE")]
    [StringLength(200)]
    [Unicode(false)]
    public string EstateSecondaryPlaque { get; set; }

    /// <summary>
    /// مساحت ملك
    /// </summary>
    [Column("ESTATE_AREA", TypeName = "NUMBER(20,3)")]
    public decimal EstateArea { get; set; }

    /// <summary>
    /// كد پستی ملك
    /// </summary>
    [Required]
    [Column("ESTATE_POST_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstatePostCode { get; set; }

    /// <summary>
    /// فایل های پیوست
    /// </summary>
    [Column("ATTACHMENTS", TypeName = "BLOB")]
    public byte[] Attachments { get; set; }

    /// <summary>
    /// تاریخ ارسال استعلام
    /// </summary>
    [Column("SEND_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SendDate { get; set; }

    /// <summary>
    /// زمان ارسال استعلام
    /// </summary>
    [Column("SEND_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SendTime { get; set; }

    /// <summary>
    /// شناسه دفترخانه ثبت كننده
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شناسه وضعیت استعلام
    /// </summary>
    [Required]
    [Column("WORKFLOW_STATES_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string WorkflowStatesId { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// کد رهگيري
    /// </summary>
    [Column("TRACKING_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string TrackingCode { get; set; }

    /// <summary>
    /// شناسه نوع کاربری ملک مرتبط
    /// </summary>
    [Required]
    [Column("ESTATE_USING_ID")]
    [StringLength(20)]
    [Unicode(false)]
    public string EstateUsingId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("ESTATE_TYPE_ID")]
    [StringLength(20)]
    [Unicode(false)]
    public string EstateTypeId { get; set; }

    /// <summary>
    /// آدرس ملک
    /// </summary>
    [Column("ADDRESS")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Address { get; set; }

    /// <summary>
    /// شماره دفتر املاک الکترونيک سند
    /// </summary>
    [Column("ESTATE_DOC_ELECTRONIC_NOTE_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string EstateDocElectronicNoteNo { get; set; }

    /// <summary>
    /// اطلاعات ملک و مالک با سرويس فراخواني شده اند
    /// </summary>
    [Column("EST_RELATED_INFO_LOAD_BY_SVC")]
    [StringLength(1)]
    [Unicode(false)]
    public string EstRelatedInfoLoadBySvc { get; set; }

    [ForeignKey("CountyId")]
    [InverseProperty("SsrArticle6Inqs")]
    public virtual SsrArticle6County County { get; set; }

    [ForeignKey("EstateSectionId")]
    [InverseProperty("SsrArticle6Inqs")]
    public virtual EstateSection EstateSection { get; set; }

    [ForeignKey("EstateSubsectionId")]
    [InverseProperty("SsrArticle6Inqs")]
    public virtual EstateSubsection EstateSubsection { get; set; }

    [ForeignKey("EstateTypeId")]
    [InverseProperty("SsrArticle6Inqs")]
    public virtual SsrArticle6EstateType EstateType { get; set; }

    [ForeignKey("EstateUsingId")]
    [InverseProperty("SsrArticle6Inqs")]
    public virtual SsrArticle6EstateUsing EstateUsing { get; set; }

    [ForeignKey("ProvinceId")]
    [InverseProperty("SsrArticle6Inqs")]
    public virtual SsrArticle6Province Province { get; set; }

    [InverseProperty("SsrArticle6Inq")]
    public virtual ICollection<SsrArticle6InqPerson> SsrArticle6InqPeople { get; set; } = new List<SsrArticle6InqPerson>();

    [InverseProperty("SsrArticle6Inq")]
    public virtual ICollection<SsrArticle6InqReceiverOrg> SsrArticle6InqReceiverOrgs { get; set; } = new List<SsrArticle6InqReceiverOrg>();

    [InverseProperty("SsrArticle6Inq")]
    public virtual ICollection<SsrArticle6InqReceiver> SsrArticle6InqReceivers { get; set; } = new List<SsrArticle6InqReceiver>();

    [InverseProperty("SsrArticle6Inq")]
    public virtual ICollection<SsrArticle6InqResponse> SsrArticle6InqResponses { get; set; } = new List<SsrArticle6InqResponse>();

    [ForeignKey("WorkflowStatesId")]
    [InverseProperty("SsrArticle6Inqs")]
    public virtual WorkflowState WorkflowStates { get; set; }
}
