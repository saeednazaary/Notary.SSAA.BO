using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IPersonFingerTypeRepository : IRepository<PersonFingerType>
    {
        Task<List<GetPersonFingerType>> GetAllPersonFingerTypes( CancellationToken cancellationToken);

    }
}
