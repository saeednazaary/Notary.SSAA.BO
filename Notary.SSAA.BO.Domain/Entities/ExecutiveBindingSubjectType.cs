using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع موضوع لازم الاجرا در اجرائیات ثبت
/// </summary>
[Table("EXECUTIVE_BINDING_SUBJECT_TYPE")]
[Index("State", Name = "IDX_EXECUTIVE_BINDING_SUBJECT_TYPE###STATE")]
[Index("Code", Name = "UDX_EXECUTIVE_BINDING_SUBJECT_TYPE###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_EXECUTIVE_BINDING_SUBJECT_TYPE###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_EXECUTIVE_BINDING_SUBJECT_TYPE###TITLE", IsUnique = true)]
public partial class ExecutiveBindingSubjectType
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
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("ExecutiveBindingSubjectType")]
    public virtual ICollection<ExecutiveRequestBinding> ExecutiveRequestBindings { get; set; } = new List<ExecutiveRequestBinding>();

    [InverseProperty("ExecutiveBindingSubjectType")]
    public virtual ICollection<ExecutiveTypeExecutiveBindingSubjectType> ExecutiveTypeExecutiveBindingSubjectTypes { get; set; } = new List<ExecutiveTypeExecutiveBindingSubjectType>();
}
