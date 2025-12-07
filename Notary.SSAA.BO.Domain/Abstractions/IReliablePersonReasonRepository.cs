using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IReliablePersonReasonRepository : IRepository<ReliablePersonReason>
    {
        Task<BaseLookupRepositoryObject> GetExecutiveRequestReliablePersonReasonLookupItems(int pageIndex, int pageSize, string agentTypeCode, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<BaseLookupRepositoryObject> GetSignRequestReliablePersonReasonLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> ResultFields, List<string> FieldsNotInFilterSearch, SignRequestReliablePersonReasonLookupExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false);

    }
}
