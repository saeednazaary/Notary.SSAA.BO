using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class GetTehranTaxBlockRadifInput : BaseExternalRequest<ApiResult<EstateTaxInquiryServiceBlockRadifObject>>
    {
        public string ServiceID { get; set; }
        public int NosaziBlockNumber { get; set; }
        public int NosaziMelkNumber { get; set; }
        public int NosaziRadifNumber { get; set; }
        public string ClientId { get; set; }
        public GetTehranTaxBlockRadifInput()
        {
            ClientId = "SSAR";
        }
    }
}
