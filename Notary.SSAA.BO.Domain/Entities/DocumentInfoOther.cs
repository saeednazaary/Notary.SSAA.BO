using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// سایر اقلام اطلاعاتی بسته به نوع اسناد رسمی
/// </summary>
[Table("DOCUMENT_INFO_OTHER")]
[Index("DocumentAssetTypeId", Name = "IDX_DOCUMENT_INFO_OTHER###DOCUMENT_ASSET_TYPE_ID")]
[Index("DocumentTypeSubjectId", Name = "IDX_DOCUMENT_INFO_OTHER###DOCUMENT_TYPE_SUBJECT_ID")]
[Index("ExecutiveReceiverUnitId", Name = "IDX_DOCUMENT_INFO_OTHER###EXECUTIVE_RECEIVER_UNIT_ID")]
[Index("ExecutiveTypeId", Name = "IDX_DOCUMENT_INFO_OTHER###EXECUTIVE_TYPE_ID")]
[Index("Ilm", Name = "IDX_DOCUMENT_INFO_OTHER###ILM")]
[Index("IsEstateRegistered", Name = "IDX_DOCUMENT_INFO_OTHER###IS_ESTATE_REGISTERED")]
[Index("IsRelatedToDivisionCommission", Name = "IDX_DOCUMENT_INFO_OTHER###IS_RELATED_TO_DIVISION_COMMISSION")]
[Index("MortageTimeUnitId", Name = "IDX_DOCUMENT_INFO_OTHER###MORTAGE_TIME_UNIT_ID")]
[Index("RecordDate", Name = "IDX_DOCUMENT_INFO_OTHER###RECORD_DATE")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_INFO_OTHER###SCRIPTORIUM_ID")]
[Index("XexecutiveId", Name = "IDX_DOCUMENT_INFO_OTHER###XEXECUTIVE_ID")]
[Index("XexecutiveOldId", Name = "IDX_DOCUMENT_INFO_OTHER###XEXECUTIVE_OLD_ID")]
[Index("DocumentId", Name = "UX_SSR_DC_OTR_DID", IsUnique = true)]
public partial class DocumentInfoOther
{
    /// <summary>
    /// شناسه
    /// </summary>
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// شناسه سند
    /// </summary>
    [Column("DOCUMENT_ID")]
    public Guid DocumentId { get; set; }

    /// <summary>
    /// شناسه انواع موضوعات اسناد وكالتنامه
    /// </summary>
    [Column("DOCUMENT_TYPE_SUBJECT_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string DocumentTypeSubjectId { get; set; }

    /// <summary>
    /// آیا سند مدت دار است؟
    /// </summary>
    [Column("HAS_TIME")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasTime { get; set; }

    /// <summary>
    /// تاریخ و زمان مختومه شدن اجرائیه
    /// </summary>
    [Column("END_DATE_TIME")]
    [StringLength(16)]
    [Unicode(false)]
    public string EndDateTime { get; set; }

    /// <summary>
    /// در این وكالتنامه حق توكیل به غیر دارد؟
    /// </summary>
    [Column("HAS_ADVOCACY_TO_OTHERS_PERMIT")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasAdvocacyToOthersPermit { get; set; }

    /// <summary>
    /// در این وكالتنامه آیا حق انجام مورد وكالت به كرات را دارد؟
    /// </summary>
    [Column("HAS_MULTIPLE_ADVOCACY_PERMIT")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasMultipleAdvocacyPermit { get; set; }

    /// <summary>
    /// در این وكالتنامه آیا حق عزل وكیل دارد؟
    /// </summary>
    [Column("HAS_DISMISSAL_PERMIT")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasDismissalPermit { get; set; }

    /// <summary>
    /// تاریخ پایان مدت وكالتنامه
    /// </summary>
    [Column("ADVOCACY_END_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string AdvocacyEndDate { get; set; }

    /// <summary>
    /// مدت اجاره
    /// </summary>
    [Column("RENT_DURATION")]
    [StringLength(300)]
    [Unicode(false)]
    public string RentDuration { get; set; }

    /// <summary>
    /// تاریخ شروع اجاره
    /// </summary>
    [Column("RENT_START_DATE")]
    [StringLength(10)]
    [Unicode(false)]
    public string RentStartDate { get; set; }

    /// <summary>
    /// در اسناد اجاره غیرمنقول آیا اجاره با حق سرقفلی است؟
    /// </summary>
    [Column("IS_RENT_WITH_SARGHOFLI")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRentWithSarghofli { get; set; }

    /// <summary>
    /// در اسناد صلح، آیا صلح عمری است؟
    /// </summary>
    [Column("IS_PEACE_FOR_LIFETIME")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsPeaceForLifetime { get; set; }

    /// <summary>
    /// مدت صلح
    /// </summary>
    [Column("PEACE_DURATION")]
    [StringLength(300)]
    [Unicode(false)]
    public string PeaceDuration { get; set; }

    /// <summary>
    /// آیا سند كاداستری است؟
    /// </summary>
    [Column("IS_KADASTR")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsKadastr { get; set; }

    /// <summary>
    /// آیا این سند مربوط به املاك ثبت شده است؟
    /// </summary>
    [Column("IS_ESTATE_REGISTERED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsEstateRegistered { get; set; }

    /// <summary>
    /// آیا این سند مربوط به كمیسیون تقسیم اسناد دولتی می شود؟
    /// </summary>
    [Column("IS_RELATED_TO_DIVISION_COMMISSION")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsRelatedToDivisionCommission { get; set; }

    /// <summary>
    /// آیا سند مختصر است؟
    /// </summary>
    [Column("IS_DOCUMENT_BRIEF")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsDocumentBrief { get; set; }

    /// <summary>
    /// در اسناد تقسیم نامه، تعداد واحد تقسیم
    /// </summary>
    [Column("DIVIDED_SECTIONS_COUNT")]
    [Precision(4)]
    public byte? DividedSectionsCount { get; set; }

    /// <summary>
    /// در سایر خدمات ثبتی، تعداد
    /// </summary>
    [Column("REGISTER_COUNT")]
    [Precision(10)]
    public int? RegisterCount { get; set; }

    /// <summary>
    /// نوع وقف
    /// </summary>
    [Column("VAGHF_TYPE")]
    [Precision(6)]
    public int? VaghfType { get; set; }

    /// <summary>
    /// نوع حق انتفاع
    /// </summary>
    [Column("HAGHE_ENTEFAE")]
    [StringLength(1)]
    [Unicode(false)]
    public string HagheEntefae { get; set; }

    /// <summary>
    /// تعداد برگ برای محاسبه حق التحریر
    /// </summary>
    [Column("PAPER_COUNT")]
    [Precision(4)]
    public byte? PaperCount { get; set; }

    /// <summary>
    /// شناسه واحد زمان مدت رهن
    /// </summary>
    [Column("MORTAGE_TIME_UNIT_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string MortageTimeUnitId { get; set; }

    /// <summary>
    /// مدت رهن
    /// </summary>
    [Column("MORTAGE_DURATION")]
    [Precision(16)]
    public long? MortageDuration { get; set; }

    /// <summary>
    /// شناسه انواع سایر اموال منقول
    /// </summary>
    [Column("DOCUMENT_ASSET_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string DocumentAssetTypeId { get; set; }

    /// <summary>
    /// عنوان اجرائیه
    /// </summary>
    [Column("TITLE", TypeName = "CLOB")]
    public string Title { get; set; }

    /// <summary>
    /// علت تقاضای صدور اجرائیه
    /// </summary>
    [Column("APPLICATION_REASON", TypeName = "CLOB")]
    public string ApplicationReason { get; set; }

    /// <summary>
    /// شناسه انواع اجرائیه
    /// </summary>
    [Column("EXECUTIVE_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string ExecutiveTypeId { get; set; }

    /// <summary>
    /// شناسه اجرائیه
    /// </summary>
    [Column("XEXECUTIVE_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string XexecutiveId { get; set; }

    /// <summary>
    /// شناسه اجرائیه قبلی اصلاح شده
    /// </summary>
    [Column("XEXECUTIVE_OLD_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string XexecutiveOldId { get; set; }

    /// <summary>
    /// شناسه واحد ثبتی دریافت كننده تقاضانامه صدور اجرائیه
    /// </summary>
    [Column("EXECUTIVE_RECEIVER_UNIT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ExecutiveReceiverUnitId { get; set; }

    /// <summary>
    /// شناسه دفترخانه
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده
    /// </summary>
    [Column("RECORD_DATE", TypeName = "DATE")]
    public DateTime RecordDate { get; set; }

    /// <summary>
    /// Information Lifecycle Management
    /// </summary>
    [Required]
    [Column("ILM")]
    [StringLength(1)]
    [Unicode(false)]
    public string Ilm { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_ASSET_TYPE_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentAssetTypeId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_DOCUMENT_TYPE_SUBJECT_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentTypeSubjectId { get; set; }

    /// <summary>
    /// آیا سرویس اخذ اطلاعات قطعات صورتمجلس تفکیکی فراخوانی شده است؟
    /// </summary>
    [Column("IS_SEPARATION_PIECES_SERVICE_CALLED")]
    [StringLength(1)]
    [Unicode(false)]
    public string IsSeparationPiecesServiceCalled { get; set; }

    /// <summary>
    /// تاریخ و زمان فراخوانی سرویس اخذ اطلاعات قطعات صورتمجلس تفکیکی
    /// </summary>
    [Column("SEPARATION_PIECES_SERVICE_CALL_DATE_TIME")]
    [StringLength(50)]
    [Unicode(false)]
    public string SeparationPiecesServiceCallDateTime { get; set; }

    /// <summary>
    /// تعداد قطعات صورتمجلس تفکیکی
    /// </summary>
    [Column("NUMBER_OF_SEPARATION_PIECES")]
    [Precision(8)]
    public int? NumberOfSeparationPieces { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentInfoOther")]
    public virtual Document Document { get; set; }

    [ForeignKey("DocumentAssetTypeId")]
    [InverseProperty("DocumentInfoOthers")]
    public virtual DocumentAssetType DocumentAssetType { get; set; }

    [ForeignKey("DocumentTypeSubjectId")]
    [InverseProperty("DocumentInfoOthers")]
    public virtual DocumentTypeSubject DocumentTypeSubject { get; set; }

    [ForeignKey("ExecutiveTypeId")]
    [InverseProperty("DocumentInfoOthers")]
    public virtual ExecutiveType ExecutiveType { get; set; }
}
