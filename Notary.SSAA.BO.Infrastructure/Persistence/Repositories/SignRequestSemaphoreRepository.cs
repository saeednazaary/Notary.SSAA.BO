using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.SharedKernel.Interfaces;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class SignRequestSemaphoreRepository : Repository<SignRequestSemaphore>, ISignRequestSemaphoreRepository
    {
        IDateTimeService _datetimeService;        

        public SignRequestSemaphoreRepository(ApplicationContext dbContext,IDateTimeService dateTimeService) : base(dbContext)
        {
            _datetimeService = dateTimeService;            
        }

        public async Task<bool> IsConfirmAllowed(string ScriptoriumId, CancellationToken cancellationToken)
        {
            return !await TableNoTracking.AnyAsync(x => x.ScriptoriumId == ScriptoriumId, cancellationToken);
        }

        public async Task<SignRequest> GetSignRequestForDigitalSign(Guid signRequestId, CancellationToken cancellationToken)
        {            
            var SemaphoreEntity = await Table
                  .Where(x => x.SignRequestId == signRequestId)
                  .FirstOrDefaultAsync(cancellationToken);
            if (SemaphoreEntity != null)
            {                
                var diff = _datetimeService.CurrentDateTime - SemaphoreEntity.RecordDate;
                if (diff.TotalSeconds <= 70)
                {                    
                    return JsonConvert.DeserializeObject<SignRequest>(SemaphoreEntity.NewSignRequestData);
                }
                else
                {                    
                    return null;
                }
            }                            
            return null;
        }

        public async Task<List<SignElectronicBook>> GetSignElectronicBookForDigitalSign(Guid signRequestId, CancellationToken cancellationToken)
        {
            var SemaphoreEntity = await Table
                  .Where(x => x.SignRequestId == signRequestId)
                  .FirstOrDefaultAsync(cancellationToken);
            if (SemaphoreEntity != null)
            {
                var diff = _datetimeService.CurrentDateTime - SemaphoreEntity.RecordDate;
                if (diff.TotalSeconds <= 70)
                    return JsonConvert.DeserializeObject<List<SignElectronicBook>>(SemaphoreEntity.SignElectronicBookData);
                else
                    return null;
            }
            return null;
        }        
    }
}
