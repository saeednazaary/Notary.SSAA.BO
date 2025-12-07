using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// کانفيگ امضاي الکترونيک
/// </summary>
[Table("DIGITAL_SIGNATURE_CONFIGURATION")]
[Index("ConfigName", Name = "IDX_DIGITAL_SIGNATURE_CONFIGURATION###CONFIG_NAME")]
[Index("FormName", Name = "IDX_DIGITAL_SIGNATURE_CONFIGURATION###FORM_NAME")]
[Index("GetDataAsJson", Name = "IDX_DIGITAL_SIGNATURE_CONFIGURATION###GET_DATA_AS_JSON")]
[Index("JsonFormatting", Name = "IDX_DIGITAL_SIGNATURE_CONFIGURATION###JSON_FORMATTING")]
[Index("LagacyId", Name = "UDX_DIGITAL_SIGNATURE_CONFIGURATION###LEGACY_ID", IsUnique = true)]
public partial class DigitalSignatureConfiguration
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// نام فرم
    /// </summary>
    [Required]
    [Column("FORM_NAME")]
    [StringLength(500)]
    [Unicode(false)]
    public string FormName { get; set; }

    /// <summary>
    /// نام کانفيگ
    /// </summary>
    [Required]
    [Column("CONFIG_NAME")]
    [StringLength(500)]
    [Unicode(false)]
    public string ConfigName { get; set; }

    /// <summary>
    /// توصيف
    /// </summary>
    [Required]
    [Column("DESCRIPTOR", TypeName = "CLOB")]
    public string Descriptor { get; set; }

    /// <summary>
    /// ريپو وابسته
    /// </summary>
    [Required]
    [Column("RELATED_REPOSITORY")]
    [StringLength(500)]
    [Unicode(false)]
    public string RelatedRepository { get; set; }

    /// <summary>
    /// متد ريپو وابسته
    /// </summary>
    [Required]
    [Column("RELATED_REPOSITORY_METHOD")]
    [StringLength(500)]
    [Unicode(false)]
    public string RelatedRepositoryMethod { get; set; }

    /// <summary>
    /// فيلد وابسته
    /// </summary>
    [Column("RELATED_DIGITAL_SIGN_FIELD")]
    [StringLength(500)]
    [Unicode(false)]
    public string RelatedDigitalSignField { get; set; }

    /// <summary>
    /// فيلد گواهي وابسته
    /// </summary>
    [Column("RELATED_CERTIFICATE_FIELD")]
    [StringLength(500)]
    [Unicode(false)]
    public string RelatedCertificateField { get; set; }

    /// <summary>
    /// تنظيمات جيسون
    /// </summary>
    [Column("JSON_SERIALIZER_SETTINGS", TypeName = "CLOB")]
    public string JsonSerializerSettings { get; set; }

    /// <summary>
    /// فرمت جيسون
    /// </summary>
    [Column("JSON_FORMATTING")]
    [StringLength(1)]
    [Unicode(false)]
    public string JsonFormatting { get; set; }

    /// <summary>
    /// آيا بصورت جيسون گرفته شود؟
    /// </summary>
    [Column("GET_DATA_AS_JSON")]
    [StringLength(1)]
    [Unicode(false)]
    public string GetDataAsJson { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("LAGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LagacyId { get; set; }

    /// <summary>
    /// شماره نسخه
    /// </summary>
    [Column("VERSION_NO")]
    [Precision(3)]
    public byte? VersionNo { get; set; }

    /// <summary>
    /// آيا مربوط به سامانه جديد است؟
    /// </summary>
    [Column("IS_FOR_NEW_SYSTEM_ENTITY")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsForNewSystemEntity { get; set; }

    [InverseProperty("DigitalSignatureConfiguration")]
    public virtual ICollection<DigitalSignaturePropertyMapping> DigitalSignaturePropertyMappings { get; set; } = new List<DigitalSignaturePropertyMapping>();
}
