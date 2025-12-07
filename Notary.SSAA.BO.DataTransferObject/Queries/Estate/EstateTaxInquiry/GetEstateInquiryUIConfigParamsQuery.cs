using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry
{
    public class GetEstateTaxInquiryUIConfigParamsQuery : BaseQueryRequest<ApiResult<GetEstateInquiryUIConfigParamsViewModel>>
    {
        public GetEstateTaxInquiryUIConfigParamsQuery()
        {

        }
        public string ConfigName { get; set; }

    }
}
