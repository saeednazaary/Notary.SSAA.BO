using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint
{
    public class TakePersonFingerprintCommand : BaseCommandRequest<ApiResult<GetInquiryPersonFingerprintRepositoryObject>>
    {
        public string FingerprintId { get; set; }
        public string FingerprintImageFile { get; set; }
        public string PersonFingerTypeId { get; set; }
        public string FingerprintImageHeight { get; set; }
        public string FingerprintImageWidth { get; set; }
        public string FingerprintScannerDeviceType { get; set; }
        public string State { get; set; }

    }
    public class TakePersonFingerprintV2Command : BaseCommandRequest<ApiResult<GetInquiryPersonFingerprintRepositoryObject>>
    {
        public string FingerprintId { get; set; }
        public string FingerprintImageFile { get; set; }
        public string PersonFingerTypeId { get; set; }
        public string FingerprintImageHeight { get; set; }
        public string FingerprintImageWidth { get; set; }
        public string FingerprintScannerDeviceType { get; set; }
        public string State { get; set; }
        public string TfaState { get; set; }
    }
}
