using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جدول سابقه فراخوانی های سرویس تصدیق اصالت سند
/// </summary>
[Table("SSR_DOC_VERIF_CALL_LOG")]
[Index("CallDateTime", Name = "IX_SSR_VERIF_CALL_LOG_CDT")]
[Index("DocumentNo", Name = "IX_SSR_VERIF_CALL_LOG_DN")]
[Index("DocumentSecretCode", Name = "IX_SSR_VERIF_CALL_LOG_DSN")]
[Index("ExecutionTimeInMillisecond", Name = "IX_SSR_VERIF_CALL_LOG_EDT")]
[Index("ScriptoriumId", Name = "IX_SSR_VERIF_CALL_LOG_SCRID")]
[Index("State", Name = "IX_SSR_VERIF_CALL_LOG_STATE")]
[Index("UserName", Name = "IX_SSR_VERIF_CALL_LOG_UN")]
[Index("SsrDocVerifExternalUserId", Name = "IX_SSR_VER_CALL_LOG_EXT_USR_ID")]
public partial class SsrDocVerifCallLog
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شماره سند مورد نظر
    /// </summary>
    [Required]
    [Column("DOCUMENT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string DocumentNo { get; set; }

    /// <summary>
    /// رمز تصدیق سند مورد نظر
    /// </summary>
    [Required]
    [Column("DOCUMENT_SECRET_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string DocumentSecretCode { get; set; }

    /// <summary>
    /// شناسه دفترخانه مربوط به سند مورد نظر
    /// </summary>
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION", TypeName = "CLOB")]
    public string Description { get; set; }

    /// <summary>
    /// ورودی سرویس
    /// </summary>
    [Column("INPUT")]
    [Unicode(false)]
    public string Input { get; set; }

    /// <summary>
    /// خروجی سرویس
    /// </summary>
    [Column("OUTPUT", TypeName = "CLOB")]
    public string Output { get; set; }

    /// <summary>
    /// تلریخ و زمان فراخوانی
    /// </summary>
    [Required]
    [Column("CALL_DATE_TIME")]
    [StringLength(20)]
    [Unicode(false)]
    public string CallDateTime { get; set; }

    /// <summary>
    /// شناسه کاربر خارج از سامانه متقاضی تصدیق اصالت سند
    /// </summary>
    [Column("SSR_DOC_VERIF_EXTERNAL_USER_ID")]
    [StringLength(20)]
    [Unicode(false)]
    public string SsrDocVerifExternalUserId { get; set; }

    [Column("USER_NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string UserName { get; set; }

    [Column("EXECUTION_TIME_IN_MILLISECOND")]
    [StringLength(50)]
    [Unicode(false)]
    public string ExecutionTimeInMillisecond { get; set; }

    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }
}
