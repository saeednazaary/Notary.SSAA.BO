using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Table("BSTREALPERSON")]
[Index("Bstpersonid", Name = "IDX_BSTREALPERSON_BSTPERSONID")]
public partial class Bstrealperson
{
    [Column("BIRTHDATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Birthdate { get; set; }

    [Column("BIRTHISSUEID")]
    [Precision(6)]
    public int? Birthissueid { get; set; }

    [Column("BIRTHPLACEID")]
    [Precision(6)]
    public int? Birthplaceid { get; set; }

    [Column("BSTPERSONID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Bstpersonid { get; set; }

    [Column("EXECUTIVETRANSFER", TypeName = "NUMBER")]
    public decimal? Executivetransfer { get; set; }

    [Column("FAMILY")]
    [StringLength(100)]
    [Unicode(false)]
    public string Family { get; set; }

    [Column("FATHERNAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string Fathername { get; set; }

    [Column("FORIEGNBIRTHPLACE")]
    [StringLength(130)]
    [Unicode(false)]
    public string Foriegnbirthplace { get; set; }

    [Column("FORIEGNISSUEPLACE")]
    [StringLength(130)]
    [Unicode(false)]
    public string Foriegnissueplace { get; set; }

    [Key]
    [Column("ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Id { get; set; }

    [Column("IDENTIFICATIONNO")]
    [Precision(16)]
    public long? Identificationno { get; set; }

    [Column("ILM")]
    [Precision(4)]
    public byte? Ilm { get; set; }

    [Column("MELLICODE")]
    [StringLength(15)]
    [Unicode(false)]
    public string Mellicode { get; set; }

    [Column("NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; }

    [Column("NATIONALCODE")]
    [Precision(10)]
    public int? Nationalcode { get; set; }

    [Column("SABTEAHVALISUUEPLACE")]
    [Unicode(false)]
    public string Sabteahvalisuueplace { get; set; }

    [Column("SCRIPTORIUMID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Scriptoriumid { get; set; }

    [Column("SERI")]
    [StringLength(3)]
    [Unicode(false)]
    public string Seri { get; set; }

    [Column("SERIAL")]
    [Precision(8)]
    public int? Serial { get; set; }

    [Column("SEX")]
    [Precision(3)]
    public byte? Sex { get; set; }

    [Column("TIMESTAMP", TypeName = "NUMBER(20)")]
    public decimal? Timestamp { get; set; }
}
