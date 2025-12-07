using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry
{
    public class DeArchiveEstateInquiryCommand : BaseCommandRequest<ApiResult>
    {
        public DeArchiveEstateInquiryCommand(string estateInquiryId)
        {
            EstateInquiryId = estateInquiryId;
        }
        public string EstateInquiryId { get; set; }
    }
}
