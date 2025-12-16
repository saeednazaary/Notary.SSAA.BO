using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع سند مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي
/// </summary>
[Table("SSR_CONFIG_CONDITION_DOCTYPE")]
[Index("DocumentTypeId", Name = "IX_SSR_CONFIG_CONDTN_DCTP_DID")]
[Index("SsrConfigId", "DocumentTypeId", Name = "UX_SSR_CONFIG_CONDITION_DOCTYPE", IsUnique = true)]
public partial class SsrConfigConditionDoctype
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه رکورد تنظيمات ثبت الکترونيک اسناد رسمي
    /// </summary>
    [Column("SSR_CONFIG_ID")]
    public Guid SsrConfigId { get; set; }

    /// <summary>
    /// شناسه نوع سند
    /// </summary>
    [Required]
    [Column("DOCUMENT_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string DocumentTypeId { get; set; }

    [ForeignKey("DocumentTypeId")]
    [InverseProperty("SsrConfigConditionDoctypes")]
    public virtual DocumentType DocumentType { get; set; }

    [ForeignKey("SsrConfigId")]
    [InverseProperty("SsrConfigConditionDoctypes")]
    public virtual SsrConfig SsrConfig { get; set; }
}
