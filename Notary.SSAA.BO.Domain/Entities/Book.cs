using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// دفاتر دست نويس
/// </summary>
[Table("BOOK")]
[Index("Code", Name = "IDX_BOOK###CODE")]
[Index("ScriptoriumId", Name = "IDX_BOOK###SCRIPTORIUMCODE")]
public partial class Book
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// کد دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// کد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(100)]
    [Unicode(false)]
    public string Code { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<Bookpage> Bookpages { get; set; } = new List<Bookpage>();
}
