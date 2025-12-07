

namespace Notary.SSAA.BO.SharedKernel.SharedModels.DocumentReport
{
    public class ONotaryRegCaseEntity
    {
        public ONotaryRegCaseEntity() 
        {
            OwnershipDocs = new List<ONotaryPersonOwnershipDocEntity>();
        }
        public decimal RegCaseCount { get; set; }
        public string Description { get; set; }
        public string HowToPay { get; set; }
        public string Id { get; set; }
        public string ONotaryAssetTypeId { get; set; }
        public string ONotaryAssetTypeTitle { get; set; }
        public string ONotaryRegisterServiceReqId { get; set; }
        public decimal? Price { get; set; }
        public int? QuotaType { get; set; }
        public string QuotaTypeDesc { get; set; }
        public decimal? SellDetailGrandeeQuota { get; set; }
        public decimal? SellDetailQuota { get; set; }
        public decimal? SellTotalGrandeeQuota { get; set; }
        public decimal? SellTotalQuota { get; set; }
        public string Address { get; set; }
        public decimal? Area { get; set; }
        public string AreaExtended { get; set; }
        public string AreaDescription { get; set; }
        public string BakhshSSAAId { get; set; }
        public string BakhshSSAATitle { get; set; }
        public string BasicPlaque { get; set; }
        public int? BasicPlaqueHasRemain { get; set; }
        public string BasicPlaqueHasRemainDesc { get; set; }
        public string BlockNo { get; set; }
        public string Commons { get; set; }
        public string Direction { get; set; }
        public string DivFromBasicPlaque { get; set; }
        public string DivFromSecondaryPlaque { get; set; }
        public string FloorNo { get; set; }
        public string GeoLocationId { get; set; }
        public string GeoLocationTitle { get; set; }
        public string HozehSSAAId { get; set; }
        public string HozehSSAATitle { get; set; }
        public int? ImmovaleType { get; set; }
        public string ImmovaleTypeDesc { get; set; }
        public string Limits { get; set; }
        public int? LocationType { get; set; }
        public string LocationTypeDesc { get; set; }
        public string NahyehSSAAId { get; set; }
        public string NahyehSSAATitle { get; set; }
        public string OldSaleDescription { get; set; }
        public string ONotaryRegCaseId { get; set; }
        public string ONotaryRegCaseTitle { get; set; }
        public string RegCaseTitle { get; set; }
        public string PieceNo { get; set; }
        public string PlaqueText { get; set; }
        public string PostalCode { get; set; }
        public string Rights { get; set; }
        public int? SecondaryPlaqueHasRemain { get; set; }
        public string SecondaryPlaqueHasRemainDesc { get; set; }
        public string SecondaryPlaqueNo { get; set; }

        public string MeasurementUnitTitle { get; set; }
        public string CountUnitTitle { get; set; }

        public string Parking { get; set; }
        public string Anbari { get; set; }
        public string AttachmentDescription { get; set; }

        public string EvacuationDescription { get; set; }

        public string MunicipalityDate { get; set; }
        public string MunicipalityNo { get; set; }
        public string MunicipalityDescription { get; set; }
        public string SeparationDate { get; set; }
        public string SeparationNo { get; set; }
        public string SeparationDescription { get; set; }
        public string SpecDescriptionInDoc { get; set; }

        public string CaseQuotaDescription { get; set; }
        public string CaseTitle { get; set; }
        public int? WealthType { get; set; }
        public string WealthTypeDesc { get; set; }
        public string HasAsset { get; set; }

        public string VehicleCardNo { get; set; }
        public string VehicleChassisNo { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleCylinderCount { get; set; }
        public string VehicleDutyFicheNo { get; set; }
        public string VehicleEngineCapacity { get; set; }
        public string VehicleEngineNo { get; set; }
        public string VehicleFuelCardNo { get; set; }
        public string VehicleNumberingLocation { get; set; }
        public string VehicleOtherInfo { get; set; }
        public string VehiclePlaqueNo { get; set; }
        public string VehicleInssuranceCo { get; set; }
        public string VehicleInssuranceNo { get; set; }
        public string VehicleOldDocumentDate { get; set; }
        public string VehicleOldDocumentIssuer { get; set; }
        public string VehicleOldDocumentNo { get; set; }
        public string VehicleOwnershipPrintedDocNo { get; set; }
        public string VehiclePlaqueSeller { get; set; }
        public string VehiclePlaqueBuyer { get; set; }

        public string VehiclePlaqueSeri { get; set; }
        public decimal? VehiclePlaqueNo1Buyer { get; set; }
        public decimal? VehiclePlaqueNo1Seller { get; set; }
        public decimal? VehiclePlaqueNo2Buyer { get; set; }
        public decimal? VehiclePlaqueNo2Seller { get; set; }
        public string VehiclePlaqueNoAlphaBuyer { get; set; }
        public string VehiclePlaqueNoAlphaSeller { get; set; }
        public decimal? VehiclePlaqueSeriBuyer { get; set; }
        public decimal? VehiclePlaqueSeriSeller { get; set; }

        public string AttachmentDescriptionRegCase { get; set; }
        public string AttachmentSpecifications { get; set; }
        public string AttachmentText { get; set; }
        public int? AttachmentType { get; set; }
        public string AttachmentTypeOthers { get; set; }
        public int? IsAttachment { get; set; }

        public string ReceiverBasicPlaque { get; set; }
        public int? ReceiverBasicPlqHasRemain { get; set; }
        public string ReceiverDivFromBasicPlaque { get; set; }
        public string ReceiverDivFromScndryPlaque { get; set; }
        public string ReceiverPlaqueText { get; set; }
        public int? ReceiverScndryPlqHasRemain { get; set; }
        public string ReceiverSecondaryPlaqueNo { get; set; }
        public string RemortageText { get; set; }
        public string AttachmentMelk { get; set; }


        public string MadeInYear { get; set; }
        public List<ONotaryPersonOwnershipDocEntity> OwnershipDocs { get; set; }
    }
}
