using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class DocumentFileRepository : Repository<DocumentFile>, IDocumentFileRepository
    {
        private readonly ApplicationContext DbContext;

        public DocumentFileRepository(ApplicationContext context) : base(context)
        {
            DbContext = context;
        }
        public async Task<DocumentFile> GetDocumentFile(Guid DocumentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(x => x.DocumentId == DocumentId).FirstOrDefaultAsync(cancellationToken);

        }

     
    }
}
