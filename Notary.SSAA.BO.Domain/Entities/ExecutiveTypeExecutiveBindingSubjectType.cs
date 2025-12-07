using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع موضوع لازم الاجرا در اجرائیات ثبت مطرح در انواع مختلف اجرائیه
/// </summary>
[Table("EXECUTIVE_TYPE_EXECUTIVE_BINDING_SUBJECT_TYPE")]
[Index("ExecutiveBindingSubjectTypeId", Name = "IDX_EXECUTIVE_TYPE_EXECUTIVE_BINDING_SUBJECT_TYPE###EXECUTIVE_BINDING_SUBJECT_TYPE_ID")]
[Index("IsRequired", Name = "IDX_EXECUTIVE_TYPE_EXECUTIVE_BINDING_SUBJECT_TYPE###IS_REQUIRED")]
[Index("ExecutiveTypeId", "ExecutiveBindingSubjectTypeId", Name = "UDX_EXECUTIVE_TYPE_EXECUTIVE_BINDING_SUBJECT_TYPE###EXECUTIVE_TYPE_ID#EXECUTIVE_BINDING_SUBJECT_TYPE_ID", IsUnique = true)]
public partial class ExecutiveTypeExecutiveBindingSubjectType
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// شناسه نوع اجرائیه
    /// </summary>
    [Required]
    [Column("EXECUTIVE_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string ExecutiveTypeId { get; set; }

    /// <summary>
    /// شناسه نوع موضوع لازم الاجرا در اجرائیات ثبت
    /// </summary>
    [Required]
    [Column("EXECUTIVE_BINDING_SUBJECT_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string ExecutiveBindingSubjectTypeId { get; set; }

    /// <summary>
    /// آیا ورود اطلاعات این نوع موضوع لازم الاجرا اجباری است؟
    /// </summary>
    [Required]
    [Column("IS_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRequired { get; set; }

    [ForeignKey("ExecutiveBindingSubjectTypeId")]
    [InverseProperty("ExecutiveTypeExecutiveBindingSubjectTypes")]
    public virtual ExecutiveBindingSubjectType ExecutiveBindingSubjectType { get; set; }

    [ForeignKey("ExecutiveTypeId")]
    [InverseProperty("ExecutiveTypeExecutiveBindingSubjectTypes")]
    public virtual ExecutiveType ExecutiveType { get; set; }
}
