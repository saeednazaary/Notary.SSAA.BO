using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// نوع ملك
/// </summary>
[Table("ESTATE_TYPE")]
public partial class EstateType
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string Id { get; set; }

    /// <summary>
    /// كد
    /// </summary>
    [Required]
    [Column("CODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Code { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    [Required]
    [Column("TITLE")]
    [StringLength(500)]
    [Unicode(false)]
    public string Title { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Required]
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    [InverseProperty("EstateType")]
    public virtual ICollection<EstateDocumentRequest> EstateDocumentRequests { get; set; } = new List<EstateDocumentRequest>();
}
