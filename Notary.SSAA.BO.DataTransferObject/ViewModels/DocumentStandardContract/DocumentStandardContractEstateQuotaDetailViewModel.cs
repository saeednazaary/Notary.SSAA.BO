using SixLabors.ImageSharp.ColorSpaces;
using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractEstateQuotaDetailViewModel: EntityState
    {
       
        public string DocumentEstateQuotaDetailId { get; set; }
        
        public string DocumentPersonSellerTitle => DocumentPersonSellerName + " " + DocumentPersonSellerFamily;
        public string DocumentPersonSellerName { get; set; }
        public string DocumentPersonSellerFamily { get; set; }
        public string DocumentPersonBuyerTitle => DocumentPersonBuyerName + " " + DocumentPersonBuyerFamily;
        public string DocumentPersonBuyerName { get; set; }
        public string DocumentPersonBuyerFamily { get; set; }

        public string DocumentEstateId { get; set; }

        public IList<string> DocumentPersonSellerId { get; set; }

        public IList<string> DocumentPersonBuyerId { get; set; }
        public IList<string> EstateOwnerShipId { get; set; }

        public string OwnershipDetailQuota { get; set; }

        public string OwnershipTotalQuota { get; set; }
        public string EstateInquiriesId { get; set; }
        public decimal? OwnershipDetailQuotaInquiry { get; set; }

        public decimal? OwnershipTotalQuotaInquiry { get; set; }
        public string DealSummaryPersonConditions { get; set; }
        public string InquiryPersonQuotaText { get; set; }

        public string SellDetailQuota { get; set; }

        public string SellTotalQuota { get; set; }

        public string QuotaText { get; set; }
    }
}
