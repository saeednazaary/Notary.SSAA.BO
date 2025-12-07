using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// پیام ها
/// </summary>
[Table("SYSTEM_MESSAGE")]
[Index("Code", Name = "UDX_SYSTEM_MESSAGE###CODE", IsUnique = true)]
public partial class SystemMessage
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// كد پیام
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(100)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// متن پیام
    /// </summary>
    [Required]
    [Column("MESSAGE")]
    [StringLength(500)]
    [Unicode(false)]
    public string Message { get; set; }
}
