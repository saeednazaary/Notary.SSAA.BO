using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    internal class DocumentSMSRepository : Repository<DocumentSm>, IDocumentSMSRepository
    {
        public DocumentSMSRepository ( ApplicationContext context ) : base ( context )
        {
        }

  
    }
}
