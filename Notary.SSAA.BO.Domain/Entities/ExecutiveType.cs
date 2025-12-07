using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع اجرائیه
/// </summary>
[Table("EXECUTIVE_TYPE")]
[Index("State", Name = "IDX_EXECUTIVE_TYPE###STATE")]
[Index("Code", Name = "UDX_EXECUTIVE_TYPE###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_EXECUTIVE_TYPE###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_EXECUTIVE_TYPE###TITLE", IsUnique = true)]
public partial class ExecutiveType
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
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("ExecutiveType")]
    public virtual ICollection<DocumentInfoOther> DocumentInfoOthers { get; set; } = new List<DocumentInfoOther>();

    [InverseProperty("ExecutiveType")]
    public virtual ICollection<ExecutiveRequest> ExecutiveRequests { get; set; } = new List<ExecutiveRequest>();

    [InverseProperty("ExecutiveType")]
    public virtual ICollection<ExecutiveSupport> ExecutiveSupports { get; set; } = new List<ExecutiveSupport>();

    [InverseProperty("ExecutiveType")]
    public virtual ICollection<ExecutiveTypeAttachmentType> ExecutiveTypeAttachmentTypes { get; set; } = new List<ExecutiveTypeAttachmentType>();

    [InverseProperty("ExecutiveType")]
    public virtual ICollection<ExecutiveTypeExecutiveBindingSubjectType> ExecutiveTypeExecutiveBindingSubjectTypes { get; set; } = new List<ExecutiveTypeExecutiveBindingSubjectType>();

    [InverseProperty("ExecutiveType")]
    public virtual ICollection<ExecutiveTypeExecutivePersonPostType> ExecutiveTypeExecutivePersonPostTypes { get; set; } = new List<ExecutiveTypeExecutivePersonPostType>();
}
