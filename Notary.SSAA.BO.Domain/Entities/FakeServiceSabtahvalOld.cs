using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Keyless]
[Table("FAKE_SERVICE_SABTAHVAL_OLD")]
[Index("Birthdate", Name = "IDX_FAKE_SERVICE_SABTAHVAL###BIRTHDATE")]
[Index("Nationalno", Name = "SYS_C0011474", IsUnique = true)]
public partial class FakeServiceSabtahvalOld
{
    [Required]
    [Column("NATIONALNO")]
    [StringLength(10)]
    public string Nationalno { get; set; }

    [Required]
    [Column("BIRTHDATE")]
    [StringLength(10)]
    public string Birthdate { get; set; }

    [Required]
    [Column("NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; }

    [Required]
    [Column("FAMILY")]
    [StringLength(50)]
    [Unicode(false)]
    public string Family { get; set; }

    [Required]
    [Column("FATHERNAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string Fathername { get; set; }

    [Column("SEXTYPE")]
    [Precision(5)]
    public short Sextype { get; set; }

    [Required]
    [Column("IDENTITYNO")]
    [StringLength(50)]
    [Unicode(false)]
    public string Identityno { get; set; }

    [Required]
    [Column("IDENTITYISSUELOCATION")]
    [StringLength(50)]
    [Unicode(false)]
    public string Identityissuelocation { get; set; }

    [Column("SERI")]
    [StringLength(10)]
    [Unicode(false)]
    public string Seri { get; set; }

    [Column("SERIAL")]
    [StringLength(10)]
    [Unicode(false)]
    public string Serial { get; set; }

    [Column("DEATHDATE")]
    [StringLength(10)]
    public string Deathdate { get; set; }

    [Required]
    [Column("POSTALCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Postalcode { get; set; }

    [Required]
    [Column("ADDRESS")]
    [StringLength(500)]
    [Unicode(false)]
    public string Address { get; set; }

    [Column("LIFESTATE")]
    [Precision(5)]
    public short Lifestate { get; set; }

    [Column("ALPHA_SERI")]
    [Precision(5)]
    public short? AlphaSeri { get; set; }
}
