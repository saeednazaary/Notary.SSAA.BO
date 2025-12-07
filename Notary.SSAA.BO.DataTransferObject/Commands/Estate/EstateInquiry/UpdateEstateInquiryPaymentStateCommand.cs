using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry
{
    public class UpdateEstateInquiryPaymentStateCommand : BaseCommandRequest<ApiResult<UpdateEstateInquiryPaymentStateViewModel>>
    {
        public UpdateEstateInquiryPaymentStateCommand(string estateInquiryId)
        {
            EstateInquiryId = estateInquiryId;
        }
        public string EstateInquiryId { get; set; }
        public bool InquiryMode { get; set; }
    }
}
