using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// ریز هزینه های اسناد
/// </summary>
[Table("DOCUMENT_COST")]
[Index("CostTypeId", Name = "IDX_DOCUMENT_COST###COST_TYPE_ID")]
[Index("DocumentId", Name = "IDX_DOCUMENT_COST###DOCUMENT_ID")]
[Index("EpaymentMethodId", Name = "IDX_DOCUMENT_COST###EPAYMENT_METHOD_ID")]
[Index("FactorDate", Name = "IDX_DOCUMENT_COST###FACTOR_DATE")]
[Index("FactorNo", Name = "IDX_DOCUMENT_COST###FACTOR_NO")]
[Index("Ilm", Name = "IDX_DOCUMENT_COST###ILM")]
[Index("RecordDate", Name = "IDX_DOCUMENT_COST###RECORD_DATE")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_COST###SCRIPTORIUM_ID")]
[Index("LegacyId", Name = "UDX_DOCUMENT_COST###LEGACY_ID", IsUnique = true)]
public partial class DocumentCost
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
    /// شناسه نوع هزینه
    /// </summary>
    [Required]
    [Column("COST_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string CostTypeId { get; set; }

    /// <summary>
    /// مبلغ
    /// </summary>
    [Column("PRICE")]
    [Precision(15)]
    public long Price { get; set; }

    /// <summary>
    /// شناسه نوع پرداخت
    /// </summary>
    [Column("EPAYMENT_METHOD_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string EpaymentMethodId { get; set; }

    /// <summary>
    /// شماره تراكنش/فیش
    /// </summary>
    [Column("FACTOR_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string FactorNo { get; set; }

    /// <summary>
    /// تاریخ تراكنش/فیش
    /// </summary>
    [Column("FACTOR_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FactorDate { get; set; }

    /// <summary>
    /// زمان تراكنش/فیش
    /// </summary>
    [Column("FACTOR_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string FactorTime { get; set; }

    /// <summary>
    /// شماره ترمینال
    /// </summary>
    [Column("TERMINAL_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string TerminalNo { get; set; }

    /// <summary>
    /// دلیل ابراز شده برای تغییر در هزینه توسط دفترخانه
    /// </summary>
    [Column("CHANGE_REASON")]
    [StringLength(2000)]
    [Unicode(false)]
    public string ChangeReason { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Description { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده سند
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
    /// تاریخ پرونده به میلادی
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

    [ForeignKey("CostTypeId")]
    [InverseProperty("DocumentCosts")]
    public virtual CostType CostType { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentCosts")]
    public virtual Document Document { get; set; }
}
