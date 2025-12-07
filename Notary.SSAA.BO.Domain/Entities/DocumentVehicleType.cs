using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع كلی وسایل نقلیه مندرج در اسناد رسمی مطابق با تقسیم بندی سازمان امور مالیاتی
/// </summary>
[Table("DOCUMENT_VEHICLE_TYPE")]
[Index("LegacyId", Name = "IDX_DOCUMENT_VEHICLE_TYPE###LEGACY_ID")]
[Index("MadeInIran", Name = "IDX_DOCUMENT_VEHICLE_TYPE###MADE_IN_IRAN")]
[Index("State", Name = "IDX_DOCUMENT_VEHICLE_TYPE###STATE")]
[Index("Code", Name = "UDX_DOCUMENT_VEHICLE_TYPE###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_DOCUMENT_VEHICLE_TYPE###TITLE", IsUnique = true)]
public partial class DocumentVehicleType
{
    /// <summary>
    ///  ردیف
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

    [InverseProperty("DocumentVehicleType")]
    public virtual ICollection<DocumentVehicleSystem> DocumentVehicleSystems { get; set; } = new List<DocumentVehicleSystem>();

    [InverseProperty("DocumentVehicleType")]
    public virtual ICollection<DocumentVehicleTip> DocumentVehicleTips { get; set; } = new List<DocumentVehicleTip>();

    [InverseProperty("DocumentVehicleType")]
    public virtual ICollection<DocumentVehicle> DocumentVehicles { get; set; } = new List<DocumentVehicle>();
}
