using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Table("DOCUMENT_INFO_PAYMENT")]
[Index("BillNo", Name = "IDX_DOCUMENT_INFO_PAYMENT_BN")]
[Index("FactorDate", Name = "IDX_DOCUMENT_INFO_PAYMENT_FD")]
[Index("FactorNo", Name = "IDX_DOCUMENT_INFO_PAYMENT_FN")]
[Index("HowToPay", Name = "IDX_DOCUMENT_INFO_PAYMENT_HTP")]
[Index("Ilm", Name = "IDX_DOCUMENT_INFO_PAYMENT_I")]
[Index("PayDate", Name = "IDX_DOCUMENT_INFO_PAYMENT_PD")]
[Index("PayTime", Name = "IDX_DOCUMENT_INFO_PAYMENT_PTI")]
[Index("PayType", Name = "IDX_DOCUMENT_INFO_PAYMENT_PTY")]
[Index("RecordDate", Name = "IDX_DOCUMENT_INFO_PAYMENT_RD")]
[Index("RefundId", Name = "IDX_DOCUMENT_INFO_PAYMENT_RID")]
[Index("ReceiptNo", Name = "IDX_DOCUMENT_INFO_PAYMENT_RN")]
[Index("RefundState", Name = "IDX_DOCUMENT_INFO_PAYMENT_RS")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_INFO_PAYMENT_SID")]
[Index("DocumentId", Name = "UDX_DOCUMENT_INFO_PAYMENT_DID", IsUnique = true)]
public partial class DocumentInfoPayment
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    [Column("DOCUMENT_ID")]
    public Guid DocumentId { get; set; }

    [Column("HOW_TO_PAY")]
    [StringLength(1000)]
    [Unicode(false)]
    public string HowToPay { get; set; }

    [Column("FACTOR_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FactorDate { get; set; }

    [Column("FACTOR_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string FactorNo { get; set; }

    [Column("RECEIPT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string ReceiptNo { get; set; }

    [Column("PAY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string PayDate { get; set; }

    [Column("PAY_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string PayTime { get; set; }

    [Column("PAY_TYPE")]
    [StringLength(50)]
    [Unicode(false)]
    public string PayType { get; set; }

    [Column("BILL_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string BillNo { get; set; }

    [Column("REFUND_ID")]
    public Guid? RefundId { get; set; }

    [Column("REFUND_PRICE")]
    [Precision(12)]
    public long? RefundPrice { get; set; }

    [Column("REFUND_STATE")]
    [Precision(3)]
    public byte? RefundState { get; set; }

    [Column("REFUND_PRICE_HAGHOSABT_CADASTRE")]
    [Precision(12)]
    public long? RefundPriceHaghosabtCadastre { get; set; }

    [Column("REFUND_PRICE_HAGHOSABT_HALF_PERCENT")]
    [Precision(12)]
    public long? RefundPriceHaghosabtHalfPercent { get; set; }

    [Column("REFUND_DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string RefundDescription { get; set; }

    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentInfoPayment")]
    public virtual Document Document { get; set; }
}
