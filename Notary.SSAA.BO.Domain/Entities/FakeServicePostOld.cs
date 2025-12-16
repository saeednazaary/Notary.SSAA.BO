using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Keyless]
[Table("FAKE_SERVICE_POST_OLD")]
[Index("Postcode", Name = "SYS_C0011462", IsUnique = true)]
public partial class FakeServicePostOld
{
    [Required]
    [Column("POSTCODE")]
    [StringLength(10)]
    public string Postcode { get; set; }

    [Required]
    [Column("ADDRESS")]
    [StringLength(500)]
    [Unicode(false)]
    public string Address { get; set; }
}
