
using Notary.SSAA.BO.DataTransferObject.Bases;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class DocumentEstateViewModel : EntityState
    {
        /// <summary>
        /// عرصه يا اعيان در ثمنيه يا ربعيه
        /// </summary>
        public string SomnyehRobeyehFieldGrandee { get; set; }
        /// <summary>
        /// نوع استثناء - ثمنيه يا ربعيه
        /// </summary>
        public string GrandeeExceptionType { get; set; }

        public string EstateId { get; set; }

        public string DocumentId { get; set; }

        public string RowNo { get; set; }

        public bool? IsRegistered { get; set; }
        public string BasicPlaque { get; set; }
        public string SecondaryPlaque { get; set; }
        public string PlaqueText { get; set; }
        public string BasicPlaqueHasRemain { get; set; }
        public string SecondaryPlaqueHasRemain { get; set; }
        public string DivFromBasicPlaque { get; set; }
        public string DivFromSecondaryPlaque { get; set; }
        public string ImmovaleType { get; set; }
        public string LocationType { get; set; }
        public string Piece { get; set; }
        public string Block { get; set; }
        public string Floor { get; set; }
        public string Direction { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string AreaDescription { get; set; }
        public string Commons { get; set; }
        public string Rights { get; set; }
        public string Limits { get; set; }
        public string RegisterCaseTitle { get; set; }
        public bool? IsEvacuated { get; set; }
        public string EvacuationPapers { get; set; }
        public string EvacuatedDate { get; set; }
        public string EvacuationDescription { get; set; }
        public string OldSaleDescription { get; set; }
        public bool? IsRemortage { get; set; }
        public bool? IsProportionateQuota { get; set; }
        public bool? IsFacilitationLaw { get; set; }
        public string Price { get; set; }
        public string EstateInquiryId { get; set; }
        public string RegionalPrice { get; set; }
        public string FieldOrGrandee { get; set; }
        public string OwnershipType { get; set; }
        public string MunicipalityNo { get; set; }
        public string MunicipalityDate { get; set; }
        public string MunicipalityIssuer { get; set; }
        public string CommitmentPrice { get; set; }
        public string SeparationDescription { get; set; }
        public string SeparationText { get; set; }
        public string SeparationType { get; set; }
        public string SeparationNo { get; set; }
        public string SeparationDate { get; set; }
        public string SeparationIssuer { get; set; }
        public bool? IsAttachment { get; set; }
        public string AttachmentType { get; set; }
        public string AttachmentDescription { get; set; }
        public string AttachmentSpecifications { get; set; }
        public string AttachmentTypeOthers { get; set; }
        public string ReceiverBasicPlaque { get; set; }
        public string ReceiverSecondaryPlaque { get; set; }
        public string ReceiverPlaqueText { get; set; }
        public string ReceiverBasicPlaqueHasRemain { get; set; }
        public string ReceiverSecondaryPlaqueHasRemain { get; set; }
        public string ReceiverDivFromBasicPlaque { get; set; }
        public string ReceiverDivFromSecondaryPlaque { get; set; }
        public string GrandeeExceptionDetailQuota { get; set; }
        public string GrandeeExceptionTotalQuota { get; set; }
        public string SomnyehRobeyehActionType { get; set; }
        public string OwnershipDetailQuota { get; set; }
        public string OwnershipTotalQuota { get; set; }
        public string SellDetailQuota { get; set; }
        public string SellTotalQuota { get; set; }
        public string QuotaText { get; set; }
        public string Description { get; set; }
        public string Ilm { get; set; }
        //حوزه ثبتی 
        public IList<string> DocumentEstateSubSectionId { get; set; }
        //بخش ثبتی
        public IList<string> DocumentEstateSectionId { get; set; }
        //ناحیه ثبتی
        public IList<string> DocumentUnitId { get; set; }
        //نوع
        public IList<string> DocumentEstateTypeId { get; set; }
        //محل جغرافیایی
        public IList<string> GeoLocationId { get; set; }
        public IList<DocumentEstateOwnerShipViewModel> DocumentEstateOwnerShips { get; set; }
        public IList<DocumentEstateAttachmentViewModel> DocumentEstateAttachments { get; set; }
        public IList<DocumentEstateSeparationPieceViewModel> DocumentEstateSeparationPieces { get; set; }
        public IList<DocumentEstateQuotumViewModel> DocumentEstateQuotums { get; set; }
        public IList<DocumentEstateQuotaDetailViewModel> DocumentEstateQuotaDetails { get; set; }


    }

}
