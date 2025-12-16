using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Text.Json.Serialization;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry
{
    public class GetEstateTaxInquiryLastNo2Query : BaseQueryRequest<ApiResult<EstateInquiryLastNoViewModel>>
    {
        public GetEstateTaxInquiryLastNo2Query()
        {

        }
        public string Year { get; set; }
        public string ScriptoriumCode { get; set; }
    }
}
