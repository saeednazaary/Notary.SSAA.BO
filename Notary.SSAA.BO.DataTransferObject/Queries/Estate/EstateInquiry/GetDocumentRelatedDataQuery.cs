using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry
{
    public class GetDocumentRelatedDataQuery : BaseQueryRequest<ApiResult<GetDocumentRelatedDataViewModel>>
    {
        public GetDocumentRelatedDataQuery(string[] estateInquiryId)
        {
            EstateInquiryId = estateInquiryId;
        }
        public string[] EstateInquiryId { get; set; } 
        public string DocumentTypeCode { get; set; }
        public bool? IsAttachment {  get; set; }
        public bool? IsRegistered {  get; set; }
        public bool? CheckRepeatedRequest { get; set; }
    }
}
