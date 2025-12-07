using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جزییات نگاشت آیتم های امضا از سامانه قدیم به جدید
/// </summary>
[Table("DIGITAL_SIGNATURE_PROPERTY_MAPPING_DETAIL")]
[Index("DigitalSignaturePropertyMappingId", Name = "IDX_DIGITAL_SIGNATURE_PROPERTY_MAPPING_DETAIL###MAPPING_ID")]
public partial class DigitalSignaturePropertyMappingDetail
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// نام ويژگي قبلي
    /// </summary>
    [Required]
    [Column("OLD_PROPERTY_NAME")]
    [StringLength(200)]
    [Unicode(false)]
    public string OldPropertyName { get; set; }

    /// <summary>
    /// نوع ويژگي قبلي
    /// </summary>
    [Column("OLD_PROPERTY_TYPE")]
    [StringLength(100)]
    [Unicode(false)]
    public string OldPropertyType { get; set; }

    /// <summary>
    /// نام ويژگي جديد
    /// </summary>
    [Column("NEW_PROPERTY_NAME")]
    [StringLength(200)]
    [Unicode(false)]
    public string NewPropertyName { get; set; }

    /// <summary>
    /// مسير ويژگي جديد
    /// </summary>
    [Column("NEW_PROPERTY_OWNER_PATH")]
    [StringLength(1000)]
    [Unicode(false)]
    public string NewPropertyOwnerPath { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("DIGITAL_SIGNATURE_PROPERTY_MAPPING_ID")]
    public Guid DigitalSignaturePropertyMappingId { get; set; }

    [ForeignKey("DigitalSignaturePropertyMappingId")]
    [InverseProperty("DigitalSignaturePropertyMappingDetails")]
    public virtual DigitalSignaturePropertyMapping DigitalSignaturePropertyMapping { get; set; }
}
