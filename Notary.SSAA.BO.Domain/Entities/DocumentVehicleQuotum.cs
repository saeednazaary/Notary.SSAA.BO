using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سهم اصحاب سند از وسایل نقلیه مورد معامله در سند
/// </summary>
[Table("DOCUMENT_VEHICLE_QUOTA")]
[Index("DocumentPersonId", Name = "IDX_DOCUMENT_VEHICLE_QUOTA###DOCUMENT_PERSON_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_VEHICLE_QUOTA###ILM")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_VEHICLE_QUOTA###SCRIPTORIUM_ID")]
[Index("DocumentVehicleId", "DocumentPersonId", Name = "IDX_SSR_DC_VICLQ_DVID_PRSID")]
[Index("LegacyId", Name = "UDX_DOCUMENT_VEHICLE_QUOTA###LEGACY_ID", IsUnique = true)]
public partial class DocumentVehicleQuotum
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه وسایل نقلیه ثبت شده در اسناد
    /// </summary>
    [Column("DOCUMENT_VEHICLE_ID")]
    public Guid DocumentVehicleId { get; set; }

    /// <summary>
    /// شناسه اشخاص اسناد
    /// </summary>
    [Column("DOCUMENT_PERSON_ID")]
    public Guid DocumentPersonId { get; set; }

    /// <summary>
    /// جزء سهم مورد معامله
    /// </summary>
    [Column("DETAIL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? DetailQuota { get; set; }

    /// <summary>
    /// كل سهم مورد معامله
    /// </summary>
    [Column("TOTAL_QUOTA", TypeName = "NUMBER(20,5)")]
    public decimal? TotalQuota { get; set; }

    /// <summary>
    /// متن سهم
    /// </summary>
    [Column("QUOTA_TEXT")]
    [StringLength(200)]
    [Unicode(false)]
    public string QuotaText { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده سند
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
    [Column("OLD_DOCUMENT_VEHICLE_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentVehicleId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_PERSON_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentPersonId { get; set; }

    [ForeignKey("DocumentPersonId")]
    [InverseProperty("DocumentVehicleQuota")]
    public virtual DocumentPerson DocumentPerson { get; set; }

    [ForeignKey("DocumentVehicleId")]
    [InverseProperty("DocumentVehicleQuota")]
    public virtual DocumentVehicle DocumentVehicle { get; set; }
}
