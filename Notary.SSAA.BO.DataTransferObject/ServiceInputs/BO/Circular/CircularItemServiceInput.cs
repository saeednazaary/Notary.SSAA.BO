

using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Circular;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.BO.Circular
{
    public class CircularItemServiceInput : BaseExternalRequest<ApiResult<CircularItem>>
    {
        public string CircularInfoId { get; set; }
        public string CircularItemId { get; set; }
    }
}
