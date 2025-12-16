using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface ISignRequestFileRepository : IRepository<SignRequestFile>
    {
        Task<SignRequestFile> GetSignRequestFile(Guid signRequestId, CancellationToken cancellationToken);
        Task<SignRequestFile> GetSignRequestEdmFile(Guid signRequestId, CancellationToken cancellationToken);

    }
}
