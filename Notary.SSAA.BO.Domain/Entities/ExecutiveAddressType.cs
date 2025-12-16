using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع نشانی اشخاص در سامانه اجرا
/// </summary>
[Table("EXECUTIVE_ADDRESS_TYPE")]
[Index("State", Name = "IDX_EXECUTIVE_ADDRESS_TYPE###STATE")]
[Index("Code", Name = "UDX_EXECUTIVE_ADDRESS_TYPE###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_EXECUTIVE_ADDRESS_TYPE###TITLE", IsUnique = true)]
public partial class ExecutiveAddressType
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

    [InverseProperty("ExecutiveAddressType")]
    public virtual ICollection<ExecutiveRequestPerson> ExecutiveRequestPeople { get; set; } = new List<ExecutiveRequestPerson>();

    [InverseProperty("ExecutiveAddressType")]
    public virtual ICollection<ExecutiveSupportPerson> ExecutiveSupportPeople { get; set; } = new List<ExecutiveSupportPerson>();
}
