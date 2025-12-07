using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;


namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class DealSummaryTransferTypeRepository : Repository<DealSummaryTransferType>,IDealSummaryTransferTypeRepository
    {
        public DealSummaryTransferTypeRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
       
    }
}
