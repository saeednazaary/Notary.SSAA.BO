using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نگاشت آیتم های امضا از سامانه قدیم به جدید
/// </summary>
[Table("DIGITAL_SIGNATURE_PROPERTY_MAPPING")]
[Index("DigitalSignatureConfigurationId", Name = "IDX_DIGITAL_SIGNATURE_PROPERTY_MAPPING###CONFIGURATION_ID")]
public partial class DigitalSignaturePropertyMapping
{
    /// <summary>
    /// نوع
    /// </summary>
    [Required]
    [Column("OWNER_TYPE")]
    [StringLength(500)]
    [Unicode(false)]
    public string OwnerType { get; set; }

    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("DIGITAL_SIGNATURE_CONFIGURATION_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string DigitalSignatureConfigurationId { get; set; }

    [ForeignKey("DigitalSignatureConfigurationId")]
    [InverseProperty("DigitalSignaturePropertyMappings")]
    public virtual DigitalSignatureConfiguration DigitalSignatureConfiguration { get; set; }

    [InverseProperty("DigitalSignaturePropertyMapping")]
    public virtual ICollection<DigitalSignaturePropertyMappingDetail> DigitalSignaturePropertyMappingDetails { get; set; } = new List<DigitalSignaturePropertyMappingDetail>();
}
