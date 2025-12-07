using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// لاگ سرويس ام او سي
/// </summary>
[Table("INQUIRY_MOC_LOG")]
[Index("ActionDate", Name = "IDX_INQUIRY_MOC_LOG###ACTION_DATE")]
[Index("ActionName", Name = "IDX_INQUIRY_MOC_LOG###ACTION_NAME")]
[Index("ActionTime", Name = "IDX_INQUIRY_MOC_LOG###ACTION_TIME")]
[Index("ActionType", Name = "IDX_INQUIRY_MOC_LOG###ACTION_TYPE")]
[Index("ErrorCode", Name = "IDX_INQUIRY_MOC_LOG###ERROR_CODE")]
[Index("FormId", Name = "IDX_INQUIRY_MOC_LOG###FORM_ID")]
[Index("Ilm", Name = "IDX_INQUIRY_MOC_LOG###ILM")]
[Index("NationalNo", Name = "IDX_INQUIRY_MOC_LOG###NATIONAL_NO")]
[Index("ObjectId", Name = "IDX_INQUIRY_MOC_LOG###OBJECT_ID")]
[Index("PersonFingerprintId", Name = "IDX_INQUIRY_MOC_LOG###PERSON_FINGERPRINT_ID")]
[Index("PersonFingerprintUseCaseId", Name = "IDX_INQUIRY_MOC_LOG###PERSON_FINGERPRINT_USE_CASE_ID")]
[Index("PersonFingerTypeId", Name = "IDX_INQUIRY_MOC_LOG###PERSON_FINGER_TYPE_ID")]
[Index("ScriptoriumId", Name = "IDX_INQUIRY_MOC_LOG###SCRIPTORIUM_ID")]
[Index("LegacyId", Name = "UDX_INQUIRY_MOC_LOG###LEGACY_ID", IsUnique = true)]
[Index("SerialNo", Name = "UDX_INQUIRY_MOC_LOG###SERIAL_NO", IsUnique = true)]
public partial class InquiryMocLog
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("PERSON_FINGERPRINT_USE_CASE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string PersonFingerprintUseCaseId { get; set; }

    /// <summary>
    /// شناسه فرم
    /// </summary>
    [Required]
    [Column("FORM_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string FormId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("OBJECT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string ObjectId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شماره ملي
    /// </summary>
    [Required]
    [Column("NATIONAL_NO")]
    [StringLength(10)]
    [Unicode(false)]
    public string NationalNo { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("PERSON_FINGER_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string PersonFingerTypeId { get; set; }

    /// <summary>
    /// تاريخ عمليات
    /// </summary>
    [Required]
    [Column("ACTION_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string ActionDate { get; set; }

    /// <summary>
    /// زمان عمليات
    /// </summary>
    [Required]
    [Column("ACTION_TIME")]
    [StringLength(10)]
    [Unicode(false)]
    public string ActionTime { get; set; }

    /// <summary>
    /// نوع عمليات
    /// </summary>
    [Required]
    [Column("ACTION_TYPE")]
    [StringLength(100)]
    [Unicode(false)]
    public string ActionType { get; set; }

    /// <summary>
    /// نام عمليات
    /// </summary>
    [Required]
    [Column("ACTION_NAME")]
    [StringLength(500)]
    [Unicode(false)]
    public string ActionName { get; set; }

    /// <summary>
    /// نتايج عمليات
    /// </summary>
    [Column("ACTION_RESULTS", TypeName = "CLOB")]
    public string ActionResults { get; set; }

    /// <summary>
    /// سريال شناسنامه
    /// </summary>
    [Column("SERIAL_NO")]
    [Precision(12)]
    public long SerialNo { get; set; }

    /// <summary>
    /// کد خطا
    /// </summary>
    [Column("ERROR_CODE")]
    [StringLength(100)]
    [Unicode(false)]
    public string ErrorCode { get; set; }

    /// <summary>
    /// متن خطا
    /// </summary>
    [Column("ERROR_TEXT", TypeName = "CLOB")]
    public string ErrorText { get; set; }

    /// <summary>
    /// تصوير شخص
    /// </summary>
    [Column("FACE_IMAGE", TypeName = "BLOB")]
    public byte[] FaceImage { get; set; }

    /// <summary>
    /// بسته دريافتي
    /// </summary>
    [Column("RECIEVED_PACKET", TypeName = "CLOB")]
    public string RecievedPacket { get; set; }

    /// <summary>
    /// بسته ارسالي
    /// </summary>
    [Column("SENT_PACKET", TypeName = "CLOB")]
    public string SentPacket { get; set; }

    /// <summary>
    /// ساير اطلاعات
    /// </summary>
    [Column("OTHER_INFORMATION", TypeName = "CLOB")]
    public string OtherInformation { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("PERSON_FINGERPRINT_ID")]
    public Guid? PersonFingerprintId { get; set; }

    /// <summary>
    /// ILM
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// کليد اصلي رکورد در سامانه قبلي
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    [ForeignKey("PersonFingerTypeId")]
    [InverseProperty("InquiryMocLogs")]
    public virtual PersonFingerType PersonFingerType { get; set; }

    [ForeignKey("PersonFingerprintId")]
    [InverseProperty("InquiryMocLogs")]
    public virtual PersonFingerprint PersonFingerprint { get; set; }

    [ForeignKey("PersonFingerprintUseCaseId")]
    [InverseProperty("InquiryMocLogs")]
    public virtual PersonFingerprintUseCase PersonFingerprintUseCase { get; set; }
}
