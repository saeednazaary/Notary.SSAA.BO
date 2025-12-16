using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices
{
    public class FindLastFingerprintServiceInput : BaseExternalRequest<ApiResult<FindLastFingerprintServiceViewModel>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PersonNationalNo { get; set; }
        public string DocumentId { get; set; }
        public int SelectedFinger { get; set; }
    }
}
