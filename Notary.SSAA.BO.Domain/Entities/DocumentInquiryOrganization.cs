using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سازمان های استعلام شونده در فرآیند ثبت اسناد
/// </summary>
[Table("DOCUMENT_INQUIRY_ORGANIZATION")]
[Index("IsSabt", Name = "IDX_DOCUMENT_INQUIRY_ORGANIZATION###IS_SABT")]
[Index("State", Name = "IDX_DOCUMENT_INQUIRY_ORGANIZATION###STATE")]
[Index("Code", Name = "UDX_DOCUMENT_INQUIRY_ORGANIZATION###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_DOCUMENT_INQUIRY_ORGANIZATION###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_DOCUMENT_INQUIRY_ORGANIZATION###TITLE", IsUnique = true)]
public partial class DocumentInquiryOrganization
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(3)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(100)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// آیا این سازمان از واحدهای ثبتی است؟
    /// </summary>
    [Required]
    [Column("IS_SABT")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSabt { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قبلی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("DocumentInquiryOrganization")]
    public virtual ICollection<DocumentInquiry> DocumentInquiries { get; set; } = new List<DocumentInquiry>();
}
