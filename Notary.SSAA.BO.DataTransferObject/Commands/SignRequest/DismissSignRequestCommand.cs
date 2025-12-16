using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.SignRequest
{
    public class DismissSignRequestCommand : BaseCommandRequest<ApiResult<SignRequestViewModel>>
    {
        public string SignRequestId { get; set; }
        public string Modifier { get; set; }
    }
}
