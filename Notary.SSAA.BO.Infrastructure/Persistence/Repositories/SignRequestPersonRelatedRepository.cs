using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class SignRequestPersonRelatedRepository : Repository<SignRequestPersonRelated>, ISignRequestPersonRelatedRepository
    {
        public SignRequestPersonRelatedRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<SignRequestPersonRelated>> LoadSignRequestPersonRelated(Guid signRequestId, string scriptoriumId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x=>x.AgentType).Where(x => x.SignRequestScriptoriumId == scriptoriumId && x.SignRequestId == signRequestId).ToListAsync(cancellationToken);
        }
    }
}
