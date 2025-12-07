using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry
{
    public class GetNewEstateTaxInquiryByCopyQuery : BaseQueryRequest<ApiResult<EstateTaxInquiryViewModel>>
    {
        public GetNewEstateTaxInquiryByCopyQuery(string estateTaxInquiryId)
        {
            EstateTaxInquiryId = estateTaxInquiryId;
        }
        public string EstateTaxInquiryId { get; set; }

    }
}
