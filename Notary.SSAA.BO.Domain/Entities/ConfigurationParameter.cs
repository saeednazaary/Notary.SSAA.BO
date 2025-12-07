using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// پارامترهای پیكربندی سامانه
/// </summary>
[Table("CONFIGURATION_PARAMETERS")]
[Index("Subject", Name = "UDX_CONFIGURATION_PARAMETERS###SUBJECT", IsUnique = true)]
public partial class ConfigurationParameter
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// موضوع
    /// </summary>
    [Required]
    [Column("SUBJECT")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Subject { get; set; }

    /// <summary>
    /// مقدار
    /// </summary>
    [Required]
    [Column("VALUE", TypeName = "CLOB")]
    public string Value { get; set; }
}
