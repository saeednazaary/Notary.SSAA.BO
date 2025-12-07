using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IExecutivePersonPostTypeRepository : IRepository<ExecutivePersonPostType>
    {
        Task<BaseLookupRepositoryObject> GetExecutivePersonPostTypeLookupItems(int pageIndex, int pageSize , string executiveTypeCode , ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);
    }
}
