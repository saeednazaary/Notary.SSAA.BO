using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جدول ارزش وسایل نقلیه برای محاسبه مالیات نقل و انتقال
/// </summary>
[Table("DOCUMENT_VEHICLE_TAX_TABLE")]
[Index("MadeInYear", Name = "IDX_DOCUMENT_VEHICLE_TAX_TABLE###MADE_IN_YEAR")]
[Index("TaxYear", Name = "IDX_DOCUMENT_VEHICLE_TAX_TABLE###TAX_YEAR")]
[Index("DocumentVehicleTipId", "TaxYear", "MadeInYear", Name = "UDX_DOCUMENT_VEHICLE_TAX_TABLE###DOCUMENT_VEHICLE_TIP_ID###TAX_YEAR###MADE_IN_YEAR", IsUnique = true)]
[Index("LegacyId", Name = "UDX_DOCUMENT_VEHICLE_TAX_TABLE###LEGACY_ID", IsUnique = true)]
public partial class DocumentVehicleTaxTable
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه تیپ های وسایل نقلیه مندرج در اسناد رسمی مطابق با تقسیم بندی سازمان امور مالیاتی
    /// </summary>
    [Required]
    [Column("DOCUMENT_VEHICLE_TIP_ID")]
    [StringLength(8)]
    [Unicode(false)]
    public string DocumentVehicleTipId { get; set; }

    /// <summary>
    /// سال تولید وسیله نقلیه
    /// </summary>
    [Required]
    [Column("MADE_IN_YEAR")]
    [StringLength(4)]
    [Unicode(false)]
    public string MadeInYear { get; set; }

    /// <summary>
    /// سال اعلام جدول ارزش مالیاتی
    /// </summary>
    [Required]
    [Column("TAX_YEAR")]
    [StringLength(4)]
    [Unicode(false)]
    public string TaxYear { get; set; }

    /// <summary>
    /// ارزش
    /// </summary>
    [Column("PRICE")]
    [Precision(15)]
    public long Price { get; set; }

    /// <summary>
    /// مالیات نقل و انتقال
    /// </summary>
    [Column("TAX")]
    [Precision(15)]
    public long Tax { get; set; }

    /// <summary>
    /// درصد مالیات نقل و انتقال
    /// </summary>
    [Column("SALE_TAX_PERCENT", TypeName = "NUMBER(5,2)")]
    public decimal SaleTaxPercent { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("DocumentVehicleTipId")]
    [InverseProperty("DocumentVehicleTaxTables")]
    public virtual DocumentVehicleTip DocumentVehicleTip { get; set; }
}
