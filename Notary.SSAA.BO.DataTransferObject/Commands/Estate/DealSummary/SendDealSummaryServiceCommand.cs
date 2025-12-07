using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;
using SSAA.Notary.DataTransferObject.ViewModels.Estate.DealSummary;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.DealSummary
{
    public class SendDealSummaryServiceCommand : BaseCommandRequest<ApiResult<DealSummaryServiceOutputViewModel>>
    {
        public List<DSUDealSummaryObject> DsuDealSummary { get; set; }
        public object Tag { get; set; }
        public bool? IsRemoveRestrictionDealSummary { get; set; }
    }
   
}
