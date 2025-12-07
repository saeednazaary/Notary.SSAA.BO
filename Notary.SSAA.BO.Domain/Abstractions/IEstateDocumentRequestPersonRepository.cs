using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IEstateDocumentRequestPersonRepository:IRepository<EstateDocumentRequestPerson>
    {
        Task<EstateDocumentRequestPersonLookupRepositoryObject> GetEstateDocumentRequestAgentPersons(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string estateDocumentRequestId, bool isOrderBy, CancellationToken cancellationToken);
        Task<EstateDocumentRequestPersonLookupRepositoryObject> GetEstateDocumentRequestOwnerPerson(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string estateDocumentRequestId, bool isOrderBy, CancellationToken cancellationToken);
    }
}
