using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// پاسخ استعلام سازمان جنگل ها
/// </summary>
[Table("FORESTORG_INQUIRY_RESPONSE")]
[Index("ForestorgCityId", Name = "IX_SSR_FOGINQPSPNS_CTYID")]
[Index("ForestorgInquiryId", Name = "IX_SSR_FOGINQPSPNS_INQID")]
[Index("ForestorgProvinceId", Name = "IX_SSR_FOGINQPSPNS_PROVID")]
[Index("ForestorgSectionId", Name = "IX_SSR_FOGINQPSPNS_SECID")]
[Index("ResponseTypeId", Name = "IX_SSR_FOGINQPSPNS_TYPID")]
public partial class ForestorgInquiryResponse
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// فایل پی دی اف پاسخ
    /// </summary>
    [Column("RESPONSE_PDF_FILE", TypeName = "BLOB")]
    public byte[] ResponsePdfFile { get; set; }

    /// <summary>
    /// ردیف عامل پارتیشن
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
    /// ردیف شهرستان
    /// </summary>
    [Column("FORESTORG_CITY_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ForestorgCityId { get; set; }

    /// <summary>
    /// ردیف استعلام
    /// </summary>
    [Column("FORESTORG_INQUIRY_ID")]
    public Guid ForestorgInquiryId { get; set; }

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
    /// ردیف نوع پاسخ
    /// </summary>
    [Required]
    [Column("RESPONSE_TYPE_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string ResponseTypeId { get; set; }

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
    /// تاريخ ايجاد
    /// </summary>
    [Column("CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CreateDate { get; set; }

    /// <summary>
    /// زمان ايجاد
    /// </summary>
    [Column("CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string CreateTime { get; set; }

    /// <summary>
    /// تاريخ ايجاد
    /// </summary>
    [Column("FORESTORG_CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ForestorgCreateDate { get; set; }

    /// <summary>
    /// زمان ايجاد
    /// </summary>
    [Column("FORESTORG_CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ForestorgCreateTime { get; set; }

    /// <summary>
    /// زمان اصلاح
    /// </summary>
    [Column("FORESTORG_MODIFY_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ForestorgModifyTime { get; set; }

    /// <summary>
    /// کاربر اصلاح کننده
    /// </summary>
    [Column("FORESTORG_MODIFY_USER")]
    [StringLength(200)]
    [Unicode(false)]
    public string ForestorgModifyUser { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("FORESTORG_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string ForestorgId { get; set; }

    /// <summary>
    /// نوع شکل
    /// </summary>
    [Column("SHAPE_TYPE")]
    [StringLength(50)]
    [Unicode(false)]
    public string ShapeType { get; set; }

    /// <summary>
    /// مساحت شکل
    /// </summary>
    [Column("SHAPE_AREA")]
    [StringLength(50)]
    [Unicode(false)]
    public string ShapeArea { get; set; }

    /// <summary>
    /// تاريخ اصلاح
    /// </summary>
    [Column("FORESTORG_MODIFY_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ForestorgModifyDate { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("FOREST_ORGANIZATION_INQUIRY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string ForestOrganizationInquiryId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("ARCHIVE_MEDIA_FILE_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string ArchiveMediaFileId { get; set; }

    [ForeignKey("ForestorgCityId")]
    [InverseProperty("ForestorgInquiryResponses")]
    public virtual ForestorgCity ForestorgCity { get; set; }

    [ForeignKey("ForestorgInquiryId")]
    [InverseProperty("ForestorgInquiryResponses")]
    public virtual ForestorgInquiry ForestorgInquiry { get; set; }

    [ForeignKey("ForestorgProvinceId")]
    [InverseProperty("ForestorgInquiryResponses")]
    public virtual ForestorgProvince ForestorgProvince { get; set; }

    [InverseProperty("ForestorgInquiryResponse")]
    public virtual ICollection<ForestorgResponsepoint> ForestorgResponsepoints { get; set; } = new List<ForestorgResponsepoint>();

    [ForeignKey("ForestorgSectionId")]
    [InverseProperty("ForestorgInquiryResponses")]
    public virtual ForestorgSection ForestorgSection { get; set; }

    [ForeignKey("ResponseTypeId")]
    [InverseProperty("ForestorgInquiryResponses")]
    public virtual ForestorgInquiryResponsetype ResponseType { get; set; }
}
