using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IEstateTaxCityRepository:IRepository<EstateTaxCity>
    {
        Task<BaseLookupRepositoryObject> GetEstateTaxCityItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, bool isOrderBy,string countyId, CancellationToken cancellationToken);
    }
}
