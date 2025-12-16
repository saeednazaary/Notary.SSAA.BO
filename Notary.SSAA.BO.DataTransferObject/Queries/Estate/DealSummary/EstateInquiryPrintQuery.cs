using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.DealSummary
{
    public class DealSummaryPrintQuery : BaseQueryRequest<ApiResult<DealSummaryPrintViewModel>>
    {
        public DealSummaryPrintQuery(string dealSummaryId)
        {
            DealSummaryId = dealSummaryId;
        }

        public string DealSummaryId { get; set; }
    }
}
