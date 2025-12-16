using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// ارگان دریافت كننده گواهی امضاء
/// </summary>
[Table("SIGN_REQUEST_GETTER")]
[Index("State", Name = "IDX_SIGN_REQUEST_GETTER###STATE")]
[Index("Code", Name = "UDX_SIGN_REQUEST_GETTER###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_SIGN_REQUEST_GETTER###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_SIGN_REQUEST_GETTER###TITLE", IsUnique = true)]
public partial class SignRequestGetter
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(100)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [InverseProperty("SignRequestGetter")]
    public virtual ICollection<SignRequest> SignRequests { get; set; } = new List<SignRequest>();
}
