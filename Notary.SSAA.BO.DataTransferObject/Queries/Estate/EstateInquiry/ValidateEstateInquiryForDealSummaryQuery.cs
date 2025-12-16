using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry
{
    public class ValidateEstateInquiryForDealSummaryQuery : BaseQueryRequest<ApiResult<List<EstateInquiryValidationResultViewModel>>>
    {
        public ValidateEstateInquiryForDealSummaryQuery(string[] estateInquiryId, bool isRestrictedDealSummary)
        {
            EstateInquiryId = estateInquiryId;
            IsRestrictedDealSummary = isRestrictedDealSummary;
        }
        public string[] EstateInquiryId { get; set; }
        public bool IsRestrictedDealSummary { get; set; }

    }
}
