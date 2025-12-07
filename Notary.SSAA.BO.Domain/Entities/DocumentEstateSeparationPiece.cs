using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Domain.Entities;

/// <summary>
/// قطعات تقسیم شده در اسناد تقسیم نامه ملكی
/// </summary>
[Table("DOCUMENT_ESTATE_SEPARATION_PIECES")]
[Index("AnbariId", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###ANBARI_ID")]
[Index("DocumentEstateId", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###DOCUMENT_ESTATE_ID")]
[Index("DocumentEstateSeparationPieceKindId", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###DOCUMENT_ESTATE_SEPARATION_PIECE_KIND_ID")]
[Index("DocumentId", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###DOCUMENT_ID")]
[Index("EstatePieceTypeId", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###ESTATE_PIECE_TYPE_ID")]
[Index("EstateSectionId", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###ESTATE_SECTION_ID")]
[Index("EstateSubsectionId", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###ESTATE_SUBSECTION_ID")]
[Index("HasOwner", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###HAS_OWNER")]
[Index("Ilm", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###ILM")]
[Index("MeasurementUnitTypeId", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###MEASUREMENT_UNIT_TYPE_ID")]
[Index("ParkingId", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###PARKING_ID")]
[Index("PieceNo", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###PIECE_NO")]
[Index("ScriptoriumId", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###SCRIPTORIUM_ID")]
[Index("UnitId", Name = "IDX_DOCUMENT_ESTATE_SEPARATION_PIECES###UNIT_ID")]
[Index("LegacyId", Name = "UDX_DOCUMENT_ESTATE_SEPARATION_PIECES###LEGACY_ID", IsUnique = true)]
public partial class DocumentEstateSeparationPiece
{
    /// <summary>
    ///  ردیف
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
    /// شناسه املاك ثبت شده در اسناد
    /// </summary>
    [Column("DOCUMENT_ESTATE_ID")]
    public Guid? DocumentEstateId { get; set; }

    /// <summary>
    /// شناسه جنس قطعه در سامانه ثبت آنی
    /// </summary>
    [Required]
    [Column("DOCUMENT_ESTATE_SEPARATION_PIECE_KIND_ID")]
    [StringLength(2)]
    [Unicode(false)]
    public string DocumentEstateSeparationPieceKindId { get; set; }

    /// <summary>
    /// شناسه حوزه ثبتی
    /// </summary>
    [Column("UNIT_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string UnitId { get; set; }

    /// <summary>
    /// شناسه بخش ثبتی
    /// </summary>
    [Column("ESTATE_SECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSectionId { get; set; }

    /// <summary>
    /// شناسه ناحیه ثبتی
    /// </summary>
    [Column("ESTATE_SUBSECTION_ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string EstateSubsectionId { get; set; }

    /// <summary>
    /// شماره قطعه
    /// </summary>
    [Column("PIECE_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string PieceNo { get; set; }

    /// <summary>
    /// شناسه نوع قطه در سامانه املاك
    /// </summary>
    [Column("ESTATE_PIECE_TYPE_ID")]
    [StringLength(4)]
    [Unicode(false)]
    public string EstatePieceTypeId { get; set; }

    /// <summary>
    /// بلوك
    /// </summary>
    [Column("BLOCK")]
    [StringLength(50)]
    [Unicode(false)]
    public string Block { get; set; }

    /// <summary>
    /// طبقه
    /// </summary>
    [Column("FLOOR")]
    [StringLength(100)]
    [Unicode(false)]
    public string Floor { get; set; }

    /// <summary>
    /// سمت
    /// </summary>
    [Column("DIRECTION")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Direction { get; set; }

    /// <summary>
    /// مساحت
    /// </summary>
    [Column("AREA")]
    [StringLength(500)]
    [Unicode(false)]
    public string Area { get; set; }

    /// <summary>
    /// شناسه واحد اندازه گیری مساحت
    /// </summary>
    [Column("MEASUREMENT_UNIT_TYPE_ID")]
    [StringLength(3)]
    [Unicode(false)]
    public string MeasurementUnitTypeId { get; set; }

    /// <summary>
    /// آیا این قطعه مالك دارد؟
    /// </summary>
    [Column("HAS_OWNER")]
    [StringLength(1)]
    [Unicode(false)]
    public string HasOwner { get; set; }

    /// <summary>
    /// پلاك ثبتی اصلی
    /// </summary>
    [Column("BASIC_PLAQUE")]
    [StringLength(100)]
    [Unicode(false)]
    public string BasicPlaque { get; set; }

    /// <summary>
    /// پلاك ثبتی فرعی
    /// </summary>
    [Column("SECONDARY_PLAQUE")]
    [StringLength(100)]
    [Unicode(false)]
    public string SecondaryPlaque { get; set; }

    /// <summary>
    /// پلاك متنی
    /// </summary>
    [Column("PLAQUE_TEXT")]
    [StringLength(500)]
    [Unicode(false)]
    public string PlaqueText { get; set; }

    /// <summary>
    /// مفروز و مجزی از پلاك اصلی
    /// </summary>
    [Column("DIVIDED_FROM_BASIC_PLAQUE")]
    [StringLength(100)]
    [Unicode(false)]
    public string DividedFromBasicPlaque { get; set; }

    /// <summary>
    /// مفروز و مجزی از پلاك فرعی
    /// </summary>
    [Column("DIVIDED_FROM_SECONDARY_PLAQUE")]
    [StringLength(100)]
    [Unicode(false)]
    public string DividedFromSecondaryPlaque { get; set; }

    /// <summary>
    /// حد شمالی
    /// </summary>
    [Column("NORTH_LIMITS", TypeName = "CLOB")]
    public string NorthLimits { get; set; }

    /// <summary>
    /// حد جنوبی
    /// </summary>
    [Column("SOUTH_LIMITS", TypeName = "CLOB")]
    public string SouthLimits { get; set; }

    /// <summary>
    /// حد شرقی
    /// </summary>
    [Column("EASTERN_LIMITS", TypeName = "CLOB")]
    public string EasternLimits { get; set; }

    /// <summary>
    /// حد غربی
    /// </summary>
    [Column("WESTERN_LIMITS", TypeName = "CLOB")]
    public string WesternLimits { get; set; }

    /// <summary>
    /// مشاعات و مشتركات
    /// </summary>
    [Column("COMMONS", TypeName = "CLOB")]
    public string Commons { get; set; }

    /// <summary>
    /// حقوق ارتفاقی
    /// </summary>
    [Column("RIGHTS", TypeName = "CLOB")]
    public string Rights { get; set; }

    /// <summary>
    /// شناسه قطعه ای كه این پاركینگ به آن منضم شده است
    /// </summary>
    [Column("PARKING_ID")]
    public Guid? ParkingId { get; set; }

    /// <summary>
    /// شناسه قطعه ای كه این انباری به آن منضم شده است
    /// </summary>
    [Column("ANBARI_ID")]
    public Guid? AnbariId { get; set; }

    /// <summary>
    /// سایر منضمات به غیر از پاركینگ و انباری
    /// </summary>
    [Column("OTHER_ATTACHMENTS", TypeName = "CLOB")]
    public string OtherAttachments { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    [Column("DESCRIPTION", TypeName = "CLOB")]
    public string Description { get; set; }

    /// <summary>
    /// شناسه دفترخانه صادركننده سند
    /// </summary>
    [Required]
    [Column("SCRIPTORIUM_ID")]
    [StringLength(5)]
    [Unicode(false)]
    public string ScriptoriumId { get; set; }

    /// <summary>
    /// وضعیت ركورد از لحاظ آرشیو
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
    [StringLength(50)]
    [Unicode(false)]
    public string LegacyId { get; set; }

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
    [Column("OLD_DOCUMENT_ESTATE_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldDocumentEstateId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_ESTATE_SECTION_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldEstateSectionId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_ESTATE_SUBSECTION_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldEstateSubsectionId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_PARKING_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldParkingId { get; set; }

    /// <summary>
    /// شناسه رکورد در جدول متناظر
    /// </summary>
    [Column("OLD_ANBARI_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string OldAnbariId { get; set; }

    [ForeignKey("AnbariId")]
    [InverseProperty("InverseAnbari")]
    public virtual DocumentEstateSeparationPiece Anbari { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("DocumentEstateSeparationPieces")]
    public virtual Document Document { get; set; }

    [ForeignKey("DocumentEstateId")]
    [InverseProperty("DocumentEstateSeparationPieces")]
    public virtual DocumentEstate DocumentEstate { get; set; }

    [ForeignKey("DocumentEstateSeparationPieceKindId")]
    [InverseProperty("DocumentEstateSeparationPieces")]
    public virtual DocumentEstateSeparationPieceKind DocumentEstateSeparationPieceKind { get; set; }

    [InverseProperty("DocumentEstateSeparationPieces")]
    public virtual ICollection<DocumentEstateSeparationPiecesQuotum> DocumentEstateSeparationPiecesQuota { get; set; } = new List<DocumentEstateSeparationPiecesQuotum>();

    [ForeignKey("EstatePieceTypeId")]
    [InverseProperty("DocumentEstateSeparationPieces")]
    public virtual EstatePieceType EstatePieceType { get; set; }

    [ForeignKey("EstateSectionId")]
    [InverseProperty("DocumentEstateSeparationPieces")]
    public virtual EstateSection EstateSection { get; set; }

    [ForeignKey("EstateSubsectionId")]
    [InverseProperty("DocumentEstateSeparationPieces")]
    public virtual EstateSubsection EstateSubsection { get; set; }

    [InverseProperty("Anbari")]
    public virtual ICollection<DocumentEstateSeparationPiece> InverseAnbari { get; set; } = new List<DocumentEstateSeparationPiece>();

    [InverseProperty("Parking")]
    public virtual ICollection<DocumentEstateSeparationPiece> InverseParking { get; set; } = new List<DocumentEstateSeparationPiece>();

    [ForeignKey("ParkingId")]
    [InverseProperty("InverseParking")]
    public virtual DocumentEstateSeparationPiece Parking { get; set; }
}
