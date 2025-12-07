using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate
{
    public class EstateSeparationDocumentRelatedData
    {
        public EstateSeparationDocumentRelatedData()
        {
            DocumentEstateSeparationPieceList = new List<EstateSeparationPiece>();
        }
        public string SeparationDescription { get; set; }
        public string SeparationType { get; set; }
        public string SeparationNo { get; set; }
        public string SeparationDate { get; set; }
        public string SeparationIssuer { get; set; }
        public string SeparationText { get; set; }

        public List<EstateSeparationPiece> DocumentEstateSeparationPieceList { get; set; }
    }

    public class EstateSeparationPiece
    {
        public string DocumentEstateSeparationPieceKindId { get; set; }
        public string UnitId { get; set; }
        public string EstateSectionId { get; set; }
        public string EstateSubsectionId { get; set; }
        public string PieceNo { get; set; }
        public string EstatePieceTypeId { get; set; }
        public string EstatePieceTypeTitle { get; set; }
        public string Block { get; set; }
        public string Floor { get; set; }
        public string Direction { get; set; }
        public string Area { get; set; }
        public string MeasurementUnitTypeId { get; set; }
        public string MeasurementUnitTypeTitle { get; set; }
        public string HasOwner { get; set; }
        public string HasOwnerTitle { get; set; }
        public string BasicPlaque { get; set; }
        public string SecondaryPlaque { get; set; }
        public string PlaqueText { get; set; }
        public string DividedFromSecondaryPlaque { get; set; }
        public string NorthLimits { get; set; }
        public string SouthLimits { get; set; }
        public string EasternLimits { get; set; }
        public string WesternLimits { get; set; }
        public string OtherAttachments { get; set; }
        public string Rights { get; set; }
        public string Description { get; set; }
        public string ScriptoriumId { get; set; }
        public string Ilm { get; set; }
    }
}
