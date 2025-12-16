using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IExecutiveRequestPersonRepository : IRepository<ExecutiveRequestPerson>
    {
        Task<ExecutiveRequestRelatedPersonLookupRepositoryObject> GetExecutiveRequestAgentedLookupItems(int pageIndex, int pageSize,string executiveRequestId, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<ExecutiveRequestRelatedPersonLookupRepositoryObject> GetExecutiveRequestAgentLookupItems(int pageIndex, int pageSize,string executiveRequestId, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);
    
    
    }
}
