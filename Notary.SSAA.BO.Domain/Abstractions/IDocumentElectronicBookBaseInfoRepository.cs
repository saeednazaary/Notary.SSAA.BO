using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentElectronicBookBaseInfoRepository : IRepository<DocumentElectronicBookBaseinfo>
    {
      
        Task<List<DocumentElectronicBookBaseinfo>> GetElectronicBooks ( List<string> details, CancellationToken cancellationToken,string scriptoriumID=null );

    }
}
