using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع اقلام اطلاعاتی مربوط به انواع اموال در اجرا
/// </summary>
[Table("EXECUTIVE_WEALTH_FIELD_TYPE")]
[Index("Code", Name = "IDX_EXECUTIVE_WEALTH_FIELD_TYPE###CODE")]
[Index("ExecutiveFieldTypeId", Name = "IDX_EXECUTIVE_WEALTH_FIELD_TYPE###EXECUTIVE_FIELD_TYPE_ID")]
[Index("IsRequired", Name = "IDX_EXECUTIVE_WEALTH_FIELD_TYPE###IS_REQUIRED")]
[Index("State", Name = "IDX_EXECUTIVE_WEALTH_FIELD_TYPE###STATE")]
[Index("Title", Name = "IDX_EXECUTIVE_WEALTH_FIELD_TYPE###TITLE")]
[Index("ExecutiveWealthTypeId", "Code", Name = "UDX_EXECUTIVE_WEALTH_FIELD_TYPE###EXECUTIVE_WEALTH_TYPE_ID#CODE", IsUnique = true)]
[Index("ExecutiveWealthTypeId", "Title", Name = "UDX_EXECUTIVE_WEALTH_FIELD_TYPE###EXECUTIVE_WEALTH_TYPE_ID#TITLE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_EXECUTIVE_WEALTH_FIELD_TYPE###LEGACY_ID", IsUnique = true)]
public partial class ExecutiveWealthFieldType
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
    /// شناسه نوع اموال در اجرا
    /// </summary>
    [Required]
    [Column("EXECUTIVE_WEALTH_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string ExecutiveWealthTypeId { get; set; }

    /// <summary>
    /// شناسه نوع اقلام اطلاعاتی در اجرا
    /// </summary>
    [Required]
    [Column("EXECUTIVE_FIELD_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string ExecutiveFieldTypeId { get; set; }

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
    /// آیا اجباری است؟
    /// </summary>
    [Required]
    [Column("IS_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRequired { get; set; }

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

    [ForeignKey("ExecutiveFieldTypeId")]
    [InverseProperty("ExecutiveWealthFieldTypes")]
    public virtual ExecutiveFieldType ExecutiveFieldType { get; set; }

    [ForeignKey("ExecutiveWealthTypeId")]
    [InverseProperty("ExecutiveWealthFieldTypes")]
    public virtual ExecutiveWealthType ExecutiveWealthType { get; set; }
}
