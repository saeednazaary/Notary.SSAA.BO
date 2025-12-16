using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سهم اصحاب سند از املاك مورد معامله در سند
/// </summary>
[Table("DOCUMENT_ESTATE_QUOTA")]
[Index("DocumentPersonId", Name = "IDX_DOCUMENT_ESTATE_QUOTA###DOCUMENT_PERSON_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_ESTATE_QUOTA###ILM")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_ESTATE_QUOTA###SCRIPTORIUM_ID")]
[Index("DocumentEstateId", "DocumentPersonId", Name = "IDX_SSR_DC_ESTQTA_DEID_PRSID")]
[Index("LegacyId", Name = "UDX_DOCUMENT_ESTATE_QUOTA###LEGACY_ID", IsUnique = true)]
public partial class DocumentEstateQuotum
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه املاك ثبت شده در اسناد
    /// </summary>
    [Column("DOCUMENT_ESTATE_ID")]
    public Guid DocumentEstateId { get; set; }

    /// <summary>
    /// شناسه اشخاص اسناد
    /// </summary>
    [Column("DOCUMENT_PERSON_ID")]
    public Guid DocumentPersonId { get; set; }

    /// <summary>
    /// جزء سهم مورد معامله
    /// </summary>
    [Column("DETAIL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? DetailQuota { get; set; }

    /// <summary>
    /// كل سهم مورد معامله
    /// </summary>
    [Column("TOTAL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? TotalQuota { get; set; }

    /// <summary>
    /// متن سهم
    /// </summary>
    [Column("QUOTA_TEXT")]
    [StringLength(200)]
    [Unicode(false)]
    public string QuotaText { get; set; }

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
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ESTATE_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentEstateId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_PERSON_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentPersonId { get; set; }

    [ForeignKey("DocumentEstateId")]
    [InverseProperty("DocumentEstateQuota")]
    public virtual DocumentEstate DocumentEstate { get; set; }

    [ForeignKey("DocumentPersonId")]
    [InverseProperty("DocumentEstateQuota")]
    public virtual DocumentPerson DocumentPerson { get; set; }
}
