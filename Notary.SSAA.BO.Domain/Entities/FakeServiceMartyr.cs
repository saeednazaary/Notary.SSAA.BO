using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Table("FAKE_SERVICE_MARTYR")]
public partial class FakeServiceMartyr
{
    [Key]
    [Column("NATIONAL_NO")]
    [StringLength(10)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    [Required]
    [Column("MARTYR_CODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string MartyrCode { get; set; }

    [Column("DESCRIPTION")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Description { get; set; }
}
