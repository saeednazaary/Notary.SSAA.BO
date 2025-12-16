using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IEstateSeriDaftarRepository : IRepository<EstateSeridaftar>
    {
        Task<EstateSeriDaftarLookupViewModel> GetEstateSeriDaftarItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch,  string unitId, bool isOrderBy, CancellationToken cancellationToken);
    }
}
