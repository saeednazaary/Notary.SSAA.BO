using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry
{
    public class ValidateEstateInquiryForDivisionRequestQuery : BaseQueryRequest<ApiResult<List<EstateInquiryValidationResultViewModel>>>
    {
        public ValidateEstateInquiryForDivisionRequestQuery(string[] estateInquiryId)
        {
            EstateInquiryId = estateInquiryId;
            
        }
        public string[] EstateInquiryId { get; set; }
        

    }
}
