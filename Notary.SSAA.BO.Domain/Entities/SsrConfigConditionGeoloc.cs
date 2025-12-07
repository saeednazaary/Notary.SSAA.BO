using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// محل هاي جغرافيايي مؤثر در تنظيمات ثبت الکترونيک اسناد رسمي
/// </summary>
[Table("SSR_CONFIG_CONDITION_GEOLOC")]
[Index("GeoLocationId", Name = "IX_SSR_CONFIG_CONDITION_GL_GID")]
[Index("SsrConfigId", "GeoLocationId", Name = "UX_SSR_CONFIG_CONDITION_GEOLOC", IsUnique = true)]
public partial class SsrConfigConditionGeoloc
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه رکورد تنظيمات ثبت الکترونيک اسناد رسمي
    /// </summary>
    [Column("SSR_CONFIG_ID")]
    public Guid SsrConfigId { get; set; }

    /// <summary>
    /// شناسه محل جغرافيايي
    /// </summary>
    [Column("GEO_LOCATION_ID")]
    [Precision(6)]
    public int GeoLocationId { get; set; }

    [ForeignKey("SsrConfigId")]
    [InverseProperty("SsrConfigConditionGeolocs")]
    public virtual SsrConfig SsrConfig { get; set; }
}
