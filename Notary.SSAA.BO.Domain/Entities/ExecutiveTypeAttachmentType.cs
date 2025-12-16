using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع مستندات و پیوست ها مطرح در انواع مختلف اجرائیه
/// </summary>
[Table("EXECUTIVE_TYPE_ATTACHMENT_TYPE")]
[Index("AttachmentTypeId", Name = "IDX_EXECUTIVE_TYPE_ATTACHMENT_TYPE###ATTACHMENT_TYPE_ID")]
[Index("IsRequired", Name = "IDX_EXECUTIVE_TYPE_ATTACHMENT_TYPE###IS_REQUIRED")]
[Index("ExecutiveTypeId", "AttachmentTypeId", Name = "UDX_EXECUTIVE_TYPE_ATTACHMENT_TYPE###EXECUTIVE_TYPE_ID#ATTACHMENT_TYPE_ID", IsUnique = true)]
public partial class ExecutiveTypeAttachmentType
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
    /// شناسه نوع پیوست
    /// </summary>
    [Required]
    [Column("ATTACHMENT_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string AttachmentTypeId { get; set; }

    /// <summary>
    /// آیا ورود اطلاعات این نوع پیوست اجباری است؟
    /// </summary>
    [Required]
    [Column("IS_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRequired { get; set; }

    [ForeignKey("ExecutiveTypeId")]
    [InverseProperty("ExecutiveTypeAttachmentTypes")]
    public virtual ExecutiveType ExecutiveType { get; set; }
}
