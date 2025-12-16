using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// وسایل نقلیه ثبت شده در اسناد
/// </summary>
[Table("DOCUMENT_VEHICLE")]
[Index("CardNo", Name = "IDX_DOCUMENT_VEHICLE###CARD_NO")]
[Index("ChassisNo", Name = "IDX_DOCUMENT_VEHICLE###CHASSIS_NO")]
[Index("DocumentVehicleSystemId", Name = "IDX_DOCUMENT_VEHICLE###DOCUMENT_VEHICLE_SYSTEM_ID")]
[Index("DocumentVehicleTipId", Name = "IDX_DOCUMENT_VEHICLE###DOCUMENT_VEHICLE_TIP_ID")]
[Index("DocumentVehicleTypeId", Name = "IDX_DOCUMENT_VEHICLE###DOCUMENT_VEHICLE_TYPE_ID")]
[Index("EngineNo", Name = "IDX_DOCUMENT_VEHICLE###ENGINE_NO")]
[Index("Ilm", Name = "IDX_DOCUMENT_VEHICLE###ILM")]
[Index("IsInTaxList", Name = "IDX_DOCUMENT_VEHICLE###IS_IN_TAX_LIST")]
[Index("MadeInIran", Name = "IDX_DOCUMENT_VEHICLE###MADE_IN_IRAN")]
[Index("MadeInYear", Name = "IDX_DOCUMENT_VEHICLE###MADE_IN_YEAR")]
[Index("Model", Name = "IDX_DOCUMENT_VEHICLE###MODEL")]
[Index("OwnershipPrintedDocumentNo", Name = "IDX_DOCUMENT_VEHICLE###OWNERSHIP_PRINTED_DOCUMENT_NO")]
[Index("OwnershipType", Name = "IDX_DOCUMENT_VEHICLE###OWNERSHIP_TYPE")]
[Index("PlaqueBuyer", Name = "IDX_DOCUMENT_VEHICLE###PLAQUE_BUYER")]
[Index("PlaqueSeller", Name = "IDX_DOCUMENT_VEHICLE###PLAQUE_SELLER")]
[Index("RowNo", Name = "IDX_DOCUMENT_VEHICLE###ROW_NO")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_VEHICLE###SCRIPTORIUM_ID")]
[Index("System", Name = "IDX_DOCUMENT_VEHICLE###SYSTEM")]
[Index("Tip", Name = "IDX_DOCUMENT_VEHICLE###TIP")]
[Index("Type", Name = "IDX_DOCUMENT_VEHICLE###TYPE")]
[Index("DocumentId", "RowNo", Name = "IDX_SSR_DC_VICL_DID_RWNO")]
[Index("LegacyId", Name = "UDX_DOCUMENT_VEHICLE###LEGACY_ID", IsUnique = true)]
public partial class DocumentVehicle
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه سند
    /// </summary>
    [Column("DOCUMENT_ID")]
    public Guid DocumentId { get; set; }

    /// <summary>
    /// ردیف
    /// </summary>
    [Column("ROW_NO")]
    [Precision(5)]
    public short RowNo { get; set; }

    /// <summary>
    /// آیا این وسیله نقلیه در جدول سازمان امور مالیاتی وجود دارد؟
    /// </summary>
    [Column("IS_IN_TAX_LIST")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsInTaxList { get; set; }

    /// <summary>
    /// آیا خودرو داخلی است؟
    /// </summary>
    [Required]
    [Column("MADE_IN_IRAN")]
    [StringLength(1)]
    [Unicode(false)]
    public string MadeInIran { get; set; }

    /// <summary>
    /// شناسه نوع وسیله نقلیه
    /// </summary>
    [Column("DOCUMENT_VEHICLE_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string DocumentVehicleTypeId { get; set; }

    /// <summary>
    /// شناسه سیستم وسیله نقلیه
    /// </summary>
    [Column("DOCUMENT_VEHICLE_SYSTEM_ID")]
    [StringLength(8)]
    [Unicode(false)]
    public string DocumentVehicleSystemId { get; set; }

    /// <summary>
    /// شناسه تیپ وسیله نقلیه
    /// </summary>
    [Column("DOCUMENT_VEHICLE_TIP_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string DocumentVehicleTipId { get; set; }

    /// <summary>
    /// سال تولید
    /// </summary>
    [Column("MADE_IN_YEAR")]
    [StringLength(4)]
    [Unicode(false)]
    public string MadeInYear { get; set; }

    /// <summary>
    /// نوع
    /// </summary>
    [Column("TYPE")]
    [StringLength(200)]
    [Unicode(false)]
    public string Type { get; set; }

    /// <summary>
    /// سیستم
    /// </summary>
    [Column("SYSTEM")]
    [StringLength(200)]
    [Unicode(false)]
    public string System { get; set; }

    /// <summary>
    /// تیپ
    /// </summary>
    [Column("TIP")]
    [StringLength(200)]
    [Unicode(false)]
    public string Tip { get; set; }

    /// <summary>
    /// مدل
    /// </summary>
    [Column("MODEL")]
    [StringLength(200)]
    [Unicode(false)]
    public string Model { get; set; }

    /// <summary>
    /// شماره موتور
    /// </summary>
    [Column("ENGINE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string EngineNo { get; set; }

    /// <summary>
    /// شماره شاسی
    /// </summary>
    [Column("CHASSIS_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string ChassisNo { get; set; }

    /// <summary>
    /// حجم موتور
    /// </summary>
    [Column("ENGINE_CAPACITY")]
    [StringLength(50)]
    [Unicode(false)]
    public string EngineCapacity { get; set; }

    /// <summary>
    /// رنگ
    /// </summary>
    [Column("COLOR")]
    [StringLength(50)]
    [Unicode(false)]
    public string Color { get; set; }

    /// <summary>
    /// تعداد سیلندر
    /// </summary>
    [Column("CYLINDER_COUNT")]
    [Precision(5)]
    public short? CylinderCount { get; set; }

    /// <summary>
    /// شماره كارت وسیله نقلیه
    /// </summary>
    [Column("CARD_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string CardNo { get; set; }

    /// <summary>
    /// شماره فیش پرداخت عوارض
    /// </summary>
    [Column("DUTY_FICHE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string DutyFicheNo { get; set; }

    /// <summary>
    /// شماره كارت سوخت
    /// </summary>
    [Column("FUEL_CARD_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string FuelCardNo { get; set; }

    /// <summary>
    /// سایر مشخصات وسیله نقلیه
    /// </summary>
    [Column("OTHER_INFO")]
    [StringLength(2000)]
    [Unicode(false)]
    public string OtherInfo { get; set; }

    /// <summary>
    /// شركت بیمه كننده
    /// </summary>
    [Column("INSSURANCE_CO")]
    [StringLength(100)]
    [Unicode(false)]
    public string InssuranceCo { get; set; }

    /// <summary>
    /// شماره بیمه شخص ثالث
    /// </summary>
    [Column("INSSURANCE_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string InssuranceNo { get; set; }

    /// <summary>
    /// شماره چاپی شناسنامه مالكیت
    /// </summary>
    [Column("OWNERSHIP_PRINTED_DOCUMENT_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string OwnershipPrintedDocumentNo { get; set; }

    /// <summary>
    /// شماره سند قبلی
    /// </summary>
    [Column("OLD_DOCUMENT_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string OldDocumentNo { get; set; }

    /// <summary>
    /// تاریخ سند قبلی
    /// </summary>
    [Column("OLD_DOCUMENT_ISSUER")]
    [StringLength(200)]
    [Unicode(false)]
    public string OldDocumentIssuer { get; set; }

    /// <summary>
    /// مرجع صادركننده سند قبلی
    /// </summary>
    [Column("OLD_DOCUMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string OldDocumentDate { get; set; }

    /// <summary>
    /// آیا وسیله نقلیه شماره گذاری شده است؟
    /// </summary>
    [Column("IS_VEHICLE_NUMBERED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsVehicleNumbered { get; set; }

    /// <summary>
    /// محل شماره گذاری
    /// </summary>
    [Column("NUMBERING_LOCATION")]
    [StringLength(100)]
    [Unicode(false)]
    public string NumberingLocation { get; set; }

    /// <summary>
    /// بخش اول عددی شماره انتظامی فروشنده
    /// </summary>
    [Column("PLAQUE_NO1_SELLER")]
    [Precision(10)]
    public int? PlaqueNo1Seller { get; set; }

    /// <summary>
    /// بخش دوم عددی شماره انتظامی فروشنده
    /// </summary>
    [Column("PLAQUE_NO2_SELLER")]
    [Precision(10)]
    public int? PlaqueNo2Seller { get; set; }

    /// <summary>
    /// سری شماره انتظامی فروشنده
    /// </summary>
    [Column("PLAQUE_SERI_SELLER")]
    [Precision(10)]
    public int? PlaqueSeriSeller { get; set; }

    /// <summary>
    /// بخش حرفی شماره انتظامی فروشنده
    /// </summary>
    [Column("PLAQUE_NO_ALPHA_SELLER")]
    [StringLength(2)]
    [Unicode(false)]
    public string PlaqueNoAlphaSeller { get; set; }

    /// <summary>
    /// شماره انتظامی فروشنده
    /// </summary>
    [Column("PLAQUE_SELLER")]
    [StringLength(50)]
    [Unicode(false)]
    public string PlaqueSeller { get; set; }

    /// <summary>
    /// بخش اول عددی شماره انتظامی خریدار
    /// </summary>
    [Column("PLAQUE_NO1_BUYER")]
    [Precision(10)]
    public int? PlaqueNo1Buyer { get; set; }

    /// <summary>
    /// بخش دوم عددی شماره انتظامی خریدار
    /// </summary>
    [Column("PLAQUE_NO2_BUYER")]
    [Precision(10)]
    public int? PlaqueNo2Buyer { get; set; }

    /// <summary>
    /// سری شماره انتظامی خریدار
    /// </summary>
    [Column("PLAQUE_SERI_BUYER")]
    [Precision(10)]
    public int? PlaqueSeriBuyer { get; set; }

    /// <summary>
    /// بخش حرفی شماره انتظامی خریدار
    /// </summary>
    [Column("PLAQUE_NO_ALPHA_BUYER")]
    [StringLength(2)]
    [Unicode(false)]
    public string PlaqueNoAlphaBuyer { get; set; }

    /// <summary>
    /// شماره انتظامی خریدار
    /// </summary>
    [Column("PLAQUE_BUYER")]
    [StringLength(50)]
    [Unicode(false)]
    public string PlaqueBuyer { get; set; }

    /// <summary>
    /// مبلغ سند
    /// </summary>
    [Column("PRICE")]
    [Precision(15)]
    public long? Price { get; set; }

    /// <summary>
    /// نوع مالكیت
    /// </summary>
    [Column("OWNERSHIP_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string OwnershipType { get; set; }

    /// <summary>
    /// جزء سهم مورد مالكیت
    /// </summary>
    [Column("OWNERSHIP_DETAIL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? OwnershipDetailQuota { get; set; }

    /// <summary>
    /// كل سهم مورد مالكیت
    /// </summary>
    [Column("OWNERSHIP_TOTAL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? OwnershipTotalQuota { get; set; }

    /// <summary>
    /// جزء سهم مورد معامله
    /// </summary>
    [Column("SELL_DETAIL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? SellDetailQuota { get; set; }

    /// <summary>
    /// كل سهم مورد معامله
    /// </summary>
    [Column("SELL_TOTAL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? SellTotalQuota { get; set; }

    /// <summary>
    /// متن سهم
    /// </summary>
    [Column("QUOTA_TEXT")]
    [StringLength(2000)]
    [Unicode(false)]
    public string QuotaText { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Description { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// Information Lifecycle Management
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_VEHICLE_TYPE_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentVehicleTypeId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_VEHICLE_SYSTEM_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentVehicleSystemId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_VEHICLE_TIP_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentVehicleTipId { get; set; }

    /// <summary>
    /// آیا فروشنده پلاک انتظامی دارد؟
    /// </summary>
    [Column("HAS_SELLER_PLAQUE")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasSellerPlaque { get; set; }

    /// <summary>
    /// آیا فروشنده پلاک انتظامی خاص دارد؟
    /// </summary>
    [Column("HAS_SELLER_EXCLUSIVE_PLAQUE")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasSellerExclusivePlaque { get; set; }

    /// <summary>
    /// آیا خریدار پلاک انتظامی دارد؟
    /// </summary>
    [Column("HAS_BUYER_PLAQUE")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasBuyerPlaque { get; set; }

    /// <summary>
    /// آیا خریدار پلاک انتظامی خاص دارد؟
    /// </summary>
    [Column("HAS_BUYER_EXCLUSIVE_PLAQUE")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasBuyerExclusivePlaque { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentVehicles")]
    public virtual Document Document { get; set; }

    [InverseProperty("DocumentVehicle")]
    public virtual ICollection<DocumentVehicleQuotum> DocumentVehicleQuota { get; set; } = new List<DocumentVehicleQuotum>();

    [InverseProperty("DocumentVehicle")]
    public virtual ICollection<DocumentVehicleQuotaDetail> DocumentVehicleQuotaDetails { get; set; } = new List<DocumentVehicleQuotaDetail>();

    [ForeignKey("DocumentVehicleSystemId")]
    [InverseProperty("DocumentVehicles")]
    public virtual DocumentVehicleSystem DocumentVehicleSystem { get; set; }

    [ForeignKey("DocumentVehicleTipId")]
    [InverseProperty("DocumentVehicles")]
    public virtual DocumentVehicleTip DocumentVehicleTip { get; set; }

    [ForeignKey("DocumentVehicleTypeId")]
    [InverseProperty("DocumentVehicles")]
    public virtual DocumentVehicleType DocumentVehicleType { get; set; }
}
