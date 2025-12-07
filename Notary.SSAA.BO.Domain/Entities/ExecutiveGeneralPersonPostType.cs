using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع كلی سمت اشخاص در اجرائیات ثبت
/// </summary>
[Table("EXECUTIVE_GENERAL_PERSON_POST_TYPE")]
[Index("State", Name = "IDX_EXECUTIVE_GENERAL_PERSON_POST_TYPE###STATE")]
[Index("Code", Name = "UDX_EXECUTIVE_GENERAL_PERSON_POST_TYPE###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_EXECUTIVE_GENERAL_PERSON_POST_TYPE###TITLE", IsUnique = true)]
public partial class ExecutiveGeneralPersonPostType
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
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    [InverseProperty("ExecutiveGeneralPersonPostType")]
    public virtual ICollection<ExecutivePersonPostType> ExecutivePersonPostTypes { get; set; } = new List<ExecutivePersonPostType>();
}
