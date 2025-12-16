using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// آخرین تاریخ در هر دفترخانه كه اطلاعات دفترخانه تا آن تاریخ كانورت شده است
/// </summary>
[Table("CONVERT_LEGACY_DATA_LOG_SCRIPTORIUM_LAST_DATE")]
[Index("LastConvertedDate", Name = "IDX_CONVERT_LEGACY_DATA_LOG_SCRIPTORIUM_LAST_DATE###LAST_DATE")]
[Index("ScriptoriumId", Name = "IDX_CONVERT_LEGACY_DATA_LOG_SCRIPTORIUM_LAST_DATE###SCRIPTORIUM_ID")]
public partial class ConvertLegacyDataLogScriptoriumLastDate
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// آخرین تاریخ اطلاعات كانورت شده
    /// </summary>
    [Required]
    [Column("LAST_CONVERTED_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LastConvertedDate { get; set; }
}
