using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint
{
    public class GetInquiryPersonFingerprintQuery : BaseQueryRequest<ApiResult<GetInquiryPersonFingerprintRepositoryObject>>
    {
        public GetInquiryPersonFingerprintQuery(string fingerprintId)
        {
            FingerprintId = fingerprintId;
            ValidateById = true;
        }
        public GetInquiryPersonFingerprintQuery(string mainObjectId, string nationalNo)
        {
            MainObjectId = mainObjectId;
            NationalNo = nationalNo;
            ValidateById = false;
        }
        public string FingerprintId { get; set; }
        public string NationalNo { get; set; }
        public string MainObjectId { get; set; }
        public bool ValidateById { get; set; }
    }
}
