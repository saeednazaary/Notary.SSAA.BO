using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IElzamArtSixCountyRepository : IRepository<SsrArticle6County>
    {
        Task<ElzamArtSixCountyLookupRepositoryObject> GetElzamArtSixCountyLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
        SortData gridSortInput,IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, ElzamArtSixCountyLookupExtraParams extraParams,
        CancellationToken cancellationToken, bool isOrderBy = false);
    }
}