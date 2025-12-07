using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// استعلام از واحدهای ثبتی (مانند استعلام از اداره مالكیت معنوی)
/// </summary>
[Table("INQUIRY_FROM_UNIT")]
[Index("BillNo", Name = "IDX_INQUIRY_FROM_UNIT###BILL_NO")]
[Index("GincomingletterId", Name = "IDX_INQUIRY_FROM_UNIT###GINCOMINGLETTER_ID")]
[Index("GoutgoingletterId", Name = "IDX_INQUIRY_FROM_UNIT###GOUTGOINGLETTER_ID")]
[Index("InquiryDate", Name = "IDX_INQUIRY_FROM_UNIT###INQUIRY_DATE")]
[Index("InquiryFromUnitTypeId", Name = "IDX_INQUIRY_FROM_UNIT###INQUIRY_FROM_UNIT_TYPE_ID")]
[Index("IsCostPaid", Name = "IDX_INQUIRY_FROM_UNIT###IS_COST_PAID")]
[Index("IsFinalVerificationVisited", Name = "IDX_INQUIRY_FROM_UNIT###IS_FINAL_VERIFICATION_VISITED")]
[Index("ModifyDate", Name = "IDX_INQUIRY_FROM_UNIT###MODIFY_DATE")]
[Index("ModifyTime", Name = "IDX_INQUIRY_FROM_UNIT###MODIFY_TIME")]
[Index("PayCostDate", Name = "IDX_INQUIRY_FROM_UNIT###PAY_COST_DATE")]
[Index("PayCostTime", Name = "IDX_INQUIRY_FROM_UNIT###PAY_COST_TIME")]
[Index("RegisteruserhistoryId", Name = "IDX_INQUIRY_FROM_UNIT###REGISTERUSERHISTORY_ID")]
[Index("RelatedInquiryFromUnitId", Name = "IDX_INQUIRY_FROM_UNIT###RELATED_INQUIRY_FROM_UNIT_ID")]
[Index("ReplyDate", Name = "IDX_INQUIRY_FROM_UNIT###REPLY_DATE")]
[Index("SardaftarConfirmDate", Name = "IDX_INQUIRY_FROM_UNIT###SARDAFTAR_CONFIRM_DATE")]
[Index("SardaftarConfirmTime", Name = "IDX_INQUIRY_FROM_UNIT###SARDAFTAR_CONFIRM_TIME")]
[Index("ScriptoriumId", Name = "IDX_INQUIRY_FROM_UNIT###SCRIPTORIUM_ID")]
[Index("State", Name = "IDX_INQUIRY_FROM_UNIT###STATE")]
[Index("StatementNo", Name = "IDX_INQUIRY_FROM_UNIT###STATEMENT_NO")]
[Index("UnitId", Name = "IDX_INQUIRY_FROM_UNIT###UNIT_ID")]
[Index("InquiryNo", Name = "UDX_INQUIRY_FROM_UNIT###INQUIRY_NO", IsUnique = true)]
[Index("LegacyId", Name = "UDX_INQUIRY_FROM_UNIT###LEGACY_ID", IsUnique = true)]
public partial class InquiryFromUnit
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شناسه واحد ثبتی استعلام شونده
    /// </summary>
    [Required]
    [Column("UNIT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string UnitId { get; set; }

    /// <summary>
    /// شناسه نوع استعلام
    /// </summary>
    [Required]
    [Column("INQUIRY_FROM_UNIT_TYPE_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string InquiryFromUnitTypeId { get; set; }

    /// <summary>
    /// تاریخ استعلام
    /// </summary>
    [Required]
    [Column("INQUIRY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string InquiryDate { get; set; }

    /// <summary>
    /// شماره استعلام
    /// </summary>
    [Required]
    [Column("INQUIRY_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string InquiryNo { get; set; }

    /// <summary>
    /// متن استعلام
    /// </summary>
    [Required]
    [Column("INQUIRY_TEXT", TypeName = "CLOB")]
    public string InquiryText { get; set; }

    /// <summary>
    /// شماره اظهارنامه
    /// </summary>
    [Column("STATEMENT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string StatementNo { get; set; }

    /// <summary>
    /// كالا
    /// </summary>
    [Column("ITEM_DESCRIPTION", TypeName = "CLOB")]
    public string ItemDescription { get; set; }

    /// <summary>
    /// كلید اصلی پاسخ استعلام دفترخانه از واحد ثبتی
    /// </summary>
    [Column("GINCOMINGLETTER_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string GincomingletterId { get; set; }

    /// <summary>
    /// كلید اصلی استعلام دفترخانه از واحد ثبتی
    /// </summary>
    [Column("GOUTGOINGLETTER_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string GoutgoingletterId { get; set; }

    /// <summary>
    /// شناسه استعلام قبلی
    /// </summary>
    [Column("RELATED_INQUIRY_FROM_UNIT_ID")]
    public Guid? RelatedInquiryFromUnitId { get; set; }

    /// <summary>
    /// تاریخ پاسخ استعلام
    /// </summary>
    [Column("REPLY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ReplyDate { get; set; }

    /// <summary>
    /// اطلاعات پاسخ
    /// </summary>
    [Column("RESPONSE_DATA", TypeName = "CLOB")]
    public string ResponseData { get; set; }

    /// <summary>
    /// متن پاسخ
    /// </summary>
    [Column("REPLY_TEXT", TypeName = "CLOB")]
    public string ReplyText { get; set; }

    /// <summary>
    /// آیا همه هزینه های قانونی دریافت شده است؟
    /// </summary>
    [Required]
    [Column("IS_COST_PAID")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsCostPaid { get; set; }

    /// <summary>
    /// تاریخ پرداخت هزینه ها
    /// </summary>
    [Column("PAY_COST_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string PayCostDate { get; set; }

    /// <summary>
    /// زمان پرداخت هزینه ها
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
    /// تاریخ تراكنش/فیش
    /// </summary>
    [Column("FACTOR_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FactorDate { get; set; }

    /// <summary>
    /// شماره تراكنش/فیش
    /// </summary>
    [Column("FACTOR_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string FactorNo { get; set; }

    /// <summary>
    /// نحوه پرداخت
    /// </summary>
    [Column("HOW_TO_PAY")]
    [StringLength(1000)]
    [Unicode(false)]
    public string HowToPay { get; set; }

    /// <summary>
    /// شبا واریز حقوق دولتی
    /// </summary>
    [Column("SHEBA")]
    [StringLength(50)]
    [Unicode(false)]
    public string Sheba { get; set; }

    /// <summary>
    /// مبلغ لازم به پرداخت
    /// </summary>
    [Column("SUM_PRICES")]
    [Precision(15)]
    public long? SumPrices { get; set; }

    /// <summary>
    /// كد ورهوف واریز حقوق دولتی
    /// </summary>
    [Column("VERHOEFF")]
    [StringLength(50)]
    [Unicode(false)]
    public string Verhoeff { get; set; }

    /// <summary>
    /// آیا وضعیت احراز هویت اشخاص با ثبت احوال و كنترل بخشنامه ها بررسی و رویت شده است؟
    /// </summary>
    [Required]
    [Column("IS_FINAL_VERIFICATION_VISITED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFinalVerificationVisited { get; set; }

    /// <summary>
    /// تاریخ تایید سردفتر
    /// </summary>
    [Column("SARDAFTAR_CONFIRM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SardaftarConfirmDate { get; set; }

    /// <summary>
    /// زمان تایید سردفتر
    /// </summary>
    [Column("SARDAFTAR_CONFIRM_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SardaftarConfirmTime { get; set; }

    /// <summary>
    /// نام و نام خانوادگی سردفتر
    /// </summary>
    [Column("SARDAFTAR_NAME_FAMILY")]
    [StringLength(100)]
    [Unicode(false)]
    public string SardaftarNameFamily { get; set; }

    /// <summary>
    /// امضای الكترونیك سردفتر
    /// </summary>
    [Column("DOCUMENT_DIGITAL_SIGN", TypeName = "CLOB")]
    public string DocumentDigitalSign { get; set; }

    /// <summary>
    /// گواهی امضای الكترونیك مورد استفاده برای امضای سردفتر
    /// </summary>
    [Column("SIGN_CERTIFICATE_DN")]
    [StringLength(1000)]
    [Unicode(false)]
    public string SignCertificateDn { get; set; }

    /// <summary>
    /// كلید اصلی سابقه تنظیمات سمت كاربر
    /// </summary>
    [Column("REGISTERUSERHISTORY_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string RegisteruserhistoryId { get; set; }

    /// <summary>
    /// نام و نام خانوادگی آخرین اصلاح كننده
    /// </summary>
    [Required]
    [Column("MODIFIER")]
    [StringLength(100)]
    [Unicode(false)]
    public string Modifier { get; set; }

    /// <summary>
    /// تاریخ آخرین ویرایش
    /// </summary>
    [Required]
    [Column("MODIFY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ModifyDate { get; set; }

    /// <summary>
    /// زمان آخرین ویرایش
    /// </summary>
    [Required]
    [Column("MODIFY_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ModifyTime { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(4)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("InquiryFromUnit")]
    public virtual ICollection<InquiryFromUnitPerson> InquiryFromUnitPeople { get; set; } = new List<InquiryFromUnitPerson>();

    [ForeignKey("InquiryFromUnitTypeId")]
    [InverseProperty("InquiryFromUnits")]
    public virtual InquiryFromUnitType InquiryFromUnitType { get; set; }

    [InverseProperty("RelatedInquiryFromUnit")]
    public virtual ICollection<InquiryFromUnit> InverseRelatedInquiryFromUnit { get; set; } = new List<InquiryFromUnit>();

    [ForeignKey("RelatedInquiryFromUnitId")]
    [InverseProperty("InverseRelatedInquiryFromUnit")]
    public virtual InquiryFromUnit RelatedInquiryFromUnit { get; set; }
}
