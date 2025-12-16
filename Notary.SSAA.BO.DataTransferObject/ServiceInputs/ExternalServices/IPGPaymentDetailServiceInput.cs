using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices
{
    public class IPGPaymentDetailServiceInput : BaseExternalRequest<ApiResult<IPGPaymentDetailServiceViewModel>>
    {
        public string SystemRequestId { get; set; }
        public string TrackingCode { get; set; }
        public string ClientId { get; set; }


    }

}
