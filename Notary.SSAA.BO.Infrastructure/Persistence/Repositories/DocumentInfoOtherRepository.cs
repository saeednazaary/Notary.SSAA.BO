using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;
using Notary.SSAA.BO.Domain.RepositoryObjects.Document;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    internal sealed class DocumentInfoOtherRepository : Repository<DocumentInfoOther>, IDocumentInfoOtherRepository
    {
        public DocumentInfoOtherRepository ( ApplicationContext dbContext ) : base ( dbContext )
        {
        }

        public Task<DocumentInfoOtherObject> GetDocumentInfoOtherInformation(Guid documentId)
        {
           return TableNoTracking.Where(t => t.DocumentId == documentId)
                .Select(t => new DocumentInfoOtherObject() { AssetTypeId = t.DocumentAssetTypeId, DocumentTypeSubjectId = t.DocumentTypeSubjectId})
                .FirstOrDefaultAsync();
        }
    }
}
