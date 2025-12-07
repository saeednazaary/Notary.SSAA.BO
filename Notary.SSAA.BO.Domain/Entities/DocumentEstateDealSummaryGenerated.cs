using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// خلاصه معامله های تولید شده در ثبت اسناد ملكی (به غیر از تقسیم نامه)
/// </summary>
[Table("DOCUMENT_ESTATE_DEAL_SUMMARY_GENERATED")]
[Index("CreateDate", Name = "IDX_DOCUMENT_ESTATE_DEAL_SUMMARY_GENERATED###CREATE_DATE")]
[Index("CreateTime", Name = "IDX_DOCUMENT_ESTATE_DEAL_SUMMARY_GENERATED###CREATE_TIME")]
[Index("DocumentId", Name = "IDX_DOCUMENT_ESTATE_DEAL_SUMMARY_GENERATED###DOCUMENT_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_ESTATE_DEAL_SUMMARY_GENERATED###ILM")]
[Index("IsSent", Name = "IDX_DOCUMENT_ESTATE_DEAL_SUMMARY_GENERATED###IS_SENT")]
[Index("RecordDate", Name = "IDX_DOCUMENT_ESTATE_DEAL_SUMMARY_GENERATED###RECORD_DATE")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_ESTATE_DEAL_SUMMARY_GENERATED###SCRIPTORIUM_ID")]
[Index("SendDate", Name = "IDX_DOCUMENT_ESTATE_DEAL_SUMMARY_GENERATED###SEND_DATE")]
[Index("SendTime", Name = "IDX_DOCUMENT_ESTATE_DEAL_SUMMARY_GENERATED###SEND_TIME")]
[Index("LegacyId", Name = "UDX_DOCUMENT_ESTATE_DEAL_SUMMARY_GENERATED###LEGACY_ID", IsUnique = true)]
public partial class DocumentEstateDealSummaryGenerated
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه سند
    /// </summary>
    [Column("DOCUMENT_ID")]
    public Guid DocumentId { get; set; }

    /// <summary>
    /// ایكس ام ال خلاصه معامله (های) تولید شده
    /// </summary>
    [Required]
    [Column("XML", TypeName = "CLOB")]
    public string Xml { get; set; }

    /// <summary>
    /// تعداد خلاصه معامله (های) ایجاد شده
    /// </summary>
    [Column("GENERATED_DEAL_SUMMARY_COUNT")]
    [Precision(5)]
    public short? GeneratedDealSummaryCount { get; set; }

    /// <summary>
    /// تاریخ ایجاد
    /// </summary>
    [Required]
    [Column("CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CreateDate { get; set; }

    /// <summary>
    /// زمان ایجاد
    /// </summary>
    [Required]
    [Column("CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string CreateTime { get; set; }

    /// <summary>
    /// آیا ارسال شده است؟
    /// </summary>
    [Column("IS_SENT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSent { get; set; }

    /// <summary>
    /// تعداد دفعات تلاش برای ارسال
    /// </summary>
    [Column("SEND_ATTEMPT_COUNT")]
    [Precision(5)]
    public short? SendAttemptCount { get; set; }

    /// <summary>
    /// تاریخ ارسال
    /// </summary>
    [Column("SEND_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SendDate { get; set; }

    /// <summary>
    /// زمان ارسال
    /// </summary>
    [Column("SEND_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SendTime { get; set; }

    /// <summary>
    /// آخرین پیام خطای دریافت شده هنگام ارسال
    /// </summary>
    [Column("LAST_ERROR_DESCRIPTION", TypeName = "CLOB")]
    public string LastErrorDescription { get; set; }

    /// <summary>
    /// شناسه دفترخانه
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
    /// تاریخ پرونده سند به میلادی
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentId { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentEstateDealSummaryGenerateds")]
    public virtual Document Document { get; set; }
}
