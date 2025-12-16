using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// گام هاي تراکنش
/// </summary>
[Table("TRANSACTION_STEPS")]
[Index("Name", Name = "IDX_TRANSACTION_STEPS###NAME")]
[Index("RowNo", Name = "IDX_TRANSACTION_STEPS###ROW_NO")]
[Index("State", Name = "IDX_TRANSACTION_STEPS###STATE")]
[Index("TransactionInfoId", "RowNo", Name = "SSR_UX_TRANS_STPS_TRNSID_RN", IsUnique = true)]
public partial class TransactionStep
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("TRANSACTION_INFO_ID")]
    public Guid TransactionInfoId { get; set; }

    /// <summary>
    /// رديف
    /// </summary>
    [Column("ROW_NO", TypeName = "NUMBER")]
    public decimal RowNo { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    [Required]
    [Column("NAME")]
    [StringLength(200)]
    [Unicode(false)]
    public string Name { get; set; }

    /// <summary>
    /// ورودي
    /// </summary>
    [Column("INPUT", TypeName = "CLOB")]
    public string Input { get; set; }

    /// <summary>
    /// خروجي
    /// </summary>
    [Column("OUTPUT", TypeName = "CLOB")]
    public string Output { get; set; }

    /// <summary>
    /// وضعيت
    /// </summary>
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    [ForeignKey("TransactionInfoId")]
    [InverseProperty("TransactionSteps")]
    public virtual TransactionInfo TransactionInfo { get; set; }
}
