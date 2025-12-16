using   Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.BO.Estate.EstateInquiry
{
    public class GetEstateOwnersByInquiryIdInput : BaseExternalRequest<ApiResult<List<DSURealLegalPersonObject>>>
    {
        public GetEstateOwnersByInquiryIdInput(string estateInquiryId)
        {
            EstateInquiryId = estateInquiryId;
        }
        public string EstateInquiryId { get; set; }

    }
}
