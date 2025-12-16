using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ReportTools
{
    public class FingerprintRAWToJPEGServiceInput : BaseExternalRequest<ApiResult<string>>
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Data { get; set; }
        public FingerDeviceType DeviceType { get; set; }
    }
}
