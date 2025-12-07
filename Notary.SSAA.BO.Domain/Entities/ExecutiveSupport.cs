using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه
/// </summary>
[Table("EXECUTIVE_SUPPORT")]
[Index("ChangedAddressPersonId", Name = "IDX_EXECUTIVE_SUPPORT###CHANGED_ADDRESS_PERSON_ID")]
[Index("ExecutiveCaseNo", Name = "IDX_EXECUTIVE_SUPPORT###EXECUTIVE_CASE_NO")]
[Index("ExecutiveDate", Name = "IDX_EXECUTIVE_SUPPORT###EXECUTIVE_DATE")]
[Index("ExecutiveNo", Name = "IDX_EXECUTIVE_SUPPORT###EXECUTIVE_NO")]
[Index("ExecutiveSupportTypeId", Name = "IDX_EXECUTIVE_SUPPORT###EXECUTIVE_SUPPORT_TYPE_ID")]
[Index("ExecutiveTypeId", Name = "IDX_EXECUTIVE_SUPPORT###EXECUTIVE_TYPE_ID")]
[Index("Ilm", Name = "IDX_EXECUTIVE_SUPPORT###ILM")]
[Index("InputLetterNoFromExecuteUnit", Name = "IDX_EXECUTIVE_SUPPORT###INPUT_LETTER_NO_FROM_EXECUTE_UNIT")]
[Index("IsFinalVerificationVisited", Name = "IDX_EXECUTIVE_SUPPORT###IS_FINAL_VERIFICATION_VISITED")]
[Index("IsPayCostConfirmed", Name = "IDX_EXECUTIVE_SUPPORT###IS_PAY_COST_CONFIRMED")]
[Index("OutputLetterNoFromExecuteUnit", Name = "IDX_EXECUTIVE_SUPPORT###OUTPUT_LETTER_NO_FROM_EXECUTE_UNIT")]
[Index("ReplyDate", Name = "IDX_EXECUTIVE_SUPPORT###REPLY_DATE")]
[Index("ReplyUnitId", Name = "IDX_EXECUTIVE_SUPPORT###REPLY_UNIT_ID")]
[Index("RequesterId", Name = "IDX_EXECUTIVE_SUPPORT###REQUESTER_ID")]
[Index("RequestDate", Name = "IDX_EXECUTIVE_SUPPORT###REQUEST_DATE")]
[Index("ScriptoriumId", Name = "IDX_EXECUTIVE_SUPPORT###SCRIPTORIUM_ID")]
[Index("State", Name = "IDX_EXECUTIVE_SUPPORT###STATE")]
[Index("UnitId", Name = "IDX_EXECUTIVE_SUPPORT###UNIT_ID")]
[Index("LegacyId", Name = "UDX_EXECUTIVE_SUPPORT###LEGACY_ID", IsUnique = true)]
[Index("No", Name = "UDX_EXECUTIVE_SUPPORT###NO", IsUnique = true)]
public partial class ExecutiveSupport
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
    /// متن درخواست
    /// </summary>
    [Column("REQUEST_TEXT", TypeName = "CLOB")]
    public string RequestText { get; set; }

    /// <summary>
    /// شناسه نوع اجرائیه
    /// </summary>
    [Required]
    [Column("EXECUTIVE_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string ExecutiveTypeId { get; set; }

    /// <summary>
    /// شناسه نوع خدمات تبعی اجرائیه
    /// </summary>
    [Required]
    [Column("EXECUTIVE_SUPPORT_TYPE_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ExecutiveSupportTypeId { get; set; }

    /// <summary>
    /// شناسه واحد اجرای دریافت كننده درخواست
    /// </summary>
    [Required]
    [Column("UNIT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string UnitId { get; set; }

    /// <summary>
    /// شناسه واحد اجرای پاسخ دهنده
    /// </summary>
    [Column("REPLY_UNIT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ReplyUnitId { get; set; }

    /// <summary>
    /// شماره اجرائیه
    /// </summary>
    [Column("EXECUTIVE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string ExecutiveNo { get; set; }

    /// <summary>
    /// شماره پرونده اجرائیه
    /// </summary>
    [Column("EXECUTIVE_CASE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string ExecutiveCaseNo { get; set; }

    /// <summary>
    /// كلید اصلی ركورد اجرائیه در سامانه اجرا
    /// </summary>
    [Column("EXECUTIVE_CASE_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string ExecutiveCaseId { get; set; }

    /// <summary>
    /// تاریخ اجرائیه
    /// </summary>
    [Column("EXECUTIVE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ExecutiveDate { get; set; }

    /// <summary>
    /// خلاصه اطلاعات اجرائیه
    /// </summary>
    [Column("EXECUTIVE_INFORMATION", TypeName = "CLOB")]
    public string ExecutiveInformation { get; set; }

    /// <summary>
    /// شناسه شخص درخواست كننده
    /// </summary>
    [Column("REQUESTER_ID")]
    public Guid? RequesterId { get; set; }

    /// <summary>
    /// شناسه شخصی كه آدرسش عوض می شود
    /// </summary>
    [Column("CHANGED_ADDRESS_PERSON_ID")]
    public Guid? ChangedAddressPersonId { get; set; }

    /// <summary>
    /// نوع نشانی اعلامی
    /// </summary>
    [Column("ADDRESS_TYPE")]
    [Precision(3)]
    public byte? AddressType { get; set; }

    /// <summary>
    /// نشانی اعلامی
    /// </summary>
    [Column("ADDRESS")]
    [StringLength(200)]
    [Unicode(false)]
    public string Address { get; set; }

    /// <summary>
    /// كد پستی اعلامی
    /// </summary>
    [Column("POSTAL_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string PostalCode { get; set; }

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
    [Column("FACTORD_ATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FactordAte { get; set; }

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
    /// متن پاسخ اداره اجرا
    /// </summary>
    [Column("REPLY_TEXT", TypeName = "CLOB")]
    public string ReplyText { get; set; }

    /// <summary>
    /// تاریخ پاسخ اداره اجرا
    /// </summary>
    [Column("REPLY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ReplyDate { get; set; }

    /// <summary>
    /// شماره نامه وارده از طرف اداره اجرا
    /// </summary>
    [Column("INPUT_LETTER_NO_FROM_EXECUTE_UNIT")]
    [StringLength(50)]
    [Unicode(false)]
    public string InputLetterNoFromExecuteUnit { get; set; }

    /// <summary>
    /// شماره نامه صادره از طرف اداره اجرا
    /// </summary>
    [Column("OUTPUT_LETTER_NO_FROM_EXECUTE_UNIT")]
    [StringLength(50)]
    [Unicode(false)]
    public string OutputLetterNoFromExecuteUnit { get; set; }

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
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(5)]
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

    [ForeignKey("ChangedAddressPersonId")]
    [InverseProperty("ExecutiveSupportChangedAddressPeople")]
    public virtual ExecutiveSupportPerson ChangedAddressPerson { get; set; }

    [InverseProperty("ExecutiveSupport")]
    public virtual ICollection<ExecutiveSupportAsset> ExecutiveSupportAssets { get; set; } = new List<ExecutiveSupportAsset>();

    [InverseProperty("ExecutiveSupport")]
    public virtual ICollection<ExecutiveSupportPerson> ExecutiveSupportPeople { get; set; } = new List<ExecutiveSupportPerson>();

    [InverseProperty("ExecutiveSupport")]
    public virtual ICollection<ExecutiveSupportPersonRelated> ExecutiveSupportPersonRelateds { get; set; } = new List<ExecutiveSupportPersonRelated>();

    [ForeignKey("ExecutiveSupportTypeId")]
    [InverseProperty("ExecutiveSupports")]
    public virtual ExecutiveSupportType ExecutiveSupportType { get; set; }

    [ForeignKey("ExecutiveTypeId")]
    [InverseProperty("ExecutiveSupports")]
    public virtual ExecutiveType ExecutiveType { get; set; }

    [ForeignKey("RequesterId")]
    [InverseProperty("ExecutiveSupportRequesters")]
    public virtual ExecutiveSupportPerson Requester { get; set; }

    [ForeignKey("State")]
    [InverseProperty("ExecutiveSupports")]
    public virtual WorkflowState StateNavigation { get; set; }
}
