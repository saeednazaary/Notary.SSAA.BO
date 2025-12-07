using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry
{
    public class GetTehranTaxBlockRadifQuery : BaseQueryRequest<ApiResult<EstateTaxInquiryServiceBlockRadifObject>>
    {
        public string ServiceID { get; set; }
        public int NosaziBlockNumber { get; set; }
        public int NosaziMelkNumber { get; set; }
        public int NosaziRadifNumber { get; set; }
        public string ClientId { get; set; }
        public GetTehranTaxBlockRadifQuery()
        {
            ClientId = "SSAR";
        }
    }
}
