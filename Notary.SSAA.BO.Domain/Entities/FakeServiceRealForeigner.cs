using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Table("FAKE_SERVICE_REAL_FOREIGNERS")]
public partial class FakeServiceRealForeigner
{
    [Key]
    [Column("FOREIGNERSCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Foreignerscode { get; set; }

    [Required]
    [Column("PERSIANFIRSTNAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string Persianfirstname { get; set; }

    [Required]
    [Column("PERSIANLASTNAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string Persianlastname { get; set; }

    [Required]
    [Column("PERSIANFATHERNAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string Persianfathername { get; set; }

    [Required]
    [Column("IDENTIFICATIONDOCUMENTNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string Identificationdocumentnumber { get; set; }

    [Required]
    [Column("GENDER")]
    [StringLength(20)]
    [Unicode(false)]
    public string Gender { get; set; }

    [Required]
    [Column("BIRTHDATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Birthdate { get; set; }

    [Required]
    [Column("NATIONALITY")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nationality { get; set; }
}
