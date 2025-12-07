using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface ISignRequestSemaphoreRepository : IRepository<SignRequestSemaphore>
    {
        Task<bool> IsConfirmAllowed(string ScriptoriumId, CancellationToken cancellationToken);
        Task<SignRequest> GetSignRequestForDigitalSign(Guid signRequestId, CancellationToken cancellationToken);
        Task<List<SignElectronicBook>> GetSignElectronicBookForDigitalSign(Guid signRequestId, CancellationToken cancellationToken);
    }
}
