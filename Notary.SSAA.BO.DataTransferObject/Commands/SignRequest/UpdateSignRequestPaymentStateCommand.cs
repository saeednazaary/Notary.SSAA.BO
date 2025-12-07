using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.SignRequest
{
    public class UpdateSignRequestPaymentStateCommand : BaseCommandRequest<ApiResult<UpdateSignRequestPaymentStateViewModel>>
    {
        public string SignRequestId { get; set; }
        public string SignRequestNo { get; set; }
        public bool InquiryMode { get; set; }
    }
}
