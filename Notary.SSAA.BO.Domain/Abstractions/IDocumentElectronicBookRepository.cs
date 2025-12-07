using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentElectronicBookRepository : IRepository<DocumentElectronicBook>
    {

        Task<DocumentElectronicBook> GetDocumentElectronicBook(string? nationalNo, CancellationToken cancellationToken);
        Task<long?> GetLastClassifyNoFromDigitalBook(string scriptoriumId, CancellationToken cancellationToken);
        Task<List<DocumentElectronicBook>> GetDocumentElectronicBooks(List<string> ids,
            CancellationToken cancellationToken);

    }
}
