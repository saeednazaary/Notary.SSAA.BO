using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// صفحات دفاتر دست نويس
/// </summary>
[Table("BOOKPAGES")]
[Index("BookId", Name = "IDX_BOOKPAGES###BOOKID")]
[Index("Code", Name = "IDX_BOOKPAGES###CODE")]
[Index("Pageno", Name = "IDX_BOOKPAGES###PAGENO")]
[Index("ScriptoriumId", Name = "IDX_BOOKPAGES###SCRIPTORIUMCODE")]
public partial class Bookpage
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("BOOK_ID")]
    public Guid BookId { get; set; }

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
    [Column("CODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// شماره صفحه
    /// </summary>
    [Column("PAGENO")]
    [Precision(6)]
    public int Pageno { get; set; }

    /// <summary>
    /// نشاني
    /// </summary>
    [Required]
    [Column("ADDRESS")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Address { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("Bookpages")]
    public virtual Book Book { get; set; }
}
