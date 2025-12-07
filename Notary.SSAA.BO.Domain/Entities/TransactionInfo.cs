using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اطلاعات تراکنش
/// </summary>
[Table("TRANSACTION_INFO")]
[Index("Name", Name = "IDX_TRANSACTION_INFO###NAME")]
[Index("RelatedRecordId", Name = "IDX_TRANSACTION_INFO###RELATED_RECORD_ID")]
[Index("State", Name = "IDX_TRANSACTION_INFO###STATE")]
public partial class TransactionInfo
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
    [StringLength(200)]
    [Unicode(false)]
    public string Name { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("RELATED_RECORD_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string RelatedRecordId { get; set; }

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

    [InverseProperty("TransactionInfo")]
    public virtual ICollection<TransactionStep> TransactionSteps { get; set; } = new List<TransactionStep>();
}
