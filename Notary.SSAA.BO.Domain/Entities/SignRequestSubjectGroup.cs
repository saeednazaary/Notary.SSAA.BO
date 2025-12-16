using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// گروه بندی موضوعات گواهی امضاء
/// </summary>
[Table("SIGN_REQUEST_SUBJECT_GROUP")]
[Index("State", Name = "IDX_SIGN_REQUEST_SUBJECT_GROUP###STATE")]
[Index("Code", Name = "UDX_SIGN_REQUEST_SUBJECT_GROUP###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_SIGN_REQUEST_SUBJECT_GROUP###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_SIGN_REQUEST_SUBJECT_GROUP###TITLE", IsUnique = true)]
public partial class SignRequestSubjectGroup
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

    [InverseProperty("SignRequestSubjectGroup")]
    public virtual ICollection<SignRequestSubject> SignRequestSubjects { get; set; } = new List<SignRequestSubject>();
}
