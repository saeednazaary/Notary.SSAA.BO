using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry
{
    public class GetEstateInquiryListQuery : BaseQueryRequest<ApiResult<GetEstateInquiryListViewModel>>
    {
        public GetEstateInquiryListQuery()
        {

        }
        public string[] EstateInquiryId { get; set; }
    }
}
