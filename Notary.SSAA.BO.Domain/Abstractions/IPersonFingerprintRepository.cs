using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IPersonFingerprintRepository : IRepository<PersonFingerprint>
    {
        Task<List<GetInquiryPersonFingerprintRepositoryObject>> GetInquiryPersonFingerprint(List<string> nationalNos, string mainObjectId, CancellationToken cancellationToken);
        Task<GetInquiryPersonFingerprintRepositoryObject> GetInquiryPersonFingerprint(string nationalNo, string mainObjectId, CancellationToken cancellationToken);
        Task<GetInquiryPersonFingerprintRepositoryObject> GetInquiryPersonFingerprint(string fingerprintId, CancellationToken cancellationToken);
        Task<List<GetPersonFingerprintImageRepositoryObject>> GetPersonFingerprintImage(List<string> nationalNos, string mainObjectId, CancellationToken cancellationToken);
        Task<List<GetPersonFingerprintImageRepositoryObject>> GetPersonFingerprintImage(string mainObjectId, CancellationToken cancellationToken);
        Task<List<SignablePersonRepositoryObject>> GetPersonFingerprint(string mainObjectId, CancellationToken cancellationToken);
        Task<PersonFingerprint> GetLastFingerprint(List<string> mainObjectIds,string nationalNo, string sabteAhvalCodeFinger, List<string> notInscriptoriumIds, 
            CancellationToken cancellationToken);
        Task<string> GetLastFingerprintDateTime(string mainObjectId, CancellationToken cancellationToken);
    }
}
