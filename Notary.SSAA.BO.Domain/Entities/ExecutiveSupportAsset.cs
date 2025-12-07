using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اموال معرفی شده در سایر تقاضاهای مربوط به اجرائیه برون سپاری شده به دفترخانه
/// </summary>
[Table("EXECUTIVE_SUPPORT_ASSET")]
[Index("BasicPersonId", Name = "IDX_EXECUTIVE_SUPPORT_ASSET###BASIC_PERSON_ID")]
[Index("ExecutiveWealthTypeId", Name = "IDX_EXECUTIVE_SUPPORT_ASSET###EXECUTIVE_WEALTH_TYPE_ID")]
[Index("Ilm", Name = "IDX_EXECUTIVE_SUPPORT_ASSET###ILM")]
[Index("IsInThirdPerson", Name = "IDX_EXECUTIVE_SUPPORT_ASSET###IS_IN_THIRD_PERSON")]
[Index("NationalNo", Name = "IDX_EXECUTIVE_SUPPORT_ASSET###NATIONAL_NO")]
[Index("OwnerPersonId", Name = "IDX_EXECUTIVE_SUPPORT_ASSET###OWNER_PERSON_ID")]
[Index("OwnerWealthType", Name = "IDX_EXECUTIVE_SUPPORT_ASSET###OWNER_WEALTH_TYPE")]
[Index("PersonType", Name = "IDX_EXECUTIVE_SUPPORT_ASSET###PERSON_TYPE")]
[Index("RowNo", Name = "IDX_EXECUTIVE_SUPPORT_ASSET###ROW_NO")]
[Index("ScriptoriumId", Name = "IDX_EXECUTIVE_SUPPORT_ASSET###SCRIPTORIUM_ID")]
[Index("State", Name = "IDX_EXECUTIVE_SUPPORT_ASSET###STATE")]
[Index("ExecutiveSupportId", "RowNo", Name = "UDX_EXECUTIVE_SUPPORT_ASSET###EXECUTIVE_SUPPORT_ID#ROW_NO", IsUnique = true)]
[Index("LegacyId", Name = "UDX_EXECUTIVE_SUPPORT_ASSET###LEGACY_ID", IsUnique = true)]
public partial class ExecutiveSupportAsset
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه سایر تقاضاهای مربوط به  اجرائیه برون سپاری شده به دفترخانه
    /// </summary>
    [Column("EXECUTIVE_SUPPORT_ID")]
    public Guid ExecutiveSupportId { get; set; }

    /// <summary>
    /// ردیف
    /// </summary>
    [Column("ROW_NO")]
    [Precision(5)]
    public short RowNo { get; set; }

    /// <summary>
    /// شناسه نوع اموال در اجرا
    /// </summary>
    [Required]
    [Column("EXECUTIVE_WEALTH_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string ExecutiveWealthTypeId { get; set; }

    /// <summary>
    /// شناسه شخص مالك
    /// </summary>
    [Column("OWNER_PERSON_ID")]
    public Guid? OwnerPersonId { get; set; }

    /// <summary>
    /// شناسه شخص از طرف
    /// </summary>
    [Column("BASIC_PERSON_ID")]
    public Guid BasicPersonId { get; set; }

    /// <summary>
    /// نوع شخص
    /// </summary>
    [Required]
    [Column("PERSON_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string PersonType { get; set; }

    /// <summary>
    /// شماره ملی
    /// </summary>
    [Column("NATIONAL_NO")]
    [StringLength(11)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    [Required]
    [Column("NAME")]
    [StringLength(200)]
    [Unicode(false)]
    public string Name { get; set; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    [Column("FAMILY")]
    [StringLength(100)]
    [Unicode(false)]
    public string Family { get; set; }

    /// <summary>
    /// جنسیت
    /// </summary>
    [Column("SEX_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string SexType { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(400)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// نشانی
    /// </summary>
    [Column("ADDRESS")]
    [StringLength(200)]
    [Unicode(false)]
    public string Address { get; set; }

    /// <summary>
    /// محل نگهداری
    /// </summary>
    [Column("KEEP_PLACE")]
    [StringLength(400)]
    [Unicode(false)]
    public string KeepPlace { get; set; }

    /// <summary>
    /// شرح
    /// </summary>
    [Column("WEALTH_DESCRIPTION", TypeName = "CLOB")]
    public string WealthDescription { get; set; }

    /// <summary>
    /// نوع مالك: متعهد/شخص ثالث
    /// </summary>
    [Required]
    [Column("OWNER_WEALTH_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string OwnerWealthType { get; set; }

    /// <summary>
    /// آیا مال نزد شخص ثالت است؟
    /// </summary>
    [Column("IS_IN_THIRD_PERSON")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsInThirdPerson { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Column("STATE")]
    [Precision(3)]
    public byte State { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده
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
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("ExecutiveSupportId")]
    [InverseProperty("ExecutiveSupportAssets")]
    public virtual ExecutiveSupport ExecutiveSupport { get; set; }

    [InverseProperty("ExecutiveSupportAsset")]
    public virtual ICollection<ExecutiveSupportAssetField> ExecutiveSupportAssetFields { get; set; } = new List<ExecutiveSupportAssetField>();

    [ForeignKey("ExecutiveWealthTypeId")]
    [InverseProperty("ExecutiveSupportAssets")]
    public virtual ExecutiveWealthType ExecutiveWealthType { get; set; }

    [ForeignKey("OwnerPersonId")]
    [InverseProperty("ExecutiveSupportAssets")]
    public virtual ExecutiveSupportPerson OwnerPerson { get; set; }
}
