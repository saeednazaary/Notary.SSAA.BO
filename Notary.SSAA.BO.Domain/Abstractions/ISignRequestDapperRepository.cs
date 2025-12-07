using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface ISignRequestDapperRepository:IDRepository
    {
        Task<SignRequestGrid> GetSignRequestGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput,
    IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, SignRequestSearchExtraParams extraParams,
    CancellationToken cancellationToken, bool isOrderBy = false);
    }
}
