using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.SharedKernel.Result;
using SSAA.Notary.DataTransferObject.ViewModels.Estate.DealSummary;


namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.BO.Estate.DealSummary
{
    public class SendDealSummaryBOServiceInput : BaseExternalRequest<ApiResult<DealSummaryServiceOutputViewModel>>
    {
        public SendDealSummaryInput<DealSummaryServiceOutputViewModel> SendDealSummaryInput { get; set; }        
    }
    public class DealSummaryVerificationBOServiceInput: BaseExternalRequest<ApiResult<DealSummaryVerificationResultViewModel>>
    {        
        public DealSummaryVerificationInput<DealSummaryVerificationResultViewModel> DealSummaryVerificationInput { get; set; }      
    }
    public class DealSummaryVerificationWithoutOwnerCheckingBOServiceInput : BaseExternalRequest<ApiResult<DealSummaryVerificationResultViewModel>>
    {        
        public DealSummaryVerificationWithoutOwnerCheckingInput<DealSummaryVerificationResultViewModel> DealSummaryVerificationWithoutOwnerCheckingInput { get; set; }
    }

}
