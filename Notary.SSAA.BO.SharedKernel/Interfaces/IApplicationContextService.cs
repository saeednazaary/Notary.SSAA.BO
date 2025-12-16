using Notary.SSAA.BO.SharedKernel.Contracts.Security;

namespace Notary.SSAA.BO.SharedKernel.Interfaces
{
    public interface IApplicationContextService
    {
        public Task BeginTransactionAsync ( CancellationToken cancellationToken);
        public Task CommitTransactionAsync ( CancellationToken cancellationToken);
        public Task RollbackTransactionAsync ( CancellationToken cancellationToken );
        public Task SaveChangesAsync ( CancellationToken cancellationToken);

    }
}
