using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.DealSummary
{
    public class UnRestrictDealSummaryCommand : BaseCommandRequest<ApiResult>
    {
        public UnRestrictDealSummaryCommand()
        {
            
        }
        public string RemoveRestrictionNo { get; set; }
        public string RemoveRestrictionDate { get; set; }
        public string RemoveRestrictionTypeId { get; set; }
        public string DealSummaryId { get; set; }
    }
}
