using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// مستندات تقاضانامه اجرائیه برون سپاری شده به دفترخانه
/// </summary>
[Table("EXECUTIVE_REQUEST_DOCUMENT")]
[Index("AttachmentTypeId", Name = "IDX_EXECUTIVE_REQUEST_DOCUMENT###ATTACHMENT_TYPE_ID")]
[Index("BankId", Name = "IDX_EXECUTIVE_REQUEST_DOCUMENT###BANK_ID")]
[Index("DocumentDate", Name = "IDX_EXECUTIVE_REQUEST_DOCUMENT###DOCUMENT_DATE")]
[Index("DocumentNo", Name = "IDX_EXECUTIVE_REQUEST_DOCUMENT###DOCUMENT_NO")]
[Index("ExecutiveRequestId", Name = "IDX_EXECUTIVE_REQUEST_DOCUMENT###EXECUTIVE_REQUEST_ID")]
[Index("ExecutiveRequestScriptoriumId", Name = "IDX_EXECUTIVE_REQUEST_DOCUMENT###EXECUTIVE_REQUEST_SCRIPTORIUM_ID")]
[Index("Ilm", Name = "IDX_EXECUTIVE_REQUEST_DOCUMENT###ILM")]
[Index("MeasurementUnitTypeId", Name = "IDX_EXECUTIVE_REQUEST_DOCUMENT###MEASUREMENT_UNIT_TYPE_ID")]
[Index("LegacyId", Name = "UDX_EXECUTIVE_REQUEST_DOCUMENT###LEGACY_ID", IsUnique = true)]
public partial class ExecutiveRequestDocument
{
    /// <summary>
    ///  ردیف
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه تقاضانامه اجرائیه برون سپاری شده به دفترخانه
    /// </summary>
    [Column("EXECUTIVE_REQUEST_ID")]
    public Guid ExecutiveRequestId { get; set; }

    /// <summary>
    /// ردیف نوع سند یا پیوست
    /// </summary>
    [Required]
    [Column("ATTACHMENT_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string AttachmentTypeId { get; set; }

    /// <summary>
    /// شماره سند مربوط
    /// </summary>
    [Column("DOCUMENT_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string DocumentNo { get; set; }

    /// <summary>
    /// تاریخ سند مربوط
    /// </summary>
    [Column("DOCUMENT_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string DocumentDate { get; set; }

    /// <summary>
    /// ردیف بانك
    /// </summary>
    [Column("BANK_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string BankId { get; set; }

    /// <summary>
    /// ردیف انواع واحد اندازه گیری
    /// </summary>
    [Column("MEASUREMENT_UNIT_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string MeasurementUnitTypeId { get; set; }

    /// <summary>
    /// اولین تاریخ
    /// </summary>
    [Column("FIELD_DATE1")]
    [StringLength(10)]
    [Unicode(false)]
    public string FieldDate1 { get; set; }

    /// <summary>
    /// دومین تاریخ
    /// </summary>
    [Column("FIELD_DATE2")]
    [StringLength(10)]
    [Unicode(false)]
    public string FieldDate2 { get; set; }

    /// <summary>
    /// سومین تاریخ
    /// </summary>
    [Column("FIELD_DATE3")]
    [StringLength(10)]
    [Unicode(false)]
    public string FieldDate3 { get; set; }

    /// <summary>
    /// اولین مقدار
    /// </summary>
    [Column("FIELD_VALUE1")]
    [Precision(18)]
    public long? FieldValue1 { get; set; }

    /// <summary>
    /// دومین مقدار
    /// </summary>
    [Column("FIELD_VALUE2")]
    [Precision(18)]
    public long? FieldValue2 { get; set; }

    /// <summary>
    /// سومین مقدار
    /// </summary>
    [Column("FIELD_VALUE3")]
    [Precision(18)]
    public long? FieldValue3 { get; set; }

    /// <summary>
    /// اولین شرح
    /// </summary>
    [Column("FIELD_DESC1")]
    [StringLength(2000)]
    [Unicode(false)]
    public string FieldDesc1 { get; set; }

    /// <summary>
    /// دومین شرح
    /// </summary>
    [Column("FIELD_DESC2")]
    [StringLength(2000)]
    [Unicode(false)]
    public string FieldDesc2 { get; set; }

    /// <summary>
    /// سومین شرح
    /// </summary>
    [Column("FIELD_DESC3")]
    [StringLength(2000)]
    [Unicode(false)]
    public string FieldDesc3 { get; set; }

    /// <summary>
    /// چهارمین شرح
    /// </summary>
    [Column("FIELD_DESC4")]
    [StringLength(2000)]
    [Unicode(false)]
    public string FieldDesc4 { get; set; }

    /// <summary>
    /// پنجمین شرح
    /// </summary>
    [Column("FIELD_DESC5")]
    [StringLength(2000)]
    [Unicode(false)]
    public string FieldDesc5 { get; set; }

    /// <summary>
    /// ششمین شرح
    /// </summary>
    [Column("FIELD_DESC6")]
    [StringLength(2000)]
    [Unicode(false)]
    public string FieldDesc6 { get; set; }

    /// <summary>
    /// هفتمین شرح
    /// </summary>
    [Column("FIELD_DESC7")]
    [StringLength(2000)]
    [Unicode(false)]
    public string FieldDesc7 { get; set; }

    /// <summary>
    /// شناسه دفترخانه تقاضانامه اجرائیه
    /// </summary>
    [Required]
    [Column("EXECUTIVE_REQUEST_SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ExecutiveRequestScriptoriumId { get; set; }

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

    [ForeignKey("ExecutiveRequestId")]
    [InverseProperty("ExecutiveRequestDocuments")]
    public virtual ExecutiveRequest ExecutiveRequest { get; set; }
}
