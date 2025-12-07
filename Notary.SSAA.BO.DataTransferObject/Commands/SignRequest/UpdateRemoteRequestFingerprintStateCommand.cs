using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.SignRequest
{
    public class UpdateRemoteRequestFingerprintStateCommand : BaseCommandRequest<ApiResult<SignRequestViewModel>>
    {
        public string SignRequestNo { get; set; }
        public string PersonNationalNo { get; set; }
    }
}
