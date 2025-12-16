using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Text.Json.Serialization;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry
{
    public class GetEstateInquiryByIdQuery : BaseQueryRequest<ApiResult<EstateInquiryViewModel>>
    {
        public GetEstateInquiryByIdQuery()
        {
            
        }
        public string EstateInquiryId { get; set; }
        public string LegacyId { get; set; }
        public string RelatedServer { get; set; }
    }
}
