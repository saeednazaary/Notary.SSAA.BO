using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Table("FAKE_SERVICE_LEGAL_FOREIGNERS_OLD")]
public partial class FakeServiceLegalForeignersOld
{
    [Key]
    [Column("FOREIGNERSCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Foreignerscode { get; set; }

    [Required]
    [Column("PERSIANNAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string Persianname { get; set; }

    [Required]
    [Column("REGISTERNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string Registernumber { get; set; }

    [Required]
    [Column("REGISTERDATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Registerdate { get; set; }

    [Required]
    [Column("REGISTRATIONCOUNTRY")]
    [StringLength(50)]
    [Unicode(false)]
    public string Registrationcountry { get; set; }
}
