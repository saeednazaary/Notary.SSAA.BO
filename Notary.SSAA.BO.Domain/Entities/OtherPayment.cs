using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سایر پرداخت های صورت گرفته در دفترخانه ها كه مربوط به مواردی نظیر اسناد، گواهی امضاء و استعلام نیستند
/// </summary>
[Table("OTHER_PAYMENTS")]
[Index("BillNo", Name = "IDX_OTHER_PAYMENTS###BILL_NO")]
[Index("CompanyName", Name = "IDX_OTHER_PAYMENTS###COMPANY_NAME")]
[Index("CreateDate", Name = "IDX_OTHER_PAYMENTS###CREATE_DATE")]
[Index("CreateTime", Name = "IDX_OTHER_PAYMENTS###CREATE_TIME")]
[Index("EstateOwnerNameFamily", Name = "IDX_OTHER_PAYMENTS###ESTATE_OWNER_NAME_FAMILY")]
[Index("EstateOwnerNationalNo", Name = "IDX_OTHER_PAYMENTS###ESTATE_OWNER_NATIONAL_NO")]
[Index("GeoLocationId", Name = "IDX_OTHER_PAYMENTS###GEO_LOCATION_ID")]
[Index("IsCostPaid", Name = "IDX_OTHER_PAYMENTS###IS_COST_PAID")]
[Index("OtherPaymentsTypeId", Name = "IDX_OTHER_PAYMENTS###OTHER_PAYMENTS_TYPE_ID")]
[Index("PayCostDate", Name = "IDX_OTHER_PAYMENTS###PAY_COST_DATE")]
[Index("PayCostTime", Name = "IDX_OTHER_PAYMENTS###PAY_COST_TIME")]
[Index("PlaqueNo", Name = "IDX_OTHER_PAYMENTS###PLAQUE_NO")]
[Index("ScriptoriumId", Name = "IDX_OTHER_PAYMENTS###SCRIPTORIUM_ID")]
[Index("UnitId", Name = "IDX_OTHER_PAYMENTS###UNIT_ID")]
[Index("LegacyId", Name = "UDX_OTHER_PAYMENTS###LEGACY_ID", IsUnique = true)]
[Index("NationalNo", Name = "UDX_OTHER_PAYMENTS###NATIONAL_NO", IsUnique = true)]
public partial class OtherPayment
{
    /// <summary>
    ///  ردیف
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شماره پرونده
    /// </summary>
    [Required]
    [Column("NATIONAL_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    /// <summary>
    /// ردیف دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شناسه ركورد موضوع پرداخت
    /// </summary>
    [Required]
    [Column("OTHER_PAYMENTS_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string OtherPaymentsTypeId { get; set; }

    /// <summary>
    /// مبلغ واحد
    /// </summary>
    [Column("FEE")]
    [Precision(15)]
    public long? Fee { get; set; }

    /// <summary>
    /// تعداد
    /// </summary>
    [Column("ITEM_COUNT")]
    [Precision(5)]
    public short? ItemCount { get; set; }

    /// <summary>
    /// كل مبلغ لازم به پرداخت
    /// </summary>
    [Column("SUM_PRICES")]
    [Precision(15)]
    public long SumPrices { get; set; }

    /// <summary>
    /// نام شركت برای استعلام ثبت شركت ها
    /// </summary>
    [Column("COMPANY_NAME")]
    [StringLength(200)]
    [Unicode(false)]
    public string CompanyName { get; set; }

    /// <summary>
    /// شماره ملی مالك (مالكین) ملك جاری
    /// </summary>
    [Column("ESTATE_OWNER_NATIONAL_NO")]
    [StringLength(200)]
    [Unicode(false)]
    public string EstateOwnerNationalNo { get; set; }

    /// <summary>
    /// نام و نام خانوادگی مالك (مالكین) ملك جاری
    /// </summary>
    [Column("ESTATE_OWNER_NAME_FAMILY")]
    [StringLength(200)]
    [Unicode(false)]
    public string EstateOwnerNameFamily { get; set; }

    /// <summary>
    /// ردیف حوزه ثبتی ملك جاری
    /// </summary>
    [Column("UNIT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string UnitId { get; set; }

    /// <summary>
    /// شماره پلاك ثبتی
    /// </summary>
    [Column("PLAQUE_NO")]
    [StringLength(500)]
    [Unicode(false)]
    public string PlaqueNo { get; set; }

    /// <summary>
    /// ردیف شهر ملك جاری
    /// </summary>
    [Column("GEO_LOCATION_ID")]
    [Precision(6)]
    public int? GeoLocationId { get; set; }

    /// <summary>
    /// نشانی ملك جاری
    /// </summary>
    [Column("ESTATE_ADDRESS")]
    [StringLength(200)]
    [Unicode(false)]
    public string EstateAddress { get; set; }

    /// <summary>
    /// آیا همه هزینه های قانونی دریافت شده است؟
    /// </summary>
    [Required]
    [Column("IS_COST_PAID")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsCostPaid { get; set; }

    /// <summary>
    /// تاریخ پرداخت
    /// </summary>
    [Column("PAY_COST_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string PayCostDate { get; set; }

    /// <summary>
    /// زمان پرداخت
    /// </summary>
    [Column("PAY_COST_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string PayCostTime { get; set; }

    /// <summary>
    /// شیوه پرداخت هزینه ها- بانك؛ابزار
    /// </summary>
    [Column("PAY_TYPE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string PayType { get; set; }

    /// <summary>
    /// شماره مرجع تراكنش یا شناسه پرداخت
    /// </summary>
    [Column("RECEIPT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string ReceiptNo { get; set; }

    /// <summary>
    /// شناسه قبض پرداخت
    /// </summary>
    [Column("BILL_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string BillNo { get; set; }

    /// <summary>
    /// شماره تراكنش
    /// </summary>
    [Column("FACTOR_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string FactorNo { get; set; }

    /// <summary>
    /// تاریخ تراكنش
    /// </summary>
    [Column("FACTOR_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FactorDate { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Description { get; set; }

    /// <summary>
    /// تاریخ ایجاد ركورد
    /// </summary>
    [Required]
    [Column("CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CreateDate { get; set; }

    /// <summary>
    /// زمان ایجاد ركورد
    /// </summary>
    [Required]
    [Column("CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string CreateTime { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("OtherPaymentsTypeId")]
    [InverseProperty("OtherPayments")]
    public virtual OtherPaymentsType OtherPaymentsType { get; set; }
}
