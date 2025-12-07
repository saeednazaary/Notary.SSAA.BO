using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سطح دوم گروه‌بندی اسناد و خدمات ثبتی
/// </summary>
[Table("DOCUMENT_TYPE_GROUP2")]
[Index("DocumentTypeGroup1Id", Name = "IDX_DOCUMENT_TYPE_GROUP2###DOCUMENT_TYPE_GROUP1_ID")]
[Index("IsSupportive", Name = "IDX_DOCUMENT_TYPE_GROUP2###IS_SUPPORTIVE")]
[Index("State", Name = "IDX_DOCUMENT_TYPE_GROUP2###STATE")]
[Index("Code", Name = "UDX_DOCUMENT_TYPE_GROUP2###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_DOCUMENT_TYPE_GROUP2###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_DOCUMENT_TYPE_GROUP2###TITLE", IsUnique = true)]
public partial class DocumentTypeGroup2
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
    /// شناسه سطح اول گروه‌بندی اسناد و خدمات ثبتی
    /// </summary>
    [Required]
    [Column("DOCUMENT_TYPE_GROUP1_ID")]
    [StringLength(1)]
    [Unicode(false)]
    public string DocumentTypeGroup1Id { get; set; }

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
    /// آیا مخصوص خدمات ثبتی است؟
    /// </summary>
    [Required]
    [Column("IS_SUPPORTIVE")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSupportive { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("DocumentTypeGroup1Id")]
    [InverseProperty("DocumentTypeGroup2s")]
    public virtual DocumentTypeGroup1 DocumentTypeGroup1 { get; set; }

    [InverseProperty("DocumentTypeGroup2")]
    public virtual ICollection<DocumentType> DocumentTypes { get; set; } = new List<DocumentType>();
}
