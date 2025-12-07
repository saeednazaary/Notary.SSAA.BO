using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// كاربردهای استفاده كننده از خدمات اخذ اثر انگشت
/// </summary>
[Table("PERSON_FINGERPRINT_USE_CASE")]
[Index("State", Name = "IDX_PERSON_FINGERPRINT_USE_CASE###STATE")]
[Index("Code", Name = "UDX_PERSON_FINGERPRINT_USE_CASE###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_PERSON_FINGERPRINT_USE_CASE###TITLE", IsUnique = true)]
public partial class PersonFingerprintUseCase
{
    /// <summary>
    /// كلید اصلی
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(2)]
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

    [InverseProperty("PersonFingerprintUseCase")]
    public virtual ICollection<InquiryMocLog> InquiryMocLogs { get; set; } = new List<InquiryMocLog>();

    [InverseProperty("PersonFingerprintUseCase")]
    public virtual ICollection<PersonFingerprint> PersonFingerprints { get; set; } = new List<PersonFingerprint>();
}
