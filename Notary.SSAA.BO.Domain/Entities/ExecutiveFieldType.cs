using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع اقلام اطلاعاتی در اجرا
/// </summary>
[Table("EXECUTIVE_FIELD_TYPE")]
[Index("State", Name = "IDX_EXECUTIVE_FIELD_TYPE###STATE")]
[Index("ExecutiveGeneralFieldTypeId", "Code", Name = "UDX_EXECUTIVE_FIELD_TYPE###EXECUTIVE_GENERAL_FIELD_TYPE_ID#CODE", IsUnique = true)]
[Index("ExecutiveGeneralFieldTypeId", "Title", Name = "UDX_EXECUTIVE_FIELD_TYPE###EXECUTIVE_GENERAL_FIELD_TYPE_ID#TITLE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_EXECUTIVE_FIELD_TYPE###LEGACY_ID", IsUnique = true)]
public partial class ExecutiveFieldType
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
    /// شناسه نوع كلی اقلام اطلاعاتی در اجرا
    /// </summary>
    [Required]
    [Column("EXECUTIVE_GENERAL_FIELD_TYPE_ID")]
    [StringLength(1)]
    [Unicode(false)]
    public string ExecutiveGeneralFieldTypeId { get; set; }

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

    [ForeignKey("ExecutiveGeneralFieldTypeId")]
    [InverseProperty("ExecutiveFieldTypes")]
    public virtual ExecutiveGeneralFieldType ExecutiveGeneralFieldType { get; set; }

    [InverseProperty("ExecutiveFieldType")]
    public virtual ICollection<ExecutiveWealthFieldType> ExecutiveWealthFieldTypes { get; set; } = new List<ExecutiveWealthFieldType>();
}
