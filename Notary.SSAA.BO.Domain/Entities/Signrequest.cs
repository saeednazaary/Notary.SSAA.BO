using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// گواهی امضاء
/// </summary>
[Table("SIGN_REQUEST")]
[Index("ConfirmDate", Name = "IDX_SIGN_REQUEST###CONFIRM_DATE")]
[Index("ConfirmTime", Name = "IDX_SIGN_REQUEST###CONFIRM_TIME")]
[Index("Ilm", Name = "IDX_SIGN_REQUEST###ILM")]
[Index("IsCostPaid", Name = "IDX_SIGN_REQUEST###IS_COST_PAID")]
[Index("IsFinalizeInProgress", Name = "IDX_SIGN_REQUEST###IS_FINALIZE_IN_PROGRESS")]
[Index("PaymentType", Name = "IDX_SIGN_REQUEST###PAYMENT_TYPE")]
[Index("PayCostDate", Name = "IDX_SIGN_REQUEST###PAY_COST_DATE")]
[Index("RecordDate", Name = "IDX_SIGN_REQUEST###RECORD_DATE")]
[Index("ReqDate", Name = "IDX_SIGN_REQUEST###REQ_DATE")]
[Index("ReqTime", Name = "IDX_SIGN_REQUEST###REQ_TIME")]
[Index("ScriptoriumId", Name = "IDX_SIGN_REQUEST###SCRIPTORIUM_ID")]
[Index("SecretCode", Name = "IDX_SIGN_REQUEST###SECRET_CODE")]
[Index("SignDate", Name = "IDX_SIGN_REQUEST###SIGN_DATE")]
[Index("SignRequestGetterId", Name = "IDX_SIGN_REQUEST###SIGN_REQUEST_GETTER_ID")]
[Index("SignRequestSubjectId", Name = "IDX_SIGN_REQUEST###SIGN_REQUEST_SUBJECT_ID")]
[Index("State", Name = "IDX_SIGN_REQUEST###STATE")]
[Index("IsReadyToPay", Name = "IX_SSR_SIGNREQ_ISRDY2PAY")]
[Index("IsRemoteRequest", Name = "IX_SSR_SIGN_REQUEST_ISREMREQ")]
[Index("RemoteRequestId", Name = "IX_SSR_SIGN_REQUEST_REMREQID")]
[Index("LegacyId", Name = "UDX_SIGN_REQUEST###LEGACY_ID", IsUnique = true)]
[Index("NationalNo", Name = "UDX_SIGN_REQUEST###NATIONAL_NO", IsUnique = true)]
[Index("ReqNo", Name = "UDX_SIGN_REQUEST###REQ_NO", IsUnique = true)]
public partial class SignRequest
{
    /// <summary>
    ///  شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شماره درخواست
    /// </summary>
    [Required]
    [Column("REQ_NO")]
    [StringLength(18)]
    [Unicode(false)]
    public string ReqNo { get; set; }

    /// <summary>
    /// تاریخ درخواست
    /// </summary>
    [Required]
    [Column("REQ_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ReqDate { get; set; }

    /// <summary>
    /// زمان درخواست
    /// </summary>
    [Column("REQ_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ReqTime { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شناسه ارگان دریافت كننده گواهی امضاء
    /// </summary>
    [Column("SIGN_REQUEST_GETTER_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string SignRequestGetterId { get; set; }

    /// <summary>
    /// شناسه موضوع گواهی امضاء
    /// </summary>
    [Required]
    [Column("SIGN_REQUEST_SUBJECT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string SignRequestSubjectId { get; set; }

    /// <summary>
    /// متن گواهی امضاء
    /// </summary>
    [Column("SIGN_TEXT", TypeName = "CLOB")]
    public string SignText { get; set; }

    /// <summary>
    /// آیا هزینه های گواهی امضاء پرداخت شده است؟
    /// </summary>
    [Column("IS_COST_PAID")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsCostPaid { get; set; }

    /// <summary>
    /// جمع هزینه های ارائه خدمت
    /// </summary>
    [Column("SUM_PRICES")]
    [Precision(15)]
    public long? SumPrices { get; set; }

    /// <summary>
    /// شناسه مرجع تراكنش
    /// </summary>
    [Column("RECEIPT_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string ReceiptNo { get; set; }

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
    [StringLength(50)]
    [Unicode(false)]
    public string PayCostTime { get; set; }

    /// <summary>
    /// نوع پرداخت
    /// </summary>
    [Column("PAYMENT_TYPE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string PaymentType { get; set; }

    /// <summary>
    /// شیوه پردخت
    /// </summary>
    [Column("HOW_TO_PAY")]
    [StringLength(1)]
    [Unicode(false)]
    public string HowToPay { get; set; }

    /// <summary>
    /// شناسه قبض
    /// </summary>
    [Column("BILL_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string BillNo { get; set; }

    /// <summary>
    /// شناسه یكتا گواهی امضاء
    /// </summary>
    [Column("NATIONAL_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    /// <summary>
    /// رمز تصدیق گواهی امضاء
    /// </summary>
    [Column("SECRET_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SecretCode { get; set; }

    /// <summary>
    /// تاریخ گواهی امضاء
    /// </summary>
    [Column("SIGN_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SignDate { get; set; }

    /// <summary>
    /// زمان گواهی امضاء
    /// </summary>
    [Column("SIGN_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SignTime { get; set; }

    /// <summary>
    /// نام و نام خانوادگی آخرین اصلاح كننده
    /// </summary>
    [Column("MODIFIER")]
    [StringLength(150)]
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
    /// تأییدكننده گواهی امضاء
    /// </summary>
    [Column("CONFIRMER")]
    [StringLength(150)]
    [Unicode(false)]
    public string Confirmer { get; set; }

    /// <summary>
    /// تاریخ تأیید گواهی امضاء
    /// </summary>
    [Column("CONFIRM_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ConfirmDate { get; set; }

    /// <summary>
    /// زمان تأیید گواهی امضاء
    /// </summary>
    [Column("CONFIRM_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ConfirmTime { get; set; }

    /// <summary>
    /// امضای الكترونیك
    /// </summary>
    [Column("DIGITAL_SIGN", TypeName = "CLOB")]
    public string DigitalSign { get; set; }

    /// <summary>
    /// شناسه گواهی امضای الكترونیك
    /// </summary>
    [Column("SIGN_CERTIFICATE_DN", TypeName = "CLOB")]
    public string SignCertificateDn { get; set; }

    /// <summary>
    /// آیا این گواهی امضاء در جریان پروسه تأیید نهایی است؟
    /// </summary>
    [Column("IS_FINALIZE_IN_PROGRESS")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsFinalizeInProgress { get; set; }

    /// <summary>
    /// آیا بصورت غیرحضوری ثبت شده است؟
    /// </summary>
    [Column("IS_REMOTE_REQUEST")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRemoteRequest { get; set; }

    /// <summary>
    /// شناسه درخواست غیرحضوری
    /// </summary>
    [Column("REMOTE_REQUEST_ID")]
    public Guid? RemoteRequestId { get; set; }

    /// <summary>
    /// ملاحظات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Description { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
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

    /// <summary>
    /// تاریخ پرونده به میلادی
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

    /// <summary>
    /// آیا آماده پرداخت هست؟
    /// </summary>
    [Column("IS_READY_TO_PAY")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsReadyToPay { get; set; }

    [InverseProperty("SignRequest")]
    public virtual ICollection<SignElectronicBook> SignElectronicBooks { get; set; } = new List<SignElectronicBook>();

    [InverseProperty("SignRequest")]
    public virtual SignRequestFile SignRequestFile { get; set; }

    [ForeignKey("SignRequestGetterId")]
    [InverseProperty("SignRequests")]
    public virtual SignRequestGetter SignRequestGetter { get; set; }

    [InverseProperty("SignRequest")]
    public virtual ICollection<SignRequestPerson> SignRequestPeople { get; set; } = new List<SignRequestPerson>();

    [InverseProperty("SignRequest")]
    public virtual ICollection<SignRequestPersonRelated> SignRequestPersonRelateds { get; set; } = new List<SignRequestPersonRelated>();

    [InverseProperty("SignRequest")]
    public virtual ICollection<SignRequestSemaphore> SignRequestSemaphores { get; set; } = new List<SignRequestSemaphore>();

    [ForeignKey("SignRequestSubjectId")]
    [InverseProperty("SignRequests")]
    public virtual SignRequestSubject SignRequestSubject { get; set; }
}
