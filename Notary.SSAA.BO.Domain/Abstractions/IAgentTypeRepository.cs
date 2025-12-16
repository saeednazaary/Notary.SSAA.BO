using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IAgentTypeRepository : IRepository<AgentType>
    {
        Task<BaseLookupRepositoryObject> GetSignRequestAgentTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> ResultFields, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);


    }
}
