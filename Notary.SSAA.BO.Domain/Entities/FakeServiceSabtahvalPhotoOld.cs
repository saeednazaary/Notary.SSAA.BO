using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Table("FAKE_SERVICE_SABTAHVAL_PHOTO_OLD")]
public partial class FakeServiceSabtahvalPhotoOld
{
    [Key]
    [Column("NATIONALNO")]
    [StringLength(20)]
    public string Nationalno { get; set; }

    [Column("IMAGE", TypeName = "BLOB")]
    public byte[] Image { get; set; }
}
