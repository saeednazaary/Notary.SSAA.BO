using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry
{
    public class SendEstateInquiryCommand : BaseCommandRequest<ApiResult>
    {
        public SendEstateInquiryCommand(string estateInquiryId)
        {
            EstateInquiryId = estateInquiryId;
        }
        public string EstateInquiryId { get; set; }

    }
}
