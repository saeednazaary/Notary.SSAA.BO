using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Text.Json.Serialization;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry
{
    public class GetNewFollowingEstateInquiryQuery : BaseQueryRequest<ApiResult<EstateInquiryViewModel>>
    {
        public GetNewFollowingEstateInquiryQuery(string estateInquiryId)
        {
            EstateInquiryId = estateInquiryId;
        }
        public string EstateInquiryId { get; set; }

    }
}
