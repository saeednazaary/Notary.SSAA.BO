using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry
{
    public class CreateEstateTaxInquiryCommand : BaseCommandRequest<ApiResult>
    {
        public int? RequestType { get; set; }
        public bool IsValid { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDirty { get; set; }
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
        public string InquiryLicenseDate { get; set; }
        public decimal? InquiryLocationAssignRightDealCurrentValue { get; set; }
        public IList<string> InquiryLocationAssignRigthOwnershipTypeId { get; set; }
        public IList<string> InquiryLocationAssignRigthUsingTypeId { get; set; }        
        public string InquiryPlateNo { get; set; }
        public bool InquiryPrevTransactionsAccordingToFacilitateRule { get; set; }
        public string InquiryRenovationRelatedBlockNo { get; set; }
        public string InquiryRenovationRelatedEstateNo { get; set; }
        public string InquiryRenovationRelatedRowNo { get; set; }        
        public string InquirySeparationProcessNo { get; set; }
        public decimal? InquiryShareOfOwnership { get; set; }        
        public decimal? InquiryTotalArea { get; set; }
        public decimal? InquiryTotalOwnershipShare { get; set; }        
        public decimal? InquiryTranceWidth { get; set; }
        public decimal? InquiryTransitionShare { get; set; }
        public decimal? InquiryValuebookletBlockNo { get; set; }
        public decimal? InquiryValuebookletRowNo { get; set; }
        public string InquiryWorkCompletionCertificateDate { get; set; }        
        public EstateTaxInquiryOwnerViewModel TheInquiryOwner { get; set; }
        public List<EstateTaxInquiryBuyerViewModel> TheInquiryBuyersList { get; set; }
        public List<EstateTaxInquiryAttachViewModel> TheInquiryEstateAttachList { get; set; }
    }
}
