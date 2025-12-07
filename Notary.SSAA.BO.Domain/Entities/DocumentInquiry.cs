using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// استعلام ها در فرآیند ثبت اسناد
/// </summary>
[Table("DOCUMENT_INQUIRY")]
[Index("DocumentEstateId", Name = "IDX_DOCUMENT_INQUIRY###DOCUMENT_ESTATE_ID")]
[Index("DocumentEstateOwnershipDocumentId", Name = "IDX_DOCUMENT_INQUIRY###DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT_ID")]
[Index("DocumentId", Name = "IDX_DOCUMENT_INQUIRY###DOCUMENT_ID")]
[Index("DocumentInquiryOrganizationId", Name = "IDX_DOCUMENT_INQUIRY###DOCUMENT_INQUIRY_ORGANIZATION_ID")]
[Index("DocumentPersonId", Name = "IDX_DOCUMENT_INQUIRY###DOCUMENT_PERSON_ID")]
[Index("EstateInquiriesId", Name = "IDX_DOCUMENT_INQUIRY###ESTATE_INQUIRIES_ID")]
[Index("ExpireDate", Name = "IDX_DOCUMENT_INQUIRY###EXPIRE_DATE")]
[Index("Ilm", Name = "IDX_DOCUMENT_INQUIRY###ILM")]
[Index("RecordDate", Name = "IDX_DOCUMENT_INQUIRY###RECORD_DATE")]
[Index("ReplyDate", Name = "IDX_DOCUMENT_INQUIRY###REPLY_DATE")]
[Index("ReplyNo", Name = "IDX_DOCUMENT_INQUIRY###REPLY_NO")]
[Index("ReplyType", Name = "IDX_DOCUMENT_INQUIRY###REPLY_TYPE")]
[Index("RequestDate", Name = "IDX_DOCUMENT_INQUIRY###REQUEST_DATE")]
[Index("RequestNo", Name = "IDX_DOCUMENT_INQUIRY###REQUEST_NO")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_INQUIRY###SCRIPTORIUM_ID")]
[Index("State", Name = "IDX_DOCUMENT_INQUIRY###STATE")]
[Index("LegacyId", Name = "UDX_DOCUMENT_INQUIRY###LEGACY_ID", IsUnique = true)]
public partial class DocumentInquiry
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
    /// شناسه سازمان های استعلام شونده در فرآیند ثبت اسناد
    /// </summary>
    [Required]
    [Column("DOCUMENT_INQUIRY_ORGANIZATION_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string DocumentInquiryOrganizationId { get; set; }

    /// <summary>
    /// شناسه استعلام املاك
    /// </summary>
    [Column("ESTATE_INQUIRIES_ID")]
    [StringLength(1000)]
    [Unicode(false)]
    public string EstateInquiriesId { get; set; }

    /// <summary>
    /// شناسه اشخاص سند
    /// </summary>
    [Column("DOCUMENT_PERSON_ID")]
    public Guid? DocumentPersonId { get; set; }

    /// <summary>
    /// شناسه املاك ثبت شده در اسناد
    /// </summary>
    [Column("DOCUMENT_ESTATE_ID")]
    public Guid? DocumentEstateId { get; set; }

    /// <summary>
    /// شناسه مستندات مالكیت املاك مندرج در سند
    /// </summary>
    [Column("DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT_ID")]
    public Guid? DocumentEstateOwnershipDocumentId { get; set; }

    /// <summary>
    /// شماره درخواست استعلام
    /// </summary>
    [Column("REQUEST_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string RequestNo { get; set; }

    /// <summary>
    /// تاریخ درخواست استعلام
    /// </summary>
    [Column("REQUEST_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string RequestDate { get; set; }

    /// <summary>
    /// متن درخواست استعلام
    /// </summary>
    [Column("REQUEST_TEXT", TypeName = "BLOB")]
    public byte[] RequestText { get; set; }

    /// <summary>
    /// شماره پاسخ استعلام
    /// </summary>
    [Column("REPLY_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string ReplyNo { get; set; }

    /// <summary>
    /// تاریخ پاسخ استعلام
    /// </summary>
    [Column("REPLY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ReplyDate { get; set; }

    /// <summary>
    /// نوع پاسخ : مثبت/منفی
    /// </summary>
    [Column("REPLY_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string ReplyType { get; set; }

    /// <summary>
    /// تصویر اسكن شده پاسخ استعلام
    /// </summary>
    [Column("REPLY_IMAGE", TypeName = "BLOB")]
    public byte[] ReplyImage { get; set; }

    /// <summary>
    /// متن پاسخ استعلام
    /// </summary>
    [Column("REPLY_TEXT", TypeName = "CLOB")]
    public string ReplyText { get; set; }

    /// <summary>
    /// جزء سهم - در پاسخ استعلام
    /// </summary>
    [Column("REPLY_DETAIL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? ReplyDetailQuota { get; set; }

    /// <summary>
    /// كل سهم - در پاسخ استعلام
    /// </summary>
    [Column("REPLY_TOTAL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? ReplyTotalQuota { get; set; }

    /// <summary>
    /// متن سهم - در پاسخ استعلام
    /// </summary>
    [Column("REPLY_QUOTA_TEXT", TypeName = "CLOB")]
    public string ReplyQuotaText { get; set; }

    /// <summary>
    /// تاریخ اعتبار
    /// </summary>
    [Column("EXPIRE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ExpireDate { get; set; }

    /// <summary>
    /// عبارات و شرایط لازم به ذكر در سند
    /// </summary>
    [Column("CONDITIONS")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Conditions { get; set; }

    /// <summary>
    /// مرجع صدور استعلام
    /// </summary>
    [Column("INQUIRY_ISSUER")]
    [StringLength(1000)]
    [Unicode(false)]
    public string InquiryIssuer { get; set; }

    /// <summary>
    /// شماره رهگیری استعلام الكترونیك دارایی
    /// </summary>
    [Column("TAX_TRACKING_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string TaxTrackingNo { get; set; }

    /// <summary>
    /// مبلغ
    /// </summary>
    [Column("PRICE")]
    [Precision(15)]
    public long? Price { get; set; }

    /// <summary>
    /// نوع پرداخت
    /// </summary>
    [Column("COST_PAY_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string CostPayType { get; set; }

    /// <summary>
    /// شماره تراكنش/فیش
    /// </summary>
    [Column("FACTOR_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string FactorNo { get; set; }

    /// <summary>
    /// تاریخ تراكنش/فیش
    /// </summary>
    [Column("FACTOR_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FactorDate { get; set; }

    /// <summary>
    /// زمان تراكنش/فیش
    /// </summary>
    [Column("FACTOR_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string FactorTime { get; set; }

    /// <summary>
    /// شماره ترمینال
    /// </summary>
    [Column("TERMINAL_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string TerminalNo { get; set; }

    /// <summary>
    /// پارامتر ورودی 1 سرویس الكترونیك
    /// </summary>
    [Column("INPUT_PARAMETER_1")]
    [StringLength(500)]
    [Unicode(false)]
    public string InputParameter1 { get; set; }

    /// <summary>
    /// پارامتر ورودی 2 سرویس الكترونیك
    /// </summary>
    [Column("INPUT_PARAMETER_2")]
    [StringLength(500)]
    [Unicode(false)]
    public string InputParameter2 { get; set; }

    /// <summary>
    /// پارامتر ورودی 3 سرویس الكترونیك
    /// </summary>
    [Column("INPUT_PARAMETER_3")]
    [StringLength(500)]
    [Unicode(false)]
    public string InputParameter3 { get; set; }

    /// <summary>
    /// پارامتر ورودی 4 سرویس الكترونیك
    /// </summary>
    [Column("INPUT_PARAMETER_4")]
    [StringLength(500)]
    [Unicode(false)]
    public string InputParameter4 { get; set; }

    /// <summary>
    /// پارامتر ورودی 5 سرویس الكترونیك
    /// </summary>
    [Column("INPUT_PARAMETER_5")]
    [StringLength(500)]
    [Unicode(false)]
    public string InputParameter5 { get; set; }

    /// <summary>
    /// پارامتر ورودی 6 سرویس الكترونیك
    /// </summary>
    [Column("INPUT_PARAMETER_6")]
    [StringLength(500)]
    [Unicode(false)]
    public string InputParameter6 { get; set; }

    /// <summary>
    /// پارامتر ورودی 7 سرویس الكترونیك
    /// </summary>
    [Column("INPUT_PARAMETER_7")]
    [StringLength(500)]
    [Unicode(false)]
    public string InputParameter7 { get; set; }

    /// <summary>
    /// پارامتر ورودی 8 سرویس الكترونیك
    /// </summary>
    [Column("INPUT_PARAMETER_8")]
    [StringLength(500)]
    [Unicode(false)]
    public string InputParameter8 { get; set; }

    /// <summary>
    /// پارامتر ورودی 9 سرویس الكترونیك
    /// </summary>
    [Column("INPUT_PARAMETER_9")]
    [StringLength(500)]
    [Unicode(false)]
    public string InputParameter9 { get; set; }

    /// <summary>
    /// پارامتر ورودی 10 سرویس الكترونیك
    /// </summary>
    [Column("INPUT_PARAMETER_10")]
    [StringLength(500)]
    [Unicode(false)]
    public string InputParameter10 { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(2)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده سند
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// وضعیت ركورد از لحاظ آرشیو
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// تاریخ پرونده سند به میلادی
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

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
    [Column("OLD_DOCUMENT_PERSON_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentPersonId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ESTATE_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentEstateId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ESTATE_OWNERSHIP_DOCUMENT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentEstateOwnershipDocumentId { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentInquiries")]
    public virtual Document Document { get; set; }

    [ForeignKey("DocumentEstateId")]
    [InverseProperty("DocumentInquiries")]
    public virtual DocumentEstate DocumentEstate { get; set; }

    [ForeignKey("DocumentEstateOwnershipDocumentId")]
    [InverseProperty("DocumentInquiries")]
    public virtual DocumentEstateOwnershipDocument DocumentEstateOwnershipDocument { get; set; }

    [ForeignKey("DocumentInquiryOrganizationId")]
    [InverseProperty("DocumentInquiries")]
    public virtual DocumentInquiryOrganization DocumentInquiryOrganization { get; set; }

    [ForeignKey("DocumentPersonId")]
    [InverseProperty("DocumentInquiries")]
    public virtual DocumentPerson DocumentPerson { get; set; }
}
