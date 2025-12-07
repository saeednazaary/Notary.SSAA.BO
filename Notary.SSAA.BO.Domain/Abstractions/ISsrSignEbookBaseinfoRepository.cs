using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface ISsrSignEbookBaseinfoRepository : IRepository<SsrSignEbookBaseinfo>
    {
        Task<int> GetLastClassifyNo(string ScriptoriumId, CancellationToken cancellationToken);
    }
}
