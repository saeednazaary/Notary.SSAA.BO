using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.DealSummary;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class DealSummaryToByteArrayInput :BaseExternalRequest<ApiResult<byte[]>>
    {
        public DSUDealSummaryObject DSUDealSummaryObject { get; set; }
    }    
}
