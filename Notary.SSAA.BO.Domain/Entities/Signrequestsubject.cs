using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// موضوع گواهی امضاء
/// </summary>
[Table("SIGN_REQUEST_SUBJECT")]
[Index("SignRequestSubjectGroupId", Name = "IDX_SIGN_REQUEST_SUBJECT###SIGN_REQUEST_SUBJECT_GROUP_ID")]
[Index("State", Name = "IDX_SIGN_REQUEST_SUBJECT###STATE")]
[Index("Code", Name = "UDX_SIGN_REQUEST_SUBJECT###CODE", IsUnique = true)]
[Index("LegacyId", Name = "UDX_SIGN_REQUEST_SUBJECT###LEGACY_ID", IsUnique = true)]
[Index("Title", Name = "UDX_SIGN_REQUEST_SUBJECT###TITLE", IsUnique = true)]
public partial class SignRequestSubject
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
    /// شناسه گروه بندی موضوعات گواهی امضاء
    /// </summary>
    [Required]
    [Column("SIGN_REQUEST_SUBJECT_GROUP_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string SignRequestSubjectGroupId { get; set; }

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

    [ForeignKey("SignRequestSubjectGroupId")]
    [InverseProperty("SignRequestSubjects")]
    public virtual SignRequestSubjectGroup SignRequestSubjectGroup { get; set; }

    [InverseProperty("SignRequestSubject")]
    public virtual ICollection<SignRequest> SignRequests { get; set; } = new List<SignRequest>();
}
