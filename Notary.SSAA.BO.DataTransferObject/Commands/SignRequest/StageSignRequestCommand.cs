using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.SignRequest
{
    public class StageSignRequestCommand : BaseCommandRequest<ApiResult<StageSignRequestViewModel>>
    {
        public string SignRequestId { get; set; }
    }
}
