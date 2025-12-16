using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class DocumentEstateQuotaDetailRepository:Repository<DocumentEstateQuotaDetail>,IDocumentEstateQuotaDetailRepository
    {
        public DocumentEstateQuotaDetailRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<DocumentEstateQuotaDetail> GetDocumentEstateQuotaDetailForEstateDocumentRequest(Guid id, bool personIsSeller, bool isAttchmentTransfer, CancellationToken cancellationToken)
        {
            DocumentEstateQuotaDetail entity = null;
            if (!isAttchmentTransfer || !personIsSeller)
            {
                entity = await TableNoTracking
                .Include(x => x.DocumentEstate)
                .ThenInclude(x => x.Document)
                .Include(x => x.DocumentPersonBuyer)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            }
            else
            {
                entity = await TableNoTracking
            .Include(x => x.DocumentEstate)
            .ThenInclude(x => x.Document)
            .Include(x => x.DocumentPersonSeller)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            }


            return entity;
        }
        public async Task<List<DocumentEstateQuotaDetail>> CollectValidPersonQuotas ( Guid documentId, string estateInquiryId ,CancellationToken cancellationToken)
        {

            return await TableNoTracking.Include ( t => t.DocumentEstate )
                .Include ( t => t.DocumentEstateOwnershipDocument )
                .Where ( t => t.DocumentEstate.DocumentId == documentId &&
                              t.DocumentEstateOwnershipDocument.EstateInquiriesId == estateInquiryId ).ToListAsync ();
        }
    }
}
