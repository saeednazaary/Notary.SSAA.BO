using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IElzamArtSixRepository : IRepository<Domain.Entities.SsrArticle6Inq>
    {
        public Task<Domain.Entities.SsrArticle6Inq> GetElzamArtSixGetById (Guid ElzamArtSixId, CancellationToken cancellationToken);

        Task<ElzamArtSixGrid> GetElzamArtSixGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
        SortData gridSortInput, IList<Guid> selectedItemsIds, List<string> FieldsNotInFilterSearch , string branchCode, ElzamArtSixGridExtraParams extraParams,
        CancellationToken cancellationToken, bool isOrderBy = false);
    }
}
