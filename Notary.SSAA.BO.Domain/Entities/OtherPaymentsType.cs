using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// انواع سایر پرداخت های صورت گرفته در دفترخانه ها كه مربوط به مواردی نظیر اسناد، گواهی امضاء و استعلام نیستند
/// </summary>
[Table("OTHER_PAYMENTS_TYPE")]
[Index("State", Name = "IDX_OTHER_PAYMENTS_TYPE###STATE")]
[Index("Code", Name = "UDX_OTHER_PAYMENTS_TYPE###CODE", IsUnique = true)]
[Index("Title", Name = "UDX_OTHER_PAYMENTS_TYPE###TITLE", IsUnique = true)]
public partial class OtherPaymentsType
{
    /// <summary>
    ///  ردیف
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(3)]
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
    /// قیمت واحد
    /// </summary>
    [Column("FEE")]
    [Precision(15)]
    public long? Fee { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    [InverseProperty("OtherPaymentsType")]
    public virtual ICollection<OtherPayment> OtherPayments { get; set; } = new List<OtherPayment>();
}
