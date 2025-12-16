

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class StageSignRequestViewModel
    {
        public StageSignRequestViewModel()
        {
            SignRequestSignObject = new();
            SignElectronicBookSignObject = new List<SignElectronicBookSignViewModel>();
        }
        public SignRequestSignViewModel SignRequestSignObject { get; set; }
        public IList<SignElectronicBookSignViewModel> SignElectronicBookSignObject { get; set; }
    }
}
