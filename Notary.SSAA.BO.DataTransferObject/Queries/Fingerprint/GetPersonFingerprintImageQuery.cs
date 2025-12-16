using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint
{
    public class GetPersonFingerprintImageQuery : BaseQueryRequest<ApiResult<List<GetPersonFingerprintImageRepositoryObject>>>
    {
        public GetPersonFingerprintImageQuery(string mainObjectId)
        {
            MainObjectId = mainObjectId;
            ValidateAllPeople = true;
        }

        public GetPersonFingerprintImageQuery(string mainObjectId, List<string> personObjectIds)
        {
            MainObjectId = mainObjectId;
            PersonObjectIds = personObjectIds;
            ValidateAllPeople = false;
        }
        public string MainObjectId { get; set; }
        public bool ValidateAllPeople { get; set; }
        public List<string> PersonObjectIds { get; set; }
    }
}
