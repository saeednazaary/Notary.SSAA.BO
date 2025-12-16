using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry
{
    public class EstateTaxInquiryExtraParam
    {
        public EstateTaxInquiryExtraParam()
        {

        }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanSend { get; set; }
        public bool CanCancel { get; set; }
        public string RelatedServer { get; set; }
    }
    public class EstateTaxInquiryViewModel : EntityState
    {
        public string InquiryId { get; set; }
        public decimal? InquiryApartmentArea { get; set; }
        public decimal? InquiryArsehArea { get; set; }
        public string InquiryAvenue { get; set; }
        public decimal? InquiryBuildingOld { get; set; }
        public IList<string> InquiryBuildingUsingTypeId { get; set; }
        public string InquiryCertificateNo { get; set; }
        public string InquiryCessionDate { get; set; }
        public decimal? InquiryCessionPrice { get; set; }
        public string InquiryCreateDate { get; set; }
        public string InquiryCreateTime { get; set; }
        public string InquiryEstateAddress { get; set; }
        public IList<string> EstateInquiryId { get; set; }
        public int EstateInquiryTimeStamp { get; set; }
        public string InquiryEstatePostCode { get; set; }
        public IList<string> InquiryEstateSectionId { get; set; }
        public IList<string> InquiryEstateSubSectionId { get; set; }
        public IList<string> InquiryEstateUnitId { get; set; }
        public string InquiryEstateSector { get; set; }
        public IList<string> InquiryEstateTaxCityId { get; set; }
        public IList<string> InquiryEstateTaxCountyId { get; set; }
        public IList<string> InquiryEstateTaxProvinceId { get; set; }
        public IList<string> InquiryBuildingConstructionStepId { get; set; }
        public IList<string> InquiryBuildingStatusId { get; set; }
        public IList<string> InquiryBuildingTypeId { get; set; }
        public IList<string> InquiryDocumentRequestTypeId { get; set; }
        public IList<string> InquiryFieldTypeId { get; set; }
        public IList<string> InquiryTransferTypeId { get; set; }
        public IList<string> InquiryEstateTaxUnitId { get; set; }
        public string InquiryEstatebasic { get; set; }
        public string InquiryEstatesecondary { get; set; }
        public bool InquiryEstateBasicRemaining { get; set; }
        public bool InquiryEstateSecondaryRemaining { get; set; }
        public IList<string> InquiryFieldUsingTypeId { get; set; }
        public decimal? InquiryFloorNo { get; set; }
        public bool InquiryHasSpecialTrance { get; set; }
        public bool InquiryHasSpecifiedTradingValue { get; set; }
        public bool InquiryIsFirstCession { get; set; }
        public bool InquiryIsFirstDeal { get; set; }
        public bool InquiryIsGroundLevel { get; set; }
        public bool InquiryIsLicenceReady { get; set; }
        public bool InquiryIsMissingSeparateDocument { get; set; }
        public bool InquiryIsWornTexture { get; set; }
        public string InquiryLastReceiveStatusDate { get; set; }
        public string InquiryLastReceiveStatusTime { get; set; }
        public string InquiryLastSendDate { get; set; }
        public string InquiryLastSendTime { get; set; }
        public string InquiryLicenseDate { get; set; }
        public decimal? InquiryLocationAssignRightDealCurrentValue { get; set; }
        public IList<string> InquiryLocationAssignRigthOwnershipTypeId { get; set; }
        public IList<string> InquiryLocationAssignRigthUsingTypeId { get; set; }
        public string InquiryNo { get; set; }
        public string InquiryNo2 { get; set; }
        public string InquiryPlateNo { get; set; }
        public bool InquiryPrevTransactionsAccordingToFacilitateRule { get; set; }
        public string InquiryRenovationRelatedBlockNo { get; set; }
        public string InquiryRenovationRelatedEstateNo { get; set; }
        public string InquiryRenovationRelatedRowNo { get; set; }
        public string InquirySeparationProcessNo { get; set; }
        public decimal? InquiryShareOfOwnership { get; set; }
        public string InquiryStatusDescription { get; set; }
        public string InquiryTaxAmount { get; set; }
        public string InquiryTaxBillHtml { get; set; }
        public string InquiryTaxBillIdentity { get; set; }
        public string InquiryTaxPaymentIdentity { get; set; }
        public decimal InquiryTimestamp { get; set; }
        public decimal? InquiryTotalArea { get; set; }
        public decimal? InquiryTotalOwnershipShare { get; set; }
        public string InquiryTrackingCode { get; set; }
        public decimal? InquiryTranceWidth { get; set; }
        public decimal? InquiryTransitionShare { get; set; }
        public decimal? InquiryValuebookletBlockNo { get; set; }
        public decimal? InquiryValuebookletRowNo { get; set; }
        public string InquiryWorkCompletionCertificateDate { get; set; }
        public string InquiryStatus { get; set; }
        public string InquiryShebaNo { get; set; }
        public string InquiryStatusTitle { get; set; }
        public EstateTaxInquiryOwnerViewModel TheInquiryOwner { get; set; }
        public List<EstateTaxInquiryBuyerViewModel> TheInquiryBuyersList { get; set; }
        public List<EstateTaxInquiryAttachViewModel> TheInquiryEstateAttachList { get; set; }
        public EstateTaxInquiryExtraParam ExtraParams { get; set; }

        public bool IsTehranEstate { get; set; }
        public bool SepartionNoIsReadOnly { get; set; }
        public bool TotalAreaIsReadOnly { get; set; }
    }

    public class EstateTaxInquiryOwnerViewModel : EstateBasePersonViewModel
    {


    }
    public class EstateTaxInquiryBuyerViewModel : EstateBasePersonViewModel
    {
        public IList<string> PersonRelationTypeId { get; set; }
        public decimal PersonSharePart { get; set; }
        public decimal PersonShareTotal { get; set; }
    }
    public partial class EstateTaxInquiryAttachViewModel : EntityState
    {
        public string AttachId { get; set; }
        public decimal AttachArea { get; set; }
        public string AttachBlock { get; set; }
        public IList<string> AttachEstatePieceTypeId { get; set; }
        public IList<string> AttachEstateSideTypeId { get; set; }
        public string AttachFloor { get; set; }
        public string AttachPiece { get; set; }
        public decimal AttachTimeStamp { get; set; }
        public string AttachEstatePieceTypeTitle { get; set; }
        public string AttachEstateSideTypeTitle { get; set; }


    }
}
