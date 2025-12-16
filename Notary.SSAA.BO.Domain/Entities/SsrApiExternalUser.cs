using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// كاربران بیرونی استفاده كننده از سرویس های سامانه
/// </summary>
[Table("SSR_API_EXTERNAL_USER")]
[Index("UserName", Name = "UX_SSR_API_EXTRNL_USER_UN", IsUnique = true)]
public partial class SsrApiExternalUser
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(20)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// نام كاربری
    /// </summary>
    [Required]
    [Column("USER_NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string UserName { get; set; }

    /// <summary>
    /// كلمه عبور
    /// </summary>
    [Required]
    [Column("USER_PASSWORD")]
    [StringLength(200)]
    [Unicode(false)]
    public string UserPassword { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    [InverseProperty("SsrApiExternalUser")]
    public virtual ICollection<SsrApiExternalUserAccess> SsrApiExternalUserAccesses { get; set; } = new List<SsrApiExternalUserAccess>();

    [InverseProperty("SsrApiExternalUser")]
    public virtual ICollection<SsrDocVerifExternalUser> SsrDocVerifExternalUsers { get; set; } = new List<SsrDocVerifExternalUser>();
}
