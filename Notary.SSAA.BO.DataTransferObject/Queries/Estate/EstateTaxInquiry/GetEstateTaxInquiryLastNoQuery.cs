using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Text.Json.Serialization;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry
{
    public class GetEstateTaxInquiryLastNoQuery : BaseQueryRequest<ApiResult<EstateInquiryLastNoViewModel>>
    {
        public GetEstateTaxInquiryLastNoQuery()
        {

        }
        public string Year { get; set; }
        public string ScriptoriumCode { get; set; }


    }
}
