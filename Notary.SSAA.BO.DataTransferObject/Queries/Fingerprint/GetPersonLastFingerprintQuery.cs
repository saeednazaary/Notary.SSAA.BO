using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint
{
    public class GetPersonLastFingerprintQuery : BaseExternalQueryRequest<ExternalApiResult<LastPersonFingerprintViewModel>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PersonNationalNo { get; set; }
        public string DocumentId { get; set; }
        public int SelectedFinger { get; set; }
        public string FingerprintRawImage { get; set; }
        public string FingerprintDevice { get; set; }
        public int FingerprintWidth { get; set; }
        public int FingerPrintHeight { get; set; }
    }
}

