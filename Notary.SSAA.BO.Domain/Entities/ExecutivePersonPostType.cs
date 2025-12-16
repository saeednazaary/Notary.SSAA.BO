using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سمت اشخاص در اجرائیات ثبت
/// </summary>
[Table("EXECUTIVE_PERSON_POST_TYPE")]
[Index("State", Name = "IDX_EXECUTIVE_PERSON_POST_TYPE###STATE")]
[Index("ExecutiveGeneralPersonPostTypeId", Name = "IX_SSR_EXPRSPSTTYP_GPSTTYPID")]
[Index("Code", Name = "UDX_EXECUTIVE_PERSON_POST_TYPE###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_EXECUTIVE_PERSON_POST_TYPE###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_EXECUTIVE_PERSON_POST_TYPE###TITLE", IsUnique = true)]
public partial class ExecutivePersonPostType
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
    /// شناسه نوع كلی سمت اشخاص در اجرائیات ثبت
    /// </summary>
    [Column("EXECUTIVE_GENERAL_PERSON_POST_TYPE_ID")]
    [StringLength(1)]
    [Unicode(false)]
    public string ExecutiveGeneralPersonPostTypeId { get; set; }

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

    [ForeignKey("ExecutiveGeneralPersonPostTypeId")]
    [InverseProperty("ExecutivePersonPostTypes")]
    public virtual ExecutiveGeneralPersonPostType ExecutiveGeneralPersonPostType { get; set; }

    [InverseProperty("ExecutivePersonPostType")]
    public virtual ICollection<ExecutiveRequestPerson> ExecutiveRequestPeople { get; set; } = new List<ExecutiveRequestPerson>();

    [InverseProperty("ExecutivePersonPostType")]
    public virtual ICollection<ExecutiveSupportPerson> ExecutiveSupportPeople { get; set; } = new List<ExecutiveSupportPerson>();

    [InverseProperty("ExecutivePersonPostType")]
    public virtual ICollection<ExecutiveTypeExecutivePersonPostType> ExecutiveTypeExecutivePersonPostTypes { get; set; } = new List<ExecutiveTypeExecutivePersonPostType>();
}
