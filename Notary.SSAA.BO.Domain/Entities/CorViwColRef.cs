using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Keyless]
public partial class CorViwColRef
{
    [Column("OWNER_PARENT")]
    [StringLength(128)]
    [Unicode(false)]
    public string OwnerParent { get; set; }

    [Required]
    [Column("PARENT_TAB")]
    [StringLength(128)]
    [Unicode(false)]
    public string ParentTab { get; set; }

    [Required]
    [Column("A_CONSTRAINT_NAME")]
    [StringLength(128)]
    [Unicode(false)]
    public string AConstraintName { get; set; }

    [Required]
    [Column("OWNER_CHILD")]
    [StringLength(128)]
    [Unicode(false)]
    public string OwnerChild { get; set; }

    [Required]
    [Column("CHILD_TAB")]
    [StringLength(128)]
    [Unicode(false)]
    public string ChildTab { get; set; }

    [Column("REF_COL")]
    [StringLength(30)]
    [Unicode(false)]
    public string RefCol { get; set; }

    [Column("STAT")]
    [StringLength(4)]
    [Unicode(false)]
    public string Stat { get; set; }

    [Column("POS")]
    [StringLength(3)]
    [Unicode(false)]
    public string Pos { get; set; }

    [Column("DEL_RULE")]
    [StringLength(7)]
    [Unicode(false)]
    public string DelRule { get; set; }
}
