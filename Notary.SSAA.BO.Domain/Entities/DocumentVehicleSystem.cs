using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سیستم های وسایل نقلیه مندرج در اسناد رسمی مطابق با تقسیم بندی سازمان امور مالیاتی
/// </summary>
[Table("DOCUMENT_VEHICLE_SYSTEM")]
[Index("DocumentVehicleTypeId", Name = "IDX_DOCUMENT_VEHICLE_SYSTEM###DOCUMENT_VEHICLE_TYPE_ID")]
[Index("LegacyId", Name = "IDX_DOCUMENT_VEHICLE_SYSTEM###LEGACY_ID")]
[Index("MadeInIran", Name = "IDX_DOCUMENT_VEHICLE_SYSTEM###MADE_IN_IRAN")]
[Index("State", Name = "IDX_DOCUMENT_VEHICLE_SYSTEM###STATE")]
[Index("Code", Name = "UDX_DOCUMENT_VEHICLE_SYSTEM###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_DOCUMENT_VEHICLE_SYSTEM###TITLE", IsUnique = true)]
public partial class DocumentVehicleSystem
{
    /// <summary>
    ///  ردیف
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(8)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(8)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(200)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// آیا تولید داخل است؟
    /// </summary>
    [Required]
    [Column("MADE_IN_IRAN")]
    [StringLength(1)]
    [Unicode(false)]
    public string MadeInIran { get; set; }

    /// <summary>
    /// شناسه انواع كلی وسایل نقلیه مندرج در اسناد رسمی مطابق با تقسیم بندی سازمان امور مالیاتی
    /// </summary>
    [Required]
    [Column("DOCUMENT_VEHICLE_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string DocumentVehicleTypeId { get; set; }

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

    [InverseProperty("DocumentVehicleSystem")]
    public virtual ICollection<DocumentVehicleTip> DocumentVehicleTips { get; set; } = new List<DocumentVehicleTip>();

    [ForeignKey("DocumentVehicleTypeId")]
    [InverseProperty("DocumentVehicleSystems")]
    public virtual DocumentVehicleType DocumentVehicleType { get; set; }

    [InverseProperty("DocumentVehicleSystem")]
    public virtual ICollection<DocumentVehicle> DocumentVehicles { get; set; } = new List<DocumentVehicle>();
}
