using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    internal class SsrSignEbookBaseinfoRepository : Repository<SsrSignEbookBaseinfo>, ISsrSignEbookBaseinfoRepository
    {
        public SsrSignEbookBaseinfoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetLastClassifyNo(string ScriptoriumId, CancellationToken cancellationToken)
        {
            int? lastClassifyNo= await TableNoTracking.Where(x => x.ScriptoriumId == ScriptoriumId).MaxAsync(x => (int?)x.LastClassifyNo,cancellationToken);

            if (lastClassifyNo > 0 && lastClassifyNo != null)
            {
                return lastClassifyNo.Value;
            }
            else
            {
                return 0;
            }
        }
    }
}
