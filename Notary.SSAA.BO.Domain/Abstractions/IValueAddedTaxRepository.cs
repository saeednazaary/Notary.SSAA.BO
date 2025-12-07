using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IValueAddedTaxRepository : IRepository<ValueAddedTax>
    {
      

        Task<ValueAddedTax> GetAddedTaxByCostTypeAndDateRange(string costTypeId,string currentDate,CancellationToken cancellationToken);

    }
}
