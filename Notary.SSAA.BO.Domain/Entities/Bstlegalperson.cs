using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Table("BSTLEGALPERSON")]
[Index("Bstpersonid", Name = "IDX_BSTLEGALPERSON_BSTPERSONID")]
public partial class Bstlegalperson
{
    [Column("BSTCOMPANYTYPEID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Bstcompanytypeid { get; set; }

    [Column("BSTPERSONID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Bstpersonid { get; set; }

    [Column("EXECUTIVETRANSFER", TypeName = "NUMBER")]
    public decimal? Executivetransfer { get; set; }

    [Key]
    [Column("ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Id { get; set; }

    [Column("ILM")]
    [Precision(4)]
    public byte? Ilm { get; set; }

    [Column("MELLICODE")]
    [StringLength(12)]
    [Unicode(false)]
    public string Mellicode { get; set; }

    [Column("NAME")]
    [StringLength(200)]
    [Unicode(false)]
    public string Name { get; set; }

    [Column("NATIONALCODE")]
    [Precision(10)]
    public int? Nationalcode { get; set; }

    [Column("REGISTERDATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Registerdate { get; set; }

    [Column("REGISTERNO")]
    [StringLength(200)]
    [Unicode(false)]
    public string Registerno { get; set; }

    [Column("SCRIPTORIUMID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Scriptoriumid { get; set; }

    [Column("TIMESTAMP", TypeName = "NUMBER(20)")]
    public decimal? Timestamp { get; set; }
}
