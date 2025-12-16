using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جزئیات سهم بندی فروشنده و خریدار از وسایل نقلیه مورد معامله در سند
/// </summary>
[Table("DOCUMENT_VEHICLE_QUOTA_DETAILS")]
[Index("DocumentPersonBuyerId", Name = "IDX_DOCUMENT_VEHICLE_QUOTA_DETAILS###DOCUMENT_PERSON_BUYER_ID")]
[Index("DocumentPersonSellerId", Name = "IDX_DOCUMENT_VEHICLE_QUOTA_DETAILS###DOCUMENT_PERSON_SELLER_ID")]
[Index("DocumentVehicleId", Name = "IDX_DOCUMENT_VEHICLE_QUOTA_DETAILS###DOCUMENT_VEHICLE_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_VEHICLE_QUOTA_DETAILS###ILM")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_VEHICLE_QUOTA_DETAILS###SCRIPTORIUM_ID")]
[Index("LegacyId", Name = "UDX_DOCUMENT_VEHICLE_QUOTA_DETAILS###LEGACY_ID", IsUnique = true)]
public partial class DocumentVehicleQuotaDetail
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه وسایل نقلیه ثبت شده در سند
    /// </summary>
    [Column("DOCUMENT_VEHICLE_ID")]
    public Guid DocumentVehicleId { get; set; }

    /// <summary>
    /// شناسه شخص فروشنده در سند
    /// </summary>
    [Column("DOCUMENT_PERSON_SELLER_ID")]
    public Guid? DocumentPersonSellerId { get; set; }

    /// <summary>
    /// شناسه شخص خریدار در سند
    /// </summary>
    [Column("DOCUMENT_PERSON_BUYER_ID")]
    public Guid? DocumentPersonBuyerId { get; set; }

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
    /// متن سهم مورد معامله
    /// </summary>
    [Column("QUOTA_TEXT")]
    [StringLength(2000)]
    [Unicode(false)]
    public string QuotaText { get; set; }

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
    [Column("OLD_DOCUMENT_VEHICLE_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentVehicleId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_PERSON_SELLER_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentPersonSellerId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_PERSON_BUYER_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentPersonBuyerId { get; set; }

    [ForeignKey("DocumentPersonBuyerId")]
    [InverseProperty("DocumentVehicleQuotaDetailDocumentPersonBuyers")]
    public virtual DocumentPerson DocumentPersonBuyer { get; set; }

    [ForeignKey("DocumentPersonSellerId")]
    [InverseProperty("DocumentVehicleQuotaDetailDocumentPersonSellers")]
    public virtual DocumentPerson DocumentPersonSeller { get; set; }

    [ForeignKey("DocumentVehicleId")]
    [InverseProperty("DocumentVehicleQuotaDetails")]
    public virtual DocumentVehicle DocumentVehicle { get; set; }
}
