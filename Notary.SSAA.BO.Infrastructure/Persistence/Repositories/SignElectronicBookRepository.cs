using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    internal class SignElectronicBookRepository : Repository<SignElectronicBook>, ISignElectronicBookRepository
    {
        public SignElectronicBookRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetLastClassifyNo(string scriptoriumId, CancellationToken cancellationToken)
        {
            int? lastClassifyNo = await TableNoTracking
                .Where(x => x.ScriptoriumId == scriptoriumId)
                .MaxAsync(x => (int?)x.ClassifyNo, cancellationToken);

            return lastClassifyNo.GetValueOrDefault();
        }
    }
}
