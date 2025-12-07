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
using Polly;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    internal sealed class DocumentElectronicBookRepository : Repository<DocumentElectronicBook>,
        IDocumentElectronicBookRepository
    {
        public readonly ApplicationContext DbContext;

        public DocumentElectronicBookRepository(ApplicationContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;

        }


        public async Task<DocumentElectronicBook> GetDocumentElectronicBook(string? nationalNo,
            CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(t => t.NationalNo == nationalNo).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<long?> GetLastClassifyNoFromDigitalBook(string scriptoriumId, CancellationToken cancellationToken)
        {
            var lastClassifyNo = await TableNoTracking
                .Join(DbContext.DocumentElectronicBookBaseinfos,
                    ondb => new { ondb.ScriptoriumId },
                    onbbi => new { onbbi.ScriptoriumId },
                    (ondb, onbbi) => new { ONotaryDigitalDocumentsBook = ondb, ONOTARYDIGITALBOOKBASEINFO = onbbi })
                .Where(x => x.ONotaryDigitalDocumentsBook.ScriptoriumId == scriptoriumId &&
                            x.ONOTARYDIGITALBOOKBASEINFO.ScriptoriumId == scriptoriumId &&
                            x.ONotaryDigitalDocumentsBook.ClassifyNo > x.ONOTARYDIGITALBOOKBASEINFO.LastClassifyNo)
                .MaxAsync(x => (long?)x.ONotaryDigitalDocumentsBook.ClassifyNo, cancellationToken);

            return lastClassifyNo;



            //return await DbContext.DocumentElectronicBooks
            //    .SelectMany ( order => DbContext.DocumentElectronicBookBaseinfos,
            //        ( documentElectronicBook, documentElectronicBookBaseinfos ) => new
            //            { documentElectronicBook, documentElectronicBookBaseinfos } )
            //    .Where ( x => x.documentElectronicBook.ScriptoriumId == scriptoriumId
            //                  && x.documentElectronicBookBaseinfos.ScriptoriumId == scriptoriumId &&
            //                  x.documentElectronicBook.ClassifyNo > x.documentElectronicBookBaseinfos.LastClassifyNo )
            //    .MaxAsync ( x => x.documentElectronicBook.ClassifyNo, cancellationToken );




        }

        public async Task<List<DocumentElectronicBook>> GetDocumentElectronicBooks(List<string> ids,
            CancellationToken cancellationToken)
        {
            List<Guid> idList = new List<Guid>();
            foreach (var id in ids)
            {
                idList.Add(Guid.Parse(id));
            }
            return await TableNoTracking.Where(t => idList.Contains(t.Id)).ToListAsync(cancellationToken);
        }
    }
}