using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentFileRepository : IRepository<DocumentFile>
    {
        Task<DocumentFile> GetDocumentFile(Guid documentId, CancellationToken cancellationToken);

    }
}
