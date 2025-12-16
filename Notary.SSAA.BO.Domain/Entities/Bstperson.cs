using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

[Table("BSTPERSON")]
[Index("Estestateinquiryid", Name = "IDX_BSTPERSON_ESTESTATEINQUIRY")]
public partial class Bstperson
{
    [Column("ADDRESS")]
    [Unicode(false)]
    public string Address { get; set; }

    [Column("CITYNAMEID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Citynameid { get; set; }

    [Column("ESTESTATEINQUIRYID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Estestateinquiryid { get; set; }

    [Key]
    [Column("ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Id { get; set; }

    [Column("ILM")]
    [Precision(4)]
    public byte? Ilm { get; set; }

    [Column("ISVERIFIED")]
    [StringLength(5)]
    [Unicode(false)]
    public string Isverified { get; set; }

    [Column("NATIONALITYID")]
    [Precision(6)]
    public int? Nationalityid { get; set; }

    [Column("OWNERMOBILENO")]
    [StringLength(20)]
    [Unicode(false)]
    public string Ownermobileno { get; set; }

    [Column("OWNERTELNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string Ownertelno { get; set; }

    [Column("POSTALCODE")]
    [StringLength(16)]
    [Unicode(false)]
    public string Postalcode { get; set; }

    [Column("SCRIPTORIUMID")]
    [StringLength(32)]
    [Unicode(false)]
    public string Scriptoriumid { get; set; }

    [Column("SHARECONTEXT")]
    [Unicode(false)]
    public string Sharecontext { get; set; }

    [Column("SHAREPART", TypeName = "NUMBER(20,4)")]
    public decimal? Sharepart { get; set; }

    [Column("SHARETOTAL", TypeName = "NUMBER(20,4)")]
    public decimal? Sharetotal { get; set; }

    [Column("TIMESTAMP", TypeName = "NUMBER(20)")]
    public decimal? Timestamp { get; set; }
}
