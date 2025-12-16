using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// محتواي امضاي الکترونيک
/// </summary>
[Table("DIGITAL_SIGNATURE_VALUE")]
[Index("MainObjectId", Name = "IDX_DIGITAL_SIGNATURE_VALUE###MAIN_OBJECT_ID")]
public partial class DigitalSignatureValue
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// گواهي
    /// </summary>
    [Required]
    [Column("CERTIFICATE", TypeName = "CLOB")]
    public string Certificate { get; set; }

    /// <summary>
    /// مقدار امضا
    /// </summary>
    [Required]
    [Column("SIGN_VALUE", TypeName = "CLOB")]
    public string SignValue { get; set; }

    /// <summary>
    /// نام هندلر
    /// </summary>
    [Required]
    [Column("DATA_HANDLER_NAME")]
    [StringLength(1000)]
    [Unicode(false)]
    public string DataHandlerName { get; set; }

    /// <summary>
    /// ورودي هنلدر
    /// </summary>
    [Required]
    [Column("DATA_HANDLER_INPUT", TypeName = "CLOB")]
    public string DataHandlerInput { get; set; }

    /// <summary>
    /// تاريخ ايجاد
    /// </summary>
    [Required]
    [Column("CREATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string CreateDate { get; set; }

    /// <summary>
    /// زمان ايجاد
    /// </summary>
    [Required]
    [Column("CREATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string CreateTime { get; set; }

    /// <summary>
    /// تاريخ اعتبار
    /// </summary>
    [Required]
    [Column("EXPIRE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ExpireDate { get; set; }

    /// <summary>
    /// زمان اعتبار
    /// </summary>
    [Required]
    [Column("EXPIRE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string ExpireTime { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("MAIN_OBJECT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string MainObjectId { get; set; }

    /// <summary>
    /// داده ی امضا شدها
    /// </summary>
    [Column("SIGN_DATA", TypeName = "CLOB")]
    public string SignData { get; set; }
}
