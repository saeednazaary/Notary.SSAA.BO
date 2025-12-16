using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Text.Json.Serialization;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry
{
    public class GetEstateInquiryUIConfigParamsQuery : BaseQueryRequest<ApiResult<GetEstateInquiryUIConfigParamsViewModel>>
    {
        public GetEstateInquiryUIConfigParamsQuery()
        {

        }
        public string ConfigName { get; set; }

    }
}
