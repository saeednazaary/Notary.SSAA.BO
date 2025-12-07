
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Domain.RepositoryObjects.SsrConfig;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface ISsrConfigRepository : IRepository<SsrConfig>
    {
        Task<SsrConfigAdvancedSearchGrid> GetSsrConfigGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput,
    IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, SsrConfigGridExtraParams extraParams,
    CancellationToken cancellationToken, bool isOrderBy = false);

        Task<SsrConfigAdvancedSearchGrid> GetLastSsrConfigGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput,
IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, SsrConfigGridExtraParams extraParams,
CancellationToken cancellationToken, bool isOrderBy = false);
        Task<List<SsrConfig>> GetBusinessConfig(SsrConfigRepositoryInput businessInput, CancellationToken cancellationToken);
    }
}
