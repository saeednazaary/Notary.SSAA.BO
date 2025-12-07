using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع سمت مطرح در انواع مختلف اجرائیه
/// </summary>
[Table("EXECUTIVE_TYPE_EXECUTIVE_PERSON_POST_TYPE")]
[Index("ExecutivePersonPostTypeId", Name = "IDX_EXECUTIVE_TYPE_EXECUTIVE_PERSON_POST_TYPE###EXECUTIVE_PERSON_POST_TYPE_ID")]
[Index("IsRequired", Name = "IDX_EXECUTIVE_TYPE_EXECUTIVE_PERSON_POST_TYPE###IS_REQUIRED")]
[Index("ExecutiveTypeId", "ExecutivePersonPostTypeId", Name = "UDX_EXECUTIVE_TYPE_EXECUTIVE_PERSON_POST_TYPE###EXECUTIVE_TYPE_ID#EXECUTIVE_PERSON_POST_TYPE_ID", IsUnique = true)]
public partial class ExecutiveTypeExecutivePersonPostType
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
    /// شناسه نوع سمت اشخاص در اجرائیات ثبت
    /// </summary>
    [Required]
    [Column("EXECUTIVE_PERSON_POST_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string ExecutivePersonPostTypeId { get; set; }

    /// <summary>
    /// آیا ورود اطلاعات این نوع سمت اجباری است؟
    /// </summary>
    [Required]
    [Column("IS_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRequired { get; set; }

    [ForeignKey("ExecutivePersonPostTypeId")]
    [InverseProperty("ExecutiveTypeExecutivePersonPostTypes")]
    public virtual ExecutivePersonPostType ExecutivePersonPostType { get; set; }

    [ForeignKey("ExecutiveTypeId")]
    [InverseProperty("ExecutiveTypeExecutivePersonPostTypes")]
    public virtual ExecutiveType ExecutiveType { get; set; }
}
