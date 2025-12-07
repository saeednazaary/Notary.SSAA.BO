using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry
{
    public class EstateInquiryResponsePrintQuery : BaseQueryRequest<ApiResult<EstateInquiryResponsePrintViewModel>>
    {
        public EstateInquiryResponsePrintQuery(string estateInquiryId)
        {
            EstateInquiryId = estateInquiryId;
        }

        public string EstateInquiryId { get; set; }
    }
}
