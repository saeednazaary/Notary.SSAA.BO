using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// خلاصه معامله های انتخاب شده در ثبت اسناد ملكی
/// </summary>
[Table("DOCUMENT_ESTATE_DEAL_SUMMARY_SELECTED")]
[Index("DocumentId", Name = "IDX_DOCUMENT_ESTATE_DEAL_SUMMARY_SELECTED###DOCUMENT_ID")]
[Index("EstateInquiryId", Name = "IDX_DOCUMENT_ESTATE_DEAL_SUMMARY_SELECTED###ESTATE_INQUIRY_ID")]
[Index("LegacyId", Name = "UDX_DOCUMENT_ESTATE_DEAL_SUMMARY_SELECTED###LEGACY_ID", IsUnique = true)]
public partial class DocumentEstateDealSummarySelected
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
    /// شناسه استعلام ملك
    /// </summary>
    [Column("ESTATE_INQUIRY_ID")]
    [StringLength(1000)]
    [Unicode(false)]
    public string EstateInquiryId { get; set; }

    /// <summary>
    /// اكس ام ال خلاصه معامله
    /// </summary>
    [Required]
    [Column("XML", TypeName = "CLOB")]
    public string Xml { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قبلی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_ESTATE_INQUIRY_ID")]
    [StringLength(1000)]
    [Unicode(false)]
    public string OldEstateInquiryId { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentEstateDealSummarySelecteds")]
    public virtual Document Document { get; set; }
}
