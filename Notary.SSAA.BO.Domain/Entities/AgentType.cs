using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع وابستگی اشخاص
/// </summary>
[Table("AGENT_TYPE")]
[Index("State", Name = "IDX_AGENT_TYPE###STATE")]
[Index("Code", Name = "UDX_AGENT_TYPE###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_AGENT_TYPE###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_AGENT_TYPE###TITLE", IsUnique = true)]
public partial class AgentType
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
    /// صفت
    /// </summary>
    [Required]
    [Column("ADJECTIVE")]
    [StringLength(100)]
    [Unicode(false)]
    public string Adjective { get; set; }

    /// <summary>
    /// ریشه
    /// </summary>
    [Required]
    [Column("ROOT")]
    [StringLength(100)]
    [Unicode(false)]
    public string Root { get; set; }

    /// <summary>
    /// نوع مستند
    /// </summary>
    [Required]
    [Column("DOCUMENT_TITLE")]
    [StringLength(100)]
    [Unicode(false)]
    public string DocumentTitle { get; set; }

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

    [InverseProperty("AgentType")]
    public virtual ICollection<DocumentPersonRelated> DocumentPersonRelateds { get; set; } = new List<DocumentPersonRelated>();

    [InverseProperty("AgentType")]
    public virtual ICollection<EstateDocReqPersonRelate> EstateDocReqPersonRelates { get; set; } = new List<EstateDocReqPersonRelate>();

    [InverseProperty("AgentType")]
    public virtual ICollection<EstateDocumentRequestPerson> EstateDocumentRequestPeople { get; set; } = new List<EstateDocumentRequestPerson>();

    [InverseProperty("AgentType")]
    public virtual ICollection<ExecutiveRequestPersonRelated> ExecutiveRequestPersonRelateds { get; set; } = new List<ExecutiveRequestPersonRelated>();

    [InverseProperty("AgentType")]
    public virtual ICollection<ExecutiveSupportPersonRelated> ExecutiveSupportPersonRelateds { get; set; } = new List<ExecutiveSupportPersonRelated>();

    [InverseProperty("AgentType")]
    public virtual ICollection<SignRequestPersonRelated> SignRequestPersonRelateds { get; set; } = new List<SignRequestPersonRelated>();

    [InverseProperty("AgentType")]
    public virtual ICollection<SsrConfigConditionAgntType> SsrConfigConditionAgntTypes { get; set; } = new List<SsrConfigConditionAgntType>();
}
