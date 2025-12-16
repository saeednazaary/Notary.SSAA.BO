using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// دلیل نیاز به معتمد
/// </summary>
[Table("RELIABLE_PERSON_REASON")]
[Index("State", Name = "IDX_RELIABLE_PERSON_REASON###STATE")]
[Index("Code", Name = "UDX_RELIABLE_PERSON_REASON###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_RELIABLE_PERSON_REASON###TITLE", IsUnique = true)]
public partial class ReliablePersonReason
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(2)]
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

    [InverseProperty("ReliablePersonReason")]
    public virtual ICollection<DocumentPersonRelated> DocumentPersonRelateds { get; set; } = new List<DocumentPersonRelated>();

    [InverseProperty("ReliablePersonReason")]
    public virtual ICollection<EstateDocReqPersonRelate> EstateDocReqPersonRelates { get; set; } = new List<EstateDocReqPersonRelate>();

    [InverseProperty("ReliablePersonReason")]
    public virtual ICollection<ExecutiveRequestPersonRelated> ExecutiveRequestPersonRelateds { get; set; } = new List<ExecutiveRequestPersonRelated>();

    [InverseProperty("ReliablePersonReason")]
    public virtual ICollection<ExecutiveSupportPersonRelated> ExecutiveSupportPersonRelateds { get; set; } = new List<ExecutiveSupportPersonRelated>();

    [InverseProperty("ReliablePersonReason")]
    public virtual ICollection<SignRequestPersonRelated> SignRequestPersonRelateds { get; set; } = new List<SignRequestPersonRelated>();
}
