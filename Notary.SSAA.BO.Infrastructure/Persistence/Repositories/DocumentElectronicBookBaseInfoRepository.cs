using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    internal sealed class DocumentElectronicBookBaseInfoRepository : Repository<DocumentElectronicBookBaseinfo>, IDocumentElectronicBookBaseInfoRepository
    {
        public DocumentElectronicBookBaseInfoRepository ( ApplicationContext dbContext ) : base ( dbContext )
        {

        }
        
          public async Task<List<DocumentElectronicBookBaseinfo>> GetElectronicBooks ( List<string> details, CancellationToken cancellationToken, string scriptoriumID )
        {
            return await TableNoTracking.Where ( t => t.ScriptoriumId == scriptoriumID ).ToListAsync ( cancellationToken );
        }
    }
}
