using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// تقاضانامه اجرائیه برون سپاری شده به دفترخانه
/// </summary>
[Table("EXECUTIVE_REQUEST")]
[Index("CurrencyTypeId", Name = "IDX_EXECUTIVE_REQUEST###CURRENCY_TYPE_ID")]
[Index("ExecutiveRequestOldId", Name = "IDX_EXECUTIVE_REQUEST###EXECUTIVE_REQUEST_OLD_ID")]
[Index("ExecutiveTypeId", Name = "IDX_EXECUTIVE_REQUEST###EXECUTIVE_TYPE_ID")]
[Index("Ilm", Name = "IDX_EXECUTIVE_REQUEST###ILM")]
[Index("IsCorrectiveOfAnotherReq", Name = "IDX_EXECUTIVE_REQUEST###IS_CORRECTIVE_OF_ANOTHER_REQ")]
[Index("IsFinalVerificationVisited", Name = "IDX_EXECUTIVE_REQUEST###IS_FINAL_VERIFICATION_VISITED")]
[Index("IsPayCostConfirmed", Name = "IDX_EXECUTIVE_REQUEST###IS_PAY_COST_CONFIRMED")]
[Index("RequestDate", Name = "IDX_EXECUTIVE_REQUEST###REQUEST_DATE")]
[Index("ScriptoriumId", Name = "IDX_EXECUTIVE_REQUEST###SCRIPTORIUM_ID")]
[Index("State", Name = "IDX_EXECUTIVE_REQUEST###STATE")]
[Index("UnitId", Name = "IDX_EXECUTIVE_REQUEST###UNIT_ID")]
[Index("LegacyId", Name = "UDX_EXECUTIVE_REQUEST###LEGACY_ID", IsUnique = true)]
[Index("No", Name = "UDX_EXECUTIVE_REQUEST###NO", IsUnique = true)]
public partial class ExecutiveRequest
{
    /// <summary>
    ///  ردیف
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شناسه یكتا
    /// </summary>
    [Required]
    [Column("NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string No { get; set; }

    /// <summary>
    /// تاریخ درخواست
    /// </summary>
    [Required]
    [Column("REQUEST_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string RequestDate { get; set; }

    /// <summary>
    /// نوع اجرائیه
    /// </summary>
    [Required]
    [Column("EXECUTIVE_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string ExecutiveTypeId { get; set; }

    /// <summary>
    /// شناسه واحد ثبتی دریافت كننده تقاضانامه اجرائیه
    /// </summary>
    [Column("UNIT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string UnitId { get; set; }

    /// <summary>
    /// عنوان تقاضای صدور  اجرائیه
    /// </summary>
    [Required]
    [Column("TITLE", TypeName = "CLOB")]
    public string Title { get; set; }

    /// <summary>
    /// دلیل درخواست
    /// </summary>
    [Required]
    [Column("APPLICATION_REASON", TypeName = "CLOB")]
    public string ApplicationReason { get; set; }

    /// <summary>
    /// متن حقوقی
    /// </summary>
    [Column("LEGAL_TEXT", TypeName = "CLOB")]
    public string LegalText { get; set; }

    /// <summary>
    /// مبلغ اجرائیه
    /// </summary>
    [Column("PRICE")]
    [Precision(15)]
    public long? Price { get; set; }

    /// <summary>
    /// شناسه واحدهای اندازه گیری - واحد پولی
    /// </summary>
    [Column("CURRENCY_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyTypeId { get; set; }

    /// <summary>
    /// تعداد اوراق
    /// </summary>
    [Column("PAPER_COUNT")]
    [Precision(5)]
    public short? PaperCount { get; set; }

    /// <summary>
    /// آیا هزینه های ارائه خدمت پرداخت شده است؟
    /// </summary>
    [Required]
    [Column("IS_PAY_COST_CONFIRMED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsPayCostConfirmed { get; set; }

    /// <summary>
    /// جمع هزینه های ارائه خدمت
    /// </summary>
    [Column("SUM_PRICES")]
    [Precision(10)]
    public int? SumPrices { get; set; }

    /// <summary>
    /// تاریخ پرداخت هزینه های ارائه خدمت
    /// </summary>
    [Column("PAY_COST_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string PayCostDate { get; set; }

    /// <summary>
    /// زمان پرداخت هزینه های ارائه خدمت
    /// </summary>
    [Column("PAY_COST_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string PayCostTime { get; set; }

    /// <summary>
    /// شیوه پرداخت
    /// </summary>
    [Column("HOW_TO_PAY")]
    [StringLength(100)]
    [Unicode(false)]
    public string HowToPay { get; set; }

    /// <summary>
    /// نوع پرداخت
    /// </summary>
    [Column("PAYMENT_TYPE")]
    [StringLength(100)]
    [Unicode(false)]
    public string PaymentType { get; set; }

    /// <summary>
    /// شناسه قبض
    /// </summary>
    [Column("BILL_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string BillNo { get; set; }

    /// <summary>
    /// شناسه مرجع تراكنش
    /// </summary>
    [Column("RECEIPT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string ReceiptNo { get; set; }

    /// <summary>
    /// شماره فاكتور
    /// </summary>
    [Column("FACTOR_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string FactorNo { get; set; }

    /// <summary>
    /// تاریخ فاكتور
    /// </summary>
    [Column("FACTOR_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FactorDate { get; set; }

    /// <summary>
    /// آیا این تقاضا، اصلاحیه تقاضای دیگری است؟
    /// </summary>
    [Required]
    [Column("IS_CORRECTIVE_OF_ANOTHER_REQ")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsCorrectiveOfAnotherReq { get; set; }

    /// <summary>
    /// شناسه تقاضانامه قبلی
    /// </summary>
    [Column("EXECUTIVE_REQUEST_OLD_ID")]
    public Guid? ExecutiveRequestOldId { get; set; }

    /// <summary>
    /// توضیحات دفترخانه
    /// </summary>
    [Column("DESCRIPTION_NOTARY", TypeName = "CLOB")]
    public string DescriptionNotary { get; set; }

    /// <summary>
    /// هامش اداره اجرا
    /// </summary>
    [Column("DESCRIPTION_UNIT", TypeName = "CLOB")]
    public string DescriptionUnit { get; set; }

    /// <summary>
    /// آیا وضعیت احراز هویت اشخاص با ثبت احوال و كنترل بخشنامه ها بررسی و رویت شده است؟
    /// </summary>
    [Required]
    [Column("IS_FINAL_VERIFICATION_VISITED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFinalVerificationVisited { get; set; }

    /// <summary>
    /// نام و نام خانوادگی سردفتر
    /// </summary>
    [Column("SARDAFTAR_NAME_FAMILY")]
    [StringLength(100)]
    [Unicode(false)]
    public string SardaftarNameFamily { get; set; }

    /// <summary>
    /// تاریخ تأیید سردفتر
    /// </summary>
    [Column("SARDAFTAR_CONFIRM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SardaftarConfirmDate { get; set; }

    /// <summary>
    /// زمان تأیید سردفتر
    /// </summary>
    [Column("SARDAFTAR_CONFIRM_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SardaftarConfirmTime { get; set; }

    /// <summary>
    /// شناسه گواهی امضای الكترونیك
    /// </summary>
    [Column("SIGN_CERTIFICATE_DN")]
    [StringLength(1000)]
    [Unicode(false)]
    public string SignCertificateDn { get; set; }

    /// <summary>
    /// امضای الكترونیك
    /// </summary>
    [Column("DOCUMENT_DIGITAL_SIGN")]
    [StringLength(1000)]
    [Unicode(false)]
    public string DocumentDigitalSign { get; set; }

    /// <summary>
    /// نام و نام خانوادگی آخرین اصلاح كننده
    /// </summary>
    [Required]
    [Column("MODIFIER")]
    [StringLength(100)]
    [Unicode(false)]
    public string Modifier { get; set; }

    /// <summary>
    /// تاریخ آخرین اصلاح
    /// </summary>
    [Required]
    [Column("MODIFY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ModifyDate { get; set; }

    /// <summary>
    /// زمان آخرین اصلاح
    /// </summary>
    [Required]
    [Column("MODIFY_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ModifyTime { get; set; }

    /// <summary>
    /// شناسه وضعیت گردش موضوعات اطلاعاتی
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(4)]
    [Unicode(false)]
    public string State { get; set; }

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
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("ExecutiveRequest")]
    public virtual ICollection<ExecutiveRequestBinding> ExecutiveRequestBindings { get; set; } = new List<ExecutiveRequestBinding>();

    [InverseProperty("ExecutiveRequest")]
    public virtual ICollection<ExecutiveRequestDocument> ExecutiveRequestDocuments { get; set; } = new List<ExecutiveRequestDocument>();

    [ForeignKey("ExecutiveRequestOldId")]
    [InverseProperty("InverseExecutiveRequestOld")]
    public virtual ExecutiveRequest ExecutiveRequestOld { get; set; }

    [InverseProperty("ExecutiveRequest")]
    public virtual ICollection<ExecutiveRequestPerson> ExecutiveRequestPeople { get; set; } = new List<ExecutiveRequestPerson>();

    [InverseProperty("ExecutiveRequest")]
    public virtual ICollection<ExecutiveRequestPersonRelated> ExecutiveRequestPersonRelateds { get; set; } = new List<ExecutiveRequestPersonRelated>();

    [ForeignKey("ExecutiveTypeId")]
    [InverseProperty("ExecutiveRequests")]
    public virtual ExecutiveType ExecutiveType { get; set; }

    [InverseProperty("ExecutiveRequestOld")]
    public virtual ICollection<ExecutiveRequest> InverseExecutiveRequestOld { get; set; } = new List<ExecutiveRequest>();

    [ForeignKey("State")]
    [InverseProperty("ExecutiveRequests")]
    public virtual WorkflowState StateNavigation { get; set; }
}
