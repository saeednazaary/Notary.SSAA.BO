using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سطح اول گروه‌بندی اسناد و خدمات ثبتی
/// </summary>
[Table("DOCUMENT_TYPE_GROUP1")]
[Index("IsSupportive", Name = "IDX_DOCUMENT_TYPE_GROUP1###IS_SUPPORTIVE")]
[Index("State", Name = "IDX_DOCUMENT_TYPE_GROUP1###STATE")]
[Index("Code", Name = "UDX_DOCUMENT_TYPE_GROUP1###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_DOCUMENT_TYPE_GROUP1###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_DOCUMENT_TYPE_GROUP1###TITLE", IsUnique = true)]
public partial class DocumentTypeGroup1
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(1)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(1)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(50)]
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

    [InverseProperty("DocumentTypeGroup1")]
    public virtual ICollection<DocumentTypeGroup2> DocumentTypeGroup2s { get; set; } = new List<DocumentTypeGroup2>();

    [InverseProperty("DocumentTypeGroup1")]
    public virtual ICollection<DocumentType> DocumentTypes { get; set; } = new List<DocumentType>();
}
