using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface ISignRequestPersonRepository : IRepository<SignRequestPerson>
    {

        public Task<SignRequestAgentPersonLookupRepositoryObject> GetSignRequestRelatedPersonLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, SignRequestAgentPersonExtraParams ExtraParams,string scriptoriumId, CancellationToken cancellationToken, bool isOrderBy = false);

        public Task<SignRequestAgentPersonLookupRepositoryObject> GetSignRequestOriginalPersonLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, SignRequestAgentPersonExtraParams ExtraParams, CancellationToken cancellationToken, bool isOrderBy = false);
    }

}

