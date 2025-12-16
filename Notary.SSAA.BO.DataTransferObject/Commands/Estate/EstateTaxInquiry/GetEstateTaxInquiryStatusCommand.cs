
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry
{
    public class GetEstateTaxInquiryStatusCommand:BaseCommandRequest<ApiResult>
    {
        public GetEstateTaxInquiryStatusCommand(string estateTaxInquiryId)
        {
            EstateTaxInquiryId = estateTaxInquiryId;
        }

        public string EstateTaxInquiryId { get; set; }
    }
}
