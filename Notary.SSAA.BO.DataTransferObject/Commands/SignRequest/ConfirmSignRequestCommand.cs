using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.SignRequest
{
    public class ConfirmSignRequestCommand : BaseCommandRequest<ApiResult<SignRequestViewModel>>
    {
        public ConfirmSignRequestCommand()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>();
            SignRequestSignedObject = new();
        }
        public IList<SignElectronicBookSignedViewModel> ElectronicBookSignedObjects { get; set; }
        public SignRequestSignedViewModel SignRequestSignedObject { get; set; }

        public string SignRequestId { get; set; }
        public List<VerifySignViewModel> SignValueIdList { get; set; }
    }
}
