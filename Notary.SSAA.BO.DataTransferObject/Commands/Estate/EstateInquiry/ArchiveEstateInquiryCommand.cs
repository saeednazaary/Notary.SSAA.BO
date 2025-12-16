using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry
{
    public class ArchiveEstateInquiryCommand : BaseCommandRequest<ApiResult>
    {
        public ArchiveEstateInquiryCommand(string estateInquiryId)
        {
            EstateInquiryId = estateInquiryId;
        }
        public string EstateInquiryId { get; set; }
    }
}
