using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface ISignElectronicBookRepository : IRepository<SignElectronicBook>
    {
        Task<int> GetLastClassifyNo(string ScriptoriumId, CancellationToken cancellationToken);
    }
}
