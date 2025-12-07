using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry
{
    public class ExistsActiveEstateTaxInquiryQuery : BaseQueryRequest<ApiResult<ExistsActiveEstateTaxInquiryViewModel>>
    {
        public ExistsActiveEstateTaxInquiryQuery()
        {

        }
        public string EstateInquiryId { get; set; }
       
    }
}
