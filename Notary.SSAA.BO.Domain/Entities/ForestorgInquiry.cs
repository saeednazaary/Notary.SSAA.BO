using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// استعلام سازمان جنگل ها
/// </summary>
[Table("FORESTORG_INQUIRY")]
[Index("ForestorgCityId", Name = "IX_SSR_FOGINQ_CTYID")]
[Index("EstateInquiryId", Name = "IX_SSR_FOGINQ_EINQID")]
[Index("ForestorgProvinceId", Name = "IX_SSR_FOGINQ_PROVID")]
[Index("ForestorgSectionId", Name = "IX_SSR_FOGINQ_SCTNID")]
[Index("WorkflowStatesId", Name = "IX_SSR_FOGINQ_WSTATID")]
public partial class ForestorgInquiry
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شماره
    /// </summary>
    [Required]
    [Column("NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string No { get; set; }

    /// <summary>
    /// كد رهگیری
    /// </summary>
    [Column("TRACKING_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string TrackingCode { get; set; }

    /// <summary>
    /// شناسه یكتای جدید
    /// </summary>
    [Column("UNIQUE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string UniqueNo { get; set; }

    /// <summary>
    /// تاریخ ارسال
    /// </summary>
    [Column("SEND_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SendDate { get; set; }

    /// <summary>
    /// زمان ارسال
    /// </summary>
    [Column("SEND_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SendTime { get; set; }

    /// <summary>
    /// شماره سریال
    /// </summary>
    [Column("SERIAL_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string SerialNo { get; set; }

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
    /// تاریخ نامه
    /// </summary>
    [Column("LETTER_DATE")]
    [StringLength(30)]
    [Unicode(false)]
    public string LetterDate { get; set; }

    /// <summary>
    /// شماره نامه
    /// </summary>
    [Column("LETTER_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string LetterNo { get; set; }

    /// <summary>
    /// فعال/غیر فعال
    /// </summary>
    [Column("ISACTIVE")]
    [StringLength(1)]
    [Unicode(false)]
    public string Isactive { get; set; }

    /// <summary>
    /// كد پستی ملك
    /// </summary>
    [Column("ESTATE_POST_CODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string EstatePostCode { get; set; }

    /// <summary>
    /// آدرس ملك
    /// </summary>
    [Column("ESTATE_ADDRESS", TypeName = "CLOB")]
    public string EstateAddress { get; set; }

    /// <summary>
    /// مساحت ملك
    /// </summary>
    [Column("ESTATE_AREA", TypeName = "NUMBER(20,3)")]
    public decimal? EstateArea { get; set; }

    /// <summary>
    /// پلاك اصلی ملك
    /// </summary>
    [Column("ESTATE_BASIC")]
    [StringLength(200)]
    [Unicode(false)]
    public string EstateBasic { get; set; }

    /// <summary>
    /// پلاك فرعی ملك
    /// </summary>
    [Column("ESTATE_SECONDARY")]
    [StringLength(200)]
    [Unicode(false)]
    public string EstateSecondary { get; set; }

    /// <summary>
    /// مفروز و مجزا شده از
    /// </summary>
    [Column("ESTATE_SEPARATE")]
    [StringLength(200)]
    [Unicode(false)]
    public string EstateSeparate { get; set; }

    /// <summary>
    /// ردیف استعلام ملك
    /// </summary>
    [Column("ESTATE_INQUIRY_ID")]
    public Guid? EstateInquiryId { get; set; }

    /// <summary>
    /// ردیف شهرستان
    /// </summary>
    [Column("FORESTORG_CITY_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ForestorgCityId { get; set; }

    /// <summary>
    /// ردیف استان
    /// </summary>
    [Column("FORESTORG_PROVINCE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string ForestorgProvinceId { get; set; }

    /// <summary>
    /// ردیف بخش
    /// </summary>
    [Column("FORESTORG_SECTION_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ForestorgSectionId { get; set; }

    /// <summary>
    /// ردیف دفتر خانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// فيلد عددي کنترلي مخصوص کنترل concurrency
    /// </summary>
    [Column("TIMESTAMP", TypeName = "NUMBER(20)")]
    public decimal Timestamp { get; set; }

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
    /// شماره نامه وارده
    /// </summary>
    [Column("INCOMMING_LETTER_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string IncommingLetterNo { get; set; }

    /// <summary>
    /// متن
    /// </summary>
    [Column("DEFECT_TEXT", TypeName = "CLOB")]
    public string DefectText { get; set; }

    /// <summary>
    /// تاريخ و زمان پاسخ
    /// </summary>
    [Column("RESPONSE_DATE_TIME")]
    [StringLength(19)]
    [Unicode(false)]
    public string ResponseDateTime { get; set; }

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

    [ForeignKey("EstateInquiryId")]
    [InverseProperty("ForestorgInquiries")]
    public virtual EstateInquiry EstateInquiry { get; set; }

    [ForeignKey("ForestorgCityId")]
    [InverseProperty("ForestorgInquiries")]
    public virtual ForestorgCity ForestorgCity { get; set; }

    [InverseProperty("ForestorgInquiry")]
    public virtual ICollection<ForestorgInquiryFile> ForestorgInquiryFiles { get; set; } = new List<ForestorgInquiryFile>();

    [InverseProperty("ForstorgInquiry")]
    public virtual ICollection<ForestorgInquiryPerson> ForestorgInquiryPeople { get; set; } = new List<ForestorgInquiryPerson>();

    [InverseProperty("ForestorgInquiry")]
    public virtual ICollection<ForestorgInquiryPoint> ForestorgInquiryPoints { get; set; } = new List<ForestorgInquiryPoint>();

    [InverseProperty("ForestorgInquiry")]
    public virtual ICollection<ForestorgInquiryResponse> ForestorgInquiryResponses { get; set; } = new List<ForestorgInquiryResponse>();

    [ForeignKey("ForestorgProvinceId")]
    [InverseProperty("ForestorgInquiries")]
    public virtual ForestorgProvince ForestorgProvince { get; set; }

    [ForeignKey("ForestorgSectionId")]
    [InverseProperty("ForestorgInquiries")]
    public virtual ForestorgSection ForestorgSection { get; set; }

    [ForeignKey("WorkflowStatesId")]
    [InverseProperty("ForestorgInquiries")]
    public virtual WorkflowState WorkflowStates { get; set; }
}
