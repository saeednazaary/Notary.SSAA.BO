using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint
{
    public class GetInquiryPersonFingerprintListQuery : BaseQueryRequest<ApiResult<List<GetInquiryPersonFingerprintRepositoryObject>>>
    {
        public GetInquiryPersonFingerprintListQuery(string mainObjectId, List<string> personObjectIds)
        {
            MainObjectId = mainObjectId;
            PersonNationalNos = personObjectIds;
        }
        public string MainObjectId { get; set; }
        public List<string> PersonNationalNos { get; set; }
    }
}
