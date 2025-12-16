using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Table("FAKE_SERVICE_ILENC")]
public partial class FakeServiceIlenc
{
    [Key]
    [Column("NATIONALNO")]
    [StringLength(11)]
    public string Nationalno { get; set; }

    [Required]
    [Column("NAME")]
    [StringLength(200)]
    [Unicode(false)]
    public string Name { get; set; }

    [Column("BANKRUPTCYDATE")]
    [StringLength(10)]
    public string Bankruptcydate { get; set; }

    [Column("BRANCHLIST", TypeName = "CLOB")]
    public string Branchlist { get; set; }

    [Column("BREAKUPDATE")]
    [StringLength(10)]
    public string Breakupdate { get; set; }

    [Column("ESTABLISHMENTDATE")]
    [StringLength(10)]
    public string Establishmentdate { get; set; }

    [Column("EXTENSIONDATA", TypeName = "CLOB")]
    public string Extensiondata { get; set; }

    [Column("FOLLOWUPNO")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Followupno { get; set; }

    [Column("ISBANKRUPT", TypeName = "NUMBER(30)")]
    public decimal? Isbankrupt { get; set; }

    [Column("ISBRANCH", TypeName = "NUMBER(30)")]
    public decimal? Isbranch { get; set; }

    [Column("ISBREAKUP", TypeName = "NUMBER(30)")]
    public decimal? Isbreakup { get; set; }

    [Column("ISSETTLE", TypeName = "NUMBER(30)")]
    public decimal? Issettle { get; set; }

    [Column("ISTAXRESTRICTED", TypeName = "NUMBER(30)")]
    public decimal? Istaxrestricted { get; set; }

    [Column("JSONRSEULT", TypeName = "NUMBER(30)")]
    public decimal? Jsonrseult { get; set; }

    [Column("LASTCHANGEDATE")]
    [StringLength(10)]
    public string Lastchangedate { get; set; }

    [Column("LEGALPERSONTYPE", TypeName = "NUMBER(30)")]
    public decimal? Legalpersontype { get; set; }

    [Column("LICENSENUMBER")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Licensenumber { get; set; }

    [Column("PARENTLEGALPERSON")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Parentlegalperson { get; set; }

    [Column("REGISTERDATE")]
    [StringLength(10)]
    public string Registerdate { get; set; }

    [Column("REGISTERNUMBER")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Registernumber { get; set; }

    [Column("REGISTERUNIT")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Registerunit { get; set; }

    [Column("RESIDENCY")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Residency { get; set; }

    [Column("SETTLEDATE")]
    [StringLength(10)]
    public string Settledate { get; set; }

    [Column("STATE", TypeName = "NUMBER(30)")]
    public decimal? State { get; set; }

    [Column("TAXRESTRICTDATE")]
    [StringLength(10)]
    public string Taxrestrictdate { get; set; }

    [Column("UNITID")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Unitid { get; set; }

    [Column("POSTCODE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Postcode { get; set; }

    [Column("ADDRESS")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Address { get; set; }

    [Column("LEGALPERSONTYPEID")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Legalpersontypeid { get; set; }
}
