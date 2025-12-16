using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// استعلام های ملكی مربوط به املاك ثبت شده در اسناد
/// </summary>
[Table("DOCUMENT_ESTATE_INQUIRY")]
[Index("EstateInquiryId", Name = "IDX_DOCUMENT_ESTATE_INQUIRY###ESTATE_INQUIRY_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_ESTATE_INQUIRY###ILM")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_ESTATE_INQUIRY###SCRIPTORIUM_ID")]
[Index("DocumentEstateId", "EstateInquiryId", Name = "IDX_SSR_DC_ESTT_INQ_DEID_NQID")]
[Index("LegacyId", Name = "UDX_DOCUMENT_ESTATE_INQUIRY###LEGACY_ID", IsUnique = true)]
public partial class DocumentEstateInquiry
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
    /// شناسه استعلام ملك
    /// </summary>
    [Column("ESTATE_INQUIRY_ID")]
    public Guid? EstateInquiryId { get; set; }

    /// <summary>
    /// شناسه دفترخانه
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
    /// كلید اصلی ركورد معادل در سامانه قبلی
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
    [Column("OLD_ESTATE_INQUIRY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldEstateInquiryId { get; set; }

    [ForeignKey("DocumentEstateId")]
    [InverseProperty("DocumentEstateInquiries")]
    public virtual DocumentEstate DocumentEstate { get; set; }
}
