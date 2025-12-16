using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// تیپ های وسایل نقلیه مندرج در اسناد رسمی مطابق با تقسیم بندی سازمان امور مالیاتی
/// </summary>
[Table("DOCUMENT_VEHICLE_TIP")]
[Index("CountUnitTitle", Name = "IDX_DOCUMENT_VEHICLE_TIP###COUNT_UNIT_TITLE")]
[Index("DocumentVehicleSystemId", Name = "IDX_DOCUMENT_VEHICLE_TIP###DOCUMENT_VEHICLE_SYSTEM_ID")]
[Index("DocumentVehicleTypeId", Name = "IDX_DOCUMENT_VEHICLE_TIP###DOCUMENT_VEHICLE_TYPE_ID")]
[Index("LegacyId", Name = "IDX_DOCUMENT_VEHICLE_TIP###LEGACY_ID")]
[Index("MadeInIran", Name = "IDX_DOCUMENT_VEHICLE_TIP###MADE_IN_IRAN")]
[Index("State", Name = "IDX_DOCUMENT_VEHICLE_TIP###STATE")]
[Index("MalyatiCode", Name = "IX_SSR_VEHICLETIP_MALYATICODE")]
[Index("Code", Name = "UDX_DOCUMENT_VEHICLE_TIP###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_DOCUMENT_VEHICLE_TIP###TITLE", IsUnique = true)]
public partial class DocumentVehicleTip
{
    /// <summary>
    ///  ردیف
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(10)]
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
    /// شناسه انواع سیستم های وسایل نقلیه مندرج در اسناد رسمی مطابق با تقسیم بندی سازمان امور مالیاتی
    /// </summary>
    [Required]
    [Column("DOCUMENT_VEHICLE_SYSTEM_ID")]
    [StringLength(8)]
    [Unicode(false)]
    public string DocumentVehicleSystemId { get; set; }

    /// <summary>
    /// عنوان واحد شمارش
    /// </summary>
    [Required]
    [Column("COUNT_UNIT_TITLE")]
    [StringLength(50)]
    [Unicode(false)]
    public string CountUnitTitle { get; set; }

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

    /// <summary>
    /// کد در جداول سازمان مالیاتی
    /// </summary>
    [Column("MALYATI_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string MalyatiCode { get; set; }

    [ForeignKey("DocumentVehicleSystemId")]
    [InverseProperty("DocumentVehicleTips")]
    public virtual DocumentVehicleSystem DocumentVehicleSystem { get; set; }

    [InverseProperty("DocumentVehicleTip")]
    public virtual ICollection<DocumentVehicleTaxTable> DocumentVehicleTaxTables { get; set; } = new List<DocumentVehicleTaxTable>();

    [ForeignKey("DocumentVehicleTypeId")]
    [InverseProperty("DocumentVehicleTips")]
    public virtual DocumentVehicleType DocumentVehicleType { get; set; }

    [InverseProperty("DocumentVehicleTip")]
    public virtual ICollection<DocumentVehicle> DocumentVehicles { get; set; } = new List<DocumentVehicle>();
}
