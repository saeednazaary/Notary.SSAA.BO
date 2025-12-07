using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// درخواست صدور سند
/// </summary>
[Table("ESTATE_DOCUMENT_REQUEST")]
[Index("DocumentCurrentTypeId", Name = "IX_SSR_EDCREQ_CRNTTYPID")]
[Index("DefectiveRequestId", Name = "IX_SSR_EDCREQ_DFREQID")]
[Index("DocumentId", Name = "IX_SSR_EDCREQ_DID")]
[Index("DocumentRequestTypeId", Name = "IX_SSR_EDCREQ_DRQTYPID")]
[Index("EstateOwnershipTypeId", Name = "IX_SSR_EDCREQ_OWNTYPID")]
[Index("RevokedRequestId", Name = "IX_SSR_EDCREQ_REVREQID")]
[Index("EstateSectionId", Name = "IX_SSR_EDCREQ_SECID")]
[Index("EstateSubsectionId", Name = "IX_SSR_EDCREQ_SUBSECID")]
[Index("TransferDocumentTypeId", Name = "IX_SSR_EDCREQ_TRANSDOCTYPID")]
[Index("EstateTypeId", Name = "IX_SSR_EDCREQ_TYPID")]
[Index("WorkflowStatesId", Name = "IX_SSR_EDCREQ_WSTTEID")]
public partial class EstateDocumentRequest
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// نشانی ملك
    /// </summary>
    [Required]
    [Column("ADDRESS", TypeName = "CLOB")]
    public string Address { get; set; }

    /// <summary>
    /// پلاك اصلی ملك
    /// </summary>
    [Required]
    [Column("BASIC")]
    [StringLength(200)]
    [Unicode(false)]
    public string Basic { get; set; }

    /// <summary>
    /// پلاك اصلی ملك-باقیمانده
    /// </summary>
    [Required]
    [Column("BASIC_REMAINING")]
    [StringLength(1)]
    [Unicode(false)]
    public string BasicRemaining { get; set; }

    /// <summary>
    /// شماره بلوك ملك
    /// </summary>
    [Column("BLOCK_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string BlockNo { get; set; }

    /// <summary>
    /// تاریخ ثبت
    /// </summary>
    [Required]
    [Column("CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CreateDate { get; set; }

    /// <summary>
    /// زمان ثبت
    /// </summary>
    [Required]
    [Column("CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string CreateTime { get; set; }

    /// <summary>
    /// ردیف درخواست دارای نقص قبلی
    /// </summary>
    [Column("DEFECTIVE_REQUEST_ID")]
    public Guid? DefectiveRequestId { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION", TypeName = "CLOB")]
    public string Description { get; set; }

    /// <summary>
    /// ردیف نوع سند فعلی
    /// </summary>
    [Required]
    [Column("DOCUMENT_CURRENT_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string DocumentCurrentTypeId { get; set; }

    /// <summary>
    /// ردیف نوع درخواست
    /// </summary>
    [Required]
    [Column("DOCUMENT_REQUEST_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string DocumentRequestTypeId { get; set; }

    /// <summary>
    /// ردیف نوع مالكیت
    /// </summary>
    [Column("ESTATE_OWNERSHIP_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string EstateOwnershipTypeId { get; set; }

    /// <summary>
    /// ردیف بخش
    /// </summary>
    [Required]
    [Column("ESTATE_SECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSectionId { get; set; }

    /// <summary>
    /// ردیف ناحیه
    /// </summary>
    [Required]
    [Column("ESTATE_SUBSECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSubsectionId { get; set; }

    /// <summary>
    /// ردیف نوع ملك
    /// </summary>
    [Column("ESTATE_TYPE_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string EstateTypeId { get; set; }

    /// <summary>
    /// آیا وضعیت احراز هویت اشخاص با ثبت احوال  و كنترل بخشناه ها و بررسی و رویت شده است؟
    /// </summary>
    [Column("FINAL_VERIFICATION_VISITED")]
    [StringLength(1)]
    [Unicode(false)]
    public string FinalVerificationVisited { get; set; }

    /// <summary>
    /// شماره طبقه ملك
    /// </summary>
    [Column("FLOOR_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string FloorNo { get; set; }

    /// <summary>
    /// نواقص و اشكالات درخواست كه از سوی اداره املاك برگشت داده شده است
    /// </summary>
    [Column("HAMESH", TypeName = "CLOB")]
    public string Hamesh { get; set; }

    /// <summary>
    /// پلاك فرعی دارد
    /// </summary>
    [Required]
    [Column("HAS_SECONDARY")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasSecondary { get; set; }

    /// <summary>
    /// شماره درخواست
    /// </summary>
    [Required]
    [Column("NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string No { get; set; }

    /// <summary>
    /// شماره قطعه ملك
    /// </summary>
    [Column("PIECE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string PieceNo { get; set; }

    /// <summary>
    /// باركد پستی
    /// </summary>
    [Column("POST_BARCODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string PostBarcode { get; set; }

    /// <summary>
    /// كد پستی ملك
    /// </summary>
    [Required]
    [Column("POSTAL_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string PostalCode { get; set; }

    /// <summary>
    /// تاریخ درخواست
    /// </summary>
    [Required]
    [Column("REQUEST_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string RequestDate { get; set; }

    /// <summary>
    /// ردیف درخواست باطل شده ای كه از هزینه آن برای پرداخت این درخواست استفاده شده است
    /// </summary>
    [Column("REVOKED_REQUEST_ID")]
    public Guid? RevokedRequestId { get; set; }

    /// <summary>
    /// تاریخ و زمان تایید سردفتر
    /// </summary>
    [Column("SARDAFTAR_CONFIRM_DATE")]
    [StringLength(20)]
    [Unicode(false)]
    public string SardaftarConfirmDate { get; set; }

    /// <summary>
    /// نام و نام خانوادگی سردفتر
    /// </summary>
    [Column("SARDAFTAR_NAMEFAMILY")]
    [StringLength(1000)]
    [Unicode(false)]
    public string SardaftarNamefamily { get; set; }

    /// <summary>
    /// ردیف دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// پلاك فرعی ملك
    /// </summary>
    [Column("SECONDARY")]
    [StringLength(200)]
    [Unicode(false)]
    public string Secondary { get; set; }

    /// <summary>
    /// پلاك فرعی ملك- باقیمانده
    /// </summary>
    [Required]
    [Column("SECONDARY_REMAINING")]
    [StringLength(1)]
    [Unicode(false)]
    public string SecondaryRemaining { get; set; }

    /// <summary>
    /// گواهی امضای الكترونیك مورد استفاده برای امضای سردفتر
    /// </summary>
    [Column("SIGN_CERTIFICATE_DN")]
    [StringLength(2000)]
    [Unicode(false)]
    public string SignCertificateDn { get; set; }

    /// <summary>
    /// فيلد عددي کنترلي مخصوص کنترل concurrency
    /// </summary>
    [Column("TIMESTAMP", TypeName = "NUMBER(20)")]
    public decimal Timestamp { get; set; }

    /// <summary>
    /// ردیف واحد ثبتی
    /// </summary>
    [Required]
    [Column("UNIT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string UnitId { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه سیستم قدیم
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// شناسه گردش کار
    /// </summary>
    [Required]
    [Column("WORKFLOW_STATES_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string WorkflowStatesId { get; set; }

    /// <summary>
    /// شماره سند ارسالي
    /// </summary>
    [Column("TRANSFER_DOCUMENT_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string TransferDocumentNo { get; set; }

    /// <summary>
    /// تاريخ سند ارسالي
    /// </summary>
    [Column("TRANSFER_DOCUMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string TransferDocumentDate { get; set; }

    /// <summary>
    /// کد سند ارسالي
    /// </summary>
    [Column("TRANSFER_DOCUMENT_VERIFICATION_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string TransferDocumentVerificationCode { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("TRANSFER_DOCUMENT_SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string TransferDocumentScriptoriumId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("TRANSFER_DOCUMENT_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string TransferDocumentTypeId { get; set; }

    /// <summary>
    /// آيا سند در ثبت آني هست؟
    /// </summary>
    [Column("TRANSFER_DOCUMENT_IS_IN_SSAR")]
    [StringLength(1)]
    [Unicode(false)]
    public string TransferDocumentIsInSsar { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("DOCUMENT_ID")]
    public Guid? DocumentId { get; set; }

    /// <summary>
    /// آيا هزينه ها پرداخت شده است؟
    /// </summary>
    [Column("IS_COST_PAID")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsCostPaid { get; set; }

    /// <summary>
    /// مبلغ قابل پرداخت
    /// </summary>
    [Column("SUM_PRICES")]
    [Precision(8)]
    public int? SumPrices { get; set; }

    /// <summary>
    /// شماره فاکتور
    /// </summary>
    [Column("RECEIPT_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string ReceiptNo { get; set; }

    /// <summary>
    /// تاريخ پرداخت
    /// </summary>
    [Column("PAY_COST_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string PayCostDate { get; set; }

    /// <summary>
    /// زمان پرداخت
    /// </summary>
    [Column("PAY_COST_TIME")]
    [StringLength(50)]
    [Unicode(false)]
    public string PayCostTime { get; set; }

    /// <summary>
    /// شيوه پرداخت
    /// </summary>
    [Column("PAYMENT_TYPE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string PaymentType { get; set; }

    /// <summary>
    /// شناسه قبض
    /// </summary>
    [Column("BILL_NO")]
    [StringLength(100)]
    [Unicode(false)]
    public string BillNo { get; set; }

    /// <summary>
    /// روش پرداخت
    /// </summary>
    [Column("HOW_TO_PAY")]
    [StringLength(1000)]
    [Unicode(false)]
    public string HowToPay { get; set; }

    /// <summary>
    /// امضای الكترونیك سند - توسط سردفتر
    /// </summary>
    [Column("DOCUMENT_DIGITAL_SIGN", TypeName = "CLOB")]
    public string DocumentDigitalSign { get; set; }

    /// <summary>
    /// گواهي
    /// </summary>
    [Column("SIGN_CERTIFICATE", TypeName = "CLOB")]
    public string SignCertificate { get; set; }

    [ForeignKey("DefectiveRequestId")]
    [InverseProperty("InverseDefectiveRequest")]
    public virtual EstateDocumentRequest DefectiveRequest { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("EstateDocumentRequests")]
    public virtual Document Document { get; set; }

    [ForeignKey("DocumentCurrentTypeId")]
    [InverseProperty("EstateDocumentRequests")]
    public virtual EstateDocumentCurrentType DocumentCurrentType { get; set; }

    [ForeignKey("DocumentRequestTypeId")]
    [InverseProperty("EstateDocumentRequests")]
    public virtual EstateDocumentRequestType DocumentRequestType { get; set; }

    [InverseProperty("EstateDocumentRequest")]
    public virtual ICollection<EstateDocReqPersonRelate> EstateDocReqPersonRelates { get; set; } = new List<EstateDocReqPersonRelate>();

    [InverseProperty("DocumentRequest")]
    public virtual ICollection<EstateDocumentRequestPerson> EstateDocumentRequestPeople { get; set; } = new List<EstateDocumentRequestPerson>();

    [ForeignKey("EstateOwnershipTypeId")]
    [InverseProperty("EstateDocumentRequests")]
    public virtual EstateOwnershipType EstateOwnershipType { get; set; }

    [ForeignKey("EstateSectionId")]
    [InverseProperty("EstateDocumentRequests")]
    public virtual EstateSection EstateSection { get; set; }

    [ForeignKey("EstateSubsectionId")]
    [InverseProperty("EstateDocumentRequests")]
    public virtual EstateSubsection EstateSubsection { get; set; }

    [ForeignKey("EstateTypeId")]
    [InverseProperty("EstateDocumentRequests")]
    public virtual EstateType EstateType { get; set; }

    [InverseProperty("DefectiveRequest")]
    public virtual ICollection<EstateDocumentRequest> InverseDefectiveRequest { get; set; } = new List<EstateDocumentRequest>();

    [InverseProperty("RevokedRequest")]
    public virtual ICollection<EstateDocumentRequest> InverseRevokedRequest { get; set; } = new List<EstateDocumentRequest>();

    [ForeignKey("RevokedRequestId")]
    [InverseProperty("InverseRevokedRequest")]
    public virtual EstateDocumentRequest RevokedRequest { get; set; }

    [ForeignKey("TransferDocumentTypeId")]
    [InverseProperty("EstateDocumentRequests")]
    public virtual DocumentType TransferDocumentType { get; set; }

    [ForeignKey("WorkflowStatesId")]
    [InverseProperty("EstateDocumentRequests")]
    public virtual WorkflowState WorkflowStates { get; set; }
}
