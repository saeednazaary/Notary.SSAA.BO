using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Keyless]
[Table("FAKE_SERVICE_SANA_OLD")]
[Index("Mobileno", Name = "IDX_FAKE_SERVICE_SANA###MOBILENO")]
[Index("Nationalno", "Mobileno", Name = "UDX_FAKE_SERVICE_SANA###NATIONALNO#MOBILENO", IsUnique = true)]
public partial class FakeServiceSanaOld
{
    [Required]
    [Column("NATIONALNO")]
    [StringLength(11)]
    [Unicode(false)]
    public string Nationalno { get; set; }

    [Required]
    [Column("MOBILENO")]
    [StringLength(11)]
    [Unicode(false)]
    public string Mobileno { get; set; }
}
