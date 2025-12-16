using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint
{
    public class MatchPersonFingerprintCommand : BaseCommandRequest<ApiResult<GetInquiryPersonFingerprintRepositoryObject>>
    {
        public string FingerprintId { get; set; }
        public string FingerprintImageFile { get; set; }
        public string FingerprintImageType { get; set; }
        public string FingerprintImageHeight { get; set; }
        public string FingerprintImageWidth { get; set; }
        public string FingerprintScannerDeviceType { get; set; }

    }
    public class MatchPersonFingerprintV2Command : BaseCommandRequest<ApiResult>
    {
        public string FingerprintId { get; set; }
        public string FingerprintImageFile { get; set; }
        public string FingerprintImageType { get; set; }
        public string FingerprintImageHeight { get; set; }
        public string FingerprintImageWidth { get; set; }
        public string FingerprintScannerDeviceType { get; set; }

    }
}
