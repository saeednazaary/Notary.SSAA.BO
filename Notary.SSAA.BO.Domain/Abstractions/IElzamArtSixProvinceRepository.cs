using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IElzamArtSixProvinceRepository : IRepository<SsrArticle6Province>
    {
        Task<ElzamArtSixProvinceLookupRepositoryObject> GetElzamArtSixProvinceLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
        SortData gridSortInput,IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, ElzamArtSixProvinceLookupExtraParams extraParams,
        CancellationToken cancellationToken, bool isOrderBy = false);
    }
}