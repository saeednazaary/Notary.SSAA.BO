using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// ریز هزینه های اسناد
/// </summary>
[Table("DOCUMENT_COST_UNCHANGED")]
[Index("CostTypeId", Name = "IDX_DOCUMENT_COST_UNCHANGED###COST_TYPE_ID")]
[Index("DocumentId", Name = "IDX_DOCUMENT_COST_UNCHANGED###DOCUMENT_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_COST_UNCHANGED###ILM")]
[Index("RecordDate", Name = "IDX_DOCUMENT_COST_UNCHANGED###RECORD_DATE")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_COST_UNCHANGED###SCRIPTORIUM_ID")]
[Index("LegacyId", Name = "UDX_DOCUMENT_COST_UNCHANGED###LEGACY_ID", IsUnique = true)]
public partial class DocumentCostUnchanged
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
    [InverseProperty("DocumentCostUnchangeds")]
    public virtual CostType CostType { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentCostUnchangeds")]
    public virtual Document Document { get; set; }
}
