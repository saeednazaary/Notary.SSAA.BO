using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Text.Json.Serialization;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry
{
    public class GetNewEstateInquiryByCopyQuery : BaseQueryRequest<ApiResult<EstateInquiryViewModel>>
    {
        public GetNewEstateInquiryByCopyQuery(string estateInquiryId)
        {
            EstateInquiryId = estateInquiryId;
        }
        public string EstateInquiryId { get; set; }

    }
}
