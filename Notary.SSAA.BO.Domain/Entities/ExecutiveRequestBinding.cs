using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// موضوعات لازم الاجرای تقاضانامه اجرائیه برون سپاری شده به دفترخانه
/// </summary>
[Table("EXECUTIVE_REQUEST_BINDING")]
[Index("CurrencyTypeId", Name = "IDX_EXECUTIVE_REQUEST_BINDING###CURRENCY_TYPE_ID")]
[Index("DurDate", Name = "IDX_EXECUTIVE_REQUEST_BINDING###DUR_DATE")]
[Index("ExecutiveBindingSubjectTypeId", Name = "IDX_EXECUTIVE_REQUEST_BINDING###EXECUTIVE_BINDING_SUBJECT_TYPE_ID")]
[Index("ExecutiveRequestId", Name = "IDX_EXECUTIVE_REQUEST_BINDING###EXECUTIVE_REQUEST_ID")]
[Index("ExecutiveRequestScriptoriumId", Name = "IDX_EXECUTIVE_REQUEST_BINDING###EXECUTIVE_REQUEST_SCRIPTORIUM_ID")]
[Index("Ilm", Name = "IDX_EXECUTIVE_REQUEST_BINDING###ILM")]
[Index("LegacyId", Name = "UDX_EXECUTIVE_REQUEST_BINDING###LEGACY_ID", IsUnique = true)]
public partial class ExecutiveRequestBinding
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه تقاضانامه اجرائیه
    /// </summary>
    [Column("EXECUTIVE_REQUEST_ID")]
    public Guid ExecutiveRequestId { get; set; }

    /// <summary>
    /// شناسه نوع موضوع لازم الاجرا
    /// </summary>
    [Required]
    [Column("EXECUTIVE_BINDING_SUBJECT_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string ExecutiveBindingSubjectTypeId { get; set; }

    /// <summary>
    /// مبلغ
    /// </summary>
    [Column("PRICE", TypeName = "NUMBER(20)")]
    public decimal? Price { get; set; }

    /// <summary>
    /// شناسه واحد پولی
    /// </summary>
    [Column("CURRENCY_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyTypeId { get; set; }

    /// <summary>
    /// تاریخ مبنای محاسبه خسارت
    /// </summary>
    [Column("DUR_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string DurDate { get; set; }

    /// <summary>
    /// شرح
    /// </summary>
    [Column("DESCRIPTION", TypeName = "CLOB")]
    public string Description { get; set; }

    /// <summary>
    /// شناسه دفترخانه تقاضانامه اجرائیه
    /// </summary>
    [Required]
    [Column("EXECUTIVE_REQUEST_SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ExecutiveRequestScriptoriumId { get; set; }

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

    [ForeignKey("ExecutiveBindingSubjectTypeId")]
    [InverseProperty("ExecutiveRequestBindings")]
    public virtual ExecutiveBindingSubjectType ExecutiveBindingSubjectType { get; set; }

    [ForeignKey("ExecutiveRequestId")]
    [InverseProperty("ExecutiveRequestBindings")]
    public virtual ExecutiveRequest ExecutiveRequest { get; set; }
}
