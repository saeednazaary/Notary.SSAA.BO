using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// تعریف انگشتان
/// </summary>
[Table("PERSON_FINGER_TYPE")]
[Index("State", Name = "IDX_PERSON_FINGER_TYPE###STATE")]
[Index("Code", Name = "UDX_PERSON_FINGER_TYPE###CODE", IsUnique = true)]
[Index("SabtahvalCode", Name = "UDX_PERSON_FINGER_TYPE###SABTAHVAL_CODE", IsUnique = true)]
[Index("Title", Name = "UDX_PERSON_FINGER_TYPE###TITLE", IsUnique = true)]
public partial class PersonFingerType
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

    /// <summary>
    /// كد انگشت در سازمان ثبت احوال
    /// </summary>
    [Column("SABTAHVAL_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SabtahvalCode { get; set; }

    [InverseProperty("PersonFingerType")]
    public virtual ICollection<InquiryMocLog> InquiryMocLogs { get; set; } = new List<InquiryMocLog>();

    [InverseProperty("PersonFingerType")]
    public virtual ICollection<PersonFingerprint> PersonFingerprints { get; set; } = new List<PersonFingerprint>();
}
