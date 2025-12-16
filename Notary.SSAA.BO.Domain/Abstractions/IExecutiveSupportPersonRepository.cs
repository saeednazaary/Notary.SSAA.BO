using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IExecutiveSupportPersonRepository : IRepository<ExecutiveSupportPerson>
    {
        Task<ExecutiveSupportPersonLookupRepositoryObject> GetExecutiveSupportRequesterPersonLookupItems(int pageIndex, int pageSize, string executiveSupportId, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<ExecutiveSupportPersonLookupRepositoryObject> GetExecutiveSupportPersonAddressChangeLookupItems(int pageIndex, int pageSize, string executiveSupportId , string executiveSupportTypeId, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);
    }
}
