using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry
{
    public class GetEstateOwnersByInquiryIdQuery : BaseQueryRequest<ApiResult<List<DSURealLegalPersonObject>>>
    {
        public GetEstateOwnersByInquiryIdQuery(string estateInquiryId)
        {
            EstateInquiryId = estateInquiryId;
        }
        public string EstateInquiryId { get; set; }

    }
}
