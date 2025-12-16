using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جدول دسترسی های کاربران بیرونی به سرویس ها
/// </summary>
[Table("SSR_API_EXTERNAL_USER_ACCESS")]
[Index("SsrApiExternalUserId", Name = "IX_SSR_API_EXT_USR_ACC_USR_ID")]
public partial class SsrApiExternalUserAccess
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
    /// شناسه کاربر بیرونی
    /// </summary>
    [Required]
    [Column("SSR_API_EXTERNAL_USER_ID")]
    [StringLength(20)]
    [Unicode(false)]
    public string SsrApiExternalUserId { get; set; }

    /// <summary>
    /// مسیر سرویس در سامانه
    /// </summary>
    [Required]
    [Column("API_PATH")]
    [StringLength(3000)]
    [Unicode(false)]
    public string ApiPath { get; set; }

    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    [ForeignKey("SsrApiExternalUserId")]
    [InverseProperty("SsrApiExternalUserAccesses")]
    public virtual SsrApiExternalUser SsrApiExternalUser { get; set; }
}
