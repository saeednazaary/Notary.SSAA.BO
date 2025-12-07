using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.DealSummary
{
    public class GetDealSummaryByIdQuery : BaseQueryRequest<ApiResult<DealSummaryViewModel>>
    {
        public GetDealSummaryByIdQuery()
        {
           
        }
        public string DealSummaryId { get; set; }
        public string LegacyId { get; set; }

    }
}
