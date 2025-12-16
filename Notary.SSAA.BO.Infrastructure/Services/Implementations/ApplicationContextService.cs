using Notary.SSAA.BO.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Infrastructure.Contexts;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Services
{
    public class ApplicationContextService: IApplicationContextService
    {
        public readonly ApplicationContext ApplicationContext;
        public ApplicationContextService(ApplicationContext applicationContext )
        {
            ApplicationContext = applicationContext;
        }

        public async Task BeginTransactionAsync ( CancellationToken cancellationToken )
        {
           await  ApplicationContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync ( CancellationToken cancellationToken )
        {
            await ApplicationContext.Database.CommitTransactionAsync(cancellationToken);
        }
        public async Task RollbackTransactionAsync ( CancellationToken cancellationToken )
        {
            await ApplicationContext.Database.RollbackTransactionAsync( cancellationToken );
        }
        public async Task SaveChangesAsync ( CancellationToken cancellationToken )
        {
            await ApplicationContext.SaveChangesAsync( cancellationToken );
        }
    }
}
