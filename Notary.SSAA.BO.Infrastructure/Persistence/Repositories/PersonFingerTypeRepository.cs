using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;


namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class PersonFingerTypeRepository : Repository<PersonFingerType>, IPersonFingerTypeRepository
    {
        public PersonFingerTypeRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<GetPersonFingerType>> GetAllPersonFingerTypes(CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(x => x.State == "1").Select(x => new GetPersonFingerType
            {
                Code = x.Code,
                Title = x.Title,
                Id = x.Id,
                SabtahvalCode = x.SabtahvalCode
            }).OrderBy(x=>x.Code).ToListAsync(cancellationToken);
        }
    }
}
