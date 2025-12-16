using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// لاگ استعلام بنياد شهيد
/// </summary>
[Table("INQUIRY_MARTYR_LOG")]
[Index("FormId", Name = "IDX_INQUIRY_MARTYR_LOG###FORM_ID")]
[Index("Ilm", Name = "IDX_INQUIRY_MARTYR_LOG###ILM")]
[Index("IsMartyrIncluded", Name = "IDX_INQUIRY_MARTYR_LOG###IS_MARTYR_INCLUDED")]
[Index("MartyrCode", Name = "IDX_INQUIRY_MARTYR_LOG###MARTYR_CODE")]
[Index("MartyrInquiryDate", Name = "IDX_INQUIRY_MARTYR_LOG###MARTYR_INQUIRY_DATE")]
[Index("MartyrInquiryTime", Name = "IDX_INQUIRY_MARTYR_LOG###MARTYR_INQUIRY_TIME")]
[Index("NationalNo", Name = "IDX_INQUIRY_MARTYR_LOG###NATIONAL_NO")]
[Index("ObjectId", Name = "IDX_INQUIRY_MARTYR_LOG###OBJECT_ID")]
[Index("ScriptoriumId", Name = "IDX_INQUIRY_MARTYR_LOG###SCRIPTORIUM_ID")]
public partial class InquiryMartyrLog
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه فرم فراخوانی كننده سرویس
    /// </summary>
    [Required]
    [Column("FORM_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string FormId { get; set; }

    /// <summary>
    /// شناسه كلاس اصلی فراخوانی كننده سرویس
    /// </summary>
    [Required]
    [Column("OBJECT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string ObjectId { get; set; }

    /// <summary>
    /// شماره ملی متقاضی
    /// </summary>
    [Required]
    [Column("NATIONAL_NO")]
    [StringLength(10)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    /// <summary>
    /// آیا شخص مشمول استفاده از تخفیف بنیاد شهید هست؟
    /// </summary>
    [Column("IS_MARTYR_INCLUDED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsMartyrIncluded { get; set; }

    /// <summary>
    /// شناسه منحصربفرد شخص متقاضی تخفیف حق الثبت مخصوص ایثارگران نزد بنیاد شهید
    /// </summary>
    [Column("MARTYR_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string MartyrCode { get; set; }

    /// <summary>
    /// توضیحات مربوط به استعلام وضعیت ایثارگری از بنیاد شهید برای شخص متقاضی تخفیف حق الثبت مخصوص ایثارگران
    /// </summary>
    [Column("MARTYR_DESCRIPTION")]
    [StringLength(1000)]
    [Unicode(false)]
    public string MartyrDescription { get; set; }

    /// <summary>
    /// تاریخ استعلام از بنیاد شهید در مورد وضعیت ایثارگری برای متقاضی تخفیف حق الثبت مخصوص ایثارگران
    /// </summary>
    [Column("MARTYR_INQUIRY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string MartyrInquiryDate { get; set; }

    /// <summary>
    /// زمان استعلام از بنیاد شهید در مورد وضعیت ایثارگری برای متقاضی تخفیف حق الثبت مخصوص ایثارگران
    /// </summary>
    [Column("MARTYR_INQUIRY_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string MartyrInquiryTime { get; set; }

    /// <summary>
    /// شناسه دفترخانه
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
}
