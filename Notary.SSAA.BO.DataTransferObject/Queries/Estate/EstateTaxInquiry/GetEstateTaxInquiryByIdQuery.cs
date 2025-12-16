using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry
{
    public class GetEstateTaxInquiryByIdQuery : BaseQueryRequest<ApiResult<EstateTaxInquiryViewModel>>
    {
        public GetEstateTaxInquiryByIdQuery()
        {

        }
        public string EstateTaxInquiryId { get; set; }
        public string LegacyId { get; set; }
        public string RelatedServer { get; set; }
    }
}
