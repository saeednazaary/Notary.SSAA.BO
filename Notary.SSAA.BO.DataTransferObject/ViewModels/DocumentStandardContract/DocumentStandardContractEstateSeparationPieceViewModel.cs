
using SixLabors.ImageSharp.ColorSpaces;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractEstateSeparationPieceViewModel : EntityState
    {

        public string EstateSeparationPieceId { get; set; } 
        public string DocumentEstateId { get; set; } 

        // مشخصات قطعه انتخاب شده
        // توضیحات
        public string Description { get; set; } 
        // شماره قطعه
        public string PieceNo { get; set; } 
        // طبقه
        public string FloorNo { get; set; } 
        // مساحت
        public string Area { get; set; } 
        // پلاک ثبتی اصلی
        public string BasicPlaque { get; set; } 
        // پلاک ثبتی فرعی
        public string SecondaryPlaqueNo { get; set; } 
        // مفروز و مجزی از اصلی
        public string DivFromBasicPlaque { get; set; } 
        // مفروز و مجزی از فرعی
        public string DivFromSecondaryPlaque { get; set; } 
        // مشاعات و مشترکات
        public string Commons { get; set; } 
        // حقوق ارتفاقی
        public string Rights { get; set; } 
        // سمت
        public string Direction { get; set; } 
        // پلاک متنی
        public string PlaqueText { get; set; } 

        // نوع قطعه
        public IList<string> PieceTypeId { get; set; }
        public string PieceTypeTitle { get; set; }
        // نوع قطعه
        public string PieceKindId { get; set; }
        public string PieceKindTitle { get; set; }
        // واحد مساحت
        public List<string> MeasurementUnitTypeId { get; set; }
        // این قطعه مالک دارد؟
        public string HasOwner { get; set; } 
        public string HasOwnerTitle { get; set; }

        // حدود
        // حد شمالی
        public string NorthLimits { get; set; } 
        // حد شرقی
        public string EasternLimits { get; set; } 
        // حد جنوبی
        public string SouthLimits { get; set; } 
        // حد غربی
        public string WesternLimits { get; set; }
        // منضمات قطعات انتخاب شده
        //// پارکینگ
        public IList<string> DocumentSeparationPiecesParkings { get; set; }
        //// انباری
        public IList<string> DocumentSeparationPiecesAnbaries { get; set; }
        //// پارکینگ
        //public IList<DocumentSeparationPiecesParkingViewModel> DocumentSeparationPiecesParkings { get; set; } 
        //// انباری
        //public IList<DocumentSeparationPiecesAnbariViewModel> DocumentSeparationPiecesAnbaries { get; set; }
        // قطعه ای که پارکینگ متعلق به آن است
        public IList<string> ParkingId { get; set; }
        // قطعه ای که انباری متعلق به آن است
        public IList<string> AnbariId { get; set; }
        public IList<DocumentStandardContractEstateSeparationPiecesQuotaViewModel> DocumentEstateSeparationPiecesQuotaList { get; set; }
    }
    public class DocumentStandardContractSeparationPiecesParkingViewModel : EntityState
    {

        public string EstateSeparationPieceId { get; set; }
        public string DocumentEstateId { get; set; }

        // مشخصات قطعه انتخاب شده
        // توضیحات
        public string Description { get; set; }
        // شماره قطعه
        public string PieceNo { get; set; }
        // طبقه
        public string FloorNo { get; set; }
        // مساحت
        public string Area { get; set; }
        // پلاک ثبتی اصلی
        public string BasicPlaque { get; set; }
        // پلاک ثبتی فرعی
        public string SecondaryPlaqueNo { get; set; }
        // مفروز و مجزی از اصلی
        public string DivFromBasicPlaque { get; set; }
        // مفروز و مجزی از فرعی
        public string DivFromSecondaryPlaque { get; set; }
        // مشاعات و مشترکات
        public string Commons { get; set; }
        // حقوق ارتفاقی
        public string Rights { get; set; }
        // سمت
        public string Direction { get; set; }
        // پلاک متنی
        public string PlaqueText { get; set; }

        // نوع قطعه
        public string PieceTypeId { get; set; }
        public string PieceTypeTitle { get; set; }
        // نوع قطعه
        public string PieceKindId { get; set; }
        public string PieceKindTitle { get; set; }
        // واحد مساحت
        public List<string> MeasurementUnitTypeId { get; set; }
        // این قطعه مالک دارد؟
        public string HasOwner { get; set; }

        // حدود
        // حد شمالی
        public string NorthLimits { get; set; }
        // حد شرقی
        public string EasternLimits { get; set; }
        // حد جنوبی
        public string SouthLimits { get; set; }
        // حد غربی
        public string WesternLimits { get; set; }
        // منضمات قطعات انتخاب شده

        // قطعه ای که پارکینگ متعلق به آن است
        public IList<string> ParkingId { get; set; }
        // قطعه ای که انباری متعلق به آن است
        public IList<string> AnbariId { get; set; }
        public IList<DocumentStandardContractEstateSeparationPiecesQuotaViewModel> DocumentEstateSeparationPiecesQuotaList { get; set; }
    }

    public class DocumentStandardContractSeparationPiecesAnbariViewModel : EntityState
    {

        public string EstateSeparationPieceId { get; set; }
        public string DocumentEstateId { get; set; }

        // مشخصات قطعه انتخاب شده
        // توضیحات
        public string Description { get; set; }
        // شماره قطعه
        public string PieceNo { get; set; }
        // طبقه
        public string FloorNo { get; set; }
        // مساحت
        public string Area { get; set; }
        // پلاک ثبتی اصلی
        public string BasicPlaque { get; set; }
        // پلاک ثبتی فرعی
        public string SecondaryPlaqueNo { get; set; }
        // مفروز و مجزی از اصلی
        public string DivFromBasicPlaque { get; set; }
        // مفروز و مجزی از فرعی
        public string DivFromSecondaryPlaque { get; set; }
        // مشاعات و مشترکات
        public string Commons { get; set; }
        // حقوق ارتفاقی
        public string Rights { get; set; }
        // سمت
        public string Direction { get; set; }
        // پلاک متنی
        public string PlaqueText { get; set; }

        // نوع قطعه
        public string PieceTypeId { get; set; }
        public string PieceTypeTitle { get; set; }
        // نوع قطعه
        public string PieceKindId { get; set; }
        public string PieceKindTitle { get; set; }
        // واحد مساحت
        public List<string> MeasurementUnitTypeId { get; set; }
        // این قطعه مالک دارد؟
        public string HasOwner { get; set; }

        // حدود
        // حد شمالی
        public string NorthLimits { get; set; }
        // حد شرقی
        public string EasternLimits { get; set; }
        // حد جنوبی
        public string SouthLimits { get; set; }
        // حد غربی
        public string WesternLimits { get; set; }
        // منضمات قطعات انتخاب شده
        // قطعه ای که پارکینگ متعلق به آن است
        public IList<string> ParkingId { get; set; }
        // قطعه ای که انباری متعلق به آن است
        public IList<string> AnbariId { get; set; }
    }
    public class DocumentStandardContractEstateSeparationPiecesQuotaViewModel : EntityState
    {

        public string EstateSeparationPieceId { get; set; } 
        public string EstateSeparationPiecesQuotaId { get; set; } 

        // مالک
        public IList<string> PersonSeparationId { get; set; } 
        public string PersonName { get; set; }
        public string PersonFamily { get; set; }
        public string PersonFullName => PersonName+PersonFamily;
        public string PersonNationalNo { get; set; }
        // متن شرط برای خلاصه معامله شخص
        public string DSUPersonConditionText { get; set; } 
        public string DetailQuota { get; set; } 
        public string TotalQuota { get; set; } 
        // متن سهم
        public string QuotaText { get; set; } 
    }



}
