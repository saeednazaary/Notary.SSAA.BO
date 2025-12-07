using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// اثر انگشت های اخذ شده
/// </summary>
[Table("PERSON_FINGERPRINT")]
[Index("Description", Name = "IDX_PERSON_FINGERPRINT###DESCRIPTION")]
[Index("FingerprintGetDate", Name = "IDX_PERSON_FINGERPRINT###FINGERPRINT_GET_DATE")]
[Index("FingerprintGetTime", Name = "IDX_PERSON_FINGERPRINT###FINGERPRINT_GET_TIME")]
[Index("FingerprintScannerDeviceType", Name = "IDX_PERSON_FINGERPRINT###FINGERPRINT_SCANNER_DEVICE_TYPE")]
[Index("Ilm", Name = "IDX_PERSON_FINGERPRINT###ILM")]
[Index("IsDeleted", Name = "IDX_PERSON_FINGERPRINT###IS_DELETED")]
[Index("MocDescription", Name = "IDX_PERSON_FINGERPRINT###MOC_DESCRIPTION")]
[Index("MocIsRequired", Name = "IDX_PERSON_FINGERPRINT###MOC_IS_REQUIRED")]
[Index("MocState", Name = "IDX_PERSON_FINGERPRINT###MOC_STATE")]
[Index("OrganizationId", Name = "IDX_PERSON_FINGERPRINT###ORGANIZATION_ID")]
[Index("PersonFingerprintUseCaseId", Name = "IDX_PERSON_FINGERPRINT###PERSON_FINGERPRINT_USE_CASE_ID")]
[Index("PersonFingerTypeId", Name = "IDX_PERSON_FINGERPRINT###PERSON_FINGER_TYPE_ID")]
[Index("PersonNationalNo", Name = "IDX_PERSON_FINGERPRINT###PERSON_NATIONAL_NO")]
[Index("RecordDate", Name = "IDX_PERSON_FINGERPRINT###RECORD_DATE")]
[Index("SmartCardRequestDate", Name = "IDX_PERSON_FINGERPRINT###SMART_CARD_REQUEST_DATE")]
[Index("SmartCardRequestTime", Name = "IDX_PERSON_FINGERPRINT###SMART_CARD_REQUEST_TIME")]
[Index("SmartCardResponseDate", Name = "IDX_PERSON_FINGERPRINT###SMART_CARD_RESPONSE_DATE")]
[Index("SmartCardResponseTime", Name = "IDX_PERSON_FINGERPRINT###SMART_CARD_RESPONSE_TIME")]
[Index("SmartCardStatus", Name = "IDX_PERSON_FINGERPRINT###SMART_CARD_STATUS")]
[Index("TfaIsRequired", Name = "IDX_PERSON_FINGERPRINT###TFA_IS_REQUIRED")]
[Index("TfaMobileNo", Name = "IDX_PERSON_FINGERPRINT###TFA_MOBILE_NO")]
[Index("TfaSendDate", Name = "IDX_PERSON_FINGERPRINT###TFA_SEND_DATE")]
[Index("TfaSendTime", Name = "IDX_PERSON_FINGERPRINT###TFA_SEND_TIME")]
[Index("TfaState", Name = "IDX_PERSON_FINGERPRINT###TFA_STATE")]
[Index("TfaValidateDate", Name = "IDX_PERSON_FINGERPRINT###TFA_VALIDATE_DATE")]
[Index("TfaValidateTime", Name = "IDX_PERSON_FINGERPRINT###TFA_VALIDATE_TIME")]
[Index("TfaValue", Name = "IDX_PERSON_FINGERPRINT###TFA_VALUE")]
[Index("State", Name = "IX_SSR_PRS_FNGRPRNT_STATE")]
[Index("LegacyId", Name = "UDX_PERSON_FINGERPRINT###LEGACY_ID", IsUnique = true)]
[Index("UseCasePersonObjectId", Name = "UDX_PERSON_FINGERPRINT###USE_CASE_PERSON_OBJECT_ID", IsUnique = true)]
public partial class PersonFingerprint
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه دفترخانه یا واحد ثبتی
    /// </summary>
    [Required]
    [Column("ORGANIZATION_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string OrganizationId { get; set; }

    /// <summary>
    /// شناسه كاربردی كه اثر انگشت برای آن اخذ شده است
    /// </summary>
    [Required]
    [Column("PERSON_FINGERPRINT_USE_CASE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string PersonFingerprintUseCaseId { get; set; }

    /// <summary>
    /// شناسه ركورد معادلی در كاربرد كه اثر انگشت برای آن گرفته شده است
    /// </summary>
    [Column("USE_CASE_PERSON_OBJECT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string UseCasePersonObjectId { get; set; }

    /// <summary>
    /// شماره ملی شخص
    /// </summary>
    [Required]
    [Column("PERSON_NATIONAL_NO")]
    [StringLength(10)]
    [Unicode(false)]
    public string PersonNationalNo { get; set; }

    /// <summary>
    /// نام و نام خانوادگی شخص
    /// </summary>
    [Column("NAME_FAMILY")]
    [StringLength(200)]
    [Unicode(false)]
    public string NameFamily { get; set; }

    /// <summary>
    /// شناسه انگشتی كه اثر آن گرفته شده است
    /// </summary>
    [Column("PERSON_FINGER_TYPE_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string PersonFingerTypeId { get; set; }

    /// <summary>
    /// آیا كارت ملی هوشمند دارد؟
    /// </summary>
    [Column("SMART_CARD_STATUS")]
    [StringLength(1)]
    [Unicode(false)]
    public string SmartCardStatus { get; set; }

    /// <summary>
    /// تاریخ استعلام كارت ملی هوشمند
    /// </summary>
    [Column("SMART_CARD_REQUEST_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SmartCardRequestDate { get; set; }

    /// <summary>
    /// زمان استعلام كارت ملی هوشمند
    /// </summary>
    [Column("SMART_CARD_REQUEST_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SmartCardRequestTime { get; set; }

    /// <summary>
    /// تاریخ پاسخ استعلام كارت ملی هوشمند
    /// </summary>
    [Column("SMART_CARD_RESPONSE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SmartCardResponseDate { get; set; }

    /// <summary>
    /// زمان پاسخ استعلام كارت ملی هوشمند
    /// </summary>
    [Column("SMART_CARD_RESPONSE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string SmartCardResponseTime { get; set; }

    /// <summary>
    /// آیا تطابق اثر انگشت با كارت ملی هوشمند (MOC) ضروری است؟
    /// </summary>
    [Required]
    [Column("MOC_IS_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string MocIsRequired { get; set; }

    /// <summary>
    /// وضعیت تطابق اثر انگشت با كارت ملی هوشمند (MOC)
    /// </summary>
    [Column("MOC_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string MocState { get; set; }

    /// <summary>
    /// توضیحات تطابق اثر انگشت با كارت ملی هوشمند (MOC)
    /// </summary>
    [Column("MOC_DESCRIPTION")]
    [StringLength(1000)]
    [Unicode(false)]
    public string MocDescription { get; set; }

    /// <summary>
    /// محتوای فایل تصویر اثر انگشت
    /// </summary>
    [Column("FINGERPRINT_IMAGE_FILE", TypeName = "BLOB")]
    public byte[] FingerprintImageFile { get; set; }

    /// <summary>
    /// پسوند فایل تصویر اثر انگشت
    /// </summary>
    [Column("FINGERPRINT_IMAGE_TYPE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FingerprintImageType { get; set; }

    /// <summary>
    /// ارتفاع تصویر خام اثر انگشت
    /// </summary>
    [Column("FINGERPRINT_IMAGE_HEIGHT")]
    [Precision(10)]
    public int? FingerprintImageHeight { get; set; }

    /// <summary>
    /// عرض تصویر خام اثر انگشت
    /// </summary>
    [Column("FINGERPRINT_IMAGE_WIDTH")]
    [Precision(10)]
    public int? FingerprintImageWidth { get; set; }

    /// <summary>
    /// فیچرهای استخراج شده از اثر انگشت
    /// </summary>
    [Column("FINGERPRINT_FEATURES", TypeName = "BLOB")]
    public byte[] FingerprintFeatures { get; set; }

    /// <summary>
    /// نوع دستگاه اخذ اثر انگشت
    /// </summary>
    [Column("FINGERPRINT_SCANNER_DEVICE_TYPE")]
    [StringLength(1000)]
    [Unicode(false)]
    public string FingerprintScannerDeviceType { get; set; }

    /// <summary>
    /// تاریخ اخذ اثر انگشت
    /// </summary>
    [Column("FINGERPRINT_GET_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string FingerprintGetDate { get; set; }

    /// <summary>
    /// زمان اخذ اثر انگشت
    /// </summary>
    [Column("FINGERPRINT_GET_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string FingerprintGetTime { get; set; }

    /// <summary>
    /// شماره موبایل برای احراز هویت دو عاملی
    /// </summary>
    [Column("TFA_MOBILE_NO")]
    [StringLength(11)]
    [Unicode(false)]
    public string TfaMobileNo { get; set; }

    /// <summary>
    /// آیا انجام مرحله اعتبارسنجی عامل دوم، بعنوان پیش نیاز اخذ اثر انگشت ضروری است؟
    /// </summary>
    [Required]
    [Column("TFA_IS_REQUIRED")]
    [StringLength(1)]
    [Unicode(false)]
    public string TfaIsRequired { get; set; }

    /// <summary>
    /// مقدار عامل دوم
    /// </summary>
    [Column("TFA_VALUE")]
    [StringLength(10)]
    [Unicode(false)]
    public string TfaValue { get; set; }

    /// <summary>
    /// تاریخ ارسال عامل دوم
    /// </summary>
    [Column("TFA_SEND_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string TfaSendDate { get; set; }

    /// <summary>
    /// زمان ارسال عامل دوم
    /// </summary>
    [Column("TFA_SEND_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string TfaSendTime { get; set; }

    /// <summary>
    /// تاریخ اعتبارسنجی عامل دوم
    /// </summary>
    [Column("TFA_VALIDATE_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string TfaValidateDate { get; set; }

    /// <summary>
    /// زمان اعتبارسنجی عامل دوم
    /// </summary>
    [Column("TFA_VALIDATE_TIME")]
    [StringLength(8)]
    [Unicode(false)]
    public string TfaValidateTime { get; set; }

    /// <summary>
    /// نتیجه انجام احراز هویت دو عاملی
    /// </summary>
    [Column("TFA_STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string TfaState { get; set; }

    /// <summary>
    /// ملاحظات
    /// </summary>
    [Column("DESCRIPTION")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Description { get; set; }

    /// <summary>
    /// آیا این اثر انگشت، حذف شده بحساب می آید؟
    /// </summary>
    [Required]
    [Column("IS_DELETED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsDeleted { get; set; }

    /// <summary>
    /// Information Lifecycle Management
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// كلید اصلی ركورد معادل در سامانه قدیمی
    /// </summary>
    [Column("LEGACY_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string LegacyId { get; set; }

    /// <summary>
    /// تاریخ ركورد به میلادی
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime? RecordDate { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("USE_CASE_MAIN_OBJECT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string UseCaseMainObjectId { get; set; }

    /// <summary>
    /// تصویر خام اثر انگشت
    /// </summary>
    [Column("FINGERPRINT_RAW_IMAGE", TypeName = "BLOB")]
    public byte[] FingerprintRawImage { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    [Column("STATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string State { get; set; }

    /// <summary>
    /// آیا درخواست مرتبط بصورت غیرحضوری ثبت شده است؟
    /// </summary>
    [Column("IS_REMOTE_REQUEST")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRemoteRequest { get; set; }

    [InverseProperty("PersonFingerprint")]
    public virtual ICollection<InquiryMocLog> InquiryMocLogs { get; set; } = new List<InquiryMocLog>();

    [ForeignKey("PersonFingerTypeId")]
    [InverseProperty("PersonFingerprints")]
    public virtual PersonFingerType PersonFingerType { get; set; }

    [ForeignKey("PersonFingerprintUseCaseId")]
    [InverseProperty("PersonFingerprints")]
    public virtual PersonFingerprintUseCase PersonFingerprintUseCase { get; set; }
}
