using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// جدول فیك قطعات تقسیم نامه ملك
/// </summary>
[Table("ESTATE_DIVISION_REQUEST_ELEMENTS")]
[Index("EstateSectionId", Name = "IX_SSR_EDIVREQELMEN_SCTNID")]
[Index("EstateSubsectionId", Name = "IX_SSR_EDIVREQELMEN_SUBSCTNID")]
public partial class EstateDivisionRequestElement
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// اصلي
    /// </summary>
    [Required]
    [Column("ESTATE_BASIC")]
    [StringLength(100)]
    [Unicode(false)]
    public string EstateBasic { get; set; }

    /// <summary>
    /// فرعي
    /// </summary>
    [Required]
    [Column("ESTATE_SECONDARY")]
    [StringLength(100)]
    [Unicode(false)]
    public string EstateSecondary { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("ESTATE_SECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSectionId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("ESTATE_SUBSECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSubsectionId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("UNITID")]
    [StringLength(5)]
    [Unicode(false)]
    public string Unitid { get; set; }

    /// <summary>
    /// جيسون
    /// </summary>
    [Required]
    [Column("ELEMENTS_JSON", TypeName = "CLOB")]
    public string ElementsJson { get; set; }

    [ForeignKey("EstateSectionId")]
    [InverseProperty("EstateDivisionRequestElements")]
    public virtual EstateSection EstateSection { get; set; }

    [ForeignKey("EstateSubsectionId")]
    [InverseProperty("EstateDivisionRequestElements")]
    public virtual EstateSubsection EstateSubsection { get; set; }
}
