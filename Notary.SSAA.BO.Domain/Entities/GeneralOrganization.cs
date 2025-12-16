using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سازمان های عمومی
/// </summary>
[Table("GENERAL_ORGANIZATION")]
[Index("State", Name = "IDX_GENERAL_ORGANIZATION###STATE")]
[Index("Type", Name = "IDX_GENERAL_ORGANIZATION###TYPE")]
[Index("Code", Name = "IX_SSR_GENORG_CODE")]
[Index("Name", Name = "IX_SSR_GENORG_NAME")]
[Index("OrganizationId", Name = "IX_SSR_GENORG_ORGANIZATION_ID")]
[Index("LegacyId", Name = "UX_SSR_GENORG_LEGACY_ID", IsUnique = true)]
public partial class GeneralOrganization
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// شناسه معادل در سازمان اطلاعات پایه
    /// </summary>
    [Column("ORGANIZATION_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string OrganizationId { get; set; }

    /// <summary>
    /// شماره
    /// </summary>
    [Column("NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string No { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    [Required]
    [Column("NAME")]
    [StringLength(400)]
    [Unicode(false)]
    public string Name { get; set; }

    /// <summary>
    /// نشانی
    /// </summary>
    [Required]
    [Column("ADDRESS")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Address { get; set; }

    /// <summary>
    /// نوع
    /// </summary>
    [Column("TYPE")]
    [StringLength(2)]
    [Unicode(false)]
    public string Type { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// کلید رکورد در سامانۀ قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("GrowthIssuer")]
    public virtual ICollection<DocumentPerson> DocumentPersonGrowthIssuers { get; set; } = new List<DocumentPerson>();

    [InverseProperty("GrowthSender")]
    public virtual ICollection<DocumentPerson> DocumentPersonGrowthSenders { get; set; } = new List<DocumentPerson>();
}
