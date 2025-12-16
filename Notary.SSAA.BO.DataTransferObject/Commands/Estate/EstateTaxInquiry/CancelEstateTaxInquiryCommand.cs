using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry
{
    public class CancelEstateTaxInquiryCommand : BaseCommandRequest<ApiResult>
    {
        public CancelEstateTaxInquiryCommand(string estateTaxInquiryId)
        {
            EstateTaxInquiryId = estateTaxInquiryId;
        }

        public string EstateTaxInquiryId { get; set; }
    }
}
