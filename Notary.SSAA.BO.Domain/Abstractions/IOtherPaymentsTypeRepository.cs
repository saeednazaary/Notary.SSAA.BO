using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IOtherPaymentsTypeRepository : IRepository<OtherPaymentsType>
    {
        Task<OtherPaymentsTypeLookupQueryRepositoryObject> GetOtherPaymentsTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, SortData gridSortInput, string GlobalSearch, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);
    }
}
