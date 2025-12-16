using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اطلاعات پرداخت هزینه اسناد رسمی
/// </summary>
[Table("DOCUMENT_PAYMENT")]
[Index("CostTypeId", Name = "IDX_DOCUMENT_PAYMENT###COST_TYPE_ID")]
[Index("DocumentId", Name = "IDX_DOCUMENT_PAYMENT###DOCUMENT_ID")]
[Index("HowToPay", Name = "IDX_DOCUMENT_PAYMENT###HOW_TO_PAY")]
[Index("Ilm", Name = "IDX_DOCUMENT_PAYMENT###ILM")]
[Index("IsReused", Name = "IDX_DOCUMENT_PAYMENT###IS_REUSED")]
[Index("No", Name = "IDX_DOCUMENT_PAYMENT###NO")]
[Index("PaymentDate", Name = "IDX_DOCUMENT_PAYMENT###PAYMENT_DATE")]
[Index("RecordDate", Name = "IDX_DOCUMENT_PAYMENT###RECORD_DATE")]
[Index("ReusedDocumentPaymentId", Name = "IDX_DOCUMENT_PAYMENT###REUSED_DOCUMENT_PAYMENT_ID")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_PAYMENT###SCRIPTORIUM_ID")]
[Index("LegacyId", Name = "UDX_DOCUMENT_PAYMENT###LEGACY_ID", IsUnique = true)]
public partial class DocumentPayment
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
    [Column("COST_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string CostTypeId { get; set; }

    /// <summary>
    /// شناسه یكتا قبض پرداخت
    /// </summary>
    [Required]
    [Column("NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string No { get; set; }

    /// <summary>
    /// مبلغ
    /// </summary>
    [Column("PRICE")]
    [Precision(12)]
    public long Price { get; set; }

    /// <summary>
    /// شیوه تسهیم
    /// </summary>
    [Column("HOW_TO_QUOTATION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string HowToQuotation { get; set; }

    /// <summary>
    /// شیوه پرداخت
    /// </summary>
    [Required]
    [Column("HOW_TO_PAY")]
    [StringLength(1)]
    [Unicode(false)]
    public string HowToPay { get; set; }

    /// <summary>
    /// شماره مرجع تراكنش پرداخت
    /// </summary>
    [Required]
    [Column("PAYMENT_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string PaymentNo { get; set; }

    /// <summary>
    /// تاریخ پرداخت
    /// </summary>
    [Required]
    [Column("PAYMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string PaymentDate { get; set; }

    /// <summary>
    /// زمان پرداخت
    /// </summary>
    [Column("PAYMENT_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string PaymentTime { get; set; }

    /// <summary>
    /// ابزار پرداخت الكترونیك - بانك؛ابزار
    /// </summary>
    [Required]
    [Column("PAYMENT_TYPE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string PaymentType { get; set; }

    /// <summary>
    /// كد و نام شعبه بانك محل پرداخت
    /// </summary>
    [Column("BANK_COUNTER_INFO")]
    [StringLength(200)]
    [Unicode(false)]
    public string BankCounterInfo { get; set; }

    /// <summary>
    /// شماره كارت
    /// </summary>
    [Column("CARD_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string CardNo { get; set; }

    /// <summary>
    /// آیا از پرداخت های اسناد بی اثر شده قبلی استفاده مجدد شده است؟
    /// </summary>
    [Required]
    [Column("IS_REUSED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsReused { get; set; }

    /// <summary>
    /// شناسه پرداخت استفاده شده مجدد قبلی
    /// </summary>
    [Column("REUSED_DOCUMENT_PAYMENT_ID")]
    public Guid? ReusedDocumentPaymentId { get; set; }

    /// <summary>
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

    /// <summary>
    /// Information Lifecycle Management
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه ركورد در سامانه قدیمی
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

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_REUSED_DOCUMENT_PAYMENT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldReusedDocumentPaymentId { get; set; }

    [ForeignKey("CostTypeId")]
    [InverseProperty("DocumentPayments")]
    public virtual CostType CostType { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentPayments")]
    public virtual Document Document { get; set; }

    [InverseProperty("ReusedDocumentPayment")]
    public virtual ICollection<DocumentPayment> InverseReusedDocumentPayment { get; set; } = new List<DocumentPayment>();

    [ForeignKey("ReusedDocumentPaymentId")]
    [InverseProperty("InverseReusedDocumentPayment")]
    public virtual DocumentPayment ReusedDocumentPayment { get; set; }
}
