using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentAgentTypeRepository : IRepository<AgentType>
    {
        Task<DocumentAgentTypeLookupRepositoryObject> GetDocumentAgentTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch,IList<string> CodesNotInAgetType, CancellationToken cancellationToken, bool isOrderBy = false);
    }
}
