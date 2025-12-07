using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.DealSummary
{
    public class SendDealSummaryQuery<T> : BaseQueryRequest<ApiResult<T>> where T : class
    {
        public List<DSUDealSummaryObject> DsuDealSummary { get; set; }
        public object Tag { get; set; }
        public bool? IsRemoveRestrictionDealSummary { get; set; }
    }
}
